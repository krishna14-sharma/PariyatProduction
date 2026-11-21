<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="chicksinward.aspx.vb" Inherits="masterforms_chicksinward" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container bg-light">

        <div class="card shadow-lg rounded-4">
            <div class="card-header bg-primary text-white">
                <h4 class="mb-0 text-center">Chicks Inward Entry</h4>
            </div>
            <div class="card-body">
                <!-- Row 1: Date, Flock, Birds F -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label class="form-label">Date</label>
                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Flock</label>
                        <asp:TextBox ID="txtFlock" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Birds Stock (F)</label>
                        <asp:TextBox ID="txtBirdsF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Row 2: Birds M, Mortality F, Mortality M -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label class="form-label">Birds Stock (M)</label>
                        <asp:TextBox ID="txtBirdsM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Mortality (F)</label>
                        <asp:TextBox ID="txtMortalityF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Mortality (M)</label>
                        <asp:TextBox ID="txtMortalityM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Row 3: Loose F, Loose M, Short F -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label class="form-label">Loose (F)</label>
                        <asp:TextBox ID="txtLooseF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Loose (M)</label>
                        <asp:TextBox ID="txtLooseM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Short (F)</label>
                        <asp:TextBox ID="txtShortF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Row 4: Short M, Good F, Good M -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label class="form-label">Short (M)</label>
                        <asp:TextBox ID="txtShortM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Good Condition (F)</label>
                        <asp:TextBox ID="txtGoodF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Good Condition (M)</label>
                        <asp:TextBox ID="txtGoodM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Row 5: Rate F, Rate M, Brooding Area -->
                <div class="row mb-3">
                    <div class="col-md-4">
                        <label class="form-label">Rate (F)</label>
                        <asp:TextBox ID="txtRateF" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Rate (M)</label>
                        <asp:TextBox ID="txtRateM" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-4">
                        <label class="form-label">Brooding Area</label>
                        <asp:TextBox ID="txtBroodingArea" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>

                <!-- Row 6: Remark (Full Width) -->
                <div class="row mb-3">
                    <div class="col-md-12">
                        <label class="form-label">Remark</label>
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <!-- Buttons -->
                <div class="text-center">
                    <asp:HiddenField ID="hfId" runat="server" />
                    <asp:Button ID="btnInsert" runat="server" CssClass="btn btn-success me-2" Text="Insert" OnClick="btnInsert_Click" />
                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-warning me-2" Text="Update" OnClick="btnUpdate_Click" />
                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete" OnClick="btnDelete_Click" />
                </div>
            </div>
        </div>
        <!-- GridView -->

        <div class="card shadow-lg rounded-4 mt-4">
            <div class="card-header bg-secondary text-white">
                <h5 class="mb-0 text-center">Chicks Summary Records</h5>
            </div>
            <div class="card-body">
                <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-striped text-center"
                    AutoGenerateColumns="False" DataKeyNames="Id"
                    OnSelectedIndexChanged="GridView1_SelectedIndexChanged">
                    <Columns>
                        <asp:TemplateField HeaderText="ID">
                            <ItemTemplate>
                                <%# Eval("Id") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <%# Eval("EntryDate", "{0:yyyy-MM-dd}") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Flock">
                            <ItemTemplate>
                                <%# Eval("Flock") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Birds F">
                            <ItemTemplate>
                                <%# Eval("BirdsStockF") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Birds M">
                            <ItemTemplate>
                                <%# Eval("BirdsStockM") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkSelect" runat="server" CommandName="Select" CssClass="btn btn-sm btn-primary">Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </div>
</asp:Content>
