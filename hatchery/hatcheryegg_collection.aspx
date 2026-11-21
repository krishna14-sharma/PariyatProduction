<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/SiteMasterHatch.master"
CodeFile="hatcheryegg_collection.aspx.vb" Inherits="hatchery_hatcheryegg_collection" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">

<div class="container mt-4">

    <div class="card shadow-lg">
        <div class="card-header bg-primary text-white text-center">
            <h3>Hatching Eggs Phase - I (Collection) Entry</h3>
        </div>

        <div class="card-body">

            <!-- Date -->
            <div class="row mb-3">
                <div class="col-md-2">
                    <label>Date</label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="eggcolldate" runat="server" TextMode="Date" CssClass="form-control"/>
                </div>
            </div>

            <!-- Flock Type -->
            <div class="row mb-3">
                <div class="col-md-2">
                    <label>Flock Type</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbftype" runat="server" AutoPostBack="True" CssClass="form-select"/>
                </div>
            </div>

            <!-- Area Name -->
            <div class="row mb-3">
                <div class="col-md-2">
                    <label>Area Name</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbAreaName" runat="server" AutoPostBack="True" CssClass="form-select"/>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="btnShowFlock" runat="server" Text="Show Flock" CssClass="btn btn-primary"/>
                </div>
            </div>

            <!-- Flock No -->
            <div class="row mb-3">
                <div class="col-md-2">
                    <label>Flock No</label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="flockno" runat="server" CssClass="form-select"/>
                </div>
                <div class="col-md-2">
                    <asp:Label ID="lblFlockno" runat="server" Font-Bold="True" ForeColor="Red"/>
                </div>
                <div class="col-md-2">
                    <asp:Button ID="Button3" runat="server" Text="Show Eggs" CssClass="btn btn-success"/>
                </div>
            </div>

            <!-- Age & Egg Weight -->
            <div class="row mb-3">
                <div class="col-md-2">
                    <label>Age (Desirable)</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="LblAge" runat="server" CssClass="fw-bold"/>
                </div>

                <div class="col-md-2">
                    <label>Egg Weight</label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtegg_wt" runat="server" CssClass="form-control"/>
                </div>
            </div>

            <!-- Eggs -->
            <div class="row mb-3">
                <div class="col-md-2">Total Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txteggs" runat="server" ReadOnly="True" CssClass="form-control"/>
                </div>

                <div class="col-md-2">Hatch Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txthatchegg" runat="server" ReadOnly="True" CssClass="form-control"/>
                </div>
            </div>

            <!-- Spot Eggs -->
            <div class="row mb-3">
                <div class="col-md-2">Spot Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtspot" runat="server" CssClass="form-control"/>
                </div>

                <div class="col-md-2">Commercial Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="TxtCommEggs" runat="server" ReadOnly="True" CssClass="form-control"/>
                </div>
            </div>

            <!-- Crack & Leak -->
            <div class="row mb-3">
                <div class="col-md-2">Crack Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="TxtCrackEgg" runat="server" ReadOnly="True" CssClass="form-control"/>
                </div>

                <div class="col-md-2">Leaker Eggs</div>
                <div class="col-md-3">
                    <asp:TextBox ID="TxtLeakEggs" runat="server" ReadOnly="True" CssClass="form-control"/>
                </div>
            </div>

            <!-- Location -->
            <div class="row mb-3">
                <div class="col-md-2">Location</div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbloc" runat="server" AutoPostBack="True" CssClass="form-select"/>
                </div>

                <div class="col-md-2">Cold Room</div>
                <div class="col-md-3">
                    <asp:DropDownList ID="coldroomname" runat="server" CssClass="form-select"/>
                </div>
            </div>

            <!-- Messages -->
            <div class="row mb-3">
                <div class="col-md-12">
                    <asp:Label ID="lblString" runat="server" Font-Bold="True" ForeColor="Red"/>
                </div>
            </div>

            <!-- Buttons -->
            <div class="row text-center mb-3">
                <div class="col-md-12">
                    <asp:Button ID="Button1" runat="server" Text="Save Egg Collection" CssClass="btn btn-success me-2"/>
                    <asp:Button ID="Button4" runat="server" Text="Show Data" CssClass="btn btn-info me-2"/>
                    <asp:Button ID="Button2" runat="server" Text="Close" CssClass="btn btn-danger"/>
                </div>
            </div>

            <div class="text-center">
                <asp:Label ID="lblprint" runat="server" ForeColor="Red" Font-Bold="true"/>
            </div>

        </div>
    </div>


    <!-- Data Grid -->
    <div class="card mt-4 shadow">
        <div class="card-header bg-dark text-white">
            Egg Collection Data
        </div>
        <div class="card-body">

            <asp:DataGrid ID="eggcollgrid"
                runat="server"
                AutoGenerateColumns="true"
                CssClass="table table-striped table-bordered table-hover">
            </asp:DataGrid>

        </div>
    </div>

</div>

</asp:Content>