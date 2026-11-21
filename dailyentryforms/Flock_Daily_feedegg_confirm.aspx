<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Flock_Daily_feedegg_confirm.aspx.vb" Inherits="dailyentryforms_Daily_feedegg_confirm" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <%-- <div class="container">--%>
        <h3>Feed/Egg Entry Confirm</h3>
        <div class="row"  id ="cont">
           <asp:Label ID="lblString" runat="server" />
            <hr/>
             <div class="col" id="prdform">
                <asp:datagrid id="dgConfirm" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
							<AlternatingItemStyle BackColor="#ccccff"></AlternatingItemStyle>
							<Columns>
								<asp:TemplateColumn>
									<ItemTemplate>
										<asp:CheckBox ID="chk1" Text="" Runat="server"></asp:CheckBox>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="feed_qty_female" HeaderText="<B>Feed (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_type_female" HeaderText="<B>Feed Type (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_qty_male" HeaderText="<B>Feed (M)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_type_male" HeaderText="<B>Feed Type (M)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="hatch_eggs" HeaderText="<B>HE</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="comm_eggs" HeaderText="<B>Comm</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="crack_eggs" HeaderText="<B>Crack</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="leakage_eggs" HeaderText="<B>Leak</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_female" HeaderText="<B>Body Wt(F)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_male" HeaderText="<B>Body Wt(M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="egg_wt" HeaderText="<B>Egg Wt</B>"></asp:BoundColumn>
								<asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete" ></asp:ButtonColumn>
							</Columns>
						</asp:datagrid>
            </div>
            <asp:Button id="btnConfirm" runat="server" Text="Confirm" />
             <asp:Button runat="server" Text="Close"  />
            <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
        </div>
   <%-- </div>--%>
    <style type="text/css">
          #BodyContent,#cont{
             background-color:#ecfacf;
         }
        #prdform {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

         #divshed_grid {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

        #SiteMaster_chikc_purchase_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }

        
    </style>
</asp:Content>
