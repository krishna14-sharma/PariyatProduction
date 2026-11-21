
Partial Class pit_egg_recived
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

    Public Sub fillFlock()
        ' sql = "select flock_no from [dbo].[flock_master] where isAlive=1"
        sql = "select distinct flock_no from Shed_Entry_Data where isAlive = 1"
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
            ' txthatchegg.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("hatch_eggs")
            TxtCrackEgg.Text = gmod.ds.Tables("eggcollfrmfarm").Rows(0)("crack_eggs")
            lblString.Text = ""
        Catch ex As Exception
            lblString.Text = ex.Message
            txteggs.Text = "0"
            TxtCommEggs.Text = "0"
            TxtLeakEggs.Text = "0"
            ' txthatchegg.Text = "0"
            TxtCrackEgg.Text = "0"
        End Try



        Try
            sql = "select  isnull(hcomm_eggs_rej,0) hcomm_eggs, hat_crack_eggs,hat_leak_eggs from [dbo].[hatch_egg_grading_entry]"
            sql &= " where egg_coll_date='" & eggcolldate.Text & "' and flock_no='" & flockno.Text & "'"

            gmod.DataSetRet(sql, "hatcrackleaker")
            hcrack.Text = gmod.ds.Tables("hatcrackleaker").Rows(0)("hat_crack_eggs")
            hleaker.Text = gmod.ds.Tables("hatcrackleaker").Rows(0)("hat_leak_eggs")
            hcomm.Text = gmod.ds.Tables("hatcrackleaker").Rows(0)("hcomm_eggs")
        Catch ex As Exception

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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' sql = "update [dbo].[hatch_pit_egg_coll_data] set isReceived= 1 where egg_coll_date='" & eggcolldate.Text & "' and flock_no='" & flockno.Text & "' and bird_type='" & cmbftype.Text & "'"
        'gmod.SqlExecuteNonQuery(sql)

        sql = "update [dbo].[hatch_pit_egg_collection] set isReceived= 1 where egg_coll_date='" & eggcolldate.Text & "' and flock_no='" & flockno.Text & "' and bird_type='" & cmbftype.Text & "'"
        Try
            qyeryresult = gmod.SqlExecuteNonQuery(sql)
            If qyeryresult = "SUCCESS" Then
                lblString.Text = "Data Saved Successfully"
                fillrid()
            Else
                lblString.Text = qyeryresult
            End If
            ClearControls()
        Catch ex As Exception

        End Try

    End Sub
    Public Sub ClearControls()
        TxtCommEggs.Text = ""
        TxtCrackEgg.Text = ""
        TxtLeakEggs.Text = ""
        hcrack.Text = ""
        hleaker.Text = ""
    End Sub

    Public Sub fillrid()
        sql = "select   convert(varchar, [egg_coll_date], 103) Coll_Date, [bird_type] Type, [flock_no] Flock, [Age], ([comm_eggs]+[crack_eggs]+[leakge_eggs]+ [hatch_crack]+[hatch_leaker]) [Totl Eggs],[comm_eggs] Comm , [crack_eggs] Crack, [leakge_eggs] Leaker, [hatch_Comm] HComm, [hatch_crack] HCrack, [hatch_leaker] HLeaker,[cl_code] CL, [user_name] [User] from [dbo].[hatch_pit_egg_collection] where egg_coll_date='" & eggcolldate.Text & "' and isReceived= 1 "
        gmod.DataSetRet(sql, "tmp_daily_entry_show1")
        eggp.DataSource = gmod.ds.Tables("tmp_daily_entry_show1")
        eggp.DataBind()
        lblString.Text = ""
    End Sub

    Protected Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        fillrid()
    End Sub

    Protected Sub cmbloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbloc.SelectedIndexChanged

    End Sub
End Class
