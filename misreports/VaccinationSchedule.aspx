<%@ Page Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="VaccinationSchedule.aspx.vb" Inherits="transforms_VaccinationSchedule" Title="Vaccination Schedule" %>

<asp:Content ID="VaccinationHead" ContentPlaceHolderID="head" runat="server">
    <link href="<%= ResolveUrl("~/Content/vaccination.css") %>" rel="stylesheet" />
</asp:Content>

<asp:Content ID="VaccinationContent" ContentPlaceHolderID="ReportMaster" runat="server">

<div class="vacc">
    <h3>Vaccination &amp; Medication Schedule</h3>
    <asp:Literal ID="litMessage" runat="server" EnableViewState="false" />

    <%-- ===================== Due & overdue across flocks ===================== --%>
    <asp:Panel ID="pnlDue" runat="server" CssClass="vacc-due">
        <div class="vacc-due-head">
            <strong>Due &amp; overdue</strong>
            <span class="vacc-muted"><asp:Literal ID="litDueHint" runat="server" /></span>
        </div>
        <div class="vacc-table-wrap">
        <asp:GridView ID="gvDue" runat="server" AutoGenerateColumns="false" GridLines="None" CssClass="table table-sm vacc-due-table"
            DataKeyNames="flock_no,sex" ShowHeader="true">
            <Columns>
                <asp:TemplateField HeaderText="Flock">
                    <ItemTemplate><strong><%#: Eval("flock_no") %></strong> <span class="vacc-sex vacc-sex-<%#: Eval("sex") %>"><%#: VaccinationData.SexName(Convert.ToString(Eval("sex"))) %></span></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Overdue">
                    <ItemTemplate><%# CountBadge(CInt(Eval("overdue")), "overdue") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Due now / soon">
                    <ItemTemplate><%# CountBadge(CInt(Eval("soon")), "soon") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Earliest pending">
                    <ItemTemplate>
                        <div class="vacc-name"><%#: Eval("vaccine") %></div>
                        <div class="vacc-muted"><%#: Eval("age_label") %> · due <%# CDate(Eval("due_date")).ToString("dd-MM-yyyy") %></div>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><%# DueBadge(CInt(Eval("days_overdue"))) %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkOpen" runat="server" CommandName="OpenFlock" CommandArgument='<%# Container.DataItemIndex %>' Text="Open" CssClass="vacc-link" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        </div>
        <asp:Literal ID="litDueEmpty" runat="server" />
    </asp:Panel>

    <%-- ===================== Flock selection ===================== --%>
    <div class="vacc-filters">
        <div class="vacc-field vacc-field-wide">
            <label for="<%= ddlFlock.ClientID %>">Flock</label>
            <asp:DropDownList ID="ddlFlock" runat="server" CssClass="form-select" AutoPostBack="true" />
        </div>
        <div class="vacc-field">
            <label>Birds</label>
            <asp:RadioButtonList ID="rblSex" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="vacc-toggle" AutoPostBack="true">
                <asp:ListItem Value="F" Selected="True">Female</asp:ListItem>
                <asp:ListItem Value="M">Male</asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div class="vacc-field">
            <label for="<%= txtHatch.ClientID %>">Hatch date</label>
            <asp:TextBox ID="txtHatch" runat="server" TextMode="Date" CssClass="form-control" />
        </div>
        <div class="vacc-field vacc-field-btn">
            <asp:Button ID="btnShow" runat="server" Text="Show schedule" CssClass="btn btn-primary" />
        </div>
    </div>
    <asp:Literal ID="litHatchNote" runat="server" />

    <%-- ===================== Schedule ===================== --%>
    <asp:Panel ID="pnlSchedule" runat="server" Visible="false">
        <div class="vacc-flockinfo"><asp:Literal ID="litFlockInfo" runat="server" /></div>

        <div class="vacc-stats">
            <div class="vacc-stat"><span>Total</span><strong><asp:Literal ID="litTotal" runat="server" /></strong></div>
            <div class="vacc-stat is-ontime"><span>Given on time</span><strong><asp:Literal ID="litOnTime" runat="server" /></strong></div>
            <div class="vacc-stat is-late"><span>Given late</span><strong><asp:Literal ID="litLate" runat="server" /></strong></div>
            <div class="vacc-stat is-overdue"><span>Overdue</span><strong><asp:Literal ID="litOverdue" runat="server" /></strong></div>
            <div class="vacc-stat is-soon"><span>Due now / next <asp:Literal ID="litSoonDays" runat="server" /> days</span><strong><asp:Literal ID="litSoon" runat="server" /></strong></div>
            <div class="vacc-stat"><span>Upcoming</span><strong><asp:Literal ID="litUpcoming" runat="server" /></strong></div>
        </div>

        <div class="vacc-toolbar">
            <div class="vacc-filter-chips" role="group" aria-label="Show">
                <button type="button" class="vacc-chip is-active" data-filter="all">All</button>
                <button type="button" class="vacc-chip" data-filter="pending">Not given</button>
                <button type="button" class="vacc-chip" data-filter="attention">Overdue &amp; due</button>
                <button type="button" class="vacc-chip" data-filter="late">Given late</button>
            </div>
            <div class="vacc-actions">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" />
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" CssClass="btn btn-outline-secondary" />
                <button type="button" class="btn btn-outline-secondary" onclick="window.print()">Print</button>
            </div>
        </div>

        <div class="vacc-table-wrap">
            <asp:GridView ID="gvSchedule" runat="server" AutoGenerateColumns="false" GridLines="None" DataKeyNames="ScheduleId"
                CssClass="table vacc-table">
                <Columns>
                    <asp:TemplateField HeaderText="#">
                        <ItemTemplate><%# Eval("SeqNo") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Age">
                        <ItemTemplate>
                            <div class="vacc-age"><%#: Eval("AgeLabel") %></div>
                            <div class="vacc-muted">Day <%# Eval("DayNo") %></div>
                            <%# PhaseBadge(Convert.ToString(Eval("Phase"))) %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Vaccine / medicine">
                        <ItemTemplate>
                            <div class="vacc-name"><%#: Eval("Vaccine") %></div>
                            <div class="vacc-muted"><%#: Eval("DetailLine") %></div>
                            <div class="vacc-muted"><%#: Eval("Remark") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Due date">
                        <ItemTemplate>
                            <div class="vacc-date"><%# CDate(Eval("DueDate")).ToString("dd-MM-yyyy") %></div>
                            <div class="vacc-muted"><%# CDate(Eval("DueDate")).ToString("dddd") %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Given on">
                        <ItemTemplate>
                            <div class="vacc-given">
                                <asp:TextBox ID="txtGiven" runat="server" TextMode="Date" CssClass="form-control form-control-sm vacc-given-input" />
                                <button type="button" class="vacc-today" title="Given today">Today</button>
                            </div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <span class="vacc-badge is-<%# Eval("Status") %>"><%#: Eval("StatusText") %></span>
                            <div class="vacc-muted vacc-by"><%#: EnteredText(Container.DataItem) %></div>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason if late">
                        <ItemTemplate>
                            <asp:TextBox ID="txtReason" runat="server" CssClass="form-control form-control-sm vacc-reason" MaxLength="300" list="vaccLateReasons" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" MaxLength="300" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EmptyDataTemplate>
                    <p class="vacc-muted">No schedule items. Load the standard schedule with Database/Vaccination_Schedule.sql or add items in Vaccine Schedule Master.</p>
                </EmptyDataTemplate>
            </asp:GridView>
        </div>

        <div class="vacc-footer">
            <asp:Button ID="btnSaveBottom" runat="server" Text="Save" CssClass="btn btn-primary" />
            <asp:LinkButton ID="lnkClose" runat="server" CssClass="vacc-link vacc-close"
                OnClientClick="return confirm('Stop showing this flock in the due list? (Saving the schedule again turns it back on.)');">Stop tracking this flock</asp:LinkButton>
        </div>

        <asp:Panel ID="pnlNotes" runat="server" CssClass="vacc-notes">
            <strong>Instructions</strong>
            <asp:Literal ID="litNotes" runat="server" />
        </asp:Panel>
    </asp:Panel>

    <datalist id="vaccLateReasons">
        <option value="Vaccine not available"></option>
        <option value="Vaccine received late"></option>
        <option value="Birds sick / under treatment"></option>
        <option value="Staff / vaccinator not available"></option>
        <option value="Weather (heat / rain)"></option>
        <option value="Shifting or grading on the due day"></option>
        <option value="As advised by the veterinarian"></option>
    </datalist>
</div>

<script type="text/javascript">
    (function () {
        // Bound once on document, so it keeps working after AJAX (UpdatePanel) refreshes.
        if (window.__vaccBound) { return; }
        window.__vaccBound = true;
        var grace = <%= VaccinationData.GraceDays %>;

        function parseDate(v) { var p = (v || '').split('-'); return p.length === 3 ? new Date(+p[0], p[1] - 1, +p[2]) : null; }
        function pad(n) { return (n < 10 ? '0' : '') + n; }
        function todayIso() { var d = new Date(); return d.getFullYear() + '-' + pad(d.getMonth() + 1) + '-' + pad(d.getDate()); }

        function refreshRow(row) {
            var input = row.querySelector('.vacc-given-input');
            var reason = row.querySelector('.vacc-reason');
            if (!input || !reason) { return; }
            var due = parseDate(row.getAttribute('data-due'));
            var given = parseDate(input.value);
            var late = due && given ? Math.round((given - due) / 86400000) : null;
            var needs = late !== null && late > grace;
            row.classList.toggle('needs-reason', needs);
            reason.placeholder = needs ? 'Reason required (' + late + (late === 1 ? ' day' : ' days') + ' late)' : '';
        }

        document.addEventListener('change', function (e) {
            if (e.target.classList && e.target.classList.contains('vacc-given-input')) {
                var row = e.target.closest('tr');
                if (row) { refreshRow(row); }
            }
        });
        document.addEventListener('click', function (e) {
            var t = e.target;
            if (t.classList && t.classList.contains('vacc-today')) {
                var row = t.closest('tr'); var input = row && row.querySelector('.vacc-given-input');
                if (input) { input.value = todayIso(); refreshRow(row); input.focus(); }
            }
            if (t.classList && t.classList.contains('vacc-chip')) {
                var f = t.getAttribute('data-filter');
                var chips = document.querySelectorAll('.vacc-chip');
                for (var i = 0; i < chips.length; i++) { chips[i].classList.toggle('is-active', chips[i] === t); }
                var rows = document.querySelectorAll('.vacc-table tr[data-status]');
                for (var j = 0; j < rows.length; j++) {
                    var s = rows[j].getAttribute('data-status'), show = true;
                    if (f === 'pending') { show = s === 'overdue' || s === 'today' || s === 'soon' || s === 'upcoming'; }
                    else if (f === 'attention') { show = s === 'overdue' || s === 'today' || s === 'soon'; }
                    else if (f === 'late') { show = s === 'late'; }
                    rows[j].style.display = show ? '' : 'none';
                }
            }
        });
    })();
</script>
</asp:Content>
