
Partial Class transforms_BirdTransfer
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub mtransferqty_TextChanged(sender As Object, e As EventArgs) Handles mtransferqty.TextChanged

    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'"
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
        'lineno_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub lineno_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lineno.SelectedIndexChanged
        'Try
        '    sql = "select [bird_type_code], [shed_code],[flock_no],[fqty],[mqty] from [dbo].[Shed_Entry_Data] where shed_code='" & shedname.SelectedValue & "' and line_code = '" & lineno.SelectedValue & "' and isAlive=1 and isEmpty=1 "
        '    gmod.DataSetRet(sql, "shed_data")
        '    If gmod.ds.Tables("shed_data").Rows.Count > 0 Then
        '        lblbirdbreed.Text = gmod.ds.Tables("shed_data").Rows(0)("bird_type_code")
        '        lblflockno.Text = gmod.ds.Tables("shed_data").Rows(0)("flock_no")
        '        lblfqty.Text = gmod.ds.Tables("shed_data").Rows(0)("fqty").ToString
        '        lblmqty.Text = gmod.ds.Tables("shed_data").Rows(0)("mqty").ToString
        '    Else
        '        lblflockno.Text = "-"
        '        lblfqty.Text = "-"
        '        lblbirdbreed.Text = "-"
        '        lblmqty.Text = "-"
        '    End If
        'Catch ex As Exception

        'End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            fillLocationto()
            Fillgrid()
            fillBirdtype()
            'shedloc_SelectedIndexChanged(sender, e)
            'shedname_SelectedIndexChanged(sender, e)
            'lineno_SelectedIndexChanged(sender, e)
           
        Else

        End If
    End Sub

    Public Sub fillLocationto()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'"
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
        ' lineno_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub toshedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles toshedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & toshedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "toshed_")
        toshedno.DataSource = gmod.ds.Tables("toshed_")
        toshedno.DataTextField = "shed_name"
        toshedno.DataValueField = "shed_code"
        toshedno.DataBind()
        ' shedname_SelectedIndexChanged(sender, e)
    End Sub

    Protected Sub toline_SelectedIndexChanged(sender As Object, e As EventArgs) Handles toline.SelectedIndexChanged

    End Sub

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Fillgrid()
            Exit Sub
        End If

        sql = "insert Bird_housing([entry_date], [from_shed_loc], [from_shed_no], [from_line_no], [to_shed_loc], [to_shed_no], [to_line_no], [fqty],[mqty], [flock_no], [bird_type_code],user_name,isPosted) values("
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
        sql &= "'" & birdtype.Text & "',"
        sql &= "'" & Session("user_name") & "',"
        sql &= "'0')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Brids Transfer Data Saved Successfully"
            Fillgrid()
            validation_check()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()
    End Sub
    Public Sub Fillgrid()
        sql = "select * from Bird_housing where user_name='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "btdate")
        transfer_grid.DataSource = gmod.ds.Tables("btdate")
        transfer_grid.DataBind()
    End Sub

    Public Sub ClearControls()
        ftransferqty.Text = ""
        mtransferqty.Text = ""
        lblflockno.Text = ""
    End Sub

    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        birdtype.DataSource = gmod.ds.Tables("shed_loc")
        birdtype.DataTextField = "bird_type_code"
        birdtype.DataValueField = "bird_type_code"
        birdtype.DataBind()
    End Sub
    Public Sub validation_check()

        'If lblfqty.Text = "-" Then
        '    lblprint.Text = "No Birds To Transfer"
        '    valiation = False
        'End If

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
