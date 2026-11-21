Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_mis_pmreport
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillAreaCode()
        End If
    End Sub

    Public Sub fillAreaCode()
        sql = "select * from location_master where org_code='10'"
        gmod.DataSetRet(sql, "location_master")
        areamaster.DataSource = gmod.ds.Tables("location_master")
        areamaster.DataTextField = "location_code"
        areamaster.DataValueField = "location_code"
        areamaster.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Sub fill()
        Try

            ' sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
            'sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
            'gmod.DataSetRet(sql, "wrflkno")


            ' sql = "select sum(pm_qtyf) f , sum(pm_qtym) m ,disease_name  from [dbo].[PM_data] "
            'sql &= " where flock_no in (select  distinct flock_no from [dbo].[Shed_Entry_Master]  "
            'sql &= " where shed_loc ='" & areamaster.Text & "'  ) and entry_date between '" & entrydate.Value.ToString & "' and '" & todate.Value.ToString & "' group by disease_name"

            If txtFlock.Text = "" Then
                sql = "select sum(pm_qtyf) f , sum(pm_qtym) m ,disease_name  from [dbo].[PM_data] "
                sql &= " where flock_no = '" & flolckno.Text & "'  and entry_date between '" & entrydate.Value.ToString & "' and '" & todate.Value.ToString & "' group by disease_name"
                gmod.DataSetRet(sql, "pmlistadata")
            Else

                sql = "select sum(pm_qtyf) f , sum(pm_qtym) m ,disease_name  from [dbo].[PM_data] "
                sql &= " where flock_no like '%" & txtFlock.Text & "%'  and entry_date between '" & entrydate.Value.ToString & "' and '" & todate.Value.ToString & "' group by disease_name"
                gmod.DataSetRet(sql, "pmlistadata")

            End If

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrPMReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("pmlistadata"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("ondata", "Date From:" & entrydate.Value.ToString & "To:" & todate.Value.ToString)
            crystalReport.SetParameterValue("flockno", areamaster.Text + " " + flolckno.Text)

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try

    End Sub

    Protected Sub areamaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles areamaster.SelectedIndexChanged
        sql = "select distinct flock_no from Shed_Entry_Master where shed_loc='" & areamaster.Text & "' and isAlive = 1"
        gmod.DataSetRet(sql, "pmflock")
        flolckno.DataSource = gmod.ds.Tables("pmflock")
        flolckno.DataTextField = "flock_no"
        flolckno.DataBind()

    End Sub
End Class
