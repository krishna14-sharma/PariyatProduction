
Partial Class hatchery_satellite_hatchery_candling2
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
            cmbloc_SelectedIndexChanged(sender, e)
            fillLocation1()
            cmbhatch_SelectedIndexChanged(sender, e)
            fresh.Attributes.Add("onkeyup", "sum()")
            bigsize.Attributes.Add("onkeyup", "sum1()")
            md.Attributes.Add("onkeyup", "sum2()")
            mplus.Attributes.Add("onkeyup", "sum3()")
            mminus.Attributes.Add("onkeyup", "sum4()")
            hcomm.Attributes.Add("onkeyup", "sum5()")
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
        sql = "select * from [dbo].[location_master] where  isRemote=1"
        gmod.DataSetRet(sql, "loc")
        cmbloc.DataSource = gmod.ds.Tables("loc")
        cmbloc.DataTextField = "location_code"
        cmbloc.DataValueField = "location_code"
        cmbloc.DataBind()
    End Sub

    Public Sub fillLocation1()
        sql = "select * from [dbo].[location_master] where  isRemote=1"
        gmod.DataSetRet(sql, "loc")
        cmbhatch.DataSource = gmod.ds.Tables("loc")
        cmbhatch.DataTextField = "location_code"
        cmbhatch.DataValueField = "location_code"
        cmbhatch.DataBind()
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
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master] order by bird_type_code desc"
        gmod.DataSetRet(sql, "shed_loc")
        ftype.DataSource = gmod.ds.Tables("shed_loc")
        ftype.DataTextField = "bird_type_code"
        ftype.DataValueField = "bird_type_code"
        ftype.DataBind()
    End Sub

    Public Sub clearControls()
        fresh.Text = ""
        freshset.Text = ""
        bigsize.Text = ""
        bigsizeset.Text = ""
        md.Text = ""
        mdset.Text = ""
        mplus.Text = ""
        mplusset.Text = ""
        mminus.Text = ""
        mminusset.Text = ""
        hcomm.Text = ""
        hcommser.Text = ""
        lblfresh.Text = ""
        lblbigsize.Text = ""
        lblmd.Text = ""
        lblmplus.Text = ""
        lblmminus.Text = ""
        lblhcomm.Text = ""
    End Sub

    Protected Sub cmbloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbloc.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbloc.Text & "'  and org_code='20'"
            gmod.DataSetRet(sql, "coldroom_")
            cmbshed.DataSource = gmod.ds.Tables("coldroom_")
            cmbshed.DataTextField = "shed_code"
            cmbshed.DataTextField = "shed_name"
            cmbshed.DataValueField = "shed_code"
            cmbshed.DataBind()
            'cmbloc_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub cmbhatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbhatch.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbhatch.Text & "' and org_code='20'"
            gmod.DataSetRet(sql, "coldroom_")
            cmbsetter.DataSource = gmod.ds.Tables("coldroom_")
            cmbsetter.DataTextField = "shed_code"
            cmbsetter.DataTextField = "shed_name"
            cmbsetter.DataValueField = "shed_code"
            cmbsetter.DataBind()

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sql = "select * from [dbo].[hatch_setter_sat] where entry_date = '" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and hatchery='" & cmbloc.Text & "'"
            gmod.DataSetRet(sql, "egggrddata")
            LblAge.Text = gmod.ds.Tables("egggrddata").Rows(0)("Age")
            lblfresh.Text = gmod.ds.Tables("egggrddata").Rows(0)("f_set")

            lblbigsize.Text = gmod.ds.Tables("egggrddata").Rows(0)("bg_set")
            lblmd.Text = gmod.ds.Tables("egggrddata").Rows(0)("md_set")
            lblmplus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mp_set")
            lblmminus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mm_set")
            lblhcomm.Text = gmod.ds.Tables("egggrddata").Rows(0)("comm_set")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        sql = " insert into [hatch_canding2_sat]([entry_date], [ftype], [flock_no], [age], [f_fertile], [f_infertile], [bg_fertile], "
        sql &= " [bg_infertile], [md_fertile], [md_infertile], [mp_fertile], [mp_infertile], [mm_fertile], [mm_infertile], [comm_fertile], "
        sql &= " [comm_infertile], [hatchery], [machine], [hatchery_set], [setter]) values("
        sql &= "'" & entrydate.Text & "',"
        sql &= "'" & ftype.Text & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & LblAge.Text & "',"
        sql &= "'" & Val(fresh.Text) & "',"
        sql &= "'" & Val(freshset.Text) & "',"
        sql &= "'" & Val(bigsize.Text) & "',"
        sql &= "'" & Val(bigsizeset.Text) & "',"
        sql &= "'" & Val(md.Text) & "',"
        sql &= "'" & Val(mdset.Text) & "',"
        sql &= "'" & Val(mplus.Text) & "',"
        sql &= "'" & Val(mplusset.Text) & "',"
        sql &= "'" & Val(mminus.Text) & "',"
        sql &= "'" & Val(mminusset.Text) & "',"
        sql &= "'" & Val(hcomm.Text) & "',"
        sql &= "'" & Val(hcommser.Text) & "',"
        sql &= "'" & cmbloc.Text & "',"
        sql &= "'" & cmbshed.Text & "',"
        sql &= "'" & cmbhatch.Text & "',"
        sql &= "'" & cmbsetter.Text & "')"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            clearControls()
            fillgrid()
            'Button1_Click(sender, e)
        Else
            AppUi.ShowMessage(Me, qyeryresult)
        End If
    End Sub

    Public Sub fillgrid()
        sql = "select * from [dbo].[hatch_canding2_sat] where entry_date='" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
        gmod.DataSetRet(sql, "setter_entry")
        hatch_setter.DataSource = gmod.ds.Tables("setter_entry")
        hatch_setter.DataBind()
    End Sub

    Protected Sub cancel_Click(sender As Object, e As EventArgs) Handles cancel.Click
        fillgrid()
    End Sub

    Protected Sub btnShowArea_Click(sender As Object, e As EventArgs) Handles btnShowArea.Click
        fillFlock()
    End Sub
End Class
