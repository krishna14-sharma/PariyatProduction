Imports CrystalDecisions.CrystalReports.Engine

Partial Class reports_setter_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()

    End Sub
    Sub fill()
        Try
            If RadioButton1.Checked = True Then
                sql = "select * from hatch_setter  where entry_date between  '" & entrydate.Value.ToString & "' and  '" & entrydateto.Value.ToString & "'  and hatchery in ('HATCHERY NO-1','HATCHERY NO-3','HATCHERY NO-2') order by entry_date"
                gmod.DataSetRet(sql, "loading_report_")
            End If

            If RadioButton2.Checked = True Then
                sql = "select * from hatch_setter_sat  where entry_date between  '" & entrydate.Value.ToString & "' and  '" & entrydateto.Value.ToString & "' order by entry_date"
                gmod.DataSetRet(sql, "loading_report_")
            End If

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrLoadingDateReport.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("loading_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("p1", "Loadin Report From:" & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)
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

   

    Protected Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        RadioButton2.Checked = False
    End Sub

    Protected Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        RadioButton1.Checked = False
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            RadioButton1.Checked = True
            entrydate.Value = DateTime.Now.ToString("yyyy-MM-dd")
            entrydateto.Value = DateTime.Now.ToString("yyyy-MM-dd")
            fill()
        Else
            If Not Session("Report:" & Request.Path) Is Nothing Then
                CrystalReportViewer1.ReportSource = Session("Report:" & Request.Path)
            End If
        End If
    End Sub
End Class
