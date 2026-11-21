
Partial Class masterforms_Sepreation_reason_master
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

        sql = "insert into [Sepration_master]([seperation_reason]) values("
        sql &= "'" & sepreason.Text & "')"
       


        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then
            lblprint.Text = "Data Saved Successfully"
            fillgrid()
            sepreason.Text = ""
        Else
            lblprint.Text = qyeryresult
        End If
    End Sub

    Sub fillgrid()
        sql = "select * from Sepration_master"
        gmod.DataSetRet(sql, "Sepration_master")
        sepreason_grid.DataSource = gmod.ds.Tables("Sepration_master")
        sepreason_grid.DataBind()
    End Sub

    Public Sub validation_check()
        If sepreason.Text = "" Then
            valiation = False
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            lblString.Text = "Welcome:" & Convert.ToString(Session("user_name"))
            fillgrid()
        Else

        End If
    End Sub
End Class
