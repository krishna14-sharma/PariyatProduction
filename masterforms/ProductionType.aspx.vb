Partial Class masterforms_ProductionType
    Inherits System.Web.UI.Page

    Dim sql As String
    Dim gmod As New GModule

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then
            'generate form
            fillGrid()
        Else
            'process submitted data
        End If
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        If prdtypecode.Text = "" Then
            Exit Sub
        End If

        If prdtypename.Text = "" Then
            Exit Sub
        End If

        sql = "Insert into [Prd_type_master]([prd_type_code], [prd_name], [created_by], [modified_by], [created_at], [modified_at]) values("
        sql &= "'" & prdtypecode.Text & "',"
        sql &= "'" & prdtypename.Text & "',"
        sql &= "'admin',"
        sql &= "'admin',"
        sql &= "'" & Now.ToShortDateString & "',"
        sql &= "'" & Now.ToShortDateString & "')"

        gmod.SqlExecuteNonQuery(sql)
        fillGrid()

    End Sub

    Public Sub fillGrid()
        sql = "select [prd_type_code], [prd_name] from Prd_type_master"
        gmod.DataSetRet(sql, "PRDTYPEMASTER")
        dgprdtype.DataSource = gmod.ds.Tables("PRDTYPEMASTER")
        dgprdtype.DataBind()

    End Sub


End Class
