Imports CrystalDecisions.CrystalReports.Engine
Partial Class dailyentryforms_Daily_Feedreceive
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim gmod As New GModule
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True
    Protected Sub btnshow_Click(sender As Object, e As EventArgs) Handles btnshow.Click
        '  sql = "select * from OtherSaleData where ToFarm='" & Session("area_code") & "' and BillDate='" & entrydate.Value.ToString & "'"
        ' GlobalModule1.DataSetRet(sql, "feed_dispatched")
        fillreport()
    End Sub


    Public Sub fillreport()
        sql = "select Vou_type, Vou_no, AccCode, AccName, Station, ProductName, OutQty, Rate, Amount, OutQtyNos, BillNo, BillDate, InQty, InQtyNos, o.Cmp_id, Session, id, mrktrate, authr,  Prdunit, Packing, Insurance, Discount, o.ToFarm CrHead from OtherSaleData o left join ACCHEAD  a on o.acccode=a.account_code where billdate = '" & entrydate.Value.ToString & "' and ToFarm='" & Session("area_code") & "'"
        GlobalModule1.DataSetRet(sql, "salerep")
        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        crystalReport.Load(Server.MapPath("CrSaleRep.rpt"))
        crystalReport.SetDataSource(GlobalModule1.ds.Tables("salerep"))
        crystalReport.SetParameterValue("p1", "Date:" & entrydate.Value.ToString)
        CrystalReportViewer1.ReportSource = crystalReport
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fillreport()
    End Sub

End Class
