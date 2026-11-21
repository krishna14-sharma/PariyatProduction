<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="SiteMasterPit.master" CodeFile="Default_pit.aspx.vb" Inherits="Default_pit" %>

<asp:Content ID="BodyContent_Pit" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Pit Management</div>
            <h1>Welcome to Pit Management</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Eggs and birds received, sold or destroyed at the pit.</p>
        </div>
        <div>
            <img src="~/images/rows.jpg" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/pit/egg_recived.aspx" runat="server"><strong>Eggs Received</strong><span>From the hatchery</span></a>
            <a class="app-tile" href="~/pit/bird_recived_pit.aspx" runat="server"><strong>Birds Received</strong><span>From the farm</span></a>
            <a class="app-tile" href="~/pit/pit_egg_sale_destroy_distibute.aspx" runat="server"><strong>Eggs Sale / Destroy</strong><span>Record egg disposal</span></a>
            <a class="app-tile" href="~/pit/pit_bird_sale.aspx" runat="server"><strong>Birds Sale / Destroy</strong><span>Record bird disposal</span></a>
            <a class="app-tile" href="~/pit/pit_egg_coll_report.aspx" runat="server"><strong>Pit Egg Collection Report</strong><span>Report</span></a>
    </div>
</asp:Content>
