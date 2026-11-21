Imports System.Data.SqlClient

'Egg Collection Form
Partial Class dailyentryforms_Daily_eggcollection
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            'fillFeedType()
            fillrid()
            fillEggCollectiionNo()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
            heggs.Attributes.Add("onkeyup", "sum()")
            commeggs.Attributes.Add("onkeyup", "sum()")
            commeggsmall.Attributes.Add("onkeyup", "sum()")
            crackeggs.Attributes.Add("onkeyup", "sum()")
            lekageeggs.Attributes.Add("onkeyup", "sum()")
            leakereggs.Attributes.Add("onkeyup", "sum()")
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

    Public Sub fillEggCollectiionNo()
        Try
            sql = "select isnull(max(collection_no),0) + 1 from [daily_eggcollection] "
            sql &= " where entry_date = '" & entrydate.Value & "' and shed_loc='" & shedloc.Text & "' and shed_no ='" & shedname.Text & "' and  line_no ='" & lineno.Text & "'"
            gmod.DataSetRet(sql, "collection_no")
            collection_no.Text = gmod.ds.Tables("collection_no").Rows(0)(0)
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_eggcollection", "fillEggCollectiionNo", Session("user_name") + "- " + Session("location_name"))
        End Try
       
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
        fillSum()
    End Sub

    Protected Sub lineno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lineno.SelectedIndexChanged
        Try
            fillEggCollectiionNo()
            fillrid()
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
        Catch ex As Exception
            lblflockno.Text = "-"
            lblfqty.Text = "-"
            lblbirdbreed.Text = "-"
            lblmqty.Text = "-"
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_eggcollection", "lineno_SelectedIndexChanged", Session("user_name") + "- " + Session("location_name"))

        End Try
    End Sub

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        'validation_check()

   If Session("user_name") Is Nothing OrElse
         String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Exit Sub
        End If


        fillEggCollectiionNo()

        If Val(totaleggs.Text) <> 0 Then
            If Val(totaleggs.Text) >= Val(lblfqty.Text) Then
                lblprint.Text = "Please Check Eggs"
                Exit Sub
            End If
        End If

        If entrydate.Value = "" Then
            lblprint.Text = "Please Select Date"
            Exit Sub
        End If
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = " insert into daily_eggcollection ([entry_date], [shed_loc], [shed_no],line_no,bird_type_code,flock_no, [hatch_eggs], [comm_eggs], [crack_eggs],"
        sql &= "[leakage_eggs], [total_eggs] ,collection_no,[user_name],comm_eggs_small,leakage_eggs_leaker) values ("

        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"

        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"

        sql &= "'" & Val(totaleggs.Text) - (Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text) + Val(commeggsmall.Text) + Val(leakereggs.Text)) & "',"
        sql &= "'" & Val(commeggs.Text) & "',"
        sql &= "'" & Val(crackeggs.Text) & "',"
        sql &= "'" & Val(lekageeggs.Text) & "',"
        'sql &= "'" & Val(heggs.Text) + Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text) & "',"
        sql &= "'" & Val(totaleggs.Text) & "',"
        sql &= "'" & Val(collection_no.Text) & "',"

        sql &= "'" & Session("user_name") & "',"
        sql &= "'" & Val(commeggsmall.Text) & "',"
        sql &= "'" & Val(leakereggs.Text) & "')"

        ' qyeryresult = gmod.SqlExecuteNonQuery(sql)
        'If qyeryresult = "SUCCESS" Then
        'lblprint.Text = "Egg Collection Data Save Successfully"
        'fillrid()
        'fillEggCollectiionNo()
        'Else
        'lblprint.Text = qyeryresult
        'End If

        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Egg Collection Data Save Successfully"
                    fillrid()
                    fillEggCollectiionNo()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_eggcollection", "Save_Egg_Collection", Session("user_name") + "- " + Session("location_name"))
        End Try
        ClearControls()
    End Sub
    Public Sub fillrid()
        'sql = "select * from daily_eggcollection where user_name='" & Session("user_name") & "'  and  shed_loc='" & shedloc.Text & "' and shed_no = '" & shedname.Text & "'  and line_no='" & lineno.Text & "' and entry_date='" & entrydate.Value & " '"
        sql = "select * from daily_eggcollection where user_name='" & Session("user_name") & "'  and  shed_loc='" & shedloc.Text & "' and entry_date='" & entrydate.Value & "'"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        dailyentry_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        dailyentry_grid.DataBind()
        fillSum()
    End Sub
    Public Sub fillSum()
        sql = "select sum(hatch_eggs) hatch_eggs,sum(comm_eggs) Comm_Big_A, sum(comm_eggs_small) Comm_Small_B , sum(crack_eggs) crack_eggs, Sum(leakage_eggs) Shell_Less , sum(leakage_eggs_leaker) Leaker,sum(hatch_eggs+comm_eggs+comm_eggs_small+crack_eggs+leakage_eggs  +leakage_eggs_leaker) Total  from daily_eggcollection where user_name='" & Session("user_name") & "'  and  shed_loc='" & shedloc.Text & "' and entry_date='" & entrydate.Value & " '"
        gmod.DataSetRet(sql, "tmp_daily_entry_show_sum")
        dgsum.DataSource = gmod.ds.Tables("tmp_daily_entry_show_sum")
        dgsum.DataBind()
    End Sub

    Public Sub ClearControls()
        heggs.Text = ""
        commeggs.Text = ""
        crackeggs.Text = ""
        lekageeggs.Text = ""
        totaleggs.Text = ""
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
    End Sub

    Protected Sub dailyentry_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dailyentry_grid.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex

        sql = "delete from daily_eggcollection where [id] ='" & dailyentry_grid.Items(y).Cells(13).Text & "'"
        'sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_no]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_no]='" & dailyentry_grid.Items(y).Cells(4).Text & "'"
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Egg Collection Data Deleted Successfully"
                    fillrid()
                    fillEggCollectiionNo()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_eggcollection", "Delete_Egg_Collection", Session("user_name") + "- " + Session("location_name"))
        End Try



        ' gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        fillrid()
    End Sub

    Protected Sub dailyentry_grid_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dailyentry_grid.SelectedIndexChanged

    End Sub

    Protected Sub dgsum_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dgsum.SelectedIndexChanged

    End Sub
End Class
