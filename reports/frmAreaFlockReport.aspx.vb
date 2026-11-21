Imports CrystalDecisions.CrystalReports.Engine

Partial Class reports_frmAreaFlockReport
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim daydiff As Integer

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Sub fill()
        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'

            daydiff = DateDiff(DateInterval.Day, CDate(entrydate.Value.ToString), CDate(todate.Value.ToString))

            For i = 1 To daydiff
                gmod.SqlExecuteNonQuery("exec [flock_report] '" & DateAdd(DateInterval.Day, i, CDate(entrydate.Value)) & "'")
            Next


            sql = "select * from tmp_flock_report "
            gmod.DataSetRet(sql, "flock_daily_report_")


            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrShedFlock_report.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("flock_daily_report_"))
            'crystalReport.Refresh()
            ' crystalReport.SetParameterValue("ondate", "AS On:" & entrydate.Value.ToString)
            'crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("flock_no") & "  Age:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("bird_type_code"))

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try

    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ' CrystalReportViewer1.ReportSource = Session("Report")
        Else

        End If
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub
End Class
