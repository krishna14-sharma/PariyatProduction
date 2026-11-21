<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/SiteMasterHatch.master" CodeFile="Satellite_hatchert_hatachibilty_report.aspx.vb" Inherits="reports_Satellite_hatchert_hatachibilty_report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">
        <div class="container">
            <h3>SATELLITE HATCHERY HATCHABILITY REPORT  </h3>
            <div class="row">
                <div class="col-sm">

                    <table>
                        <tr>
                            <td>
                                <label for="entrydate">Loading Date</label></td>
                            <td>
                                <input type="date" id="entrydate" name="entrydate" runat="server"></td>
                            <td>
                                <asp:CheckBox ID="chkbox" runat="server" Text="B-Grade" />
                            </td>

                            <td>&nbsp;</td>
                        </tr>

                    </table>
                </div>
                <div class="form-group">
                    Hatchery :<asp:DropDownList ID="cmbloc" runat="server">
                    </asp:DropDownList>
                    <asp:Button ID="Button1" runat="server" Text="Show Report" />
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
