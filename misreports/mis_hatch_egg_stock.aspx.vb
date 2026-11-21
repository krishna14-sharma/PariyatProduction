Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_mis_hatch_egg_stock
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True


    Sub fill()
        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'

            sql = "select * from hatchery_egg_stock order by flock_no"
            gmod.DataSetRet(sql, "hatcherry_egg_stock")


            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrHatchert_Egg_Stock_Report.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("hatcherry_egg_stock"))
            'crystalReport.Refresh()
            ' crystalReport.SetParameterValue("ondate", "Production Report On:" & entrydate.Value.ToString)
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

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
End Class
