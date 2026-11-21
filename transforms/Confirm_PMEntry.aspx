<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="false" CodeFile="Confirm_PMEntry.aspx.vb" Inherits="transforms_Confirm_PMEntry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Confirm Daily PM Data</h3>
              <div class="row">
        <asp:Label ID="Label1" runat="server" />
        <hr />
        <div class="col-sm-3">
            <div class="form-group">
                <label for="shedloc">Shed Location</label>              
            </div>
        </div>
        <div class="col-sm-3">
            <div class="form-group">              
                <asp:DropDownList ID="shedloc" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>               
            </div>
         </div>
        <div class="col-sm-3">
            <div class="form-group">
               <asp:Button runat="server" ID="Button3" Text="SHOW" />
            </div>
         </div>
        <div class="col-sm-3">           
        </div>
    </div>
        <div class="row">
            <div class="col-sm-12">
                  <asp:datagrid id="dailyentry_grid" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
							<AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
							<Columns>
                                 <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk1" Text="" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
								<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_loc" HeaderText="<B>Shed Location</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_name" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="line_pen" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="disease_name" HeaderText="<B>Disease Name</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pm_qtyf" HeaderText="<B>Qty (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pm_qtym" HeaderText="<B>Qty (M)</B>"></asp:BoundColumn>
								 <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
							
							</Columns>
						</asp:datagrid>
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