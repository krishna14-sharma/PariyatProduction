Imports CrystalDecisions.CrystalReports.Engine
Partial Class reports_Chattan_report
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

            sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
            sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
            gmod.DataSetRet(sql, "wrflkno")


            sql = "select flock_no, sum(sep_fem) f , reason_female, sum(sep_male) m , reason_male  "
            sql &= "  from  [dbo].[daily_sepration] where flock_no in "
            sql &= "(select  distinct flock_no from [dbo].[Shed_Entry_Master]  "
            sql &= " where shed_loc in ( select AreaCode from [dbo].[Users] "
            sql &= " where UserName='" & Session("user_name") & "')) and entry_date between '" & entrydate.Value.ToString & "' and '" & todate.Value.ToString & "' group by reason_female,reason_male,flock_no "

            gmod.DataSetRet(sql, "chattan_data")


            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrChattanReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("chattan_data"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ondata", "Date From:" & entrydate.Value.ToString & "To:" & todate.Value.ToString)
            ' crystalReport.SetParameterValue("flockno", "Flock Number:" & gmod.ds.Tables("wrflkno").Rows(0)("flock_no"))

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try

    End Sub
End Class
