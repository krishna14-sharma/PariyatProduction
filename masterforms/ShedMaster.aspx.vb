
Partial Class masterforms_ShedMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim storeflag As Integer

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[shed_master]([org_code], [loc_code], [shed_code], [shed_name], [shed_capacity], [shed_type], [no_of_pen],isStore) values( "
        sql &= "'" & orgname.SelectedValue & "',"
        sql &= "'" & locationname.SelectedValue & "',"
        sql &= "'" & shedcode.Text & "',"
        sql &= "'" & shedname.Text & "',"

        sql &= "'" & shedcapacity.Text & "',"
        sql &= "'" & shedtype.Text & "',"
        sql &= "'" & noofpen.Text & "',"
        sql &= "'" & storeflag & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Shed Data Saved Successfully"
            fillGrid()

        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
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

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillOrg()
            fillLocation()
            fillGrid()
        Else
        End If
    End Sub

    Public Sub validation_check()
        If shedcode.Text = "" Then
            valiation = False
        End If

        If shedname.Text = "" Then
            valiation = False
        End If

        If shedcapacity.Text = "" Then
            valiation = False
        End If

        If shedtype.Text = "" Then
            valiation = False
        End If
        If noofpen.Text = "" Then
            valiation = False
        End If
    End Sub

    Public Sub fillGrid()
        sql = "select * from shed_master"
        gmod.DataSetRet(sql, "bird_type_master")
        shed_grid.DataSource = gmod.ds.Tables("bird_type_master")
        shed_grid.DataBind()
    End Sub

    Public Sub ClearControls()
        shedcode.Text = ""
        shedname.Text = ""
        shedcapacity.Text = ""
        shedtype.Text = ""
        noofpen.Text = ""
    End Sub

    Protected Sub ISSTORE_CheckedChanged(sender As Object, e As EventArgs) Handles ISSTORE.CheckedChanged
        If ISSTORE.Checked = True Then
            storeflag = 1
        Else
            storeflag = 0
        End If
    End Sub
End Class
