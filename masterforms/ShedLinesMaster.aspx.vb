
Partial Class masterforms_ShedLinesMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        sql = "insert into [dbo].[shed_line_master] ([org_code], [loc_code], [shed_code], [shed_name], [Line_code], [Line_no]) values ("
        sql &= "'" & orgname.SelectedValue & "',"
        sql &= "'" & locationname.SelectedValue & "',"
        sql &= "'" & shedname.SelectedValue & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & linecode.Text & "',"
        sql &= "'" & linename.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Shed Pen/Line Data Saved Successfully"
            fillGrid()

        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillOrg()
            fillLocation()
            fillGrid()
        Else
        End If
    End Sub

    Public Sub fillOrg()
        sql = "select [org_code],[org_name] from [dbo].[Org]"
        gmod.DataSetRet(sql, "shed_org")
        orgname.DataSource = gmod.ds.Tables("shed_org")
        orgname.DataTextField = "org_name"
        orgname.DataValueField = "org_code"
        orgname.DataBind()
    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]"
        gmod.DataSetRet(sql, "shed_loc")
        locationname.DataSource = gmod.ds.Tables("shed_loc")
        locationname.DataTextField = "location_name"
        locationname.DataValueField = "location_code"
        locationname.DataBind()
    End Sub

    Protected Sub locationname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles locationname.SelectedIndexChanged
        sql = "select * from shed_master where loc_code='" & locationname.SelectedValue & "' and org_code='" & orgname.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_master")
        shedname.DataSource = gmod.ds.Tables("shed_master")
        shedname.DataTextField = "shed_name"
        shedname.DataValueField = "shed_code"
        shedname.DataBind()
    End Sub

    Public Sub fillGrid()
        sql = "select * from shed_line_master"
        gmod.DataSetRet(sql, "bird_type_master")
        shed_grid.DataSource = gmod.ds.Tables("bird_type_master")
        shed_grid.DataBind()
    End Sub

    Public Sub ClearControls()
       
        linecode.Text = ""
        linename.Text = ""
    End Sub
End Class
