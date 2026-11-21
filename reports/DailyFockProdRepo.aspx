<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="DailyFockProdRepo.aspx.vb" Inherits="reports_DailyFockProdRepo" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Flock Production Report <asp:Button ID="Button1" runat="server" Text="Button" />
        </h3>
        <div class="row">
            <div class="sm-3">

            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform">            
                <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>              
            </div>
        </div>
        <div class="row">
            <div class="col-12" id="prdform1">
            </div>
        </div>

    </div>
  
</asp:Content>