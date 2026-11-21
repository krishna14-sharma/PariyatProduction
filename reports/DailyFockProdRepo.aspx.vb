Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.Data
Imports System.Data.SqlClient
Partial Class reports_DailyFockProdRepo
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
        ' birdtype.DataSource = gmod.ds.Tables("shed_loc")

        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        Try
            sql = "select * from [dbo].[tmp_flock_daily_report]"
            gmod.DataSetRet(sql, "flock_tmp_daily_report_")
            crystalReport.Load(Server.MapPath("~/reports/CrFlockProductionRepo.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("flock_tmp_daily_report_"))
            crystalReport.Refresh()
            CrystalReportViewer1.ReportSource = crystalReport
        Catch ex As Exception
            
            AppUi.ShowError(Me, ex.Message)
        Finally
            

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
