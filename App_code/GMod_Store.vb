Imports System
Imports System.Web

Imports System.Data
Imports System.Data.SqlClient
Public Class GModule_Store

    ' Connection string comes from Web.config <connectionStrings name="StoreDb">.
    Public Connstr As String = AppDb.ConnectionString(AppDb.Store)

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
            AppLog.Write(ex.Message & Environment.NewLine & sql, "GModule_Store", "DataSetRet(" & tablename & ")", AppLog.CurrentUserArea())
        End Try
    End Sub
    Public Function SqlExecuteNonQuery(ByVal sql As String) As String
        Try
            Using conn As New SqlConnection(Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    Return "SUCCESS"
                End Using
            End Using
        Catch ex As Exception
            AppLog.Write(ex.Message & Environment.NewLine & sql, "GModule_Store", "SqlExecuteNonQuery", AppLog.CurrentUserArea())
            Return ex.Message
        End Try
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
End Class


