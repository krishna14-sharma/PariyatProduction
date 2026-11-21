Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports System.Data
Imports System.Data.SqlClient
Partial Class reports_Daily_temperature_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.Load
        fill()
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
    Private Sub fill()

        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        'sql = "select * from [dbo].[daily_shed_temp] "
        sql = "select * from [dbo].[daily_shed_temp] where entry_date between '" & d1.Value & "' and '" & d2.Value & "'"
        sql &= " and location in ( select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
        ' gmod.DataSetRet(sql, "tmp_daily_report_shed")
        Dim dsRepo As New DataSet
        Dim adpRepo As New SqlDataAdapter(sql, gmod.Connstr)
        adpRepo.Fill(dsRepo)
        'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
        ' birdtype.DataSource = gmod.ds.Tables("shed_loc")


        Try
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/Crdailytemprature_repo.rpt"))
            crystalReport.SetDataSource(dsRepo.Tables(0))
            crystalReport.SetParameterValue("area", Session("location_name").ToString)
            crystalReport.SetParameterValue("d", "From:" & d1.Value.ToString & " To" & d2.Value.ToString)
            'rystalReport.Refresh()
            CrystalReportViewer1.ReportSource = crystalReport
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)

        Finally

        End Try

    End Sub

    Protected Sub btnMonthly_Click(sender As Object, e As EventArgs) Handles btnMonthly.Click

        'sql = "SELECT entry_date, MIN(shed_in) AS minshedin, MAX(shed_in) AS maxshedin, AVG(shed_in) AS avgshedin, MIN(shed_out) AS minshedout, MAX(shed_out) AS maxshedout, AVG(shed_out) AS avgshedout"
        'sql &= " FROM dbo.daily_shed_temp "
        'sql &= " WHERE  (location in ( select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')) AND (MONTH(entry_date) = " & Now.Month & ") "
        'sql &= " GROUP BY entry_date"


        'Dim dsRepoTempMonthly As New DataSet
        'Dim da As New SqlDataAdapter(sql, gmod.Connstr)
        'da.Fill(dsRepoTempMonthly)
        ''Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
        '' birdtype.DataSource = gmod.ds.Tables("shed_loc")
        'Dim crystalReport1 As New ReportDocument()
        'crystalReport1.Load(Server.MapPath("~/reports/CrMonthly_temp.rpt"))
        'crystalReport1.SetDataSource(dsRepoTempMonthly.Tables(0))
        'crystalReport1.SetParameterValue("a", Session("area_code"))

        '' crystalReport1.Refresh()
        'CrystalReportViewer1.ReportSource = crystalReport1
    End Sub

    Protected Sub CrystalReportViewer1_Init(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Init

    End Sub
End Class
