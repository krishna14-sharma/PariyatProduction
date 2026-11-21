
Partial Class masterforms_ExpenseHeadMaste
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim GModule As New GModule
    Dim valiation As Boolean = True
    Protected Sub btnSaveExpmaster_Click(sender As Object, e As EventArgs) Handles btnSaveExpmaster.Click
        sql = "insert into [dbo].[ExpMaster]([Exp_type], [Exp_head]) values ( "
        sql &= "'" & exptype.Text & "',"
        sql &= "'" & exphead.Text & "')"
        qyeryresult = GModule.SqlExecuteNonQuery(sql)
        AppUi.ShowMessage(Me, qyeryresult)

    End Sub
End Class
