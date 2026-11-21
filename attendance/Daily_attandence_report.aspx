<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../SiteMasterAttendance.master" CodeFile="Daily_attandence_report.aspx.vb" Inherits="attendance_Daily_attandence_report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
        <h4>Daily Attandence Report
        </h4>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                        <td>Select Departmant</td>
                        <td>
                            <asp:DropDownList ID="cmbdept" runat="server" /></td>
                        <td>
                            <label for="entrydate">Select Date</label></td>
                        <td>
                            <input type="date" id="entrydate" name="entrydate" runat="server"></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Show Attendance" /></td>
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
