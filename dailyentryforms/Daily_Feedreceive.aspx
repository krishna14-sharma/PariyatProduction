<%@ Page Language="VB" MasterPageFile="../AreaPage.master" AutoEventWireup="false" CodeFile="Daily_Feedreceive.aspx.vb" Inherits="dailyentryforms_Daily_Feedreceive" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="cr" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Feed Received Information</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                    <div class="col-sm-3">
                            <div class="form-group">
                                <label for="entrydate">SelectDate</label>
                                <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control">                              
                            <asp:Button ID="btnshow" runat="server" Text="Show" />
                <div class="app-report"><cr:crystalreportviewer id="CrystalReportViewer1" runat="server" autodatabind="true" /></div>
                            </div>
                        
                    </div>
            </div>
            </div>
        </div>
    </asp:Content>