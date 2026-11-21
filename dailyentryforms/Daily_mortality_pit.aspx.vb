Imports System.Data.SqlClient

Partial Class dailyentryforms_Daily_mortality_pit
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            ' fillFeedType()
            ' fillrid()
            shedlocation_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
        Else

        End If
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'  and location_code = '" & Session("area_code") & "'"
        gmod.DataSetRet(sql, "shed_loc")
        shedlocation.DataSource = gmod.ds.Tables("shed_loc")
        shedlocation.DataTextField = "location_name"
        shedlocation.DataValueField = "location_code"
        shedlocation.DataBind()
    End Sub

    Protected Sub shedlocation_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedlocation.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & shedlocation.SelectedValue & "'"
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

            sql = "select Sum(isnull([fqty],0)) fqty ,Sum(isnull([mqty],0))  mqty from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 "
            gmod.DataSetRet(sql, "shed_qtydata")


            If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
                Label2.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
                lblflockfem.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
                lblfem.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("fqty").ToString
                lblmale.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("mqty").ToString
            Else
                lblflockfem.Text = "-"
                lblfem.Text = "-"
                Label2.Text = "-"
                lblmale.Text = "-"
            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub btnSAvemortality_Click(sender As Object, e As EventArgs) Handles btnSAvemortality.Click
        If Session("user_name") Is Nothing OrElse
            String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Exit Sub
        End If

        If Session("user_name") Is Nothing OrElse
    String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then

            Response.Redirect("~/Login.aspx", False)
            Context.ApplicationInstance.CompleteRequest()
            Return

        End If

        If String.IsNullOrWhiteSpace(lblflockfem.Text) Then
            Exit Sub
        End If


        validation_check()

        If Val(Txtfemalemort.Text) > Val(lblfem.Text) Then
            lblprint.Text = "Please Check the Mortality"
            Exit Sub
        End If


        If Val(TxtMalemort.Text) > Val(lblmale.Text) Then
            lblprint.Text = "Please Check the Mortality"
            Exit Sub
        End If

        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[tmp_daily_morpit]([entry_date], [shed_loc], [shed_no], [line_no], [bird_type_code], [flock_no], [mor_fem], [mor_male], [pit_fem], [pit_male], [sale_fem], [sale_male], [user_name], [isPosted],pit_fem_ext,pit_male_ext) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedlocation.Text & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & lineno.Text & "',"

        sql &= "'" & Label2.Text & "',"
        sql &= "'" & lblflockfem.Text & "',"

        sql &= "'" & Val(Txtfemalemort.Text) & "',"
        sql &= "'" & Val(TxtMalemort.Text) & "',"

        sql &= "'" & Val(txtpitfem.Text) & "',"
        sql &= "'" & Val(txtpitmal.Text) & "',"

        sql &= "'" & Val(TxtFemalesale.Text) & "',"
        sql &= "'" & Val(TxtMaleSale.Text) & "',"

        sql &= "'" & Session("user_name") & "',"
        sql &= "'0',"

        sql &= "'" & Val(TxtFemalepit.Text) & "',"
        sql &= "'" & Val(TxtMalePit.Text) & "')"

        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Mortality Data Save Successfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_mortality_pit", "Save_Mortality_pit", Session("user_name") + "- " + Session("location_name"))
        End Try
        ClearControls()


        ' qyeryresult = gmod.SqlExecuteNonQuery(sql)
        'If qyeryresult = "SUCCESS" Then
        'lblprint.Text = "Chicks Purchase Data Saved Successfully"
        'fillgrid()
        '  Else
        'lblprint.Text = qyeryresult
        'End If

    End Sub

    Public Sub ClearControls()
        Label2.Text = ""
        lblflockfem.Text = ""
        Txtfemalemort.Text = ""
        TxtMalemort.Text = ""
        'txtpitfem.Text = ""
        'txtpitmal.Text = ""
        TxtFemalesale.Text = ""
        TxtMaleSale.Text = ""
        TxtFemalepit.Text = ""
        TxtMalePit.Text = ""
        txtpitfem.Text = ""
        txtpitmal.Text = ""
    End Sub

    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Value = "" Then
            valiation = False
        End If
    End Sub

    Protected Sub IStime_CheckedChanged(sender As Object, e As EventArgs) Handles IStime.CheckedChanged
        If IStime.Checked = True Then
            gmod.timeflag = 1
        Else
            gmod.timeflag = 0
        End If
    End Sub

    Public Sub fillgrid()
        Try
            sql = "select   convert(varchar, entry_date, 5) [entry_date], [shed_no],   [flock_no], [mor_fem], [mor_male], [pit_fem], [pit_male], [sale_fem], [sale_male] , [pit_fem_ext], [pit_male_ext],id from tmp_daily_morpit where shed_loc='" & shedlocation.Text & "'  and entry_date='" & entrydate.Value & "'"
            gmod.DataSetRet(sql, "mtmpmorpit")
            morptdata.DataSource = gmod.ds.Tables("mtmpmorpit")
            morptdata.DataBind()
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
        mortality_sum()
    End Sub

    Public Sub mortality_sum()
        sql = "select sum(mor_fem) Mort_F, sum(mor_male) Mort_M , sum(pit_fem_ext) Pit_F, sum(pit_male_ext) Pit_M, sum(pit_fem) Sexing_Error_F, sum(pit_male) Sexing_Error_M,sum(sale_fem) Sale_F, sum(sale_male) Sale_M "
        sql &= "  from tmp_daily_morpit where shed_loc='" & shedlocation.Text & "'  and entry_date='" & entrydate.Value & "'"

        gmod.DataSetRet(sql, "sum_mortality")
        sum_mor.DataSource = gmod.ds.Tables("sum_mortality")
        sum_mor.DataBind()

    End Sub
    Protected Sub mordata_Click(sender As Object, e As EventArgs) Handles mordata.Click
        fillgrid()
    End Sub

    Protected Sub morptdata_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles morptdata.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex

        sql = "delete from tmp_daily_morpit where [id] ='" & morptdata.Items(y).Cells(11).Text & "' and isPosted=0"
        'sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_no]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_no]='" & dailyentry_grid.Items(y).Cells(4).Text & "'"
        ' gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        'fillgrid()
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Mortality Data Deleted Successfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            lblprint.Text = ex.Message
            gmod.LogErrorToFile(ex.Message.ToString, "Daily_mortality_pit", "Delete_Mortality_pit", Session("user_name") + "- " + Session("location_name"))
        End Try

    End Sub

    Protected Sub morptdata_SelectedIndexChanged(sender As Object, e As EventArgs) Handles morptdata.SelectedIndexChanged

    End Sub
End Class
