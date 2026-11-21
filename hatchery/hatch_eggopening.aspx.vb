
Partial Class hatchery_hatch_eggopening
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[hatchery_egg_opening]( [entry_date], [bird_breed], [flock_no],  [location], [cl_name], [cl_code], [total_hhe],user_id,age ) values( "
        sql &= "'" & entrydate.Text & "',"
        sql &= "'" & birdtype.Text & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & cmbloc.Text & "',"
        sql &= "'" & coldroomname.SelectedValue & "',"
        sql &= "'" & coldroomname.SelectedItem.ToString & "',"
        sql &= "'" & Val(txteggs.Text) & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'" & Age.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "HE Opeing Data Saved Successfully"


            fillrid()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
    End Sub

    Public Sub fillFlock()
        sql = "select distinct flock_no from [dbo].[flock_master] where isAlive=1"
        gmod.DataSetRet(sql, "data_flock")
        flockno.DataSource = gmod.ds.Tables("data_flock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()
    End Sub

    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        birdtype.DataSource = gmod.ds.Tables("shed_loc")
        birdtype.DataTextField = "bird_type_code"
        birdtype.DataValueField = "bird_type_code"
        birdtype.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            fillLocation()

        End If
    End Sub
    Public Sub fillLocation()
        sql = "select * from [dbo].[shed_master] where shed_type='CL' and  org_code='20'"

        gmod.DataSetRet(sql, "coldroom_loc")
        cmbloc.DataSource = gmod.ds.Tables("coldroom_loc")
        cmbloc.DataTextField = "loc_code"
        cmbloc.DataValueField = "loc_code"
        cmbloc.DataBind()
        'cmbloc_SelectedIndexChanged(sender, e)
    End Sub
    Public Sub ClearControls()
        txteggs.Text = ""
    End Sub
    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If
        If entrydate.Text = "" Then
            valiation = False
        End If

        If txteggs.Text = "" Then
            valiation = False
        End If
    End Sub

    Public Sub fillrid()
        sql = "select * from hatchery_egg_opening where user_id='" & Session("user_name") & "'"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        eggopening_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        eggopening_grid.DataBind()
    End Sub

    Protected Sub cmbloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbloc.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbloc.Text & "'  and shed_type='CL' and  org_code='20'"
            gmod.DataSetRet(sql, "coldroom_")
            coldroomname.DataSource = gmod.ds.Tables("coldroom_")
            coldroomname.DataTextField = "shed_code"
            coldroomname.DataTextField = "shed_name"
            coldroomname.DataValueField = "shed_code"
            coldroomname.DataBind()
            'cmbloc_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try

    End Sub
End Class
