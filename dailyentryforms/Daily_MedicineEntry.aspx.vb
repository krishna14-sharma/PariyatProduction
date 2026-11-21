
Partial Class dailyentryforms_Daily_MedicineEntry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillrid()
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillmedicine()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
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

    Public Sub fillmedicine()
        sql = "select med_code,med_name from [dbo].[Med_master] order by med_name"
        gmod.DataSetRet(sql, "med-master")
        medlist.DataSource = gmod.ds.Tables("med-master")
        medlist.DataTextField = "med_code"
        medlist.DataTextField = "med_name"
        medlist.DataValueField = "med_code"
        medlist.DataBind()
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
        fillrid()
    End Sub

    Protected Sub lineno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lineno.SelectedIndexChanged
        Try
            'fillEggCollectiionNo()
            ' fillrid()
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

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click

        validation_check()

        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If


        sql = "insert into [dbo].[daily_medicine]([entry_date], [shed_loc], [shed_no], [line_no], "
        sql &= "[age], [flock_no], med_code,med_name,[qtypbird], [totqty], [remarks], [user_name]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & lineno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & lblflockno.Text & "',"
        sql &= "'" & Val(medlist.SelectedValue) & "',"
        sql &= "'" & medlist.SelectedItem.ToString & "',"
        sql &= "'" & Val(perbqty.Text) & "',"
        sql &= "'" & Val(totqty.Text) & "',"
        sql &= "'" & txtremarks.Text & "',"
        sql &= "'" & Session("user_name") & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Medicine Data Save Successfully"
            fillrid()
            ' fillEggCollectiionNo()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
    End Sub

    Public Sub ClearControls()
        perbqty.Text = ""
        totqty.Text = ""
       

    End Sub
    Public Sub validation_check()
        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Value = "" Then
            valiation = False
        End If

        If totqty.Text = "" Then
            valiation = False
        End If
    End Sub


    Public Sub fillrid()
        Try
            sql = "select * from [daily_medicine] where user_name='" & Session("user_name") & "'  and  shed_loc='" & shedloc.Text & "' and entry_date='" & entrydate.Value & " '"
            gmod.DataSetRet(sql, "tmp_daily_entry_show")
            dailyentry_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
            dailyentry_grid.DataBind()
        Catch ex As Exception

        End Try

        ' fillSum()
    End Sub
End Class
