<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/SiteMasterPit.master" CodeFile="pit_egg_coll_report.aspx.vb" Inherits="pit_pit_egg_coll_report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <h3>Pit Egg Collection Report </h3>
        <div class="row">
            <div class="col-sm" id="prdform">
               From Date
                <input type="date" id="entrydatefrom" name="entrydate" runat="server">TO 
                <input type="date" id="entrydateto" name="entrydate" runat="server">
                <asp:Button ID="showRepo" Text="Show" runat="server" />
        </div>
        <div class="row">
            <div class="col-sm" id="prdform">
                <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
        </div>
        <div class="row">
            <div class="col-12" id="prdform1">
            </div>
        </div>

    </div>
     </div>
  
    </asp:Content>