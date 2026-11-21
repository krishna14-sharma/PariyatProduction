
Partial Class transforms_Confirm_PMEntry
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillLocation()
            fillrid()
        End If
    End Sub
    Public Sub fillrid()
        Try
            sql = "select * from Pm_data where isPosted=0 and shed_loc = '" & shedloc.Text & "'"
            gmod.DataSetRet(sql, "Pm_data")
            Session("usernamemorpit") = gmod.ds.Tables("Pm_data").Rows(0)("user_name")
            dailyentry_grid.DataSource = gmod.ds.Tables("Pm_data")
            dailyentry_grid.DataBind()
            lblprint.Text = ""
        Catch ex As Exception
            dailyentry_grid.DataSource = Nothing
            dailyentry_grid.DataBind()
            lblprint.Text = ex.Message
            ' Response.Write(ex.Message)
        End Try

    End Sub

    Protected Sub dailyentry_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dailyentry_grid.DeleteCommand
        Dim y As Int64
        Dim z As String

        y = e.Item.ItemIndex

        sql = "delete from Pm_data where [entry_date] ='" & dailyentry_grid.Items(y).Cells(1).Text & "' and "
        sql &= "[shed_loc]='" & dailyentry_grid.Items(y).Cells(2).Text & "' and [shed_name]='" & dailyentry_grid.Items(y).Cells(3).Text & "' and [line_pen]='" & dailyentry_grid.Items(y).Cells(4).Text & "'  and disease_name='" & dailyentry_grid.Items(y).Cells(6).Text & "'"
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        fillrid()
    End Sub

    Protected Sub btconfirm_Click(sender As Object, e As EventArgs) Handles btconfirm.Click
        gmod.SqlExecuteNonQuery("update Pm_data set isPosted=1  where isPosted=0 and shed_loc = '" & shedloc.Text & "' and user_name = '" & Session("usernamemorpit") & "'")
        fillrid()
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        fillrid()
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10' "
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub
End Class
