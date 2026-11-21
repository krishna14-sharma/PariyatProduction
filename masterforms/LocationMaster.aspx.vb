
Partial Class images_LocationMaster
    Inherits System.Web.UI.Page

    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        lblprint.Text = orgname.SelectedValue
        sql = "insert into [dbo].[location_master]([org_code], [location_code], [location_name], [location_prd_type]) values("
        sql &= "'" & orgname.SelectedValue & "',"
        sql &= "'" & loccode.Text & "',"
        sql &= "'" & locname.Text & "',"
        sql &= "'" & productiontype.SelectedValue & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Organization Data Saved Successfully"
            fillGrid()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'select [org_code],[org_name] from [dbo].[Org]
            fillprdtType()
            fillOrg()
            fillGrid()
        Else

        End If
    End Sub
    Public Sub fillprdtType()
        sql = "select prd_type_code,[prd_name] from Prd_type_master"
        gmod.DataSetRet(sql, "prdtype_org")
        productiontype.DataSource = gmod.ds.Tables("prdtype_org")
        productiontype.DataTextField = "prd_name"
        productiontype.DataValueField = "prd_type_code"
        productiontype.DataBind()
    End Sub

    Public Sub fillOrg()
        sql = "select [org_code],[org_name] from [dbo].[Org]"
        gmod.DataSetRet(sql, "loc_org")
        orgname.DataSource = gmod.ds.Tables("loc_org")
        orgname.DataTextField = "org_name"
        orgname.DataValueField = "org_code"
        orgname.DataBind()
    End Sub

    Public Sub validation_check()
        If loccode.Text = "" Then
            valiation = False
        End If

        If locname.Text = "" Then
            valiation = False
        End If

    End Sub

    Public Sub fillGrid()
        sql = "select [org_code], [location_code], [location_name], [location_prd_type] from [dbo].[location_master]"
        gmod.DataSetRet(sql, "org_master")
        loc_grid.DataSource = gmod.ds.Tables("org_master")
        loc_grid.DataBind()
    End Sub
End Class
