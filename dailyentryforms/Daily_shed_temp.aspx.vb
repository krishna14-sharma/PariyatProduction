
Partial Class dailyentryforms_Daily_shed_temp
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillLocation()
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

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        If entrydate.Value = "" Then
            AppUi.ShowError(Me, "Please Select Date")
            Exit Sub
        End If

        sql = "insert into [dbo].[daily_shed_temp]([entry_date], [entry_time], [shed_in], [shed_out], [location], [humidiy_inside], [humudity_outside],day_night,feed_cleanup_time) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & time6.Text & "',"

        sql &= "'" & Val(tempinside.Text) & "',"
        sql &= "'" & Val(tempoutside.Text) & "',"
        sql &= "'" & shedloc.Text & "',"

        sql &= "'" & Val(humidityinside.Text) & "',"
        sql &= "'" & Val(humidityoutside.Text) & "',"
        sql &= "'" & day_night.Text & "',"
        sql &= "'" & feedcleanuptime.Text & "')"

        AppUi.ShowMessage(Me, gmod.SqlExecuteNonQuery(sql))
        tempinside.Text = ""
        tempoutside.Text = ""
        humidityinside.Text = ""
        humidityoutside.Text = ""
        feedcleanuptime.Text = ""
        fillrid()
    End Sub
    Sub fillrid()
        sql = "select * from [dbo].[daily_shed_temp] where entry_date = '" & entrydate.Value & "' and location ='" & shedloc.Text & "'"
        gmod.DataSetRet(sql, "daily_shed_temp")
        dgtemerpatue.DataSource = gmod.ds.Tables("daily_shed_temp")
        dgtemerpatue.DataBind()
    End Sub

    Protected Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        fillrid()
        ' Response.Write(entrydate.Value)   ' debug output removed (Response.Write breaks AJAX postbacks)
    End Sub
    Protected Sub dgtemerpatue_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgtemerpatue.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex

        sql = "delete from daily_shed_temp where [id] ='" & dgtemerpatue.Items(y).Cells(7).Text & "'"
        'sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_no]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_no]='" & dailyentry_grid.Items(y).Cells(4).Text & "'"
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        fillrid()
    End Sub
End Class
