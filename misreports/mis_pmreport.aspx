<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/ReportPage.master" CodeFile="mis_pmreport.aspx.vb" Inherits="misreports_mis_pmreport" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>PM Report
        </h3>
        <div class="row">
            <div class="col-sm">
                    <table>
                    <tr> 
                        <td><asp:DropDownList ID="areamaster" runat="server" AutoPostBack="True" /></td>
                        
                    </tr>
                </table>
                <table>
                    <tr>
                        <td><label for="entrydate">Select Date(From)</label></td>
                        <td> <input type="date" id="entrydate"  runat="server"></td>
                        <td><label for="entrydate">(To)</label></td>
                        <td> <input type="date" id="todate" name="todate" runat="server"></td>
                         <td><label for="entrydate">(Flock No)</label></td>
                         <td> <asp:DropDownList ID="flolckno"  runat="server" /></asp:DropDownList> </td>
                        <td> <asp:TextBox ID="txtFlock"  runat="server" /></asp:TextBox> </td>
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