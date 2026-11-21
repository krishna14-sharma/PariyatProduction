Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Globalization
Partial Class dailyentryforms_FeedReceived
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True
    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        If Val(tbNoOfBags.Text) = 0 Then
            lblMsg.CssClass = "error"
            lblMsg.Text = "Please enter received bags"
            Exit Sub
        End If

        If Val(tbQry.Text) = 0 Then
            lblMsg.CssClass = "error"
            lblMsg.Text = "Please enter received quantity"
            Exit Sub
        End If

        If Val(tbQry.Text) = 0 Then
            lblMsg.CssClass = "error"
            lblMsg.Text = "Please show dispatch quantity"
            Exit Sub
        End If

        If Val(tbQtyDispatch.Text) = 0 Then
            lblMsg.CssClass = "error"
            lblMsg.Text = "Please show dispatch bags"
            Exit Sub
        End If

        Try
            Dim pav() As String
            pav = ddlFeedName.Text.ToString.Split("#")

            sql = "INSERT INTO [dbo].[FeedRecData] "
            sql &= " ([FeedRecDate],[FeedName],[NoOfBags],[Qry],[BagsDispatch],[QtyDispatch],[Users],[entrtdate],[VehNo]) "
            sql &= "   VALUES (@FeedRecDate,@FeedName,@NoOfBags,@Qry,@BagsDispatch,@QtyDispatch,@Users,@entrtdate,@VehNo)"

            Using con As New SqlConnection(gmod.Connstr)
                Using cmd As New SqlCommand(sql, con)

                    cmd.Parameters.Add("@FeedRecDate", SqlDbType.Date).Value = CDate(tbFeedRecDate.Text)
                    cmd.Parameters.Add("@FeedName", SqlDbType.VarChar, 150).Value = pav(0)
                    cmd.Parameters.Add("@NoOfBags", SqlDbType.Int).Value = tbNoOfBags.Text
                    cmd.Parameters.Add("@Qry", SqlDbType.Decimal).Value = Val(tbQry.Text)
                    cmd.Parameters.Add("@BagsDispatch", SqlDbType.Int).Value = Val(tbBagsDispatch.Text)
                    cmd.Parameters.Add("@QtyDispatch", SqlDbType.Decimal).Value = Val(tbQtyDispatch.Text)
                    cmd.Parameters.Add("@Users", SqlDbType.VarChar, 50).Value = Session("area_code")
                    cmd.Parameters.Add("@entrtdate", SqlDbType.Date).Value = Now.ToString
                    'VehNo
                    cmd.Parameters.Add("@VehNo", SqlDbType.VarChar).Value = pav(1)
                    con.Open()
                    Dim rows = cmd.ExecuteNonQuery()
                    lblMsg.CssClass = If(rows > 0, "success", "error")
                    lblMsg.Text = If(rows > 0, "Record inserted successfully.", "Insert did not affect any rows.")
                End Using
            End Using

            ClearForm()
            BindGrid()
            Exit Sub
        Catch ex As Exception
            lblMsg.CssClass = "error"
            lblMsg.Text = "Insert failed: " & ex.Message
        End Try
    End Sub

    Private Sub ClearForm()
        tbFeedRecDate.Text = ""
        tbNoOfBags.Text = ""
        tbQry.Text = ""
        tbBagsDispatch.Text = ""
        tbQtyDispatch.Text = ""

    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Private Sub BindGrid(Optional ByVal sortExpression As String = "id DESC")
        Dim dt As New DataTable()
        Dim sql As String = "SELECT [id],[FeedRecDate],[FeedName],[NoOfBags],[Qry],"
        sql &= "  [BagsDispatch],[QtyDispatch],[Users],[entrtdate] FROM [dbo].[FeedRecData] "
        sql &= " where Users = '" & Session("area_code") & "' and FeedRecDate='" & tbFeedRecDate.Text & "'"
        If Not String.IsNullOrEmpty(sortExpression) Then
            sql &= " ORDER BY " & sortExpression
        End If
        Using con As New SqlConnection(gmod.Connstr)
            Using cmd As New SqlCommand(sql, con)
                Using da As New SqlDataAdapter(cmd)
                    da.Fill(dt)
                End Using
            End Using
        End Using
        GridView1.DataSource = dt
        GridView1.DataBind()
    End Sub
    ' GRID EVENTS
    Protected Sub GridView1_PageIndexChanging(sender As Object, e As System.Web.UI.WebControls.GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As System.Web.UI.WebControls.GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        BindGrid()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As System.Web.UI.WebControls.GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As System.Web.UI.WebControls.GridViewUpdateEventArgs)
       
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As System.Web.UI.WebControls.GridViewDeleteEventArgs)
       
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Try
            sql = "select  ProductName + '#' + BillNo  as ProductName from OtherSaleData where billdate = '" & tbFeedRecDate.Text & "' and ToFarm='" & Session("area_code") & "'"
            GlobalModule1.DataSetRet(sql, "salerep")

            ' tbQtyDispatch.Text = Val(GlobalModule1.ds.Tables("salerep")(0)("OutQty")).ToString
            ddlFeedName.DataSource = GlobalModule1.ds.Tables("salerep")
            ddlFeedName.DataTextField = "ProductName"
            ddlFeedName.DataBind()
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
       
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim pav() As String

        pav = ddlFeedName.Text.ToString.Split("#")
        Try
            sql = "select Vou_type, Vou_no, AccCode, AccName, Station, ProductName, OutQty, Rate, Amount, OutQtyNos, BillNo, "
            sql &= " BillDate, InQty, InQtyNos, o.Cmp_id, Session, id, mrktrate, authr,  Prdunit, Packing, Insurance, Discount, "
            sql &= " o.ToFarm CrHead from OtherSaleData o left join ACCHEAD  a "
            sql &= " on o.acccode=a.account_code where billdate = '" & tbFeedRecDate.Text & "' and ToFarm='" & Session("area_code") & "' and ProductName='" & pav(0) & "'  and BillNo='" & pav(1) & "'"
            GlobalModule1.DataSetRet(sql, "salerepdata")

            tbQtyDispatch.Text = Val(GlobalModule1.ds.Tables("salerepdata")(0)("OutQty")).ToString
            tbBagsDispatch.Text = Val(GlobalModule1.ds.Tables("salerepdata")(0)("OutQtyNos")).ToString
        Catch ex As Exception
            lblMsg.Text = ex.Message
        End Try
        
    End Sub
End Class
