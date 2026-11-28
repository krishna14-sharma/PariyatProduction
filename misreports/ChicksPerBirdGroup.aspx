<%@ Page Title="Chicks Per Bird – Flock Group" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="ChicksPerBirdGroup.aspx.vb" Inherits="misreports_ChicksPerBirdGroup" %>

<asp:Content ID="CpgHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/chickgroup.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="CpgBody" ContentPlaceHolderID="ReportMaster" runat="server">
<div class="container-fluid cpg">
    <h3>Chicks Per Bird – Flock Group (consolidated)</h3>
    <p class="cpg-sub">Enter a flock number such as <strong>190</strong>: the report adds up all its sub-flocks
        (190 A, 190 B, 190 C, 190 D …) week by week, using the same weekly figures as the Chicks Per Bird report.</p>
    <asp:Literal ID="litMessage" runat="server" EnableViewState="false" />

    <div class="cpg-filters">
        <div class="cpg-field">
            <label for="<%= txtFlock.ClientID %>">Flock group</label>
            <asp:TextBox ID="txtFlock" runat="server" CssClass="form-control" placeholder="e.g. 190" MaxLength="30" />
        </div>
        <div class="cpg-field">
            <label for="<%= txtHoused.ClientID %>">Housed females</label>
            <asp:TextBox ID="txtHoused" runat="server" CssClass="form-control" placeholder="from Flock Master" MaxLength="12" />
        </div>
        <div class="cpg-check">
            <asp:CheckBox ID="chkAllColumns" runat="server" Text="Show all columns" />
        </div>
        <div class="cpg-actions">
            <asp:Button ID="btnShow" runat="server" Text="Show Report" CssClass="btn btn-primary" />
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-outline-secondary" />
            <button type="button" class="btn btn-outline-secondary" onclick="window.print();">Print</button>
        </div>
        <asp:Panel ID="pnlFlocks" runat="server" Visible="false" CssClass="cpg-field" style="flex-basis:100%">
            <span class="cpg-label">Sub-flocks included (untick to leave one out, then Show Report)</span>
            <asp:CheckBoxList ID="cblFlocks" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" CssClass="cpg-flocks" />
        </asp:Panel>
    </div>

    <asp:Panel ID="pnlResult" runat="server" Visible="false">
        <div class="cpg-stats">
            <div class="cpg-stat is-main"><span>Chicks per bird</span><strong><asp:Literal ID="litPerBird" runat="server" /></strong><small>total chicks ÷ housed females</small></div>
            <div class="cpg-stat"><span>HE per bird</span><strong><asp:Literal ID="litHEPerBird" runat="server" /></strong><small>hatching eggs ÷ housed females</small></div>
            <div class="cpg-stat"><span>Total chicks</span><strong><asp:Literal ID="litChicks" runat="server" /></strong><small><asp:Literal ID="litLoading" runat="server" /></small></div>
            <div class="cpg-stat"><span>Hatch %</span><strong><asp:Literal ID="litHatch" runat="server" /></strong><small>chicks ÷ eggs loaded</small></div>
            <div class="cpg-stat"><span>Housed females</span><strong><asp:Literal ID="litHoused" runat="server" /></strong><small><asp:Literal ID="litHousedFrom" runat="server" /></small></div>
        </div>

        <h4>Week by week – <asp:Literal ID="litGroupName" runat="server" /></h4>
        <div class="cpg-table-wrap"><asp:Literal ID="litWeekly" runat="server" /></div>

        <h4>Sub-flocks</h4>
        <div class="cpg-table-wrap"><asp:Literal ID="litFlocks" runat="server" /></div>

        <div class="cpg-notes">
            <strong>How it is worked out</strong>
            <ul>
                <li>For each sub-flock the existing procedure <em>sp_WeeklyPerforance_Report</em> is run (the same figures as the Chicks Per Bird report); the sub-flocks are added up by age week.</li>
                <li>Hatch % = (fresh + B chicks) ÷ (fresh + B loading), worked out again from the added-up numbers – not an average of the sub-flocks. Fertility % = fertile ÷ loading.</li>
                <li>Chicks / bird and HE / bird are cumulative: everything up to that week ÷ housed females (Flock Master, or the number you type).</li>
                <li>"Sub-flocks" = how many sub-flocks have figures in that week. Weeks with no figures at all are left out.</li>
            </ul>
        </div>
    </asp:Panel>
</div>
</asp:Content>
