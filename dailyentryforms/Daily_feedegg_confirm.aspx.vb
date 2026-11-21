Imports System.Data.SqlClient

Partial Class dailyentryforms_Daily_feedegg_confirm
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim user_name As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillLocation()
        End If
    End Sub
    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10'"
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub

    Public Sub fillgrid()
        Try
            sql = "select  * from [dbo].[tmp_daily_entry] where isPosted=0 and shed_loc='" & shedloc.Text & "' and entry_date ='" & cnfdate.Text & "'"
            gmod.DataSetRet(sql, "tmp_daily_entry")
            Session("username") = gmod.ds.Tables("tmp_daily_entry").Rows(0)("user_name")
            dgConfirm.DataSource = gmod.ds.Tables("tmp_daily_entry")
            dgConfirm.DataBind()
        Catch ex As Exception
            dgConfirm.DataSource = Nothing
            dgConfirm.DataBind()
            lblprint.Text = ex.Message
        End Try

    End Sub
    Protected Sub dgConfirm_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles dgConfirm.DeleteCommand
        Dim y As Int64
        Dim z As String = ""
        y = e.Item.ItemIndex
        sql = "delete from [dbo].[tmp_daily_entry] where [entry_date] ='" & dgConfirm.Items(y).Cells(1).Text & "' and "
        sql &= "[shed_loc]='" & dgConfirm.Items(y).Cells(2).Text & "' and [shed_no]='" & dgConfirm.Items(y).Cells(3).Text & "' and [line_no]='" & dgConfirm.Items(y).Cells(4).Text & "'"
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Delete Feed Egg  Sucessfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "delted_feed_egg_entries", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try
        gmod.SqlExecuteNonQuery(sql)
        ' Response.Write(y)   ' debug output removed (Response.Write breaks AJAX postbacks)
        fillgrid()
    End Sub
    Protected Sub btnConfirm_Click(sender As Object, e As EventArgs) Handles btnConfirm.Click
     If Session("user_name") Is Nothing OrElse
       String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then
            Exit Sub
        End If

        Try
            'gmod.SqlExecuteNonQuery("exec confirm_daily_feedegg_entries '" & Session("username") & "','" & cnfdate.Text & "'")
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand("exec confirm_daily_feedegg_entries '" & Session("username") & "','" & cnfdate.Text & "'", conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Daily Feed Egg Confirmed Sucessfully"
                    fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "confirm_feed_egg_entries", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try

    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        fillgrid()
    End Sub
End Class
