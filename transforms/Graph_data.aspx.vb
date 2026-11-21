
Partial Class transforms_Graph_data
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub flockmaster_btnSave_Click(sender As Object, e As EventArgs) Handles flockmaster_btnSave.Click
        ' validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If
        '[total_loading] [numeric](18, 0) NULL,
        '[total_fertile] [numeric](18, 0) NULL,
        '[total_infertile] [numeric](18, 0) NULL,
        '[total_chicks] [numeric](18, 0) NULL,
        '[oth1] [numeric](18, 0) NULL,
        '[oth2] [numeric](18, 0) NULL,

        sql = "insert into [dbo].[Graph_data]([entry_date], [flock_no], [fstock], [eggstock], [heggstock], [mortality], [age],crack,comm,leaker,total_loading,total_fertile,total_infertile,total_chicks,oth1,oth2) values("
        sql &= "'" & Now() & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & fqty.Text & "',"
        sql &= "'" & egstock.Text & "',"
        sql &= "'" & heggstock.Text & "',"
        sql &= "'" & mortality.Text & "',"
        sql &= "'" & age.Text & "',"
        sql &= "'" & Val(crackegg.Text) & "',"
        sql &= "'" & Val(commercial.Text) & "',"
        sql &= "'" & Val(leaker.Text) & "',"

        sql &= "'" & Val(txttotal_loading.Text) & "',"
        sql &= "'" & Val(txttotal_fertile.Text) & "',"
        sql &= "'" & Val(txttotal_infertile.Text) & "',"
        sql &= "'" & Val(txttotalchicks.Text) & "',"
        sql &= "'" & Val("") & "',"
        sql &= "'" & Val("") & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Flock Data Saved Successfully"
            fillGrid()
            ClearControls()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub
    Public Sub fillGrid()
        sql = "select *  from [dbo].[Graph_data] where flock_no ='" & flockno.Text & "'"
        gmod.DataSetRet(sql, "flockamster")
        flockmaster_grid.DataSource = gmod.ds.Tables("flockamster")
        flockmaster_grid.DataBind()
    End Sub

    Public Sub ClearControls()
        flockno.Text = ""
        fqty.Text = ""
        egstock.Text = ""
        heggstock.Text = ""
        mortality.Text = ""
        age.Text = ""
    End Sub
    Public Sub validation_check()
        If fqty.Text = "" Then
            valiation = False
        End If


        If flockno.Text = "" Then
            valiation = False
        End If

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillGrid()
    End Sub
End Class
