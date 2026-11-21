<%@ Page Language="VB" MasterPageFile="~/SiteMasterPit.master" AutoEventWireup="false" CodeFile="pit_mis_opening.aspx.vb" Inherits="pit_pit_mis_opening" EnableEventValidation="false" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <h2>Pit Egg/Bird Opening</h2>
        <form>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="entrydate">Entry Date</label>
                        <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control">
                    </div>
                </div>
            </div>
            <div class="row">

                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="itemname">Product Type</label>
                        <asp:DropDownList ID="cmbPrdtype" runat="server" class="form-control">
                            <asp:ListItem>Egg</asp:ListItem>
                            <asp:ListItem>Bird</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="itemname"></label>
                        <asp:DropDownList ID="cmbBreedType" runat="server" class="form-control" AutoPostBack="True">
                            <asp:ListItem>BR</asp:ListItem>
                            <asp:ListItem>LR</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="itemname">Item Name</label>
                        <asp:DropDownList ID="cmbItemName" runat="server" class="form-control" />
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="itemname">Qty In Number</label>
                        <asp:TextBox ID="txtQtyNumber" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="itemname">Qty In Kg</label>
                        <asp:TextBox ID="txtKg" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
             <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        
                        <asp:Label ID="lblmsg" runat="server" class="form-control" />
                    </div>
                </div>
            </div>
        </form>

        <asp:Button ID="item_btnSave" Text="Save" runat="server" class="btn btn-primary" />


        <div class="row">
            <div class="col-sm-6">
                <asp:GridView ID="dgItemOpening" GridLines="Both" class="table table-striped table-hover" Font-Size="Small" runat="server">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
