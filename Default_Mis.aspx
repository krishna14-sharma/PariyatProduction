<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/ReportPage.master" CodeFile ="Default_Mis.aspx.vb" Inherits="Default_Mis" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">MIS Reports</div>
            <h1>Phoenix Reporting</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Farm, hatchery and feed reports in one place.</p>
        </div>
        <div>
            <img src="~/images/home_page.jpg" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/misreports/mis_shedreport.aspx" runat="server"><strong>Shed Wise Production</strong><span>Farm report</span></a>
            <a class="app-tile" href="~/misreports/mis_weelkyreport.aspx" runat="server"><strong>Weekly Report</strong><span>Farm report</span></a>
            <a class="app-tile" href="~/misreports/mis_flockprod_report.aspx" runat="server"><strong>Flock Production</strong><span>Farm report</span></a>
            <a class="app-tile" href="~/misreports/mis_harch_hhereport.aspx" runat="server"><strong>Daily HHE Report</strong><span>Hatchery report</span></a>
            <a class="app-tile" href="~/misreports/mis_Hatchery_hatchablity_report.aspx" runat="server"><strong>Hatchery Hatchability</strong><span>Hatchery report</span></a>
            <a class="app-tile" href="~/feed/rawstock.aspx" runat="server"><strong>Raw Stock</strong><span>Feed report</span></a>
    </div>
</asp:Content>
