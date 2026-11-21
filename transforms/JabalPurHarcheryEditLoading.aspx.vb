Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class transforms_JabalPurHarcheryEditLoading
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        BindGrid()
    End Sub
    Sub BindGrid()
        Try
            Using con As New SqlConnection(gmod.Connstr)
                Dim da As New SqlDataAdapter("SELECT  CONVERT(VARCHAR, entry_date, 103) [entry_date], CONVERT(VARCHAR, egg_coll_date, 103) [egg_coll_date], [ftype], [flock_no], [age], [f_taken], [f_set], [f_rej], [bg_taken], [bg_set], [bg_rej], [md_taken], [md_set], [md_rej], [mp_taken], [mp_set], [mp_rej], [mm_taken], [mm_set], [mm_rej], [comm_taken], [comm_set], [comm_rej], [hatchery], [machine],[f_leaker], [bg_leaker], [md_leaker], [mp_leaker], [mm_leaker], [comm_leaker], CONVERT(VARCHAR, egg_coll_date_to, 103) [egg_coll_date_to], id FROM hatch_setter where [entry_date] = '" & lpoadinfdate.Value & "' and flock_no = '" & flock_no.Value & "' ORDER BY id DESC", con)
                Dim dt As New DataTable()
                da.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
            End Using
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)

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
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)
        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)

        Dim sql As String = _
 "UPDATE hatch_setter SET " & _
 "f_taken=@f_taken, " & _
 "f_set=@f_set, " & _
 "f_rej=@f_rej, " & _
 "f_leaker=@f_leaker, " & _
 "bg_taken=@bg_taken, " & _
 "bg_set=@bg_set, " & _
 "bg_rej=@bg_rej, " & _
 "bg_leaker=@bg_leaker, " & _
 "md_taken=@md_taken, " & _
 "md_set=@md_set, " & _
 "md_rej=@md_rej, " & _
 "md_leaker=@md_leaker, " & _
 "mp_taken=@mp_taken, " & _
 "mp_set=@mp_set, " & _
 "mp_rej=@mp_rej, " & _
 "mp_leaker=@mp_leaker, " & _
 "mm_taken=@mm_taken, " & _
 "mm_set=@mm_set, " & _
 "mm_rej=@mm_rej, " & _
 "mm_leaker=@mm_leaker, " & _
 "comm_taken=@comm_taken, " & _
 "comm_set=@comm_set, " & _
 "comm_rej=@comm_rej, " & _
 "comm_leaker=@comm_leaker " & _
  "WHERE id=@id"


        Dim cmd As New SqlCommand(sql, gmod.SqlConn)
        gmod.SqlConn.Open()

        Dim values As New Dictionary(Of String, String)
        Dim cols As String() = {
            "entry_date", "egg_coll_date",
            "ftype", "flock_no", "age",
            "f_taken", "f_set", "f_rej", "f_leaker",
            "bg_taken", "bg_set", "bg_rej", "bg_leaker",
            "md_taken", "md_set", "md_rej", "md_leaker",
            "mp_taken", "mp_set", "mp_rej", "mp_leaker",
            "mm_taken", "mm_set", "mm_rej", "mm_leaker",
            "comm_taken", "comm_set", "comm_rej", "comm_leaker",
            "hatchery", "machine", "egg_coll_date_to"}

        Dim cellIndex As Integer = 1   ' start after ID
        For Each col As String In cols
            Dim txtValue As String = ""
            For Each ctrl As Control In row.Cells(cellIndex).Controls
                If TypeOf ctrl Is TextBox Then
                    txtValue = DirectCast(ctrl, TextBox).Text.Trim()
                    Exit For
                End If
            Next
            values.Add(col, txtValue)
            cellIndex += 1
        Next

        For Each kv In values
            cmd.Parameters.AddWithValue("@" & kv.Key, kv.Value)
        Next
        cmd.Parameters.AddWithValue("@id", id)
        cmd.ExecuteNonQuery()
        gmod.SqlConn.Close()

        GridView1.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub GridView1_RowDeleting(sender As Object, e As GridViewDeleteEventArgs)
        'Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)

        'Using con As New SqlConnection(gmod.Connstr)
        '    Dim cmd As New SqlCommand("DELETE FROM hatch_setter_sat WHERE id=@id", con)
        '    cmd.Parameters.AddWithValue("@id", id)
        '    con.Open()
        '    cmd.ExecuteNonQuery()
        'End Using

        BindGrid()
    End Sub
End Class
