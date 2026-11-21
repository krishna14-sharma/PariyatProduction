<%@ Page Title="" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="mis_consolidatePMreport.aspx.vb" Inherits="misreports_mis_consolidatePMreport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>Consolidate PM Report
        </h3>
        <div class="row">
            <div class="col-12">
                <table>
                    <tr>
                        <td>
                            <label for="entrydate">Select Date(From)</label></td>
                        <td>
                            <input type="date" id="entrydate" runat="server"></td>
                        <td>
                            <label for="entrydate">(To)</label></td>
                        <td>
                            <input type="date" id="todate" name="todate" runat="server"></td>
                         <td>
                            <asp:DropDownList  id="ddlMF" name="todate" runat="server">
                                <asp:ListItem>F</asp:ListItem>
                                <asp:ListItem>M</asp:ListItem>
                             </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Report" /></td>

                         <td>
                            <asp:Button ID="Button2" runat="server" Text="Export Report" /></td>
                    </tr>

                </table>
            </div>
            <div class="form-group">
                <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                    <AlternatingRowStyle BackColor="#F7F7F7" />
                    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                    <SortedAscendingCellStyle BackColor="#F4F4FD" />
                    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                    <SortedDescendingCellStyle BackColor="#D8D8F0" />
                    <SortedDescendingHeaderStyle BackColor="#3E3277" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-12" id="prdform">
        </div>
    </div>
    <div class="row">
        <div class="col-12" id="prdform1">
        </div>
    </div>

</asp:Content>
