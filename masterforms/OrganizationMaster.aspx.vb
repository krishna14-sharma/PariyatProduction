
Partial Class masterforms_OrganizationMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillprdtType()
            fillGrid()
        Else
            ' fillprdtType()
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
    Protected Sub btnsaveorg_Click(sender As Object, e As EventArgs) Handles btnsaveorg.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into org([org_code], [org_name], [org_address], [org_state], [org_country], [org_phone], [org_email], [org_gst], [org_prdtype], [is_accounting]) values("
        sql &= "'" & orgcode.Text & "',"
        sql &= "'" & orgname.Text & "',"
        sql &= "'" & orgaddress.Text & "',"
        sql &= "'" & state.Text & "',"
        sql &= "'" & country.Text & "',"
        sql &= "'" & phone.Text & "',"
        sql &= "'" & email.Text & "',"
        sql &= "'" & gst.Text & "',"
        sql &= "'" & productiontype.SelectedValue & "',"
        sql &= "'0')"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Organization Data Saved Successfully"
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Public Sub validation_check()
        If orgcode.Text = "" Then
            valiation = False
        End If

        If orgname.Text = "" Then
            valiation = False
        End If

        If orgaddress.Text = "" Then
            valiation = False
        End If
        If state.Text = "" Then
            valiation = False
        End If
        If country.Text = "" Then
            valiation = False
        End If
        If phone.Text = "" Then
            valiation = False
        End If
        If gst.Text = "" Then
            valiation = False
        End If

        If email.Text = "" Then
            valiation = False
        End If
    End Sub

    Public Sub fillGrid()
        sql = "select * from Org"
        gmod.DataSetRet(sql, "org_master")
        org_grid.DataSource = gmod.ds.Tables("org_master")
        org_grid.DataBind()
    End Sub
End Class
