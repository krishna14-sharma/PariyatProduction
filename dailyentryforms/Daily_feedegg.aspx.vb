Imports System.Data.SqlClient

'Shed Entry From
Partial Class dailyentryforms_Daily_feedegg
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillFeedType()
            fillrid()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
            heggs.Attributes.Add("onkeyup", "sum()")
            commeggs.Attributes.Add("onkeyup", "sum()")
            crackeggs.Attributes.Add("onkeyup", "sum()")
            lekageeggs.Attributes.Add("onkeyup", "sum()")

            fqtyfperbird.Attributes.Add("onkeyup", "calctotfeed()")
            fqtymperbird.Attributes.Add("onkeyup", "calctotfeedm()")

            fillEggs()

        Else

        End If
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'  and location_code = '" & Session("area_code") & "'"
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & shedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_")
        shedname.DataSource = gmod.ds.Tables("shed_")
        shedname.DataTextField = "shed_name"
        shedname.DataValueField = "shed_code"
        shedname.DataBind()
        shedname_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub shedname_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedname.SelectedIndexChanged
        sql = "select * from [dbo].[shed_line_master] where shed_code = '" & shedname.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_line_master")
        lineno.DataSource = gmod.ds.Tables("shed_line_master")
        lineno.DataTextField = "line_no"
        lineno.DataValueField = "line_code"
        lineno.DataBind()
        lineno_SelectedIndexChanged(sender, e)
    End Sub

    Public Sub fillEggs()

        Try
            sql = "select  isnull(sum(hatch_eggs),0) hatch_eggs, isnull(sum(comm_eggs),0) comm_eggs, isnull(sum(comm_eggs_small),0) comm_eggs_small, isnull(sum(crack_eggs),0) crack_eggs,isnull(sum(leakage_eggs),0) leakage_eggs, isnull(sum(leakage_eggs_leaker),0) leakage_eggs_leaker,isnull(sum(total_eggs),0) total_eggs from [daily_eggcollection] "
            sql &= " where entry_date = '" & entrydate.Value & "' and shed_loc='" & shedloc.Text & "' and shed_no ='" & shedname.Text & "' and  line_no ='" & lineno.Text & "'"

            gmod.DataSetRet(sql, "egg_dailt_date")

            heggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(0)
            commeggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(1)

            commeggsmall.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(2)

            crackeggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(3)

            leakereggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(4)

            lekageeggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(5)

            totaleggs.Text = gmod.ds.Tables("egg_dailt_date").Rows(0)(6)

            Dim heggper As Double
            heggper = (Val(heggs.Text) / Val(totaleggs.Text)) * 100

            hegg_per.Text = heggper.ToString("N2")

            heggper = (Val(totaleggs.Text) / Val(lblfqty.Text)) * 100

            prodeggper.Text = heggper.ToString("N2")

            '  sql = "select sum(total_eggs) total_eggs from [dbo].[tmp_daily_entry] "
            ' sql &= " where entry_date = '" & entrydate.Value & "' and shed_loc='" & shedloc.Text & "' and shed_no ='" & shedname.Text & "' and  line_no ='" & lineno.Text & "'"
            ' gmod.DataSetRet(sql, "eggplusminus")

            sql = "select sum(total_eggs) total_eggs from [dbo].[tmp_daily_entry] "
            sql &= " where entry_date = '" & CDate(entrydate.Value).AddDays(-1) & "' and shed_loc='" & shedloc.Text & "' and shed_no ='" & shedname.Text & "' and  line_no ='" & lineno.Text & "'"
            gmod.DataSetRet(sql, "eggplusminus1")


            Dim diffegg As Integer = 0

            diffegg = Val(totaleggs.Text) - Val(gmod.ds.Tables("eggplusminus1").Rows(0)(0))

            eggprevday.Text = diffegg.ToString

            If Val(eggprevday.Text) > 0 Then
                eggprevday.ForeColor = System.Drawing.Color.White
                eggprevday.BackColor = System.Drawing.Color.Green
            Else
                eggprevday.ForeColor = System.Drawing.Color.White
                eggprevday.BackColor = System.Drawing.Color.Red
            End If


        Catch ex As Exception

        End Try


    End Sub

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click

         If Session("user_name") Is Nothing OrElse
   String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Exit Sub
        End If

        If entrydate.Value = "" Then
            lblprint.Text = "Please Select Date"
            Exit Sub
        End If
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        If heggs.Text = "" Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[tmp_daily_entry]([entry_date], [shed_loc], [shed_no],line_no,bird_type_code,flock_no, [hatch_eggs], [comm_eggs], [crack_eggs],"
        sql &= "[leakage_eggs], [total_eggs], [Feed_type_female], [feed_qty_female], [Feed_type_male], [feed_qty_male], [body_wt_female],"
        sql &= "[body_wt_male], [egg_wt], [user_name], [isPosted],comm_eggs_small,leakage_eggs_leaker,per_bird_feed_f,per_bird_feed_m,prevday,Remark) values ("

        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"

        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"

        ' sql &= "'" & Val(totaleggs.Text) - (Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text)) & "',"
        sql &= "'" & Val(heggs.Text) & "',"
        sql &= "'" & Val(commeggs.Text) & "',"
        sql &= "'" & Val(crackeggs.Text) & "',"
        sql &= "'" & Val(leakereggs.Text) & "',"
        ' sql &= "'" & Val(heggs.Text) + Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text) & "',"
        sql &= "'" & Val(totaleggs.Text) & "',"

        sql &= "'" & ftypef.SelectedValue & "',"
        sql &= "'" & Val(fqtyf.Text) & "',"
        sql &= "'" & ftypem.SelectedValue & "',"
        sql &= "'" & Val(lblmqty.Text) * Val(fqtymperbird.Text) & "',"
        sql &= "'" & Val(bodywtf.Text) & "',"
        sql &= "'" & Val(bodywtm.Text) & "',"
        sql &= "'" & Val(eggwt.Text) & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'0',"
        sql &= "'" & Val(commeggsmall.Text) & "',"
        sql &= "'" & Val(lekageeggs.Text) & "',"

        sql &= "'" & Val(fqtyfperbird.Text) & "',"
        sql &= "'" & Val(fqtymperbird.Text) & "',"
        sql &= "'" & Val(eggprevday.Text) & "',"
        sql &= "'" & txtremarks.Text & "')"


        'qyeryresult = gmod.SqlExecuteNonQuery(sql)
        'If qyeryresult = "SUCCESS" Then
        'lblprint.Text = "Data Saved Successfully"
        'fillrid()
        'Else
        'lblprint.Text = qyeryresult
        'End If
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Feed/Egg Data Save Successfully"
                    fillrid()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_Feedegg", "Save_FeedEgg_Collection", Session("user_name") + "- " + Session("location_name"))
        End Try
        ClearControls()
    End Sub
    Public Sub ClearControls()
        heggs.Text = ""
        commeggs.Text = ""
        crackeggs.Text = ""
        lekageeggs.Text = ""
        totaleggs.Text = ""
        fqtyf.Text = ""
        fqtym.Text = ""
        bodywtf.Text = ""
        bodywtm.Text = ""
        eggwt.Text = ""
        eggprevday.Text = ""
        fqtyfperbird.Text = ""
        fqtymperbird.Text = ""
        commeggsmall.Text = ""
        leakereggs.Text = ""


    End Sub
    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Value = "" Then
            valiation = False
        End If

        If fqtyf.Text <> "" Or fqtym.Text <> "" Then
            valiation = True
        Else
            valiation = False
        End If


        'If bodywtf.Text = "" Or bodywtm.Text = "" Then
        '    valiation = False
        'Else
        '    valiation = True
        'End If

    End Sub

    Public Sub fillFeedType()
        sql = "select * from [dbo].[product_master] where Prd_category = 'FEED'"
        gmod.DataSetRet(sql, "feed_type")
        ftypef.DataSource = gmod.ds.Tables("feed_type")
        ftypef.DataTextField = "Prd_name"
        ftypef.DataValueField = "prd_code"
        ftypef.DataBind()


        sql = "select * from [dbo].[product_master] where Prd_category = 'FEED'"
        gmod.DataSetRet(sql, "feed_type_m")
        ftypem.DataSource = gmod.ds.Tables("feed_type_m")
        ftypem.DataTextField = "Prd_name"
        ftypem.DataValueField = "prd_code"
        ftypem.DataBind()


    End Sub

    Protected Sub lineno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lineno.SelectedIndexChanged
        Try
            sql = "select [bird_type_code], [shed_code],[flock_no] from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 "
            gmod.DataSetRet(sql, "shed_data")

            sql = "select Sum(isnull([fqty],0)) fqty ,Sum(isnull([mqty],0))  mqty from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 and flock_no = '" & gmod.ds.Tables("shed_data").Rows(0)("flock_no").ToString & "' "
            gmod.DataSetRet(sql, "shed_qtydata")
            If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
                lblbirdbreed.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
                lblflockno.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
                lblfqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("fqty").ToString
                lblmqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("mqty").ToString

                sql = "	select DATEDIFF(d,hatch_date,GETDATE())/7 Weeks , (DATEDIFF(d,hatch_date,GETDATE()) % 7)+1  Days   from flock_master  where flock_no='" & lblflockno.Text & "'"
                gmod.DataSetRet(sql, "shed_data_bird_age")
                lblbirdbreed.Text = gmod.ds.Tables("shed_data_bird_age").Rows(0)("Weeks").ToString + "-" + gmod.ds.Tables("shed_data_bird_age").Rows(0)("Days").ToString

            Else
                lblflockno.Text = "-"
                lblfqty.Text = "-"
                lblbirdbreed.Text = "-"
                lblmqty.Text = "-"
            End If

            fillEggs()
        Catch ex As Exception
            lblflockno.Text = "-"
            lblfqty.Text = "-"
            lblbirdbreed.Text = "-"
            lblmqty.Text = "-"

        End Try
    End Sub
    Public Sub fillrid()
        sql = "select * from [dbo].[tmp_daily_entry] where user_name='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        dailyentry_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        dailyentry_grid.DataBind()
    End Sub


    Protected Sub shedname_Load(sender As Object, e As EventArgs) Handles shedname.Load
        lineno_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub fqtyfperbird_TextChanged(sender As Object, e As EventArgs) Handles fqtyfperbird.TextChanged
        fqtyf.Text = Val(fqtyfperbird.Text) * Val(lblfqty.Text)
    End Sub

    Protected Sub fqtymperbird_TextChanged(sender As Object, e As EventArgs) Handles fqtymperbird.TextChanged
        fqtym.Text = Val(fqtymperbird.Text) * Val(lblfqty.Text)
    End Sub

    Protected Sub dailyentry_grid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dailyentry_grid.SelectedIndexChanged

    End Sub

    Protected Sub dailyentry_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dailyentry_grid.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex

        sql = "delete from [dbo].[tmp_daily_entry]  where [id] ='" & dailyentry_grid.Items(y).Cells(17).Text & "'"
        'sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_no]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_no]='" & dailyentry_grid.Items(y).Cells(4).Text & "'"

        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Data Deleted Successfully"
                    fillrid()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_feedeggcollection", "Delete_feedEgg_Collection", Session("user_name") + "- " + Session("location_name"))
        End Try



        'gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        'fillrid()
    End Sub
End Class
