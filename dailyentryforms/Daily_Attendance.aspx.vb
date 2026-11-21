
Partial Class dailyentryforms_Daily_Attendance
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim tblName As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            ' lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")

            sql = "select location_name from location_master where location_code='" & Session("area_code") & "'"
            gmod.DataSetRet(sql, "loc_name")
            hlabel.Text = gmod.ds.Tables("loc_name").Rows(0)(0).ToString() + ":Daily Attendance Entry"


            sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS EmpSerial,* from EmpMaster where Areacode='" & Session("area_code") & "'  and status =1"
            gmod.DataSetRet(sql, "fillempData")
            daily_att.DataSource = gmod.ds.Tables("fillempData")
            daily_att.DataBind()
            'lblprint.Text = ""
        Else

        End If
    End Sub

    Protected Sub btnAttSAve_Click(sender As Object, e As EventArgs) Handles btnAttSAve.Click
       If Session("user_name") Is Nothing OrElse
    String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Exit Sub
        End If

        If Session("area_code") Is Nothing OrElse
           String.IsNullOrWhiteSpace(Session("area_code").ToString()) Then
            Exit Sub
        End If


        Dim res As String = ""

        Try
            Dim inputDate As Date = Date.Parse(lbldate.Text)
            Dim year As Integer = inputDate.Year
            Dim month As Integer = inputDate.Month
            Dim day As Integer = inputDate.Day

            'Dim d1 As String = month + "/" + "26" + "/" + year
            'Dim d2 As String = month + "/" + "25" + "/" + year

            Dim d1 As Date = New Date(year, month, 26).AddMonths(-1)
            Dim d2 As Date = New Date(year, month, 25)

            For Each item As DataGridItem In daily_att.Items
                sql = " select count(*) absn from daily_attendance where loc='" & Session("area_code") & "' and [date] between '" & d1 & "' and '" & d2 & "' "
                sql &= "'and absent = 0 and leave = -1 and empcode='" & item.Cells(1).Text & "' "
                gmod.DataSetRet(sql, "absn1")

                If Val(gmod.ds.Tables("absn1").Rows(0)("absn")) >= 4 Then
                    Label1.Text = "Leave Limit Exceed" & item.Cells(1).Text
                    Exit Sub
                End If
            Next
        Catch ex As Exception

        End Try

        Try
            For Each item As DataGridItem In daily_att.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                'sql = "update [Standard_Winter] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                'qyeryresult = gmod.SqlExecuteNonQuery(sql)

                Dim chkabsent As CheckBox = CType(item.FindControl("chkAbsent"), CheckBox)
                Dim chkleave As CheckBox = CType(item.FindControl("chkLeave"), CheckBox)


                sql = "insert into daily_attendance([date], [empcode], [empname], [absent], [leave], [remark], [loc], [locname],emptype,sex) values("
                sql &= "'" & entrydate.Value.ToString & "',"
                sql &= "'" & item.Cells(1).Text & "',"
                sql &= "'" & item.Cells(2).Text & "',"
                sql &= "'" & CInt(chkabsent.Checked) & "',"
                sql &= "'" & CInt(chkleave.Checked) & "',"
                sql &= "'" & CType(item.FindControl("txtremerk"), TextBox).Text & "',"
                sql &= "'" & Session("area_code") & "',"
                sql &= "'-',"
                sql &= "'P',"
                sql &= "'" & item.Cells(6).Text & "')"

                res = gmod.SqlExecuteNonQuery(sql)

            Next

            Label1.Text = "Todat Attendance Saved..."
            daily_att.DataSource = Nothing
            daily_att.DataBind()
            Label1.Text = res
        Catch ex As Exception
            Label1.Text = res
        End Try
    End Sub
End Class
