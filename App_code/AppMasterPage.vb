Imports System
Imports System.Configuration
Imports System.Web
Imports System.Web.UI

''' <summary>
''' Base class for all master pages: sign-in check, current user details for the header,
''' and the "AJAX active" flag used by the page shell.
''' </summary>
Public Class AppMasterPage
    Inherits System.Web.UI.MasterPage

    ''' <summary>The login page all masters send anonymous users to.</summary>
    Public Const LoginUrl As String = "~/loginforms/index.html"

    Protected Overrides Sub OnInit(ByVal e As EventArgs)
        MyBase.OnInit(e)
        If RequiresLogin() AndAlso Session("user_name") Is Nothing Then
            Response.Redirect(LoginUrl & "?expired=1", True)
        End If
    End Sub

    ''' <summary>Set appSetting "Auth:RequireLogin" to "false" to switch off the master-page sign-in check.</summary>
    Protected Overridable Function RequiresLogin() As Boolean
        Dim setting As String = ConfigurationManager.AppSettings("Auth:RequireLogin")
        Return Not String.Equals(setting, "false", StringComparison.OrdinalIgnoreCase)
    End Function

    ''' <summary>Signed-in user name (empty when not signed in).</summary>
    Public ReadOnly Property CurrentUserName() As String
        Get
            Return Convert.ToString(Session("user_name"))
        End Get
    End Property

    ''' <summary>Farm / location of the signed-in user, when known.</summary>
    Public ReadOnly Property CurrentLocation() As String
        Get
            Dim loc As String = Convert.ToString(Session("location_name"))
            If String.IsNullOrEmpty(loc) Then loc = Convert.ToString(Session("area_code"))
            Return loc
        End Get
    End Property

    ''' <summary>First letter of the user name, for the avatar circle.</summary>
    Public ReadOnly Property UserInitial() As String
        Get
            Dim n As String = CurrentUserName
            If String.IsNullOrEmpty(n) Then Return "?"
            Return n.Substring(0, 1).ToUpperInvariant()
        End Get
    End Property

    ''' <summary>"true" / "false" for the page shell script: is AJAX partial rendering active on this page?</summary>
    Public ReadOnly Property AjaxActiveJs() As String
        Get
            Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)
            If sm IsNot Nothing AndAlso sm.EnablePartialRendering Then Return "true"
            Return "false"
        End Get
    End Property

End Class
