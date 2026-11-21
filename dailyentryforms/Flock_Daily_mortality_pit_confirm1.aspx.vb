
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

        gmod.SqlExecuteNonQuery("exec flock_confirm_daily_mortalitypitsale_entries '" & Session("user_name") & "'")
        lblprint.Text = "Data Confirmed Sucessfully"
        fillgrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillgrid()
        End If
    End Sub

    Public Sub fillgrid()
        sql = "select  * from [dbo].[flock_tmp_daily_morpit] where [user_name]='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "tmp_daily_entry")
        dgConfirm.DataSource = gmod.ds.Tables("tmp_daily_entry")
        dgConfirm.DataBind()
    End Sub

    Protected Sub dgConfirm_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgConfirm.DeleteCommand
        Dim y As Int64
        Dim z As String

        y = e.Item.ItemIndex

        sql = "delete from flock_tmp_daily_morpit where [entry_date] ='" & dgConfirm.Items(y).Cells(1).Text & "' and "
        sql &= " and [flock_no]='" & dgConfirm.Items(y).Cells(2).Text & "'"
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        fillgrid()
    End Sub
End Class
