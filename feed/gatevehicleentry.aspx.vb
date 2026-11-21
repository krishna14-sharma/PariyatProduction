Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Partial Class feed_gatevehicleentry
    Inherits System.Web.UI.Page
    Dim QuertStr As String
    Dim ConStrFeed As String = "Data Source=192.168.0.130;Initial Catalog=phxfeed_2324;User ID=sa;Password=Ph@hoenix#g;Connect Timeout=60;"

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        '
        fillReport()
    End Sub


    Public Sub fillReport()
        QuertStr = "select * from MRN where  MRDate between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "'"
        Dim da As New SqlDataAdapter(QuertStr, ConStrFeed)
        Dim dsFeed As New DataSet
        da.Fill(dsFeed, "MRN")

        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()

        crystalReport.Load(Server.MapPath("CrMRNReport.rpt"))
        crystalReport.SetDataSource(dsFeed.Tables("MRN"))

        'crystalReport.SetParameterValue("cmpname", "Phoenix(BeejaPuri Unit)")
        crystalReport.SetParameterValue("d1", entrydate.Value.ToString)
        crystalReport.SetParameterValue("d2", entrydate.Value.ToString)
        'crystalReport.SetParameterValue("subhead1", "From " & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)
        CrystalReportViewer1.ReportSource = crystalReport

        Session("Report:" & Request.Path) = crystalReport
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fillReport()
    End Sub

End Class
