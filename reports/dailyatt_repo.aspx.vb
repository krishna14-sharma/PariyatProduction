Imports CrystalDecisions.CrystalReports.Engine

Partial Class reports_dailyatt_repo
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

    Sub fill()
        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        Try

            sql = "select * from daily_attendance where loc ='" & Session("area_code") & "' and [date] ='" & entrydate.Value.ToString & "'  and emptype ='P'"
            gmod.DataSetRet(sql, "daily_attrepo")


            sql = "select count(*) P from daily_attendance where loc= '" & Session("area_code") & "' and [date]='" & entrydate.Value.ToString & "' and absent =0 and leave = 0  and emptype ='P'"
            gmod.DataSetRet(sql, "P")
            sql = "select count(*) A from daily_attendance where loc= '" & Session("area_code") & "' and [date]='" & entrydate.Value.ToString & "' and absent =-1 and leave = 0 and emptype ='P'"
            gmod.DataSetRet(sql, "A")
            sql = "select count(*) L  from daily_attendance where loc= '" & Session("area_code") & "' and [date]='" & entrydate.Value.ToString & "' and absent =0 and leave = -1 and emptype ='P'"
            gmod.DataSetRet(sql, "L")

            crystalReport.Load(Server.MapPath("~/reports/CrDailtAreaAttandenceReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("daily_attrepo"))
            crystalReport.SetParameterValue("org", Session("location_name").ToString)
            crystalReport.SetParameterValue("P", gmod.ds.Tables("P").Rows(0)(0))
            crystalReport.SetParameterValue("A", gmod.ds.Tables("A").Rows(0)(0))
            crystalReport.SetParameterValue("L", gmod.ds.Tables("L").Rows(0)(0))
            crystalReport.SetParameterValue("D", entrydate.Value.ToString)
            crystalReport.SetParameterValue("e", "Monthly")

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
           
            AppUi.ShowError(Me, ex.Message)
        Finally
            
        End Try

    End Sub

    Protected Sub CrystalReportViewer1_Init(sender As Object, e As EventArgs) Handles CrystalReportViewer1.Init

    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
End Class
