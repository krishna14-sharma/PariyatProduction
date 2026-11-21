<%@ Page Language="VB" MasterPageFile="../SiteMaster.master"  AutoEventWireup="true" CodeFile="ChicksPurchase.aspx.vb" Inherits="transforms_ChicksPurchase" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <div class="container">
            <h3>Chicks Purchase Entry</h3>
            <div class="row">
                <div class="col-sm" id="prdform">
                    <hr class="divider" />
                    <form>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="orgname">Bird Breed</label>
                                    <asp:DropDownList ID="birdtype" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="locationname">Party Name</label>
                                    <asp:DropDownList ID="partyname" runat="server" class="form-control"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="shedcode">Inward No</label>
                                    <asp:TextBox ID="inwno" runat="server" class="form-control" placeholder="Inward Number" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="shedname">Inward Date </label>
                                    <input type="date" id="inwdate" name="inwdate" runat="server" class="form-control">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="shedcapacity">Invoice Number</label>
                                    <asp:TextBox ID="invno" runat="server" class="form-control" placeholder="Invoice Number" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="shedtype">Invoice Date</label>
                                    <input type="date" id="invdate" name="invdate" runat="server" class="form-control">
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="fqty">Female Quantity</label>
                                    <asp:TextBox ID="fqty" runat="server" class="form-control" placeholder="Entry Female Quantity" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="mqty">Male Quantity</label>
                                    <asp:TextBox ID="mqty" runat="server" class="form-control" placeholder="Entry Male Quantity" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="frate">Female Rate</label>
                                    <asp:TextBox ID="frate" runat="server" class="form-control" placeholder="Entry Female Rate" />
                                </div>
                            </div>
                            <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="mrate">Male Rate</label>
                                    <asp:TextBox ID="mrate" runat="server" class="form-control" placeholder="Entry Male Rate" />
                                </div>
                            </div>
                        </div>
                        <br>
                        <asp:Button ID="chickspurchase_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                        <br />
                        <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                    </form>
                </div>
            </div>
            <div class="row">
                <div class="col-sm" id="divshed_grid">
                    <asp:GridView ID="chikc_purchase_grid" runat="server" class="table table-striped table-hover" />
                </div>
            </div>
        </div>
    </div>

        <style type="text/css">
        #prdform {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

         #divshed_grid {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

        #SiteMaster_chikc_purchase_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
