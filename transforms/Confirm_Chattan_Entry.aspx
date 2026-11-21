<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../SiteMaster.master" CodeFile="Confirm_Chattan_Entry.aspx.vb" Inherits="transforms_Confirm_Chattan_Entry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>ConfirmChattan Data Shed Wise</h3>
        <div class="col-sm-3">
            <div class="form-group">
                <label for="shedloc">Shed Location</label>              
            </div>
        </div>
        <div class="col-sm-3">
                     
                <asp:DropDownList ID="shedloc" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                <asp:Button ID="btnShoe" Text="Show data" runat ="server" />           
           
         </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:DataGrid ID="chattan_grid" runat="server" PageSize="5"  GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
                    <AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
                    <Columns>
                          <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
                <table>
                    <tr>
                        <td> <asp:Button ID="btconfirm" Text="Confirm" runat ="server" />  </td>
                        <td>  <asp:label ID="lblprint" Text="-" runat ="server" /> </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>