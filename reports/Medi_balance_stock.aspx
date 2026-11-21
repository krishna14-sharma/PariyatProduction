<%@ Page Language="VB" MasterPageFile="../AreaPage.master" AutoEventWireup="false" CodeFile="Medi_balance_stock.aspx.vb" Inherits="reports_Medi_balance_stock" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Medicine Balance Stock At Farm</h3>
        <div class="row">
            <div class="col-9">
                  <asp:DataGrid runat="server" ID="bal_stock">
                <AlternatingItemStyle BackColor="#99ccff"/>
            </asp:DataGrid>
                </div>
            </div>
        </div>
    </asp:Content>