<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Daily_mortality_pit_confirm1.aspx.vb" Inherits="dailyentryforms_Daily_mortality_pit_confirm1" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
   <%-- <div class="container">--%>
        <h3>Mortality Pit Sale Entry Confirm</h3>
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
                <asp:TextBox ID="cnfdate" runat="server" class="form-control" type="date" />           
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
    </hr>
        <div class="row">
            <asp:Label ID="lblString" runat="server" />
            <hr />
            <div class="col" id="prdform">
                <asp:datagrid id="dgConfirm" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Medium">
							<AlternatingItemStyle BackColor="#ccccff"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:CheckBox ID="chk1" Text="" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
        						<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_loc" HeaderText="<B>Shed Location</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_no" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="line_no" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="mor_fem" HeaderText="<B>Mortality (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="mor_male" HeaderText="<B>Mortality (M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="pit_fem" HeaderText="<B>SexingError (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pit_male" HeaderText="<B>SexingError (M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="sale_fem" HeaderText="<B>Sale (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="sale_male" HeaderText="<B>Sale (M)</B>"></asp:BoundColumn>	
                                <asp:BoundColumn DataField="pit_fem_ext" HeaderText="<B>Pit (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pit_male_ext" HeaderText="<B>Pit (M)</B>"></asp:BoundColumn>									
								<asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete" ></asp:ButtonColumn>
							</Columns>
						</asp:datagrid>
            </div>
            <asp:Button id="btnConfirm" runat="server" Text="Confirm"  />
             <asp:Button runat="server" Text="Close"  />
            <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </div>
   <%-- </div>--%>
</asp:Content>