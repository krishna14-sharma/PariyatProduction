<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Confrim_BirdTransfer.aspx.vb" Inherits="transforms_Confrim_BirdTransfer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Confirm Bird Transfer Entry Shed Wise</h3>
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
                <asp:DataGrid ID="transfer_grid" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
                    <AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk1" Text="" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="from_shed_loc" HeaderText="<B>From Shed </B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="from_shed_no" HeaderText="<B>From Shed No.</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="from_line_no" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="to_shed_loc" HeaderText="<B>To Location</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="to_shed_no" HeaderText="<B>To Shed</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="to_line_no" HeaderText="<B>To Line/Pen</B>"></asp:BoundColumn>
                         <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="fqty" HeaderText="<B>F Qty</B>"></asp:BoundColumn>
                        <asp:BoundColumn DataField="mqty" HeaderText="<B>M Qty</B>"></asp:BoundColumn>
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

