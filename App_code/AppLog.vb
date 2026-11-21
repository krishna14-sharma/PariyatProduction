Imports System
Imports System.IO
Imports System.Web

''' <summary>
''' Thread-safe error log. Writes to ~/Logs/ErrorLog.txt in the same format the application already used,
''' so existing log readers keep working.
''' </summary>
Public NotInheritable Class AppLog

    Private Shared ReadOnly SyncRoot As New Object()

    Private Sub New()
    End Sub

    ''' <summary>Writes one entry. All arguments are optional text; failures while logging are ignored.</summary>
    Public Shared Sub Write(ByVal message As String, ByVal formName As String, ByVal eventName As String, ByVal area As String)
        Try
            Dim logPath As String = ResolveLogPath()
            If logPath Is Nothing Then Return
            Dim dir As String = Path.GetDirectoryName(logPath)
            SyncLock SyncRoot
                If Not Directory.Exists(dir) Then Directory.CreateDirectory(dir)
                Using writer As New StreamWriter(logPath, True)
                    writer.WriteLine("----- " & DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") & " -----")
                    writer.WriteLine(area)
                    writer.WriteLine(formName)
                    writer.WriteLine(eventName)
                    writer.WriteLine(message)
                    writer.WriteLine("-----------------------------------------------------")
                End Using
            End SyncLock
        Catch
            ' Never let logging break the page.
        End Try
    End Sub

    ''' <summary>Logs an exception, adding the current user, area and page automatically.</summary>
    Public Shared Sub Write(ByVal ex As Exception, ByVal eventName As String)
        If ex Is Nothing Then Return
        Write(ex.ToString(), CurrentPage(), eventName, CurrentUserArea())
    End Sub

    ''' <summary>"user - location" for the current request, or "-" when not signed in.</summary>
    Public Shared Function CurrentUserArea() As String
        Try
            Dim ctx As HttpContext = HttpContext.Current
            If ctx IsNot Nothing AndAlso ctx.Session IsNot Nothing Then
                Return Convert.ToString(ctx.Session("user_name")) & "- " & Convert.ToString(ctx.Session("location_name"))
            End If
        Catch
        End Try
        Return "-"
    End Function

    Private Shared Function CurrentPage() As String
        Try
            Dim ctx As HttpContext = HttpContext.Current
            If ctx IsNot Nothing Then Return ctx.Request.AppRelativeCurrentExecutionFilePath
        Catch
        End Try
        Return ""
    End Function

    Private Shared Function ResolveLogPath() As String
        Dim ctx As HttpContext = HttpContext.Current
        If ctx IsNot Nothing Then Return ctx.Server.MapPath("~/Logs/ErrorLog.txt")
        If HttpRuntime.AppDomainAppPath IsNot Nothing Then
            Return Path.Combine(HttpRuntime.AppDomainAppPath, "Logs", "ErrorLog.txt")
        End If
        Return Nothing
    End Function

End Class
