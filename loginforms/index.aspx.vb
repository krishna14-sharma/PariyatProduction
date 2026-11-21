Imports System.Data

''' <summary>
''' Handles the sign-in form posted from index.html.
''' - Uses a parameterised query (the old code was open to SQL injection: user name  ' or 1=1 --  logged in).
''' - Redirects to the role's home page (Server.Transfer left the browser on index.aspx, so refresh re-posted the password).
''' - On failure goes back to the sign-in page with a message.
''' </summary>
Partial Class loginforms_index
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not String.Equals(Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) Then
            Response.Redirect("~/loginforms/index.html", False)
            Context.ApplicationInstance.CompleteRequest()
            Return
        End If

        If WindowsOnly() AndAlso Request.Browser.Platform <> "WinNT" Then
            BackToLogin("browser")
            Return
        End If

        Dim userName As String = Convert.ToString(Request.Form("username")).Trim()
        Dim password As String = Convert.ToString(Request.Form("pass"))
        If userName.Length = 0 OrElse password.Length = 0 Then
            BackToLogin("missing")
            Return
        End If

        Dim users As DataTable
        Try
            users = AppDb.GetTable(AppDb.Poultry,
                "select UserName, AreaCode, Role from [Users] where UserName = @user and Password = @pass",
                AppDb.P("@user", userName), AppDb.P("@pass", password))
        Catch ex As Exception
            AppLog.Write(ex, "Login")
            BackToLogin("server")
            Return
        End Try

        If users.Rows.Count = 0 Then
            AppLog.Write("Failed sign-in for '" & userName & "' from " & Request.UserHostAddress, "loginforms/index.aspx", "Login", "-")
            BackToLogin("invalid")
            Return
        End If

        Dim row As DataRow = users.Rows(0)
        Dim role As String = Convert.ToString(row("Role")).Trim().ToUpperInvariant()
        Dim home As String = HomePageFor(role)
        If home Is Nothing Then
            BackToLogin("role")
            Return
        End If

        ' Start from a clean session (release any reports from an earlier sign-in).
        CrystalReportManager.OnSessionEnd(Session)
        Session.Clear()
        Session("user_name") = Convert.ToString(row("UserName"))
        Session("role") = role
        If role = "OP" Then Session("area_code") = row("AreaCode")

        Response.Redirect(home, False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

    ''' <summary>Home page for each role (same mapping as before).</summary>
    Private Shared Function HomePageFor(ByVal role As String) As String
        Select Case role
            Case "OP" : Return "~/Default_Area.aspx"
            Case "HAT" : Return "~/Default_hatch.aspx"
            Case "CNF" : Return "~/Default.aspx"
            Case "PIT" : Return "~/Default_pit.aspx"
            Case "TRU" : Return "~/Default_Pvctray.aspx"
            Case "MIS" : Return "~/Default_Mis.aspx"
            Case "ATTENDANCE" : Return "~/Default_Attendance.aspx"
            Case Else : Return Nothing
        End Select
    End Function

    Private Shared Function WindowsOnly() As Boolean
        Return Not String.Equals(ConfigurationManager.AppSettings("Login:WindowsOnly"), "false", StringComparison.OrdinalIgnoreCase)
    End Function

    Private Sub BackToLogin(ByVal reason As String)
        Response.Redirect("~/loginforms/index.html?error=" & reason, False)
        Context.ApplicationInstance.CompleteRequest()
    End Sub

End Class
