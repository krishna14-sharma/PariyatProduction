
Partial Class hatchery_satellite_hatchery_candling
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
            freshrej.Attributes.Add("onkeyup", "sum()")
            bigsizerej.Attributes.Add("onkeyup", "sum1()")
            mdrej.Attributes.Add("onkeyup", "sum2()")
            mplusrej.Attributes.Add("onkeyup", "sum3()")
            mminusrej.Attributes.Add("onkeyup", "sum4()")
            hcommrej.Attributes.Add("onkeyup", "sum5()")

            cmbloc_SelectedIndexChanged(sender, e)
            cmbshed_SelectedIndexChanged(sender, e)

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
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]  order by bird_type_code desc"
        gmod.DataSetRet(sql, "shed_loc")
        ftype.DataSource = gmod.ds.Tables("shed_loc")
        ftype.DataTextField = "bird_type_code"
        ftype.DataValueField = "bird_type_code"
        ftype.DataBind()
    End Sub


    Protected Sub cmbloc_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbloc.Text & "' and org_code='20'"
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

    Protected Sub cmbshed_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbshed.SelectedIndexChanged, cmbloc.SelectedIndexChanged
        Try
            sql = "select * from [dbo].[shed_master] where loc_code='" & cmbloc.Text & "' and  org_code='20'"
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


    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        sql = " insert into hatch_setter_sat([entry_date], [egg_coll_date], [ftype], [flock_no], [age], [f_taken], [f_set], [f_rej], [bg_taken], [bg_set],"
        sql &= " [bg_rej], [md_taken], [md_set], [md_rej], [mp_taken], [mp_set], [mp_rej], [mm_taken], [mm_set], [mm_rej], [comm_taken], "
        sql &= " [comm_set], [comm_rej], [hatchery], [machine],[f_leaker], [bg_leaker], [md_leaker], [mp_leaker], [mm_leaker], [comm_leaker],egg_coll_date_to, eggf) values("
        sql &= "'" & entrydate.Text & "',"
        sql &= "'" & eggcolldate.Text & "',"
        sql &= "'" & ftype.Text & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & LblAge.Text & "',"
        sql &= "'" & Val(fresh.Text) & "',"
        sql &= "'" & Val(freshset.Text) & "',"
        sql &= "'" & Val(freshrej.Text) & "',"
        sql &= "'" & Val(bigsize.Text) & "',"
        sql &= "'" & Val(bigsizeset.Text) & "',"
        sql &= "'" & Val(bigsizerej.Text) & "',"
        sql &= "'" & Val(md.Text) & "',"
        sql &= "'" & Val(mdset.Text) & "',"
        sql &= "'" & Val(mdrej.Text) & "',"
        sql &= "'" & Val(mplus.Text) & "',"
        sql &= "'" & Val(mplusset.Text) & "',"
        sql &= "'" & Val(mplusrej.Text) & "',"
        sql &= "'" & Val(mminus.Text) & "',"
        sql &= "'" & Val(mminusset.Text) & "',"
        sql &= "'" & Val(mminusset.Text) & "',"
        sql &= "'" & Val(hcomm.Text) & "',"
        sql &= "'" & Val(hcommser.Text) & "',"
        sql &= "'" & Val(hcommrej.Text) & "',"
        sql &= "'" & cmbloc.Text & "',"
        sql &= "'" & cmbshed.Text & "',"

        sql &= "'" & Val(fresh.Text) - (Val(freshset.Text) + Val(freshrej.Text)) & "',"
        sql &= "'" & Val(bigsize.Text) - (Val(bigsizeset.Text) + Val(bigsizerej.Text)) & "',"
        sql &= "'" & Val(md.Text) - (Val(mdset.Text) + Val(mdrej.Text)) & "',"
        sql &= "'" & Val(mplus.Text) - (Val(mplusset.Text) + Val(mplusrej.Text)) & "',"
        sql &= "'" & Val(mminus.Text) - (Val(mminusset.Text) + Val(mminusrej.Text)) & "',"
        sql &= "'" & Val(hcomm.Text) - (Val(hcommser.Text) + Val(hcommrej.Text)) & "',"
        sql &= "'" & eggto.Text & "',"
        sql &= "'" & eggf.Text & "')"

        'stockupdation()
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then

            'sql = " update [dbo].[hatch_egg_grading] set [fresh_eggs] = [fresh_eggs] -'" & fresh.Text & "' ,[bigsize_eggs]=[bigsize_eggs] -'" & bigsize.Text & "',"
            'sql &= " [md_eggs]= [md_eggs] - '" & md.Text & "' ,[mplus_eggs]=[mplus_eggs]-'" & mplus.Text & "',[mminus_eggs]=[mminus_eggs]-'" & mminus.Text & "',[hcomm_eggs]=[hcomm_eggs] - '" & hcomm.Text & "' "
            'sql &= " where [bird_type]='" & ftype.Text & "' and [flock_no]= '" & flockno.Text & "' and egg_coll_date='" & eggcolldate.Text & "'"

            'gmod.SqlExecuteNonQuery(sql)
            stockupdation()
            clearControls()
            fillgrid()
            Button1_Click(sender, e)
        End If
    End Sub

    Public Sub stockupdation()
        sql = "select  * from [dbo].[hatch_egg_grading_sat] where egg_coll_date between '" & eggcolldate.Text & "' and '" & toeggcolldate.Text & " ' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and loc = '" & cmbloc.Text & "' order by egg_coll_date asc"
        gmod.DataSetRet(sql, "egg_grading")
        Dim fresh_le As Integer = 0
        Dim fresh_data As Integer = 0
        Dim bigsize_le As Integer = 0
        Dim bigsize_data As Integer = 0
        Dim md_le As Integer = 0
        Dim md_data As Integer = 0
        Dim mdplus_le As Integer = 0
        Dim mdplus_data As Integer = 0
        Dim mdminus_le As Integer = 0
        Dim mdminus_data As Integer = 0
        Dim comm_le As Integer = 0
        Dim comm_data As Integer = 0

        Dim i As Integer = 0
        Dim id As Integer = 0
        fresh_le = Val(fresh.Text)
        bigsize_le = Val(bigsize.Text)
        md_le = Val(md.Text)
        mdplus_le = Val(mplus.Text)
        mdminus_le = Val(mminus.Text)
        comm_le = Val(hcomm.Text)


        For i = 0 To gmod.ds.Tables("egg_grading").Rows.Count - 1
            fresh_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("fresh_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If fresh_le > 0 Then
                If Val(fresh_le) >= fresh_data Then
                    fresh_le = fresh_le - fresh_data
                    fresh_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  fresh_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    fresh_data = fresh_data - fresh_le
                    fresh_le = fresh_le - fresh_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  fresh_eggs = '" & fresh_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    fresh_le = 0
                End If
            Else
                'Exit For
            End If

            ''Big Size
            bigsize_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("bigsize_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If bigsize_le > 0 Then
                If Val(bigsize_le) >= bigsize_data Then
                    bigsize_le = bigsize_le - bigsize_data
                    bigsize_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  bigsize_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    bigsize_data = bigsize_data - bigsize_le
                    bigsize_le = bigsize_le - bigsize_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  bigsize_eggs = '" & bigsize_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    bigsize_le = 0
                End If
            Else
                'Exit For
            End If

            ''MD
            md_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("md_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If md_le > 0 Then
                If Val(md_le) >= md_data Then
                    md_le = md_le - md_data
                    md_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  md_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    md_data = md_data - md_le
                    md_le = md_le - md_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  md_eggs = '" & md_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    md_le = 0
                End If
            Else
                'Exit For
            End If

            ''M Plus
            mdplus_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("mplus_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If mdplus_le > 0 Then
                If Val(mdplus_le) >= md_data Then
                    mdplus_le = mdplus_le - mdplus_data
                    mdplus_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  mplus_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdplus_data = mdplus_data - mdplus_le
                    mdplus_le = mdplus_le - mdplus_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  mplus_eggs = '" & md_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    mdplus_le = 0
                End If
            Else
                'Exit For
            End If

            ''M minus
            mdminus_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("mminus_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If mdminus_le > 0 Then
                If Val(mdminus_le) >= mdminus_data Then
                    mdminus_le = mdminus_le - mdminus_data
                    mdplus_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  mminus_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdminus_data = mdminus_data - mdminus_le
                    mdminus_le = mdminus_le - mdminus_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  mminus_eggs = '" & mdminus_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    mdminus_le = 0
                End If
            Else
                'Exit For
            End If

            ''Hatchey Commercial
            comm_data = Val(gmod.ds.Tables("egg_grading").Rows(i)("hcomm_eggs"))
            id = Val(gmod.ds.Tables("egg_grading").Rows(i)("id"))
            If comm_le > 0 Then
                If Val(comm_le) >= comm_data Then
                    comm_le = comm_le - comm_data
                    comm_data = 0
                    sql = "update [dbo].[hatch_egg_grading_sat] set  hcomm_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdminus_data = mdminus_data - comm_le
                    comm_le = comm_le - comm_data
                    sql = "update [dbo].[hatch_egg_grading_sat] set  hcomm_eggs = '" & comm_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                    comm_le = 0
                End If
            Else
                'Exit For
            End If
        Next
    End Sub
    Public Sub clearControls()
        fresh.Text = ""
        freshset.Text = ""
        freshrej.Text = ""

        bigsize.Text = ""
        bigsizeset.Text = ""
        bigsizerej.Text = ""

        md.Text = ""
        mdset.Text = ""
        mdrej.Text = ""

        mplus.Text = ""
        mplusset.Text = ""
        mplusrej.Text = ""

        mminus.Text = ""
        mminusset.Text = ""
        mminusrej.Text = ""

        hcomm.Text = ""
        hcommser.Text = ""
        hcommrej.Text = ""
    End Sub

    Public Sub fillgrid()
        sql = "select convert(varchar, [entry_date], 103) [Loading Date],[flock_no], [age], [f_taken], [f_set], [f_rej],[f_leaker], [bg_taken], [bg_set], [bg_rej],[bg_leaker], [md_taken], [md_set], [md_rej],[md_leaker], [mp_taken], [mp_set], [mp_rej], [mm_taken], [mp_leaker],[mm_set], [mm_rej], [mm_leaker],[comm_taken], [comm_set], [comm_rej] ,[comm_leaker] from [dbo].[hatch_setter_sat] where entry_date='" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
        gmod.DataSetRet(sql, "setter_entry")
        hatch_setter.DataSource = gmod.ds.Tables("setter_entry")
        hatch_setter.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sql = "select  Age from [dbo].[hatch_egg_grading_sat] where entry_date = '" & eggcolldate.Text & "' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and loc = '" & cmbloc.Text & "'"
            gmod.DataSetRet(sql, "egggrddataage")
            LblAge.Text = gmod.ds.Tables("egggrddataage").Rows(0)("Age")
        Catch ex As Exception
            lblString.Text = ex.Message
        End Try

        Try
            sql = "select  egg_coll_date, egg_coll_date_to from [dbo].[hatch_egg_grading_sat] where entry_date = '" & eggcolldate.Text & "' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and loc = '" & cmbloc.Text & "'"
            gmod.DataSetRet(sql, "egggrddataeggcollectiondate")
            eggf.Text = gmod.ds.Tables("egggrddataeggcollectiondate").Rows(0)("egg_coll_date")
            eggto.Text = gmod.ds.Tables("egggrddataeggcollectiondate").Rows(0)("egg_coll_date_to")
        Catch ex As Exception
            lblString.Text = "Egg collection date not found"
        End Try

        Try
            sql = "select  sum([hatch_eggs]) [hatch_eggs],sum([fresh_eggs]) [fresh_eggs],sum([bigsize_eggs]) [bigsize_eggs],sum([md_eggs]) [md_eggs],sum([mplus_eggs]) [mplus_eggs],sum([mminus_eggs]) [mminus_eggs],sum([hcomm_eggs]) [hcomm_eggs] from [dbo].[hatch_egg_grading_sat] where entry_date = '" & eggcolldate.Text & "' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' and loc = '" & cmbloc.Text & "'"
            gmod.DataSetRet(sql, "egggrddata")
            'LblAge.Text = gmod.ds.Tables("egggrddata").Rows(0)("Age")
            lblfresh.Text = gmod.ds.Tables("egggrddata").Rows(0)("fresh_eggs")
            lblbigsize.Text = gmod.ds.Tables("egggrddata").Rows(0)("bigsize_eggs")
            lblmd.Text = gmod.ds.Tables("egggrddata").Rows(0)("md_eggs")
            lblmplus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mplus_eggs")
            lblmminus.Text = gmod.ds.Tables("egggrddata").Rows(0)("mminus_eggs")
            lblhcomm.Text = gmod.ds.Tables("egggrddata").Rows(0)("hcomm_eggs")
        Catch ex As Exception
            lblString.Text = ex.Message
            lblfresh.Text = "0"
            lblmd.Text = "0"
        End Try
    End Sub

    Protected Sub cancel_Click(sender As Object, e As EventArgs) Handles cancel.Click
        fillgrid()
    End Sub

    Protected Sub btnShowArea_Click(sender As Object, e As EventArgs) Handles btnShowArea.Click
        fillFlock()
    End Sub
End Class
