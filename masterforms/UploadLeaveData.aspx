<%@ Page Title="Upload Leave Data" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false"
    EnableViewState="false" CompilerOptions="/optionexplicit+ /optioninfer+" %>
<%@ Assembly Name="System.IO.Compression, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>
<%@ Assembly Name="System.Xml.Linq, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Data.SqlClient" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.IO.Compression" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Text.RegularExpressions" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.Caching" %>
<%@ Import Namespace="System.Web.Script.Serialization" %>
<%@ Import Namespace="System.Xml.Linq" %>
<%--
    UploadLeaveData.aspx - SINGLE FILE page (no .aspx.vb code-behind needed)
    * Delete UploadLeaveData.aspx.vb and UploadLeaveData.aspx.designer.vb if present.
    * ContentPlaceHolderID below must equal the ID of an <asp:ContentPlaceHolder>
      inside SiteMaster.master.
    * The page answers its own AJAX calls (form field "ajaxAction") in OnPreInit
      and ends the request before the master page renders.
--%>

<script runat="server">

    ' ---- settings ---------------------------------------------------
    Private Const ConnectionName As String = "DefaultConnection"
    Private Const TableName As String = "[dbo].[EmployeeYearlyLeavedata]"
    Private Const MaxFileBytes As Integer = 10 * 1024 * 1024   ' 10 MB
    Private Const CacheMinutes As Integer = 30
    Private Const MaxIssuesReturned As Integer = 1000

    ' Header names (normalised: upper case, punctuation -> space, single spaces)
    Private Const DefaultMdaysHeader As String = "MONTH OF DAY"
    Private Const DefaultRlHeader As String = "LEAVE DAY"
    Private Const DefaultExtlHeader As String = "ABSENT DAY"

    Private Shared ReadOnly AllowedHeaders As String() = {
        "MONTH OF DAY", "PRASENT DAY", "LEAVE DAY", "TOTAL PRASENT DAY", "ABSENT DAY"}

    ' =================================================================
    '  AJAX ENTRY POINT
    ' =================================================================
    Protected Overrides Sub OnPreInit(e As EventArgs)
        Dim action = Request.Form("ajaxAction")
        If String.Equals(Request.HttpMethod, "POST", StringComparison.OrdinalIgnoreCase) AndAlso
           Not String.IsNullOrEmpty(action) Then
            HandleAjax(action.Trim().ToLowerInvariant())   ' ends the response
            Return
        End If
        MyBase.OnPreInit(e)
    End Sub

    Private Sub HandleAjax(action As String)
        Dim result As Object
        Try
            Select Case action
                Case "preview"
                    result = Preview()
                Case "import"
                    result = Import()
                Case Else
                    Throw New UserException("Unknown action.")
            End Select

        Catch ex As ThreadAbortException
            Throw
        Catch ex As UserException
            result = New With {.ok = False, .message = ex.Message}
        Catch ex As SqlException
            result = New With {.ok = False, .message = "Database error: " & ex.Message}
        Catch ex As Exception
            result = New With {.ok = False, .message = "Server error: " & ex.Message}
        End Try

        Dim js As New JavaScriptSerializer() With {.MaxJsonLength = Integer.MaxValue}
        Response.Clear()
        Response.ContentType = "application/json"
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        Response.Write(js.Serialize(result))
        Response.End()   ' stops the page (and Site.Master) from rendering
    End Sub

    ' =================================================================
    '  PREVIEW
    ' =================================================================
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Session("user_name") Is Nothing Then
            Response.Redirect("~/loginforms/index.html", True)
        End If
    End Sub
    Private Function Preview() As Object
        Dim req = Request

        If req.Files.Count = 0 OrElse req.Files(0) Is Nothing OrElse req.Files(0).ContentLength = 0 Then
            Throw New UserException("Please choose an Excel file.")
        End If
        Dim file As HttpPostedFile = req.Files(0)

        Dim ext = Path.GetExtension(file.FileName).ToLowerInvariant()
        If ext = ".xls" Then
            Throw New UserException("Old .xls format is not supported. Open the file in Excel and use Save As > Excel Workbook (.xlsx).")
        End If
        If ext <> ".xlsx" Then
            Throw New UserException("Only .xlsx files are allowed.")
        End If
        If file.ContentLength > MaxFileBytes Then
            Throw New UserException("File is larger than " & (MaxFileBytes \ 1024 \ 1024) & " MB.")
        End If

        Dim month = ParseRequired(req.Form("month"), 1, 12, "Month")
        Dim year = ParseRequired(req.Form("year"), 2000, 2100, "Year")
        Dim stripStars = (req.Form("stripStars") = "1")

        Dim opt As New ParseOptions With {
            .MdaysHeader = PickHeader(req.Form("mdaysCol"), DefaultMdaysHeader),
            .RlHeader = PickHeader(req.Form("rlCol"), DefaultRlHeader),
            .ExtlHeader = PickHeader(req.Form("extlCol"), DefaultExtlHeader),
            .StripStars = stripStars
        }

        ' ZipArchive needs a seekable stream
        Dim ms As New MemoryStream()
        file.InputStream.CopyTo(ms)
        ms.Position = 0

        Dim parsed As ParseResult
        Try
            parsed = ParseWorkbook(ms, opt)
        Catch ex As UserException
            Throw
        Catch ex As Exception
            Throw New UserException("Could not read the Excel file: " & ex.Message)
        Finally
            ms.Dispose()
        End Try

        If parsed.Rows.Count = 0 Then
            Throw New UserException("No valid employee rows were found. Check that the sheet has a CODE column and the selected leave columns.")
        End If

        ' How many rows already exist for this month/year?
        Dim existing As Integer = -1
        Dim dbWarning As String = Nothing
        Try
            existing = CountExisting(month, year)
        Catch ex As Exception
            dbWarning = "Could not check existing data: " & ex.Message
        End Try

        ' Cache the clean rows; the import step uses only this token
        Dim token = Guid.NewGuid().ToString("N")
        Dim upload As New CachedUpload With {
            .FileName = Path.GetFileName(file.FileName),
            .Month = month,
            .Year = year,
            .Rows = parsed.Rows
        }
        HttpRuntime.Cache.Insert(CacheKey(token), upload, Nothing,
                                 DateTime.UtcNow.AddMinutes(CacheMinutes), System.Web.Caching.Cache.NoSlidingExpiration)

        Return New With {
            .ok = True,
            .token = token,
            .fileName = upload.FileName,
            .month = month,
            .year = year,
            .validRows = parsed.Rows.Count,
            .existingRows = existing,
            .dbWarning = dbWarning,
            .sheets = parsed.Sheets,
            .issueCount = parsed.Issues.Count,
            .issues = parsed.Issues.Take(MaxIssuesReturned).ToList(),
            .rows = parsed.Rows.Select(Function(r) New With {
                         .sheet = r.Sheet, .row = r.ExcelRow, .code = r.EmpCode,
                         .name = r.Name, .rl = r.RL, .extl = r.EXTL, .mdays = r.MDAYS}).ToList()
        }
    End Function

    ' =================================================================
    '  IMPORT
    ' =================================================================
    Private Function Import() As Object
        Dim token = (If(Request.Form("token"), "")).Trim()
        Dim mode = (If(Request.Form("mode"), "replace")).Trim().ToLowerInvariant()
        If mode <> "replace" AndAlso mode <> "skip" Then mode = "replace"

        Dim upload = TryCast(HttpRuntime.Cache(CacheKey(token)), CachedUpload)
        If upload Is Nothing Then
            Throw New UserException("The preview has expired. Please read the file again.")
        End If

        Dim deleted As Integer = 0
        Dim skippedExisting As Integer = 0
        Dim toInsert As List(Of LeaveRow) = Nothing

        Using conn As New SqlConnection(GetConnectionString())
            conn.Open()
            Using tran = conn.BeginTransaction()
                Try
                    If mode = "replace" Then
                        Using cmd As New SqlCommand(
                            "DELETE FROM " & TableName & " WHERE [MONTH] = @m AND [YEAR] = @y", conn, tran)
                            cmd.Parameters.Add("@m", SqlDbType.Int).Value = upload.Month
                            cmd.Parameters.Add("@y", SqlDbType.Int).Value = upload.Year
                            cmd.CommandTimeout = 120
                            deleted = cmd.ExecuteNonQuery()
                        End Using
                        toInsert = upload.Rows
                    Else
                        Dim existingCodes As New HashSet(Of String)(StringComparer.OrdinalIgnoreCase)
                        Using cmd As New SqlCommand(
                            "SELECT [EmpCode] FROM " & TableName & " WHERE [MONTH] = @m AND [YEAR] = @y", conn, tran)
                            cmd.Parameters.Add("@m", SqlDbType.Int).Value = upload.Month
                            cmd.Parameters.Add("@y", SqlDbType.Int).Value = upload.Year
                            Using rd = cmd.ExecuteReader()
                                While rd.Read()
                                    If Not rd.IsDBNull(0) Then existingCodes.Add(rd.GetString(0).Trim())
                                End While
                            End Using
                        End Using
                        toInsert = upload.Rows.Where(Function(r) Not existingCodes.Contains(r.EmpCode)).ToList()
                        skippedExisting = upload.Rows.Count - toInsert.Count
                    End If

                    If toInsert.Count > 0 Then
                        Using bulk As New SqlBulkCopy(conn, SqlBulkCopyOptions.CheckConstraints, tran)
                            bulk.DestinationTableName = TableName
                            bulk.BatchSize = 1000
                            bulk.BulkCopyTimeout = 300
                            For Each col In New String() {"EmpCode", "RL", "EXTL", "MDAYS", "MONTH", "YEAR"}
                                bulk.ColumnMappings.Add(col, col)
                            Next
                            bulk.WriteToServer(BuildTable(toInsert, upload.Month, upload.Year))
                        End Using
                    End If

                    tran.Commit()
                Catch
                    tran.Rollback()
                    Throw
                End Try
            End Using
        End Using

        HttpRuntime.Cache.Remove(CacheKey(token))

        Return New With {
            .ok = True,
            .inserted = toInsert.Count,
            .deleted = deleted,
            .skippedExisting = skippedExisting,
            .month = upload.Month,
            .year = upload.Year,
            .message = String.Format("{0} rows saved for {1} {2}.", toInsert.Count,
                                     CultureInfo.InvariantCulture.DateTimeFormat.GetMonthName(upload.Month), upload.Year)
        }
    End Function

    ' =================================================================
    '  EXCEL PARSING
    ' =================================================================
    Private Function ParseWorkbook(stream As Stream, opt As ParseOptions) As ParseResult
        Dim result As New ParseResult()
        Dim seen As New Dictionary(Of String, LeaveRow)(StringComparer.OrdinalIgnoreCase)

        For Each sheet In XlsxReader.ReadAllSheets(stream)
            result.Sheets.Add(ParseSheet(sheet.Key, sheet.Value, opt, seen, result))
        Next

        Return result
    End Function

    Private Function ParseSheet(sheetName As String, rows As List(Of Object()), opt As ParseOptions,
                                seen As Dictionary(Of String, LeaveRow), result As ParseResult) As SheetSummary
        Dim summary As New SheetSummary With {.name = sheetName}

        ' 1) find the header row: must contain a CODE column + the 3 selected columns
        Dim headerIdx = -1, codeCol = -1, nameCol = -1, mdaysCol = -1, rlCol = -1, extlCol = -1
        For r = 0 To Math.Min(rows.Count, 25) - 1
            Dim h = rows(r).Select(Function(v) Normalize(v)).ToList()
            Dim c = h.FindIndex(Function(x) Regex.IsMatch(x, "(^| )CODE( |$)"))
            Dim m = h.IndexOf(opt.MdaysHeader)
            Dim l = h.IndexOf(opt.RlHeader)
            Dim e = h.IndexOf(opt.ExtlHeader)
            If c >= 0 AndAlso m >= 0 AndAlso l >= 0 AndAlso e >= 0 Then
                headerIdx = r : codeCol = c : mdaysCol = m : rlCol = l : extlCol = e
                nameCol = h.FindIndex(Function(x) x = "NAME" OrElse x = "EMPLOYEE NAME" OrElse x = "EMP NAME")
                Exit For
            End If
        Next

        If headerIdx < 0 Then
            summary.status = "skipped"
            summary.note = "No header row with a CODE column and the selected leave columns."
            Return summary
        End If

        summary.status = "read"
        summary.headerRow = headerIdx + 1

        ' 2) data rows
        For r = headerIdx + 1 To rows.Count - 1
            Dim row = rows(r)
            Dim excelRow = r + 1
            Dim rawCode = CellText(row, codeCol)
            Dim name = If(nameCol >= 0, CellText(row, nameCol), "")

            If rawCode = "" Then
                ' blank lines, signature lines and totals are ignored silently
                If name <> "" AndAlso Not Regex.IsMatch(name, "SIGNATURE|TOTAL", RegexOptions.IgnoreCase) Then
                    AddIssue(result, summary, sheetName, excelRow, "", name, "skip", "No staff code - row skipped.")
                End If
                Continue For
            End If

            If Regex.IsMatch(rawCode, "^(GRAND\s*)?TOTAL", RegexOptions.IgnoreCase) Then Continue For

            Dim code = Regex.Replace(rawCode, "\s+", "")
            If opt.StripStars Then code = code.TrimStart("*"c)

            If code = "" OrElse Not Regex.IsMatch(code, "\d") Then
                AddIssue(result, summary, sheetName, excelRow, rawCode, name, "skip", "Invalid staff code - row skipped.")
                Continue For
            End If
            If code.Length > 50 Then
                AddIssue(result, summary, sheetName, excelRow, rawCode, name, "skip", "Staff code longer than 50 characters - row skipped.")
                Continue For
            End If

            Dim notes As New List(Of String)()
            Dim mdays, rl, extl As Integer
            Dim err As String = Nothing
            If Not TryGetInt(row, mdaysCol, opt.MdaysHeader, mdays, notes, err) OrElse
               Not TryGetInt(row, rlCol, opt.RlHeader, rl, notes, err) OrElse
               Not TryGetInt(row, extlCol, opt.ExtlHeader, extl, notes, err) Then
                AddIssue(result, summary, sheetName, excelRow, code, name, "skip", err & " - row skipped.")
                Continue For
            End If

            Dim item As New LeaveRow With {
                .Sheet = sheetName, .ExcelRow = excelRow, .EmpCode = code, .Name = name,
                .MDAYS = mdays, .RL = rl, .EXTL = extl}

            Dim first As LeaveRow = Nothing
            If seen.TryGetValue(code, first) Then
                Dim same = first.MDAYS = mdays AndAlso first.RL = rl AndAlso first.EXTL = extl
                AddIssue(result, summary, sheetName, excelRow, code, name, If(same, "info", "warn"),
                         String.Format("Duplicate of '{0}' row {1}{2} - kept the first one.",
                                       first.Sheet, first.ExcelRow, If(same, " (same values)", " (DIFFERENT values)")))
                Continue For
            End If

            For Each n In notes
                AddIssue(result, summary, sheetName, excelRow, code, name, "warn", n)
            Next

            seen.Add(code, item)
            result.Rows.Add(item)
            summary.rows += 1
        Next

        Return summary
    End Function

    Private Shared Function TryGetInt(row As Object(), col As Integer, header As String, ByRef value As Integer,
                                      notes As List(Of String), ByRef err As String) As Boolean
        Dim v = If(col < row.Length, row(col), Nothing)
        value = 0

        If v Is Nothing OrElse v Is DBNull.Value OrElse v.ToString().Trim() = "" Then
            notes.Add("'" & header & "' is blank - saved as 0.")
            Return True
        End If

        Dim d As Double
        If TypeOf v Is Double OrElse TypeOf v Is Integer OrElse TypeOf v Is Decimal OrElse TypeOf v Is Long Then
            d = Convert.ToDouble(v, CultureInfo.InvariantCulture)
        ElseIf Not Double.TryParse(v.ToString().Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, d) Then
            err = "'" & header & "' is not a number (" & v.ToString().Trim() & ")"
            Return False
        End If

        If d < 0 OrElse d > 366 Then
            err = "'" & header & "' is out of range (" & d.ToString(CultureInfo.InvariantCulture) & ")"
            Return False
        End If

        value = CInt(Math.Round(d, MidpointRounding.AwayFromZero))
        If Math.Abs(d - value) > 0.0001 Then
            notes.Add("'" & header & "' value " & d.ToString(CultureInfo.InvariantCulture) & " rounded to " & value & ".")
        End If
        Return True
    End Function

    Private Shared Sub AddIssue(result As ParseResult, summary As SheetSummary, sheet As String, row As Integer,
                                code As String, name As String, level As String, message As String)
        result.Issues.Add(New RowIssue With {
            .sheet = sheet, .row = row, .code = code, .name = name, .level = level, .message = message})
        If level = "skip" OrElse level = "info" Then summary.skipped += 1
    End Sub

    ' =================================================================
    '  HELPERS
    ' =================================================================
    Private Shared Function Normalize(v As Object) As String
        If v Is Nothing OrElse v Is DBNull.Value Then Return ""
        Dim s = Regex.Replace(v.ToString().ToUpperInvariant(), "[^A-Z0-9]+", " ")
        Return s.Trim()
    End Function

    Private Shared Function CellText(row As Object(), col As Integer) As String
        If col < 0 OrElse col >= row.Length OrElse row(col) Is Nothing OrElse row(col) Is DBNull.Value Then Return ""
        Return row(col).ToString().Trim()
    End Function

    Private Shared Function PickHeader(value As String, fallback As String) As String
        Dim n = Normalize(value)
        If AllowedHeaders.Contains(n) Then Return n
        Return fallback
    End Function

    Private Shared Function ParseRequired(value As String, min As Integer, max As Integer, label As String) As Integer
        Dim n As Integer
        If Not Integer.TryParse(value, n) OrElse n < min OrElse n > max Then
            Throw New UserException("Please select a valid " & label & ".")
        End If
        Return n
    End Function

    Private Shared Function CountExisting(month As Integer, year As Integer) As Integer
        Using conn As New SqlConnection(GetConnectionString())
            Using cmd As New SqlCommand(
                "SELECT COUNT(*) FROM " & TableName & " WHERE [MONTH] = @m AND [YEAR] = @y", conn)
                cmd.Parameters.Add("@m", SqlDbType.Int).Value = month
                cmd.Parameters.Add("@y", SqlDbType.Int).Value = year
                conn.Open()
                Return Convert.ToInt32(cmd.ExecuteScalar())
            End Using
        End Using
    End Function

    Private Shared Function BuildTable(rows As List(Of LeaveRow), month As Integer, year As Integer) As DataTable
        Dim dt As New DataTable()
        dt.Columns.Add("EmpCode", GetType(String))
        dt.Columns.Add("RL", GetType(Integer))
        dt.Columns.Add("EXTL", GetType(Integer))
        dt.Columns.Add("MDAYS", GetType(Integer))
        dt.Columns.Add("MONTH", GetType(Integer))
        dt.Columns.Add("YEAR", GetType(Integer))
        For Each r In rows
            dt.Rows.Add(r.EmpCode, r.RL, r.EXTL, r.MDAYS, month, year)
        Next
        Return dt
    End Function

    Dim gmod As New GModule
    Private Shared Function GetConnectionString() As String
        Dim gmod As New GModule
        Return gmod.Connstr
    End Function

    Private Shared Function CacheKey(token As String) As String
        Return "LeaveUpload_" & token
    End Function


    ' =================================================================
    '  HELPER CLASSES (nested so they cannot clash with your project)
    ' =================================================================

    ' =====================================================================
    '  Data classes
    ' =====================================================================
    Private Class UserException
        Inherits Exception
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class

    Private Class ParseOptions
        Public Property MdaysHeader As String
        Public Property RlHeader As String
        Public Property ExtlHeader As String
        Public Property StripStars As Boolean
    End Class

    Private Class LeaveRow
        Public Property Sheet As String
        Public Property ExcelRow As Integer
        Public Property EmpCode As String
        Public Property Name As String
        Public Property RL As Integer
        Public Property EXTL As Integer
        Public Property MDAYS As Integer
    End Class

    Private Class RowIssue
        Public Property sheet As String
        Public Property row As Integer
        Public Property code As String
        Public Property name As String
        Public Property level As String
        Public Property message As String
    End Class

    Private Class SheetSummary
        Public Property name As String
        Public Property status As String
        Public Property headerRow As Integer
        Public Property rows As Integer
        Public Property skipped As Integer
        Public Property note As String
    End Class

    Private Class ParseResult
        Public Property Rows As New List(Of LeaveRow)()
        Public Property Sheets As New List(Of SheetSummary)()
        Public Property Issues As New List(Of RowIssue)()
    End Class

    Private Class CachedUpload
        Public Property FileName As String
        Public Property Month As Integer
        Public Property Year As Integer
        Public Property Rows As List(Of LeaveRow)
    End Class

    ' =====================================================================
    '  XlsxReader - minimal .xlsx reader using only built-in .NET 4.8
    '  (an .xlsx file is a zip of XML files). Reads cell values only:
    '  each cell becomes a String, a Double, or Nothing. Formula cells
    '  return the value Excel last calculated and saved.
    ' =====================================================================
    Private NotInheritable Class XlsxReader

        Private Const MaxColumns As Integer = 16384

        Private Sub New()
        End Sub

        ''' <summary>Returns every worksheet (name, rows) in workbook order.
        ''' rows(i) is Excel row i+1, rows(i)(j) is column j (A = 0).</summary>
        Public Shared Function ReadAllSheets(stream As Stream) As List(Of KeyValuePair(Of String, List(Of Object())))
            Dim result As New List(Of KeyValuePair(Of String, List(Of Object())))()

            Using zip As New ZipArchive(stream, ZipArchiveMode.Read, True)
                Dim wb = LoadXml(zip, "xl/workbook.xml")
                If wb Is Nothing Then Throw New InvalidDataException("This is not a valid .xlsx file.")

                ' relationship id -> part path (e.g. rId1 -> xl/worksheets/sheet1.xml)
                Dim targets As New Dictionary(Of String, String)(StringComparer.Ordinal)
                Dim sstPath = "xl/sharedStrings.xml"
                Dim rels = LoadXml(zip, "xl/_rels/workbook.xml.rels")
                If rels IsNot Nothing Then
                    For Each rel In Children(rels.Root, "Relationship")
                        Dim id = CStr(rel.Attribute("Id"))
                        Dim target = CStr(rel.Attribute("Target"))
                        Dim relType = If(CStr(rel.Attribute("Type")), "")
                        If id Is Nothing OrElse target Is Nothing Then Continue For
                        targets(id) = ResolvePath(target)
                        If relType.EndsWith("/sharedStrings", StringComparison.OrdinalIgnoreCase) Then sstPath = ResolvePath(target)
                    Next
                End If

                Dim sst = ReadSharedStrings(zip, sstPath)

                Dim sheetsEl = Child(wb.Root, "sheets")
                If sheetsEl Is Nothing Then Return result

                For Each sh In Children(sheetsEl, "sheet")
                    Dim name = If(CStr(sh.Attribute("name")), "")
                    ' r:id attribute (relationships namespace)
                    Dim ridAttr = sh.Attributes().FirstOrDefault(
                        Function(a) a.Name.LocalName = "id" AndAlso a.Name.NamespaceName <> "")
                    Dim partPath As String = Nothing
                    If ridAttr Is Nothing OrElse Not targets.TryGetValue(ridAttr.Value, partPath) Then Continue For

                    Dim doc = LoadXml(zip, partPath)
                    If doc Is Nothing Then Continue For   ' chart sheet or missing part
                    result.Add(New KeyValuePair(Of String, List(Of Object()))(name, ReadRows(doc, sst)))
                Next
            End Using

            Return result
        End Function

        Private Shared Function ReadRows(doc As XDocument, sst As List(Of String)) As List(Of Object())
            Dim rows As New List(Of Object())()
            Dim sheetData = Child(doc.Root, "sheetData")
            If sheetData Is Nothing Then Return rows

            Dim nextRow = 1
            For Each rowEl In Children(sheetData, "row")
                Dim rowNum = nextRow
                Dim rAttr = CStr(rowEl.Attribute("r"))
                Dim n As Integer
                If rAttr IsNot Nothing AndAlso Integer.TryParse(rAttr, n) AndAlso n >= nextRow Then rowNum = n

                ' Excel skips empty rows in the XML - pad so index = Excel row - 1
                While rows.Count < rowNum - 1
                    rows.Add(New Object() {})
                End While

                Dim cells As New List(Of Object)()
                Dim nextCol = 0
                For Each c In Children(rowEl, "c")
                    Dim col = ColumnIndex(CStr(c.Attribute("r")), nextCol)
                    If col >= MaxColumns Then Continue For
                    While cells.Count <= col
                        cells.Add(Nothing)
                    End While
                    cells(col) = CellValue(c, sst)
                    nextCol = col + 1
                Next

                rows.Add(cells.ToArray())
                nextRow = rowNum + 1
            Next

            Return rows
        End Function

        Private Shared Function CellValue(c As XElement, sst As List(Of String)) As Object
            Dim t = If(CStr(c.Attribute("t")), "n")
            Dim vEl = Child(c, "v")
            Dim v = If(vEl Is Nothing, Nothing, vEl.Value)

            Select Case t
                Case "s"        ' shared string
                    Dim idx As Integer
                    If v IsNot Nothing AndAlso Integer.TryParse(v, NumberStyles.Integer, CultureInfo.InvariantCulture, idx) _
                       AndAlso idx >= 0 AndAlso idx < sst.Count Then
                        Return sst(idx)
                    End If
                    Return Nothing

                Case "inlineStr"
                    Dim isEl = Child(c, "is")
                    Return If(isEl Is Nothing, Nothing, RichText(isEl))

                Case "b"
                    Return If(v = "1", "TRUE", "FALSE")

                Case "str", "e", "d"   ' formula string, error (#N/A), ISO date
                    Return v

                Case Else           ' "n" = number
                    If v Is Nothing OrElse v = "" Then Return Nothing
                    Dim d As Double
                    If Double.TryParse(v, NumberStyles.Float, CultureInfo.InvariantCulture, d) Then Return d
                    Return v
            End Select
        End Function

        Private Shared Function ReadSharedStrings(zip As ZipArchive, partPath As String) As List(Of String)
            Dim list As New List(Of String)()
            Dim doc = LoadXml(zip, partPath)
            If doc Is Nothing Then Return list
            For Each si In Children(doc.Root, "si")
                list.Add(RichText(si))
            Next
            Return list
        End Function

        ' Joins all <t> text in a string item, ignoring phonetic (<rPh>) runs
        Private Shared Function RichText(el As XElement) As String
            Return String.Concat(el.Descendants().
                Where(Function(e) e.Name.LocalName = "t" AndAlso e.Parent IsNot Nothing AndAlso e.Parent.Name.LocalName <> "rPh").
                Select(Function(e) e.Value))
        End Function

        ' "C12" -> 2 ; no reference -> fallback
        Private Shared Function ColumnIndex(cellRef As String, fallback As Integer) As Integer
            If String.IsNullOrEmpty(cellRef) Then Return fallback
            Dim col = 0
            Dim found = False
            For Each ch In cellRef.ToUpperInvariant()
                If ch >= "A"c AndAlso ch <= "Z"c Then
                    col = col * 26 + (AscW(ch) - AscW("A"c) + 1)
                    found = True
                    If col > MaxColumns Then Return MaxColumns
                Else
                    Exit For
                End If
            Next
            Return If(found, col - 1, fallback)
        End Function

        ' Relationship targets are relative to xl/ unless they start with "/"
        Private Shared Function ResolvePath(target As String) As String
            Dim t = target.Replace("\"c, "/"c)
            If t.StartsWith("/") Then Return t.TrimStart("/"c)
            If t.StartsWith("../") Then Return t.Substring(3)
            Return "xl/" & t
        End Function

        Private Shared Function LoadXml(zip As ZipArchive, partPath As String) As XDocument
            Dim entry = zip.Entries.FirstOrDefault(
                Function(e) String.Equals(e.FullName.Replace("\"c, "/"c), partPath, StringComparison.OrdinalIgnoreCase))
            If entry Is Nothing Then Return Nothing
            Using s = entry.Open()
                Return XDocument.Load(s)
            End Using
        End Function

        Private Shared Function Child(el As XElement, localName As String) As XElement
            If el Is Nothing Then Return Nothing
            Return el.Elements().FirstOrDefault(Function(e) e.Name.LocalName = localName)
        End Function

        Private Shared Function Children(el As XElement, localName As String) As IEnumerable(Of XElement)
            If el Is Nothing Then Return Enumerable.Empty(Of XElement)()
            Return el.Elements().Where(Function(e) e.Name.LocalName = localName)
        End Function

    End Class
</script>

<asp:Content ID="LeaveUploadContent" ContentPlaceHolderID="SiteMaster" runat="server">
<style>
    #leaveUpload {
        --lu-line: #dfe3e8; --lu-text: #1f2933; --lu-muted: #637381; --lu-soft: #f9fafb;
        --lu-accent: #1565c0; --lu-accent-dark: #0d47a1;
        --lu-ok: #2e7d32; --lu-ok-bg: #e8f5e9; --lu-warn: #b26a00; --lu-warn-bg: #fff4e5;
        --lu-err: #c62828; --lu-err-bg: #fdecea; --lu-info-bg: #e3f2fd;
        color: var(--lu-text); font-size: 14px; line-height: 1.5; padding: 16px 0 24px;
    }
    #leaveUpload * { box-sizing: border-box; }
    #leaveUpload h2.lu-title { font-size: 22px; margin: 0 0 4px; font-weight: 600; }
    #leaveUpload .lu-sub { color: var(--lu-muted); margin: 0 0 18px; }
    #leaveUpload .lu-card { background: #fff; border: 1px solid var(--lu-line); border-radius: 8px; padding: 20px; margin-bottom: 16px; }
    #leaveUpload .lu-card h3 { font-size: 16px; margin: 0 0 14px; font-weight: 600; }
    #leaveUpload .lu-step { display: inline-block; width: 24px; height: 24px; line-height: 24px; text-align: center;
        border-radius: 50%; background: var(--lu-accent); color: #fff; font-size: 13px; margin-right: 8px; }
    #leaveUpload .lu-grid { display: grid; grid-template-columns: repeat(auto-fit, minmax(180px, 1fr)); gap: 14px; }
    #leaveUpload .lu-span2 { grid-column: span 2; }
    @media (max-width: 420px) { #leaveUpload .lu-span2 { grid-column: auto; } }
    #leaveUpload label.lu-label { display: block; font-weight: 600; font-size: 13px; margin: 0 0 4px; }
    #leaveUpload select, #leaveUpload input[type=text], #leaveUpload input[type=file] {
        width: 100%; max-width: none; padding: 7px 9px; border: 1px solid var(--lu-line); border-radius: 5px;
        font: inherit; background: #fff; height: auto; }
    #leaveUpload .lu-hint { color: var(--lu-muted); font-size: 12px; margin-top: 3px; }
    #leaveUpload label.lu-check { display: flex; align-items: flex-start; gap: 6px; font-weight: normal; margin: 12px 0 0; }
    #leaveUpload label.lu-check input { margin-top: 4px; }
    #leaveUpload .lu-actions { margin-top: 16px; display: flex; gap: 10px; flex-wrap: wrap; align-items: center; }
    #leaveUpload button.lu-btn { padding: 9px 18px; border: 0; border-radius: 5px; font: inherit; font-weight: 600;
        cursor: pointer; background: var(--lu-accent); color: #fff; }
    #leaveUpload button.lu-btn:hover:not(:disabled) { background: var(--lu-accent-dark); }
    #leaveUpload button.lu-btn.lu-secondary { background: #fff; color: var(--lu-text); border: 1px solid var(--lu-line); }
    #leaveUpload button.lu-btn:disabled { opacity: .55; cursor: not-allowed; }
    #leaveUpload .lu-progress { flex: 1; min-width: 160px; height: 8px; background: var(--lu-line); border-radius: 4px; overflow: hidden; display: none; }
    #leaveUpload .lu-progress > div { height: 100%; width: 0; background: var(--lu-accent); transition: width .2s; }
    #leaveUpload .lu-msg { padding: 10px 14px; border-radius: 5px; margin-top: 14px; display: none; }
    #leaveUpload .lu-msg.ok { background: var(--lu-ok-bg); color: var(--lu-ok); }
    #leaveUpload .lu-msg.err { background: var(--lu-err-bg); color: var(--lu-err); }
    #leaveUpload .lu-msg.warn { background: var(--lu-warn-bg); color: var(--lu-warn); }
    #leaveUpload .lu-stats { display: grid; grid-template-columns: repeat(auto-fit, minmax(150px, 1fr)); gap: 12px; margin-bottom: 16px; }
    #leaveUpload .lu-stat { border: 1px solid var(--lu-line); border-radius: 6px; padding: 10px 12px; }
    #leaveUpload .lu-stat b { display: block; font-size: 22px; }
    #leaveUpload .lu-stat span { color: var(--lu-muted); font-size: 12px; }
    #leaveUpload table.lu-table { width: 100%; border-collapse: collapse; font-size: 13px; margin: 0; }
    #leaveUpload .lu-table th, #leaveUpload .lu-table td { padding: 6px 8px; border: 0; border-bottom: 1px solid var(--lu-line); text-align: left; white-space: nowrap; }
    #leaveUpload .lu-table th { background: var(--lu-soft); position: sticky; top: 0; z-index: 1; font-weight: 600; }
    #leaveUpload .lu-table .num { text-align: right; }
    #leaveUpload .lu-scroll { max-height: 380px; overflow: auto; border: 1px solid var(--lu-line); border-radius: 6px; }
    #leaveUpload .lu-tabs { display: flex; gap: 4px; border-bottom: 1px solid var(--lu-line); margin: 16px 0 10px; }
    #leaveUpload button.lu-tab { padding: 7px 14px; cursor: pointer; border: 1px solid transparent; border-bottom: 0;
        border-radius: 5px 5px 0 0; color: var(--lu-muted); background: none; font: inherit; font-weight: 600; }
    #leaveUpload button.lu-tab.active { border-color: var(--lu-line); background: #fff; color: var(--lu-text); margin-bottom: -1px; }
    #leaveUpload .lu-pill { display: inline-block; padding: 1px 8px; border-radius: 10px; font-size: 11px; font-weight: 600; }
    #leaveUpload .lu-pill.skip { background: var(--lu-err-bg); color: var(--lu-err); }
    #leaveUpload .lu-pill.warn { background: var(--lu-warn-bg); color: var(--lu-warn); }
    #leaveUpload .lu-pill.info { background: var(--lu-info-bg); color: var(--lu-accent); }
    #leaveUpload .lu-pill.read { background: var(--lu-ok-bg); color: var(--lu-ok); }
    #leaveUpload .lu-pill.skipped { background: #eceff1; color: var(--lu-muted); }
    #leaveUpload .lu-toolbar { display: flex; gap: 10px; align-items: center; margin-bottom: 8px; flex-wrap: wrap; }
    #leaveUpload .lu-toolbar input { max-width: 260px; }
    #leaveUpload .lu-radio { display: flex; gap: 18px; flex-wrap: wrap; margin-top: 4px; }
    #leaveUpload .lu-radio label { font-weight: normal; display: flex; gap: 6px; align-items: flex-start; margin: 0; }
    #leaveUpload .lu-radio input { margin-top: 4px; }
    #leaveUpload .lu-hide { display: none !important; }
</style>

<div id="leaveUpload">
    <h2 class="lu-title">Upload Monthly Leave Data</h2>
    <p class="lu-sub">Excel &rarr; <code>dbo.EmployeeYearlyLeavedata</code> (EmpCode, RL, EXTL, MDAYS, MONTH, YEAR)</p>

    <!-- STEP 1 -->
    <div class="lu-card">
        <h3><span class="lu-step">1</span>Choose file and period</h3>
        <div class="lu-grid">
            <div class="lu-span2">
                <label class="lu-label" for="luFile">Excel file (.xlsx)</label>
                <input type="file" id="luFile" accept=".xlsx" />
                <div class="lu-hint">All sheets are read. Sheets without a CODE column (e.g. VOUCHER) are skipped.</div>
            </div>
            <div>
                <label class="lu-label" for="luMonth">Month</label>
                <select id="luMonth"></select>
            </div>
            <div>
                <label class="lu-label" for="luYear">Year</label>
                <select id="luYear"></select>
            </div>
        </div>

        <div class="lu-grid" style="margin-top:14px;">
            <div>
                <label class="lu-label" for="luMdaysCol">MDAYS &larr; Excel column</label>
                <select id="luMdaysCol"></select>
            </div>
            <div>
                <label class="lu-label" for="luRlCol">RL &larr; Excel column</label>
                <select id="luRlCol"></select>
            </div>
            <div>
                <label class="lu-label" for="luExtlCol">EXTL &larr; Excel column</label>
                <select id="luExtlCol"></select>
            </div>
        </div>

        <label class="lu-check"><input type="checkbox" id="luStripStars" checked="checked" />
            <span>Remove leading <code>**</code> from staff code (e.g. <code>**FS0308</code> &rarr; <code>FS0308</code>)</span></label>

        <div class="lu-actions">
            <button type="button" class="lu-btn" id="luBtnPreview">Read &amp; validate file</button>
            <div class="lu-progress" id="luProgress"><div></div></div>
        </div>
        <div class="lu-msg" id="luMsg1"></div>
    </div>

    <!-- STEP 2 -->
    <div class="lu-card lu-hide" id="luCardPreview">
        <h3><span class="lu-step">2</span>Check the data</h3>

        <div class="lu-stats">
            <div class="lu-stat"><b id="luStValid">0</b><span>Rows ready to save</span></div>
            <div class="lu-stat"><b id="luStIssues">0</b><span>Skipped / warnings</span></div>
            <div class="lu-stat"><b id="luStExisting">0</b><span>Already in DB for this month</span></div>
            <div class="lu-stat"><b id="luStPeriod">-</b><span>Period</span></div>
        </div>

        <div class="lu-tabs">
            <button type="button" class="lu-tab active" data-tab="luTabRows">Rows</button>
            <button type="button" class="lu-tab" data-tab="luTabIssues">Issues <span id="luIssueBadge"></span></button>
            <button type="button" class="lu-tab" data-tab="luTabSheets">Sheets</button>
        </div>

        <div id="luTabRows">
            <div class="lu-toolbar">
                <input type="text" id="luFilter" placeholder="Search code or name..." />
                <span class="lu-hint" id="luRowCount"></span>
            </div>
            <div class="lu-scroll">
                <table class="lu-table">
                    <thead><tr><th>Sheet</th><th class="num">Row</th><th>EmpCode</th><th>Name</th>
                        <th class="num">MDAYS</th><th class="num">RL</th><th class="num">EXTL</th></tr></thead>
                    <tbody id="luTbRows"></tbody>
                </table>
            </div>
        </div>

        <div id="luTabIssues" class="lu-hide">
            <div class="lu-scroll">
                <table class="lu-table">
                    <thead><tr><th>Type</th><th>Sheet</th><th class="num">Row</th><th>Code</th><th>Name</th><th>Message</th></tr></thead>
                    <tbody id="luTbIssues"></tbody>
                </table>
            </div>
        </div>

        <div id="luTabSheets" class="lu-hide">
            <div class="lu-scroll">
                <table class="lu-table">
                    <thead><tr><th>Sheet</th><th>Status</th><th class="num">Header row</th><th class="num">Rows read</th><th class="num">Skipped</th><th>Note</th></tr></thead>
                    <tbody id="luTbSheets"></tbody>
                </table>
            </div>
        </div>
    </div>

    <!-- STEP 3 -->
    <div class="lu-card lu-hide" id="luCardImport">
        <h3><span class="lu-step">3</span>Save to database</h3>
        <div class="lu-radio">
            <label><input type="radio" name="luMode" value="replace" checked="checked" /> Replace all existing data for this month/year</label>
            <label><input type="radio" name="luMode" value="skip" /> Keep existing rows, add only new employee codes</label>
        </div>
        <div class="lu-actions">
            <button type="button" class="lu-btn" id="luBtnImport">Save to database</button>
            <button type="button" class="lu-btn lu-secondary" id="luBtnReset">Start over</button>
        </div>
        <div class="lu-msg" id="luMsg2"></div>
    </div>
</div>

<script>
// Uses the master page's jQuery if it has one, otherwise loads jQuery 3.7.1
(function (start) {
    if (window.jQuery) { start(window.jQuery); return; }
    var s = document.createElement('script');
    s.src = 'https://code.jquery.com/jquery-3.7.1.min.js';
    s.integrity = 'sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=';
    s.crossOrigin = 'anonymous';
    s.onload = function () { start(window.jQuery); };
    document.head.appendChild(s);
})(function ($) {
    'use strict';

    // AJAX goes to this same page (works with or without FriendlyUrls)
    var PAGE_URL = window.location.href.split('#')[0];
    var MONTHS = ['January','February','March','April','May','June','July','August','September','October','November','December'];
    var COLUMNS = ['MONTH OF DAY', 'PRASENT DAY', 'LEAVE DAY', 'TOTAL PRASENT DAY', 'ABSENT DAY'];
    var token = null, allRows = [];
    var $root = $('#leaveUpload');

    // Enter key inside this tool must not submit the master page's <form>
    $root.on('keydown', 'input', function (e) { if (e.which === 13) e.preventDefault(); });

    // ---------- dropdowns ----------
    var now = new Date(), prev = new Date(now.getFullYear(), now.getMonth() - 1, 1);
    $.each(MONTHS, function (i, m) { $('#luMonth').append($('<option>').val(i + 1).text(m)); });
    for (var y = now.getFullYear() + 1; y >= now.getFullYear() - 5; y--) $('#luYear').append($('<option>').val(y).text(y));
    $('#luMonth').val(prev.getMonth() + 1);
    $('#luYear').val(prev.getFullYear());

    $.each(COLUMNS, function (_, c) { $('#luMdaysCol,#luRlCol,#luExtlCol').append($('<option>').val(c).text(c)); });
    $('#luMdaysCol').val('MONTH OF DAY');
    $('#luRlCol').val('LEAVE DAY');
    $('#luExtlCol').val('ABSENT DAY');

    // ---------- guess month/year from file name, e.g. "oct_2026.xlsx" ----------
    $('#luFile').on('change', function () {
        var name = (this.files[0] ? this.files[0].name : '').toLowerCase();
        var mm = name.match(/(jan|feb|mar|apr|may|jun|jul|aug|sep|oct|nov|dec)[a-z]*/);
        var yy = name.match(/(20\d{2})/);
        if (mm) $('#luMonth').val(['jan','feb','mar','apr','may','jun','jul','aug','sep','oct','nov','dec'].indexOf(mm[1]) + 1);
        if (yy && $('#luYear option[value=' + yy[1] + ']').length) $('#luYear').val(yy[1]);
        resetPreview();
    });
    $('#luMonth,#luYear,#luMdaysCol,#luRlCol,#luExtlCol,#luStripStars').on('change', resetPreview);

    // ---------- STEP 1: upload + preview ----------
    $('#luBtnPreview').on('click', function () {
        var f = $('#luFile')[0].files[0];
        if (!f) return showMsg('#luMsg1', 'err', 'Please choose an Excel file.');
        if (/\.xls$/i.test(f.name)) return showMsg('#luMsg1', 'err', 'Old .xls format is not supported. Open it in Excel and Save As .xlsx.');
        if (!/\.xlsx$/i.test(f.name)) return showMsg('#luMsg1', 'err', 'Only .xlsx files are allowed.');

        var fd = new FormData();
        fd.append('ajaxAction', 'preview');
        fd.append('file', f);
        fd.append('month', $('#luMonth').val());
        fd.append('year', $('#luYear').val());
        fd.append('mdaysCol', $('#luMdaysCol').val());
        fd.append('rlCol', $('#luRlCol').val());
        fd.append('extlCol', $('#luExtlCol').val());
        fd.append('stripStars', $('#luStripStars').is(':checked') ? '1' : '0');

        var $btn = $(this).prop('disabled', true).text('Reading...');
        $('#luProgress').show().children().css('width', '0');
        hideMsg('#luMsg1');
        resetPreview();

        $.ajax({
            url: PAGE_URL, type: 'POST', data: fd, dataType: 'json',
            processData: false, contentType: false,
            xhr: function () {
                var x = $.ajaxSettings.xhr();
                if (x.upload) x.upload.addEventListener('progress', function (e) {
                    if (e.lengthComputable) $('#luProgress > div').css('width', (e.loaded / e.total * 100) + '%');
                });
                return x;
            }
        }).done(function (r) {
            if (!r.ok) return showMsg('#luMsg1', 'err', r.message);
            renderPreview(r);
        }).fail(function (x) {
            showMsg('#luMsg1', 'err', ajaxError(x));
        }).always(function () {
            $btn.prop('disabled', false).text('Read & validate file');
            $('#luProgress').hide();
        });
    });

    // ---------- STEP 3: import ----------
    $('#luBtnImport').on('click', function () {
        if (!token) return;
        var mode = $('input[name=luMode]:checked').val();
        var period = $('#luStPeriod').text();
        var existing = parseInt($('#luStExisting').text(), 10) || 0;
        var q = mode === 'replace' && existing > 0
            ? 'This will DELETE ' + existing + ' existing rows for ' + period + ' and insert ' + allRows.length + ' new rows. Continue?'
            : 'Save ' + allRows.length + ' rows for ' + period + '?';
        if (!window.confirm(q)) return;

        var $btn = $(this).prop('disabled', true).text('Saving...');
        hideMsg('#luMsg2');

        $.ajax({
            url: PAGE_URL, type: 'POST', dataType: 'json',
            data: { ajaxAction: 'import', token: token, mode: mode }
        }).done(function (r) {
            if (!r.ok) { $btn.prop('disabled', false).text('Save to database'); return showMsg('#luMsg2', 'err', r.message); }
            var t = r.message;
            if (r.deleted) t += ' ' + r.deleted + ' old rows replaced.';
            if (r.skippedExisting) t += ' ' + r.skippedExisting + ' rows skipped (already in DB).';
            showMsg('#luMsg2', 'ok', t);
            token = null;
            $btn.text('Saved');
        }).fail(function (x) {
            $btn.prop('disabled', false).text('Save to database');
            showMsg('#luMsg2', 'err', ajaxError(x));
        });
    });

    $('#luBtnReset').on('click', function () {
        $('#luFile').val('');
        resetPreview();
        hideMsg('#luMsg1');
        $('html,body').animate({ scrollTop: $root.offset().top - 10 }, 200);
    });

    // ---------- tabs + filter ----------
    $root.on('click', '.lu-tab', function () {
        $('.lu-tab', $root).removeClass('active'); $(this).addClass('active');
        $('#luTabRows,#luTabIssues,#luTabSheets').addClass('lu-hide');
        $('#' + $(this).data('tab')).removeClass('lu-hide');
    });

    var filterTimer;
    $('#luFilter').on('input', function () {
        clearTimeout(filterTimer);
        filterTimer = setTimeout(renderRows, 150);
    });

    // ---------- rendering ----------
    function renderPreview(r) {
        token = r.token;
        allRows = r.rows || [];

        $('#luStValid').text(r.validRows);
        $('#luStIssues').text(r.issueCount);
        $('#luStExisting').text(r.existingRows >= 0 ? r.existingRows : '?');
        $('#luStPeriod').text(MONTHS[r.month - 1] + ' ' + r.year);
        $('#luIssueBadge').text(r.issueCount ? '(' + r.issueCount + ')' : '');

        $('#luFilter').val('');
        renderRows();

        var ih = $.map(r.issues || [], function (i) {
            return '<tr><td><span class="lu-pill ' + i.level + '">' + label(i.level) + '</span></td><td>' + esc(i.sheet) +
                   '</td><td class="num">' + i.row + '</td><td>' + esc(i.code) + '</td><td>' + esc(i.name) +
                   '</td><td>' + esc(i.message) + '</td></tr>';
        });
        if (r.issueCount > (r.issues || []).length) ih.push('<tr><td colspan="6" class="lu-hint">Showing first ' + r.issues.length + ' of ' + r.issueCount + '.</td></tr>');
        $('#luTbIssues').html(ih.join('') || '<tr><td colspan="6" class="lu-hint">No issues found.</td></tr>');

        $('#luTbSheets').html($.map(r.sheets || [], function (s) {
            return '<tr><td>' + esc(s.name) + '</td><td><span class="lu-pill ' + s.status + '">' + s.status +
                   '</span></td><td class="num">' + (s.headerRow || '') + '</td><td class="num">' + s.rows +
                   '</td><td class="num">' + s.skipped + '</td><td>' + esc(s.note || '') + '</td></tr>';
        }).join(''));

        var info = 'File read: ' + r.validRows + ' rows ready.';
        if (r.issueCount) info += ' ' + r.issueCount + ' rows skipped or flagged &mdash; see the Issues tab.';
        showMsg('#luMsg1', r.issueCount ? 'warn' : 'ok', info, true);
        if (r.dbWarning) showMsg('#luMsg2', 'warn', r.dbWarning);

        $('#luCardPreview,#luCardImport').removeClass('lu-hide');
        $('#luBtnImport').prop('disabled', false).text('Save to database');
        $('html,body').animate({ scrollTop: $('#luCardPreview').offset().top - 10 }, 250);
    }

    function renderRows() {
        var q = $.trim($('#luFilter').val()).toLowerCase();
        var list = q ? $.grep(allRows, function (x) {
            return (x.code || '').toLowerCase().indexOf(q) >= 0 || (x.name || '').toLowerCase().indexOf(q) >= 0;
        }) : allRows;
        var html = [];
        for (var i = 0; i < list.length; i++) {
            var x = list[i];
            html.push('<tr><td>' + esc(x.sheet) + '</td><td class="num">' + x.row + '</td><td>' + esc(x.code) +
                      '</td><td>' + esc(x.name) + '</td><td class="num">' + x.mdays + '</td><td class="num">' +
                      x.rl + '</td><td class="num">' + x.extl + '</td></tr>');
        }
        $('#luTbRows').html(html.join('') || '<tr><td colspan="7" class="lu-hint">No rows.</td></tr>');
        $('#luRowCount').text(q ? list.length + ' of ' + allRows.length + ' rows' : allRows.length + ' rows');
    }

    function resetPreview() {
        token = null; allRows = [];
        $('#luCardPreview,#luCardImport').addClass('lu-hide');
        hideMsg('#luMsg2');
    }

    // ---------- helpers ----------
    function label(l) { return { skip: 'Skipped', warn: 'Warning', info: 'Duplicate' }[l] || l; }
    function esc(s) { return $('<div>').text(s == null ? '' : s).html(); }
    function showMsg(sel, type, text, isHtml) {
        var $m = $(sel).removeClass('ok err warn').addClass(type).show();
        if (isHtml) $m.html(text); else $m.text(text);
        return false;
    }
    function hideMsg(sel) { $(sel).hide().text(''); }
    function ajaxError(x) {
        if (x.status === 200) return 'The server returned a page instead of data. Open the page directly in the browser to see the server error.';
        if (x.status === 401 || x.status === 403) return 'Not authorised - please log in again.';
        if (x.status === 413 || (x.status === 500 && /maxRequestLength|too large/i.test(x.responseText || '')))
            return 'File is too large for the server. Increase maxRequestLength in web.config.';
        return 'Request failed (' + x.status + ' ' + x.statusText + ').';
    }
});
</script>
</asp:Content>
