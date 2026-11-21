Imports CrystalDecisions.CrystalReports.Engine

Partial Class misreports_mis_weelkyreport
    Inherits System.Web.UI.Page

    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True
    Dim bdwtf As String
    Dim bdwtm As String

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            fillAreaCode()
            For i = 0 To 100
                cmbAge.Items.Add(i.ToString)
            Next
        End If
    End Sub

    Protected Shared ReadOnly reportQueue As New Queue()

    Protected Shared Function CreateReport(reportClass As Type) As ReportClass
        Dim report As ReportClass = DirectCast(
            Activator.CreateInstance(reportClass), 
            ReportClass
        )

        SyncLock reportQueue
            reportQueue.Enqueue(report)
        End SyncLock

        Return report
    End Function

    Public Shared Function GetReport(reportClass As Type) As ReportClass

        SyncLock reportQueue
            If reportQueue.Count > 75 Then
                DirectCast(reportQueue.Dequeue(), ReportClass).Dispose()
            End If
        End SyncLock

        Return CreateReport(reportClass)

    End Function

    Public Sub fillAreaCode()
        sql = "select * from location_master where org_code='10'"
        gmod.DataSetRet(sql, "location_master")
        areamaster.DataSource = gmod.ds.Tables("location_master")
        areamaster.DataTextField = "location_code"
        areamaster.DataValueField = "location_code"
        areamaster.DataBind()
    End Sub

    Sub getBodyWt()

        sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc ='" & areamaster.Text & "' and isAlive=1"
        ' sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
        gmod.DataSetRet(sql, "flknoage")

        bdwtf = ""
        bdwtm = ""

        Dim age As Integer
        For i = 0 To gmod.ds.Tables("flknoage").Rows.Count - 1

            sql = "select  MONTH(hatch_date) mnth ,DATEDIFF(d,hatch_date,GETDATE())/7  from flock_master  where flock_no ='" & gmod.ds.Tables("flknoage").Rows(i)(0) & "'"
            gmod.DataSetRet(sql, "flockmonth")

            age = Val(gmod.ds.Tables("flockmonth").Rows(0)(1))
            If age > 66 Then
                age = 66

            End If

            Dim value As Integer = Integer.Parse(gmod.ds.Tables("flockmonth").Rows(0)(0))
            Select Case value
                Case 2, 3, 4, 5, 6, 7
                    ' Both 99 and 100 will end up here.
                    sql = "select body_wt_female_g, body_wt_male_g from Standard_Summer where Age_Weeks = '" & age & "'"
                    gmod.DataSetRet(sql, "bdwts")
                    bdwtf = bdwtf & " WEEK(" & gmod.ds.Tables("flockmonth").Rows(0)(1) & ") Std Wt: " & gmod.ds.Tables("bdwts").Rows(0)(0).ToString
                    bdwtm = bdwtm & " WEEK" & gmod.ds.Tables("flockmonth").Rows(0)(1) & ") Std Wt: " & gmod.ds.Tables("bdwts").Rows(0)(1).ToString
                Case 8, 9, 10, 11, 12, 1
                    sql = "select body_wt_female_g, body_wt_male_g from Standard_Winter where Age_Weeks = '" & age & "'"
                    gmod.DataSetRet(sql, "bdwtw")
                    bdwtf = bdwtf & " WEEK(" & gmod.ds.Tables("flknoage").Rows(i)(0) & ")  Std Wt:" & gmod.ds.Tables("bdwtw").Rows(0)(0).ToString
                    bdwtm = bdwtm & " WEEK(" & gmod.ds.Tables("flknoage").Rows(i)(0) & ")  Std Wt:" & gmod.ds.Tables("bdwtw").Rows(0)(1).ToString
            End Select
        Next
    End Sub


    Sub fill()
        Dim d, d1 As Integer
        Try
            sql = "delete from [dbo].[tmp_wkly_shed_report] where [User_Name]='" & Session("user_name") & "'"
            gmod.SqlExecuteNonQuery(sql)

            ' Dim i As Integer
            'sql = "select  distinct flock_no from [dbo].[Shed_Entry_Data]   where shed_loc = '" & areamaster.Text & "' and isAlive=1"
            'gmod.DataSetRet(sql, "wrflkno")

            d = Val(cmbAge.Text) * 7
            d1 = Val(cmbAge.Text) * 7 + 7
            sql = " select DATEADD(d, " & d & ",hatch_date) ,   DATEADD(d, " & d1 & ",hatch_date)  from flock_master where flock_no = '" & cmbFlock.Text & "'"
            gmod.DataSetRet(sql, "weekdate")


            ' For i = 0 To gmod.ds.Tables("wrflkno").Rows.Count - 1
            sql = "exec wkk_shed_report '" & gmod.ds.Tables("weekdate").Rows(0)(0) & "' , '" & gmod.ds.Tables("weekdate").Rows(0)(1) & "' ,'" & cmbFlock.Text & "' ,'" & Session("user_name") & "', '" & areamaster.Text & "'"
            gmod.SqlExecuteNonQuery(sql)
            'Next


            sql = "select * from [dbo].[tmp_wkly_shed_report] where User_Name='" & Session("user_name") & "'"
            gmod.DataSetRet(sql, "flock_tmp_weekly_report_")

            getBodyWt()

            ' sql = "select  body_wt_female, body_wt_male from [dbo].[daily_entry] where entry_date='11/17/22' and flock_no='157B' and shed_no='DHR-1-S18' "
            'gmod.DataSetRet(sql, "body_wt_act")

            'sql = "select body_wt_female_g,body_wt_male_g from [dbo].[Standard] where Age_Weeks='65' "
            'gmod.DataSetRet(sql, "body_wt_std")

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")

            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("~/reports/CrWeeklyShedReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("flock_tmp_weekly_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("datetofrom", "Date From:" & gmod.ds.Tables("weekdate").Rows(0)(0) & "To:" & gmod.ds.Tables("weekdate").Rows(0)(1))
            crystalReport.SetParameterValue("flckno", "Flock Number:" & gmod.ds.Tables("flock_tmp_weekly_report_").Rows(0)("flock_no"))
            ' crystalReport.SetParameterValue("bd_wt_act", "Female Body Weight(KG) Actual: " & gmod.ds.Tables("body_wt_act").Rows(0)("body_wt_female") & "      Male Body Weight(KG) Actual: " & gmod.ds.Tables("body_wt_act").Rows(0)("body_wt_male"))
            crystalReport.SetParameterValue("bd_wt_std", "Female Body Weight Standard: " & bdwtf & "  Male Body Weight Standard: " & bdwtm)
            'crystalReport.SetParameterValue("bd_wt_act", "Female Body Weight(KG) Actual:")
            'crystalReport.SetParameterValue("bd_wt_std", "Female Body Weight(KG) Standard:")

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
            AppUi.ShowError(Me, ex.Message)
        End Try


    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub

    Protected Sub areamaster_SelectedIndexChanged(sender As Object, e As EventArgs) Handles areamaster.SelectedIndexChanged
        Dim selectedArea As String = areamaster.SelectedValue
        sql = "select  distinct flock_no from [dbo].[Shed_Entry_Data]   where shed_loc = '" & selectedArea & "' and isAlive=1"
        gmod.DataSetRet(sql, "wrflkno")

        cmbFlock.DataSource = gmod.ds.Tables("wrflkno")
        cmbFlock.DataTextField = "flock_no"
        cmbFlock.DataBind()
    End Sub

   
End Class
