
Partial Class pit_pit_mis_itemname
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String
  
    Protected Sub item_btnSave_Click(sender As Object, e As EventArgs) Handles item_btnSave.Click
        sql = "insert into [pit_mis_itemname]([Item_Name], [Breed_Type], [Item_Type]) values( "
        sql &= "'" & txtitemname.Text & "',"
        sql &= "'" & cmbBreedType.Text & "',"
        sql &= "'" & cmbPrdtype.Text & "')"
        qyeryresult = gmod.SqlExecuteNonQuery(sql)

        fillgrid()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillgrid()
        End If
    End Sub

    Public Sub fillgrid()
        sql = "select * from  pit_mis_itemname"
        gmod.DataSetRet(sql, "pit_mis_itemname")

        dgItenName.DataSource = gmod.ds.Tables("pit_mis_itemname")
        dgItenName.DataBind()
    End Sub
End Class
