
Partial Class pit_pit_mis_receving
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillCombo()
            fillGrid()
        End If
    End Sub

    Protected Sub fillCombo()
        sql = "select * from [dbo].[pit_mis_itemname] where Item_Type='" & cmbPrdtype.Text & "' and Breed_Type='" & cmbBreedType.Text & "'"
        gmod.DataSetRet(sql, "pit_mis_itemname1")
        cmbItemName.DataSource = gmod.ds.Tables("pit_mis_itemname1")
        cmbItemName.DataTextField = "Item_Name"
        cmbItemName.DataValueField = "Item_Name"
        cmbItemName.DataBind()
    End Sub
    Protected Sub item_btnSave_Click(sender As Object, e As EventArgs) Handles item_btnSave.Click
        sql = "insert into [dbo].[pit_mis_trans](transdate,[item_type], [breed_type], [item_name], [r], [s], [d], [kg], [neccrate], [phxrate], [isOpening]) values("
        sql &= "'" & entrydate.Value & "',"
        sql &= "'" & cmbPrdtype.Text & "',"
        sql &= "'" & cmbBreedType.Text & "',"
        sql &= "'" & cmbItemName.Text & "',"
        sql &= "'" & Val(txtQtyNumber.Text) & "',"
        sql &= "'0',"
        sql &= "'0',"
        sql &= "'" & Val(txtKg.Text) & "',"
        sql &= "'0',"
        sql &= "'0',"
        sql &= "'0')"

        lblmsg.Text = gmod.SqlExecuteNonQuery(sql)
        fillGrid()
        txtQtyNumber.Text = ""
        txtKg.Text = ""
    End Sub

    Protected Sub fillGrid()
        sql = "select CONVERT (varchar(10), transdate, 103) AS OpeningDate, [item_type], [breed_type], [item_name], [r] Qty, [kg], [isOpening] from [dbo].[pit_mis_trans] where [isOpening]=0"
        gmod.DataSetRet(sql, "pit_mis_open")

        dgItemOpening.DataSource = gmod.ds.Tables("pit_mis_open")
        dgItemOpening.DataBind()
    End Sub

    Protected Sub cmbBreedType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbBreedType.SelectedIndexChanged
        fillCombo()
    End Sub
End Class
