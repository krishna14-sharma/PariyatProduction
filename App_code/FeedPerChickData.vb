Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

''' <summary>
''' Feed per chick for each flock: feed eaten by the flock (daily feed entries) compared with the eggs it laid
''' and the chicks hatched from its eggs (main + satellite hatchery).
''' Feed is entered in grams (feed per bird x birds on the Feed/Egg form); shown here in kg.
''' Read-only, parameterised SQL. Uses the site's own connection (GModule.Connstr).
''' Used by reports/FeedPerChick.aspx (Administration) and misreports/FeedPerChick.aspx (MIS Reports).
''' </summary>
Public NotInheritable Class FeedPerChickData

    Private Sub New()
    End Sub

    ''' <summary>Hatchery figure summed over the egg types (fresh, BG, MD, MP, MM, commercial) of alias h.</summary>
    Private Shared Function HatchSum(ByVal suffix As String) As String
        Dim parts As New List(Of String)()
        For Each t As String In New String() {"fresh", "bg", "md", "mp", "mm", "comm"}
            Dim col As String = t & suffix
            If col = "fresh_chicks" Then col = "fresh_chichs"   ' column name as it is in the database
            parts.Add("isnull(try_cast(h." & col & " as float), 0)")
        Next
        Return String.Join(" + ", parts.ToArray())
    End Function

    ''' <summary>Rows that belong in the chosen window: whole life up to @to, or only @from..@to.</summary>
    Private Const InWindow As String = "try_cast({0} as date) <= @to and (@life = 1 or try_cast({0} as date) >= @from)"

    ''' <summary>
    ''' One row per flock that has feed entries between @from and @to.
    ''' @life = 1: feed, eggs and chicks from the start of the flock up to @to; 0: only @from..@to.
    ''' </summary>
    Public Shared ReadOnly Property Sql As String
        Get
            Return _
                "select f.flock_no as Flock, f.bird_type_code as Breed, " &
                "isnull(stuff((select distinct ', ' + isnull(l.location_name, s.shed_loc) from Shed_Entry_Data s " &
                "  left join location_master l on l.location_code = s.shed_loc where s.flock_no = f.flock_no for xml path('')), 1, 2, ''), '') as Farms, " &
                "f.hatch_date as HatchDate, datediff(day, f.hatch_date, @to) / 7 as AgeWeeks, " &
                "fd.first_entry as FirstEntry, fd.last_entry as LastEntry, " &
                "isnull(fd.ff, 0) / 1000.0 as FemaleFeedKg, isnull(fd.fm, 0) / 1000.0 as MaleFeedKg, " &
                "isnull(eg.eggs, 0) as Eggs, isnull(eg.he, 0) as HatchingEggs, " &
                "isnull(c.chicks, 0) as Chicks, isnull(c.supply, 0) as SaleableChicks " &
                "from flock_master f " &
                "join (select d.flock_no, sum(try_cast(d.feed_qty_female as float)) as ff, sum(try_cast(d.feed_qty_male as float)) as fm, " &
                "  min(try_cast(d.entry_date as date)) as first_entry, max(try_cast(d.entry_date as date)) as last_entry, " &
                "  sum(case when try_cast(d.entry_date as date) >= @from then 1 else 0 end) as recent " &
                "  from tmp_daily_entry d where " & String.Format(InWindow, "d.entry_date") & " group by d.flock_no) fd on fd.flock_no = f.flock_no " &
                "left join (select e.flock_no, sum(try_cast(e.total_eggs as float)) as eggs, sum(try_cast(e.hatch_eggs as float)) as he " &
                "  from daily_eggcollection e where " & String.Format(InWindow, "e.entry_date") & " group by e.flock_no) eg on eg.flock_no = f.flock_no " &
                "left join (select y.flock_no, sum(y.chicks) as chicks, sum(y.supply) as supply from (" &
                "  select h.flock_no, h.loading_date as d, " & HatchSum("_chicks") & " as chicks, " & HatchSum("_supply") & " as supply from hatch_production h" &
                "  union all" &
                "  select h.flock_no, h.loading_date, " & HatchSum("_chicks") & ", " & HatchSum("_supply") & " from hatch_production_sat h" &
                "  ) y where " & String.Format(InWindow, "y.d") & " group by y.flock_no) c on c.flock_no = f.flock_no " &
                "where fd.recent > 0 and (@flock is null or f.flock_no = @flock) " &
                "and (@farm is null or f.flock_no in (select s.flock_no from Shed_Entry_Data s where s.shed_loc = @farm)) " &
                "order by f.hatch_date desc, f.flock_no"
        End Get
    End Property

    ''' <summary>
    ''' Week by week (flock age week = DATEDIFF(day, hatch date, date) / 7, as in the other reports) for the same
    ''' flocks as the main table. Feed and eggs by entry date, chicks by loading date.
    ''' </summary>
    Public Shared ReadOnly Property WeeklySql As String
        Get
            Return _
                "select w.flock_no as Flock, w.wk as AgeWeek, min(w.d) as WeekFrom, max(w.d) as WeekTo, " &
                "sum(w.ff) / 1000.0 as FemaleFeedKg, sum(w.fm) / 1000.0 as MaleFeedKg, sum(w.eggs) as Eggs, sum(w.he) as HatchingEggs, " &
                "sum(w.chicks) as Chicks, sum(w.supply) as SaleableChicks " &
                "from (" &
                "  select d.flock_no, try_cast(d.entry_date as date) as d, try_cast(d.feed_qty_female as float) as ff, try_cast(d.feed_qty_male as float) as fm, " &
                "  0.0 as eggs, 0.0 as he, 0.0 as chicks, 0.0 as supply, f.hatch_date, datediff(day, f.hatch_date, try_cast(d.entry_date as date)) / 7 as wk " &
                "  from tmp_daily_entry d join flock_master f on f.flock_no = d.flock_no where " & String.Format(InWindow, "d.entry_date") &
                "  union all " &
                "  select e.flock_no, try_cast(e.entry_date as date), 0.0, 0.0, try_cast(e.total_eggs as float), try_cast(e.hatch_eggs as float), 0.0, 0.0, " &
                "  f.hatch_date, datediff(day, f.hatch_date, try_cast(e.entry_date as date)) / 7 " &
                "  from daily_eggcollection e join flock_master f on f.flock_no = e.flock_no where " & String.Format(InWindow, "e.entry_date") &
                "  union all " &
                "  select h.flock_no, try_cast(h.loading_date as date), 0.0, 0.0, 0.0, 0.0, " & HatchSum("_chicks") & ", " & HatchSum("_supply") & ", " &
                "  f.hatch_date, datediff(day, f.hatch_date, try_cast(h.loading_date as date)) / 7 " &
                "  from hatch_production h join flock_master f on f.flock_no = h.flock_no where " & String.Format(InWindow, "h.loading_date") &
                "  union all " &
                "  select h.flock_no, try_cast(h.loading_date as date), 0.0, 0.0, 0.0, 0.0, " & HatchSum("_chicks") & ", " & HatchSum("_supply") & ", " &
                "  f.hatch_date, datediff(day, f.hatch_date, try_cast(h.loading_date as date)) / 7 " &
                "  from hatch_production_sat h join flock_master f on f.flock_no = h.flock_no where " & String.Format(InWindow, "h.loading_date") &
                ") w " &
                "where w.wk >= 0 and w.flock_no in (select d.flock_no from tmp_daily_entry d where try_cast(d.entry_date as date) between @from and @to) " &
                "and (@flock is null or w.flock_no = @flock) " &
                "and (@farm is null or w.flock_no in (select s.flock_no from Shed_Entry_Data s where s.shed_loc = @farm)) " &
                "group by w.flock_no, w.wk order by w.flock_no, w.wk"
        End Get
    End Property

    ''' <summary>Week-by-week rows with the worked-out columns (this week and cumulative).</summary>
    Public Shared Function LoadWeekly(ByVal fromDate As Date, ByVal toDate As Date, ByVal farmCode As String, ByVal flockNo As String,
                                      ByVal wholeLife As Boolean, ByVal includeMaleFeed As Boolean) As DataTable
        Dim dt As DataTable = Query(WeeklySql, Params(fromDate, toDate, farmCode, flockNo, wholeLife))
        AddWeeklyFigures(dt, includeMaleFeed)
        Return dt
    End Function

    ''' <summary>
    ''' Per week: total feed, feed per chick / per HE for that week, and the running totals from the first week shown
    ''' (cumulative feed per chick = all feed so far / all chicks so far). Rows must be ordered by flock, week.
    ''' </summary>
    Public Shared Sub AddWeeklyFigures(ByVal dt As DataTable, ByVal includeMaleFeed As Boolean)
        For Each name As String In New String() {"FeedKg", "FeedPerChickKg", "FeedPerHatchingEggG", "CumFeedKg", "CumChicks", "CumHatchingEggs", "CumFeedPerChickKg", "CumFeedPerHatchingEggG"}
            If Not dt.Columns.Contains(name) Then dt.Columns.Add(name, GetType(Double))
        Next
        Dim flock As String = Nothing
        Dim cumFeed As Double = 0, cumChicks As Double = 0, cumHE As Double = 0
        For Each r As DataRow In dt.Rows
            Dim thisFlock As String = Convert.ToString(r("Flock"))
            If thisFlock <> flock Then
                flock = thisFlock : cumFeed = 0 : cumChicks = 0 : cumHE = 0
            End If
            Dim feed As Double = Num(r("FemaleFeedKg")) + If(includeMaleFeed, Num(r("MaleFeedKg")), 0)
            r("FeedKg") = feed
            SetRatio(r, "FeedPerChickKg", feed, Num(r("Chicks")), 1)
            SetRatio(r, "FeedPerHatchingEggG", feed, Num(r("HatchingEggs")), 1000)
            cumFeed += feed : cumChicks += Num(r("Chicks")) : cumHE += Num(r("HatchingEggs"))
            r("CumFeedKg") = cumFeed
            r("CumChicks") = cumChicks
            r("CumHatchingEggs") = cumHE
            SetRatio(r, "CumFeedPerChickKg", cumFeed, cumChicks, 1)
            SetRatio(r, "CumFeedPerHatchingEggG", cumFeed, cumHE, 1000)
        Next
    End Sub

    Private Shared Function Params(ByVal fromDate As Date, ByVal toDate As Date, ByVal farmCode As String, ByVal flockNo As String, ByVal wholeLife As Boolean) As List(Of SqlParameter)
        Dim prms As New List(Of SqlParameter)()
        prms.Add(New SqlParameter("@from", SqlDbType.Date) With {.Value = fromDate})
        prms.Add(New SqlParameter("@to", SqlDbType.Date) With {.Value = toDate})
        prms.Add(New SqlParameter("@farm", SqlDbType.NVarChar, 100) With {.Value = If(String.IsNullOrEmpty(farmCode), CObj(DBNull.Value), farmCode)})
        prms.Add(New SqlParameter("@flock", SqlDbType.NVarChar, 100) With {.Value = If(String.IsNullOrEmpty(flockNo), CObj(DBNull.Value), flockNo)})
        prms.Add(New SqlParameter("@life", SqlDbType.Int) With {.Value = If(wholeLife, 1, 0)})
        Return prms
    End Function

    ''' <summary>Runs the query and adds the worked-out columns (total feed, feed per chick / egg).</summary>
    Public Shared Function Load(ByVal fromDate As Date, ByVal toDate As Date, ByVal farmCode As String, ByVal flockNo As String,
                                ByVal wholeLife As Boolean, ByVal includeMaleFeed As Boolean) As DataTable
        Dim prms As New List(Of SqlParameter)()
        prms.Add(New SqlParameter("@from", SqlDbType.Date) With {.Value = fromDate})
        prms.Add(New SqlParameter("@to", SqlDbType.Date) With {.Value = toDate})
        prms.Add(New SqlParameter("@farm", SqlDbType.NVarChar, 100) With {.Value = If(String.IsNullOrEmpty(farmCode), CObj(DBNull.Value), farmCode)})
        prms.Add(New SqlParameter("@flock", SqlDbType.NVarChar, 100) With {.Value = If(String.IsNullOrEmpty(flockNo), CObj(DBNull.Value), flockNo)})
        prms.Add(New SqlParameter("@life", SqlDbType.Int) With {.Value = If(wholeLife, 1, 0)})
        Dim dt As DataTable = Query(Sql, prms)
        AddFigures(dt, includeMaleFeed)
        Return dt
    End Function

    ''' <summary>Total feed and the per-chick / per-egg figures for each row.</summary>
    Public Shared Sub AddFigures(ByVal dt As DataTable, ByVal includeMaleFeed As Boolean)
        For Each name As String In New String() {"FeedKg", "FeedPerChickKg", "FeedPerSaleableChickKg", "FeedPerEggG", "FeedPerHatchingEggG", "ChicksPerTonne"}
            If Not dt.Columns.Contains(name) Then dt.Columns.Add(name, GetType(Double))
        Next
        For Each r As DataRow In dt.Rows
            Dim feed As Double = Num(r("FemaleFeedKg")) + If(includeMaleFeed, Num(r("MaleFeedKg")), 0)
            r("FeedKg") = feed
            SetRatio(r, "FeedPerChickKg", feed, Num(r("Chicks")), 1)
            SetRatio(r, "FeedPerSaleableChickKg", feed, Num(r("SaleableChicks")), 1)
            SetRatio(r, "FeedPerEggG", feed, Num(r("Eggs")), 1000)
            SetRatio(r, "FeedPerHatchingEggG", feed, Num(r("HatchingEggs")), 1000)
            SetRatio(r, "ChicksPerTonne", Num(r("Chicks")), feed, 1000)
        Next
    End Sub

    ''' <summary>Totals over all rows; the ratios are worked out from the totals (not averaged).</summary>
    Public Shared Function Totals(ByVal dt As DataTable) As Dictionary(Of String, Double)
        Dim t As New Dictionary(Of String, Double)()
        For Each name As String In New String() {"FemaleFeedKg", "MaleFeedKg", "FeedKg", "Eggs", "HatchingEggs", "Chicks", "SaleableChicks"}
            Dim s As Double = 0
            For Each r As DataRow In dt.Rows
                s += Num(r(name))
            Next
            t(name) = s
        Next
        t("FeedPerChickKg") = Ratio(t("FeedKg"), t("Chicks"), 1)
        t("FeedPerSaleableChickKg") = Ratio(t("FeedKg"), t("SaleableChicks"), 1)
        t("FeedPerEggG") = Ratio(t("FeedKg"), t("Eggs"), 1000)
        t("FeedPerHatchingEggG") = Ratio(t("FeedKg"), t("HatchingEggs"), 1000)
        t("ChicksPerTonne") = Ratio(t("Chicks"), t("FeedKg"), 1000)
        Return t
    End Function

    ''' <summary>Farms that have sheds (for the farm list).</summary>
    Public Shared Function Farms() As DataTable
        Return Query("select distinct l.location_code as Code, l.location_name as Name from location_master l " &
                     "where l.location_code in (select s.shed_loc from Shed_Entry_Data s) order by l.location_name", New List(Of SqlParameter)())
    End Function

    ''' <summary>Flocks with feed entries in the last two years, newest first (for the flock list).</summary>
    Public Shared Function Flocks() As DataTable
        Return Query("select f.flock_no as Flock, f.bird_type_code as Breed, f.hatch_date as HatchDate from flock_master f " &
                     "where f.flock_no in (select d.flock_no from tmp_daily_entry d where try_cast(d.entry_date as date) >= dateadd(year, -2, cast(getdate() as date))) " &
                     "order by f.hatch_date desc, f.flock_no", New List(Of SqlParameter)())
    End Function

    Public Shared Function Query(ByVal commandText As String, ByVal prms As List(Of SqlParameter)) As DataTable
        Dim dt As New DataTable()
        Using con As New SqlConnection(New GModule().Connstr)
            Using cmd As New SqlCommand(commandText, con)
                cmd.CommandTimeout = 120
                cmd.Parameters.AddRange(prms.ToArray())
                Using adp As New SqlDataAdapter(cmd)
                    adp.Fill(dt)
                End Using
            End Using
        End Using
        Return dt
    End Function

    Public Shared Function Num(ByVal v As Object) As Double
        If v Is Nothing OrElse Convert.IsDBNull(v) Then Return 0
        Dim d As Double
        If Double.TryParse(Convert.ToString(v, Globalization.CultureInfo.InvariantCulture), Globalization.NumberStyles.Any, Globalization.CultureInfo.InvariantCulture, d) Then Return d
        Return 0
    End Function

    Private Shared Sub SetRatio(ByVal r As DataRow, ByVal col As String, ByVal a As Double, ByVal b As Double, ByVal factor As Double)
        If b > 0 Then r(col) = a / b * factor Else r(col) = DBNull.Value
    End Sub

    Private Shared Function Ratio(ByVal a As Double, ByVal b As Double, ByVal factor As Double) As Double
        If b > 0 Then Return a / b * factor
        Return Double.NaN
    End Function

End Class
