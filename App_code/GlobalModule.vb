Imports System
Imports System.Web
Imports System.Data.SqlClient
Imports System.Data
Public Class GlobalModule

    Public userid As Integer = 0
    Public username As String
    Public rolename As String = ""
    Public status As String = ""
    Public firmid As Integer = 0
    Public firmname As String = ""
    ' Connection string comes from Web.config <connectionStrings name="FeedDb">.
    Public Connstr As String = AppDb.ConnectionString(AppDb.Feed)

    Public SqlConn As New SqlConnection(Connstr)
    Public ds As New DataSet
    Public Function session(ByVal scss As Date)
        If scss.Month > 3 Then
            Return scss.Date.Year
        Else
            Return scss.Date.Year - 1
        End If
    End Function
    Public Sub DataSetRet(ByVal sql As String, ByVal tablename As String)
        Try
            ds.Tables(tablename).Clear()
        Catch ex As Exception

        End Try

        Try
            Dim adp As New SqlDataAdapter(sql, Connstr)
            adp.Fill(ds, tablename)
            adp.Dispose()
        Catch ex As Exception
            AppLog.Write(ex.Message & Environment.NewLine & sql, "GlobalModule", "DataSetRet(" & tablename & ")", AppLog.CurrentUserArea())
        End Try
    End Sub
    Public Function SqlExecuteNonQuery(ByVal sql As String) As String

        ' Note: returns "Sucess" (original spelling) because existing pages compare against it.
        Try
            Using conn As New SqlConnection(Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    Return "Sucess"
                End Using
            End Using
        Catch ex As Exception
            AppLog.Write(ex.Message & Environment.NewLine & sql, "GlobalModule", "SqlExecuteNonQuery", AppLog.CurrentUserArea())
            Return ex.Message
        End Try
    End Function
    Public sp1(3) As String
    Function spliterbook(ByVal st As String)
        Try
            Dim word() As String
            word = st.Split(" ")
            sp1(0) = ""
            sp1(1) = ""
            sp1(2) = ""
            Dim i As Integer
            For i = 0 To word.Length - 1
                'earlier it was 55
                If sp1(0).Length < 50 Then
                    sp1(0) = sp1(0) & word(i) & " "
                Else
                    sp1(1) = sp1(1) & word(i) & " "
                End If
            Next
            word = sp1(1).Split(" ")
            sp1(1) = ""
            For i = 0 To word.Length - 1
                If sp1(1).Length < 50 Then
                    sp1(1) = sp1(1) & word(i) & " "
                Else
                    sp1(2) = sp1(2) & word(i) & " "
                End If
            Next
        Catch ex As Exception
            ' MsgBox cannot be used on a web server (it fails under IIS); log instead.
            AppLog.Write(ex.Message, "GlobalModule", "spliterbook", AppLog.CurrentUserArea())
        End Try
    End Function

End Class
