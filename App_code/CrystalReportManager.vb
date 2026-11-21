Imports System
Imports System.Collections.Generic
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web

''' <summary>
''' Fixes "Load report failed" by managing the life-time of every Crystal ReportDocument.
'''
''' Why the error happened:
'''   Every ReportDocument that is never Close()d/Dispose()d keeps a print-engine "job" and temp files open.
'''   Crystal allows only ~75 open jobs per server (PrintJobLimit). After enough report views the limit is hit
'''   and every report fails with "Load report failed" until IIS is recycled.
'''
''' What this class does (wired up from Global.asax, no per-page work required):
'''   1. Pages create reports with CrystalReportManager.NewReport() so each document is tracked for the request.
'''   2. After the page has rendered, the report shown in each CrystalReportViewer is remembered in Session
'''      (one per viewer per page), so paging / export / print / zoom keep working on postbacks and callbacks.
'''   3. Every other tracked document - and any document that was replaced - is closed and disposed immediately.
'''   4. When the session ends, all remaining documents in it are disposed.
''' </summary>
Public NotInheritable Class CrystalReportManager

    Private Const CreatedKey As String = "__CR_Created"
    Private Const SessionRefKey As String = "__CR_Session"
    Private Const FinishedKey As String = "__CR_Finished"
    Private Const ViewerSessionPrefix As String = "__CR_View:"

    Private Sub New()
    End Sub

    ' ------------------------------------------------------------------ API used by pages

    ''' <summary>Creates a new, tracked ReportDocument. Use instead of "New ReportDocument()".</summary>
    Public Shared Function NewReport() As ReportDocument
        Return Track(New ReportDocument())
    End Function

    ''' <summary>Tracks an existing document (for example a strongly typed report class) and returns it.</summary>
    Public Shared Function Track(Of T As ReportDocument)(ByVal doc As T) As T
        Dim ctx As HttpContext = HttpContext.Current
        If ctx Is Nothing OrElse doc Is Nothing Then Return doc
        Dim created As List(Of ReportDocument) = TryCast(ctx.Items(CreatedKey), List(Of ReportDocument))
        If created Is Nothing Then
            created = New List(Of ReportDocument)()
            ctx.Items(CreatedKey) = created
        End If
        If Not created.Contains(doc) Then created.Add(doc)
        Return doc
    End Function

    ''' <summary>Closes and disposes a document, ignoring errors (safe to call more than once).</summary>
    Public Shared Sub SafeDispose(ByVal doc As ReportDocument)
        If doc Is Nothing Then Return
        Try
            doc.Close()
        Catch
        End Try
        Try
            doc.Dispose()
        Catch
        End Try
    End Sub

    ' ------------------------------------------------------------------ pipeline hooks (called from Global.asax)

    ''' <summary>PostAcquireRequestState: remember the session object for use at the end of the request.</summary>
    Public Shared Sub OnPostAcquireRequestState(ByVal ctx As HttpContext)
        If ctx Is Nothing OrElse ctx.Session Is Nothing Then Return
        ctx.Items(SessionRefKey) = ctx.Session
    End Sub

    ''' <summary>PreRequestHandlerExecute: hook the page so remembered reports are re-attached on viewer postbacks.</summary>
    Public Shared Sub OnPreRequestHandlerExecute(ByVal ctx As HttpContext)
        If ctx Is Nothing Then Return
        Dim page As Page = TryCast(ctx.Handler, Page)
        If page Is Nothing Then Return
        AddHandler page.InitComplete, AddressOf Page_InitComplete
    End Sub

    ''' <summary>PostRequestHandlerExecute: the page has rendered - remember viewer reports, dispose the rest.</summary>
    Public Shared Sub OnPostRequestHandlerExecute(ByVal ctx As HttpContext)
        Finish(ctx, True)
    End Sub

    ''' <summary>EndRequest: safety net for requests that ended early (Response.End, Redirect, errors).</summary>
    Public Shared Sub OnEndRequest(ByVal ctx As HttpContext)
        Finish(ctx, False)
    End Sub

    ''' <summary>Session_End: dispose every report still held by the expiring session.</summary>
    Public Shared Sub OnSessionEnd(ByVal session As HttpSessionState)
        If session Is Nothing Then Return
        For Each doc As ReportDocument In ReportsIn(session)
            SafeDispose(doc)
        Next
    End Sub

    ' ------------------------------------------------------------------ internals

    Private Shared Sub Page_InitComplete(ByVal sender As Object, ByVal e As EventArgs)
        Dim page As Page = TryCast(sender, Page)
        If page Is Nothing OrElse Not page.IsPostBack Then Return
        Dim session As HttpSessionState = SessionOf(HttpContext.Current)
        If session Is Nothing Then Return

        For Each viewer As CrystalReportViewer In FindViewers(page)
            If viewer.ReportSource IsNot Nothing Then Continue For
            If Not IsViewerRequest(page, viewer) Then Continue For
            Dim key As String = ViewerKey(page, viewer)
            Dim doc As ReportDocument = TryCast(session(key), ReportDocument)
            If doc Is Nothing Then Continue For
            If IsAlive(doc) Then
                viewer.ReportSource = doc
            Else
                session.Remove(key)
            End If
        Next
    End Sub

    Private Shared Sub Finish(ByVal ctx As HttpContext, ByVal pageCompleted As Boolean)
        If ctx Is Nothing Then Return
        If ctx.Items(FinishedKey) IsNot Nothing Then Return
        ctx.Items(FinishedKey) = True

        Try
            Dim created As List(Of ReportDocument) = TryCast(ctx.Items(CreatedKey), List(Of ReportDocument))
            Dim candidates As New List(Of ReportDocument)()
            If created IsNot Nothing Then candidates.AddRange(created)

            Dim session As HttpSessionState = SessionOf(ctx)
            Dim page As Page = TryCast(ctx.Handler, Page)

            ' Remember what each viewer is showing so the next viewer postback can use it.
            If pageCompleted AndAlso session IsNot Nothing AndAlso page IsNot Nothing Then
                For Each viewer As CrystalReportViewer In FindViewers(page)
                    Dim doc As ReportDocument = TryCast(viewer.ReportSource, ReportDocument)
                    If doc Is Nothing Then Continue For
                    Dim key As String = ViewerKey(page, viewer)
                    Dim previous As ReportDocument = TryCast(session(key), ReportDocument)
                    session(key) = doc
                    If previous IsNot Nothing AndAlso previous IsNot doc Then candidates.Add(previous)
                Next
            End If

            If candidates.Count = 0 Then Return

            ' Without session information we cannot tell what is still needed: keep everything
            ' (Session_End will clean it up). This only happens on unusual aborted requests.
            If session Is Nothing Then
                If Not pageCompleted Then Return
            End If

            Dim keep As List(Of ReportDocument) = ReportsIn(session)
            For Each doc As ReportDocument In candidates
                If Not keep.Contains(doc) Then SafeDispose(doc)
            Next
        Catch ex As Exception
            AppLog.Write(ex, "CrystalReportManager.Finish")
        End Try
    End Sub

    ''' <summary>True when this postback/callback was raised by the viewer itself (page navigation, export, print...).</summary>
    Private Shared Function IsViewerRequest(ByVal page As Page, ByVal viewer As CrystalReportViewer) As Boolean
        Dim form As System.Collections.Specialized.NameValueCollection = page.Request.Form
        Dim uid As String = viewer.UniqueID
        Dim target As String = form("__EVENTTARGET")
        If Not String.IsNullOrEmpty(target) AndAlso target.StartsWith(uid, StringComparison.Ordinal) Then Return True
        Dim callbackId As String = form("__CALLBACKID")
        If Not String.IsNullOrEmpty(callbackId) AndAlso callbackId.StartsWith(uid, StringComparison.Ordinal) Then Return True
        If Not String.IsNullOrEmpty(form("crprint")) Then Return True
        For Each name As String In form.AllKeys
            If name IsNot Nothing AndAlso name.StartsWith(uid & "$", StringComparison.Ordinal) Then Return True
        Next
        Return False
    End Function

    Private Shared Function IsAlive(ByVal doc As ReportDocument) As Boolean
        Try
            Return doc.IsLoaded
        Catch
            Return False
        End Try
    End Function

    Private Shared Function ViewerKey(ByVal page As Page, ByVal viewer As CrystalReportViewer) As String
        Return ViewerSessionPrefix & page.AppRelativeVirtualPath.ToLowerInvariant() & "|" & viewer.UniqueID
    End Function

    Private Shared Function SessionOf(ByVal ctx As HttpContext) As HttpSessionState
        If ctx Is Nothing Then Return Nothing
        If ctx.Session IsNot Nothing Then Return ctx.Session
        Return TryCast(ctx.Items(SessionRefKey), HttpSessionState)
    End Function

    Private Shared Function ReportsIn(ByVal session As HttpSessionState) As List(Of ReportDocument)
        Dim result As New List(Of ReportDocument)()
        If session Is Nothing Then Return result
        Try
            For Each key As String In session.Keys
                Dim doc As ReportDocument = TryCast(session(key), ReportDocument)
                If doc IsNot Nothing AndAlso Not result.Contains(doc) Then result.Add(doc)
            Next
        Catch
        End Try
        Return result
    End Function

    Private Shared Function FindViewers(ByVal root As Control) As List(Of CrystalReportViewer)
        Dim result As New List(Of CrystalReportViewer)()
        CollectViewers(root, result)
        Return result
    End Function

    Private Shared Sub CollectViewers(ByVal parent As Control, ByVal result As List(Of CrystalReportViewer))
        If parent Is Nothing Then Return
        For Each child As Control In parent.Controls
            Dim viewer As CrystalReportViewer = TryCast(child, CrystalReportViewer)
            If viewer IsNot Nothing Then
                result.Add(viewer)
            ElseIf child.HasControls() Then
                CollectViewers(child, result)
            End If
        Next
    End Sub

End Class
