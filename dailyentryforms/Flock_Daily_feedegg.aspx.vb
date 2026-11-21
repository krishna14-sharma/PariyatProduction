
Partial Class dailyentryforms_Daily_feedegg
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            entrydate.Text = d
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillFlock()
            fillFeedType()
            fillrid()
            flockno_SelectedIndexChanged(sender, e)
            heggs.Attributes.Add("onkeyup", "sum()")
            commeggs.Attributes.Add("onkeyup", "sum()")
            crackeggs.Attributes.Add("onkeyup", "sum()")
            lekageeggs.Attributes.Add("onkeyup", "sum()")
        Else

        End If
    End Sub

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[flock_tmp_daily_entry]([entry_date], bird_type_code,flock_no, [hatch_eggs], [comm_eggs], [crack_eggs],"
        sql &= "[leakage_eggs], [total_eggs], [Feed_type_female], [feed_qty_female], [Feed_type_male], [feed_qty_male], [body_wt_female],"
        sql &= "[body_wt_male], [egg_wt], [user_name], [isPosted]) values ("

        sql &= "'" & entrydate.Text & "',"

        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"

        sql &= "'" & Val(heggs.Text) & "',"
        sql &= "'" & Val(commeggs.Text) & "',"
        sql &= "'" & Val(crackeggs.Text) & "',"
        sql &= "'" & Val(lekageeggs.Text) & "',"
        sql &= "'" & Val(heggs.Text) + Val(commeggs.Text) + Val(crackeggs.Text) + Val(lekageeggs.Text) & "',"

        sql &= "'" & ftypef.SelectedValue & "',"
        sql &= "'" & Val(fqtyf.Text) & "',"
        sql &= "'" & ftypem.SelectedValue & "',"
        sql &= "'" & Val(fqtym.Text) & "',"
        sql &= "'" & Val(bodywtf.Text) & "',"
        sql &= "'" & Val(bodywtm.Text) & "',"
        sql &= "'" & Val(eggwt.Text) & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'0')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chicks Purchase Data Saved Successfully"
            fillrid()
        Else
            lblprint.Text = qyeryresult
        End If
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
    End Sub

    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Text = "" Then
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

    Public Sub fillrid()
        sql = "select * from [dbo].[flock_tmp_daily_entry] where user_name='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        dailyentry_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        dailyentry_grid.DataBind()
    End Sub

    Public Sub fillFlock()
        sql = "select flock_no from [dbo].[flock_master] where isAlive=1"
        gmod.DataSetRet(sql, "data_flock")
        flockno.DataSource = gmod.ds.Tables("data_flock")
        flockno.DataTextField = "flock_no"
        flockno.DataValueField = "flock_no"
        flockno.DataBind()

    End Sub

    Protected Sub flockno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles flockno.SelectedIndexChanged
        Try
            sql = "select [bird_type_code], [flock_no],[fqty],[mqty] from flock_data where flock_no='" & flockno.SelectedValue & "' and isAlive=1 "
            gmod.DataSetRet(sql, "shed_data")
            If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
                lblbirdbreed.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
                lblflockno.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
                lblfqty.Text = gmod.ds.Tables("shed_data").Rows(0)("fqty").ToString
                lblmqty.Text = gmod.ds.Tables("shed_data").Rows(0)("mqty").ToString
            Else
                lblflockno.Text = "-"
                lblfqty.Text = "-"
                lblbirdbreed.Text = "-"
                lblmqty.Text = "-"
            End If
        Catch ex As Exception

        End Try
          entrydate.Text = d
    End Sub

    Protected Sub entrydate_TextChanged(sender As Object, e As EventArgs) Handles entrydate.TextChanged
        d = entrydate.Text
    End Sub
End Class
