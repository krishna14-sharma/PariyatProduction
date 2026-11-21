Imports CrystalDecisions.CrystalReports.Engine

Partial Class attendance_Default
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    Protected Sub Page_Init(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then

        Else
            fill()
        End If
    End Sub

    Sub fill()
        Try

            gmod.SqlExecuteNonQuery("EXECUTE [SP_FillEmployeeMonthlyConsilData] " & year.Text & "")
            sql = "select * from v_empmonthltyleavedata where year = " & year.Text & ""
            gmod.DataSetRet(sql, "compile_leavedata")

            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("CrEmpMonthlyLeaveReport.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("compile_leavedata"))
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try

    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub
End Class
