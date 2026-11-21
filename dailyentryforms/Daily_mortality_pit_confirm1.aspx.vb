Imports System.Data.SqlClient

Partial Class dailyentryforms_Daily_mortality_pit_confirm1
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        'Dim i As Integer
        'Dim ck As Integer

        'For i = 0 To dgConfirm.Items.Count - 1
        '    ck = CInt(CType(dgConfirm.Items(i).FindControl("chk1"), CheckBox).Checked)
        '    If ck <> 0 Then
        '        Dim z As String
        '        z = "delete from tmpfeed where " _
        '            & "entrydate='" & dgConfirm.Items(i).Cells(1).Text & "' and " _
        '            & "shedlocation='" & dgConfirm.Items(i).Cells(2).Text & "' and " _
        '            & "shedno=" & dgConfirm.Items(i).Cells(3).Text & " and " _
        '            & "ispen=" & dgConfirm.Items(i).Cells(4).Text & " and " _
        '            & "typeno=" & dgConfirm.Items(i).Cells(5).Text
        '        Try


        '        Catch ex As Exception

        '        End Try
        '    End If
        'Next
        '' exec [confirm_daily_mortalitypitsale_entries] 'OP1'

        sql = "select * from Bird_transfer where isPosted = 0 and from_shed_loc='" & shedloc.Text & "'"
        gmod.DataSetRet(sql, "isTransferCheck")
        If gmod.ds.Tables("isTransferCheck").Rows.Count > 0 Then
            lblprint.Text = "Please Confirm Bird transfer"
            Exit Sub
        End If

        Try
            ' gmod.SqlExecuteNonQuery("exec confirm_daily_mortalitypitsale_entries '" & Session("usernamemorpit") & "', '" & cnfdate.Text & "'")
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand("exec confirm_daily_mortalitypitsale_entries '" & Session("usernamemorpit") & "', '" & cnfdate.Text & "'", conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Daily_mortalitypitsale_entries Confirmed Sucessfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "confirm_daily_mortalitypitsale_entries", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
        End If
    End Sub


    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10' "
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub

    Public Sub fillgrid()
        Try
            sql = "select  * from [dbo].[tmp_daily_morpit] where isPosted=0 and shed_loc='" & shedloc.Text & "' and entry_date = '" & cnfdate.Text & "'"
            gmod.DataSetRet(sql, "tmp_daily_morpit")
            Session("usernamemorpit") = gmod.ds.Tables("tmp_daily_morpit").Rows(0)("user_name")
            dgConfirm.DataSource = gmod.ds.Tables("tmp_daily_morpit")
            dgConfirm.DataBind()
        Catch ex As Exception
            dgConfirm.DataSource = Nothing
            dgConfirm.DataBind()
            lblprint.Text = ex.Message
        End Try


    End Sub

    Protected Sub dgConfirm_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgConfirm.DeleteCommand
        Dim y As Int64
        Dim z As String

        y = e.Item.ItemIndex

        sql = "delete from [dbo].[tmp_daily_morpit] where [entry_date] ='" & dgConfirm.Items(y).Cells(1).Text & "' and "
        sql &= "[shed_loc]='" & dgConfirm.Items(y).Cells(2).Text & "' and [shed_no]='" & dgConfirm.Items(y).Cells(3).Text & "' and [line_no]='" & dgConfirm.Items(y).Cells(4).Text & "'"

        Try
            ' gmod.SqlExecuteNonQuery("exec confirm_daily_mortalitypitsale_entries '" & Session("usernamemorpit") & "', '" & cnfdate.Text & "'")
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Daily_mortalitypitsale_entries Deleted Sucessfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "delete_daily_mortalitypitsale_entries", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try


        'gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        'fillgrid()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        fillgrid()
    End Sub
End Class
