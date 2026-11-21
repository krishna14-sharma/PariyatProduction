<%@ Page Language="VB" MasterPageFile="~/SiteMasterPit.master" AutoEventWireup="false" CodeFile="pit_mis_itemname.aspx.vb" Inherits="pit_pit_mis_itemname" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterPit" runat="server">
    <div class="container">
        <div class="row">

            <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemname">Product Type</label>
                    <asp:DropDownList  ID="cmbPrdtype" runat="server" class="form-control" >
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
                     <asp:DropDownList  ID="cmbBreedType" runat="server" class="form-control" >
                        <asp:ListItem>BR</asp:ListItem>
                        <asp:ListItem>LR</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
              </div>
          
             <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemname">Enter Item Name:</label>
                    <asp:TextBox ID="txtitemname" runat="server" class="form-control" />
                </div>
            </div>
                 </div>
          
            
             <div class="row">
              <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemname"></label>
                    <asp:Button ID="item_btnSave" Text="Save" runat="server" class="btn btn-primary form-control" />
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <asp:GridView ID="dgItenName" GridLines="Both" runat="server">
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
