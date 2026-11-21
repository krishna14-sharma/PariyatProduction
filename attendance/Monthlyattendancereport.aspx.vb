Imports CrystalDecisions.CrystalReports.Engine
Imports System.Globalization
Imports System.Data.SqlClient

Partial Class attendance_Monthlyattendancereport
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
        ' sql = "select distinct loc from daily_attendance where emptype ='W'"
        sql = "select distinct AreaCode from EmpMaster order by AreaCode"
        gmod.DataSetRet(sql, "warea")
        warealist.DataSource = gmod.ds.Tables("warea")
        warealist.DataTextField = "AreaCode"
        warealist.DataValueField = "AreaCode"
        warealist.DataBind()
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub


    Sub fill()
        Try
            gmod.DataSetRet("delete from monthly_attendance", "monthly_attrepo_deleyte")
            ' If Not IsPostBack Then
            Dim sql As String =
            "INSERT INTO [dbo].[monthly_attendance] "
            sql &= " ([empcode], [loc], [month], [year], [P], [L], [A], [S]) "
            sql &= " SELECT  empcode,  @loc, MONTH(@monthDate) AS [month],"
            sql &= " YEAR(@monthDate) AS [year],"
            sql &= " SUM(CASE WHEN absent = 0 AND [leave] = 0 THEN 1 ELSE 0 END) AS P,"
            sql &= " SUM(CASE WHEN absent = 0 AND [leave] = -1 THEN 1 ELSE 0 END) AS L,"
            sql &= " SUM(CASE WHEN absent = -1 AND [leave] = 0 THEN 1 ELSE 0 END) AS A,"
            sql &= " 0 AS S"
            sql &= " FROM daily_attendance "
            sql &= " WHERE loc = @loc  AND [date] BETWEEN @fromDate AND @toDate GROUP BY empcode"

            Using con As New SqlConnection(gmod.Connstr)
                Using cmd As New SqlCommand(sql, con)
                    ' Set parameters
                    cmd.Parameters.AddWithValue("@loc", warealist.Text)
                    cmd.Parameters.AddWithValue("@monthDate", entrydate1.Value)
                    cmd.Parameters.AddWithValue("@fromDate", entrydate.Value)
                    cmd.Parameters.AddWithValue("@toDate", entrydate1.Value)

                    con.Open()
                    Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
                    con.Close()

                    ' Optional: Show a message or log result
                    'System.Diagnostics.Debug.WriteLine(rowsAffected & " records inserted into monthly_attendance.")
                End Using
            End Using


            'End If
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try







        Dim m As String = ""
        Dim y As String = ""
        Try

            Dim dateString As String = entrydate1.Value

            Dim myDate As DateTime
            If DateTime.TryParseExact(dateString, "M/d/yy", CultureInfo.InvariantCulture, DateTimeStyles.None, dateString) Then
                Dim monthName1 As String = myDate.ToString("MMMM", CultureInfo.InvariantCulture)
                Dim year1 As Integer = myDate.Year
                m = monthName1
                y = year1.ToString
            Else
            End If



            sql = "  select * from V_monthly_attendance where AreaCode='" & warealist.Text & "' and [month]=month('" & entrydate.Value & "') and [year] = year('" & entrydate1.Value & "')"
            gmod.DataSetRet(sql, "monthly_attrepo")

            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("CrMantylyAttReport.rpt"))
            crystalReport.SetDataSource(gmod.ds.Tables("monthly_attrepo"))
            crystalReport.SetParameterValue("loc", warealist.Text)
            crystalReport.SetParameterValue("m", m)
            crystalReport.SetParameterValue("y", y)
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            If IsPostBack Then
                AppLog.Write(ex, "Report")
                AppUi.ShowError(Me, "Could not show the report: " & ex.Message)
            End If
        End Try

    End Sub

    Protected Sub Page_PreRender(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub

End Class
