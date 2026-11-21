Imports CrystalDecisions.CrystalReports.Engine
Partial Class reports_Flock_Weekly_report
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim bdwtf As String
    Dim bdwtm As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True


    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    
    Sub getBodyWt()

        sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
        sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "') and isAlive=1"
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
        If Not Session("Report:" & Request.Path) Is Nothing Then
            Dim oldRpt As ReportDocument = CType(Session("Report:" & Request.Path), ReportDocument)
            oldRpt.Close()
            oldRpt.Dispose()
            Session("Report:" & Request.Path) = Nothing
        End If
        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        Try
            sql = "delete from [dbo].[tmp_wkly_shed_report] where [User_Name]='" & Session("user_name") & "'"
            gmod.SqlExecuteNonQuery(sql)

            Dim i As Integer
            sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
            sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "') and isAlive=1"
            gmod.DataSetRet(sql, "wrflkno")

            For i = 0 To gmod.ds.Tables("wrflkno").Rows.Count - 1
                sql = "exec wkk_shed_report '" & entrydate.Value.ToString & "' , '" & todate.Value.ToString & "' ,'" & gmod.ds.Tables("wrflkno").Rows(i)(0) & "' ,'" & Session("user_name") & "'"
                gmod.SqlExecuteNonQuery(sql)
            Next

            sql = "select * from [dbo].[tmp_wkly_shed_report] where User_Name='" & Session("user_name") & "'"
            gmod.DataSetRet(sql, "flock_tmp_weekly_report_")

            getBodyWt()

            ' sql = "select  body_wt_female, body_wt_male from [dbo].[daily_entry] where entry_date='11/17/22' and flock_no='157B' and shed_no='DHR-1-S18' "
            'gmod.DataSetRet(sql, "body_wt_act")

            'sql = "select body_wt_female_g,body_wt_male_g from [dbo].[Standard] where Age_Weeks='65' "
            'gmod.DataSetRet(sql, "body_wt_std")

            'Response.Write(gmod.ds.Tables("tmp_daily_report_shed").Rows.Count)
            ' birdtype.DataSource = gmod.ds.Tables("shed_loc")

            crystalReport.Load(Server.MapPath("~/reports/CrWeeklyShedReport.rpt"))

            crystalReport.SetDataSource(gmod.ds.Tables("flock_tmp_weekly_report_"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("datetofrom", "Date From:" & entrydate.Value.ToString & "To:" & todate.Value.ToString)
            crystalReport.SetParameterValue("flckno", "Flock Number:" & gmod.ds.Tables("flock_tmp_weekly_report_").Rows(0)("flock_no"))
            ' crystalReport.SetParameterValue("bd_wt_act", "Female Body Weight(KG) Actual: " & gmod.ds.Tables("body_wt_act").Rows(0)("body_wt_female") & "      Male Body Weight(KG) Actual: " & gmod.ds.Tables("body_wt_act").Rows(0)("body_wt_male"))
            crystalReport.SetParameterValue("bd_wt_std", "Female Body Weight Standard: " & bdwtf & "  Male Body Weight Standard: " & bdwtm)
            'crystalReport.SetParameterValue("bd_wt_act", "Female Body Weight(KG) Actual:")
            'crystalReport.SetParameterValue("bd_wt_std", "Female Body Weight(KG) Standard:")

            CrystalReportViewer1.ReportSource = crystalReport

            Session("Report:" & Request.Path) = crystalReport
        Catch ex As Exception
           
            AppUi.ShowError(Me, ex.Message)
        Finally
          
        End Try


    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            sql = "select  distinct flock_no from [dbo].[Shed_Entry_Master]   where shed_loc in ("
            sql &= " select AreaCode from [dbo].[Users] where UserName='" & Session("user_name") & "')"
            gmod.DataSetRet(sql, "wrflkno")

            flolckno.DataSource = gmod.ds.Tables("wrflkno")
            flolckno.DataTextField = "flock_no"
            flolckno.DataBind()
        Else
            ' 🔥 VERY IMPORTANT: Reload report from session
            If Not Session("Report") Is Nothing Then
                CrystalReportViewer1.ReportSource = Session("Report")
            End If

        End If
    End Sub

    
End Class
