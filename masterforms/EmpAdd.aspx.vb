
Partial Class masterforms_EmpAdd
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim storeflag As Integer
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            fillWorkType()
            'fillGrid()
            'fillLocationto()
        Else
        End If
    End Sub
    Public Sub fillLocation()
        sql = "select distinct AreaCode location_code,AreaName location_name from EmpMaster  Union select Dept,Dept from Department"
        gmod.DataSetRet(sql, "shed_loc")
        arealist.DataSource = gmod.ds.Tables("shed_loc")
        arealist.DataTextField = "location_name"
        arealist.DataValueField = "location_code"
        arealist.DataBind()
    End Sub

    Public Sub fillWorkType()
        sql = "select distinct WorkType from EmpMaster"
        gmod.DataSetRet(sql, "WorkType")
        cmbWorkType.DataSource = gmod.ds.Tables("WorkType")
        cmbWorkType.DataTextField = "WorkType"
        cmbWorkType.DataValueField = "WorkType"
        cmbWorkType.DataBind()
    End Sub
    
    Protected Sub btnaddemp_Click(sender As Object, e As EventArgs) Handles btnaddemp.Click
             If Session("user_name") Is Nothing OrElse
   String.IsNullOrWhiteSpace(Session("user_name").ToString()) Then

            Exit Sub
        End If


        sql = "insert into EmpMaster([EmpCode], [EmpName], [AreaCode], [AreaName], [EmpType], [WorkType], [Sex], [Status]) values("
        sql &= "'" & txtEmpCode.Text & "',"
        sql &= "'" & txtEmpName.Text & "',"
        sql &= "'" & arealist.SelectedValue & "',"
        sql &= "'" & arealist.SelectedItem.Text & "',"
        sql &= "'p',"
        sql &= "'" & cmbWorkType.SelectedValue & "',"
        sql &= "'" & cmbGender.SelectedValue & "',"
        sql &= "'1')"

        lblstring.Text = gmod.SqlExecuteNonQuery(sql)

        txtEmpCode.Text = ""
        txtEmpName.Text = ""
    End Sub
End Class
