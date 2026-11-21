Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_mis_hatch_production_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub

    Sub fill()
        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'

            sql = "select * from [hatch_production] where loading_date = '" & entrydate.Value.ToString & "'"
            gmod.DataSetRet(sql, "hatcherry_production_report_")


            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrHatch_Report.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("hatcherry_production_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ldate", entrydate.Value.ToString)
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
End Class
