Imports CrystalDecisions.CrystalReports.Engine

Partial Class attendance_ConsoldatedAttRepo
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()

    End Sub
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        Else
            fill()
        End If
    End Sub

    Sub fill()
        Try
            GridView1.Visible = False
            CrystalReportViewer1.Visible = True

            gmod.SqlExecuteNonQuery("EXECUTE CompiledAttendanceRepo '" & entrydate.Value.ToString & "'")
            sql = "select * from tmpcmpAtt"
            gmod.DataSetRet(sql, "compile_attrepo")


          
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("CrCompileAttandenceReport.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("compile_attrepo"))
            crystalReport.SetParameterValue("d", entrydate.Value.ToString)
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try

    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        GridView1.Visible = True
        CrystalReportViewer1.Visible = False

        sql = " select distinct AreaCode from EmpMaster where AreaCode "
        sql &= " not in (select distinct loc from daily_attendance where date='" & entrydate.Value.ToString & "')  and status = 0"
        gmod.DataSetRet(sql, "pendingattendancearea")

        GridView1.DataSource = gmod.ds.Tables("pendingattendancearea")
        GridView1.DataBind()

    End Sub
End Class
