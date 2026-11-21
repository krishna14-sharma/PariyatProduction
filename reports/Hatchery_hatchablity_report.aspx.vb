Imports CrystalDecisions.CrystalReports.Engine
Partial Class reports_Hatchery_hatchablity_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
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
        If chkbox.Checked = False Then
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            Try
                'exec [shedwise_report] '10/14/22', 'DHR-1'

                sql = "exec hatchery_final_report '" & entrydate.Value.ToString & "','A'"
                gmod.DataSetRet(sql, "hatchery_hatchablity_report_")


                'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
                ' birdtype.DataSource = gmod.ds.Tables("shed_loc")

                crystalReport.Load(Server.MapPath("~/reports/CrhheReport.rpt"))

                crystalReport.SetDataSource(gmod.ds.Tables("hatchery_hatchablity_report_"))
                'crystalReport.Refresh()
                crystalReport.SetParameterValue("ldate", entrydate.Value.ToString)
                'crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("flock_no") & "  Age:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("bird_type_code"))

                CrystalReportViewer1.ReportSource = crystalReport
                Session("Report:" & Request.Path) = crystalReport


            Catch ex As Exception
                AppUi.ShowError(Me, ex.Message)
                crystalReport.Close()
                crystalReport.Dispose()
            Finally
                crystalReport.Close()
                crystalReport.Dispose()

            End Try



        Else

            Try
                'exec [shedwise_report] '10/14/22', 'DHR-1'
                sql = "exec hatchery_final_report '" & entrydate.Value.ToString & "','B'"
                gmod.DataSetRet(sql, "hatchery_hatchablity_report_")

                'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
                ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
                Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
                crystalReport.Load(Server.MapPath("~/reports/CrhheReport_Bgrage.rpt"))

                crystalReport.SetDataSource(gmod.ds.Tables("hatchery_hatchablity_report_"))
                'crystalReport.Refresh()
                crystalReport.SetParameterValue("ldate", entrydate.Value.ToString)
                'crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("flock_no") & "  Age:" & gmod.ds.Tables("flock_tmp_daily_report_").Rows(0)("bird_type_code"))

                CrystalReportViewer1.ReportSource = crystalReport
                Session("Report:" & Request.Path) = crystalReport

            Catch ex As Exception
                AppUi.ShowError(Me, ex.Message)
            End Try


        End If


    End Sub

    
End Class
