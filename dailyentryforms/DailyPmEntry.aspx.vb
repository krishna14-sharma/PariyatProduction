Imports System.Data.SqlClient

Partial Class dailyentryforms_DailyPmEntry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        If Session("user_name") Is Nothing OrElse
        String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            lblprint.Text = "INVALID USER NAME"
            Exit Sub
        End If

        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into PM_data([entry_date], [shed_loc], [shed_name], [line_pen], [age], [flock_no], [disease_name], [pm_qtyf], [remarks],user_name,pm_qtym)  values ("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"
        sql &= "'" & diseaselist.Text & "',"
        sql &= "'" & morqtyf.Text & "',"
        sql &= "'" & txtremarks.Text & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'" & morqtym.Text & "')"



        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "PM Data Saved Successfully"
                    fillrid()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "DailyPMEntry", "SavePM", Session("user_name") + "- " + Session("location_name"))
        End Try

        'qyeryresult = gmod.SqlExecuteNonQuery(sql)
        'If qyeryresult = "SUCCESS" Then
        'lblprint.Text = "PM Data Saved Successfully"
        'fillrid()
        'Else
        'lblprint.Text = qyeryresult
        'End If
    End Sub

    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Value = "" Then
            valiation = False
        End If

        If morqtyf.Text = "" Then
            valiation = False
        Else
            valiation = True
        End If


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillDisease()
            'fillFeedType()
            fillrid()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
            'heggs.Attributes.Add("onkeyup", "sum()")
            'commeggs.Attributes.Add("onkeyup", "sum()")
            'crackeggs.Attributes.Add("onkeyup", "sum()")
            'lekageeggs.Attributes.Add("onkeyup", "sum()")
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
        Catch ex As Exception

        End Try
    End Sub


    Public Sub fillDisease()
        sql = "select * from [dbo].[Disease_list]  where disease_name <>'' order by disease_name"
        gmod.DataSetRet(sql, "diseaselist")
        diseaselist.DataSource = gmod.ds.Tables("diseaselist")
        diseaselist.DataTextField = "disease_name"
        diseaselist.DataValueField = "disease_name"
        diseaselist.DataBind()
    End Sub

    Public Sub fillrid()
        Try
            sql = "select * from Pm_data where user_name='" & Session("user_name") & "' and isPosted=0 "
            sql &= " and entry_date='" & entrydate.Value & "'"
            gmod.DataSetRet(sql, "Pm_data")
            dailyentry_grid.DataSource = gmod.ds.Tables("Pm_data")
            dailyentry_grid.DataBind()


            sql = "select  sum(pm_qtyf) Female , sum(pm_qtym) Male from PM_data  where flock_no in("
            sql &= " select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ( select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')) "
            sql &= " and entry_date='" & entrydate.Value & "'"

            gmod.DataSetRet(sql, "Pm_data_consolidate")
            showsum.DataSource = gmod.ds.Tables("Pm_data_consolidate")
            showsum.DataBind()

        Catch ex As Exception

        End Try

    End Sub

    Protected Sub dailyentry_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dailyentry_grid.DeleteCommand
        Try
            Dim y As Int64
            Dim z As String = ""

            y = e.Item.ItemIndex
            sql = "delete from Pm_data where [id] ='" & dailyentry_grid.Items(y).Cells(8).Text & "'"
            Try
                Using conn As New SqlConnection(gmod.Connstr)
                    conn.Open()
                    Using cmd As New SqlCommand(sql, conn)
                        cmd.ExecuteNonQuery()
                        lblprint.Text = "Delete PM Data Saved Successfully"
                        fillrid()
                    End Using
                End Using
            Catch ex As Exception
                lblprint.Text = ex.Message
                gmod.LogErrorToFile(ex.Message.ToString, "DailyPMEntry", "DeletePM", Session("user_name") + "-" + Session("location_name"))
            End Try
        Catch ex As Exception

        End Try

        'sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_no]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_no]='" & dailyentry_grid.Items(y).Cells(4).Text & "'"
        'gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        'fillrid()


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillrid()
    End Sub
End Class
