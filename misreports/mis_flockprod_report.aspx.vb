Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_mis_flockprod_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim day As String
    Dim breed As String = ""
    Dim molper As Double = 0.0
    Dim molperm As Double = 0.0
    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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

    Sub fill()
        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        Try
            'exec [shedwise_report] '10/14/22', 'DHR-1'
            'exec [flockwise_report] '08/20/23', '08/20/23'
            If chklayer.Checked = True Then
                breed = "BV300"
                molper = 0.12
                molperm = 0.24
            ElseIf chkbroiler.Checked = True Then
                breed = "COBB430"
                molper = 0.2
                molperm = 0.4
            ElseIf chkgrower.Checked = False Then
                breed = "COBB430"
                molper = 0.2
                molperm = 0.4
            Else
                breed = "COBB430"
                molper = 0.2
                molperm = 0.4
            End If

            gmod.SqlExecuteNonQuery("delete from tmp_flock_daily_report")
            gmod.SqlExecuteNonQuery("exec [flockwise_report_gpt] '" & entrydate.Value.ToString & "', '" & entrydate.Value.ToString & "','" & breed & "'")
            ' gmod.SqlExecuteNonQuery("update  up_mort  set Mor_Female_Std = ((fqty*.18)/100)/7 , Mor_Male_Std =  (mqty*.3)/700")
            gmod.SqlExecuteNonQuery("update tmp_flock_daily_report set Mor_Female_Std = (Stock_F*" & molper & "/100)/7 , Mor_Male_Std = (Stock_M*" & molperm & "/100)/7  ")

            If chklayer.Checked = True Then
                sql = "select * from tmp_flock_daily_report where Age >=24 and  bird_type_code='BV300' order by left(Flock,3)  "
                breed = "LAYER"
                gmod.DataSetRet(sql, "flock_daily_report_")
            ElseIf chkbroiler.Checked = True Then
                sql = "select * from tmp_flock_daily_report where Age >=24 and  bird_type_code='COBB430' and (Stock_F> 0 or Stock_M>0) order by left(Flock,3) "
                breed = "BROILER"
                gmod.DataSetRet(sql, "flock_daily_report_")
            Else
                sql = "select * from tmp_flock_daily_report where Age >=24 and  bird_type_code='COBB430' and (Stock_F> 0 or Stock_M>0) order by left(Flock,3) "
                breed = "BROILER"
                gmod.DataSetRet(sql, "flock_daily_report_")
            End If
            ' gmod.DataSetRet(sql, "flock_daily_report_")

            'sql = "select Remark from daily_entry where shed_loc='" & areamaster.Text & "'   and entry_date='" & Now.ToShortDateString & "' and LEN(remark)> 0 Union Select 'Remarks'"
            'gmod.DataSetRet(sql, "daily_remark_")

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")

            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            '' crystalReport.Load(Server.MapPath("~/reports/CrShed_report.rpt")) CrShedFlockWise_report
            crystalReport.Load(Server.MapPath("CrFlockProdReport1.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("flock_daily_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("tday", "AS On:" & entrydate.Value.ToString)
            crystalReport.SetParameterValue("breed", breed)
            'crystalReport.SetParameterValue("remark", gmod.ds.Tables("daily_remark_").Rows(0)("remark"))
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport

            If chkgrower.Checked = True Then
                sql = "select * from tmp_flock_daily_report where Age <24 order by left(Flock,3)  asc"
                gmod.DataSetRet(sql, "flock_daily_report_")
                Dim crystalReportchicks As ReportDocument = CrystalReportManager.NewReport()
                '' crystalReport.Load(Server.MapPath("~/reports/CrShed_report.rpt")) CrShedFlockWise_report
                crystalReportchicks.Load(Server.MapPath("CrFlockChicksAndGrower.rpt"))
                crystalReportchicks.SetDataSource(gmod.ds.Tables("flock_daily_report_"))
                'crystalReport.Refresh()
                crystalReportchicks.SetParameterValue("tday", "AS On:" & entrydate.Value.ToString)

                'crystalReport.SetParameterValue("remark", gmod.ds.Tables("daily_remark_").Rows(0)("remark"))
                CrystalReportViewer1.ReportSource = crystalReportchicks
                Session("Report:" & Request.Path) = crystalReportchicks
            End If
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        Finally
            
        End Try
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        'fill1()
    End Sub

End Class
