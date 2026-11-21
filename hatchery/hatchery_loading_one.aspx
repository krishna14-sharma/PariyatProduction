<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/SiteMasterHatch.master" CodeFile="hatchery_loading_one.aspx.vb" Inherits="hatchery_hatchery_loading_one" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">

        <table border="1" class="table">
            <tr >
                <td>
                    <table border="1">
                        <tbody>
                            <tr>
                                <td colspan="5" valign="middle" align="center" rowspan="1">
                                    <h3>&nbsp;Hatching Candling I Entry</h3>
                                </td>
                            </tr>
                            <tr>
                                <td>Entry Date</td>
                                <td align="left">
                                    <asp:TextBox ID="entrydate" TextMode="Date" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Egg Collection Date</td>
                                <td>
                                    <asp:TextBox ID="eggcolldate" TextMode="Date" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Flock Type</td>
                                <td>
                                    <asp:DropDownList ID="ftype" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Flock No</td>
                                <td>
                                    <asp:DropDownList ID="flockno" runat="server" />
                                    <asp:Button ID="Button1" runat="server" Text="Show" />
                                </td>
                            </tr>
                            <tr>
                                <td>Age (Desirable)</td>
                                <td>
                                    <asp:Label ID="LblAge" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Total HE</td>
                                <td>
                                    <asp:TextBox ID="the" runat="server" ReadOnly="True" /></td>
                            </tr>

                            <tr>
                                <td>Fresh</td>
                                <td>
                                    <asp:TextBox ID="fresh" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="freshset" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="freshcrack" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Big Size</td>
                                <td>
                                    <asp:TextBox ID="bigsize" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>MD</td>
                                <td>
                                    <asp:TextBox ID="md" runat="server" /></td>
                            </tr>

                            <tr>
                                <td>M+</td>
                                <td>
                                    <asp:TextBox ID="mplus" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>M-</td>
                                <td>
                                    <asp:TextBox ID="mminus" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Hathery Comm</td>
                                <td>
                                    <asp:TextBox ID="hcomm" runat="server" /></td>
                            </tr>

                            <tr>
                                <td>Hathery Crack</td>
                                <td>
                                    <asp:TextBox ID="hatchery_crack" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Hathery Leaker</td>
                                <td>
                                    <asp:TextBox ID="hatchery_leaker" runat="server" /></td>
                            </tr>


                            <tr>

                                <td>
                                    <asp:DropDownList ID="cmbloc" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="coldroomname" runat="server"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="3">&nbsp;
                        <asp:Label ID="lblString" runat="server" Font-Bold="True" ForeColor="#FF3300" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="centre">
                                    <asp:Button runat="server" ID="btnsave" Text="OK" /></td>
                                <td>
                                    <asp:Button runat="server" ID="cancel" Text="Show Data" />
                                    <asp:Button runat="server" ID="cancel0" Text="Cancel" /></td>
                </td>
            </tr>
            <tr>
                <td colspan="2"></td>
            </tr>

            </tbody>
        </table>
        </td>
                <td>
                    <asp:DataGrid ID="egggrading" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                        <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                    </asp:DataGrid>

                </td>
        </tr>
        </table>



        
    </div>
</asp:Content>
