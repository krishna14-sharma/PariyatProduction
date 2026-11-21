Imports CrystalDecisions.CrystalReports.Engine
Partial Class feed_finishfeedprod
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fill()
    End Sub
    Public Sub fill()

        Dim sql As String
        sql = "delete from tmp_stock_det"
        GlobalModule1.SqlExecuteNonQuery(sql)
        'inserting all the formula

        sql = " select formulaname,sum(actual_qty) actual_qty,sum(rem_qty) rem_qty,sum(no_bags) no_bags ,sum(wastage_qty)  wastage_qty,sum(rem_bags) rem_bags from stock_det" _
        & " where proddate between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' group by formulaname order by formulaname"
        GlobalModule1.DataSetRet(sql, "fwf")
       

        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        crystalReport.Load(Server.MapPath("CRFormulaWiseStock.rpt"))

        crystalReport.SetDataSource(GlobalModule1.ds.Tables("fwf"))
        'crystalReport.Refresh()
        crystalReport.SetParameterValue("subhead", "From " & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)

        CrystalReportViewer1.ReportSource = crystalReport

        Session("Report:" & Request.Path) = crystalReport
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fill()
    End Sub
End Class
