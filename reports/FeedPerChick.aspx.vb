Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Feed per chick, flock wise. Used by reports/FeedPerChick.aspx (Administration › Reports)
''' and misreports/FeedPerChick.aspx (MIS Reports › Farm Reports). Read-only.
''' </summary>
Partial Class reports_FeedPerChick
    Inherits System.Web.UI.Page

    Private Const IsoDate As String = "yyyy-MM-dd"
    Private Const ShowDate As String = "dd-MM-yyyy"
    Private Const MaleFeedColumn As Integer = 4
    Private _totals As Dictionary(Of String, Double)

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' The Excel download must be a normal (non-AJAX) postback.
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)
        If sm IsNot Nothing Then sm.RegisterPostBackControl(btnExport)

        If Session("user_name") Is Nothing Then
            Response.Redirect("~/loginforms/index.html", True)
        End If

        If Not IsPostBack Then
            ' default: this month so far (from the 1st; on the 1st, the whole of last month)
            Dim today As Date = DateTime.Today
            Dim fromDate As Date = New Date(today.Year, today.Month, 1)
            If today.Day = 1 Then fromDate = fromDate.AddMonths(-1)
            txtFrom.Text = fromDate.ToString(IsoDate, CultureInfo.InvariantCulture)
            txtTo.Text = today.ToString(IsoDate, CultureInfo.InvariantCulture)
            Try
                LoadLists()
                ShowReport()
            Catch ex As Exception
                ShowFailure(ex, "Page_Load")
            End Try
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click
        Try
            ShowReport()
        Catch ex As Exception
            ShowFailure(ex, "Show")
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Dim fromDate As Date, toDate As Date
        If Not ReadDates(fromDate, toDate) Then Return
        Dim dt As DataTable
        Try
            dt = LoadData(fromDate, toDate)
        Catch ex As Exception
            ShowFailure(ex, "Export")
            Return
        End Try
        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = Encoding.UTF8
        Response.AddHeader("Content-Disposition", "attachment; filename=FeedPerChick_" & toDate.ToString("yyyyMMdd", CultureInfo.InvariantCulture) & ".xls")
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())
        Dim weekly As DataTable = FeedPerChickData.LoadWeekly(fromDate, toDate, ddlFarm.SelectedValue, ddlFlock.SelectedValue, WholeLife, chkMale.Checked)
        Response.Write(BuildExcel(dt, fromDate, toDate).Replace("</body></html>", WeeklyExcel(weekly) & "</body></html>"))
        Response.End()
    End Sub

    ' ------------------------------------------------------------------ loading

    Private Sub LoadLists()
        ddlFarm.Items.Clear()
        ddlFarm.Items.Add(New ListItem("All farms", ""))
        For Each r As DataRow In FeedPerChickData.Farms().Rows
            ddlFarm.Items.Add(New ListItem(Convert.ToString(r("Name")), Convert.ToString(r("Code"))))
        Next
        ddlFlock.Items.Clear()
        ddlFlock.Items.Add(New ListItem("All flocks", ""))
        For Each r As DataRow In FeedPerChickData.Flocks().Rows
            Dim text As String = Convert.ToString(r("Flock"))
            If Not Convert.IsDBNull(r("Breed")) AndAlso Convert.ToString(r("Breed")) <> "" Then text &= " – " & Convert.ToString(r("Breed"))
            If Not Convert.IsDBNull(r("HatchDate")) Then text &= " (hatch " & CDate(r("HatchDate")).ToString(ShowDate, CultureInfo.InvariantCulture) & ")"
            ddlFlock.Items.Add(New ListItem(text, Convert.ToString(r("Flock"))))
        Next
    End Sub

    Private ReadOnly Property WholeLife As Boolean
        Get
            Return rblBasis.SelectedValue <> "period"
        End Get
    End Property

    Private Function LoadData(ByVal fromDate As Date, ByVal toDate As Date) As DataTable
        Return FeedPerChickData.Load(fromDate, toDate, ddlFarm.SelectedValue, ddlFlock.SelectedValue, WholeLife, chkMale.Checked)
    End Function

    Private Function ReadDates(ByRef fromDate As Date, ByRef toDate As Date) As Boolean
        If Not Date.TryParseExact(txtFrom.Text, IsoDate, CultureInfo.InvariantCulture, DateTimeStyles.None, fromDate) OrElse
           Not Date.TryParseExact(txtTo.Text, IsoDate, CultureInfo.InvariantCulture, DateTimeStyles.None, toDate) Then
            ShowMessage("Please enter both dates.")
            Return False
        End If
        If fromDate > toDate Then
            ShowMessage("The From date is after the To date.")
            Return False
        End If
        Return True
    End Function

    Private Sub ShowReport()
        Dim fromDate As Date, toDate As Date
        pnlResult.Visible = False
        If Not ReadDates(fromDate, toDate) Then Return

        Dim dt As DataTable = LoadData(fromDate, toDate)
        _totals = FeedPerChickData.Totals(dt)
        pnlResult.Visible = True

        Dim scope As String = If(ddlFlock.SelectedValue <> "", "Flock " & HttpUtility.HtmlEncode(ddlFlock.SelectedValue),
                                 If(ddlFarm.SelectedValue <> "", "Flocks at " & HttpUtility.HtmlEncode(ddlFarm.SelectedItem.Text), "All flocks"))
        If WholeLife Then
            litPeriod.Text = "<strong>" & scope & "</strong> with feed entries between " & fromDate.ToString(ShowDate, CultureInfo.InvariantCulture) & " and " &
                             toDate.ToString(ShowDate, CultureInfo.InvariantCulture) & " · figures for the <strong>whole flock life up to " &
                             toDate.ToString(ShowDate, CultureInfo.InvariantCulture) & "</strong>" & If(chkMale.Checked, " · male feed included", " · female feed only")
        Else
            litPeriod.Text = "<strong>" & scope & "</strong> · figures for <strong>" & fromDate.ToString(ShowDate, CultureInfo.InvariantCulture) & " to " &
                             toDate.ToString(ShowDate, CultureInfo.InvariantCulture) & "</strong> only" & If(chkMale.Checked, " · male feed included", " · female feed only")
        End If

        litPerChick.Text = N(_totals("FeedPerChickKg"), 3)
        litPerSaleable.Text = N(_totals("FeedPerSaleableChickKg"), 3)
        litPerHE.Text = N(_totals("FeedPerHatchingEggG"), 1)
        litFeed.Text = N(_totals("FeedKg") / 1000.0, 2) & " t"
        litFeedSplit.Text = "female " & N(_totals("FemaleFeedKg") / 1000.0, 2) & " t" & If(chkMale.Checked, " · male " & N(_totals("MaleFeedKg") / 1000.0, 2) & " t", "")
        litChicks.Text = N(_totals("Chicks"), 0)
        litSaleable.Text = N(_totals("SaleableChicks"), 0) & " saleable"

        gvFlocks.Columns(MaleFeedColumn).Visible = chkMale.Checked
        gvFlocks.ShowFooter = dt.Rows.Count > 1
        gvFlocks.DataSource = dt
        gvFlocks.DataBind()
        litEmpty.Text = If(dt.Rows.Count = 0, "<div class=""fpc-empty"">No flock has feed entries between these dates.</div>", "")

        Dim weekly As DataTable = FeedPerChickData.LoadWeekly(fromDate, toDate, ddlFarm.SelectedValue, ddlFlock.SelectedValue, WholeLife, chkMale.Checked)
        litWeekly.Text = WeeklyHtml(dt, weekly)
    End Sub

    Protected Sub gvFlocks_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvFlocks.RowDataBound
        If e.Row.RowType <> DataControlRowType.Footer OrElse _totals Is Nothing Then Return
        Dim c As TableCellCollection = e.Row.Cells
        c(0).Text = "Total"
        c(3).Text = N(_totals("FemaleFeedKg"), 0)
        c(4).Text = N(_totals("MaleFeedKg"), 0)
        c(5).Text = N(_totals("FeedKg"), 0)
        c(6).Text = N(_totals("Eggs"), 0)
        c(7).Text = N(_totals("HatchingEggs"), 0)
        c(8).Text = N(_totals("Chicks"), 0)
        c(9).Text = N(_totals("SaleableChicks"), 0)
        c(10).Text = N(_totals("FeedPerEggG"), 1)
        c(11).Text = N(_totals("FeedPerHatchingEggG"), 1)
        c(12).Text = N(_totals("FeedPerChickKg"), 3)
        c(13).Text = N(_totals("FeedPerSaleableChickKg"), 3)
        c(14).Text = N(_totals("ChicksPerTonne"), 0)
    End Sub

    ' ------------------------------------------------------------------ week by week

    ' main figures first, details after
    Private Shared ReadOnly WeekCols As String() = {"FeedKg", "Chicks", "FeedPerChickKg", "CumFeedPerChickKg", "FeedPerHatchingEggG",
                                                     "FemaleFeedKg", "MaleFeedKg", "Eggs", "HatchingEggs", "SaleableChicks", "CumFeedKg", "CumChicks"}
    Private Shared ReadOnly WeekHeads As String() = {"Total feed (kg)", "Chicks", "Feed / chick (kg)", "Cum. feed / chick (kg)", "Feed / HE (g)",
                                                      "Female feed (kg)", "Male feed (kg)", "Eggs", "Hatching eggs", "Saleable chicks", "Cum. feed (kg)", "Cum. chicks"}

    Private Shared Function WeekDecimals(ByVal col As String) As Integer
        If col.EndsWith("ChickKg") Then Return 3
        If col.EndsWith("G") Then Return 1
        Return 0
    End Function

    ''' <summary>One collapsible table per flock (the first one open), weeks in order.</summary>
    Private Function WeeklyHtml(ByVal flocks As DataTable, ByVal weekly As DataTable) As String
        If weekly.Rows.Count = 0 Then Return "<div class=""fpc-empty"">No weekly figures.</div>"
        Dim sb As New StringBuilder()
        Dim first As Boolean = True
        For Each f As DataRow In flocks.Rows
            Dim flock As String = Convert.ToString(f("Flock"))
            Dim rows As DataRow() = weekly.Select("Flock = '" & flock.Replace("'", "''") & "'", "AgeWeek")
            If rows.Length = 0 Then Continue For
            Dim last As DataRow = rows(rows.Length - 1)
            sb.Append("<details class=""fpc-week""").Append(If(first, " open", "")).Append("><summary><strong>Flock ").Append(HttpUtility.HtmlEncode(flock)).Append("</strong>")
            sb.Append(" <span class=""fpc-muted"">").Append(HttpUtility.HtmlEncode(Convert.ToString(f("Breed")))).Append(" · weeks ").Append(rows(0)("AgeWeek")).Append("–").Append(last("AgeWeek"))
            sb.Append(" · cum. feed / chick ").Append(N(last("CumFeedPerChickKg"), 3)).Append(" kg</span></summary>")
            sb.Append("<div class=""fpc-table-wrap""><table class=""fpc-table""><thead><tr><th class=""fpc-left"">Age week</th><th class=""fpc-left"">Dates</th>")
            For i As Integer = 0 To WeekCols.Length - 1
                If WeekCols(i) = "MaleFeedKg" AndAlso Not chkMale.Checked Then Continue For
                sb.Append("<th").Append(If(WeekCols(i) = "CumFeedPerChickKg", " class=""fpc-key""", "")).Append(">").Append(HttpUtility.HtmlEncode(WeekHeads(i))).Append("</th>")
            Next
            sb.Append("</tr></thead><tbody>")
            For Each r As DataRow In rows
                sb.Append("<tr><td class=""fpc-left""><strong>").Append(r("AgeWeek")).Append("</strong></td><td class=""fpc-left fpc-muted"">")
                sb.Append(ShortDate(r("WeekFrom"))).Append(" – ").Append(ShortDate(r("WeekTo"))).Append("</td>")
                For Each c As String In WeekCols
                    If c = "MaleFeedKg" AndAlso Not chkMale.Checked Then Continue For
                    sb.Append("<td").Append(If(c = "CumFeedPerChickKg", " class=""fpc-key""", "")).Append(">").Append(N(r(c), WeekDecimals(c))).Append("</td>")
                Next
                sb.Append("</tr>")
            Next
            sb.Append("</tbody></table></div></details>")
            first = False
        Next
        Return sb.ToString()
    End Function

    Private Function WeeklyExcel(ByVal weekly As DataTable) As String
        Dim sb As New StringBuilder()
        sb.Append("<br/><table border=""1""><tr><th colspan=""").Append(WeekCols.Length + 4).Append(""">Week by week</th></tr><tr><th>Flock</th><th>Age week</th><th>From</th><th>To</th>")
        For Each h As String In WeekHeads
            sb.Append("<th>").Append(HttpUtility.HtmlEncode(h)).Append("</th>")
        Next
        sb.Append("</tr>")
        For Each r As DataRow In weekly.Rows
            sb.Append("<tr>")
            Cell(sb, r("Flock"), True) : Cell(sb, r("AgeWeek"), False) : Cell(sb, DateText(r("WeekFrom")), True) : Cell(sb, DateText(r("WeekTo")), True)
            For Each c As String In WeekCols
                Dim v As Object = r(c)
                Cell(sb, If(Convert.IsDBNull(v), v, CObj(Math.Round(FeedPerChickData.Num(v), WeekDecimals(c)))), False)
            Next
            sb.Append("</tr>")
        Next
        sb.Append("</table>")
        Return sb.ToString()
    End Function

    Private Shared Function ShortDate(ByVal v As Object) As String
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return ""
        Return CDate(v).ToString("dd-MM-yy", CultureInfo.InvariantCulture)
    End Function

    Private Shared Function DateText(ByVal v As Object) As String
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return ""
        Return CDate(v).ToString(ShowDate, CultureInfo.InvariantCulture)
    End Function

    ' ------------------------------------------------------------------ helpers

    ''' <summary>Number with thousands separators; blank when there is no value.</summary>
    Protected Function N(ByVal value As Object, ByVal decimals As Integer) As String
        If value Is Nothing OrElse Convert.IsDBNull(value) Then Return "–"
        Dim d As Double = FeedPerChickData.Num(value)
        If Double.IsNaN(d) OrElse Double.IsInfinity(d) Then Return "–"
        Return d.ToString("N" & decimals.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
    End Function

    Protected Function HtmlEncode(ByVal value As Object) As String
        Return HttpUtility.HtmlEncode(Convert.ToString(value))
    End Function

    Private Function BuildExcel(ByVal dt As DataTable, ByVal fromDate As Date, ByVal toDate As Date) As String
        Dim t As Dictionary(Of String, Double) = FeedPerChickData.Totals(dt)
        Dim sb As New StringBuilder()
        sb.Append("<html><head><meta charset=""utf-8""></head><body><table border=""1"">")
        sb.Append("<tr><th colspan=""15"">Feed per Chick – ").Append(If(WholeLife, "flock life up to " & toDate.ToString(ShowDate, CultureInfo.InvariantCulture),
                  fromDate.ToString(ShowDate, CultureInfo.InvariantCulture) & " to " & toDate.ToString(ShowDate, CultureInfo.InvariantCulture)))
        sb.Append(If(chkMale.Checked, " (male feed included)", " (female feed only)")).Append("</th></tr>")
        Dim heads As String() = {"Flock", "Breed", "Farm", "Age (wk)", "Female feed (kg)", "Male feed (kg)", "Total feed (kg)", "Eggs", "Hatching eggs",
                                 "Chicks", "Saleable chicks", "Feed / egg (g)", "Feed / HE (g)", "Feed / chick (kg)", "Feed / saleable chick (kg)", "Chicks / tonne feed"}
        sb.Append("<tr>")
        For Each h As String In heads
            sb.Append("<th>").Append(HttpUtility.HtmlEncode(h)).Append("</th>")
        Next
        sb.Append("</tr>")
        For Each r As DataRow In dt.Rows
            sb.Append("<tr>")
            Cell(sb, r("Flock"), True) : Cell(sb, r("Breed"), True) : Cell(sb, r("Farms"), True) : Cell(sb, r("AgeWeeks"), False)
            For Each col As String In New String() {"FemaleFeedKg", "MaleFeedKg", "FeedKg", "Eggs", "HatchingEggs", "Chicks", "SaleableChicks",
                                                    "FeedPerEggG", "FeedPerHatchingEggG", "FeedPerChickKg", "FeedPerSaleableChickKg", "ChicksPerTonne"}
                Cell(sb, Rounded(r(col), col), False)
            Next
            sb.Append("</tr>")
        Next
        sb.Append("<tr><td><b>Total</b></td><td></td><td></td><td></td>")
        For Each col As String In New String() {"FemaleFeedKg", "MaleFeedKg", "FeedKg", "Eggs", "HatchingEggs", "Chicks", "SaleableChicks",
                                                "FeedPerEggG", "FeedPerHatchingEggG", "FeedPerChickKg", "FeedPerSaleableChickKg", "ChicksPerTonne"}
            Dim v As Double = t(col)
            Cell(sb, If(Double.IsNaN(v), CObj(DBNull.Value), Rounded(v, col)), False)
        Next
        sb.Append("</tr></table></body></html>")
        Return sb.ToString()
    End Function

    Private Shared Function Rounded(ByVal v As Object, ByVal col As String) As Object
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return v
        Dim d As Double = FeedPerChickData.Num(v)
        If col.EndsWith("ChickKg") Then Return Math.Round(d, 3)
        If col.EndsWith("G") Then Return Math.Round(d, 1)
        Return Math.Round(d, 0)
    End Function

    Private Shared Sub Cell(ByVal sb As StringBuilder, ByVal v As Object, ByVal isText As Boolean)
        Dim s As String = If(v Is Nothing OrElse Convert.IsDBNull(v), "", Convert.ToString(v, CultureInfo.InvariantCulture))
        If isText Then
            sb.Append("<td style=""mso-number-format:'\@'"">").Append(HttpUtility.HtmlEncode(s)).Append("</td>")
        Else
            sb.Append("<td>").Append(HttpUtility.HtmlEncode(s)).Append("</td>")
        End If
    End Sub

    Private Sub ShowMessage(ByVal text As String)
        litMessage.Text = "<div class=""fpc-msg is-error"">" & HttpUtility.HtmlEncode(text) & "</div>"
    End Sub

    Private Sub ShowFailure(ByVal ex As Exception, ByVal action As String)
        Try
            Call New GModule().LogErrorToFile(ex.ToString(), "FeedPerChick", action, Convert.ToString(Session("user_name")))
        Catch
        End Try
        pnlResult.Visible = False
        ShowMessage("Could not read the data (" & ex.Message & "). The error has been logged.")
    End Sub

End Class
