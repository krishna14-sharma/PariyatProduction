
Partial Class dailyentryforms_Daily_bird_sepration_entry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

   

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)

            fillresaonFeMale()
            fillresaonMale()
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
            'fillEggCollectiionNo()
            'fillrid()
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
    Public Sub fillresaonFeMale()
        sql = "select * from [dbo].[Sepration_master]"
        gmod.DataSetRet(sql, "Sepration_master_male")
        reasonfem.DataSource = gmod.ds.Tables("Sepration_master_male")
        reasonfem.DataTextField = "seperation_reason"
        reasonfem.DataValueField = "seperation_reason"
        reasonfem.DataBind()

    End Sub


    Public Sub fillresaonMale()
        sql = "select * from [dbo].[Sepration_master]"
        gmod.DataSetRet(sql, "Sepration_master_male")
        reasonmale.DataSource = gmod.ds.Tables("Sepration_master_male")
        reasonmale.DataTextField = "seperation_reason"
        reasonmale.DataValueField = "seperation_reason"
        reasonmale.DataBind()

    End Sub

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        sql = "insert into [dbo].[daily_sepration]([entry_date], [shed_loc], [shed_no], [line_no], [bird_type_code], [flock_no], [reason_female], [sep_fem], [reason_male], [sep_male], [remarks], [user_name]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"

        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"

        sql &= "'" & reasonfem.Text & "',"
        sql &= "'" & Val(fqty.Text) & "',"

        sql &= "'" & reasonmale.Text & "',"
        sql &= "'" & Val(mqty.Text) & "',"

        sql &= "'" & reason.Text & "',"
        sql &= "'" & Session("user_name") & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chattan Data Save Successfully"
            fillgrid()
            'fillEggCollectiionNo()
        Else
            lblprint.Text = qyeryresult
        End If
        ' ClearControls()
    End Sub


    Public Sub fillgrid()
        sql = "select * from [dbo].[daily_sepration] where entry_date = '" & entrydate.Value & "' and user_name='" & Session("user_name") & "' "
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        dgchattan.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        dgchattan.DataBind()

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillgrid()
    End Sub

    Protected Sub dgchattan_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgchattan.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex
        sql = "delete from daily_sepration where [id] ='" & dgchattan.Items(y).Cells(13).Text & "' and isPosted=0"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chattan Data Deleted Successfully"
        End If
        'Response.Write(dgchattan.Items(y).Cells(13).Text)
    End Sub
End Class
