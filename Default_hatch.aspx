<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="SiteMasterHatch.master" CodeFile="Default_hatch.aspx.vb" Inherits="Default_hatch" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Hatchery Management</div>
            <h1>Welcome to Phoenix Hatchery</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Egg collection, setting, transfer and chick production.</p>
        </div>
        <div>
            <img src="~/images/himage.jpg" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/hatchery/hatcheryegg_collection.aspx" runat="server"><strong>Egg Collection</strong><span>Receive hatching eggs</span></a>
            <a class="app-tile" href="~/hatchery/hatch_cat_entry.aspx" runat="server"><strong>Egg Grading</strong><span>Grade received eggs</span></a>
            <a class="app-tile" href="~/hatchery/hatcandling_entry.aspx" runat="server"><strong>Setter Entry</strong><span>Load setters</span></a>
            <a class="app-tile" href="~/hatchery/chicksproduce.aspx" runat="server"><strong>Chicks Production</strong><span>Record the hatch</span></a>
            <a class="app-tile" href="~/reports/harch_hhereport.aspx" runat="server"><strong>Daily HHE Report</strong><span>Hatching egg report</span></a>
            <a class="app-tile" href="~/reports/Hatchery_hatchablity_report.aspx" runat="server"><strong>Hatchability Report</strong><span>Hatch performance</span></a>
    </div>
</asp:Content>
