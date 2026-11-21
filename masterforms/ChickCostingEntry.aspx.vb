
Partial Class masterforms_ChickCostingEntry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
            FillMonth()
            FillYear()
            FillUnits()
            BindGrid()
        Else

        End If
    End Sub
    Private Sub FillMonth()

        ddlMonth.Items.Clear()

        ddlMonth.Items.Add(New ListItem("Select Month", ""))

        ddlMonth.Items.Add(New ListItem("JAN", "1"))
        ddlMonth.Items.Add(New ListItem("FEB", "2"))
        ddlMonth.Items.Add(New ListItem("MAR", "3"))
        ddlMonth.Items.Add(New ListItem("APR", "4"))
        ddlMonth.Items.Add(New ListItem("MAY", "5"))
        ddlMonth.Items.Add(New ListItem("JUN", "6"))
        ddlMonth.Items.Add(New ListItem("JUL", "7"))
        ddlMonth.Items.Add(New ListItem("AUG", "8"))
        ddlMonth.Items.Add(New ListItem("SEP", "9"))
        ddlMonth.Items.Add(New ListItem("OCT", "10"))
        ddlMonth.Items.Add(New ListItem("NOV", "11"))
        ddlMonth.Items.Add(New ListItem("DEC", "12"))

    End Sub

    Private Sub FillYear()
        ddlYear.Items.Clear()
        ddlYear.Items.Add(New ListItem("Select Year", ""))

        For i As Integer = 1990 To 2090

            ddlYear.Items.Add(New ListItem(i.ToString(), i.ToString()))

        Next

    End Sub

    Private Sub FillUnits()

        ddlUnit.Items.Clear()

        ddlUnit.Items.Add(New ListItem("Select Unit", ""))

        ' Weight
        ddlUnit.Items.Add(New ListItem("KG", "KG"))
        ddlUnit.Items.Add(New ListItem("GRAM", "GRAM"))
        ddlUnit.Items.Add(New ListItem("TON", "TON"))
        ddlUnit.Items.Add(New ListItem("QUINTAL", "QUINTAL"))

        ' Liquid
        ddlUnit.Items.Add(New ListItem("LITRE", "LITRE"))
        ddlUnit.Items.Add(New ListItem("ML", "ML"))

        ' Length
        ddlUnit.Items.Add(New ListItem("METER", "METER"))
        ddlUnit.Items.Add(New ListItem("FEET", "FEET"))
        ddlUnit.Items.Add(New ListItem("INCH", "INCH"))

        ' Quantity
        ddlUnit.Items.Add(New ListItem("PIECE", "PIECE"))
        ddlUnit.Items.Add(New ListItem("NOS", "NOS"))
        ddlUnit.Items.Add(New ListItem("BOX", "BOX"))
        ddlUnit.Items.Add(New ListItem("BAG", "BAG"))
        ddlUnit.Items.Add(New ListItem("PACKET", "PACKET"))

        ' Area
        ddlUnit.Items.Add(New ListItem("SQFT", "SQFT"))
        ddlUnit.Items.Add(New ListItem("SQM", "SQM"))
        ddlUnit.Items.Add(New ListItem("ACRE", "ACRE"))

        ' Poultry / Feed
        ddlUnit.Items.Add(New ListItem("BIRD", "BIRD"))
        ddlUnit.Items.Add(New ListItem("CHICK", "CHICK"))
        ddlUnit.Items.Add(New ListItem("TRAY", "TRAY"))

    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10' "
        gmod.DataSetRet(sql, "shed_loc")
        ddlAreaName.DataSource = gmod.ds.Tables("shed_loc")
        ddlAreaName.DataTextField = "location_code"
        ddlAreaName.DataValueField = "location_code"
        ddlAreaName.DataBind()
    End Sub

    Protected Sub ddlAreaName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlAreaName.SelectedIndexChanged
        sql = "select distinct left([flock_no],3) flock_no from [dbo].[Shed_Entry_Data] where shed_loc ='" & ddlAreaName.SelectedValue & "'"
        gmod.DataSetRet(sql, "flocknoforcosting")
        ddlFlockNo.DataSource = gmod.ds.Tables("flocknoforcosting")
        ddlFlockNo.DataTextField = "flock_no"
        ddlFlockNo.DataValueField = "flock_no"
        ddlFlockNo.DataBind()
    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim qry As String = ""
        Try
            qry = qry & " INSERT INTO ChickCostingEntry "
            qry = qry & " (SystemType,AreaName,FlockNo,ExpenseType,ExpenseHead,Qty,Unit,Rate,MonthNo,MonthName,YearNo,CreatedBy,TotalAmount) "
        qry = qry & " VALUES ( "
        qry = qry & " '" & ddlSystemType.SelectedValue & "', "
        qry = qry & " '" & ddlAreaName.SelectedItem.Text & "', "
        qry = qry & " '" & ddlFlockNo.SelectedItem.Text & "', "
        qry = qry & " '" & ddlExpenseType.SelectedItem.Text & "', "
        qry = qry & " '" & ddlExpenseHead.SelectedItem.Text & "', "
            qry = qry & " '" & Val(txtQty.Text) & "', "
        qry = qry & " '" & ddlUnit.SelectedValue & "', "
            qry = qry & " '" & Val(txtRate.Text) & "', "
        qry = qry & " '" & ddlMonth.SelectedValue & "', "
        qry = qry & " '" & ddlMonth.SelectedItem.Text & "', "
        qry = qry & " '" & ddlYear.SelectedValue & "', "
            qry = qry & " '" & Session("UserName") & "', "
            qry = qry & " '" & Val(txtAmount.Text) & "' "
        qry = qry & " ) "

        qyeryresult = gmod.SqlExecuteNonQuery(qry)
        AppUi.ShowMessage(Me, qyeryresult)
            ClearControls()
            BindGrid()
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
    End Sub

    Protected Sub ddlExpenseType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlExpenseType.SelectedIndexChanged
        sql = "select * from [ExpMaster] where Exp_type='" & ddlExpenseType.Text & "'"
        gmod.DataSetRet(sql, "expheads")
        ddlExpenseHead.DataSource = gmod.ds.Tables("expheads")
        ddlExpenseHead.DataTextField = "Exp_head"
        ddlExpenseHead.DataValueField = "Exp_head"
        ddlExpenseHead.DataBind()
    End Sub

    Protected Sub ClearControls()

        ddlSystemType.SelectedIndex = 0
        ddlAreaName.SelectedIndex = 0
        ddlFlockNo.SelectedIndex = 0

        ddlExpenseType.SelectedIndex = 0
        ddlExpenseHead.SelectedIndex = 0

        ddlUnit.SelectedIndex = 0
        ddlMonth.SelectedIndex = 0
        ddlYear.SelectedIndex = 0

        txtQty.Text = ""
        txtRate.Text = ""
        txtAmount.Text = ""

        txtQty.Focus()

    End Sub

    Private Sub BindGrid()

        sql = "select * from ChickCostingEntry order by Id desc"
        gmod.DataSetRet(sql, "ChickCostingData")
        gvCosting.DataSource = gmod.ds.Tables("ChickCostingData")
        gvCosting.DataBind()

    End Sub

    Protected Sub gvCosting_RowDeleting(sender As Object,
                                    e As GridViewDeleteEventArgs)
        Dim Id As String = gvCosting.DataKeys(e.RowIndex).Value.ToString()
        Dim qry As String = ""
        qry = qry & " delete from ChickCostingEntry "
        qry = qry & " where Id = '" & Id & "' "
        gmod.SqlExecuteNonQuery(qry)
        BindGrid()
    End Sub

    Protected Sub gvCosting_RowEditing(sender As Object,
                                   e As GridViewEditEventArgs)

        Dim Id As String = gvCosting.DataKeys(e.NewEditIndex).Value.ToString()

        Response.Redirect("ChickCostingEntry.aspx?Id=" & Id)

    End Sub
End Class
