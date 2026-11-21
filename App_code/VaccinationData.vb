Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

''' <summary>One line of a flock's vaccination schedule (standard item + what was actually given).</summary>
Public Class VaccinationItem
    Public Property ScheduleId As Integer
    Public Property SeqNo As Integer
    Public Property Phase As String
    Public Property AgeLabel As String
    Public Property DayNo As Integer
    Public Property Vaccine As String
    Public Property Dose As String
    Public Property Route As String
    Public Property Company As String
    Public Property Remark As String
    Public Property DueDate As Date
    Public Property GivenDate As Nullable(Of Date)
    Public Property LateReason As String
    Public Property Remarks As String
    Public Property EnteredBy As String
    Public Property EnteredOn As Nullable(Of DateTime)

    ''' <summary>ontime, late, early, overdue, today, soon or upcoming.</summary>
    Public Property Status As String
    ''' <summary>Short text for the status badge, e.g. "Late by 2 days".</summary>
    Public Property StatusText As String
    ''' <summary>Given date minus due date (given items) or today minus due date (pending items).</summary>
    Public Property DaysDifference As Integer

    Public ReadOnly Property IsGiven As Boolean
        Get
            Return GivenDate.HasValue
        End Get
    End Property

    Public ReadOnly Property DetailLine As String
        Get
            Dim parts As New List(Of String)()
            If Not String.IsNullOrEmpty(Dose) Then parts.Add(Dose)
            If Not String.IsNullOrEmpty(Route) Then parts.Add(Route)
            If Not String.IsNullOrEmpty(Company) Then parts.Add(Company)
            Return String.Join(" · ", parts.ToArray())
        End Get
    End Property
End Class

''' <summary>One change to save: a schedule item's given date / reason / remarks (given date Nothing = clear it).</summary>
Public Class VaccinationChange
    Public Property ScheduleId As Integer
    Public Property GivenDate As Nullable(Of Date)
    Public Property LateReason As String
    Public Property Remarks As String
End Class

''' <summary>
''' Data access and rules for the vaccination &amp; medication schedule.
''' Tables: vacc_schedule_master, vacc_flock, vacc_flock_entry, vacc_schedule_note (Database/Vaccination_Schedule.sql).
''' Due date = hatch date + (day_no - 1). Given later than the due date (plus grace days) = late, a reason is required.
''' </summary>
Public NotInheritable Class VaccinationData

    Private Sub New()
    End Sub

    ' ------------------------------------------------------------------ settings

    ''' <summary>Days a vaccine may be given after its due date and still count as "on time" (appSetting Vaccination:GraceDays, default 0).</summary>
    Public Shared ReadOnly Property GraceDays As Integer
        Get
            Return SettingInt("Vaccination:GraceDays", 0)
        End Get
    End Property

    ''' <summary>How many days ahead the "due soon" list looks (appSetting Vaccination:DueSoonDays, default 7).</summary>
    Public Shared ReadOnly Property DueSoonDays As Integer
        Get
            Return SettingInt("Vaccination:DueSoonDays", 7)
        End Get
    End Property

    Private Shared Function SettingInt(ByVal key As String, ByVal fallback As Integer) As Integer
        Dim value As Integer
        If Integer.TryParse(ConfigurationManager.AppSettings(key), value) AndAlso value >= 0 Then Return value
        Return fallback
    End Function

    Public Shared Function SexName(ByVal sex As String) As String
        If sex = "M" Then Return "Male"
        If sex = "F" Then Return "Female"
        Return sex
    End Function

    ' ------------------------------------------------------------------ flocks

    ''' <summary>Flocks the user can see: their farm's live flocks (area users) or all flocks, newest first.</summary>
    Public Shared Function GetFlocks(ByVal areaCode As String) As DataTable
        Dim sql As String =
            "select fm.flock_no, fm.hatch_date, fm.fqty, fm.mqty, fm.bird_type_code " &
            "from flock_master fm "
        If String.IsNullOrEmpty(areaCode) Then
            Return VaccinationDb.GetTable(sql & "order by fm.hatch_date desc, fm.flock_no")
        End If
        sql &= "where fm.flock_no in (select distinct s.flock_no from Shed_Entry_Data s where s.shed_loc = @area and s.isAlive = 1) " &
               "order by fm.hatch_date desc, fm.flock_no"
        Return VaccinationDb.GetTable(sql, VaccinationDb.P("@area", areaCode))
    End Function

    ''' <summary>flock_master row for a flock (Nothing if not found).</summary>
    Public Shared Function GetFlock(ByVal flockNo As String) As DataRow
        Dim dt As DataTable = VaccinationDb.GetTable(
            "select top 1 flock_no, hatch_date, fqty, mqty, bird_type_code from flock_master where flock_no = @f",
            VaccinationDb.P("@f", flockNo))
        If dt.Rows.Count = 0 Then Return Nothing
        Return dt.Rows(0)
    End Function

    ''' <summary>Hatch date saved for this flock + sex (Nothing when tracking has not started).</summary>
    Public Shared Function GetSavedHatchDate(ByVal flockNo As String, ByVal sex As String) As Nullable(Of Date)
        Dim value As Object = VaccinationDb.Scalar(
            "select hatch_date from vacc_flock where flock_no = @f and sex = @s", VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex))
        If value Is Nothing Then Return Nothing
        Return CDate(value).Date
    End Function

    ''' <summary>Saved hatch date for flock + sex, otherwise the flock's hatch date from flock_master.</summary>
    Public Shared Function GetHatchDate(ByVal flockNo As String, ByVal sex As String) As Nullable(Of Date)
        Dim saved As Nullable(Of Date) = GetSavedHatchDate(flockNo, sex)
        If saved.HasValue Then Return saved
        Dim flock As DataRow = GetFlock(flockNo)
        If flock Is Nothing OrElse Convert.IsDBNull(flock("hatch_date")) Then Return Nothing
        Return CDate(flock("hatch_date")).Date
    End Function

    Public Shared Function IsTrackingClosed(ByVal flockNo As String, ByVal sex As String) As Boolean
        Dim value As Object = VaccinationDb.Scalar(
            "select is_closed from vacc_flock where flock_no = @f and sex = @s", VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex))
        Return value IsNot Nothing AndAlso CBool(value)
    End Function

    ' ------------------------------------------------------------------ schedule

    ''' <summary>The flock's schedule for one sex: every active standard item, with due dates and what was given.</summary>
    Public Shared Function GetSchedule(ByVal flockNo As String, ByVal sex As String, ByVal hatchDate As Date) As List(Of VaccinationItem)
        Dim dt As DataTable = VaccinationDb.GetTable(
            "select m.id, m.seq_no, m.phase, m.age_label, m.day_no, m.vaccine, m.dose, m.route, m.company, m.remark, " &
            "       e.given_date, e.late_reason, e.remarks, isnull(e.updated_by, e.entered_by) as entered_by, " &
            "       isnull(e.updated_on, e.entered_on) as entered_on " &
            "from vacc_schedule_master m " &
            "left join vacc_flock_entry e on e.schedule_id = m.id and e.flock_no = @f and e.sex = @s " &
            "where m.sex = @s and (m.is_active = 1 or e.id is not null) " &
            "order by m.day_no, m.seq_no",
            VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex))

        Dim today As Date = DateTime.Today
        Dim items As New List(Of VaccinationItem)()
        For Each row As DataRow In dt.Rows
            Dim item As New VaccinationItem()
            item.ScheduleId = CInt(row("id"))
            item.SeqNo = CInt(row("seq_no"))
            item.Phase = Convert.ToString(row("phase"))
            item.AgeLabel = Convert.ToString(row("age_label"))
            item.DayNo = CInt(row("day_no"))
            item.Vaccine = Convert.ToString(row("vaccine"))
            item.Dose = Convert.ToString(row("dose"))
            item.Route = Convert.ToString(row("route"))
            item.Company = Convert.ToString(row("company"))
            item.Remark = Convert.ToString(row("remark"))
            item.DueDate = DueDateFor(hatchDate, item.DayNo)
            If Not Convert.IsDBNull(row("given_date")) Then item.GivenDate = CDate(row("given_date")).Date
            item.LateReason = Convert.ToString(row("late_reason"))
            item.Remarks = Convert.ToString(row("remarks"))
            item.EnteredBy = Convert.ToString(row("entered_by"))
            If Not Convert.IsDBNull(row("entered_on")) Then item.EnteredOn = CDate(row("entered_on"))
            Classify(item, today)
            items.Add(item)
        Next
        Return items
    End Function

    Public Shared Function DueDateFor(ByVal hatchDate As Date, ByVal dayNo As Integer) As Date
        Return hatchDate.Date.AddDays(dayNo - 1)
    End Function

    ''' <summary>Sets Status, StatusText and DaysDifference for an item.</summary>
    Public Shared Sub Classify(ByVal item As VaccinationItem, ByVal today As Date)
        Dim grace As Integer = GraceDays
        If item.GivenDate.HasValue Then
            Dim diff As Integer = (item.GivenDate.Value - item.DueDate).Days
            item.DaysDifference = diff
            If diff > grace Then
                item.Status = "late"
                item.StatusText = "Given late by " & DaysText(diff)
            ElseIf diff < 0 Then
                item.Status = "early"
                item.StatusText = "Given early by " & DaysText(-diff)
            Else
                item.Status = "ontime"
                item.StatusText = "Given on time"
            End If
        Else
            Dim diff As Integer = (today - item.DueDate).Days
            item.DaysDifference = diff
            If diff > grace Then
                item.Status = "overdue"
                item.StatusText = "Overdue by " & DaysText(diff)
            ElseIf diff >= 0 Then
                item.Status = "today"
                item.StatusText = If(diff = 0, "Due today", "Due (" & DaysText(diff) & " ago)")
            ElseIf -diff <= DueSoonDays Then
                item.Status = "soon"
                item.StatusText = If(diff = -1, "Due tomorrow", "Due in " & DaysText(-diff))
            Else
                item.Status = "upcoming"
                item.StatusText = "Upcoming"
            End If
        End If
    End Sub

    Public Shared Function DaysText(ByVal days As Integer) As String
        If days = 1 Then Return "1 day"
        Return days.ToString(CultureInfo.InvariantCulture) & " days"
    End Function

    ''' <summary>True when a vaccine given on this date needs a reason (later than due date + grace days).</summary>
    Public Shared Function NeedsReason(ByVal dueDate As Date, ByVal givenDate As Date) As Boolean
        Return (givenDate - dueDate).Days > GraceDays
    End Function

    ' ------------------------------------------------------------------ save

    ''' <summary>
    ''' Saves the hatch date and the given changes for one flock + sex in a single transaction.
    ''' Validation must be done by the caller. Returns the number of schedule lines changed.
    ''' </summary>
    Public Shared Function Save(ByVal flockNo As String, ByVal sex As String, ByVal hatchDate As Date,
                                ByVal changes As List(Of VaccinationChange), ByVal userName As String) As Integer
        Dim count As Integer = 0
        Using con As New SqlConnection(VaccinationDb.ConnectionString())
            con.Open()
            Using tx As SqlTransaction = con.BeginTransaction()
                Try
                    ' 1. Hatch date for this flock + sex (re-opens tracking if it was stopped).
                    Exec(con, tx,
                        "update vacc_flock set hatch_date = @h, is_closed = 0, updated_by = @u, updated_on = getdate() where flock_no = @f and sex = @s; " &
                        "insert into vacc_flock (flock_no, sex, hatch_date, created_by) " &
                        "select @f, @s, @h, @u where not exists (select 1 from vacc_flock where flock_no = @f and sex = @s);",
                        VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex), DateParam("@h", hatchDate), VaccinationDb.P("@u", userName))

                    ' 2. Keep stored due dates in line with the hatch date and the standard schedule.
                    Exec(con, tx,
                        "update e set due_date = dateadd(day, m.day_no - 1, @h) " &
                        "from vacc_flock_entry e join vacc_schedule_master m on m.id = e.schedule_id " &
                        "where e.flock_no = @f and e.sex = @s and e.due_date <> dateadd(day, m.day_no - 1, @h)",
                        VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex), DateParam("@h", hatchDate))

                    ' 3. Given dates, reasons and remarks.
                    For Each change As VaccinationChange In changes
                        If change.GivenDate.HasValue Then
                            Exec(con, tx,
                                "update e set due_date = dateadd(day, m.day_no - 1, @h), given_date = @g, late_reason = @r, remarks = @rm, " &
                                "       updated_by = @u, updated_on = getdate() " &
                                "from vacc_flock_entry e join vacc_schedule_master m on m.id = e.schedule_id " &
                                "where e.flock_no = @f and e.sex = @s and e.schedule_id = @id; " &
                                "insert into vacc_flock_entry (flock_no, sex, schedule_id, due_date, given_date, late_reason, remarks, entered_by) " &
                                "select @f, @s, m.id, dateadd(day, m.day_no - 1, @h), @g, @r, @rm, @u from vacc_schedule_master m " &
                                "where m.id = @id and m.sex = @s " &
                                "  and not exists (select 1 from vacc_flock_entry x where x.flock_no = @f and x.sex = @s and x.schedule_id = @id);",
                                VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex), VaccinationDb.P("@id", change.ScheduleId),
                                DateParam("@h", hatchDate), DateParam("@g", change.GivenDate.Value),
                                VaccinationDb.P("@r", NullIfEmpty(change.LateReason)), VaccinationDb.P("@rm", NullIfEmpty(change.Remarks)),
                                VaccinationDb.P("@u", userName))
                        Else
                            Exec(con, tx,
                                "delete from vacc_flock_entry where flock_no = @f and sex = @s and schedule_id = @id",
                                VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex), VaccinationDb.P("@id", change.ScheduleId))
                        End If
                        count += 1
                    Next
                    tx.Commit()
                Catch
                    tx.Rollback()
                    Throw
                End Try
            End Using
        End Using
        Return count
    End Function

    ''' <summary>Stops showing this flock + sex in the due list (e.g. flock sold). Saving again re-opens it.</summary>
    Public Shared Sub CloseTracking(ByVal flockNo As String, ByVal sex As String, ByVal userName As String)
        VaccinationDb.Execute(
            "update vacc_flock set is_closed = 1, updated_by = @u, updated_on = getdate() where flock_no = @f and sex = @s",
            VaccinationDb.P("@f", flockNo), VaccinationDb.P("@s", sex), VaccinationDb.P("@u", userName))
    End Sub

    ' ------------------------------------------------------------------ due list + notes

    ''' <summary>
    ''' Pending items that are overdue or due within DueSoonDays, for every tracked (open) flock the user can see.
    ''' Columns: flock_no, sex, schedule_id, age_label, vaccine, due_date, days_overdue (positive = overdue).
    ''' </summary>
    Public Shared Function GetDueList(ByVal areaCode As String) As DataTable
        Dim sql As String =
            "select top 5000 f.flock_no, f.sex, m.id as schedule_id, m.age_label, m.vaccine, " &
            "       dateadd(day, m.day_no - 1, f.hatch_date) as due_date, " &
            "       datediff(day, dateadd(day, m.day_no - 1, f.hatch_date), cast(getdate() as date)) as days_overdue " &
            "from vacc_flock f " &
            "join vacc_schedule_master m on m.sex = f.sex and m.is_active = 1 " &
            "left join vacc_flock_entry e on e.flock_no = f.flock_no and e.sex = f.sex and e.schedule_id = m.id " &
            "where f.is_closed = 0 and e.id is null " &
            "  and dateadd(day, m.day_no - 1, f.hatch_date) <= dateadd(day, @soon, cast(getdate() as date)) "
        Dim prms As New List(Of SqlParameter)()
        prms.Add(VaccinationDb.P("@soon", DueSoonDays))
        If Not String.IsNullOrEmpty(areaCode) Then
            sql &= "  and f.flock_no in (select distinct s.flock_no from Shed_Entry_Data s where s.shed_loc = @area and s.isAlive = 1) "
            prms.Add(VaccinationDb.P("@area", areaCode))
        End If
        sql &= "order by due_date, f.flock_no, f.sex, m.seq_no"
        Return VaccinationDb.GetTable(sql, prms.ToArray())
    End Function

    Public Shared Function GetNotes() As List(Of String)
        Dim notes As New List(Of String)()
        For Each row As DataRow In VaccinationDb.GetTable("select note from vacc_schedule_note order by seq_no").Rows
            notes.Add(Convert.ToString(row("note")))
        Next
        Return notes
    End Function

    ' ------------------------------------------------------------------ helpers

    Private Shared Sub Exec(ByVal con As SqlConnection, ByVal tx As SqlTransaction, ByVal sql As String, ByVal ParamArray prms() As SqlParameter)
        Using cmd As New SqlCommand(sql, con, tx)
            cmd.CommandTimeout = 120
            cmd.Parameters.AddRange(prms)
            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Shared Function DateParam(ByVal name As String, ByVal value As Date) As SqlParameter
        Dim p As New SqlParameter(name, SqlDbType.Date)
        p.Value = value.Date
        Return p
    End Function

    Private Shared Function NullIfEmpty(ByVal value As String) As Object
        If value Is Nothing Then Return Nothing
        value = value.Trim()
        If value.Length = 0 Then Return Nothing
        Return value
    End Function

End Class

''' <summary>
''' Small data-access helper for the vaccination pages. Uses the same database connection as the rest
''' of the site (GModule.Connstr), always with parameters (no SQL built from user text).
''' </summary>
Public NotInheritable Class VaccinationDb

    Private Sub New()
    End Sub

    Public Shared Function ConnectionString() As String
        Return New GModule().Connstr
    End Function

    Public Shared Function P(ByVal name As String, ByVal value As Object) As SqlParameter
        If value Is Nothing Then value = DBNull.Value
        Return New SqlParameter(name, value)
    End Function

    Public Shared Function GetTable(ByVal sql As String, ByVal ParamArray prms() As SqlParameter) As DataTable
        Dim dt As New DataTable()
        Using con As New SqlConnection(ConnectionString())
            Using cmd As SqlCommand = Command(con, sql, prms)
                Using adp As New SqlDataAdapter(cmd)
                    adp.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function Execute(ByVal sql As String, ByVal ParamArray prms() As SqlParameter) As Integer
        Using con As New SqlConnection(ConnectionString())
            Using cmd As SqlCommand = Command(con, sql, prms)
                con.Open()
                Return cmd.ExecuteNonQuery()
            End Using
        End Using
    End Function

    Public Shared Function Scalar(ByVal sql As String, ByVal ParamArray prms() As SqlParameter) As Object
        Using con As New SqlConnection(ConnectionString())
            Using cmd As SqlCommand = Command(con, sql, prms)
                con.Open()
                Dim result As Object = cmd.ExecuteScalar()
                If result Is DBNull.Value Then Return Nothing
                Return result
            End Using
        End Using
    End Function

    ''' <summary>Writes an error to Logs/ErrorLog.txt using the site's existing logger.</summary>
    Public Shared Sub LogError(ByVal ex As Exception, ByVal formName As String, ByVal eventName As String)
        Try
            Dim ctx As System.Web.HttpContext = System.Web.HttpContext.Current
            Dim who As String = ""
            If ctx IsNot Nothing AndAlso ctx.Session IsNot Nothing Then who = Convert.ToString(ctx.Session("user_name"))
            Call New GModule().LogErrorToFile(ex.ToString(), formName, eventName, who)
        Catch
        End Try
    End Sub

    Private Shared Function Command(ByVal con As SqlConnection, ByVal sql As String, ByVal prms() As SqlParameter) As SqlCommand
        Dim cmd As New SqlCommand(sql, con)
        cmd.CommandTimeout = 300
        If prms IsNot Nothing Then
            For Each prm As SqlParameter In prms
                If prm IsNot Nothing Then cmd.Parameters.Add(prm)
            Next
        End If
        Return cmd
    End Function

End Class
