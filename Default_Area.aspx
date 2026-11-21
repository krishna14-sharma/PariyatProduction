<%@ Page Language="VB" MasterPageFile="~/AreaPage.master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Farm Operations</div>
            <h1>Welcome to Phoenix Poultry</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Record today&rsquo;s shed data and view your farm reports.</p>
        </div>
        <div>
            <img src="~/images/home_page.jpg" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/dailyentryforms/Daily_eggcollection.aspx" runat="server"><strong>Daily Egg Collection</strong><span>Record eggs by shed and line</span></a>
            <a class="app-tile" href="~/dailyentryforms/Daily_feedegg.aspx" runat="server"><strong>Daily Feed Egg Entry</strong><span>Feed, eggs and production</span></a>
            <a class="app-tile" href="~/dailyentryforms/Daily_mortality_pit.aspx" runat="server"><strong>Daily Mortality</strong><span>Mortality and pit entry</span></a>
            <a class="app-tile" href="~/dailyentryforms/DailyPmEntry.aspx" runat="server"><strong>Daily PM Entry</strong><span>Post-mortem findings</span></a>
            <a class="app-tile" href="~/reports/frmShedReport.aspx" runat="server"><strong>Shed Production Report</strong><span>Shed-wise production</span></a>
            <a class="app-tile" href="~/reports/Flock_Weekly_report.aspx" runat="server"><strong>Weekly Report</strong><span>Flock weekly summary</span></a>
    </div>
</asp:Content>
