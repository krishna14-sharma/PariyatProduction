
Partial Class dailyentryforms_DailyWeather
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
            'fillrid()
            shedloc_SelectedIndexChanged(sender, e)
            'shedname_SelectedIndexChanged(sender, e)
            'lineno_SelectedIndexChanged(sender, e)
            'heggs.Attributes.Add("onkeyup", "sum()")
            'commeggs.Attributes.Add("onkeyup", "sum()")
            'crackeggs.Attributes.Add("onkeyup", "sum()")
            'lekageeggs.Attributes.Add("onkeyup", "sum()")
        Else

        End If
    End Sub

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged

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
        If Session("user_name") Is Nothing OrElse
        String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            lblprint.Text = "INVALID USER NAME"
            Exit Sub
        End If

        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[tmp_daily_entry]([entry_date], [shed_loc], [shed_no],line_no,bird_type_code,flock_no, [hatch_eggs], [comm_eggs], [crack_eggs],"
        sql &= "[leakage_eggs], [total_eggs], [Feed_type_female], [feed_qty_female], [Feed_type_male], [feed_qty_male], [body_wt_female],"
        sql &= "[body_wt_male], [egg_wt], [user_name], [isPosted]) values ("

        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"

        'sql &= "'" & lineno.Text & "',"
        'sql &= "'" & lblbirdbreed.Text & "',"
        'sql &= "'" & lblflockno.Text & "',"

        'sql &= "'" & Val(heggs.Text) & "',"
        'sql &= "'" & Val(commeggs.Text) & "',"
        'sql &= "'" & Val(crackeggs.Text) & "',"
        'sql &= "'" & Val(lekageeggs.Text) & "',"
        'sql &= "'" & Val(heggs.Text) + Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text) & "',"

        'sql &= "'" & ftypef.SelectedValue & "',"
        'sql &= "'" & Val(fqtyf.Text) & "',"
        'sql &= "'" & ftypem.SelectedValue & "',"
        'sql &= "'" & Val(fqtym.Text) & "',"
        'sql &= "'" & Val(bodywtf.Text) & "',"
        'sql &= "'" & Val(bodywtm.Text) & "',"
        'sql &= "'" & Val(eggwt.Text) & "',"
        'sql &= "'" & Session("user_name") & "',"
        'sql &= "'0')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chicks Purchase Data Saved Successfully"
            'fillrid()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub
End Class
