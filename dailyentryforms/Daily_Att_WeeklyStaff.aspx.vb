Imports System.Data
Imports System.Data.SqlClient
Partial Class dailyentryforms_Daily_Att_WeeklyStaff
    Inherits System.Web.UI.Page
    Dim ConnstrWeelyStaffAtt As String = "Data Source=192.168.0.130;Initial Catalog=LabourMgt;User ID=sa;pwd=Ph@hoenix#g;Connect Timeout=6000"
    Dim SqlConnWeelyStaffAtt As New SqlConnection(ConnstrWeelyStaffAtt)
    Dim readerWeelyStaffAtt As SqlDataReader
    Dim dsWeelyStaffAtt, dsWeelyStaffAtt1 As New DataSet
    Dim sql As String
    Dim gmod As New GModule

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")
            sql = "select location_name from location_master where location_code='" & Session("area_code") & "'"
            gmod.DataSetRet(sql, "loc_name")
            hlabel.Text = gmod.ds.Tables("loc_name").Rows(0)(0).ToString() + ":Weekly Staff Attendance Entry"


            sql = "select AreaMaster from AreaMaster where AreaCode ='" & Session("area_code") & "' and isActive =1"
            Dim dadaatweekly1 As New SqlDataAdapter(sql, ConnstrWeelyStaffAtt)
            dadaatweekly1.Fill(dsWeelyStaffAtt1, "dsWeelyStaffAtt1")

            Label1.Text = dsWeelyStaffAtt1.Tables("dsWeelyStaffAtt1").Rows(0)(0).ToString



            'Sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS EmpSerial,* from EmpMaster where Areacode='" & Session("area_code") & "'  and status =1"

            sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS EmpSerial ,empid EmpCode, LabourName + '-'+ FatherName EmpName , Photo ,AreaName , Sex from [dbo].[LabourMaster] where "
            sql &= "  AreaName in (select AreaMaster from AreaMaster where AreaCode ='" & Session("area_code") & "' and Active =1)"

            Dim dadaatweekly As New SqlDataAdapter(sql, ConnstrWeelyStaffAtt)
            dadaatweekly.Fill(dsWeelyStaffAtt, "dsWeelyStaffAtt")

            daily_att.DataSource = dsWeelyStaffAtt.Tables("dsWeelyStaffAtt")
            daily_att.DataBind()
            'lblprint.Text = ""
        Else

        End If
    End Sub

    Protected Sub btnAttSAve_Click(sender As Object, e As EventArgs) Handles btnAttSAve.Click
        If Session("user_name") Is Nothing OrElse
       String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Label1.Text = "INVALID USER NAME"
            Exit Sub
        End If

        'If Session("user_name") = "" Then
        'Environment.Exit(0)
        'End If

        If Session("area_code") Is Nothing OrElse
           String.IsNullOrWhiteSpace(Session("area_code").ToString()) Then

            Exit Sub
        End If


        If String.IsNullOrWhiteSpace(entrydate.Value.ToString()) Then
            Exit Sub
        End If

        Dim res As String = ""
        Try


            For Each item As DataGridItem In daily_att.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                'sql = "update [Standard_Winter] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                'qyeryresult = gmod.SqlExecuteNonQuery(sql)

                Dim chkabsent As CheckBox = CType(item.FindControl("chkAbsent"), CheckBox)
                'Dim chkleave As CheckBox = CType(item.FindControl("chkLeave"), CheckBox)


                sql = "insert into daily_attendance([date], [empcode], [empname], [absent], [leave], [remark], [loc], [locname], emptype,sex) values("
                sql &= "'" & entrydate.Value.ToString & "',"
                sql &= "'" & item.Cells(1).Text & "',"
                sql &= "'" & item.Cells(2).Text & "',"
                sql &= "'" & CInt(chkabsent.Checked) & "',"
                sql &= "'0',"
                sql &= "'" & CType(item.FindControl("txtremerk"), TextBox).Text & "',"
                sql &= "'" & item.Cells(6).Text & "',"
                sql &= "'-',"
                sql &= "'W',"
                sql &= "'" & item.Cells(7).Text & "')"
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
