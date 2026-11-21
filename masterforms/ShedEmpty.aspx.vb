
Partial Class masterforms_ShedEmpty
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
        Else
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select distinct shed_loc from Shed_Entry_Data where isAlive=1 and isEmpty=1"
        gmod.DataSetRet(sql, "shed_loc")
        loc.DataSource = gmod.ds.Tables("shed_loc")
        loc.DataTextField = "shed_loc"
        loc.DataValueField = "shed_loc"
        loc.DataBind()
    End Sub

    Protected Sub btnshw_Click(sender As Object, e As EventArgs) Handles btnshw.Click
        sql = "select distinct flock_no from Shed_Entry_Data where shed_loc ='" & loc.Text & "' and isAlive=1 and isEmpty=1"
        gmod.DataSetRet(sql, "shed_flk")
        dflock.DataSource = gmod.ds.Tables("shed_flk")
        dflock.DataTextField = "flock_no"
        dflock.DataValueField = "flock_no"
        dflock.DataBind()
    End Sub

    Protected Sub btnshd_Click(sender As Object, e As EventArgs) Handles btnshd.Click
        sql = "select * from Shed_Entry_Data where isAlive=1 and isEmpty=1 and flock_no='" & dflock.Text & "' and shed_loc='" & loc.Text & "'"
        gmod.DataSetRet(sql, "shed_d")
        shedepty.DataSource = gmod.ds.Tables("shed_d")
        shedepty.DataBind()

    End Sub

    Protected Sub btnShedEnpyu_Click(sender As Object, e As EventArgs) Handles btnShedEnpyu.Click
        sql = "update Shed_Entry_Data set isAlive=0 , isEmpty=0 where flock_no='" & dflock.Text & "' and shed_loc='" & loc.Text & "'"
        gmod.SqlExecuteNonQuery(sql)

        sql = "update Shed_Entry_Master set isAlive=0 , isEmpty=0 where flock_no='" & dflock.Text & "' and shed_loc='" & loc.Text & "'"
        gmod.SqlExecuteNonQuery(sql)

        btnshd_Click(sender, e)
    End Sub
End Class
