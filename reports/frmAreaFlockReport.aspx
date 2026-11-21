<%@ Page Language="VB" MasterPageFile="../AreaPage.master"  AutoEventWireup="false" CodeFile="frmAreaFlockReport.aspx.vb" Inherits="reports_frmAreaFlockReport" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Flock Report
        </h3>
         <div class="row">
        <div class="col-sm">

            <table>
                <tr>
                    <td>From Date</td>
                     <td> <input type="date" id="entrydate" name="entrydate" runat="server" ></td>
                     <td>To</td>
                     <td><input type="date" id="todate" name="toentrydate" runat="server" ></td>
                     <td> <asp:Button ID="Button1" runat="server" Text="Show Report" />           </td>
                </tr>
            </table>
                          
                        </div>
             <div class="form-group">
               
                                           
                  </div>
               </div>
             </div>
        <div class="row">
            <div class="col-sm" id="prdform">
                <div class="app-report"><cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" /></div>
            </div>
        </div>
        <div class="row">
            <div class="col-12" id="prdform1">
            </div>
        </div>

    </div>
</asp:Content>