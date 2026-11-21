Imports System
Imports System.Web
Imports System.Configuration
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Web.Caching
Imports System.Web.Hosting
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls

''' <summary>
''' Decides whether a page may use ASP.NET AJAX partial rendering (UpdatePanel).
'''
''' Partial rendering is switched OFF automatically for pages that cannot work inside an UpdatePanel:
'''   * pages with a Crystal Reports viewer (the viewer needs full postbacks for paging/export/print),
'''   * pages with a file upload control,
'''   * pages listed in Web.config appSetting "Ajax:FullPostBackPages" (semicolon separated, e.g. Excel export pages),
''' and globally when appSetting "Ajax:PartialRendering" is "false".
''' </summary>
Public NotInheritable Class AjaxPolicy

    Private Sub New()
    End Sub

    ''' <summary>Returns Nothing when partial rendering is allowed, otherwise the reason it is not.</summary>
    Public Shared Function FullPostBackReason(ByVal page As Page) As String
        If page Is Nothing Then Return "no page"

        Dim globalSetting As String = ConfigurationManager.AppSettings("Ajax:PartialRendering")
        If String.Equals(globalSetting, "false", StringComparison.OrdinalIgnoreCase) Then Return "disabled in Web.config"

        Dim listed As String = ConfigurationManager.AppSettings("Ajax:FullPostBackPages")
        If Not String.IsNullOrEmpty(listed) Then
            Dim current As String = page.AppRelativeVirtualPath
            For Each item As String In listed.Split(New Char() {";"c, ","c}, StringSplitOptions.RemoveEmptyEntries)
                If String.Equals(item.Trim(), current, StringComparison.OrdinalIgnoreCase) Then Return "listed in Web.config"
            Next
        End If

        Dim reason As String = MarkupReason(page)
        If reason IsNot Nothing Then Return reason
        Return FindBlockingControl(page)
    End Function

    ''' <summary>
    ''' Looks at the page's .aspx markup. This works before the UpdatePanel has created its child controls
    ''' (the ScriptManager must decide during its own Init). Result is cached until the file changes.
    ''' </summary>
    Private Shared Function MarkupReason(ByVal page As Page) As String
        Dim path As String = page.AppRelativeVirtualPath
        If String.IsNullOrEmpty(path) Then Return Nothing
        Dim key As String = "AjaxPolicy:" & path.ToLowerInvariant()
        Dim cached As String = TryCast(HttpRuntime.Cache(key), String)
        If cached IsNot Nothing Then
            If cached.Length = 0 Then Return Nothing
            Return cached
        End If

        Dim reason As String = ""
        Try
            Dim physical As String = HostingEnvironment.MapPath(path)
            If physical IsNot Nothing AndAlso File.Exists(physical) Then
                Dim markup As String = Regex.Replace(File.ReadAllText(physical), "<%--.*?--%>", "", RegexOptions.Singleline)
                If markup.IndexOf("CrystalReportViewer", StringComparison.OrdinalIgnoreCase) >= 0 Then
                    reason = "report viewer"
                ElseIf Regex.IsMatch(markup, "<asp:FileUpload\b|type\s*=\s*[""']?file\b", RegexOptions.IgnoreCase) Then
                    reason = "file upload"
                End If
                HttpRuntime.Cache.Insert(key, reason, New CacheDependency(physical))
            End If
        Catch ex As Exception
            AppLog.Write(ex, "AjaxPolicy")
        End Try
        If reason.Length = 0 Then Return Nothing
        Return reason
    End Function

    Private Shared Function FindBlockingControl(ByVal parent As Control) As String
        For Each child As Control In parent.Controls
            If TypeOf child Is FileUpload OrElse TypeOf child Is HtmlInputFile Then Return "file upload"
            If child.GetType().FullName = "CrystalDecisions.Web.CrystalReportViewer" Then Return "report viewer"
            If child.HasControls() Then
                Dim reason As String = FindBlockingControl(child)
                If reason IsNot Nothing Then Return reason
            End If
        Next
        Return Nothing
    End Function

End Class

''' <summary>
''' Shows messages to the user as a toast. Works for normal postbacks AND AJAX (UpdatePanel) postbacks.
''' Replaces Response.Write(...), which breaks AJAX postbacks.
''' </summary>
Public NotInheritable Class AppUi

    Private Sub New()
    End Sub

    Public Shared Sub ShowMessage(ByVal page As Page, ByVal message As Object)
        Show(page, message, "info")
    End Sub

    Public Shared Sub ShowSuccess(ByVal page As Page, ByVal message As Object)
        Show(page, message, "success")
    End Sub

    Public Shared Sub ShowError(ByVal page As Page, ByVal message As Object)
        Show(page, message, "error")
    End Sub

    ''' <summary>kind: "info", "success" or "error".</summary>
    Public Shared Sub Show(ByVal page As Page, ByVal message As Object, ByVal kind As String)
        If page Is Nothing Then Return
        Dim text As String = Convert.ToString(message)
        If String.IsNullOrEmpty(text) Then Return
        ' A plain "SUCCESS" from SqlExecuteNonQuery is shown as a friendly success message.
        If String.Equals(text, "SUCCESS", StringComparison.OrdinalIgnoreCase) AndAlso kind = "info" Then
            text = "Saved successfully."
            kind = "success"
        End If
        Dim script As String = "if(window.App){App.toast(""" & System.Web.HttpUtility.JavaScriptStringEncode(text) & """,""" & kind & """);}else{alert(""" & System.Web.HttpUtility.JavaScriptStringEncode(text) & """);}"
        Dim key As String = "appmsg_" & Guid.NewGuid().ToString("N")
        If ScriptManager.GetCurrent(page) IsNot Nothing Then
            ScriptManager.RegisterStartupScript(page, GetType(AppUi), key, script, True)
        Else
            page.ClientScript.RegisterStartupScript(GetType(AppUi), key, script, True)
        End If
    End Sub

End Class

Namespace PoultryApp.Controls

    ''' <summary>
    ''' ScriptManager used by every master page. Before initialising, it asks AjaxPolicy whether this page
    ''' can use partial rendering, and turns it off when it cannot (report viewer, file upload, listed pages).
    ''' </summary>
    Public Class AppScriptManager
        Inherits ScriptManager

        Private _fullPostBackReason As String

        ''' <summary>Why partial rendering was turned off for this page (Nothing when AJAX is active).</summary>
        Public ReadOnly Property FullPostBackReason() As String
            Get
                Return _fullPostBackReason
            End Get
        End Property

        Protected Overrides Sub OnInit(ByVal e As EventArgs)
            ' Must be decided before ScriptManager initialises (the property cannot change afterwards).
            If Not DesignMode AndAlso Page IsNot Nothing AndAlso EnablePartialRendering Then
                _fullPostBackReason = AjaxPolicy.FullPostBackReason(Page)
                If _fullPostBackReason IsNot Nothing Then EnablePartialRendering = False
            End If
            MyBase.OnInit(e)
        End Sub

    End Class

End Namespace
