<%@ Page Language="VB" MasterPageFile="~/SiteMasterPit.master" AutoEventWireup="false" CodeFile="egg_recived.aspx.vb" Inherits="pit_egg_recived" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <table class="table" border="1">
            <tr>
                <td class="auto-style2">
                    <!------------ entry table-->
                    <table border="1">
                        <tr>
                            <td colspan="3">
                                <h2>&nbsp;Egg Received To Pit</h2>
                            </td>
                        </tr>
                        <tr>
                            <td>Entry Date
                            </td>
                            <td class="auto-style1">
                                <asp:TextBox ID="entrydate" TextMode="Date" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>Egg Coll Date</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="eggcolldate" TextMode="Date" runat="server" /></td>
                        </tr>
                        <tr>
                            <td>Flock Type</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="cmbftype" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td>Flock No</td>
                            <td class="auto-style1">
                                <asp:DropDownList ID="flockno" runat="server"></asp:DropDownList></td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Text="Show Eggs" Font-Bold="True" />
                            </td>
                        </tr>
                        <tr>
                            <td>Age (Desirable)</td>
                            <td class="auto-style1">
                                <asp:Label ID="LblAge" runat="server" Width="84px" Font-Bold="True"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="txteggs" runat="server" ReadOnly="True" Font-Bold="True" Visible="False"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>Commercial Eggs</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TxtCommEggs" runat="server" ReadOnly="True"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td>Crack Eggs</td>
                            <td class="auto-style1">
                                <asp:TextBox ID="TxtCrackEgg" runat="server" ReadOnly="True"></asp:TextBox></td>
                </td>
            </tr>
            <tr>
                <td>Leaker Eggs</td>
                <td class="auto-style1">
                    <asp:TextBox ID="TxtLeakEggs" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>
             <tr>
                <td>Rejected Hatchery Comm Eggs</td>
                <td class="auto-style1">
                    <asp:TextBox ID="hcomm" runat="server" ReadOnly="True"></asp:TextBox></td>
                </td>
            </tr>

            <tr>
                <td>Rejected Hatchery Crack Eggs</td>
                <td class="auto-style1">
                    <asp:TextBox ID="hcrack" runat="server" ReadOnly="True"></asp:TextBox></td>
                </td>
            </tr>
            <tr>
                <td>Rejected Hatchery Leaker Eggs</td>
                <td class="auto-style1">
                    <asp:TextBox ID="hleaker" runat="server" ReadOnly="True"></asp:TextBox></td>
            </tr>

            <tr>
                <td>Location</td>
                <td class="auto-style1">
                    <asp:DropDownList ID="cmbloc" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                <td>Cold Room</td>
                <td>
                    <asp:DropDownList ID="coldroomname" runat="server"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Receive" Width="64px"></asp:Button></td>
                <td class="auto-style1">
                    <asp:Button ID="Button4" runat="server" Text="Show Data" Width="77px"></asp:Button></td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Close" Width="77px"></asp:Button></td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:Label ID="lblString" runat="server" Font-Bold="True" ForeColor="#FF3300" />
                </td>
            </tr>
        </table>
        <!------------ end entry table-->
        </td>
             <td>

                 <asp:DataGrid ID="eggp" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                     <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                 </asp:DataGrid>
             </td>
        </tr>
    </table>   
      
    </div>
</asp:Content>
