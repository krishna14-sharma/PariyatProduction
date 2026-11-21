Imports System.Data
Imports System.Data.SqlClient
Partial Class hatchery_heggsalw2per
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sql = "select  Age from [dbo].[hatch_egg_grading] where egg_coll_date between '" & eggcolldate.Text & "' and '" & toeggcolldate.Text & "' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
            gmod.DataSetRet(sql, "egggrddataage")
            LblAge.Text = gmod.ds.Tables("egggrddataage").Rows(0)("Age")
        Catch ex As Exception

            lblString.Text = ex.Message
        End Try

        Try
            sql = "select  sum([hatch_eggs]) [hatch_eggs],sum([fresh_eggs]) [fresh_eggs],sum([bigsize_eggs]) [bigsize_eggs],sum([md_eggs]) [md_eggs],sum([mplus_eggs]) [mplus_eggs],sum([mminus_eggs]) [mminus_eggs],sum([hcomm_eggs]) [hcomm_eggs] from [dbo].[hatch_egg_grading] where egg_coll_date between '" & eggcolldate.Text & "' and '" & toeggcolldate.Text & " ' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
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
        End Try
    End Sub
    Public Sub fillArea()
        sql = "select distinct shed_loc from Shed_Entry_Data where isAlive=1"
        gmod.DataSetRet(sql, "shed_locdate")
        cmbAreaName.DataSource = gmod.ds.Tables("shed_locdate")
        cmbAreaName.DataTextField = "shed_loc"
        cmbAreaName.DataValueField = "shed_loc"
        cmbAreaName.DataBind()
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillBirdtype()
            fillLocation()
            fillArea()

            ' fresh.Attributes.Add("onkeyup", "sum()")
            'bigsize.Attributes.Add("onkeyup", "sum1()")
            'md.Attributes.Add("onkeyup", "sum2()")
            'mplus.Attributes.Add("onkeyup", "sum3()")
            'mminus.Attributes.Add("onkeyup", "sum4()")
            'hcomm.Attributes.Add("onkeyup", "sum5()")
            'calcamt
            freshrej.Attributes.Add("onkeyup", "calcamt()")
            bigsizerej.Attributes.Add("onkeyup", "calcamt1()")
            mdrej.Attributes.Add("onkeyup", "calcamt2()")
            mplusrej.Attributes.Add("onkeyup", "calcamt3()")
            mminusrej.Attributes.Add("onkeyup", "calcamt4()")
            hcommrej.Attributes.Add("onkeyup", "calcamt5()")
            'cmbloc_SelectedIndexChanged(sender, e)
            'cmbshed_SelectedIndexChanged(sender, e)

        End If


    End Sub


    Public Sub fillLocation()
        'sql = "select * from [dbo].[location_master] where  location_prd_type=5"
        sql = "select account_head_name + '#' + account_code  acc from ACC_HEAD_PHOE_2526 where sub_group_name like '%EGGS CUSTOMER%' order by account_head_name"

        'cmbloc_SelectedIndexChanged(sender, e)
        Dim ConnstrPP As String = "Data Source=192.168.0.130;Initial Catalog=PhxGroupERP;User ID=sa;pwd=Ph@hoenix#g;Connect Timeout=6000"
        Dim dsHatchingEggCustomer As New DataSet
        Dim da As New SqlDataAdapter(sql, ConnstrPP)
        da.Fill(dsHatchingEggCustomer)
        cmbloc.DataSource = dsHatchingEggCustomer
        cmbloc.DataTextField = "acc"
        cmbloc.DataValueField = "acc"
        cmbloc.DataBind()


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

    Protected Sub btnsave_Click(sender As Object, e As EventArgs) Handles btnsave.Click
        sql = " insert into hatch_setter_eggsale([entry_date], [egg_coll_date], [ftype], [flock_no], [age], [f_taken], [f_set], [f_rej], [bg_taken], [bg_set],"
        sql &= " [bg_rej], [md_taken], [md_set], [md_rej], [mp_taken], [mp_set], [mp_rej], [mm_taken], [mm_set], [mm_rej], [comm_taken], "
        sql &= " [comm_set], [comm_rej], [hatchery], [machine],[f_leaker], [bg_leaker], [md_leaker], [mp_leaker], [mm_leaker], [comm_leaker],egg_coll_date_to) values("
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
        sql &= "'" & Val(mminusrej.Text) & "',"
        sql &= "'" & Val(hcomm.Text) & "',"
        sql &= "'" & Val(hcommser.Text) & "',"
        sql &= "'" & Val(hcommrej.Text) & "',"
        sql &= "'" & cmbloc.Text & "',"
        sql &= "'" & cmbshed.Text & "',"

        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & toeggcolldate.Text & "')"

        stockupdation()
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then

            ''sql = " update [dbo].[hatch_egg_grading] set [fresh_eggs] = [fresh_eggs] -'" & fresh.Text & "' ,[bigsize_eggs]=[bigsize_eggs] -'" & bigsize.Text & "',"
            ''sql &= " [md_eggs]= [md_eggs] - '" & md.Text & "' ,[mplus_eggs]=[mplus_eggs]-'" & mplus.Text & "',[mminus_eggs]=[mminus_eggs]-'" & mminus.Text & "',[hcomm_eggs]=[hcomm_eggs] - '" & hcomm.Text & "' "
            ''sql &= " where [bird_type]='" & ftype.Text & "' and [flock_no]= '" & flockno.Text & "' and egg_coll_date='" & eggcolldate.Text & "'"
            ''gmod.SqlExecuteNonQuery(sql)
            ''stockupdation()
  
            'Dim cmd As New SqlCommand("sp_DeductEggGrading", gmod.SqlConn)
            'cmd.CommandType = CommandType.StoredProcedure
            'cmd.Parameters.AddWithValue("@FromDate", eggcolldate.Text)
            'cmd.Parameters.AddWithValue("@ToDate", toeggcolldate.Text)
            'cmd.Parameters.AddWithValue("@BirdType", ftype.Text)
            'cmd.Parameters.AddWithValue("@FlockNo", flockno.Text)
            'cmd.Parameters.AddWithValue("@Fresh", Val(fresh.Text))
            'cmd.Parameters.AddWithValue("@BigSize", Val(bigsize.Text))
            'cmd.Parameters.AddWithValue("@MD", Val(md.Text))
            'cmd.Parameters.AddWithValue("@MPlus", Val(mplus.Text))
            'cmd.Parameters.AddWithValue("@MMinus", Val(mminus.Text))
            'cmd.Parameters.AddWithValue("@HComm", Val(hcomm.Text))
            'gmod.SqlConn.Open()
            'cmd.ExecuteNonQuery()
            'gmod.SqlConn.Close()

            clearControls()
            fillgrid()
            Button1_Click(sender, e)
        End If
    End Sub

    Public Sub stockupdation()
        sql = "select  * from [dbo].[hatch_egg_grading] where egg_coll_date between '" & eggcolldate.Text & "' and '" & toeggcolldate.Text & " ' and bird_type='" & ftype.Text & "' and flock_no='" & flockno.Text & "' order by egg_coll_date asc"
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
                    sql = "update [dbo].[hatch_egg_grading] set  fresh_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    fresh_data = fresh_data - fresh_le
                    fresh_le = fresh_le - fresh_data
                    sql = "update [dbo].[hatch_egg_grading] set  fresh_eggs = '" & fresh_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
                    sql = "update [dbo].[hatch_egg_grading] set  bigsize_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    bigsize_data = bigsize_data - bigsize_le
                    bigsize_le = bigsize_le - bigsize_data
                    sql = "update [dbo].[hatch_egg_grading] set  bigsize_eggs = '" & bigsize_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
                    sql = "update [dbo].[hatch_egg_grading] set  md_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    md_data = md_data - md_le
                    md_le = md_le - md_data
                    sql = "update [dbo].[hatch_egg_grading] set  md_eggs = '" & md_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
                    sql = "update [dbo].[hatch_egg_grading] set  mplus_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdplus_data = mdplus_data - mdplus_le
                    mdplus_le = mdplus_le - mdplus_data
                    sql = "update [dbo].[hatch_egg_grading] set  mplus_eggs = '" & md_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
                    sql = "update [dbo].[hatch_egg_grading] set  mminus_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdminus_data = mdminus_data - mdminus_le
                    mdminus_le = mdminus_le - mdminus_data
                    sql = "update [dbo].[hatch_egg_grading] set  mminus_eggs = '" & mdminus_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
                    sql = "update [dbo].[hatch_egg_grading] set  hcomm_eggs = 0 where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
                Else
                    mdminus_data = mdminus_data - comm_le
                    comm_le = comm_le - comm_data
                    sql = "update [dbo].[hatch_egg_grading] set  hcomm_eggs = '" & comm_data & "' where id = '" & id & "'"
                    gmod.SqlExecuteNonQuery(sql)
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
        sql = "select convert(varchar, [entry_date], 103) [Loading Date],[flock_no], [age], [f_taken], [f_set], [f_rej],[f_leaker], [bg_taken], [bg_set], [bg_rej],[bg_leaker], [md_taken], [md_set], [md_rej],[md_leaker], [mp_taken], [mp_set], [mp_rej], [mm_taken], [mp_leaker],[mm_set], [mm_rej], [mm_leaker],[comm_taken], [comm_set], [comm_rej] ,[comm_leaker] from [dbo].[hatch_setter_eggsale] where entry_date='" & entrydate.Text & "' and ftype='" & ftype.Text & "' and flock_no='" & flockno.Text & "'"
        gmod.DataSetRet(sql, "setter_entry")
        hatch_setter.DataSource = gmod.ds.Tables("setter_entry")
        hatch_setter.DataBind()
    End Sub

    Protected Sub btnShowArea_Click(sender As Object, e As EventArgs) Handles btnShowArea.Click
        fillFlock()
    End Sub
End Class
