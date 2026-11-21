Imports System
Imports System.Web
Imports System.Data
Imports System.Data.SqlClient
Module GMod
    
    ' Connection string comes from Web.config <connectionStrings name="PoultryDb">.
    ' NOTE: members of a Module are shared by ALL users of the site. Pages use the GModule class instead.
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

    Public ClickVou_Type As String

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
            AppLog.Write(ex.Message & Environment.NewLine & sql, "GMod", "DataSetRet(" & tablename & ")", AppLog.CurrentUserArea())
        End Try
    End Sub
    Public Function SqlExecuteNonQuery(ByVal sql As String) As String
        Dim retval As String = ""
        If SqlConn.State = ConnectionState.Closed Then
            SqlConn.ConnectionString = Connstr
            SqlConn.Open()
        End If

        Dim sqlcmd As New SqlCommand(sql, SqlConn)
        Try
            sqlcmd.ExecuteNonQuery()
            retval = "SUCCESS"
        Catch ex As Exception
            retval = ex.Message
        End Try
        sqlcmd.Dispose()
        If SqlConn.State = ConnectionState.Open Then SqlConn.Close()
        Return retval
    End Function
  
    'Public Function getsession(ByVal d As Date) As String
    '    If d.Month > 3 Then
    '        Return Format(d.Year Mod 100, "00") & Format((d.Year + 1) Mod 100, "00")
    '    Else
    '        Return Format((d.Year - 1) Mod 100, "00") & Format(d.Year Mod 100, "00")
    '    End If
    'End Function

    Public Function getTime() As String
        Dim systime As String
        systime = Now().ToShortTimeString
        Return systime
    End Function
    Public Function getDate() As String
        Dim sysdate As String
        sysdate = Now().ToShortDateString
        Return sysdate
    End Function
    Public Function EncryptionStr(ByVal strval As String, ByVal isencrypt As Boolean) As String
        Dim lenstr, i, t, addval As Integer
        Dim chr1, tmpstr As String
        tmpstr = ""
        lenstr = Len(strval)
        If isencrypt = True Then
            addval = 11
        Else
            addval = -11
        End If
        For i = 0 To lenstr - 1
            chr1 = strval.Substring(i, 1)
            t = Asc(chr1) + addval
            tmpstr = tmpstr + chr(t).ToString
        Next
        EncryptionStr = tmpstr
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

End Module