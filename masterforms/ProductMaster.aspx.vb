
Partial Class masterforms_ProductMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim isactivebit As Integer = 0

    Protected Sub btnsaveprd_Click(sender As Object, e As EventArgs) Handles btnsaveprd.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If
        If isactive.Checked = True Then
            isactivebit = 1
        Else
            isactivebit = 0
        End If

        sql = "insert into [product_master]([Prd_code], [Prd_name], [Prd_category], [Prd_Desc], [Generic_name], [Compositin], [Rate], [Location_code], [isactive]) values("
        sql &= "'" & prdcode.Text & "',"
        sql &= "'" & prdname.Text & "',"
        sql &= "'" & prdcat.SelectedValue & "',"
        sql &= "'" & prddesc.Text & "',"

        sql &= "'" & genericname.Text & "',"
        sql &= "'" & composition.Text & "',"

        sql &= "'" & rate.Text & "',"
        sql &= "'" & location.SelectedValue & "',"
        sql &= "'" & isactivebit & "')"


        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Product Data Saved Successfully"
            fillGrid()
            ClearControls()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub
    Public Sub validation_check()
        If prdcode.Text = "" Then
            valiation = False
        End If

        If prdname.Text = "" Then
            valiation = False
        End If

        If prddesc.Text = "" Then
            valiation = False
        End If

        If rate.Text = "" Then
            valiation = False
        End If
    End Sub

    Public Sub ClearControls()
        prdcode.Text = ""
        prdname.Text = ""
        prddesc.Text = ""
        rate.Text = ""
        composition.Text = ""
        genericname.Text = ""
    End Sub
    Public Sub fillGrid()
        sql = "select * from product_master"
        gmod.DataSetRet(sql, "bird_stage_master")
        prd_grid.DataSource = gmod.ds.Tables("bird_stage_master")
        prd_grid.DataBind()
    End Sub

    Public Sub fillProducrCategory()
        sql = "select [prd_cat_name] from product_cat"
        gmod.DataSetRet(sql, "product_cat")
        prdcat.DataSource = gmod.ds.Tables("product_cat")
        prdcat.DataTextField = "prd_cat_name"
        prdcat.DataValueField = "prd_cat_name"
        prdcat.DataBind()
    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]"
        gmod.DataSetRet(sql, "shed_loc")
        location.DataSource = gmod.ds.Tables("shed_loc")
        location.DataTextField = "location_name"
        location.DataValueField = "location_code"
        location.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillProducrCategory()
            fillLocation()
            fillGrid()
        Else
        End If
    End Sub
End Class
