Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Globalization
Imports System.IO
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

''' <summary>
''' Vaccination &amp; medication schedule for a flock (male and female have their own standard schedules).
''' Due dates come from the hatch date; the user records the date each item was given.
''' Given after the due date = late: a reason is required. Administration menu page (all flocks).
''' </summary>
Partial Class transforms_VaccinationSchedule
    Inherits System.Web.UI.Page

    Private Const IsoDate As String = "yyyy-MM-dd"
    Private _items As List(Of VaccinationItem)

    Private ReadOnly Property UserName As String
        Get
            Return Convert.ToString(Session("user_name"))
        End Get
    End Property

    ' ================================================================== page events

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ' The Excel download must be a normal (non-AJAX) postback.
        Dim sm As ScriptManager = ScriptManager.GetCurrent(Page)
        If sm IsNot Nothing Then sm.RegisterPostBackControl(btnExport)

        If Session("user_name") Is Nothing Then
            Response.Redirect("~/loginforms/index.html", True)
        End If

        If Not IsPostBack Then
            Try
                LoadFlocks()
                LoadDueList()
                If ddlFlock.Items.Count > 0 AndAlso ddlFlock.SelectedValue <> "" Then
                    LoadHatchDate()
                    ShowSchedule()
                End If
            Catch ex As Exception
                ShowFailure(ex, "Load")
            End Try
        End If
    End Sub

    Protected Sub ddlFlock_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlFlock.SelectedIndexChanged
        LoadHatchDate()
        ShowSchedule()
    End Sub

    Protected Sub rblSex_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblSex.SelectedIndexChanged
        LoadHatchDate()
        ShowSchedule()
    End Sub

    Protected Sub btnShow_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnShow.Click
        ShowSchedule()
    End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSave.Click, btnSaveBottom.Click
        SaveSchedule()
    End Sub

    Protected Sub lnkClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles lnkClose.Click
        If ddlFlock.SelectedValue = "" Then Return
        Try
            VaccinationData.CloseTracking(ddlFlock.SelectedValue, rblSex.SelectedValue, UserName)
            LoadDueList()
            ShowMessage("success", "Flock " & ddlFlock.SelectedValue & " (" & VaccinationData.SexName(rblSex.SelectedValue) & ") is no longer shown in the due list.")
        Catch ex As Exception
            ShowFailure(ex, "CloseTracking")
        End Try
    End Sub

    Protected Sub gvDue_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles gvDue.RowCommand
        If e.CommandName <> "OpenFlock" Then Return
        Dim index As Integer = Convert.ToInt32(e.CommandArgument, CultureInfo.InvariantCulture)
        Dim flockNo As String = Convert.ToString(gvDue.DataKeys(index)("flock_no"))
        Dim sex As String = Convert.ToString(gvDue.DataKeys(index)("sex"))
        Dim item As ListItem = ddlFlock.Items.FindByValue(flockNo)
        If item Is Nothing Then
            ddlFlock.Items.Add(New ListItem(flockNo, flockNo))
            item = ddlFlock.Items.FindByValue(flockNo)
        End If
        ddlFlock.ClearSelection()
        item.Selected = True
        rblSex.SelectedValue = sex
        LoadHatchDate()
        ShowSchedule()
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExport.Click
        Dim hatch As Nullable(Of Date) = ParseIso(txtHatch.Text)
        If ddlFlock.SelectedValue = "" OrElse Not hatch.HasValue Then
            ShowMessage("error", "Select a flock and hatch date first.")
            Return
        End If
        Dim items As List(Of VaccinationItem) = VaccinationData.GetSchedule(ddlFlock.SelectedValue, rblSex.SelectedValue, hatch.Value)
        Dim fileName As String = "Vaccination_" & SafeFileName(ddlFlock.SelectedValue) & "_" & VaccinationData.SexName(rblSex.SelectedValue) & ".xls"
        Response.Clear()
        Response.ContentType = "application/vnd.ms-excel"
        Response.ContentEncoding = Encoding.UTF8
        Response.AddHeader("Content-Disposition", "attachment; filename=" & fileName)
        Response.BinaryWrite(Encoding.UTF8.GetPreamble())
        Response.Write(BuildExcel(ddlFlock.SelectedValue, rblSex.SelectedValue, hatch.Value, items))
        Response.End()
    End Sub

    ' ================================================================== loading

    Private Sub LoadFlocks()
        ddlFlock.Items.Clear()
        Dim flocks As DataTable = VaccinationData.GetFlocks(Nothing)
        For Each row As DataRow In flocks.Rows
            Dim flockNo As String = Convert.ToString(row("flock_no"))
            Dim text As String = flockNo
            If Not Convert.IsDBNull(row("hatch_date")) Then text &= "  (hatch " & CDate(row("hatch_date")).ToString("dd-MM-yyyy") & ")"
            ddlFlock.Items.Add(New ListItem(text, flockNo))
        Next
        If ddlFlock.Items.Count = 0 Then
            ddlFlock.Items.Add(New ListItem("No flocks found", ""))
        End If
    End Sub

    Private Sub LoadHatchDate()
        litHatchNote.Text = ""
        If ddlFlock.SelectedValue = "" Then
            txtHatch.Text = ""
            Return
        End If
        Dim hatch As Nullable(Of Date) = VaccinationData.GetHatchDate(ddlFlock.SelectedValue, rblSex.SelectedValue)
        txtHatch.Text = If(hatch.HasValue, hatch.Value.ToString(IsoDate, CultureInfo.InvariantCulture), "")
    End Sub

    Private Sub LoadDueList()
        Dim due As DataTable = SummariseDue(VaccinationData.GetDueList(Nothing))
        gvDue.DataSource = due
        gvDue.DataBind()
        gvDue.Visible = due.Rows.Count > 0
        litDueHint.Text = HttpUtility.HtmlEncode("Flocks with vaccines not yet given that are overdue or due within " & VaccinationData.DueSoonDays & " days (flocks whose schedule has been saved).")
        litDueEmpty.Text = If(due.Rows.Count = 0, "<p class=""vacc-muted"">Nothing is overdue or due in the next " & VaccinationData.DueSoonDays & " days.</p>", "")
    End Sub

    ''' <summary>One row per flock + sex: overdue count, due-soon count and the earliest pending item.</summary>
    Private Shared Function SummariseDue(ByVal items As DataTable) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("flock_no", GetType(String))
        result.Columns.Add("sex", GetType(String))
        result.Columns.Add("overdue", GetType(Integer))
        result.Columns.Add("soon", GetType(Integer))
        result.Columns.Add("age_label", GetType(String))
        result.Columns.Add("vaccine", GetType(String))
        result.Columns.Add("due_date", GetType(Date))
        result.Columns.Add("days_overdue", GetType(Integer))
        Dim byKey As New Dictionary(Of String, DataRow)()
        Dim grace As Integer = VaccinationData.GraceDays
        ' Items arrive ordered by due date, so the first one seen per flock is the earliest pending.
        For Each item As DataRow In items.Rows
            Dim key As String = Convert.ToString(item("flock_no")) & "|" & Convert.ToString(item("sex"))
            Dim row As DataRow = Nothing
            If Not byKey.TryGetValue(key, row) Then
                row = result.NewRow()
                row("flock_no") = item("flock_no")
                row("sex") = item("sex")
                row("overdue") = 0
                row("soon") = 0
                row("age_label") = item("age_label")
                row("vaccine") = item("vaccine")
                row("due_date") = item("due_date")
                row("days_overdue") = item("days_overdue")
                result.Rows.Add(row)
                byKey(key) = row
            End If
            If CInt(item("days_overdue")) > grace Then
                row("overdue") = CInt(row("overdue")) + 1
            Else
                row("soon") = CInt(row("soon")) + 1
            End If
        Next
        Return result
    End Function

    Private Sub ShowSchedule()
        pnlSchedule.Visible = False
        If ddlFlock.SelectedValue = "" Then Return
        Dim hatch As Nullable(Of Date) = ParseIso(txtHatch.Text)
        If Not hatch.HasValue Then
            ShowMessage("error", "Enter the hatch date for this flock.")
            Return
        End If
        If hatch.Value > DateTime.Today Then
            ShowMessage("error", "The hatch date cannot be in the future.")
            Return
        End If
        Try
            Dim flockNo As String = ddlFlock.SelectedValue
            Dim sex As String = rblSex.SelectedValue
            _items = VaccinationData.GetSchedule(flockNo, sex, hatch.Value)
            gvSchedule.DataSource = _items
            gvSchedule.DataBind()
            ShowSummary(flockNo, sex, hatch.Value)
            ShowHatchNote(flockNo, sex, hatch.Value)
            ShowNotes()
            pnlSchedule.Visible = True
        Catch ex As Exception
            ShowFailure(ex, "ShowSchedule")
        End Try
    End Sub

    Private Sub ShowSummary(ByVal flockNo As String, ByVal sex As String, ByVal hatch As Date)
        Dim counts As New Dictionary(Of String, Integer)()
        For Each key As String In New String() {"ontime", "early", "late", "overdue", "today", "soon", "upcoming"}
            counts(key) = 0
        Next
        For Each item As VaccinationItem In _items
            counts(item.Status) += 1
        Next
        litTotal.Text = _items.Count.ToString()
        litOnTime.Text = (counts("ontime") + counts("early")).ToString()
        litLate.Text = counts("late").ToString()
        litOverdue.Text = counts("overdue").ToString()
        litSoon.Text = (counts("today") + counts("soon")).ToString()
        litSoonDays.Text = VaccinationData.DueSoonDays.ToString()
        litUpcoming.Text = counts("upcoming").ToString()

        Dim ageDays As Integer = (DateTime.Today - hatch).Days
        Dim info As New StringBuilder()
        info.Append("<strong>Flock ").Append(HttpUtility.HtmlEncode(flockNo)).Append("</strong> · ")
        info.Append(VaccinationData.SexName(sex)).Append(" · Hatch ").Append(hatch.ToString("dd-MM-yyyy"))
        info.Append(" · Age ").Append(ageDays \ 7).Append(" weeks ").Append(ageDays Mod 7 + 1).Append(" days (day ").Append(ageDays + 1).Append(")")
        Dim flock As DataRow = VaccinationData.GetFlock(flockNo)
        If flock IsNot Nothing Then
            Dim qtyColumn As String = If(sex = "M", "mqty", "fqty")
            If Not Convert.IsDBNull(flock(qtyColumn)) Then info.Append(" · Birds placed ").Append(HttpUtility.HtmlEncode(Convert.ToString(flock(qtyColumn))))
        End If
        If VaccinationData.IsTrackingClosed(flockNo, sex) Then info.Append(" · <span class=""vacc-badge is-upcoming"">Not in due list</span>")
        litFlockInfo.Text = info.ToString()
    End Sub

    Private Sub ShowHatchNote(ByVal flockNo As String, ByVal sex As String, ByVal hatch As Date)
        Dim saved As Nullable(Of Date) = VaccinationData.GetSavedHatchDate(flockNo, sex)
        If Not saved.HasValue Then
            litHatchNote.Text = "<p class=""vacc-note"">This schedule has not been saved yet. Check the hatch date, enter the dates already given, then click <strong>Save</strong> &mdash; the flock then appears in the due list.</p>"
        ElseIf saved.Value <> hatch Then
            litHatchNote.Text = "<p class=""vacc-note is-warn"">Hatch date changed from " & saved.Value.ToString("dd-MM-yyyy") & ". Click <strong>Save</strong> to keep the new date (all due dates and late days are recalculated).</p>"
        Else
            litHatchNote.Text = ""
        End If
    End Sub

    Private Sub ShowNotes()
        Dim notes As List(Of String) = VaccinationData.GetNotes()
        pnlNotes.Visible = notes.Count > 0
        Dim html As New StringBuilder("<ol>")
        For Each note As String In notes
            html.Append("<li>").Append(HttpUtility.HtmlEncode(note)).Append("</li>")
        Next
        litNotes.Text = html.Append("</ol>").ToString()
    End Sub

    Protected Sub gvSchedule_RowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs) Handles gvSchedule.RowDataBound
        If e.Row.RowType <> DataControlRowType.DataRow Then Return
        Dim item As VaccinationItem = CType(e.Row.DataItem, VaccinationItem)
        e.Row.Attributes("data-due") = item.DueDate.ToString(IsoDate, CultureInfo.InvariantCulture)
        e.Row.Attributes("data-status") = item.Status
        e.Row.CssClass = "is-" & item.Status

        Dim txtGiven As TextBox = CType(e.Row.FindControl("txtGiven"), TextBox)
        Dim txtReason As TextBox = CType(e.Row.FindControl("txtReason"), TextBox)
        Dim txtRemarks As TextBox = CType(e.Row.FindControl("txtRemarks"), TextBox)

        If item.GivenDate.HasValue Then txtGiven.Text = item.GivenDate.Value.ToString(IsoDate, CultureInfo.InvariantCulture)
        txtGiven.Attributes("max") = DateTime.Today.ToString(IsoDate, CultureInfo.InvariantCulture)
        txtReason.Text = item.LateReason
        txtRemarks.Text = item.Remarks
        If item.Status = "late" Then
            e.Row.CssClass &= " needs-reason"
            txtReason.Attributes("placeholder") = "Reason required (" & VaccinationData.DaysText(item.DaysDifference) & " late)"
        End If

    End Sub

    ' ================================================================== saving

    Private Sub SaveSchedule()
        Dim flockNo As String = ddlFlock.SelectedValue
        Dim sex As String = rblSex.SelectedValue
        Dim hatch As Nullable(Of Date) = ParseIso(txtHatch.Text)
        If flockNo = "" Then
            ShowMessage("error", "Select a flock.")
            Return
        End If
        If Not hatch.HasValue OrElse hatch.Value > DateTime.Today Then
            ShowMessage("error", "Enter a valid hatch date (not in the future).")
            Return
        End If

        Try
            ' Current state from the database, to save only what changed.
            Dim current As New Dictionary(Of Integer, VaccinationItem)()
            For Each existing As VaccinationItem In VaccinationData.GetSchedule(flockNo, sex, hatch.Value)
                current(existing.ScheduleId) = existing
            Next

            Dim changes As New List(Of VaccinationChange)()
            Dim errors As New List(Of String)()
            Dim errorIds As New HashSet(Of Integer)()
            For Each row As GridViewRow In gvSchedule.Rows
                If row.RowType <> DataControlRowType.DataRow Then Continue For
                Dim scheduleId As Integer = CInt(gvSchedule.DataKeys(row.RowIndex).Value)
                If Not current.ContainsKey(scheduleId) Then Continue For
                Dim item As VaccinationItem = current(scheduleId)

                Dim givenText As String = CType(row.FindControl("txtGiven"), TextBox).Text.Trim()
                Dim reason As String = CType(row.FindControl("txtReason"), TextBox).Text.Trim()
                Dim remarks As String = CType(row.FindControl("txtRemarks"), TextBox).Text.Trim()
                Dim given As Nullable(Of Date) = Nothing
                Dim label As String = item.AgeLabel & " – " & item.Vaccine

                If givenText.Length > 0 Then
                    given = ParseIso(givenText)
                    If Not given.HasValue Then
                        errorIds.Add(scheduleId) : errors.Add(label & ": the given date is not valid.")
                        Continue For
                    End If
                    If given.Value > DateTime.Today Then
                        errorIds.Add(scheduleId) : errors.Add(label & ": the given date cannot be in the future.")
                        Continue For
                    End If
                    If given.Value < hatch.Value Then
                        errorIds.Add(scheduleId) : errors.Add(label & ": the given date is before the hatch date.")
                        Continue For
                    End If
                    If VaccinationData.NeedsReason(item.DueDate, given.Value) AndAlso reason.Length = 0 Then
                        errorIds.Add(scheduleId) : errors.Add(label & ": given " & VaccinationData.DaysText((given.Value - item.DueDate).Days) & " late – enter the reason.")
                        Continue For
                    End If
                ElseIf reason.Length > 0 OrElse remarks.Length > 0 Then
                    If Not item.GivenDate.HasValue Then
                        errorIds.Add(scheduleId) : errors.Add(label & ": enter the date it was given (reason/remarks are saved with the given date).")
                        Continue For
                    End If
                End If

                Dim changed As Boolean =
                    Not Nullable.Equals(given, item.GivenDate) OrElse
                    (given.HasValue AndAlso (reason <> If(item.LateReason, "") OrElse remarks <> If(item.Remarks, "")))
                If changed Then
                    Dim change As New VaccinationChange()
                    change.ScheduleId = scheduleId
                    change.GivenDate = given
                    change.LateReason = reason
                    change.Remarks = remarks
                    changes.Add(change)
                End If
            Next

            If errors.Count > 0 Then
                Dim message As New StringBuilder("Not saved. Please correct:")
                For i As Integer = 0 To Math.Min(errors.Count, 5) - 1
                    message.Append(Environment.NewLine).Append("• ").Append(errors(i))
                Next
                If errors.Count > 5 Then message.Append(Environment.NewLine).Append("… and ").Append(errors.Count - 5).Append(" more.")
                ShowMessage("error", message.ToString())
                MarkErrorRows(errorIds)
                Return
            End If

            Dim saved As Integer = VaccinationData.Save(flockNo, sex, hatch.Value, changes, UserName)
            ShowSchedule()
            LoadDueList()
            If saved = 0 Then
                ShowMessage("success", "Saved. Hatch date " & hatch.Value.ToString("dd-MM-yyyy") & " – no vaccination entries changed.")
            Else
                ShowMessage("success", "Saved " & saved & If(saved = 1, " entry", " entries") & " for flock " & flockNo & " (" & VaccinationData.SexName(sex) & ").")
            End If
        Catch ex As Exception
            ShowFailure(ex, "Save")
        End Try
    End Sub

    Private Sub MarkErrorRows(ByVal errorIds As HashSet(Of Integer))
        For Each row As GridViewRow In gvSchedule.Rows
            If errorIds.Contains(CInt(gvSchedule.DataKeys(row.RowIndex).Value)) Then row.CssClass &= " has-error"
        Next
    End Sub

    ' ================================================================== helpers used by the markup

    Protected Function PhaseBadge(ByVal phase As String) As String
        If String.IsNullOrEmpty(phase) OrElse phase = "Rearing" Then Return ""
        Return "<span class=""vacc-badge is-phase"">" & HttpUtility.HtmlEncode(phase) & "</span>"
    End Function

    Protected Function DueBadge(ByVal daysOverdue As Integer) As String
        Dim css As String, text As String
        If daysOverdue > VaccinationData.GraceDays Then
            css = "overdue" : text = "Overdue by " & VaccinationData.DaysText(daysOverdue)
        ElseIf daysOverdue >= 0 Then
            css = "today" : text = If(daysOverdue = 0, "Due today", "Due")
        ElseIf daysOverdue = -1 Then
            css = "soon" : text = "Due tomorrow"
        Else
            css = "soon" : text = "Due in " & VaccinationData.DaysText(-daysOverdue)
        End If
        Return "<span class=""vacc-badge is-" & css & """>" & HttpUtility.HtmlEncode(text) & "</span>"
    End Function

    Protected Function CountBadge(ByVal count As Integer, ByVal kind As String) As String
        If count = 0 Then Return "<span class=""vacc-muted"">0</span>"
        Return "<span class=""vacc-badge is-" & kind & """>" & count.ToString(CultureInfo.InvariantCulture) & "</span>"
    End Function

    Protected Function EnteredText(ByVal dataItem As Object) As String
        Dim item As VaccinationItem = TryCast(dataItem, VaccinationItem)
        If item Is Nothing OrElse Not item.IsGiven OrElse String.IsNullOrEmpty(item.EnteredBy) Then Return ""
        Dim text As String = "by " & item.EnteredBy
        If item.EnteredOn.HasValue Then text &= ", " & item.EnteredOn.Value.ToString("dd-MM-yyyy HH:mm")
        Return text
    End Function

    ' ================================================================== Excel export (same layout as the paper schedule)

    Private Function BuildExcel(ByVal flockNo As String, ByVal sex As String, ByVal hatch As Date, ByVal items As List(Of VaccinationItem)) As String
        Dim h As Func(Of String, String) = Function(s As String) HttpUtility.HtmlEncode(If(s, ""))
        Dim sb As New StringBuilder()
        sb.Append("<html><head><meta charset=""utf-8""></head><body>")
        sb.Append("<table border=""1"" style=""border-collapse:collapse;font-family:Calibri;font-size:11pt"">")
        sb.Append("<tr><th colspan=""13"" style=""font-size:14pt"">BROILER PARENTS - ").Append(VaccinationData.SexName(sex).ToUpperInvariant()).Append("</th></tr>")
        sb.Append("<tr><th colspan=""13"">VACCINATION &amp; MEDICATION SCHEDULE - Flock ").Append(h(flockNo)).Append(" - Hatch ").Append(hatch.ToString("dd-MM-yyyy")).Append("</th></tr>")
        sb.Append("<tr style=""background:#dfe9f3""><th>S. NO.</th><th>AGE</th><th>DAY</th><th>VACCINE</th><th>DUE DATE</th><th>ACT DATE</th><th>STATUS</th><th>LATE DAYS</th><th>REASON IF LATE</th><th>DOSE</th><th>ROUTE</th><th>COMPANY NAME</th><th>REMARK</th></tr>")
        Dim phase As String = "Rearing"
        For Each item As VaccinationItem In items
            If item.Phase <> phase Then
                phase = item.Phase
                sb.Append("<tr><td></td><td colspan=""12""><b>").Append(h(phase)).Append("</b></td></tr>")
            End If
            Dim lateDays As String = If(item.IsGiven AndAlso item.DaysDifference > 0, item.DaysDifference.ToString(), "")
            Dim remark As String = item.Remark
            If Not String.IsNullOrEmpty(item.Remarks) Then remark = If(String.IsNullOrEmpty(remark), item.Remarks, remark & " / " & item.Remarks)
            sb.Append("<tr><td>").Append(item.SeqNo).Append("</td><td>").Append(h(item.AgeLabel)).Append("</td><td>").Append(item.DayNo)
            sb.Append("</td><td>").Append(h(item.Vaccine)).Append("</td><td>").Append(item.DueDate.ToString("dd-MM-yyyy"))
            sb.Append("</td><td>").Append(If(item.IsGiven, item.GivenDate.Value.ToString("dd-MM-yyyy"), ""))
            sb.Append("</td><td>").Append(h(item.StatusText)).Append("</td><td>").Append(lateDays)
            sb.Append("</td><td>").Append(h(item.LateReason)).Append("</td><td>").Append(h(item.Dose)).Append("</td><td>").Append(h(item.Route))
            sb.Append("</td><td>").Append(h(item.Company)).Append("</td><td>").Append(h(remark)).Append("</td></tr>")
        Next
        sb.Append("</table></body></html>")
        Return sb.ToString()
    End Function

    ' ================================================================== small helpers

    Private Shared Function ParseIso(ByVal text As String) As Nullable(Of Date)
        Dim value As Date
        If Not String.IsNullOrEmpty(text) AndAlso
           Date.TryParseExact(text.Trim(), IsoDate, CultureInfo.InvariantCulture, DateTimeStyles.None, value) Then
            Return value.Date
        End If
        Return Nothing
    End Function

    Private Shared Function SafeFileName(ByVal text As String) As String
        For Each c As Char In Path.GetInvalidFileNameChars()
            text = text.Replace(c, "_"c)
        Next
        Return text.Replace(" ", "_")
    End Function

    ''' <summary>Message box at the top of the page (kind "success" or "error"). Works with and without AJAX.</summary>
    Private Sub ShowMessage(ByVal kind As String, ByVal text As String)
        Dim css As String = If(kind = "success", "alert-success", "alert-danger")
        Dim html As String = HttpUtility.HtmlEncode(text).Replace(Environment.NewLine, "<br />")
        litMessage.Text = "<div class=""alert " & css & " vacc-message"" role=""alert"">" & html & "</div>"
    End Sub

    Private Sub ShowFailure(ByVal ex As Exception, ByVal action As String)
        VaccinationDb.LogError(ex, "transforms/VaccinationSchedule.aspx", action)
        ShowMessage("error", "Could not complete the request: " & ex.Message)
    End Sub

End Class
