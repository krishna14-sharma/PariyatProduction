Imports CrystalDecisions.CrystalReports.Engine
Partial Class reports_pm_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
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

            sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
            sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "') and isAlive=1"
            gmod.DataSetRet(sql, "wrflkno")


            sql = "select sum(pm_qtyf) f , sum(pm_qtym) m ,disease_name  from [dbo].[PM_data] "
            sql &= " where flock_no in (select  distinct flock_no from [dbo].[Shed_Entry_Master]  "
            sql &= " where shed_loc in ( select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')) and entry_date between '" & entrydate.Value.ToString & "' and '" & todate.Value.ToString & "' group by disease_name"
            gmod.DataSetRet(sql, "pmlistadata")

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrPMReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("pmlistadata"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ondata", "Date From:" & entrydate.Value.ToString & "To:" & todate.Value.ToString)
            crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("wrflkno").Rows(0)("flock_no"))

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message.ToString)
        End Try

    End Sub

   
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        Else
            ' 🔥 VERY IMPORTANT: Reload report from session
            If Not Session("Report:" & Request.Path) Is Nothing Then
                CrystalReportViewer1.ReportSource = Session("Report:" & Request.Path)
            End If
        End If
    End Sub
End Class
