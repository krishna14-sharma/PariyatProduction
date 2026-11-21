Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.Data
Imports System.Data.SqlClient


Partial Class reports_Daily_trans_repo_shed
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

   
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        

        Try
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            sql = "select * from [dbo].[tmp_daily_report]"
            ' gmod.DataSetRet(sql, "tmp_daily_report_shed")
            Dim dsRepo As New DataSet
            Dim adpRepo As New SqlDataAdapter(sql, gmod.Connstr)
            adpRepo.Fill(dsRepo)
            crystalReport.Load(Server.MapPath("~/reports/CrDailyProd_Shed.rpt"))
            crystalReport.SetDataSource(dsRepo.Tables(0))
            crystalReport.Refresh()
            CrystalReportViewer1.ReportSource = crystalReport

        Catch ex As Exception
           
            AppUi.ShowError(Me, ex.Message)
        Finally

        End Try
       
    End Sub
End Class
