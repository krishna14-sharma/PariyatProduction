Imports CrystalDecisions.CrystalReports.Engine

Partial Class reports_shedinfomationreport_temp
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim Remark As String
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        Else
            ' 🔥 VERY IMPORTANT: Reload report from session
            If Not Session("Report:" & Request.Path) Is Nothing Then
                CrystalReportViewer1.ReportSource = Session("Report:" & Request.Path)
            End If
        End If
    End Sub
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    Sub fill()
        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        Try
            sql = "select Remark from daily_entry where shed_loc='" & Session("area_code") & "'   and entry_date='" & entrydate.Value.ToString & "' and LEN(remark)> 5" '"  'Union Select 'Rem'"
            gmod.DataSetRet(sql, "daily_remark_")
            Remark = gmod.ds.Tables("daily_remark_").Rows(0)("remark")

        Catch ex As Exception
            Remark = "No Remark "
        End Try

        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'
            gmod.SqlExecuteNonQuery("exec [tmp_shedwise_report] '" & entrydate.Value.ToString & "', '" & Session("area_code") & "'")

            sql = "select * from tmp_daily_shed_report"
            gmod.DataSetRet(sql, "flock_tmp_daily_report_")

            sql = "select Remark from daily_entry where shed_loc='" & Session("area_code") & "'   and entry_date='" & entrydate.Value.ToString & "' and LEN(remark)> 0 Union Select 'Remarks'"
            gmod.DataSetRet(sql, "daily_remark_")
            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            '' crystalReport.Load(Server.MapPath("~/reports/CrShed_report.rpt")) CrShedFlockWise_report
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrShedFlockWise_report.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("flock_tmp_daily_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ondate", "AS On:" & entrydate.Value.ToString)
            crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("flock_no") & "  Age:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("bird_type_code"))
            crystalReport.SetParameterValue("remark", Remark)
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
            ' crystalReport.Close()
            'crystalReport.Dispose()
        Catch ex As Exception

            AppUi.ShowError(Me, ex.Message)

        Finally
            ' crystalReport.Close()
            ' crystalReport.Dispose()
        End Try

    End Sub

    

End Class
