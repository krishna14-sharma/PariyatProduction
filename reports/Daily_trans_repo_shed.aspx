<%@ Page Language="VB" MasterPageFile="~/AreaPage.master" AutoEventWireup="true" CodeFile="Daily_trans_repo_shed.aspx.vb" Inherits="reports_Daily_trans_repo_shed" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Daily Production Report Shed Wise<asp:Button ID="Button1" runat="server" Text="Button" />
        </h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                
            </div><div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
        </div>
        <div class="row">
            <div class="col-12" id="prdform1">
            </div>
        </div>

    </div>
  
</asp:Content>
