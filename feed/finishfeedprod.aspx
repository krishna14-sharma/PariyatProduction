<%@ Page Language="VB"  MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="finishfeedprod.aspx.vb" Inherits="feed_finishfeedprod" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>Finish Feed Production Report
        </h3>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                        <td>
                            <label for="entrydate">Select Date</label></td>
                        <td>
                            <input type="date" id="entrydate" name="entrydate" runat="server" /></td>
                        <td>
                            <input type="date" id="entrydateto" name="entrydateto" runat="server" /></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Report" /></td>
                    </tr>

                </table>

            </div>
            <div class="form-group">
                <div class="app-report"><cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" /></div>
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

    </div>
</asp:Content>