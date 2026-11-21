Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.IO

Public Class GModule

    ' Connection string comes from Web.config <connectionStrings name="PoultryDb">.
    Public Connstr As String = AppDb.ConnectionString(AppDb.Poultry)
    Public SqlConn As New SqlConnection(Connstr)
    Public reader As SqlDataReader
    Public ds As New DataSet
    Public session As String
    Public Cmpid As String
    Public CmpName As String
    Public username As String
    Public ACC_HEAD As String
    Public VENTRY As String
    Public Voutype As String
    Public cust As String
    Public Vou_no As String
    Public Vou_Code As String
    Public role As String
    Public vat_type As String

    Public BrandName As String
    Public ItemName As String
    Public Type As String
    Public Location As String
    Public timeflag As Integer
    Public Sub DataSetRet(ByVal sql As String, ByVal tablename As String)
        Try
            ds.Tables(tablename).Clear()
            ds.Tables(tablename).Rows.Clear()
        Catch ex As Exception

        End Try

        Try
            'SqlConn.ConnectionString = Connstr
            Dim adp As New SqlDataAdapter(sql, Connstr)
            adp.Fill(ds, tablename)
            adp.Dispose()
        Catch ex As Exception
            ' Errors used to be silently ignored; they are now written to Logs/ErrorLog.txt.
            AppLog.Write(ex.Message & Environment.NewLine & sql, CurrentPagePath(), "DataSetRet(" & tablename & ")", AppLog.CurrentUserArea())
        End Try

    End Sub
    Public Function SqlExecuteNonQuery(ByVal sql As String) As String
        'Dim retval As String = ""
        'If SqlConn.State = ConnectionState.Closed Then
        '    SqlConn.ConnectionString = Connstr
        '    SqlConn.Open()
        'End If

        'Dim sqlcmd As New SqlCommand(sql, SqlConn)
        'Try
        '    sqlcmd.ExecuteNonQuery()
        '    retval = "SUCCESS"
        'Catch ex As Exception
        '    retval = ex.Message
        'End Try
        'sqlcmd.Dispose()
        'If SqlConn.State = ConnectionState.Open Then SqlConn.Close()
        'Return retval

        Try
            Using conn As New SqlConnection(Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    Return "SUCCESS"
                End Using
            End Using
        Catch ex As Exception
            AppLog.Write(ex.Message & Environment.NewLine & sql, CurrentPagePath(), "SqlExecuteNonQuery", AppLog.CurrentUserArea())
            Return ex.Message
        End Try
    End Function

    Private Shared Function CurrentPagePath() As String
        If HttpContext.Current Is Nothing Then Return ""
        Return HttpContext.Current.Request.AppRelativeCurrentExecutionFilePath
    End Function

    Public Function Getsession(ByVal statusdate As DateTime) As String
        Dim year, x As Integer
        Dim yearstrsuff, yearstrpfx As String
        If statusdate.Month > 3 Then
            year = statusdate.Year
            x = year + 1
            yearstrsuff = year.ToString.Substring(2, 2)
            yearstrpfx = x.ToString.Substring(2, 2)
            Return yearstrsuff & yearstrpfx
        Else
            year = statusdate.Year - 1
            x = year + 1
            yearstrsuff = year.ToString.Substring(2, 2)
            yearstrpfx = x.ToString.Substring(2, 2)
            Return yearstrsuff & yearstrpfx
        End If
    End Function

    Public Sub LogErrorToFile(ByVal errorMessage As String, ByVal errorForm As String, ByVal errorEvent As String, ByVal errorArea As String)
        ' Thread-safe logging (several users can write at the same time).
        AppLog.Write(errorMessage, errorForm, errorEvent, errorArea)
    End Sub

End Class
