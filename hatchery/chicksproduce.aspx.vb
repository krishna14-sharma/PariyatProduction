
Partial Class hatchery_chicksproduce
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        clearControls()
        Try
            sql = "select * from [dbo].[hatch_canding2]  where entry_date = '" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and hatchery='" & cmbhatch.Text & "'"
            gmod.DataSetRet(sql, "egggrddata")
            LblAge.Text = gmod.ds.Tables("egggrddata").Rows(0)("Age")
            lblfresh.Text = gmod.ds.Tables("egggrddata").Rows(0)("f_fertile")
            fresh_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("f_infertile")

            lblbigsize.Text = gmod.ds.Tables("egggrddata").Rows(0)("bg_fertile")
            bigsize_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("bg_infertile")

            lblmd.Text = gmod.ds.Tables("egggrddata").Rows(0)("md_fertile")
            md_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("md_infertile")

            lblmplus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mp_fertile")
            mplus_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("mp_infertile")

            lblmminus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mm_fertile")
            mminus_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("mm_infertile")

            lblhcomm.Text = gmod.ds.Tables("egggrddata").Rows(0)("comm_fertile")
            hcomm_infer.Text = gmod.ds.Tables("egggrddata").Rows(0)("comm_infertile")
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            'fillLocation()
            'cmbloc_SelectedIndexChanged(sender, e)
            fillLocation1()
            fillArea()
            cmbhatch_SelectedIndexChanged(sender, e)

            fresh_chicks.Attributes.Add("onkeyup", "sum()")
            fresh_supply.Attributes.Add("onkeyup", "sum()")

            md_chicks.Attributes.Add("onkeyup", "sum1()")
            md_supply.Attributes.Add("onkeyup", "sum1()")

            bigsize_chicks.Attributes.Add("onkeyup", "sum2()")
            bigsize_supply.Attributes.Add("onkeyup", "sum2()")

            mplus_chicks.Attributes.Add("onkeyup", "sum3()")
            mplus_supply.Attributes.Add("onkeyup", "sum3()")

            mminus_chicks.Attributes.Add("onkeyup", "sum4()")
            mminus_supply.Attributes.Add("onkeyup", "sum4()")

            hcomm_chicks.Attributes.Add("onkeyup", "sum5()")
            hcomm_supply.Attributes.Add("onkeyup", "sum5()")
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
        ftype.DataSource = gmod.ds.Tables("shed_loc")
        ftype.DataTextField = "bird_type_code"
        ftype.DataValueField = "bird_type_code"
        ftype.DataBind()
    End Sub
    Public Sub fillLocation1()
        sql = "select * from [dbo].[location_master] where  location_prd_type=5"
        gmod.DataSetRet(sql, "loc")
        cmbhatch.DataSource = gmod.ds.Tables("loc")
        cmbhatch.DataTextField = "location_code"
        cmbhatch.DataValueField = "location_code"
        cmbhatch.DataBind()

    End Sub
    Protected Sub cmbhatch_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbhatch.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbhatch.Text & "'  and shed_type='HAT' and  org_code='20'"
            gmod.DataSetRet(sql, "coldroom_")
            cmbsetter.DataSource = gmod.ds.Tables("coldroom_")
            cmbsetter.DataTextField = "shed_code"
            cmbsetter.DataTextField = "shed_name"
            cmbsetter.DataValueField = "shed_code"
            cmbsetter.DataBind()
            'cmbloc_SelectedIndexChanged(sender, e)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        sql = "insert into [dbo].[hatch_production]([loading_date], [ftype], [flock_no], [age], [fresh_fertile_eggs], [fresh_infertile_eggs], "
        sql &= " [fresh_chichs], [fresh_hatcherloss], [fresh_supply], [fresh_culls], [fresh_per], [bg_fertile_eggs], [bg_infertile_eggs], "
        sql &= " [bg_chicks], [bg_hatcherloss], [bg_supply], [bg_culls], [bg_per], [md_fertile_eggs], [md_infertile_eggs], [md_chicks], [md_hatcherloss], "
        sql &= " [md_supply], [md_culls], [md_per], [mp_fertile_eggs], [mp_infertile_eggs], [mp_chicks], [mp_hatcherloss], [mp_supply], [mp_culls], [mp_per], "
        sql &= " [mm_fertile_eggs], [mm_infertile_eggs], [mm_chicks], [mm_hatcherloss], [mm_supply], [mm_culls], [mm_per], [comm_fertile_eggs],  "
        sql &= " [comm_infertile_eggs], [comm_chicks], [comm_hatcherloss], [comm_supply], [comm_culls], [comm_per], [hatchery], [machine]) values ("

        sql &= "'" & entrydate.Text & "',"
        sql &= "'" & ftype.Text & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & LblAge.Text & "',"

        sql &= "'" & Val(lblfresh.Text) & "',"
        sql &= "'" & Val(fresh_infer.Text) & "',"
        sql &= "'" & Val(fresh_chicks.Text) & "',"
        sql &= "'" & Val(fresh_hatcherloss.Text) & "',"
        sql &= "'" & Val(fresh_supply.Text) & "',"
        sql &= "'" & Val(fresh_culls.Text) & "',"
        sql &= "'" & Val(fresh_hatch_per.Text) & "',"

        sql &= "'" & Val(lblbigsize.Text) & "',"
        sql &= "'" & Val(bigsize_infer.Text) & "',"
        sql &= "'" & Val(bigsize_chicks.Text) & "',"
        sql &= "'" & Val(bigsize_hatcherloss.Text) & "',"
        sql &= "'" & Val(bigsize_supply.Text) & "',"
        sql &= "'" & Val(bigsize_culls.Text) & "',"
        sql &= "'" & Val(big_hatch_per.Text) & "',"

        sql &= "'" & Val(lblmd.Text) & "',"
        sql &= "'" & Val(md_infer.Text) & "',"
        sql &= "'" & Val(md_chicks.Text) & "',"
        sql &= "'" & Val(md_hatcherloss.Text) & "',"
        sql &= "'" & Val(md_supply.Text) & "',"
        sql &= "'" & Val(md_culls.Text) & "',"
        sql &= "'" & Val(md_hatch_per.Text) & "',"

        sql &= "'" & Val(lblmplus.Text) & "',"
        sql &= "'" & Val(mplus_infer.Text) & "',"
        sql &= "'" & Val(mplus_chicks.Text) & "',"
        sql &= "'" & Val(mplus_hatcherloss.Text) & "',"
        sql &= "'" & Val(mplus_supply.Text) & "',"
        sql &= "'" & Val(mplus_culls.Text) & "',"
        sql &= "'" & Val(mplus_hatch_per.Text) & "',"

        sql &= "'" & Val(lblmminus.Text) & "',"
        sql &= "'" & Val(mminus_infer.Text) & "',"
        sql &= "'" & Val(mminus_chicks.Text) & "',"
        sql &= "'" & Val(mminus_hatcherloss.Text) & "',"
        sql &= "'" & Val(mminus_supply.Text) & "',"
        sql &= "'" & Val(mminus_culls.Text) & "',"
        sql &= "'" & Val(mminus_hatch_per.Text) & "',"

        sql &= "'" & Val(lblhcomm.Text) & "',"
        sql &= "'" & Val(hcomm_infer.Text) & "',"
        sql &= "'" & Val(hcomm_chicks.Text) & "',"
        sql &= "'" & Val(hcomm_hatcherloss.Text) & "',"
        sql &= "'" & Val(hcomm_supply.Text) & "',"
        sql &= "'" & Val(hcomm_culls.Text) & "',"
        sql &= "'" & Val(hcomm_hatch_per.Text) & "',"

        sql &= "'" & cmbhatch.Text & "',"
        sql &= "'" & cmbsetter.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            clearControls()
            fillgrid()
            'Button1_Click(sender, e)
            lblString.Text = ""
        Else
            lblString.Text = qyeryresult
        End If


    End Sub

    Public Sub clearControls()

        fresh_infer.Text = ""
        fresh_supply.Text = ""
        fresh_hatcherloss.Text = ""
        fresh_supply.Text = ""
        fresh_culls.Text = ""
        fresh_hatch_per.Text = ""
        fresh_chicks.Text = ""

        bigsize_infer.Text = ""
        bigsize_supply.Text = ""
        bigsize_hatcherloss.Text = ""
        bigsize_supply.Text = ""
        bigsize_culls.Text = ""
        big_hatch_per.Text = ""
        bigsize_chicks.Text = ""


        md_infer.Text = ""
        md_supply.Text = ""
        md_hatcherloss.Text = ""
        md_supply.Text = ""
        md_culls.Text = ""
        md_hatch_per.Text = ""
        md_chicks.Text = ""

        mplus_infer.Text = ""
        mplus_supply.Text = ""
        mplus_hatcherloss.Text = ""
        mplus_supply.Text = ""
        mplus_culls.Text = ""
        mplus_hatch_per.Text = ""
        mplus_chicks.Text = ""

        mminus_infer.Text = ""
        mminus_supply.Text = ""
        mminus_hatcherloss.Text = ""
        mminus_supply.Text = ""
        mminus_culls.Text = ""
        mminus_hatch_per.Text = ""
        mminus_chicks.Text = ""

        hcomm_infer.Text = ""
        hcomm_supply.Text = ""
        hcomm_hatcherloss.Text = ""
        hcomm_supply.Text = ""
        hcomm_culls.Text = ""
        hcomm_hatch_per.Text = ""
        hcomm_chicks.Text = ""

        lblfresh.Text = ""
        lblbigsize.Text = ""
        lblmd.Text = ""
        lblmplus.Text = ""
        lblmminus.Text = ""
        lblhcomm.Text = ""
    End Sub

    Public Sub fillgrid()
        sql = "select convert(varchar, [loading_date], 103) as [loading_date], [ftype], [flock_no], [age], [fresh_fertile_eggs], [fresh_infertile_eggs], [fresh_chichs], [fresh_hatcherloss], [fresh_supply], [fresh_culls], [fresh_per], [bg_fertile_eggs], [bg_infertile_eggs], [bg_chicks], [bg_hatcherloss], [bg_supply], [bg_culls], [bg_per], [md_fertile_eggs], [md_infertile_eggs], [md_chicks], [md_hatcherloss], [md_supply], [md_culls], [md_per], [mp_fertile_eggs], [mp_infertile_eggs], [mp_chicks], [mp_hatcherloss], [mp_supply], [mp_culls], [mp_per], [mm_fertile_eggs], [mm_infertile_eggs], [mm_chicks], [mm_hatcherloss], [mm_supply], [mm_culls], [mm_per], [comm_fertile_eggs], [comm_infertile_eggs], [comm_chicks], [comm_hatcherloss], [comm_supply], [comm_culls], [comm_per], [hatchery], [machine], [id] from [hatch_production] where loading_date='" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
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
