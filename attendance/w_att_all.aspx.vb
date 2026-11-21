Imports System.Data
Imports System.Data.SqlClient
Partial Class attendance_w_att_all
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim ConnstrWStaffAtt As String = "Data Source=192.168.0.130;Initial Catalog=LabourMgt;User ID=sa;pwd=Ph@hoenix#g;Connect Timeout=6000"
    Dim SqlConnWStaffAtt As New SqlConnection(ConnstrWStaffAtt)
    Dim readerWStaffAtt As SqlDataReader
    Dim dsWStaffAtt As New DataSet
    Protected Sub showemp_Click(sender As Object, e As EventArgs) Handles showemp.Click
        Try
            sql = "select ROW_NUMBER() OVER(ORDER BY (SELECT 1)) AS EmpSerial ,empid EmpCode, LabourName + '-'+ FatherName EmpName , Photo,AreaName,Sex  from [dbo].[LabourMaster] where AreaName = '" & warealist.Text & "' and active=1"
            ' sql &= "  AreaName = (select AreaMaster from AreaMaster where AreaCode ='" & Session("area_code") & "') and active =1"
            Dim dadaatweekly As New SqlDataAdapter(sql, ConnstrWStaffAtt)
            dadaatweekly.Fill(dsWStaffAtt, "dsWStaffAtt")
            daily_att.DataSource = dsWStaffAtt.Tables("dsWStaffAtt")
            daily_att.DataBind()
        Catch ex As Exception

        End Try
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
            lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Else
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select * from [dbo].[AreaMaster] where Areacode = 'n' and isActive =1"
        Dim da As New SqlDataAdapter(sql, ConnstrWStaffAtt)
        da.Fill(dsWStaffAtt, "AreaMasterCode")
        warealist.DataSource = dsWStaffAtt.Tables("AreaMasterCode")
        warealist.DataTextField = "AreaMaster"
        warealist.DataValueField = "AreaMaster"
        warealist.DataBind()
    End Sub

    Protected Sub btnAttSAve_Click(sender As Object, e As EventArgs) Handles btnAttSAve.Click
        
        Dim res As String = ""
        Try


            For Each item As DataGridItem In daily_att.Items
                ' Response.Write(CType(item.FindControl("Text1"), TextBox).Text)
                'sql = "update [Standard_Winter] set " & Combo3.SelectedValue & " = '" & CType(item.FindControl("Text1"), TextBox).Text & "' where  Bird_breed_code='" & Combo2.Text & "' and Age_Weeks ='" & item.Cells(0).Text & "'"
                'qyeryresult = gmod.SqlExecuteNonQuery(sql)

                Dim chkabsent As CheckBox = CType(item.FindControl("chkAbsent"), CheckBox)
                'Dim chkleave As CheckBox = CType(item.FindControl("chkLeave"), CheckBox)

                sql = "insert into daily_attendance([date], [empcode], [empname], [absent], [leave], [remark], [loc], [locname], emptype,sex) values("
                sql &= "'" & lbldate.Text & "',"
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
