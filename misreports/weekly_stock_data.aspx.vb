Imports CrystalDecisions.CrystalReports.Engine
Partial Class misreports_weekly_stock_data
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim bdwtf As String
    Dim bdwtm As String

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Sub fill()
        Try
            sql = "select  Today,d.shed_no, d.flock_no, DATEDIFF(d,hatch_date,d.Today)/7 Weeks , (DATEDIFF(d,hatch_date,d.Today) % 7)+1 days, "
            sql &= " d.fqty, d.mqty "
            sql &= "from [daily_bird_stock] d "
            sql &= " inner join flock_master f on d.flock_no = f.flock_no "
            sql &= " where left(f.flock_no,3) ='" & flock.Text & "'  and  DATEDIFF(d,hatch_date,d.Today)/7 ='" & week.Text & "'"
            sql &= " and  (DATEDIFF(d,hatch_date,d.Today) % 7)+1 ='7'"
            gmod.DataSetRet(sql, "WEKLY_BIRD_STOCK")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/misreports/weekly_bird_stock.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("WEKLY_BIRD_STOCK"))
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try
    End Sub


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillFlock()
            fillWeeks()
        End If
    End Sub


    Sub fillFlock()
        sql = "select distinct left(flock_no,3) flock_no from Shed_Entry_Data where isAlive=1 and flock_no <>''"
        gmod.DataSetRet(sql, "con_flock")
        flock.DataSource = gmod.ds.Tables("con_flock")
        flock.DataTextField = "flock_no"
        flock.DataValueField = "flock_no"
        flock.DataBind()
    End Sub

    Sub fillWeeks()
        sql = "select distinct Age_Weeks from [dbo].[Standard_Summer]  where Age_Weeks>=22  order by Age_Weeks"
        gmod.DataSetRet(sql, "Age_Weeks")
        week.DataSource = gmod.ds.Tables("Age_Weeks")
        week.DataTextField = "Age_Weeks"
        week.DataValueField = "Age_Weeks"
        week.DataBind()

    End Sub
End Class
