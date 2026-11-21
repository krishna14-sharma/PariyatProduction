
Partial Class hatchery_hegg_sale_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim d As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            filldata()
        End If
    End Sub

    Sub filldata()
        sql = "select entry_date,hatchery,f_set fsale ,f_rej frate ,mp_set mpsale ,mp_rej mprate,mm_set mmsale  from hatch_setter_eggsale"
        gmod.DataSetRet(sql, "heggsale")

        GridView1.DataSource = gmod.ds.Tables("heggsale")
        GridView1.DataBind()
       
    End Sub
End Class
