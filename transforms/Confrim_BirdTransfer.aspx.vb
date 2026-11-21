Imports System.Data.SqlClient

Partial Class transforms_Confrim_BirdTransfer
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillLocation()
            Fillgrid()
        End If
    End Sub
    Public Sub Fillgrid()
        Try
            sql = "select * from Bird_transfer where from_shed_loc='" & shedloc.Text & "' and isPosted=0"
            gmod.DataSetRet(sql, "btdate")
            Session("usernametrf_birds") = gmod.ds.Tables("btdate").Rows(0)("user_name")
            transfer_grid.DataSource = gmod.ds.Tables("btdate")
            transfer_grid.DataBind()
        Catch ex As Exception
            transfer_grid.DataSource = Nothing
            transfer_grid.DataBind()
        End Try

    End Sub

    Protected Sub transfer_grid_DeleteCommand(source As Object, e As DataGridCommandEventArgs) Handles transfer_grid.DeleteCommand
        Dim y As Int64
        Dim z As String

        y = e.Item.ItemIndex

        sql = "delete from [dbo].[Bird_transfer] where [entry_date] ='" & transfer_grid.Items(y).Cells(1).Text & "' and "
        sql &= "[from_shed_loc]='" & transfer_grid.Items(y).Cells(2).Text & "' and [from_shed_no]='" & transfer_grid.Items(y).Cells(3).Text & "' and [from_line_no]='" & transfer_grid.Items(y).Cells(4).Text & "'"
        ' gmod.SqlExecuteNonQuery(sql)
        'Response.Write(y)
        'Fillgrid()

        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand(sql, conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Delete Bird Transfer Confirmed Sucessfully"
                    Fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "delete", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try



    End Sub

    Protected Sub btconfirm_Click(sender As Object, e As EventArgs) Handles btconfirm.Click
        Try
            Using conn As New SqlConnection(gmod.Connstr)
                conn.Open()
                Using cmd As New SqlCommand("exec confirm_birdtransfer '" & Session("usernametrf_birds") & "'", conn)
                    cmd.ExecuteNonQuery()
                    lblprint.Text = "Daily Bird Transfer Confirmed Sucessfully"
                    Fillgrid()
                End Using
            End Using
        Catch ex As Exception
            gmod.LogErrorToFile(ex.Message.ToString, "confirm_daily_mortalitypitsale_entries", "confirm", Session("user_name") + "-" + Session("location_name"))
        End Try

        ' gmod.SqlExecuteNonQuery("exec confirm_birdtransfer '" & Session("usernametrf_birds") & "'")
        'lblprint.Text = "Data Confirmed Sucessfully"
        'Fillgrid()
    End Sub

    Public Sub fillLocation()
        sql = "select [location_code],[location_name] from [dbo].[location_master]  where org_code='10' "
        gmod.DataSetRet(sql, "shed_loc")
        shedloc.DataSource = gmod.ds.Tables("shed_loc")
        shedloc.DataTextField = "location_name"
        shedloc.DataValueField = "location_code"
        shedloc.DataBind()
    End Sub
    Protected Sub btnShoe_Click(sender As Object, e As EventArgs) Handles btnShoe.Click
        Fillgrid()
    End Sub
End Class
