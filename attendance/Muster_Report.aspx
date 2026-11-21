<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMasterAttendance.master" AutoEventWireup="false" CodeFile="Muster_Report.aspx.vb" Inherits="attendance_Muster_Report" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
       <h3> Muster Roll Report:&nbsp;</h3>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                        <td>Select Departmant</td>
                        <td>
                            <asp:DropDownList ID="cmbdept" runat="server" /></td>
                        <td>
                            <label for="entrydate">Select Date From-TO</label></td>
                        <td>
                            <input type="date" id="entrydate" name="entrydate" runat="server"></td>
                         <td>
                            <input type="date" id="entrydate1" name="entrydate1" runat="server"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Attendance" /></td>
                         <td>
                            <asp:Button ID="Button2" runat="server" Text="Export To Excel" /></td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
            </div>
        </div>
        <div class="row">
            <asp:GridView ID="GridView1" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
