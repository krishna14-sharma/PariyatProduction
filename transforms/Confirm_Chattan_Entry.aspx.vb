
Partial Class transforms_Confirm_Chattan_Entry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged

    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10' "
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub

    Protected Sub btnShoe_Click(sender As Object, e As EventArgs) Handles btnShoe.Click
        fillgrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillLocation()
            fillgrid()
        End If
    End Sub

    Public Sub fillgrid()
        sql = "select * from [dbo].[daily_sepration] where shed_loc='" & shedloc.Text & "' and isPosted=0"
        gmod.DataSetRet(sql, "tmp_daily_entry_show")
        chattan_grid.DataSource = gmod.ds.Tables("tmp_daily_entry_show")
        chattan_grid.DataBind()
    End Sub

    Protected Sub chattan_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles chattan_grid.DeleteCommand
        Dim y As Int64
        Dim z As String = ""

        y = e.Item.ItemIndex
        sql = "delete from daily_sepration where [id] ='" & chattan_grid.Items(y).Cells(13).Text & "' and isPosted=0"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Chattan Data Deleted Successfully"
        End If
    End Sub

    Protected Sub btconfirm_Click(sender As Object, e As EventArgs) Handles btconfirm.Click
        sql = "update daily_sepration set isPosted =1 where shed_loc='" & shedloc.Text & "' and isPosted=0"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Confirm Successfully"
            fillgrid()
        End If
    End Sub
End Class
