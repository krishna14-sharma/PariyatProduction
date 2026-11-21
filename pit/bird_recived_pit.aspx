<%@ Page Language="VB" MasterPageFile="~/SiteMasterPit.master" AutoEventWireup="false" CodeFile="bird_recived_pit.aspx.vb" Inherits="pit_bird_recived_pit" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <table class="table" border="1">
            <tr>
                <td colspan="3">
                    <h2>&nbsp;Birds Received To Pit</h2>
                </td>
            </tr>
            <tr>
                <td>Entry Date</td>
                <td>
                    <asp:TextBox ID="entrydate" TextMode="Date" runat="server" /></td>
            </tr>
            <tr>
                <td>Transfer Date</td>
                <td>
                    <asp:TextBox ID="eggcolldate" TextMode="Date" runat="server" /></td>
            </tr>
            <tr>
                <td>Flock Type</td>
                <td> <asp:DropDownList ID="cmbftype" runat="server" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Flock No</td>
                <td>
                    <asp:DropDownList ID="flockno" runat="server" AutoPostBack="True"></asp:DropDownList>
                    <asp:Button ID="showdata" Text="Show" runat="server" Font-Bold="True" /> 
                </td>
            </tr>
            <tr>
                <td>Female Bird</td>
                <td><asp:TextBox ID="fbirds" runat="server" ReadOnly="True" />  </td>
            </tr>
            <tr>
                <td>Male Bird</td>
                <td><asp:TextBox ID="mbirds" runat="server" ReadOnly="True" /></td>
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
                <td colspan="5"> <asp:Label ID="lblString" runat="server"></asp:Label>  </td>
            </tr>
             <tr>
                <td><asp:Button ID="save" Text="Save" runat="server" />  </td>
                <td><asp:Button ID="Button1" Text="Show Data" runat="server" /></td>
            </tr>

            <tr>
                <td colspan="2">
                    
                 <asp:DataGrid ID="pitbirds" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                     <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                 </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
