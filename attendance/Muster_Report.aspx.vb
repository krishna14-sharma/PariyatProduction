
Partial Class attendance_Muster_Report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    Sub fill()

        sql = "EXEC usp_GenerateAttendanceReport '" & entrydate.Value.ToString & "','" & entrydate1.Value.ToString & "'"
        gmod.DataSetRet(sql, "musterroll")


        sql = " select a.*,e.AreaCode,e.WorkType from EmpMaster e inner join  Attendance_Report a "
        sql &= " on e.EmpCode= a.EmpCode  where e.AreaCode ='" & cmbdept.Text & "'"

        gmod.DataSetRet(sql, "pmcondata")

        GridView1.DataSource = gmod.ds.Tables("pmcondata")
        GridView1.DataBind()
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
            'lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Else
        End If
    End Sub

    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        Else
            ' fill()
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select  distinct AreaCode from EmpMaster order by AreaCode"
        gmod.DataSetRet(sql, "Dept")
        cmbdept.DataSource = gmod.ds.Tables("Dept")
        cmbdept.DataTextField = "AreaCode"
        cmbdept.DataValueField = "AreaCode"
        cmbdept.DataBind()
    End Sub
    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' DO NOT remove this override
    End Sub
    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=MustrReport.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"
        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)

        ' Remove paging to export all rows if needed
        GridView1.AllowPaging = False

        sql = "EXEC usp_GenerateAttendanceReport '" & entrydate.Value.ToString & "','" & entrydate1.Value.ToString & "'"
        gmod.DataSetRet(sql, "musterroll")


        sql = " select a.*,e.AreaCode,e.WorkType from EmpMaster e inner join  Attendance_Report a "
        sql &= " on e.EmpCode= a.EmpCode  where e.AreaCode ='" & cmbdept.Text & "'"

        gmod.DataSetRet(sql, "pmcondata")

        GridView1.DataSource = gmod.ds.Tables("pmcondata")
        GridView1.DataBind()

        ' Export
        GridView1.RenderControl(hw)
        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
    End Sub
End Class
