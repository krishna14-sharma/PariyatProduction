# Phoenix Poultry – upgrade to .NET Framework 4.8 + ASP.NET AJAX + Crystal Reports fix

## 1. "Load report failed" – cause and fix

**Cause.** The report pages created Crystal `ReportDocument` objects and never closed them. Each open document
holds a Crystal print-engine "job" and temp files in `C:\Windows\Temp`. Crystal allows about 75 open jobs per
server (PrintJobLimit). After enough report views every report failed with *Load report failed* until IIS was
recycled. Two more problems made it worse:
* all report pages shared one session slot (`Session("Report")`), so pages overwrote and disposed each other's reports;
* errors were swallowed (`Catch ... End Try` with nothing inside), so users just saw an empty page.

**Fix (automatic for every report page).**
* `App_code/CrystalReportManager.vb` + `Global.asax` track every report created in a request. After the page has
  rendered, the report shown in each viewer is kept (one per viewer per page) so paging / export / print keep
  working; every other report is closed and disposed immediately. When a user signs out or the session expires,
  their remaining reports are released.
* All 55 `New ReportDocument()` calls now use `CrystalReportManager.NewReport()`.
* `Session("Report")` is now `Session("Report:" & Request.Path)` – one slot per page.
* Report errors are written to `Logs/ErrorLog.txt` and shown to the user as a message.
* The viewer loads its scripts from the default location `/aspnet_client/system_web/4_0_30319/crystalreportviewers13`
  (same as before). If the site runs in a virtual folder (e.g. `/Poultry`) and the viewer is blank, copy the
  `aspnet_client` folder to the IIS site root (`C:\inetpub\wwwroot`).

## 2. Deploying

1. **Server:** install the **.NET Framework 4.8** runtime. **Developer PC:** Visual Studio 2019/2022 with the
   *.NET Framework 4.8 Developer Pack*; open the folder with *File › Open › Web Site*.
2. **Crystal runtime:** install *SAP Crystal Reports runtime engine for .NET Framework* 13.0.x – the same Service
   Pack as on your developer PC (assemblies version 13.0.4000.0). Match the bitness of the app pool:
   64-bit runtime with *Enable 32-Bit Applications = False*, or 32-bit runtime with it set to *True*.
3. **IIS application pool:** .NET CLR **v4.0**, **Integrated** pipeline. Give the pool identity
   (e.g. `IIS AppPool\Poultry`) **Modify** permission on `C:\Windows\Temp` and on the site's `Logs` folder.
4. Copy the whole folder (including `aspnet_client`, `Content`, `Scripts`, `Global.asax`).
5. Check the connection strings at the top of `Web.config` (they are no longer in the `.vb` files).
6. Recycle the application pool. Optionally delete old `*.rpt` / `*.tmp` files from `C:\Windows\Temp`.

## 3. Switches in Web.config (appSettings)

| Setting | Default | Meaning |
|---|---|---|
| `Ajax:PartialRendering` | `true` | `false` turns AJAX off for the whole site (instant rollback). |
| `Ajax:FullPostBackPages` | Excel-export + upload pages | Pages that must always do full postbacks. Report-viewer and upload pages are detected automatically. |
| `Auth:RequireLogin` | `true` | Every page that uses a master page requires a signed-in user. |
| `Login:WindowsOnly` | `true` | Original rule: sign-in only from Windows PCs. `false` allows phones/tablets. |

`compilation debug="false"` and `customErrors mode="RemoteOnly"` are set for production. On the server itself you
still see full error details; other users see `Error.aspx`, and the details go to `Logs/ErrorLog.txt`.

## 4. What changed

| Area | Change |
|---|---|
| Framework | `targetFramework="4.8"`; duplicate Crystal entries removed from Web.config. |
| AJAX | Every master page has a ScriptManager + UpdatePanel. Data-entry pages update without a full page reload, with a "Please wait…" indicator. Pages with a report viewer, file upload or Excel export automatically use normal postbacks. |
| Messages | 42 `Response.Write(...)` calls (they break AJAX) replaced by `AppUi.ShowMessage / ShowError` toasts; debug writes removed. |
| Database | Connection strings moved to Web.config (`PoultryDb`, `FeedDb`, `StoreDb`). `DataSetRet` / `SqlExecuteNonQuery` now log errors instead of hiding them. New `AppDb.GetTable / Execute / Scalar` helpers for parameterised SQL. |
| Sign-in | Parameterised query (the old one let anyone in with the user name `' or 1=1 --`). Redirects to the role's home page instead of `Server.Transfer`. Clean new sign-in page (no fake Facebook/Twitter/sign-up links). New **Sign out**. |
| Look & feel | One shared theme (`Content/app.css`, `Scripts/app.js`) for all 7 masters: brand bar with user + location, consistent menus (current page highlighted), mobile menu, module home pages with quick links, styled buttons/grids, toast messages, friendly error page. All menus keep exactly the same links. |
| Bugs fixed | Tray Unit home page could not open (wrong ContentPlaceHolderID). Graph Data Edit page used a missing connection string. Pit / Attendance logos linked to the Hatchery home. Menu links broke when the site ran in a virtual folder. `MsgBox` (fails on IIS) removed from GlobalModule. |

## 5. Checked before delivery

* All 156 page/master code files, App_Code (at VB 10 – the level the built-in .NET 4.8 compiler supports),
  Global.asax and the new pages compile against the .NET Framework 4.8 reference assemblies.
* The site was run on an ASP.NET test host: sign-in redirects, all master pages, AJAX partial postbacks and
  messages, automatic full postbacks on report pages, and the report life-cycle (11 scenarios: show, page, replace,
  orphan, sign out – every report released).
* **Not available here:** the real Crystal runtime and your SQL Server. Please test on the server:
  open a report → page 2 → export to PDF → print; then open about 100 reports in a row (it used to fail near 75).

## 6. Recommended next steps (not changed)

* **SQL injection:** about 130 pages still build SQL by joining text (`"... where x='" & TextBox.Text & "'"`).
  The error log already shows *Incorrect syntax near …* when users type a quote. Convert page by page:
  ```vb
  Dim dt As DataTable = AppDb.GetTable(AppDb.Poultry,
      "select * from hatch_production where loading_date = @d", AppDb.P("@d", entrydate.Value))
  ```
* Passwords are stored as plain text in `[Users]`; store hashes instead.
* Use a SQL login with limited rights instead of `sa`.
* `Web1.config` and `Web.Debug1.config` are unused copies that contain passwords – delete them if not needed.
