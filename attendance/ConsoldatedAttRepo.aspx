<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../SiteMasterAttendance.master" CodeFile="ConsoldatedAttRepo.aspx.vb" Inherits="attendance_ConsoldatedAttRepo" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
        <h4>Consolidate Attandence Report
        </h4>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                       <td><label for="entrydate">Select Date</label></td>
                        <td>
                            <input type="date" id="entrydate" name="entrydate" runat="server"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Attendance" /></td>
                        <td>
                            <asp:Button ID="Button2" runat="server" Text="Pending Attendance" /></td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
            </div>
        </div>
        <div class="row">
            <div class="app-report"><CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>
        </div>
         <div class="row">
             <asp:GridView ID="GridView1" runat="server"></asp:GridView>
        </div>
    </div>
</asp:Content>