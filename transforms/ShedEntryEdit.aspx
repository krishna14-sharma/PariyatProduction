<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="ShedEntryEdit.aspx.vb" Inherits="transforms_ShedEntryEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SiteMaster" runat="Server">
    <div class="container">
        <h3>Birds Shed Entry</h3>
        <div class="row">
            <div class="col-12">
                <asp:Label ID="lbl" runat="server">Select Area</asp:Label>
                <asp:DropDownList ID="cmbarea" runat="server"></asp:DropDownList>
                <asp:Button ID="btnshw" Text="Show Area Shed Entry" runat="server"></asp:Button>
            <br />
        </div>
            </div>
    <div class="row">
        <div class="col-12">
            <asp:GridView ID="GridView1" runat="server"
                AutoGenerateColumns="False"
                DataKeyNames="id"
                OnRowEditing="GridView1_RowEditing"
                OnRowCancelingEdit="GridView1_RowCancelingEdit"
                OnRowUpdating="GridView1_RowUpdating" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4">
                <Columns>
                    <asp:CommandField ShowEditButton="True" />

                     <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <%# Eval("id") %>
                        </ItemTemplate>
                     
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Bird Type">
                        <ItemTemplate>
                            <%# Eval("bird_type_code") %>
                        </ItemTemplate>
                                           </asp:TemplateField>


                    <asp:TemplateField HeaderText="Shed Loc">
                        <ItemTemplate><%# Eval("shed_loc") %></ItemTemplate>
                       
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Shed Code">
                        <ItemTemplate><%# Eval("shed_code") %></ItemTemplate>
                        
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Flock No">
                        <ItemTemplate><%# Eval("flock_no") %></ItemTemplate>
                     
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="F Qty">
                        <ItemTemplate><%# Eval("fqty") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtFQty" runat="server" Text='<%# Bind("fqty") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="M Qty">
                        <ItemTemplate><%# Eval("mqty") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtMQty" runat="server" Text='<%# Bind("mqty") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Entry Date">
                        <ItemTemplate><%# Eval("entry_date", "{0:yyyy-MM-dd}") %></ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtEntryDate" runat="server" Text='<%# Bind("entry_date", "{0:yyyy-MM-dd}") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Alive">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkAlive" runat="server" Enabled="false"
                                Checked='<%# Eval("isAlive") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkAliveEdit" runat="server"
                                Checked='<%# Bind("isAlive") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Empty">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmpty" runat="server" Enabled="false"
                                Checked='<%# Eval("isEmpty") %>' />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:CheckBox ID="chkEmptyEdit" runat="server"
                                Checked='<%# Bind("isEmpty") %>' />
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
        </div>
    </div>
    <asp:Label ID="lblMsg" runat="server" ForeColor="Green" />
        </div>
    <style>
        body {
            font-family: 'Segoe UI', Arial, sans-serif;
            margin: 40px;
        }

        .grid {
            width: 100%;
            border-collapse: collapse;
        }

            .grid th, .grid td {
                padding: 6px 8px;
                border: 1px solid #ccc;
            }

            .grid th {
                background: #f2f2f2;
            }
    </style>
</asp:Content>

