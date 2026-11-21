Imports System.Data
Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Partial Class pit_pit_egg_coll_report
    Inherits System.Web.UI.Page

    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub showRepo_Click(sender As Object, e As EventArgs) Handles showRepo.Click
        sql = "select * from [dbo].[hatch_pit_egg_collection] where egg_coll_date between '" & entrydatefrom.Value & "' and '" & entrydateto.Value & "'"
        ' gmod.DataSetRet(sql, "tmp_daily_report_shed")
        Dim dsRepo As New DataSet
        Dim adpRepo As New SqlDataAdapter(sql, gmod.Connstr)
        adpRepo.Fill(dsRepo)


        'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
        ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        crystalReport.Load(Server.MapPath("CrPit_EggCollectionReport.rpt"))
        crystalReport.SetDataSource(dsRepo.Tables(0))
        crystalReport.Refresh()
        CrystalReportViewer1.ReportSource = crystalReport
    End Sub
End Class
