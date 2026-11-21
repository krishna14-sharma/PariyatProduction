
Partial Class transforms_BirdTransfer
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True


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
                lblbreed1.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
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
            lblfqty.Text = "0"
            lblbirdbreed.Text = "-"
            lblmqty.Text = "0"
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillLocationto()
            Fillgrid()
            shedloc_SelectedIndexChanged(sender, e)
            shedname_SelectedIndexChanged(sender, e)
            lineno_SelectedIndexChanged(sender, e)
            toshedloc_SelectedIndexChanged(sender, e)
            toline_SelectedIndexChanged(sender, e)
        Else

        End If
    End Sub

    Public Sub fillLocationto()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'  and location_code = '" & Session("area_code") & "'"
        gmod.DataSetRet(sql, "toshed_loc")
        toshedloc.DataSource = gmod.ds.Tables("toshed_loc")
        toshedloc.DataTextField = "location_name"
        toshedloc.DataValueField = "location_code"
        toshedloc.DataBind()
    End Sub

    Protected Sub toshedno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles toshedno.SelectedIndexChanged
        sql = "select * from [dbo].[shed_line_master] where shed_code = '" & toshedno.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_line_master_to")
        toline.DataSource = gmod.ds.Tables("shed_line_master_to")
        toline.DataTextField = "line_no"
        toline.DataValueField = "line_code"
        toline.DataBind()
        toline_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub toshedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles toshedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & toshedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "toshed_")
        toshedno.DataSource = gmod.ds.Tables("toshed_")
        toshedno.DataTextField = "shed_name"
        toshedno.DataValueField = "shed_code"
        toshedno.DataBind()
        ' shedname_SelectedIndexChanged(sender, e)
        toshedno_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub toline_SelectedIndexChanged(sender As Object, e As EventArgs) Handles toline.SelectedIndexChanged
        Try

            sql = "select [bird_type_code], [shed_code],[flock_no] from [dbo].[Shed_Entry_Data] where shed_code='" & toshedno.SelectedValue & "' and line_code = '" & toline.SelectedValue & "' and isAlive=1 and isEmpty=1 "
            gmod.DataSetRet(sql, "shed_data")

            sql = "select Sum(isnull([fqty],0)) fqty ,Sum(isnull([mqty],0))  mqty from [dbo].[Shed_Entry_Data] where shed_code='" & toshedno.SelectedValue & "' and line_code = '" & toline.SelectedValue & "' and isAlive=1 and isEmpty=1 and flock_no = '" & gmod.ds.Tables("shed_data").Rows(0)("flock_no").ToString & "' "
            gmod.DataSetRet(sql, "shed_qtydata")
            If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
                'lblbreed1.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
                lblFlockNoTo.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
                'lblfqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("fqty").ToString
                'lblmqty.Text = gmod.ds.Tables("shed_qtydata").Rows(0)("mqty").ToString

                'sql = "	select DATEDIFF(d,hatch_date,GETDATE())/7 Weeks , (DATEDIFF(d,hatch_date,GETDATE()) % 7)+1  Days   from flock_master  where flock_no='" & lblflockno.Text & "'"
                'gmod.DataSetRet(sql, "shed_data_bird_age")
                'lblbirdbreed.Text = gmod.ds.Tables("shed_data_bird_age").Rows(0)("Weeks").ToString + "-" + gmod.ds.Tables("shed_data_bird_age").Rows(0)("Days").ToString

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
            Fillgrid()
            Exit Sub
        End If


        If Val(lblfqty.Text) >= Val(ftransferqty.Text) Then

        Else
            lblprint.Text = "Please Take Proper Female Quantity"
            Exit Sub
        End If

        If Val(lblmqty.Text) >= Val(mtransferqty.Text) Then

        Else
            lblprint.Text = "Please Take Proper Male Quantity"
            Exit Sub
        End If


        If lblflockno.Text = lblFlockNoTo.Text Then

        Else
            lblprint.Text = "Mismatch Flock Number (Plese Contact Adiminstrator)"
            Exit Sub
        End If
        sql = "insert [Bird_transfer]([entry_date], [from_shed_loc], [from_shed_no], [from_line_no], [to_shed_loc], [to_shed_no], [to_line_no], [fqty],[mqty], [flock_no], [bird_type_code],user_name,[bird_type_code1]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & shedloc.Text & "',"
        sql &= "'" & shedname.Text & "',"
        sql &= "'" & lineno.Text & "',"
        sql &= "'" & toshedloc.Text & "',"
        sql &= "'" & toshedno.Text & "',"
        sql &= "'" & toline.Text & "',"
        sql &= "'" & ftransferqty.Text & "',"
        sql &= "'" & mtransferqty.Text & "',"
        sql &= "'" & lblflockno.Text & "',"
        sql &= "'" & lblbirdbreed.Text & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'" & lblbreed1.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            Fillgrid()
            lblprint.Text = "Brids Transfer Data Saved Successfully"
            validation_check()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
    End Sub
    Public Sub Fillgrid()
        sql = "select * from [Bird_transfer] where user_name='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "btdate")
        transfer_grid.DataSource = gmod.ds.Tables("btdate")
        transfer_grid.DataBind()
    End Sub

    Public Sub ClearControls()
        ftransferqty.Text = ""
        mtransferqty.Text = ""

    End Sub
    Public Sub validation_check()

        If lblfqty.Text = "-" Then
            lblprint.Text = "No Birds To Transfer"
            valiation = False
        End If

        If Session("user_name") = "" Then
            valiation = False
        End If

        If entrydate.Value = "" Then
            valiation = False
        End If

        If ftransferqty.Text = "" Then
            valiation = False
        End If


        If mtransferqty.Text = "" Then
            valiation = False
        End If
       
    End Sub
End Class
