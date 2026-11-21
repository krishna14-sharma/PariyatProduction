
Partial Class transforms_Confrim_BirdTransfer
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Fillgrid()
        End If
    End Sub

    Public Sub Fillgrid()
        sql = "select * from Bird_Housing where user_name='" & Session("user_name") & "' and isPosted=0"
        gmod.DataSetRet(sql, "btdate")
        transfer_grid.DataSource = gmod.ds.Tables("btdate")
        transfer_grid.DataBind()
    End Sub

    Protected Sub transfer_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles transfer_grid.DeleteCommand
        Dim y As Int64
        Dim z As String
        y = e.Item.ItemIndex
        sql = "delete from [dbo].[Bird_Housing] where [entry_date] ='" & transfer_grid.Items(y).Cells(1).Text & "' and "
        sql &= "[from_shed_loc]='" & transfer_grid.Items(y).Cells(2).Text & "' and [from_shed_no]='" & transfer_grid.Items(y).Cells(3).Text & "' and [from_line_no]='" & transfer_grid.Items(y).Cells(4).Text & "'"
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        Fillgrid()
    End Sub

    Protected Sub btconfirm_Click(sender As Object, e As EventArgs) Handles btconfirm.Click
        gmod.SqlExecuteNonQuery("exec confirm_birdtransfer '" & Session("user_name") & "'")
        lblprint.Text = "Data Confirmed Sucessfully"
        Fillgrid()
    End Sub
End Class
