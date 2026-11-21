
Partial Class attendance_Daily_atttendancearea
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub showemp_Click(sender As Object, e As EventArgs) Handles showemp.Click
        sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS EmpSerial,* from EmpMaster  where AreaCode = '" & arealist.SelectedValue.ToString & "'  and status=1  order by Empname"
        gmod.DataSetRet(sql, "areaemp_daily_att")
        daily_att.DataSource = gmod.ds.Tables("areaemp_daily_att")
        daily_att.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
            ' lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Else
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select  distinct AreaCode from EmpMaster where status=1 order by AreaCode"
        gmod.DataSetRet(sql, "Dept")
        arealist.DataSource = gmod.ds.Tables("Dept")
        arealist.DataTextField = "AreaCode"
        arealist.DataValueField = "AreaCode"
        arealist.DataBind()

    End Sub


    Protected Sub btnAttSAve_Click(sender As Object, e As EventArgs) Handles btnAttSAve.Click
        'Try
        '    Dim inputDate As Date = Date.Parse(lbldate.Text)
        '    Dim year As Integer = inputDate.Year
        '    Dim month As Integer = inputDate.Month
        '    Dim day As Integer = inputDate.Day

        '    Dim d1 As String = month + "/" + "26" + "/" + year
        '    Dim d2 As String = month + "/" + "25" + "/" + year
        '    For Each item As DataGridItem In daily_att.Items
        '        sql = " select count(*) absn from daily_attendance where loc='" & arealist.SelectedValue & "' and [date] between '" & d1 & "' and '" & d2 & "' "
        '        sql &= "'and absent = 0 and leave = -1 and empcode='" & item.Cells(1).Text & "' "
        '        gmod.DataSetRet(sql, "absn1")

        '        If Val(gmod.ds.Tables("absn1").Rows(0)("absn")) >= 4 Then
        '            Label1.Text = "Leave Limit Exceed" & item.Cells(1).Text
        '            Exit Sub
        '        End If
        '    Next
        'Catch ex As Exception

        'End Try
        Dim res As String = ""
        Try
            For Each item As DataGridItem In daily_att.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                'sql = "update [Standard_Winter] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                'qyeryresult = gmod.SqlExecuteNonQuery(sql)

                Dim chkabsent As CheckBox = CType(item.FindControl("chkAbsent"), CheckBox)
                Dim chkleave As CheckBox = CType(item.FindControl("chkLeave"), CheckBox)

                sql = "insert into daily_attendance([date], [empcode], [empname], [absent], [leave], [remark], [loc], [locname], emptype, sex) values("
                sql &= "'" & lbldate.SelectedDate.ToShortDateString & "',"
                sql &= "'" & item.Cells(1).Text & "',"
                sql &= "'" & item.Cells(2).Text & "',"
                sql &= "'" & CInt(chkabsent.Checked) & "',"
                sql &= "'" & CInt(chkleave.Checked) & "',"
                sql &= "'" & CType(item.FindControl("txtremerk"), TextBox).Text & "',"
                sql &= "'" & arealist.SelectedValue & "',"
                sql &= "'-',"
                sql &= "'P',"
                sql &= "'" & item.Cells(6).Text & "')"
                res = gmod.SqlExecuteNonQuery(sql)
            Next
            daily_att.DataSource = Nothing
            daily_att.DataBind()
            Label1.Text = res
            'Label1.Text = "Attendance Saved..."
        Catch ex As Exception
            Label1.Text = res
        End Try
    End Sub
End Class
