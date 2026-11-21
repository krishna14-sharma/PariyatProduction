<%@ Page Title="" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="mis_floc_prod_consoli.aspx.vb" Inherits="misreports_mis_floc_prod_consoli" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>Flock Production Consolidate Report
        </h3>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr> 
                        <td><asp:DropDownList ID="areamaster" runat="server" /></td>
                         <td><label for="entrydate">Select Date</label></td>
                        <td><input type="date" id="entrydate" name="entrydate" runat="server"/></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Show Report" /></td>
                    </tr>
                    <tr>
                        <td><asp:CheckBox ID="chkbroiler" runat="server" Text="Broiler Laying Flocks"/> </td>
                         <td><asp:CheckBox ID="chkgrower" runat="server" Text="Chicks And Grower"/> </td>
                         <td><asp:CheckBox ID="chklayer" runat="server" Text="Layer Laying Flocks"/> </td>
                    </tr>

                </table>
         
              </div>
            <div class="form-group">
                <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm" id="prdform">
            <%--<div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>--%>
        </div>
    </div>
    <div class="row">
        <div class="col-12" id="prdform1">
        </div>
    </div>
     
</asp:Content>