
Partial Class dailyentryforms_Daily_MedicineIssueToShed
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub lftqty_Click(sender As Object, e As EventArgs) Handles lftqty.Click

        Dim item() As String
        item = itemnane.Text.Split("#")
        Try
            sql = "select rem_qty from medicin_rem_qty_area where item_code = '" & item(1) & "' and farm_code = '" & Session("area_code") & "'"
            gmod.DataSetRet(sql, "rem_mat_qty")
            remqty.Text = gmod.ds.Tables("rem_mat_qty").Rows(0)(0)
        Catch ex As Exception
            remqty.Text = "0"
        End Try

        Try
            sql = "select Unit from [dbo].[Medicine_received] where item_code = '" & item(1) & "' and farm_code = '" & Session("area_code") & "'"
            gmod.DataSetRet(sql, "mat_unit")
            lblunit.Text = gmod.ds.Tables("mat_unit").Rows(0)(0)
        Catch ex As Exception

        End Try

        fillGrid()

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillLocation()
            shedloc_SelectedIndexChanged(sender, e)
            fillItemName()
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'  and location_code = '" & Session("area_code") & "'"
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub

    Public Sub fillItemName()
        sql = "select   distinct Item_Name + '#' + Item_Code as itemanme,Item_Code  from [Medicine_Received] order by itemanme"
        gmod.DataSetRet(sql, "itemname")

        itemnane.DataSource = gmod.ds.Tables("itemname")
        itemnane.DataTextField = "itemanme"
        itemnane.DataBind()
        'itemcode.DataSource = gmod.ds.Tables("itemname")
        'itemcode.DataTextField = "Item_Code"
        'itemcode.DataValueField = "Item_Category"
        'itemcode.DataBind()
    End Sub

    Protected Sub shedloc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles shedloc.SelectedIndexChanged
        sql = "select shed_code,shed_name from [dbo].[shed_master] where loc_code='" & shedloc.SelectedValue & "'"
        gmod.DataSetRet(sql, "shed_")
        shedno.DataSource = gmod.ds.Tables("shed_")
        shedno.DataTextField = "shed_name"
        shedno.DataValueField = "shed_code"
        shedno.DataBind()
    End Sub

    Protected Sub Issue_Click(sender As Object, e As EventArgs) Handles Issue.Click
        Dim item() As String
        item = itemnane.Text.Split("#")
        If Val(issueqty.Text) <= Val(remqty.Text) Then
            sql = "insert into [dbo].[Medicine_IssueToShed]([Rec_No], [Rec_Date], [Rec_Person], [Book_No], [Item_Name],"
            sql &= " [Item_Code], [Item_Category], [Unit], [Issue_Qty], [Dept_Name], [Receipt_No], [loc_code], [shed_no]) values("
            sql &= "'-',"
            sql &= "'" & issue_date.Value & "',"
            sql &= "'" & recperson.Text & "',"
            sql &= "'-',"
            sql &= "'" & item(0) & "',"
            sql &= "'" & item(1) & "',"
            sql &= "'" & lblitemCategory.Text & "',"
            sql &= "'" & lblunit.Text & "',"
            sql &= "'" & Val(issueqty.Text) & "',"
            sql &= "'" & lblDept.Text & "',"
            sql &= "'-',"
            sql &= "'" & shedloc.Text & "',"
            sql &= "'" & shedno.Text & "')"
            gmod.SqlExecuteNonQuery(sql)
        Else
            AppUi.ShowError(Me, "Please Enter Lesser Quantity...")

            inf.Text = "Please Enter Smaller Quantity..."
        End If
    End Sub

    Public Sub fillGrid()
        sql = " select "
        sql &= " [Rec_Date] IssueDate, [Rec_Person], [Item_Name], [Item_Code], [Item_Category], [Unit], [Issue_Qty], [Dept_Name], [loc_code], [shed_no] "
        sql &= " from [dbo].[Medicine_IssueToShed] where loc_code = '" & Session("area_code") & "' and Rec_Date='" & issue_date.Value & "'"
        gmod.DataSetRet(sql, "issue_data")
        issuelist.DataSource = gmod.ds.Tables("issue_data")
        issuelist.DataBind()
    End Sub
End Class
