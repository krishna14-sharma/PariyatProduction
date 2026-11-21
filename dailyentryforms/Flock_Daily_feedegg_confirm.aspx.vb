
Partial Class dailyentryforms_Daily_feedegg_confirm
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillgrid()
        End If
    End Sub

    Public Sub fillgrid()
        sql = "select  * from [flock_tmp_daily_entry] where [user_name]='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "tmp_daily_entry")
        dgConfirm.DataSource = gmod.ds.Tables("tmp_daily_entry")
        dgConfirm.DataBind()
    End Sub

   
    Protected Sub dgConfirm_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgConfirm.DeleteCommand
        Dim y As Int64
        Dim z As String

        y = e.Item.ItemIndex

        sql = "delete from flock_tmp_daily_entry where [entry_date] ='" & dgConfirm.Items(y).Cells(1).Text & "' and "
        sql &= "[flock_no]='" & dgConfirm.Items(y).Cells(2).Text & "'"
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        fillgrid()
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
        gmod.SqlExecuteNonQuery("exec flock_confirm_daily_feedegg_entries '" & Session("user_name") & "'")
        lblprint.Text = "Data Confirmed Sucessfully"
        fillgrid()



    End Sub
End Class
