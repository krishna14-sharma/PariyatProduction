
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Configuration
Imports System.Web.UI.WebControls

Partial Class transforms_Graph_data_edit
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Private ReadOnly Property ConnString() As String
        Get
            Return ConfigurationManager.ConnectionStrings( _
                "PoultryConnection").ConnectionString
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, _
                            ByVal e As EventArgs) Handles Me.Load

        If Not IsPostBack Then
            BindGrid()
        End If

    End Sub

    Private Sub BindGrid()

        Using con As New SqlConnection(gmod.Connstr)

            Dim sql As String = _
                "SELECT id, entry_date, flock_no, fstock, " & _
                "eggstock, heggstock, mortality, age, crack, " & _
                "comm, leaker, total_loading, total_fertile, " & _
                "total_infertile, total_chicks, oth1, oth2 " & _
                "FROM dbo.Graph_data   where flock_no = '" & flock_no.Value & "' ORDER BY flock_no, age"

            Using da As New SqlDataAdapter(sql, con)

                Dim dt As New DataTable()
                da.Fill(dt)

                gvGraph.DataSource = dt
                gvGraph.DataBind()

            End Using

        End Using

    End Sub

    Protected Sub gvGraph_RowEditing( _
        ByVal sender As Object, _
        ByVal e As GridViewEditEventArgs)

        gvGraph.EditIndex = e.NewEditIndex
        BindGrid()

    End Sub

    Protected Sub gvGraph_RowCancelingEdit( _
        ByVal sender As Object, _
        ByVal e As GridViewCancelEditEventArgs)

        gvGraph.EditIndex = -1
        BindGrid()

        lblMessage.Text = ""

    End Sub

    Private Function GetDecimalValue(ByVal value As Object) As Object

        Dim s As String = Convert.ToString(value).Trim()

        If s = "" Then Return DBNull.Value

        Dim result As Decimal

        If Not Decimal.TryParse(s, NumberStyles.Number, _
            CultureInfo.CurrentCulture, result) Then

            Throw New Exception("Invalid numeric value: " & s)

        End If

        Return result

    End Function

    Private Function GetIntValue(ByVal value As Object) As Object

        Dim s As String = Convert.ToString(value).Trim()

        If s = "" Then Return DBNull.Value

        Dim result As Integer

        If Not Integer.TryParse(s, result) Then
            Throw New Exception("Invalid integer value: " & s)
        End If

        Return result

    End Function

    Private Function GetDateValue(ByVal value As Object) As Object

        Dim s As String = Convert.ToString(value).Trim()

        If s = "" Then Return DBNull.Value

        Dim result As DateTime

        If Not DateTime.TryParse(s, CultureInfo.CurrentCulture, _
            DateTimeStyles.None, result) Then

            Throw New Exception("Invalid entry date: " & s)

        End If

        Return result

    End Function

    Private Sub AddDecimalParameter( _
        ByVal cmd As SqlCommand, _
        ByVal name As String, _
        ByVal value As Object)

        Dim p As SqlParameter = _
            cmd.Parameters.Add(name, SqlDbType.Decimal)

        p.Precision = 18
        p.Scale = 0
        p.Value = GetDecimalValue(value)

    End Sub

    Protected Sub gvGraph_RowUpdating( _
        ByVal sender As Object, _
        ByVal e As GridViewUpdateEventArgs)

        Try

            Dim flockNo As String = _
                Convert.ToString( _
                gvGraph.DataKeys(e.RowIndex).Values("flock_no"))

            Dim age As Integer = Convert.ToInt32( _
                gvGraph.DataKeys(e.RowIndex).Values("age"))

            Using con As New SqlConnection(gmod.Connstr)

                Dim sql As String = _
                    "UPDATE dbo.Graph_data SET " & _
                    "entry_date=@entry_date, " & _
                    "fstock=@fstock, eggstock=@eggstock, " & _
                    "heggstock=@heggstock, mortality=@mortality, " & _
                    "crack=@crack, comm=@comm, leaker=@leaker, " & _
                    "total_loading=@total_loading, " & _
                    "total_fertile=@total_fertile, " & _
                    "total_infertile=@total_infertile, " & _
                    "total_chicks=@total_chicks, " & _
                    "oth1=@oth1, oth2=@oth2 " & _
                    "WHERE flock_no=@KeyFlockNo AND age=@KeyAge"

                Using cmd As New SqlCommand(sql, con)

                    cmd.Parameters.Add("@KeyFlockNo", _
                        SqlDbType.VarChar, 50).Value = flockNo

                    cmd.Parameters.Add("@KeyAge", _
                        SqlDbType.Int).Value = age

                    AddDecimalParameter(cmd, "@fstock", _
                        e.NewValues("fstock"))

                    AddDecimalParameter(cmd, "@eggstock", _
                        e.NewValues("eggstock"))

                    AddDecimalParameter(cmd, "@heggstock", _
                        e.NewValues("heggstock"))

                    AddDecimalParameter(cmd, "@crack", _
                        e.NewValues("crack"))

                    AddDecimalParameter(cmd, "@comm", _
                        e.NewValues("comm"))

                    AddDecimalParameter(cmd, "@leaker", _
                        e.NewValues("leaker"))

                    AddDecimalParameter(cmd, "@total_loading", _
                        e.NewValues("total_loading"))

                    AddDecimalParameter(cmd, "@total_fertile", _
                        e.NewValues("total_fertile"))

                    AddDecimalParameter(cmd, "@total_infertile", _
                        e.NewValues("total_infertile"))

                    AddDecimalParameter(cmd, "@total_chicks", _
                        e.NewValues("total_chicks"))

                    AddDecimalParameter(cmd, "@oth1", _
                        e.NewValues("oth1"))

                    AddDecimalParameter(cmd, "@oth2", _
                        e.NewValues("oth2"))

                    cmd.Parameters.Add("@mortality", _
                        SqlDbType.Int).Value = _
                        GetIntValue(e.NewValues("mortality"))

                    cmd.Parameters.Add("@entry_date", _
                        SqlDbType.DateTime).Value = _
                        GetDateValue(e.NewValues("entry_date"))

                    con.Open()

                    Dim affected As Integer = cmd.ExecuteNonQuery()

                    If affected = 0 Then
                        Throw New Exception( _
                            "Record not found or not updated.")
                    End If

                End Using

            End Using

            gvGraph.EditIndex = -1
            lblMessage.ForeColor = _
                System.Drawing.Color.Green

            lblMessage.Text = "Record updated successfully."

            BindGrid()

        Catch ex As Exception

            lblMessage.ForeColor = _
                System.Drawing.Color.Red

            lblMessage.Text = "Update failed: " & ex.Message

            e.Cancel = True

        End Try

    End Sub

    Protected Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        BindGrid()
    End Sub
End Class
