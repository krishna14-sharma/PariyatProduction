<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="false" CodeFile="ExpenseHeadMaste.aspx.vb" Inherits="masterforms_ExpenseHeadMaste" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Expense Head</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
            </div>
        </div>

          <div class="row">
            <div class="col-sm-3">
                Expense Type:
            </div>
            <div class="col-sm-3 form-group">
                <asp:DropDownList ID="exptype" runat="server"  class="form-control">
                    <asp:ListItem>Direct Expenses</asp:ListItem>
                    <asp:ListItem>InDirectExpense</asp:ListItem>
                </asp:DropDownList>
            </div>
            </div>
        <div class="row">
            <div class="col-sm" id="prdform1">
                <hr class="divider" />
            </div>
        </div>

        <div class="row">
            <div class="col-sm-3">
                Expense Head:
            </div>
            <div class="col-sm-3 form-group">
                <asp:TextBox ID="exphead" runat="server"  class="form-control"/>
            </div>
            </div>
         <div class="row">
            <div class="col-sm" id="prdform1">
                <hr class="divider" />
            </div>
        </div>

            <div class="row">
                <div class="col-sm-3">
                    <asp:Button Text="Save" runat="server" ID="btnSaveExpmaster" />
                </div>
                <div class="col-sm-3">
                    <asp:Button Text="Close" runat="server" />
                </div>
            </div>
         <div class="row">
            <div class="col-sm" id="prdform1">
                <hr class="divider" />
            </div>
        </div>

            <div class="row">
                <div class="col-sm-12">
                    <asp:DataGrid ID="dgexphead" runat="server" />
                </div>
               
            </div>
        </div>
</asp:Content>
