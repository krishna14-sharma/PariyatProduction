<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="JabalPurHarcheryEditLoading.aspx.vb" Inherits="transforms_JabalPurHarcheryEditLoading" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Edit Jabalpur Hatchery Loading Data</h3>
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
                <asp:Button runat="server" ID="btnSubmit" Text="Show Loading Data" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="GridView1" runat="server"
                    CssClass="grid"
                    AutoGenerateColumns="False"
                    DataKeyNames="id"
                    AllowPaging="True"
                    OnRowEditing="GridView1_RowEditing"
                    OnRowUpdating="GridView1_RowUpdating"
                    OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowDeleting="GridView1_RowDeleting" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">

                    <Columns>

                        <asp:BoundField DataField="entry_date" HeaderText="Entry Date" ReadOnly="true" />
                        <asp:BoundField DataField="egg_coll_date" HeaderText="Egg Date From" ReadOnly="true" />
                        <asp:BoundField DataField="egg_coll_date_to" HeaderText="Egg Date To" ReadOnly="true" />

                        <asp:BoundField DataField="ftype" HeaderText="Flock Type" ReadOnly="true" />
                        <asp:BoundField DataField="flock_no" HeaderText="Flock No" ReadOnly="true" />
                        <asp:BoundField DataField="age" HeaderText="Age" ReadOnly="true" />

                        <asp:BoundField DataField="f_taken" HeaderText="F Taken" />
                        <asp:BoundField DataField="f_set" HeaderText="F Set" />
                        <asp:BoundField DataField="f_rej" HeaderText="F Rej" />
                        <asp:BoundField DataField="f_leaker" HeaderText="F Leaker" />

                        <asp:BoundField DataField="bg_taken" HeaderText="BG Taken" />
                        <asp:BoundField DataField="bg_set" HeaderText="BG Set" />
                        <asp:BoundField DataField="bg_rej" HeaderText="BG Rej" />
                        <asp:BoundField DataField="bg_leaker" HeaderText="BG Leaker" />

                        <asp:BoundField DataField="md_taken" HeaderText="MD Taken" />
                        <asp:BoundField DataField="md_set" HeaderText="MD Set" />
                        <asp:BoundField DataField="md_rej" HeaderText="MD Rej" />
                        <asp:BoundField DataField="md_leaker" HeaderText="MD Leaker" />

                        <asp:BoundField DataField="mp_taken" HeaderText="MP Taken" />
                        <asp:BoundField DataField="mp_set" HeaderText="MP Set" />
                        <asp:BoundField DataField="mp_rej" HeaderText="MP Rej" />
                        <asp:BoundField DataField="mp_leaker" HeaderText="MP Leaker" />

                        <asp:BoundField DataField="mm_taken" HeaderText="MM Taken" />
                        <asp:BoundField DataField="mm_set" HeaderText="MM Set" />
                        <asp:BoundField DataField="mm_rej" HeaderText="MM Rej" />
                        <asp:BoundField DataField="mm_leaker" HeaderText="MM Leaker" />

                        <asp:BoundField DataField="comm_taken" HeaderText="Comm Taken" />
                        <asp:BoundField DataField="comm_set" HeaderText="Comm Set" />
                        <asp:BoundField DataField="comm_rej" HeaderText="Comm Rej" />
                        <asp:BoundField DataField="comm_leaker" HeaderText="Comm Leaker" />

                        <asp:BoundField DataField="hatchery" HeaderText="Hatchery" ReadOnly="true" />
                        <asp:BoundField DataField="machine" HeaderText="Machine" ReadOnly="true" />
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
            </div>
        </div>
    </div>
</asp:Content>
