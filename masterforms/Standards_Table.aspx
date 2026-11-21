<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Standards_Table.aspx.vb" Inherits="masterforms_Standards_Table" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Standard Entry</h3>
        <div class="row">
            <div class="col-12" id="prdform">
                <table>
                    <tr><td>
                        <asp:RadioButton ID="rdSummer" runat="server" Text="Summer" AutoPostBack="True" />
                        </td>
                        <td>
                        <asp:RadioButton ID="rdWinter" runat="server" Text="Winter" AutoPostBack="True" />
                        </td>
                    </tr>
                </table>
                <table id="table1" runat="server">
                    <tr id="Tr0" valign="top" runat="server">
                        <td align="center" runat="server">
                            <asp:DataGrid ID="DataGrid1" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                AutoGenerateColumns="False" Height="301px" Width="285px" BorderStyle="Solid" HorizontalAlign="Justify">
                                <AlternatingItemStyle BackColor="#ccccff"></AlternatingItemStyle>
                                <Columns>
                                    <asp:BoundColumn DataField="Age_Weeks" HeaderText="&lt;B&gt;Age&lt;/B&gt;"></asp:BoundColumn>
                                    <asp:TemplateColumn>
                                        <HeaderTemplate>
                                            <b>
                                                <%# Combo3.SelectedValue %>
                                            </b>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="Text1" BorderStyle="None" Text='<%# Container.DataItem("value") %>' runat="server">
                                            </asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid></td>
                        <td runat="server">
                            <table runat="server" border="0">
                                <tr runat="server" id="Tr1">
                                    <td align="center">
                                        &nbsp;</td>
                                </tr>
                                <tr runat="server" id="Tr2">
                                    <td align="center">
                                        <asp:DropDownList ID="Combo2" runat="server" Design_Time_Lock="True" Height="24px" Width="162px" AutoPostBack="True"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr3">
                                    <td align="center">
                                        <asp:DropDownList ID="Combo3" runat="server" Design_Time_Lock="True" Height="24px" Width="162px" AutoPostBack="True"
                                            Style="design_time_lock: True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="Tr6">
                                    <td align="center"></td>
                                </tr>
                                <tr runat="server" id="Tr4">
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" Text="Save" Height="24" Width="50"></asp:Button></td>
                                </tr>
                                <tr runat="server" id="Tr5">
                                    <td align="center">
                                        <asp:Button ID="Button2" runat="server" Text="Close" Height="24" Width="50"></asp:Button></td>
                                </tr>
                                <tr>
                                    <td align="center"> 
                                        <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
</asp:Content>


