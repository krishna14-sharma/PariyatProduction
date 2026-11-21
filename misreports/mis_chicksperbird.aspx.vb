
Partial Class misreports_mis_chicksperbird
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim valiation As Boolean = True

    Public Overrides Sub VerifyRenderingInServerForm(control As Control)
        ' This will allow GridView export
    End Sub

    Protected Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If chkFlock.Checked = True Then
            sql = "EXEC sp_WeeklyPerforance_Report '" & flock.Text & "'"
            gmod.DataSetRet(sql, "chicksperbird1")

            'sql = "select * , FreshLoading+BLoading TotalLoading ,FreshChicks+BChicks TotalChicks from tmpWeeklyPerReport"


            sql = "SELECT [FlockNo], [Weeks], [TotalHatchEggs], [TotalCommEggs], [TotalCrackEggs], " &
                  "[TotalLeakerEggs], [TotalEggs], [TotalMortality], [FreshLoading], [FreshFertile], " &
                  "[FreshInfertile], [FreshHLoss], [FreshCulls], [FreshChicks], [FreshHatchability], " &
                  "[BLoading], [BFertile], [BInfertile], [BHLoss], [BCulls], [BChicks], [BHatchability], " &
                  "[HatcheryHHE], [HatcheryCrack], [HatcheryLeaker], [HatcherySale], " &
                  "FreshLoading + BLoading AS TotalLoading, " &
                  "FreshFertile + BFertile AS TotalFertile, " &
                  "FreshInfertile + BInfertile AS TotalInFertile, " &
                  "FreshChicks + BChicks AS TotalChicks, " &
                  "CAST(ISNULL(" &
                  "((FreshChicks + BChicks) * 100.0) / " &
                  "NULLIF((FreshLoading + BLoading), 0), " &
                  "0) AS DECIMAL(18,2)) AS Percentage " &
                  "FROM tmpWeeklyPerReport"
            gmod.DataSetRet(sql, "chicksperbird")
            GridView1.DataSource = gmod.ds.Tables("chicksperbird")
            GridView1.DataBind()
        ElseIf chkFlockGroup.Checked = True Then
            SQL = "EXEC [sp_WeeklyPerforance_Report_claud] '" & flock.Text & "'"
            gmod.DataSetRet(SQL, "chicksperbird1")

            'sql = "select * , FreshLoading+BLoading TotalLoading ,FreshChicks+BChicks TotalChicks from tmpWeeklyPerReport"


            SQL = "SELECT [FlockNo], [Weeks], [TotalHatchEggs], [TotalCommEggs], [TotalCrackEggs], " &
                  "[TotalLeakerEggs], [TotalEggs], [TotalMortality], [FreshLoading], [FreshFertile], " &
                  "[FreshInfertile], [FreshHLoss], [FreshCulls], [FreshChicks], [FreshHatchability], " &
                  "[BLoading], [BFertile], [BInfertile], [BHLoss], [BCulls], [BChicks], [BHatchability], " &
                  "[HatcheryHHE], [HatcheryCrack], [HatcheryLeaker], [HatcherySale], " &
                  "FreshLoading + BLoading AS TotalLoading, " &
                  "FreshFertile + BFertile AS TotalFertile, " &
                  "FreshInfertile + BInfertile AS TotalInFertile, " &
                  "FreshChicks + BChicks AS TotalChicks, " &
                  "CAST(ISNULL(" &
                  "((FreshChicks + BChicks) * 100.0) / " &
                  "NULLIF((FreshLoading + BLoading), 0), " &
                  "0) AS DECIMAL(18,2)) AS Percentage " &
                  "FROM tmpWeeklyPerReport"
            gmod.DataSetRet(SQL, "chicksperbird")
            GridView1.DataSource = gmod.ds.Tables("chicksperbird")
            GridView1.DataBind()

        End If
    End Sub

    Protected Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

       Response.Clear()
        Response.Buffer = True
        Response.AddHeader("content-disposition", "attachment;filename=ChickPerBird.xls")
        Response.Charset = ""
        Response.ContentType = "application/vnd.ms-excel"

        Dim sw As New System.IO.StringWriter()
        Dim hw As New System.Web.UI.HtmlTextWriter(sw)

        GridView1.AllowPaging = False

        If chkFlock.Checked = True Then
            sql = "EXEC sp_WeeklyPerforance_Report '" & flock.Text & "'"
        ElseIf chkFlockGroup.Checked = True Then
            sql = "EXEC sp_WeeklyPerforance_FlockGrouping '" & flock.Text & "'"
        End If

        gmod.DataSetRet(sql, "chicksperbird1")

        'sql = "select *, FreshLoading+BLoading TotalLoading, FreshChicks+BChicks TotalChicks from tmpWeeklyPerReport"

        SQL = "SELECT [FlockNo], [Weeks], [TotalHatchEggs], [TotalCommEggs], [TotalCrackEggs], " &
              "[TotalLeakerEggs], [TotalEggs], [TotalMortality], [FreshLoading], [FreshFertile], " &
              "[FreshInfertile], [FreshHLoss], [FreshCulls], [FreshChicks], [FreshHatchability], " &
              "[BLoading], [BFertile], [BInfertile], [BHLoss], [BCulls], [BChicks], [BHatchability], " &
              "[HatcheryHHE], [HatcheryCrack], [HatcheryLeaker], [HatcherySale], " &
              "FreshLoading + BLoading AS TotalLoading, " &
              "FreshFertile + BFertile AS TotalFertile, " &
              "FreshInfertile + BInfertile AS TotalInFertile, " &
              "FreshChicks + BChicks AS TotalChicks, " &
              "CAST(ISNULL(" &
              "((FreshChicks + BChicks) * 100.0) / " &
              "NULLIF((FreshLoading + BLoading), 0), " &
              "0) AS DECIMAL(18,2)) AS Percentage " &
              "FROM tmpWeeklyPerReport"
        gmod.DataSetRet(sql, "chicksperbird")

        GridView1.DataSource = gmod.ds.Tables("chicksperbird")
        GridView1.DataBind()

        GridView1.RenderControl(hw)

        Response.Output.Write(sw.ToString())
        Response.Flush()
        Response.End()
        

    End Sub
End Class
