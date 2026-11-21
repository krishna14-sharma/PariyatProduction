<%@ Page Language="VB" AutoEventWireup="true" %>

<script runat="server">
    ' Signs the user out: releases their Crystal reports, ends the session and returns to the sign-in page.
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        CrystalReportManager.OnSessionEnd(Session)
        Session.Clear()
        Session.Abandon()
        FormsAuthentication.SignOut()
        Response.Redirect("~/loginforms/index.html?signedout=1", False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub
</script>

<!DOCTYPE html>
<html lang="en">
<head><meta charset="utf-8" /><title>Signing out</title></head>
<body><p><a href="loginforms/index.html">Sign in again</a></p></body>
</html>
