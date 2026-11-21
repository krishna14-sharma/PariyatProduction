Imports CrystalDecisions.CrystalReports.Engine

Partial Class reports_hatchery_comparaionreport
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
        Dim i As Integer = 0
        gmod.SqlExecuteNonQuery("exec [hatchery_camparision_report1] '" & entrydate.Value & "' ,'1/13/24'")
        Dim age() As String
        sql = "select age from tmpComparsion where len(age)>0"
        gmod.DataSetRet(sql, "tmpcomparaiondate")
        For i = 0 To gmod.ds.Tables("tmpcomparaiondate").Rows.Count - 1
            age = gmod.ds.Tables("tmpcomparaiondate").Rows(i)(0).ToString.Split("-")
            'Response.Write(age(0))
            sql = "select Hatch_Per from [dbo].[Standard_Summer] where Age_Weeks = " & age(0) & " and Bird_breed_code='COBB430'"
            gmod.DataSetRet(sql, "agehper")
            sql = "update [tmpComparsion] set Standard = " & gmod.ds.Tables("agehper").Rows(0)(0).ToString & " where age = '" & gmod.ds.Tables("tmpcomparaiondate").Rows(i)(0).ToString & "'"
            gmod.SqlExecuteNonQuery(sql)
        Next


        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'

            sql = "select * from tmpComparsion where len(age)>0"
            gmod.DataSetRet(sql, "hatcherry_comaprsion_report_")


            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrHatcheryComparsionReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("hatcherry_comaprsion_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ld", entrydate.Value.ToString)
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

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
End Class
