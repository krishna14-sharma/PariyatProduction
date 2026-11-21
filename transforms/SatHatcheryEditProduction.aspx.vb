Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Partial Class transforms_SatHatcheryEditProduction
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

                sql = "SELECT   [loading_date], [ftype], [flock_no], [age], [fresh_fertile_eggs], [fresh_infertile_eggs], [fresh_chichs], [fresh_hatcherloss], [fresh_supply], [fresh_culls], [fresh_per], [bg_fertile_eggs], [bg_infertile_eggs], [bg_chicks], [bg_hatcherloss], [bg_supply], [bg_culls], [bg_per], [md_fertile_eggs], [md_infertile_eggs], [md_chicks], [md_hatcherloss], [md_supply], [md_culls], [md_per], [mp_fertile_eggs], [mp_infertile_eggs], [mp_chicks], [mp_hatcherloss], [mp_supply], [mp_culls], [mp_per], [mm_fertile_eggs], [mm_infertile_eggs], [mm_chicks], [mm_hatcherloss], [mm_supply], [mm_culls], [mm_per], [comm_fertile_eggs], [comm_infertile_eggs], [comm_chicks], [comm_hatcherloss], [comm_supply], [comm_culls], [comm_per], [hatchery], [machine], [id] from hatch_production_sat where [loading_date] = '" & lpoadinfdate.Value & "' and flock_no = '" & flock_no.Value & "' ORDER BY id DESC"
                Dim da As New SqlDataAdapter(sql, con)
                Dim dt As New DataTable()
                da.Fill(dt)
                GridView1.DataSource = dt
                GridView1.DataBind()
            End Using
        Catch ex As Exception

        End Try

    End Sub
    Protected Sub gvHatch_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim r As GridViewRow = GridView1.Rows(e.RowIndex)
        Dim id As Integer = GridView1.DataKeys(e.RowIndex).Value

        Dim sql As String =
            "UPDATE hatch_production_sat SET " & _
            "ftype=@ftype, flock_no=@flock_no, age=@age, " & _
            "fresh_fertile_eggs=@f1, fresh_infertile_eggs=@f2, fresh_chichs=@f3, " & _
            "fresh_hatcherloss=@f4, fresh_supply=@f5, fresh_culls=@f6, fresh_per=@f7, " & _
            "bg_fertile_eggs=@b1, bg_infertile_eggs=@b2, bg_chicks=@b3, " & _
            "bg_hatcherloss=@b4, bg_supply=@b5, bg_culls=@b6, bg_per=@b7, " & _
            "md_fertile_eggs=@m1, md_infertile_eggs=@m2, md_chicks=@m3, " & _
            "md_hatcherloss=@m4, md_supply=@m5, md_culls=@m6, md_per=@m7, " & _
             "mp_fertile_eggs=@p1, mp_infertile_eggs=@p2, mp_chicks=@p3, " & _
            "mp_hatcherloss=@p4, mp_supply=@p5, mp_culls=@p6, mp_per=@p7, " & _
            "mm_fertile_eggs=@mm1, mm_infertile_eggs=@mm2, mm_chicks=@mm3, " & _
            "mm_hatcherloss=@mm4, mm_supply=@mm5, mm_culls=@mm6, mm_per=@mm7, " & _
             "comm_fertile_eggs=@c1, comm_infertile_eggs=@c2, comm_chicks=@c3, " & _
            "comm_hatcherloss=@c4, comm_supply=@c5, comm_culls=@c6, comm_per=@c7, " & _
           "hatchery=@hatchery, machine=@machine " & _
            "WHERE id=@id"


        Dim cmd As New SqlCommand(sql, gmod.SqlConn)

        '--- BASIC INFO ---
        cmd.Parameters.AddWithValue("@ftype", GetStr(CType(r.Cells(1).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@flock_no", GetStr(CType(r.Cells(2).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@age", GetStr(CType(r.Cells(3).Controls(0), TextBox)))

        '--- FRESH ---
        cmd.Parameters.AddWithValue("@f1", GetInt(CType(r.Cells(4).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f2", GetInt(CType(r.Cells(5).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f3", GetInt(CType(r.Cells(6).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f4", GetInt(CType(r.Cells(7).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f5", GetInt(CType(r.Cells(8).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f6", GetInt(CType(r.Cells(9).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@f7", GetDec(CType(r.Cells(10).Controls(0), TextBox)))

        '--- BG ---
        cmd.Parameters.AddWithValue("@b1", GetInt(CType(r.Cells(11).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b2", GetInt(CType(r.Cells(12).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b3", GetInt(CType(r.Cells(13).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b4", GetInt(CType(r.Cells(14).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b5", GetInt(CType(r.Cells(15).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b6", GetInt(CType(r.Cells(16).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@b7", GetDec(CType(r.Cells(17).Controls(0), TextBox)))

        '--- MD ---
        cmd.Parameters.AddWithValue("@m1", GetInt(CType(r.Cells(18).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m2", GetInt(CType(r.Cells(19).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m3", GetInt(CType(r.Cells(20).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m4", GetInt(CType(r.Cells(21).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m5", GetInt(CType(r.Cells(22).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m6", GetInt(CType(r.Cells(23).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@m7", GetDec(CType(r.Cells(24).Controls(0), TextBox)))

        '--- MP ---
        cmd.Parameters.AddWithValue("@p1", GetInt(CType(r.Cells(25).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p2", GetInt(CType(r.Cells(26).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p3", GetInt(CType(r.Cells(27).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p4", GetInt(CType(r.Cells(28).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p5", GetInt(CType(r.Cells(29).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p6", GetInt(CType(r.Cells(30).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@p7", GetDec(CType(r.Cells(31).Controls(0), TextBox)))

        '--- MM ---
        cmd.Parameters.AddWithValue("@mm1", GetInt(CType(r.Cells(32).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm2", GetInt(CType(r.Cells(33).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm3", GetInt(CType(r.Cells(34).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm4", GetInt(CType(r.Cells(35).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm5", GetInt(CType(r.Cells(36).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm6", GetInt(CType(r.Cells(37).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@mm7", GetDec(CType(r.Cells(38).Controls(0), TextBox)))

        '--- COMM ---
        cmd.Parameters.AddWithValue("@c1", GetInt(CType(r.Cells(39).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c2", GetInt(CType(r.Cells(40).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c3", GetInt(CType(r.Cells(41).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c4", GetInt(CType(r.Cells(42).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c5", GetInt(CType(r.Cells(43).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c6", GetInt(CType(r.Cells(44).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@c7", GetDec(CType(r.Cells(45).Controls(0), TextBox)))

        '--- FINAL ---
        cmd.Parameters.AddWithValue("@hatchery", GetStr(CType(r.Cells(46).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@machine", GetStr(CType(r.Cells(47).Controls(0), TextBox)))
        cmd.Parameters.AddWithValue("@id", id)

        gmod.SqlConn.Open()
        cmd.ExecuteNonQuery()
        gmod.SqlConn.Close()

        GridView1.EditIndex = -1
        BindGrid()

    End Sub

    Protected Sub GridView1_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)

        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
        Dim id As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Value)

        Dim sql As String = "UPDATE hatch_production_sat SET " & _
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
        'Dim id As Integer = gvHatch.DataKeys(e.RowIndex).Value
        'Dim cmd As New SqlCommand("DELETE FROM hatch_production_sat WHERE id=@id", con)
        'cmd.Parameters.AddWithValue("@id", id)

        'con.Open()
        'cmd.ExecuteNonQuery()
        'con.Close()

        'BindGrid()
    End Sub

    Protected Sub GridView1_RowEditing(sender As Object, e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        BindGrid()
    End Sub

    Protected Sub GridView1_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        GridView1.EditIndex = -1
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
