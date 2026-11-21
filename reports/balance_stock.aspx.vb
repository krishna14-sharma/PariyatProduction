
Partial Class reports_balance_stock
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True


   

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillGrid()
        End If
    End Sub


    Public Sub fillGrid()
        sql = " select * from [dbo].[mat_rem_qty_area] where loc_code ='" & Session("area_code") & "'"
       
        gmod.DataSetRet(sql, "bal_stock")

        bal_stock.DataSource = gmod.ds.Tables("bal_stock")
        bal_stock.DataBind()

    End Sub
End Class
