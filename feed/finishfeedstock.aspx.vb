Imports CrystalDecisions.CrystalReports.Engine

Partial Class feed_finishfeedstock
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub

    Public Sub fill()
        'Dim sql As String
        'sql = "exec FinishedStock '" & entrydate.Value.ToString & "'"     
        'GlobalModule1.DataSetRet(sql, "ffs")

        'Dim crystalReport As New ReportDocument()
        'crystalReport.Load(Server.MapPath("CrFinishFeedStockReport.rpt"))

        'crystalReport.SetDataSource(GlobalModule1.ds.Tables("ffs"))
        ''crystalReport.Refresh()
        'crystalReport.SetParameterValue("d", "As On date:" & entrydate.Value.ToString)

        'CrystalReportViewer1.ReportSource = crystalReport

        'Session("Report") = crystalReport



        Dim sql As String
        sql = "exec sp_FinishedFeedReport '" & entrydate.Value.ToString & "', '2025'"
        GlobalModule1.SqlExecuteNonQuery(sql)

        If ComboBox1.Text = "SELF CONSUME" Then
            'sql = "select * from tmpfisnhedstock where formulaname  NOT like '%COM.%' AND  formulaname  NOT like '%COM-%'"
            sql = "select * from tmpfisnhedstock where formulaname  NOT like '%COM.%' AND  balanceqty>0 and formulaname  NOT like '%COM-%'"
            GlobalModule1.DataSetRet(sql, "ffstockrep")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("CrFinishFeedStockReport.rpt"))

            crystalReport.SetDataSource(GlobalModule1.ds.Tables("ffstockrep"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("d", "As On date:" & entrydate.Value.ToString)
            crystalReport.SetParameterValue("d1", Combobox1.Text)
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport


        ElseIf ComboBox1.Text = "COMMERCIAL FEED" Then
            'sql = "select * from tmpfisnhedstock where formulaname  like '%COM.%' or formulaname  like '%COM-%'"
            sql = "select * from tmpfisnhedstock where  balanceqty>0 and  formulaname  like '%COM.%' or formulaname  like '%COM-%'"

            GlobalModule1.DataSetRet(sql, "ffstockrep")
            Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
            crystalReport.Load(Server.MapPath("CrFinishFeedStockReport.rpt"))

            crystalReport.SetDataSource(GlobalModule1.ds.Tables("ffstockrep"))
            'crystalReport.Refresh()
            crystalReport.SetParameterValue("d", "As On date:" & entrydate.Value.ToString)
            crystalReport.SetParameterValue("d1", Combobox1.Text)
            CrystalReportViewer1.ReportSource = crystalReport
            Session("Report:" & Request.Path) = crystalReport
        End If
       
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub
End Class
