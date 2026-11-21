<%@ Page Language="VB" MasterPageFile="~/SiteMasterPit.master" AutoEventWireup="false" CodeFile="pit_bird_sale.aspx.vb" Inherits="pit_pit_bird_sale" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <table class="table" border="1">
            <tr>
                <td colspan="5">
                    <h2>Bird Sale/Mortality/Distribute From Pit</h2>
                </td>
            </tr>
            <tr>
                <td>Entry Date
                </td>
                <td>
                    <asp:TextBox ID="entrydate" TextMode="Date" runat="server" />
                </td>
                <td></td>
            </tr>

            <tr>
                <td>DM Number
                </td>
                <td>
                    <asp:TextBox ID="txtdmno" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Party Name
                </td>
                <td>
                    <asp:TextBox ID="txtpartyname" runat="server" />
                </td>
                <td></td>
            </tr>
            <tr>
                <td>Bird Type
                </td>
                <td>
                    <asp:DropDownList ID="cmbftype" runat="server"></asp:DropDownList>
                </td>
                <td>
                    <asp:Button runat="server" ID="Button3" Text="Show" />
                </td>
            </tr>
            <tr>
                <td>Bird Stock 
                </td>
                <td>Sale/Rate/Weight
                </td>
                <td>Distribute
                </td>
                <td>Mortality
                </td>
            </tr>

            <tr>
                <td>Female<asp:TextBox runat="server" ID="commeggs" ReadOnly="True" Font-Bold="True" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="commsale" />
                    <asp:TextBox runat="server" ID="rate_comm" />
                     <asp:TextBox runat="server" ID="wtf" />

                </td>
                <td>
                    <asp:TextBox runat="server" ID="commdis" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="commdestroy" />
                </td>
            </tr>
            <tr>
                <td>Male<asp:TextBox runat="server" ID="crackeggs" ReadOnly="True" Font-Bold="True" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="cracksale" />
                    <asp:TextBox runat="server" ID="rate_crack" />
                       <asp:TextBox runat="server" ID="wtm" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="crackdis" />
                </td>
                <td>
                    <asp:TextBox runat="server" ID="crackdestroy" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button runat="server" ID="btnsave" Text="Save" />
                </td>
                <td>
                    <asp:Button runat="server" ID="Button1" Text="Cancel" />
                </td>
                <td>
                    <asp:Button runat="server" ID="Button2" Text="Showdata" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:DataGrid ID="commeggdata" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                        <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
