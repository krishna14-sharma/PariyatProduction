<%@ Page Language="VB" MasterPageFile="SiteMaster.master" AutoEventWireup="true" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Farm Administration</div>
            <h1>Welcome to Phoenix Poultry</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Manage masters, confirm farm entries and maintain flock records.</p>
        </div>
        <div>
            <img src="~/images/home_page.jpg" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/transforms/FlockMaster.aspx" runat="server"><strong>Flock Master</strong><span>Create and update flocks</span></a>
            <a class="app-tile" href="~/transforms/ShedEntry.aspx" runat="server"><strong>Bird Shed Entry</strong><span>Place birds into sheds</span></a>
            <a class="app-tile" href="~/transforms/Confrim_BirdTransfer.aspx" runat="server"><strong>Confirm Bird Transfer</strong><span>Approve transfers</span></a>
            <a class="app-tile" href="~/dailyentryforms/Daily_feedegg_confirm.aspx" runat="server"><strong>Confirm Feed Egg Entry</strong><span>Approve daily feed &amp; egg data</span></a>
            <a class="app-tile" href="~/transforms/Confirm_PMEntry.aspx" runat="server"><strong>Confirm PM Data</strong><span>Approve post-mortem entries</span></a>
            <a class="app-tile" href="~/masterforms/ShedMaster.aspx" runat="server"><strong>Shed Master</strong><span>Sheds, lines and pens</span></a>
    </div>

    <div class="app-table-wrap mt-4">
        <asp:DataGrid ID="eggp" runat="server" AutoGenerateColumns="true" GridLines="None" CssClass="table table-striped table-hover"></asp:DataGrid>
    </div>
</asp:Content>
