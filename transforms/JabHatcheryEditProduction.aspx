<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="JabHatcheryEditProduction.aspx.vb" Inherits="transforms_JabHatcheryEditProduction" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3><strong>Jabalpur Hatchery Production Data</strong></h3>
        <div class="row">
            <div class="col-sm-4">
                Select Loading Date:
                <input type="date" id="lpoadinfdate" runat="server" />
            </div>
            <div class="col-sm-4">
                Select Flock Number:
                <input type="text" id="flock_no" runat="server" />
            </div>
            <div class="col-sm-4">
                <asp:Button runat="server" ID="btnSubmit" Text="Show Production Data" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
    DataKeyNames="id"
    OnRowEditing="GridView1_RowEditing"
    OnRowCancelingEdit="GridView1_RowCancelingEdit"
    OnRowUpdating="GridView1_RowUpdating"
    OnRowDeleting="GridView1_RowDeleting" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">

<Columns>


<asp:TemplateField HeaderText="ID">
    <ItemTemplate><%# Eval("id") %></ItemTemplate>
    <EditItemTemplate>
        <asp:Label ID="lblID" runat="server" Text='<%# Eval("id") %>' />
    </EditItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="Date">
    <ItemTemplate><%# Eval("loading_date","{0:yyyy-MM-dd}") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="loading_date" runat="server" Text='<%# Bind("loading_date","{0:yyyy-MM-dd}") %>' /></EditItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="FType">
    <ItemTemplate><%# Eval("ftype") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="ftype" runat="server" Text='<%# Bind("ftype") %>' /></EditItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Flock">
    <ItemTemplate><%# Eval("flock_no") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="flock_no" runat="server" Text='<%# Bind("flock_no") %>' /></EditItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Age">
    <ItemTemplate><%# Eval("age") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="age" runat="server" Text='<%# Bind("age") %>' /></EditItemTemplate>
</asp:TemplateField>


<asp:TemplateField HeaderText="fresh_fertile_eggs"><ItemTemplate><%# Eval("fresh_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_fertile_eggs" runat="server" Text='<%# Bind("fresh_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_infertile_eggs"><ItemTemplate><%# Eval("fresh_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_infertile_eggs" runat="server" Text='<%# Bind("fresh_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_chichs"><ItemTemplate><%# Eval("fresh_chichs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_chichs" runat="server" Text='<%# Bind("fresh_chichs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_hatcherloss"><ItemTemplate><%# Eval("fresh_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_hatcherloss" runat="server" Text='<%# Bind("fresh_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_supply"><ItemTemplate><%# Eval("fresh_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_supply" runat="server" Text='<%# Bind("fresh_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_culls"><ItemTemplate><%# Eval("fresh_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_culls" runat="server" Text='<%# Bind("fresh_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="fresh_per"><ItemTemplate><%# Eval("fresh_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="fresh_per" runat="server" Text='<%# Bind("fresh_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="bg_fertile_eggs"><ItemTemplate><%# Eval("bg_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_fertile_eggs" runat="server" Text='<%# Bind("bg_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_infertile_eggs"><ItemTemplate><%# Eval("bg_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_infertile_eggs" runat="server" Text='<%# Bind("bg_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_chicks"><ItemTemplate><%# Eval("bg_chicks") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_chicks" runat="server" Text='<%# Bind("bg_chicks") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_hatcherloss"><ItemTemplate><%# Eval("bg_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_hatcherloss" runat="server" Text='<%# Bind("bg_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_supply"><ItemTemplate><%# Eval("bg_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_supply" runat="server" Text='<%# Bind("bg_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_culls"><ItemTemplate><%# Eval("bg_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_culls" runat="server" Text='<%# Bind("bg_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="bg_per"><ItemTemplate><%# Eval("bg_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="bg_per" runat="server" Text='<%# Bind("bg_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="md_fertile_eggs"><ItemTemplate><%# Eval("md_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_fertile_eggs" runat="server" Text='<%# Bind("md_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_infertile_eggs"><ItemTemplate><%# Eval("md_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_infertile_eggs" runat="server" Text='<%# Bind("md_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_chicks"><ItemTemplate><%# Eval("md_chicks") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_chicks" runat="server" Text='<%# Bind("md_chicks") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_hatcherloss"><ItemTemplate><%# Eval("md_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_hatcherloss" runat="server" Text='<%# Bind("md_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_supply"><ItemTemplate><%# Eval("md_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_supply" runat="server" Text='<%# Bind("md_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_culls"><ItemTemplate><%# Eval("md_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_culls" runat="server" Text='<%# Bind("md_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="md_per"><ItemTemplate><%# Eval("md_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="md_per" runat="server" Text='<%# Bind("md_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="mp_fertile_eggs"><ItemTemplate><%# Eval("mp_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_fertile_eggs" runat="server" Text='<%# Bind("mp_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_infertile_eggs"><ItemTemplate><%# Eval("mp_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_infertile_eggs" runat="server" Text='<%# Bind("mp_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_chicks"><ItemTemplate><%# Eval("mp_chicks") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_chicks" runat="server" Text='<%# Bind("mp_chicks") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_hatcherloss"><ItemTemplate><%# Eval("mp_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_hatcherloss" runat="server" Text='<%# Bind("mp_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_supply"><ItemTemplate><%# Eval("mp_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_supply" runat="server" Text='<%# Bind("mp_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_culls"><ItemTemplate><%# Eval("mp_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_culls" runat="server" Text='<%# Bind("mp_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mp_per"><ItemTemplate><%# Eval("mp_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mp_per" runat="server" Text='<%# Bind("mp_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="mm_fertile_eggs"><ItemTemplate><%# Eval("mm_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_fertile_eggs" runat="server" Text='<%# Bind("mm_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_infertile_eggs"><ItemTemplate><%# Eval("mm_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_infertile_eggs" runat="server" Text='<%# Bind("mm_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_chicks"><ItemTemplate><%# Eval("mm_chicks") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_chicks" runat="server" Text='<%# Bind("mm_chicks") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_hatcherloss"><ItemTemplate><%# Eval("mm_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_hatcherloss" runat="server" Text='<%# Bind("mm_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_supply"><ItemTemplate><%# Eval("mm_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_supply" runat="server" Text='<%# Bind("mm_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_culls"><ItemTemplate><%# Eval("mm_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_culls" runat="server" Text='<%# Bind("mm_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="mm_per"><ItemTemplate><%# Eval("mm_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="mm_per" runat="server" Text='<%# Bind("mm_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="comm_fertile_eggs"><ItemTemplate><%# Eval("comm_fertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_fertile_eggs" runat="server" Text='<%# Bind("comm_fertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_infertile_eggs"><ItemTemplate><%# Eval("comm_infertile_eggs") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_infertile_eggs" runat="server" Text='<%# Bind("comm_infertile_eggs") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_chicks"><ItemTemplate><%# Eval("comm_chicks") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_chicks" runat="server" Text='<%# Bind("comm_chicks") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_hatcherloss"><ItemTemplate><%# Eval("comm_hatcherloss") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_hatcherloss" runat="server" Text='<%# Bind("comm_hatcherloss") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_supply"><ItemTemplate><%# Eval("comm_supply") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_supply" runat="server" Text='<%# Bind("comm_supply") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_culls"><ItemTemplate><%# Eval("comm_culls") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_culls" runat="server" Text='<%# Bind("comm_culls") %>' /></EditItemTemplate></asp:TemplateField>
<asp:TemplateField HeaderText="comm_per"><ItemTemplate><%# Eval("comm_per") %></ItemTemplate><EditItemTemplate><asp:TextBox ID="comm_per" runat="server" Text='<%# Bind("comm_per") %>' /></EditItemTemplate></asp:TemplateField>


<asp:TemplateField HeaderText="Hatchery">
    <ItemTemplate><%# Eval("hatchery") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="hatchery" runat="server" Text='<%# Bind("hatchery") %>' /></EditItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="Machine">
    <ItemTemplate><%# Eval("machine") %></ItemTemplate>
    <EditItemTemplate><asp:TextBox ID="machine" runat="server" Text='<%# Bind("machine") %>' /></EditItemTemplate>
</asp:TemplateField>

<asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />

</Columns>
                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                    <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#FFF1D4" />
                    <SortedAscendingHeaderStyle BackColor="#B95C30" />
                    <SortedDescendingCellStyle BackColor="#F1E5CE" />
                    <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            </div>
        </div>
    </div>
</asp:Content>