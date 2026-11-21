
Partial Class transforms_FlockMaster
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub flockmaster_btnSave_Click(sender As Object, e As EventArgs) Handles flockmaster_btnSave.Click
        validation_check()
        If valiation = False Then
            lblprint.Text = "Please Fill All the Data and then Save"
            Exit Sub
        End If
        sql = "insert into [flock_master]([bird_type_code], [flock_no], [fqty], [mqty], [hatch_date], [inw_no],[entry_date]) values("
        sql &= "'" & birdtype.Text & "',"
        sql &= "'" & flockno.Text & "',"
        sql &= "'" & fqty.Text & "',"
        sql &= "'" & mqty.Text & "',"
        sql &= "'" & hatchdate.Value & "',"
        sql &= "'" & inwno.Text & "',"
        sql &= "'" & Now() & "')"




        qyeryresult = gmod.SqlExecuteNonQuery(sql)
        If qyeryresult = "SUCCESS" Then

            sql = "insert into [flock_data]([bird_type_code], [flock_no], [fqty], [mqty], [hatch_date], [inw_no],[entry_date]) values("
            sql &= "'" & birdtype.Text & "',"
            sql &= "'" & flockno.Text & "',"
            sql &= "'" & fqty.Text & "',"
            sql &= "'" & mqty.Text & "',"
            sql &= "'" & hatchdate.Value & "',"
            sql &= "'" & inwno.Text & "',"
            sql &= "'" & Now() & "')"
            gmod.SqlExecuteNonQuery(sql)
            lblprint.Text = "Flock Data Saved Successfully"
            fillGrid()
        Else
            lblprint.Text = qyeryresult
        End If
        ClearControls()

    End Sub

    Public Sub ClearControls()
        flockno.Text = ""
        inwno.Text = ""
        fqty.Text = ""
        mqty.Text = ""
        
    End Sub

    Public Sub validation_check()
        If fqty.Text = "" Then
            valiation = False
        End If

        If mqty.Text = "" Then
            valiation = False
        End If
        If flockno.Text = "" Then
            valiation = False
        End If
        If hatchdate.Value = "" Then
            valiation = False
        End If
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            fillBirdtype()
            fillGrid()
        Else

        End If
    End Sub

    Public Sub fillBirdtype()
        sql = "select [bird_type_code], [bird_type_name] from [dbo].[bird_type_master]"
        gmod.DataSetRet(sql, "shed_loc")
        birdtype.DataSource = gmod.ds.Tables("shed_loc")
        birdtype.DataTextField = "bird_type_code"
        birdtype.DataValueField = "bird_type_code"
        birdtype.DataBind()
    End Sub
    Public Sub fillGrid()
        sql = "select [bird_type_code], [flock_no], [fqty], [mqty],  convert(varchar,hatch_date,103) [hatch_date], [inw_no]  from [dbo].[flock_master]"
        gmod.DataSetRet(sql, "flockamster")
        flockmaster_grid.DataSource = gmod.ds.Tables("flockamster")
        flockmaster_grid.DataBind()
    End Sub
End Class
