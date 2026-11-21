<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../AreaPage.master" CodeFile="balance_stock.aspx.vb" Inherits="reports_balance_stock" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Balance Stock At Farm</h3>
        <div class="row">
            <div class="col-9">
                  <asp:DataGrid runat="server" ID="bal_stock">
                <AlternatingItemStyle BackColor="YellowGreen" />
            </asp:DataGrid>
                </div>
            </div>
        </div>
    </asp:Content>
 