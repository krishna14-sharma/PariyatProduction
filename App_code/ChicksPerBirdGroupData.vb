Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Chicks Per Bird report for a flock group: 190 = 190 A + 190 B + 190 C + 190 D.
''' Runs the existing weekly procedure (sp_WeeklyPerforance_Report) for every sub-flock - the same figures as the
''' single-flock Chicks Per Bird report - and adds them up by age week. Hatchability % is worked out again from
''' the added-up chicks and loading (not averaged). Read-only apart from what the procedure itself does.
''' </summary>
Public NotInheritable Class ChicksPerBirdGroupData

    Private Sub New()
    End Sub

    ''' <summary>Columns of tmpWeeklyPerReport that are counts (added up across sub-flocks).</summary>
    Public Shared ReadOnly CountColumns As String() = {
        "TotalHatchEggs", "TotalCommEggs", "TotalCrackEggs", "TotalLeakerEggs", "TotalEggs", "TotalMortality",
        "FreshLoading", "FreshFertile", "FreshInfertile", "FreshHLoss", "FreshCulls", "FreshChicks",
        "BLoading", "BFertile", "BInfertile", "BHLoss", "BCulls", "BChicks",
        "HatcheryHHE", "HatcheryCrack", "HatcheryLeaker", "HatcherySale"}

    ''' <summary>
    ''' Sub-flocks of a base flock number: the base itself and every flock number that continues with a space,
    ''' hyphen, slash, underscore or letter ("190 A", "190-B", "190C"), but not another digit ("1901").
    ''' </summary>
    Public Shared Function FindSubFlocks(ByVal baseFlock As String) As DataTable
        Const sql As String =
            "select f.flock_no as Flock, f.bird_type_code as Breed, f.hatch_date as HatchDate, try_cast(f.fqty as float) as Females " &
            "from flock_master f " &
            "where ltrim(rtrim(f.flock_no)) = @b " &
            "or (left(ltrim(f.flock_no), len(@b)) = @b and len(ltrim(rtrim(f.flock_no))) > len(@b) " &
            "    and (substring(ltrim(f.flock_no), len(@b) + 1, 1) in (' ', '-', '/', '_') or substring(ltrim(f.flock_no), len(@b) + 1, 1) like '[A-Za-z]')) " &
            "order by f.flock_no"
        Dim prms As New List(Of SqlParameter)()
        prms.Add(New SqlParameter("@b", SqlDbType.NVarChar, 50) With {.Value = baseFlock.Trim()})
        Return Query(sql, prms)
    End Function

    ''' <summary>Weekly figures of one sub-flock, exactly as the existing Chicks Per Bird report gets them.</summary>
    Public Shared Function WeeklyForFlock(ByVal flockNo As String) As DataTable
        ' The procedure fills tmpWeeklyPerReport; read it back in the same batch (and only this flock's rows,
        ' in case someone else runs the report at the same moment).
        Dim sql As String =
            "exec sp_WeeklyPerforance_Report @flock; " &
            "select [FlockNo], [Weeks], " & String.Join(", ", WrapColumns()) & " from tmpWeeklyPerReport where [FlockNo] = @flock order by [Weeks]"
        Dim prms As New List(Of SqlParameter)()
        prms.Add(New SqlParameter("@flock", SqlDbType.NVarChar, 50) With {.Value = flockNo})
        Return Query(sql, prms)
    End Function

    Private Shared Function WrapColumns() As String()
        Dim list As New List(Of String)()
        For Each c As String In CountColumns
            list.Add("[" & c & "]")
        Next
        Return list.ToArray()
    End Function

    ''' <summary>
    ''' Adds up the weekly tables of the sub-flocks by week. Result: one row per week with the count columns,
    ''' FlocksWithData, the hatchability columns worked out again, and cumulative chicks / HE per bird.
    ''' Weeks where every figure is 0 are left out.
    ''' </summary>
    Public Shared Function Consolidate(ByVal perFlock As Dictionary(Of String, DataTable), ByVal housedFemales As Double) As DataTable
        Dim result As New DataTable()
        result.Columns.Add("Weeks", GetType(Integer))
        result.Columns.Add("FlocksWithData", GetType(Integer))
        For Each c As String In CountColumns
            result.Columns.Add(c, GetType(Double))
        Next
        For Each c As String In New String() {"FreshHatchability", "BHatchability", "TotalLoading", "TotalFertile", "TotalInFertile", "TotalChicks",
                                              "FertilityPercent", "Percentage", "CumHatchEggs", "CumChicks", "CumHEPerBird", "CumChicksPerBird"}
            result.Columns.Add(c, GetType(Double))
        Next

        Dim byWeek As New SortedDictionary(Of Integer, DataRow)()
        For Each pair As KeyValuePair(Of String, DataTable) In perFlock
            For Each src As DataRow In pair.Value.Rows
                Dim week As Integer
                If Not Integer.TryParse(Convert.ToString(src("Weeks")).Trim(), week) Then Continue For
                Dim hasData As Boolean = False
                For Each c As String In CountColumns
                    If Num(src(c)) <> 0 Then hasData = True : Exit For
                Next
                If Not hasData Then Continue For
                Dim row As DataRow = Nothing
                If Not byWeek.TryGetValue(week, row) Then
                    row = result.NewRow()
                    row("Weeks") = week
                    row("FlocksWithData") = 0
                    For Each c As String In CountColumns
                        row(c) = 0.0
                    Next
                    byWeek(week) = row
                End If
                row("FlocksWithData") = CInt(row("FlocksWithData")) + 1
                For Each c As String In CountColumns
                    row(c) = CDbl(row(c)) + Num(src(c))
                Next
            Next
        Next

        Dim cumChicks As Double = 0, cumHE As Double = 0
        For Each week As Integer In byWeek.Keys
            Dim r As DataRow = byWeek(week)
            Dim loading As Double = CDbl(r("FreshLoading")) + CDbl(r("BLoading"))
            Dim fertile As Double = CDbl(r("FreshFertile")) + CDbl(r("BFertile"))
            Dim chicks As Double = CDbl(r("FreshChicks")) + CDbl(r("BChicks"))
            r("FreshHatchability") = Pct(CDbl(r("FreshChicks")), CDbl(r("FreshLoading")))
            r("BHatchability") = Pct(CDbl(r("BChicks")), CDbl(r("BLoading")))
            r("TotalLoading") = loading
            r("TotalFertile") = fertile
            r("TotalInFertile") = CDbl(r("FreshInfertile")) + CDbl(r("BInfertile"))
            r("TotalChicks") = chicks
            r("FertilityPercent") = Pct(fertile, loading)
            r("Percentage") = Pct(chicks, loading)
            cumChicks += chicks
            cumHE += CDbl(r("TotalHatchEggs"))
            r("CumHatchEggs") = cumHE
            r("CumChicks") = cumChicks
            If housedFemales > 0 Then
                r("CumHEPerBird") = Math.Round(cumHE / housedFemales, 2)
                r("CumChicksPerBird") = Math.Round(cumChicks / housedFemales, 2)
            End If
            result.Rows.Add(r)
        Next
        Return result
    End Function

    ''' <summary>Totals of one weekly table (any of the sub-flocks, or the consolidated one).</summary>
    Public Shared Function Totals(ByVal weekly As DataTable) As Dictionary(Of String, Double)
        Dim t As New Dictionary(Of String, Double)()
        For Each c As String In CountColumns
            Dim s As Double = 0
            If weekly.Columns.Contains(c) Then
                For Each r As DataRow In weekly.Rows
                    s += Num(r(c))
                Next
            End If
            t(c) = s
        Next
        t("TotalLoading") = t("FreshLoading") + t("BLoading")
        t("TotalFertile") = t("FreshFertile") + t("BFertile")
        t("TotalInFertile") = t("FreshInfertile") + t("BInfertile")
        t("TotalChicks") = t("FreshChicks") + t("BChicks")
        t("FreshHatchability") = Pct(t("FreshChicks"), t("FreshLoading"))
        t("BHatchability") = Pct(t("BChicks"), t("BLoading"))
        t("FertilityPercent") = Pct(t("TotalFertile"), t("TotalLoading"))
        t("Percentage") = Pct(t("TotalChicks"), t("TotalLoading"))
        Return t
    End Function

    Public Shared Function Pct(ByVal part As Double, ByVal whole As Double) As Double
        If whole = 0 Then Return 0
        Return Math.Round(part * 100.0 / whole, 2)
    End Function

    Public Shared Function Num(ByVal v As Object) As Double
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return 0
        Dim d As Double
        If Double.TryParse(Convert.ToString(v, Globalization.CultureInfo.InvariantCulture), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, d) Then Return d
        Return 0
    End Function

    ''' <summary>Runs a command and returns its last result set (the procedure may return its own first).</summary>
    Public Shared Function Query(ByVal commandText As String, ByVal prms As List(Of SqlParameter)) As DataTable
        Dim ds As New DataSet()
        Using con As New SqlConnection(New GModule().Connstr)
            Using cmd As New SqlCommand(commandText, con)
                cmd.CommandTimeout = 300
                cmd.Parameters.AddRange(prms.ToArray())
                Using adp As New SqlDataAdapter(cmd)
                    adp.Fill(ds)
                End Using
            End Using
        End Using
        If ds.Tables.Count = 0 Then Return New DataTable()
        Return ds.Tables(ds.Tables.Count - 1)
    End Function

End Class
