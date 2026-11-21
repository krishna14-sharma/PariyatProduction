<%@ Page Language="VB" Debug ="True" MasterPageFile="../AreaPage.master" AutoEventWireup="false" CodeFile="DailyIremReceived.aspx.vb" Inherits="dailyentryforms_DailyIremReceived" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>List of Materail Send From Store</h3>
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="entrydate">Entry Date</label>
                    <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control">
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="entrydate">Show Materail List</label>
                    <asp:Button Text="Show" runat="server" class="form-control" ID="btnShow"/>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <asp:DataGrid ID="materialrec_grid" runat="server" PageSize="5"  GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
                    <AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
                </asp:DataGrid>
            </div>
        </div>
         <div class="row">
             <div class="col-sm-3">
                <div class="form-group">
                    <label for="entrydate">Receive Materail </label>
                    <asp:Button Text="Receive" runat="server" class="form-control" id="btnReceve" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
