Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.Data

Partial Class attendance_monthltatt
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Try

       
        ' If Not IsPostBack Then
        Dim sql As String =
        "INSERT INTO [dbo].[monthly_attendance] "
        sql &= " ([empcode], [loc], [month], [year], [P], [L], [A], [S]) "
        sql &= " SELECT  empcode,  @loc, MONTH(@monthDate) AS [month],"
        sql &= " YEAR(@monthDate) AS [year],"
        sql &= " SUM(CASE WHEN absent = 0 AND [leave] = 0 THEN 1 ELSE 0 END) AS P,"
        sql &= " SUM(CASE WHEN absent = 0 AND [leave] = -1 THEN 1 ELSE 0 END) AS L,"
        sql &= " SUM(CASE WHEN absent = -1 AND [leave] = 0 THEN 1 ELSE 0 END) AS A,"
        sql &= " 0 AS S"
        sql &= " FROM daily_attendance "
        sql &= " WHERE loc = @loc  AND [date] BETWEEN @fromDate AND @toDate GROUP BY empcode"

        Using con As New SqlConnection(gmod.Connstr)
            Using cmd As New SqlCommand(sql, con)
                ' Set parameters
                cmd.Parameters.AddWithValue("@loc", warealist.Text)
                cmd.Parameters.AddWithValue("@monthDate", entrydate1.Value)
                    cmd.Parameters.AddWithValue("@fromDate", entrydate.Value)
                cmd.Parameters.AddWithValue("@toDate", entrydate1.Value)

                con.Open()
                Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                con.Close()

                ' Optional: Show a message or log result
                'System.Diagnostics.Debug.WriteLine(rowsAffected & " records inserted into monthly_attendance.")
            End Using
        End Using


        'End If
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try
        BindGrid()
    End Sub
    Private Sub BindGrid()
        Using con As New SqlConnection(gmod.Connstr)
            Dim cmd As New SqlCommand("select * from monthly_attendance m inner join empmaster e on m.empcode = e.EmpCode  and m.loc  = e.AreaCode where AreaCode='" & warealist.Text & "' and [month]=month('" & entrydate1.Value & "') and [year] = year('" & entrydate1.Value & "')", con)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvAttendance.DataSource = dt
            gvAttendance.DataBind()
        End Using
    End Sub

    Protected Sub gvAttendance_RowEditing(sender As Object, e As GridViewEditEventArgs)
        gvAttendance.EditIndex = e.NewEditIndex
        BindGrid()
    End Sub

    Protected Sub gvAttendance_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs)
        gvAttendance.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub gvAttendance_RowUpdating(sender As Object, e As GridViewUpdateEventArgs)
        Dim id As Integer = Convert.ToInt32(gvAttendance.DataKeys(e.RowIndex).Value)

        ' Get textbox safely using FindControl
        Dim txtS As TextBox = CType(gvAttendance.Rows(e.RowIndex).FindControl("txtS"), TextBox)
        If txtS Is Nothing Then
            Throw New Exception("Unable to find txtS textbox in GridView row.")
        End If

        Dim newS As Integer
        Integer.TryParse(txtS.Text, newS)

        Using con As New SqlConnection(gmod.Connstr)
            Dim cmd As New SqlCommand("UPDATE monthly_attendance SET S=@S WHERE id=@id", con)
            cmd.Parameters.AddWithValue("@S", newS)
            cmd.Parameters.AddWithValue("@id", id)
            con.Open()
            cmd.ExecuteNonQuery()
        End Using

        gvAttendance.EditIndex = -1
        BindGrid()
    End Sub

    Public Sub fillLocation()
        sql = "select distinct AreaCode from EmpMaster order by AreaCode"
        gmod.DataSetRet(sql, "warea")
        warealist.DataSource = gmod.ds.Tables("warea")
        warealist.DataTextField = "AreaCode"
        warealist.DataValueField = "AreaCode"
        warealist.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

            fillLocation()
            
        Else
        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs)
       
    End Sub

    Protected Sub Button1_Click1(sender As Object, e As EventArgs) Handles Button1.Click
        Using con As New SqlConnection(gmod.Connstr)
            Dim cmd As New SqlCommand("delete from monthly_attendance where loc='" & warealist.Text & "' and [month]=month('" & entrydate1.Value & "') and [year] = year('" & entrydate1.Value & "')", con)
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            gvAttendance.DataSource = dt
            gvAttendance.DataBind()
        End Using

        BindGrid()
    End Sub
    Protected Sub gvAttendance_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvAttendance.PageIndexChanging
        gvAttendance.PageIndex = e.NewPageIndex
        BindGrid() ' Call your method that binds the data source to GridView
    End Sub

    Protected Sub gvAttendance_PageIndexChanged(sender As Object, e As EventArgs) Handles gvAttendance.PageIndexChanged

    End Sub

    Protected Sub gvAttendance_PageIndexChanging1(sender As Object, e As GridViewPageEventArgs)

    End Sub
End Class
