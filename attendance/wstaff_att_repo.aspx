<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="../SiteMasterAttendance.master" CodeFile="wstaff_att_repo.aspx.vb" Inherits="attendance_wstaff_att_repo" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
         <h4>Weekly Staff Attandence Report
        </h4>
           <div class="row">
            <div class="col-sm">
                Select Area: <asp:DropDownList ID ="warealist" runat="server" />
            </div>
        </div>
         <div class="row">
            <div class="col-sm">
               <table>
                    <tr>
                        <td><label for="entrydate">Select Date</label></td>
                        <td> <input type="date" id="entrydate" name="entrydate" runat="server"></td>
                        <td><asp:Button ID="Button1" runat="server" Text="Show Attendance" /></td>
                    </tr>
                </table>
              </div>
            <div class="form-group">
            </div>
        </div>
        <div class="row">
               <div class="app-report"><cr:crystalreportviewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" /></div>           
            </div>
        </div>
    </asp:Content>