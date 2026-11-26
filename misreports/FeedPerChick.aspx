<%@ Page Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="~/reports/FeedPerChick.aspx.vb" Inherits="reports_FeedPerChick" Title="Feed per Chick" %>
<%-- MIS Reports version. Shares its code with reports/FeedPerChick.aspx. --%>

<asp:Content ID="FeedChickHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/feedchick.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="FeedChickContent" ContentPlaceHolderID="ReportMaster" runat="server">
<div class="fpc">
    <h3>Feed per Chick – flock wise</h3>
    <p class="fpc-sub">Feed eaten by each flock compared with the eggs it laid and the chicks hatched from its eggs (main and satellite hatchery).</p>
    <asp:Literal ID="litMessage" runat="server" EnableViewState="false" />

    <div class="fpc-filters">
        <div class="fpc-field">
            <label for="<%= txtFrom.ClientID %>">From</label>
            <asp:TextBox ID="txtFrom" runat="server" TextMode="Date" CssClass="form-control" />
        </div>
        <div class="fpc-field">
            <label for="<%= txtTo.ClientID %>">To</label>
            <asp:TextBox ID="txtTo" runat="server" TextMode="Date" CssClass="form-control" />
        </div>
        <div class="fpc-field">
            <label for="<%= ddlFarm.ClientID %>">Farm</label>
            <asp:DropDownList ID="ddlFarm" runat="server" CssClass="form-select" />
        </div>
        <div class="fpc-field fpc-field-wide">
            <label for="<%= ddlFlock.ClientID %>">Flock</label>
            <asp:DropDownList ID="ddlFlock" runat="server" CssClass="form-select" />
        </div>
        <div class="fpc-field">
            <span class="fpc-label">Count feed, eggs &amp; chicks</span>
            <asp:RadioButtonList ID="rblBasis" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="fpc-toggle">
                <asp:ListItem Value="life" Selected="True">Flock life to date</asp:ListItem>
                <asp:ListItem Value="period">Selected period</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="fpc-check">
            <asp:CheckBox ID="chkMale" runat="server" Checked="true" Text="Include male feed" />
        </div>
        <div class="fpc-actions">
            <asp:Button ID="btnShow" runat="server" Text="Show" CssClass="btn btn-primary" />
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-outline-secondary" />
            <button type="button" class="btn btn-outline-secondary" onclick="window.print();">Print</button>
        </div>
    </div>

    <asp:Panel ID="pnlResult" runat="server" Visible="false">
        <p class="fpc-period"><asp:Literal ID="litPeriod" runat="server" /></p>

        <div class="fpc-stats">
            <div class="fpc-stat is-main"><span>Feed per chick</span><strong><asp:Literal ID="litPerChick" runat="server" /></strong><small>kg feed per chick hatched</small></div>
            <div class="fpc-stat"><span>Feed per saleable chick</span><strong><asp:Literal ID="litPerSaleable" runat="server" /></strong><small>kg</small></div>
            <div class="fpc-stat"><span>Feed per hatching egg</span><strong><asp:Literal ID="litPerHE" runat="server" /></strong><small>grams</small></div>
            <div class="fpc-stat"><span>Total feed</span><strong><asp:Literal ID="litFeed" runat="server" /></strong><small><asp:Literal ID="litFeedSplit" runat="server" /></small></div>
            <div class="fpc-stat"><span>Chicks</span><strong><asp:Literal ID="litChicks" runat="server" /></strong><small><asp:Literal ID="litSaleable" runat="server" /></small></div>
        </div>

        <div class="fpc-table-wrap">
            <asp:GridView ID="gvFlocks" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="fpc-table"
                ShowFooter="true" UseAccessibleHeader="true" EmptyDataText="">
                <Columns>
                    <asp:TemplateField HeaderText="Flock" HeaderStyle-CssClass="fpc-left" ItemStyle-CssClass="fpc-left" FooterStyle-CssClass="fpc-left">
                        <ItemTemplate><strong><%# HtmlEncode(Eval("Flock")) %></strong><div class="fpc-muted"><%# HtmlEncode(Eval("Breed")) %></div></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Farm" HeaderStyle-CssClass="fpc-left" ItemStyle-CssClass="fpc-left">
                        <ItemTemplate><%# HtmlEncode(Eval("Farms")) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Age (wk)">
                        <ItemTemplate><%# Eval("AgeWeeks") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Female feed (kg)">
                        <ItemTemplate><%# N(Eval("FemaleFeedKg"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Male feed (kg)">
                        <ItemTemplate><%# N(Eval("MaleFeedKg"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Total feed (kg)">
                        <ItemTemplate><%# N(Eval("FeedKg"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Eggs">
                        <ItemTemplate><%# N(Eval("Eggs"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Hatching eggs">
                        <ItemTemplate><%# N(Eval("HatchingEggs"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Chicks">
                        <ItemTemplate><%# N(Eval("Chicks"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Saleable chicks">
                        <ItemTemplate><%# N(Eval("SaleableChicks"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feed / egg (g)">
                        <ItemTemplate><%# N(Eval("FeedPerEggG"), 1) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feed / HE (g)">
                        <ItemTemplate><%# N(Eval("FeedPerHatchingEggG"), 1) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feed / chick (kg)" HeaderStyle-CssClass="fpc-key" ItemStyle-CssClass="fpc-key" FooterStyle-CssClass="fpc-key">
                        <ItemTemplate><%# N(Eval("FeedPerChickKg"), 3) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Feed / saleable chick (kg)">
                        <ItemTemplate><%# N(Eval("FeedPerSaleableChickKg"), 3) %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Chicks / tonne feed">
                        <ItemTemplate><%# N(Eval("ChicksPerTonne"), 0) %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <asp:Literal ID="litEmpty" runat="server" />
        </div>

        <div class="fpc-notes">
            <strong>How it is worked out</strong>
            <ul>
                <li>Feed comes from the daily Feed/Egg entries (feed per bird × birds, saved in grams) and is shown in kg.</li>
                <li>Eggs come from Egg Collection; chicks and saleable chicks from Hatchery Production (main + satellite), by loading date.</li>
                <li><em>Flock life to date</em>: everything from the start of the flock up to the <em>To</em> date. <em>Selected period</em>: only From – To. In a short period the chicks are from eggs laid about 3–4 weeks earlier, so life to date gives the truer figure.</li>
                <li>Flocks listed: those with feed entries between From and To. The farm filter picks the flocks housed at that farm; their feed from every farm is counted.</li>
                <li>Totals row: ratios are worked out from the totals, not averaged.</li>
            </ul>
        </div>
    </asp:Panel>
</div>
</asp:Content>
