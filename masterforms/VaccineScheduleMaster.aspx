<%@ Page Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="VaccineScheduleMaster.aspx.vb" Inherits="masterforms_VaccineScheduleMaster" Title="Vaccine Schedule Master | Phoenix Poultry" %>

<asp:Content ID="ScheduleMasterHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/vaccination.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="ScheduleMasterContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="vacc">
        <h3>Vaccine Schedule Master</h3>
        <asp:Literal ID="litMessage" runat="server" EnableViewState="false" />
        <p class="vacc-muted">The standard vaccination &amp; medication schedule used for every flock. Male and female have separate schedules.
            Due date = hatch date + (day number &minus; 1). Changes apply to all flocks immediately.</p>

        <div class="vacc-filters">
            <div class="vacc-field">
                <label>Schedule</label>
                <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vacc-toggle" AutoPostBack="true">
                    <asp:ListItem Value="F" Selected="True">Female</asp:ListItem>
                    <asp:ListItem Value="M">Male</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="vacc-field vacc-field-btn">
                <asp:CheckBox ID="chkInactive" runat="server" Text=" Show inactive items" AutoPostBack="true" />
            </div>
        </div>

        <%-- ===================== Add / edit ===================== --%>
        <div class="vacc-due">
            <div class="vacc-due-head"><strong><asp:Literal ID="litFormTitle" runat="server" Text="Add an item" /></strong></div>
            <asp:HiddenField ID="hfEditId" runat="server" />
            <div class="row g-3">
                <div class="col-sm-2">
                    <label for="<%= txtSeq.ClientID %>">S. No.</label>
                    <asp:TextBox ID="txtSeq" runat="server" TextMode="Number" CssClass="form-control" />
                </div>
                <div class="col-sm-2">
                    <label for="<%= txtAge.ClientID %>">Age (as printed)</label>
                    <asp:TextBox ID="txtAge" runat="server" CssClass="form-control" MaxLength="30" placeholder="e.g. 8 week I" />
                </div>
                <div class="col-sm-2">
                    <label for="<%= txtDay.ClientID %>">Day number</label>
                    <asp:TextBox ID="txtDay" runat="server" TextMode="Number" CssClass="form-control" placeholder="1 = hatch day" />
                </div>
                <div class="col-sm-2">
                    <label for="<%= txtPhase.ClientID %>">Phase</label>
                    <asp:TextBox ID="txtPhase" runat="server" CssClass="form-control" MaxLength="50" list="vaccPhases" placeholder="Rearing" />
                </div>
                <div class="col-sm-4">
                    <label for="<%= txtVaccine.ClientID %>">Vaccine / medicine</label>
                    <asp:TextBox ID="txtVaccine" runat="server" CssClass="form-control" MaxLength="200" />
                </div>
                <div class="col-sm-3">
                    <label for="<%= txtDose.ClientID %>">Dose</label>
                    <asp:TextBox ID="txtDose" runat="server" CssClass="form-control" MaxLength="100" />
                </div>
                <div class="col-sm-3">
                    <label for="<%= txtRoute.ClientID %>">Route</label>
                    <asp:TextBox ID="txtRoute" runat="server" CssClass="form-control" MaxLength="100" list="vaccRoutes" />
                </div>
                <div class="col-sm-3">
                    <label for="<%= txtCompany.ClientID %>">Company</label>
                    <asp:TextBox ID="txtCompany" runat="server" CssClass="form-control" MaxLength="150" />
                </div>
                <div class="col-sm-3">
                    <label for="<%= txtRemark.ClientID %>">Remark</label>
                    <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" MaxLength="200" />
                </div>
                <div class="col-sm-12 d-flex flex-wrap gap-2 align-items-center">
                    <asp:CheckBox ID="chkActive" runat="server" Text=" Active" Checked="true" />
                    <asp:Button ID="btnSave" runat="server" Text="Add item" CssClass="btn btn-primary" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btn btn-outline-secondary" Visible="false" CausesValidation="false" />
                    <span class="vacc-muted">Day number: 1 day = 1, 10 day = 10, 8 week I = 56 (8 &times; 7), 23 week I = 161.</span>
                </div>
            </div>
        </div>

        <%-- ===================== List ===================== --%>
        <div class="vacc-table-wrap">
            <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="false" GridLines="None" DataKeyNames="id,entries" CssClass="table vacc-table">
                <Columns>
                    <asp:BoundField DataField="seq_no" HeaderText="#" />
                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>
                            <div class="vacc-age"><%#: Eval("age_label") %></div>
                            <div class="vacc-muted">Day <%# Eval("day_no") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="phase" HeaderText="Phase" />
                    <asp:BoundField DataField="vaccine" HeaderText="Vaccine / medicine" ItemStyle-CssClass="vacc-name" />
                    <asp:BoundField DataField="dose" HeaderText="Dose" />
                    <asp:BoundField DataField="route" HeaderText="Route" />
                    <asp:BoundField DataField="company" HeaderText="Company" />
                    <asp:BoundField DataField="remark" HeaderText="Remark" />
                    <asp:TemplateField HeaderText="Used">
                        <ItemTemplate><span class="vacc-muted"><%# Eval("entries") %> entries</span></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate><span class='vacc-badge <%# If(CBool(Eval("is_active")), "is-ontime", "") %>'><%# If(CBool(Eval("is_active")), "Active", "Inactive") %></span></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="EditItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="vacc-link" Text="Edit" />
                            &nbsp;
                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="DeleteItem" CommandArgument='<%# Container.DataItemIndex %>' CssClass="vacc-link vacc-close"
                                Text='<%# If(CInt(Eval("entries")) > 0, If(CBool(Eval("is_active")), "Deactivate", "Activate"), "Delete") %>'
                                OnClientClick="return confirm('Are you sure?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate><p class="vacc-muted">No items. Run Database/Vaccination_Schedule.sql to load the standard schedules.</p></EmptyDataTemplate>
            </asp:GridView>
        </div>

        <datalist id="vaccPhases"><option value="Rearing"></option><option value="Midlay-I"></option><option value="Midlay-II"></option></datalist>
        <datalist id="vaccRoutes"><option value="D/W"></option><option value="S/C"></option><option value="I/M"></option><option value="Eye Drop"></option><option value="Oral Drop"></option><option value="Eye Drop S/C"></option><option value="I/M or S/C"></option><option value="Line Set"></option><option value="Feed / DW"></option></datalist>
    </div>
</asp:Content>
