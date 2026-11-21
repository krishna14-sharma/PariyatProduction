
Partial Class masterforms_BirdTypeMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub birdtype_btnSave_Click(sender As Object, e As EventArgs) Handles birdtype_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [dbo].[bird_type_master]([bird_type_code], [bird_type_name]) values("
        sql &= "'" & birdtypecode.Text & "',"
        sql &= "'" & birdname.Text & "')"

        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Bird Type Data Saved Successfully"
            fillGrid()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Public Sub validation_check()
        If birdtypecode.Text = "" Then
            valiation = False
        End If

        If birdname.Text = "" Then
            valiation = False
        End If

    End Sub
    Public Sub fillGrid()
        sql = "select * from bird_type_master"
        gmod.DataSetRet(sql, "bird_type_master")
        birdtype_grid.DataSource = gmod.ds.Tables("bird_type_master")
        birdtype_grid.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillGrid()
        Else

        End If
    End Sub
End Class
