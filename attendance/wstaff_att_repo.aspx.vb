Imports CrystalDecisions.CrystalReports.Engine

Partial Class attendance_wstaff_att_repo
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True


    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'fillOrg()
            fillLocation()
            'fillGrid()
            ' lbldate.Text = DateTime.Now.ToString("yyyy-MM-dd")
        Else
        End If
    End Sub

    Public Sub fillLocation()
        sql = "select distinct loc from daily_attendance where emptype ='W'"
        gmod.DataSetRet(sql, "warea")
        warealist.DataSource = gmod.ds.Tables("warea")
        warealist.DataTextField = "loc"
        warealist.DataValueField = "loc"
        warealist.DataBind()
    End Sub

    Sub fill()
        Try

            sql = "select * from daily_attendance where loc ='" & warealist.SelectedValue & "' and [date] ='" & entrydate.Value.ToString & "'  and emptype ='W'"
            gmod.DataSetRet(sql, "daily_attrepo")


            sql = "select count(*) P from daily_attendance where loc= '" & warealist.SelectedValue & "' and [date]='" & entrydate.Value.ToString & "' and absent =0 and leave = 0 and emptype ='W'"
            gmod.DataSetRet(sql, "P")
            sql = "select count(*) A from daily_attendance where loc= '" & warealist.SelectedValue & "' and [date]='" & entrydate.Value.ToString & "' and absent =-1 and leave = 0 and emptype ='W'"
            gmod.DataSetRet(sql, "A")
            sql = "select count(*) L  from daily_attendance where loc= '" & warealist.SelectedValue & "' and [date]='" & entrydate.Value.ToString & "' and absent =0 and leave = -1 and emptype ='W'"
            gmod.DataSetRet(sql, "L")

            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrDailtAreaAttandenceReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("daily_attrepo"))
            crystalReport.SetParameterValue("org", Session("location_name").ToString)
            crystalReport.SetParameterValue("P", gmod.ds.Tables("P").Rows(0)(0))
            crystalReport.SetParameterValue("A", gmod.ds.Tables("A").Rows(0)(0))
            crystalReport.SetParameterValue("L", "0")
            crystalReport.SetParameterValue("D", entrydate.Value.ToString)
            crystalReport.SetParameterValue("e", "Weekly")

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
