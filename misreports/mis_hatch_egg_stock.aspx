<%@ Page Title="" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="mis_hatch_egg_stock.aspx.vb" Inherits="misreports_mis_hatch_egg_stock" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ReportMaster" Runat="Server">
    <div class="container">
        <div class="container">
        <h3>HATCHERY EGGS STOCK REPORT  </h3>
        <div class="row">
            <div class="col-sm">
                
                <table>
                    <tr>
                        <td>&nbsp;</td>
                        <td> &nbsp;</td>
                        <td><asp:Button ID="Button1" runat="server" Text="Show Report" /></td>
                    </tr>

                </table>
              </div>
            <div class="form-group">
            </div>
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

