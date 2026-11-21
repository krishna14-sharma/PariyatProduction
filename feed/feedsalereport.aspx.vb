Imports CrystalDecisions.CrystalReports.Engine

Partial Class feed_feedsalereport
    Inherits System.Web.UI.Page
    Dim sql As String
    Dim qyeryresult As String
    Dim GlobalModule1 As New GlobalModule
    Dim valiation As Boolean = True

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        fillreport()
    End Sub

    Public Sub fillreport()

        sql = "select Vou_type, Vou_no, AccCode, AccName, Station, ProductName, OutQty, Rate, Amount, OutQtyNos, BillNo, BillDate, InQty, InQtyNos, o.Cmp_id, Session, id, mrktrate, authr,  Prdunit, Packing, Insurance, Discount, o.ToFarm CrHead from OtherSaleData o left join ACCHEAD  a on o.acccode=a.account_code where billdate between '" & entrydate.Value.ToString & "' and '" & entrydateto.Value.ToString & "' and vou_type<>'Return'"


        GlobalModule1.DataSetRet(sql, "salerep")

        Dim crystalReport As ReportDocument = CrystalReportManager.NewReport()
        crystalReport.Load(Server.MapPath("CrSaleRep.rpt"))

        crystalReport.SetDataSource(GlobalModule1.ds.Tables("salerep"))
        crystalReport.SetParameterValue("p1", "Date From:" & entrydate.Value.ToString & " To " & entrydateto.Value.ToString)
        CrystalReportViewer1.ReportSource = crystalReport
    End Sub

    Protected Sub Page_PreInit(sender As Object, e As EventArgs) Handles Me.Load
        fillreport()
    End Sub
End Class
