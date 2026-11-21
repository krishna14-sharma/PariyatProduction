<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/AreaPage.master" CodeFile="Daily_temperature_report.aspx.vb" Inherits="reports_Daily_temperature_report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Daily Temperature Report</h3>
        <div class="row">
             <div class="col-sm" id="prdform1">
                 Date From
               <input type="date" id="d1" name="entrydate" runat="server" class="form-control">                              

                 </div>
             <div class="col-sm" id="prdform2">
                 To
                 <input type="date" id="d2" name="entrydate" runat="server" class="form-control">                              

                 </div>

             <div class="col-sm" id="prdform3">
            <asp:Button ID="Button1" runat="server" Text="Show" />
                 </div>
            
        </div>
         <div class="row">
              <div class="col-sm" id="prdform3">
            <asp:Button ID="btnMonthly" runat="server" Text="Monthly Temperature Report" />
                 </div>
         </div>

        <div class="row">
            <div class="col-sm" id="prdform">
            </div>
            <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
        </div>
        <div class="row">
            <div class="col-12" id="prdform1">
            </div>
        </div>

    </div>

</asp:Content>
