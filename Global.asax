<%@ Application Language="VB" %>

<script runat="server">

    ' ---------------------------------------------------------------------------------
    ' Crystal Reports life-time management (fixes "Load report failed").
    ' See App_code/CrystalReportManager.vb for the details.
    ' ---------------------------------------------------------------------------------

    Sub Application_PostAcquireRequestState(ByVal sender As Object, ByVal e As EventArgs)
        CrystalReportManager.OnPostAcquireRequestState(HttpContext.Current)
    End Sub

    Sub Application_PreRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        CrystalReportManager.OnPreRequestHandlerExecute(HttpContext.Current)
    End Sub

    Sub Application_PostRequestHandlerExecute(ByVal sender As Object, ByVal e As EventArgs)
        CrystalReportManager.OnPostRequestHandlerExecute(HttpContext.Current)
    End Sub

    Sub Application_EndRequest(ByVal sender As Object, ByVal e As EventArgs)
        CrystalReportManager.OnEndRequest(HttpContext.Current)
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires for InProc session state when a session times out or is abandoned.
        CrystalReportManager.OnSessionEnd(Session)
    End Sub

    ' ---------------------------------------------------------------------------------
    ' Unhandled errors are written to Logs/ErrorLog.txt (users see ~/Error.aspx).
    ' ---------------------------------------------------------------------------------

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        Dim ex As Exception = Server.GetLastError()
        If ex Is Nothing Then Return
        If TypeOf ex Is HttpUnhandledException AndAlso ex.InnerException IsNot Nothing Then ex = ex.InnerException
        ' 404s are not application errors.
        Dim httpEx As HttpException = TryCast(ex, HttpException)
        If httpEx IsNot Nothing AndAlso httpEx.GetHttpCode() = 404 Then Return
        AppLog.Write(ex, "Unhandled error")
    End Sub

</script>
