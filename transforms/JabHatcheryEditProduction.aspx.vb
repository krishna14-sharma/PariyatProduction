Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data

Partial Class transforms_JabHatcheryEditProduction
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim con As New SqlConnection(gmod.Connstr)
    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        BindGrid()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub
    Sub BindGrid()
        Try
            Using con As New SqlConnection(gmod.Connstr)

                sql = "SELECT  [loading_date], [ftype], [flock_no], [age], [fresh_fertile_eggs], [fresh_infertile_eggs], [fresh_chichs], [fresh_hatcherloss], [fresh_supply], [fresh_culls], [fresh_per], [bg_fertile_eggs], [bg_infertile_eggs], [bg_chicks], [bg_hatcherloss], [bg_supply], [bg_culls], [bg_per], [md_fertile_eggs], [md_infertile_eggs], [md_chicks], [md_hatcherloss], [md_supply], [md_culls], [md_per], [mp_fertile_eggs], [mp_infertile_eggs], [mp_chicks], [mp_hatcherloss], [mp_supply], [mp_culls], [mp_per], [mm_fertile_eggs], [mm_infertile_eggs], [mm_chicks], [mm_hatcherloss], [mm_supply], [mm_culls], [mm_per], [comm_fertile_eggs], [comm_infertile_eggs], [comm_chicks], [comm_hatcherloss], [comm_supply], [comm_culls], [comm_per], [hatchery], [machine], [id] from hatch_production where [loading_date] = '" & lpoadinfdate.Value & "' and flock_no = '" & flock_no.Value & "' ORDER BY id DESC"
                Dim da As New SqlDataAdapter(sql, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
            End Using
        Catch ex As Exception

        End Try

    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        BindGrid()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
        BindGrid()
    End Sub
    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)

        Dim sql As String = "UPDATE hatch_production SET " & _
        "loading_date=@loading_date, ftype=@ftype, flock_no=@flock_no, age=@age, " & _
        "fresh_fertile_eggs=@fresh_fertile_eggs, fresh_infertile_eggs=@fresh_infertile_eggs, fresh_chichs=@fresh_chichs, fresh_hatcherloss=@fresh_hatcherloss, fresh_supply=@fresh_supply, fresh_culls=@fresh_culls, fresh_per=@fresh_per, " & _
        "bg_fertile_eggs=@bg_fertile_eggs, bg_infertile_eggs=@bg_infertile_eggs, bg_chicks=@bg_chicks, bg_hatcherloss=@bg_hatcherloss, bg_supply=@bg_supply, bg_culls=@bg_culls, bg_per=@bg_per, " & _
        "md_fertile_eggs=@md_fertile_eggs, md_infertile_eggs=@md_infertile_eggs, md_chicks=@md_chicks, md_hatcherloss=@md_hatcherloss, md_supply=@md_supply, md_culls=@md_culls, md_per=@md_per, " & _
        "mp_fertile_eggs=@mp_fertile_eggs, mp_infertile_eggs=@mp_infertile_eggs, mp_chicks=@mp_chicks, mp_hatcherloss=@mp_hatcherloss, mp_supply=@mp_supply, mp_culls=@mp_culls, mp_per=@mp_per, " & _
        "mm_fertile_eggs=@mm_fertile_eggs, mm_infertile_eggs=@mm_infertile_eggs, mm_chicks=@mm_chicks, mm_hatcherloss=@mm_hatcherloss, mm_supply=@mm_supply, mm_culls=@mm_culls, mm_per=@mm_per, " & _
        "comm_fertile_eggs=@comm_fertile_eggs, comm_infertile_eggs=@comm_infertile_eggs, comm_chicks=@comm_chicks, comm_hatcherloss=@comm_hatcherloss, comm_supply=@comm_supply, comm_culls=@comm_culls, comm_per=@comm_per, " & _
        "hatchery=@hatchery, machine=@machine WHERE id=@id"

        Dim cmd As New SqlCommand(sql, con)

        ' Loop all textboxes automatically
        For Each cell As TableCell In row.Cells
            For Each ctrl As Control In cell.Controls
                If TypeOf ctrl Is TextBox Then
                    Dim txt As TextBox = CType(ctrl, TextBox)
                    cmd.Parameters.AddWithValue("@" & txt.ID, txt.Text)
                End If
            Next
        Next

        cmd.Parameters.AddWithValue("@id", id)

        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()

        GridView1.EditIndex = -1
        BindGrid()

    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)

        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)

        Dim cmd As New SqlCommand("DELETE FROM hatch_production WHERE id=@id", con)
        'cmd.Parameters.AddWithValue("@id", id)

        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()

        BindGrid()

    End Sub
    Private Function GetStr(txt As TextBox) As String
        Return txt.Text.Trim()
    End Function

    Private Function GetInt(txt As TextBox) As Integer
        If String.IsNullOrWhiteSpace(txt.Text) Then Return 0
        Return Convert.ToInt32(txt.Text)
    End Function

    Private Function GetDec(txt As TextBox) As Decimal
        If String.IsNullOrWhiteSpace(txt.Text) Then Return 0D
        Return Convert.ToDecimal(txt.Text)
    End Function

End Class
