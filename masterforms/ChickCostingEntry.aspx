<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMaster.master" AutoEventWireup="false" CodeFile="ChickCostingEntry.aspx.vb" Inherits="masterforms_ChickCostingEntry" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
         <title>Expense Entry Form</title>
        <style>
            body {
                font-family: Arial;
                background-color: #f5f5f5;
            }

            .container {
                width: 650px;
                margin: 30px auto;
                background: white;
                padding: 25px;
                border-radius: 10px;
                box-shadow: 0px 0px 8px #ccc;
            }

            h2 {
                text-align: center;
                margin-bottom: 25px;
            }

            .row {
                margin-bottom: 15px;
                display: flex;
                align-items: center;
            }

            .label {
                width: 180px;
                font-weight: bold;
            }

            .textbox,
            .dropdown_exp {
                width: 250px;
                height: 32px;
                padding-left: 8px;
                border: 1px solid #999;
                border-radius: 4px;
            }

            .small-box {
                width: 100px;
                height: 32px;
                padding-left: 8px;
                border: 1px solid #999;
                border-radius: 4px;
            }

            .btn-save {
                background-color: #007bff;
                color: white;
                border: none;
                padding: 10px 25px;
                border-radius: 5px;
                cursor: pointer;
                font-size: 16px;
            }

                .btn-save:hover {
                    background-color: #0056b3;
                }

            .month-year {
                display: flex;
                gap: 20px;
            }

            .table {
                border-collapse: collapse;
                width: 100%;
            }

                .table th {
                    background-color: #007bff;
                    color: white;
                    padding: 10px;
                }

                .table td {
                    border: 1px solid #ddd;
                    padding: 8px;
                }
        </style>
 
 
        <div class="container">
            <h2>Costing Entry</h2>
            <!-- System Type -->
            <div class="row">
                <div class="label">System Type</div>
                <asp:DropDownList ID="ddlSystemType" runat="server" CssClass="dropdown_exp">
                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                    <asp:ListItem Text="DL" Value="DL"></asp:ListItem>
                    <asp:ListItem Text="CAGE" Value="CAGE"></asp:ListItem>
                    <asp:ListItem>E.C</asp:ListItem>
                </asp:DropDownList>
            </div>
            <!-- Area Name -->
            <div class="row">
                <div class="label">Area Name</div>
                <asp:DropDownList ID="ddlAreaName" runat="server" CssClass="dropdown_exp" AutoPostBack="True">
                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <!-- Flock No -->
            <div class="row">
                <div class="label">Flock No</div>
                <asp:DropDownList ID="ddlFlockNo" runat="server" CssClass="dropdown_exp">
                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>

            <!-- Expense Type -->
            <div class="row">
                <div class="label">Expense Type</div>
                <asp:DropDownList ID="ddlExpenseType" runat="server" CssClass="dropdown_exp" AutoPostBack="true">
                    <asp:ListItem>Direct Expenses</asp:ListItem>
                    <asp:ListItem>InDirectExpense</asp:ListItem>
                    <asp:ListItem>OtherExpense</asp:ListItem>
                </asp:DropDownList>
            </div>
            <!-- Expense Head -->
            <div class="row">
                <div class="label">Expense Head</div>
                <asp:DropDownList ID="ddlExpenseHead" runat="server" CssClass="dropdown_exp">
                    <asp:ListItem Text="Select" Value=""></asp:ListItem>
                </asp:DropDownList>
            </div>
            <!-- Qty -->
            <div class="row">
                <div class="label">Qty</div>
                <asp:TextBox ID="txtQty" runat="server" CssClass="textbox"></asp:TextBox>
            </div>

            <!-- Unit -->
            <div class="row">
                <div class="label">Unit</div>
                <asp:DropDownList ID="ddlUnit" runat="server" CssClass="textbox"></asp:DropDownList>
            </div>
            <!-- Rate -->
            <div class="row">
                <div class="label">Rate</div>
                <asp:TextBox ID="txtRate" runat="server" CssClass="textbox"></asp:TextBox>
            </div>
            <!-- Rate -->
            <div class="row">
                <div class="label">Amount</div>
                <asp:TextBox ID="txtAmount" runat="server" CssClass="textbox"></asp:TextBox>
            </div>
            <!-- Month and Year -->
            <div class="row">
                <div class="label">Month / Year</div>
                <div class="month-year">
                    <asp:DropDownList ID="ddlMonth" runat="server" CssClass="small-box"
                        placeholder="Month">
                    </asp:DropDownList>
                    <asp:DropDownList ID="ddlYear" runat="server" CssClass="small-box"
                        placeholder="Year">
                    </asp:DropDownList>
                </div>
            </div>
            <!-- Save Button -->
            <div class="row" style="justify-content: center; margin-top: 25px;">
                <asp:Button ID="btnSave"
                    runat="server"
                    Text="SAVE"
                    CssClass="btn-save"
                                   UseSubmitBehavior="false"
                    OnClientClick="this.disabled=true;this.value='Saving...';" />
            </div>
        </div>

        <div class="row">
            <div class="col">
                <br />

                <asp:GridView ID="gvCosting"
                    runat="server"
                    AutoGenerateColumns="False"
                    Width="100%"
                    DataKeyNames="Id"
                    OnRowDeleting="gvCosting_RowDeleting"
                    OnRowEditing="gvCosting_RowEditing"
                    CssClass="table">

                    <Columns>

                        <asp:BoundField DataField="Id" HeaderText="ID" />

                        <asp:BoundField DataField="SystemType"
                            HeaderText="System Type" />

                        <asp:BoundField DataField="AreaName"
                            HeaderText="Area" />

                        <asp:BoundField DataField="FlockNo"
                            HeaderText="Flock No" />

                        <asp:BoundField DataField="ExpenseType"
                            HeaderText="Expense Type" />

                        <asp:BoundField DataField="ExpenseHead"
                            HeaderText="Expense Head" />

                        <asp:BoundField DataField="Qty"
                            HeaderText="Qty" />

                        <asp:BoundField DataField="Unit"
                            HeaderText="Unit" />

                        <asp:BoundField DataField="Rate"
                            HeaderText="Rate" />

                        <asp:BoundField DataField="TotalAmount"
                            HeaderText="Total" />

                        <asp:CommandField ShowEditButton="True"
                            ButtonType="Button"
                            EditText="Edit" />

                        <asp:CommandField ShowDeleteButton="True"
                            ButtonType="Button"
                            DeleteText="Delete" />

                    </Columns>

                </asp:GridView>
            </div>
        </div>


</asp:Content>

