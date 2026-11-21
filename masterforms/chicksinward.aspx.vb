Imports System.Data.SqlClient
Imports System.Data

Partial Class masterforms_chicksinward
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            LoadGrid()
        End If
    End Sub

    Private Sub LoadGrid()
        Using conn As New SqlConnection(gmod.Connstr)
            Dim cmd As New SqlCommand("SELECT * FROM ChicksInward ORDER BY Id DESC", conn)
            conn.Open()
            Dim da As New SqlDataAdapter(cmd)
            Dim dt As New DataTable()
            da.Fill(dt)
            GridView1.DataSource = dt
            GridView1.DataBind()
        End Using
    End Sub

    Protected Sub btnInsert_Click(sender As Object, e As EventArgs) Handles btnInsert.Click
        If txtFlock.Text <> "" Then
            Using con As New SqlConnection(gmod.Connstr)
                Dim query As String = "INSERT INTO ChicksInward " &
                                      "([EntryDate], [Flock], [BirdsStockF], [BirdsStockM], [MortalityF], [MortalityM], [LooseF],[LooseM], [ShortF], [ShortM], [GoodConditionF], [GoodConditionM], [RateF], [RateM], [BroodingArea], [Remark]) " &
                                      "VALUES (@EntryDate, @Flock, @BirdsStockF, @BirdsStockM, @MortalityF, @MortalityM, @LooseF, @LooseM, @ShortF, @ShortM, @GoodF, @GoodM, @RateF, @RateM, @BroodingArea, @Remark)"
                Using cmd As New SqlCommand(query, con)
                    cmd.Parameters.AddWithValue("@EntryDate", txtDate.Text)
                    cmd.Parameters.AddWithValue("@Flock", txtFlock.Text)
                    cmd.Parameters.AddWithValue("@BirdsStockF", txtBirdsF.Text)
                    cmd.Parameters.AddWithValue("@BirdsStockM", txtBirdsM.Text)
                    cmd.Parameters.AddWithValue("@MortalityF", txtMortalityF.Text)
                    cmd.Parameters.AddWithValue("@MortalityM", txtMortalityM.Text)
                    cmd.Parameters.AddWithValue("@LooseF", txtLooseF.Text)
                    cmd.Parameters.AddWithValue("@LooseM", txtLooseM.Text)
                    cmd.Parameters.AddWithValue("@ShortF", txtShortF.Text)
                    cmd.Parameters.AddWithValue("@ShortM", txtShortM.Text)
                    cmd.Parameters.AddWithValue("@GoodF", txtGoodF.Text)
                    cmd.Parameters.AddWithValue("@GoodM", txtGoodM.Text)
                    cmd.Parameters.AddWithValue("@RateF", txtRateF.Text)
                    cmd.Parameters.AddWithValue("@RateM", txtRateM.Text)
                    cmd.Parameters.AddWithValue("@BroodingArea", txtBroodingArea.Text)
                    cmd.Parameters.AddWithValue("@Remark", txtRemarks.Text)
                    con.Open()
                    cmd.ExecuteNonQuery()
                End Using
            End Using
            ClearFields()
            LoadGrid()
        End If
    End Sub

    Protected Sub btnUpdate_Click(sender As Object, e As EventArgs) Handles btnUpdate.Click
        If String.IsNullOrEmpty(hfId.Value) Then Exit Sub
        Using conn As New SqlConnection(gmod.Connstr)
            Dim sql As String = "UPDATE ChicksSummary SET EntryDate=@EntryDate,Flock=@Flock,BirdsStockF=@BirdsStockF,BirdsStockM=@BirdsStockM,"
            sql &= " MortalityF=@MortalityF,MortalityM=@MortalityM,LooseF=@LooseF,LooseM=@LooseM,ShortF=@ShortF,ShortM=@ShortM,"
            sql &= " GoodConditionF=@GoodConditionF,GoodConditionM=@GoodConditionM WHERE Id=@Id"
            Dim cmd As New SqlCommand(sql, conn)
            AddParameters(cmd)
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hfId.Value))
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
        ClearFields()
        LoadGrid()
    End Sub

    Protected Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If String.IsNullOrEmpty(hfId.Value) Then Exit Sub
        Using conn As New SqlConnection(gmod.Connstr)
            Dim cmd As New SqlCommand("DELETE FROM ChicksInward WHERE Id=@Id", conn)
            cmd.Parameters.AddWithValue("@Id", Convert.ToInt32(hfId.Value))
            conn.Open()
            cmd.ExecuteNonQuery()
        End Using
        ClearFields()
        LoadGrid()
    End Sub

    Private Sub AddParameters(cmd As SqlCommand)
        Try
            cmd.Parameters.AddWithValue("@EntryDate", Convert.ToDateTime(txtDate.Text))
        cmd.Parameters.AddWithValue("@Flock", txtFlock.Text)
        cmd.Parameters.AddWithValue("@BirdsStockF", Val(txtBirdsF.Text))
        cmd.Parameters.AddWithValue("@BirdsStockM", Val(txtBirdsM.Text))
        cmd.Parameters.AddWithValue("@MortalityF", Val(txtMortalityF.Text))
        cmd.Parameters.AddWithValue("@MortalityM", Val(txtMortalityM.Text))
        cmd.Parameters.AddWithValue("@LooseF", Val(txtLooseF.Text))
        cmd.Parameters.AddWithValue("@LooseM", Val(txtLooseM.Text))
        cmd.Parameters.AddWithValue("@ShortF", Val(txtShortF.Text))
        cmd.Parameters.AddWithValue("@ShortM", Val(txtShortM.Text))
        cmd.Parameters.AddWithValue("@GoodConditionF", Val(txtGoodF.Text))
            cmd.Parameters.AddWithValue("@GoodConditionM", Val(txtGoodM.Text))
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GridView1_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim row As GridViewRow = GridView1.SelectedRow
        hfId.Value = GridView1.DataKeys(row.RowIndex).Value.ToString()
        txtDate.Text = row.Cells(1).Text
        txtFlock.Text = row.Cells(2).Text
        txtBirdsF.Text = row.Cells(3).Text
        txtBirdsM.Text = row.Cells(4).Text
    End Sub

    Private Sub ClearFields()
        txtDate.Text = ""
        txtFlock.Text = ""
        txtBirdsF.Text = ""
        txtBirdsM.Text = ""
        txtMortalityF.Text = ""
        txtMortalityM.Text = ""
        txtLooseF.Text = ""
        txtLooseM.Text = ""
        txtShortF.Text = ""
        txtShortM.Text = ""
        txtGoodF.Text = ""
        txtGoodM.Text = ""
        hfId.Value = ""
    End Sub
End Class
