<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMasterAttendance.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="attendance_Default" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
        <h4>Consolidate Leave Report
        </h4>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                       <td>Select Year</td>
                        <td>
                            <asp:DropDownList id="year" runat="server" >
                                <asp:ListItem>2025</asp:ListItem>
                                <asp:ListItem>2026</asp:ListItem>
                                <asp:ListItem>2027</asp:ListItem>
                                <asp:ListItem>2028</asp:ListItem>
                                <asp:ListItem>2029</asp:ListItem>
                                <asp:ListItem>2029</asp:ListItem>
                            </asp:DropDownList></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Leave data" /></td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
            </div>
        </div>
        <div class="row">
            <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
        </div>
    </div>
</asp:Content>