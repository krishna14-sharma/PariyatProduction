Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Chicks Per Bird report consolidated for a flock group (190 = 190 A + 190 B + 190 C + 190 D).
''' MIS Reports › Farm Reports › Chicks Per Bird (Flock Group).
''' </summary>
Partial Class misreports_ChicksPerBirdGroup
    Inherits System.Web.UI.Page

    Private Class Col
        Public Key As String
        Public Header As String
        Public Decimals As Integer
        Public Compact As Boolean
        Public IsKey As Boolean
        Public Sub New(ByVal key As String, ByVal header As String, ByVal decimals As Integer, ByVal compact As Boolean, Optional ByVal isKey As Boolean = False)
            Me.Key = key : Me.Header = header : Me.Decimals = decimals : Me.Compact = compact : Me.IsKey = isKey
        End Sub
    End Class

    ''' <summary>Columns of the weekly table: key, heading, decimals, shown in the short view, highlighted.</summary>
    Private Shared ReadOnly Columns As Col() = {
        New Col("Weeks", "Week", 0, True), New Col("FlocksWithData", "Sub-flocks", 0, True),
        New Col("TotalHatchEggs", "Hatching eggs", 0, True), New Col("TotalCommEggs", "Comm. eggs", 0, False),
        New Col("TotalCrackEggs", "Crack eggs", 0, False), New Col("TotalLeakerEggs", "Leaker eggs", 0, False),
        New Col("TotalEggs", "Total eggs", 0, True), New Col("TotalMortality", "Mortality", 0, True),
        New Col("FreshLoading", "Fresh loading", 0, False), New Col("FreshFertile", "Fresh fertile", 0, False),
        New Col("FreshInfertile", "Fresh infertile", 0, False), New Col("FreshHLoss", "Fresh H.loss", 0, False),
        New Col("FreshCulls", "Fresh culls", 0, False), New Col("FreshChicks", "Fresh chicks", 0, False),
        New Col("FreshHatchability", "Fresh hatch %", 2, False),
        New Col("BLoading", "B loading", 0, False), New Col("BFertile", "B fertile", 0, False),
        New Col("BInfertile", "B infertile", 0, False), New Col("BHLoss", "B H.loss", 0, False),
        New Col("BCulls", "B culls", 0, False), New Col("BChicks", "B chicks", 0, False),
        New Col("BHatchability", "B hatch %", 2, False),
        New Col("HatcheryHHE", "Hatchery HHE", 0, False), New Col("HatcheryCrack", "Hatchery crack", 0, False),
        New Col("HatcheryLeaker", "Hatchery leaker", 0, False), New Col("HatcherySale", "Hatchery sale", 0, False),
        New Col("TotalLoading", "Total loading", 0, True), New Col("TotalFertile", "Total fertile", 0, True),
        New Col("TotalInFertile", "Total infertile", 0, False), New Col("TotalChicks", "Total chicks", 0, True),
        New Col("FertilityPercent", "Fertility %", 2, True), New Col("Percentage", "Hatch %", 2, True, True),
        New Col("CumHatchEggs", "Cum. HE", 0, False), New Col("CumHEPerBird", "HE / bird (cum.)", 2, True),
        New Col("CumChicks", "Cum. chicks", 0, True), New Col("CumChicksPerBird", "Chicks / bird (cum.)", 2, True, True)}

    Private Class Report
        Public GroupName As String
        Public Flocks As DataTable
        Public PerFlock As New Dictionary(Of String, DataTable)()
        Public Weekly As DataTable
        Public Housed As Double
        Public HousedFromMaster As Boolean
        Public Problems As New List(Of String)()
        Public Seconds As Double
    End Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' Show Report and Export run the weekly procedure once per sub-flock, which can take a few minutes.
        ' They must be normal (full) postbacks: an AJAX postback gives up after 90 seconds and the page
        ' then simply stays empty.
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)
        If sm IsNot Nothing Then
            sm.RegisterPostBackControl(btnShow)
            sm.RegisterPostBackControl(btnExport)
            sm.AsyncPostBackTimeout = 600
        End If
        Server.ScriptTimeout = 900
        If Not IsPostBack AndAlso Request.QueryString("flock") IsNot Nothing Then
            txtFlock.Text = Request.QueryString("flock")
            ShowReport()
        End If
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click
        Try
            ShowReport()
        Catch ex As Exception
            Fail(ex, "Show")
        End Try
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Dim rep As Report
        Try
            rep = Build()
        Catch ex As Exception
            Fail(ex, "Export")
            Return
        End Try
        If rep Is Nothing Then Return
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = Encoding.UTF8
        Response.AddHeader("content-disposition", "attachment;filename=ChickPerBird_" & SafeName(rep.GroupName) & "_group.xls")
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())
        Response.Write(ExcelHtml(rep))
        Response.Flush()
        Response.End()
    End Sub

    ' ------------------------------------------------------------------ building the report

    ''' <summary>Finds the sub-flocks (first time for this flock number), runs the weekly report for each and adds them up.</summary>
    Private Function Build() As Report
        Dim baseFlock As String = txtFlock.Text.Trim()
        If baseFlock.Length = 0 Then
            ShowMessage("Enter a flock number, e.g. 190.", True)
            pnlResult.Visible = False
            Return Nothing
        End If

        Dim rep As New Report()
        Try
            rep.Flocks = ChicksPerBirdGroupData.FindSubFlocks(baseFlock)
        Catch ex As Exception
            Fail(ex, "FindSubFlocks")
            Return Nothing
        End Try
        If rep.Flocks.Rows.Count = 0 Then
            ShowMessage("No flock " & baseFlock & " or sub-flocks (" & baseFlock & " A, " & baseFlock & " B …) found in Flock Master.", True)
            pnlFlocks.Visible = False
            pnlResult.Visible = False
            ViewState("base") = Nothing
            Return Nothing
        End If

        ' New flock number: list its sub-flocks, all ticked. Same number again: keep the user's ticks.
        If Not String.Equals(Convert.ToString(ViewState("base")), baseFlock, StringComparison.OrdinalIgnoreCase) Then
            cblFlocks.Items.Clear()
            For Each r As DataRow In rep.Flocks.Rows
                Dim flock As String = Convert.ToString(r("Flock")).Trim()
                Dim item As New ListItem(flock, flock)
                item.Selected = True
                cblFlocks.Items.Add(item)
            Next
            ViewState("base") = baseFlock
        End If
        pnlFlocks.Visible = True

        Dim chosen As New List(Of String)()
        For Each item As ListItem In cblFlocks.Items
            If item.Selected Then chosen.Add(item.Value)
        Next
        If chosen.Count = 0 Then
            ShowMessage("Tick at least one sub-flock.", True)
            pnlResult.Visible = False
            Return Nothing
        End If

        ' housed females: typed, or the sum of the chosen sub-flocks in Flock Master
        Dim typed As Double
        If txtHoused.Text.Trim().Length > 0 AndAlso Double.TryParse(txtHoused.Text.Trim().Replace(",", ""), NumberStyles.Any, CultureInfo.InvariantCulture, typed) AndAlso typed > 0 Then
            rep.Housed = typed
        Else
            For Each r As DataRow In rep.Flocks.Rows
                If chosen.Contains(Convert.ToString(r("Flock")).Trim()) Then rep.Housed += ChicksPerBirdGroupData.Num(r("Females"))
            Next
            rep.HousedFromMaster = True
        End If

        rep.GroupName = baseFlock & " (" & String.Join(", ", chosen.ToArray()) & ")"
        Dim started As DateTime = DateTime.Now
        For Each flock As String In chosen
            Try
                rep.PerFlock(flock) = ChicksPerBirdGroupData.WeeklyForFlock(flock)
                If rep.PerFlock(flock).Rows.Count = 0 Then rep.Problems.Add(flock & ": no weekly figures")
            Catch ex As Exception
                rep.Problems.Add(flock & ": " & ex.Message)
                Try
                    Call New GModule().LogErrorToFile(ex.ToString(), "ChicksPerBirdGroup", flock, Convert.ToString(Session("user_name")))
                Catch
                End Try
            End Try
        Next
        rep.Weekly = ChicksPerBirdGroupData.Consolidate(rep.PerFlock, rep.Housed)
        rep.Seconds = (DateTime.Now - started).TotalSeconds
        Return rep
    End Function

    Private Sub ShowReport()
        Dim rep As Report = Build()
        If rep Is Nothing Then Return
        Dim info As String = "Sub-flocks: " & String.Join(", ", New List(Of String)(rep.PerFlock.Keys).ToArray()) &
                             " – " & rep.Weekly.Rows.Count & " weeks with figures, read in " & Math.Round(rep.Seconds, 1).ToString(CultureInfo.InvariantCulture) & " s."
        If rep.Problems.Count > 0 Then info &= " Note – " & String.Join("; ", rep.Problems.ToArray())
        ShowMessage(info, rep.PerFlock.Count = 0)
        If rep.Weekly.Rows.Count = 0 Then
            ShowMessage(info & " No weekly figures were returned by sp_WeeklyPerforance_Report for these sub-flocks.", True)
            pnlResult.Visible = False
            Return
        End If

        Dim t As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(rep.Weekly)
        pnlResult.Visible = True
        litGroupName.Text = HttpUtility.HtmlEncode(rep.GroupName)
        litPerBird.Text = If(rep.Housed > 0, N(t("TotalChicks") / rep.Housed, 2), "–")
        litHEPerBird.Text = If(rep.Housed > 0, N(t("TotalHatchEggs") / rep.Housed, 2), "–")
        litChicks.Text = N(t("TotalChicks"), 0)
        litLoading.Text = "from " & N(t("TotalLoading"), 0) & " eggs loaded"
        litHatch.Text = N(t("Percentage"), 2) & " %"
        litHoused.Text = If(rep.Housed > 0, N(rep.Housed, 0), "–")
        litHousedFrom.Text = If(rep.HousedFromMaster, "Flock Master (sum of sub-flocks)", "as typed")

        litWeekly.Text = WeeklyHtml(rep, chkAllColumns.Checked)
        litFlocks.Text = FlocksHtml(rep)
    End Sub

    ' ------------------------------------------------------------------ tables

    Private Function WeeklyHtml(ByVal rep As Report, ByVal allColumns As Boolean) As String
        Dim sb As New StringBuilder()
        sb.Append("<table class=""cpg-table""><thead><tr>")
        For Each c As Col In Columns
            If allColumns OrElse c.Compact Then sb.Append("<th").Append(If(c.IsKey, " class=""cpg-key""", "")).Append(">").Append(HttpUtility.HtmlEncode(c.Header)).Append("</th>")
        Next
        sb.Append("</tr></thead><tbody>")
        For Each r As DataRow In rep.Weekly.Rows
            sb.Append("<tr>")
            For Each c As Col In Columns
                If allColumns OrElse c.Compact Then sb.Append("<td").Append(If(c.IsKey, " class=""cpg-key""", "")).Append(">").Append(N(r(c.Key), c.Decimals)).Append("</td>")
            Next
            sb.Append("</tr>")
        Next
        ' totals: counts added up, % from the totals, cumulative figures = last week
        Dim t As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(rep.Weekly)
        Dim lastRow As DataRow = rep.Weekly.Rows(rep.Weekly.Rows.Count - 1)
        sb.Append("<tr class=""cpg-total"">")
        For Each c As Col In Columns
            If Not (allColumns OrElse c.Compact) Then Continue For
            Dim text As String
            If c.Key = "Weeks" Then
                text = "Total"
            ElseIf c.Key = "FlocksWithData" Then
                text = ""
            ElseIf c.Key.StartsWith("Cum") Then
                text = N(lastRow(c.Key), c.Decimals)
            ElseIf t.ContainsKey(c.Key) Then
                text = N(t(c.Key), c.Decimals)
            Else
                text = ""
            End If
            sb.Append("<td").Append(If(c.IsKey, " class=""cpg-key""", "")).Append(">").Append(text).Append("</td>")
        Next
        sb.Append("</tr></tbody></table>")
        Return sb.ToString()
    End Function

    Private Function FlocksHtml(ByVal rep As Report) As String
        Dim sb As New StringBuilder()
        sb.Append("<table class=""cpg-table""><thead><tr><th>Flock</th><th>Breed</th><th>Hatch date</th><th>Females (Flock Master)</th><th>Weeks</th>")
        sb.Append("<th>Total eggs</th><th>Hatching eggs</th><th>Total loading</th><th>Total chicks</th><th>Fertility %</th><th class=""cpg-key"">Hatch %</th><th>HE / bird</th><th class=""cpg-key"">Chicks / bird</th></tr></thead><tbody>")
        For Each r As DataRow In rep.Flocks.Rows
            Dim flock As String = Convert.ToString(r("Flock")).Trim()
            If Not rep.PerFlock.ContainsKey(flock) Then Continue For
            Dim weekly As DataTable = rep.PerFlock(flock)
            Dim t As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(weekly)
            Dim females As Double = ChicksPerBirdGroupData.Num(r("Females"))
            Dim weeks As Integer = 0
            For Each w As DataRow In weekly.Rows
                For Each c As String In ChicksPerBirdGroupData.CountColumns
                    If ChicksPerBirdGroupData.Num(w(c)) <> 0 Then weeks += 1 : Exit For
                Next
            Next
            sb.Append("<tr><td class=""cpg-text""><strong>").Append(HttpUtility.HtmlEncode(flock)).Append("</strong></td>")
            sb.Append("<td class=""cpg-text"">").Append(HttpUtility.HtmlEncode(Convert.ToString(r("Breed")))).Append("</td>")
            sb.Append("<td>").Append(If(Convert.IsDBNull(r("HatchDate")), "", CDate(r("HatchDate")).ToString("dd-MM-yyyy", CultureInfo.InvariantCulture))).Append("</td>")
            sb.Append("<td>").Append(N(females, 0)).Append("</td><td>").Append(weeks).Append("</td>")
            sb.Append("<td>").Append(N(t("TotalEggs"), 0)).Append("</td><td>").Append(N(t("TotalHatchEggs"), 0)).Append("</td>")
            sb.Append("<td>").Append(N(t("TotalLoading"), 0)).Append("</td><td>").Append(N(t("TotalChicks"), 0)).Append("</td>")
            sb.Append("<td>").Append(N(t("FertilityPercent"), 2)).Append("</td><td class=""cpg-key"">").Append(N(t("Percentage"), 2)).Append("</td>")
            sb.Append("<td>").Append(If(females > 0, N(t("TotalHatchEggs") / females, 2), "–")).Append("</td>")
            sb.Append("<td class=""cpg-key"">").Append(If(females > 0, N(t("TotalChicks") / females, 2), "–")).Append("</td></tr>")
        Next
        Dim g As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(rep.Weekly)
        sb.Append("<tr class=""cpg-total""><td>Group</td><td></td><td></td><td>").Append(N(rep.Housed, 0)).Append("</td><td>").Append(rep.Weekly.Rows.Count).Append("</td>")
        sb.Append("<td>").Append(N(g("TotalEggs"), 0)).Append("</td><td>").Append(N(g("TotalHatchEggs"), 0)).Append("</td>")
        sb.Append("<td>").Append(N(g("TotalLoading"), 0)).Append("</td><td>").Append(N(g("TotalChicks"), 0)).Append("</td>")
        sb.Append("<td>").Append(N(g("FertilityPercent"), 2)).Append("</td><td class=""cpg-key"">").Append(N(g("Percentage"), 2)).Append("</td>")
        sb.Append("<td>").Append(If(rep.Housed > 0, N(g("TotalHatchEggs") / rep.Housed, 2), "–")).Append("</td>")
        sb.Append("<td class=""cpg-key"">").Append(If(rep.Housed > 0, N(g("TotalChicks") / rep.Housed, 2), "–")).Append("</td></tr></tbody></table>")
        Return sb.ToString()
    End Function

    ''' <summary>Excel: the same column names as the single-flock export (FlockNo, Weeks, TotalHatchEggs …) plus the new ones.</summary>
    Private Function ExcelHtml(ByVal rep As Report) As String
        Dim sb As New StringBuilder()
        sb.Append("<html><head><meta charset=""utf-8""></head><body><table border=""1""><tr><th>FlockNo</th>")
        For Each c As Col In Columns
            sb.Append("<th>").Append(c.Key).Append("</th>")
        Next
        sb.Append("</tr>")
        For Each r As DataRow In rep.Weekly.Rows
            sb.Append("<tr><td style=""mso-number-format:'\@'"">").Append(HttpUtility.HtmlEncode(rep.GroupName)).Append("</td>")
            For Each c As Col In Columns
                Dim v As Object = r(c.Key)
                sb.Append("<td>").Append(If(Convert.IsDBNull(v), "", Math.Round(ChicksPerBirdGroupData.Num(v), c.Decimals).ToString(CultureInfo.InvariantCulture))).Append("</td>")
            Next
            sb.Append("</tr>")
        Next
        sb.Append("</table><br/><table border=""1""><tr><th>Flock</th><th>Females</th><th>TotalEggs</th><th>TotalHatchEggs</th><th>TotalLoading</th><th>TotalChicks</th><th>Percentage</th><th>ChicksPerBird</th></tr>")
        For Each pair As KeyValuePair(Of String, DataTable) In rep.PerFlock
            Dim t As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(pair.Value)
            Dim females As Double = 0
            For Each r As DataRow In rep.Flocks.Rows
                If Convert.ToString(r("Flock")).Trim() = pair.Key Then females = ChicksPerBirdGroupData.Num(r("Females"))
            Next
            sb.Append("<tr><td style=""mso-number-format:'\@'"">").Append(HttpUtility.HtmlEncode(pair.Key)).Append("</td><td>").Append(females.ToString(CultureInfo.InvariantCulture))
            sb.Append("</td><td>").Append(t("TotalEggs").ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(t("TotalHatchEggs").ToString(CultureInfo.InvariantCulture))
            sb.Append("</td><td>").Append(t("TotalLoading").ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(t("TotalChicks").ToString(CultureInfo.InvariantCulture))
            sb.Append("</td><td>").Append(t("Percentage").ToString(CultureInfo.InvariantCulture)).Append("</td><td>")
            sb.Append(If(females > 0, Math.Round(t("TotalChicks") / females, 2).ToString(CultureInfo.InvariantCulture), "")).Append("</td></tr>")
        Next
        Dim g As Dictionary(Of String, Double) = ChicksPerBirdGroupData.Totals(rep.Weekly)
        sb.Append("<tr><td><b>Group</b></td><td>").Append(rep.Housed.ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(g("TotalEggs").ToString(CultureInfo.InvariantCulture))
        sb.Append("</td><td>").Append(g("TotalHatchEggs").ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(g("TotalLoading").ToString(CultureInfo.InvariantCulture))
        sb.Append("</td><td>").Append(g("TotalChicks").ToString(CultureInfo.InvariantCulture)).Append("</td><td>").Append(g("Percentage").ToString(CultureInfo.InvariantCulture)).Append("</td><td>")
        sb.Append(If(rep.Housed > 0, Math.Round(g("TotalChicks") / rep.Housed, 2).ToString(CultureInfo.InvariantCulture), "")).Append("</td></tr></table></body></html>")
        Return sb.ToString()
    End Function

    ' ------------------------------------------------------------------ helpers

    Private Shared Function N(ByVal value As Object, ByVal decimals As Integer) As String
        If value Is Nothing OrElse Convert.IsDBNull(value) Then Return ""
        Dim d As Double = ChicksPerBirdGroupData.Num(value)
        If Double.IsNaN(d) OrElse Double.IsInfinity(d) Then Return "–"
        Return d.ToString("N" & decimals.ToString(CultureInfo.InvariantCulture), CultureInfo.InvariantCulture)
    End Function

    Private Shared Function SafeName(ByVal s As String) As String
        Dim sb As New StringBuilder()
        For Each ch As Char In s
            If Char.IsLetterOrDigit(ch) Then sb.Append(ch)
        Next
        Return If(sb.Length = 0, "flocks", sb.ToString())
    End Function

    Private Sub ShowMessage(ByVal text As String, ByVal isError As Boolean)
        litMessage.Text = "<div class=""cpg-msg " & If(isError, "is-error", "is-info") & """>" & HttpUtility.HtmlEncode(text) & "</div>"
    End Sub

    Private Sub Fail(ByVal ex As Exception, ByVal action As String)
        Try
            Call New GModule().LogErrorToFile(ex.ToString(), "ChicksPerBirdGroup", action, Convert.ToString(Session("user_name")))
        Catch
        End Try
        pnlResult.Visible = False
        ShowMessage("Could not read the data (" & ex.Message & "). The error has been logged.", True)
    End Sub

End Class
