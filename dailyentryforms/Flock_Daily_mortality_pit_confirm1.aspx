<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Flock_Daily_mortality_pit_confirm1.aspx.vb" Inherits="dailyentryforms_Daily_mortality_pit_confirm1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <%-- <div class="container">--%>
    <h3>Mortality Pit Sale Entry Confirm</h3>
    <div class="row" id="cont">
        <asp:Label ID="lblString" runat="server" />
        <hr />
        <div class="col" id="prdform">
            <asp:DataGrid ID="dgConfirm" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Medium">
                <AlternatingItemStyle BackColor="#ccccff"></AlternatingItemStyle>
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk1" Text="" runat="server"></asp:CheckBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="mor_fem" HeaderText="<B>Mortality (F)</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="mor_male" HeaderText="<B>Mortality (M)</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="pit_fem" HeaderText="<B>Pit (F)</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="pit_male" HeaderText="<B>Pit (M)</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="sale_fem" HeaderText="<B>Sale (F)</B>"></asp:BoundColumn>
                    <asp:BoundColumn DataField="sale_male" HeaderText="<B>Sale (M)</B>"></asp:BoundColumn>
                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                </Columns>
            </asp:DataGrid>
        </div>
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" />
        <asp:Button runat="server" Text="Close" />
        <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
    </div>
    <%-- </div>--%>
    <style type="text/css">
        #BodyContent, #cont {
            background-color: #ecfacf;
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

        .auto-style1 {
            height: 35px;
            width: 135px;
        }

        .auto-style2 {
            width: 135px;
        }

        .auto-style3 {
            height: 37px;
            width: 135px;
        }
    </style>
</asp:Content>
