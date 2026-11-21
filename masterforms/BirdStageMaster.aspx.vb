
Partial Class masterforms_BirdStageMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub loc_btnSave_Click(sender As Object, e As EventArgs) Handles loc_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If

        sql = "insert into [bird_stage_master]([stage_code], [stage_name], [start_age], [end_age], [age]) values("
        sql &= "'" & birdstagecode.Text & "',"
        sql &= "'" & birdstagename.Text & "',"
        sql &= "'" & startage.Text & "',"
        sql &= "'" & endage.Text & "',"
        sql &= "'" & age.Text & "')"


        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Bird Stage Type Data Saved Successfully"
            fillGrid()
            ClearControls()
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Public Sub validation_check()
        If birdstagecode.Text = "" Then
            valiation = False
        End If

        If birdstagename.Text = "" Then
            valiation = False
        End If

        If startage.Text = "" Then
            valiation = False
        End If

        If endage.Text = "" Then
            valiation = False
        End If

        If age.Text = "" Then
            valiation = False
        End If

    End Sub
    Public Sub fillGrid()
        sql = "select * from bird_stage_master"
        gmod.DataSetRet(sql, "bird_stage_master")
        birdstage_grid.DataSource = gmod.ds.Tables("bird_stage_master")
        birdstage_grid.DataBind()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillGrid()
        Else

        End If
    End Sub
    Public Sub ClearControls()
        birdstagecode.Text = ""
        birdstagename.Text = ""
        startage.Text = ""
        endage.Text = ""
        age.Text = ""

    End Sub
    Protected Sub birdstagecode_TextChanged(sender As Object, e As EventArgs) Handles birdstagecode.TextChanged

    End Sub
End Class
