
Partial Class hatchery_hatcheryegg_collection
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            fillLocation()
            fillArea()
        End If
    End Sub
    Public Sub fillArea()
        sql = "select distinct shed_loc from Shed_Entry_Data where isAlive=1"
        gmod.DataSetRet(sql, "shed_locdate")
        cmbAreaName.DataSource = gmod.ds.Tables("shed_locdate")
        cmbAreaName.DataTextField = "shed_loc"
        cmbAreaName.DataValueField = "shed_loc"
        cmbAreaName.DataBind()
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

    Public Sub fillFlock()
        'sql = "select distinct flock_no from [dbo].[flock_master] where isAlive=1 order by flock_no"
        sql = "select distinct flock_no  from Shed_Entry_Data where shed_loc='" & cmbAreaName.Text & "' and isAlive =1"
        gmod.DataSetRet(sql, "data_flock")
        flockno.DataSource = gmod.ds.Tables("data_flock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()
    End Sub
    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        cmbftype.DataSource = gmod.ds.Tables("shed_loc")
        cmbftype.DataTextField = "bird_type_code"
        cmbftype.DataValueField = "bird_type_code"
        cmbftype.DataBind()
    End Sub
    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            sql = "select sum(hatch_eggs) hatch_eggs, sum(comm_eggs+comm_eggs_small) comm_eggs,sum(crack_eggs) crack_eggs,"
            sql &= " sum(leakage_eggs+leakage_eggs_leaker) leaker_eggs, sum(total_eggs) total_eggs "
            sql &= " from [dbo].[daily_entry]  where flock_no='" & flockno.Text & "' and entry_date='" & eggcolldate.Text & "'"

            gmod.DataSetRet(sql, "eggcollfrmfarm")
            txteggs.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("total_eggs")
            TxtCommEggs.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("comm_eggs")
            TxtLeakEggs.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("leaker_eggs")
            txthatchegg.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("hatch_eggs")
            TxtCrackEgg.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("crack_eggs")
            lblFlockno.Text = flockno.Text
            lblString.Text = ""
        Catch ex As Exception
            lblString.Text = ex.Message
            txteggs.Text = "0"
            TxtCommEggs.Text = "0"
            TxtLeakEggs.Text = "0"
            txthatchegg.Text = "0"
            TxtCrackEgg.Text = "0"
            lblFlockno.Text = ""
        End Try

        Try
            sql = "select distinct bird_type_code from  [dbo].[daily_entry]   where flock_no='" & flockno.Text & "' and entry_date='" & eggcolldate.Text & "'"
            gmod.DataSetRet(sql, "bage")
            LblAge.Text = gmod.ds.Tables("bage").Rows(0)(0)
            lblString.Text = ""

        Catch ex As Exception
            lblString.Text = ex.Message
            LblAge.Text = "-"
        End Try

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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'If Val(txtegg_wt.Text) > 0 Then

        sql = "insert into [dbo].[Hatchery_egg_collection] ([entry_date], [bird_type], [flock_no], [Age], [total_eggs], [hatch_eggs], [comm_eggs], [crack_eggs], [leakge_eggs], [loc], [cl_name], [cl_code], [user_name],egg_wt) values("
        sql &= "'" & eggcolldate.Text & "',"
        sql &= "'" & cmbftype.Text & "',"
        sql &= "'" & lblFlockno.Text & "',"
        sql &= "'" & LblAge.Text & "',"
        sql &= "'" & Val(txteggs.Text) & "',"

        sql &= "'" & Val(txthatchegg.Text) & "',"
        sql &= "'" & Val(TxtCommEggs.Text) & "',"
        sql &= "'" & Val(TxtCrackEgg.Text) & "',"
        sql &= "'" & Val(TxtLeakEggs.Text) & "',"
        sql &= "'" & cmbloc.Text & "',"

        sql &= "'" & coldroomname.SelectedValue & "',"
        sql &= "'" & coldroomname.SelectedItem.ToString & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'" & Val(txtegg_wt.Text) & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "HE Opeing Data Saved Successfully"
            fillrid()
        Else
            lblprint.Text = qyeryresult
        End If

        'Else
        ' lblprint.Text = "Please Enter Egg Weight.."

        'End If
        'ClearControls()

    End Sub

    Public Sub fillrid()
        sql = "select convert(varchar, [entry_date], 103) as [entry_date], [bird_type], [flock_no], [Age], [total_eggs], [hatch_eggs], [comm_eggs], [crack_eggs], [leakge_eggs], [loc], [cl_name], [cl_code], [user_name], [id] from [dbo].[Hatchery_egg_collection] where entry_date='" & eggcolldate.Text & "' and bird_type='" & cmbftype.Text & "' and flock_no='" & flockno.Text & "'"
        gmod.DataSetRet(sql, "setter_entry")
        eggcollgrid.DataSource = gmod.ds.Tables("setter_entry")
        eggcollgrid.DataBind()
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        fillrid()
    End Sub

    Protected Sub btnShowFlock_Click(sender As Object, e As EventArgs) Handles btnShowFlock.Click
        fillFlock()
    End Sub
End Class
