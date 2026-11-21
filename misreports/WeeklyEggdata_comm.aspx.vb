Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_WeeklyEggdata_comm
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
            If flock.Text <> "186" Then

                sql = "select Weeks,sum(comm_eggs) comm_eggs,sum(crack_eggs) crack_eggs, sum(leaker_eggs) leaker_eggs , sum(hatch_eggs) hatch_eggs,"
                sql &= " sum(total_eggs) total_eggs from ( "
                sql &= " select  d.shed_no, d.flock_no, DATEDIFF(d,hatch_date,d.entry_date)/7 Weeks , (DATEDIFF(d,hatch_date,d.entry_date) % 7)+1 days,"
                sql &= " comm_eggs+comm_eggs_small comm_eggs,crack_eggs, leakage_eggs+leakage_eggs_leaker leaker_eggs ,"
                sql &= " total_eggs, hatch_eggs  from daily_entry d  inner join flock_master f on d.flock_no = f.flock_no  "
                sql &= "  where left(f.flock_no,3) ='" & flock.Text & "'  and isAlive =1 and fqty>0 and total_eggs>0 ) p group by Weeks order by Weeks"
            Else
                sql = "select * from A186"
            End If

            gmod.DataSetRet(sql, "Weekly_egg_data")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/misreports/CrWeeklyEggData.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("Weekly_egg_data"))

            crystalReport.SetParameterValue("flock_no", flock.Text)

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
    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub

End Class
