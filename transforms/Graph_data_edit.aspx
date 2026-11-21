<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="Graph_data_edit.aspx.vb" Inherits="transforms_Graph_data_edit" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">

    <div class="container">

        <h3>Edit Graph Data</h3>

        <div class="row">
      
            <div class="col-sm-4">
                Select Flock Number:
                <input type="text" id="flock_no" runat="server" />
            </div>
            <div class="col-sm-4">
                                <asp:Button runat="server" ID="btnSubmit" Text="Show Flock Data" />
            </div>
        </div>
        <asp:Label ID="lblMessage" runat="server"
            CssClass="graph-message" />
        <br />
        <br />

        <div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="gvGraph" runat="server"
                    AutoGenerateColumns="false"
                    DataKeyNames="flock_no,age"
                    CellPadding="4"
                    GridLines="Both"
                    OnRowEditing="gvGraph_RowEditing"
                    OnRowCancelingEdit="gvGraph_RowCancelingEdit"
                    OnRowUpdating="gvGraph_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="id"
                            HeaderText="ID" ReadOnly="True" />

                        <asp:BoundField DataField="flock_no"
                            HeaderText="Flock No" ReadOnly="True" />

                        <asp:BoundField DataField="age"
                            HeaderText="Age" ReadOnly="True" />

                        <asp:BoundField DataField="entry_date"
                            HeaderText="Entry Date"
                            DataFormatString="{0:dd-MMM-yyyy}"
                            HtmlEncode="False" />

                        <asp:BoundField DataField="fstock"
                            HeaderText="Flock Stock" />

                        <asp:BoundField DataField="eggstock"
                            HeaderText="Egg Stock" />

                        <asp:BoundField DataField="heggstock"
                            HeaderText="Hatching Egg Stock" />

                        <asp:BoundField DataField="mortality"
                            HeaderText="Mortality" />

                        <asp:BoundField DataField="crack"
                            HeaderText="Crack" />

                        <asp:BoundField DataField="comm"
                            HeaderText="Commercial" />

                        <asp:BoundField DataField="leaker"
                            HeaderText="Leaker" />

                        <asp:BoundField DataField="total_loading"
                            HeaderText="Total Loading" />

                        <asp:BoundField DataField="total_fertile"
                            HeaderText="Total Fertile" />

                        <asp:BoundField DataField="total_infertile"
                            HeaderText="Total Infertile" />

                        <asp:BoundField DataField="total_chicks"
                            HeaderText="Total Chicks" />

                        <asp:BoundField DataField="oth1"
                            HeaderText="Other 1" />

                        <asp:BoundField DataField="oth2"
                            HeaderText="Other 2" />

                        <asp:CommandField ShowEditButton="True"
                            EditText="Edit"
                            UpdateText="Update"
                            CancelText="Cancel" />
                    </Columns>
                </asp:GridView>
                <
            </div>
        </div>
    </div>
</asp:Content>





