<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="ProductMaster.aspx.vb" Inherits="masterforms_ProductMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Product Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:Label ID="lblString" runat="server" />
                <hr class="divider">
                <form>
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgcode">Product Code</label>
                                <asp:TextBox ID="prdcode" runat="server" class="form-control" placeholder="Product Code" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Product Name</label>
                                <asp:TextBox ID="prdname" runat="server" class="form-control" placeholder="Product Name" />
                            </div>
                        </div>
                    </div>

                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgcode">Product Category</label>
                                <asp:DropDownList ID="prdcat" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Product Description</label>
                                <asp:TextBox ID="prddesc" runat="server" class="form-control" placeholder="Product Name" />
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="state">Generic Name</label>
                                <asp:TextBox ID="genericname" runat="server" class="form-control" placeholder="Generic" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="country">Composition</label>
                                <asp:TextBox ID="composition" runat="server" class="form-control" placeholder="Composition" />
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="phone">Rate</label>
                                <asp:TextBox ID="rate" runat="server" class="form-control" placeholder="Rate" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="email">Location/Area</label>
                                <asp:DropDownList ID="location" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        Is Active
                       <asp:CheckBox ID="isactive" runat="server" class="form-control" />
                    </div>
                    <br />
                    <asp:Button runat="server" class="btn btn-primary" Text="Save" ID="btnsaveprd" />
                    <br/>
                     <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">
                <asp:GridView ID="prd_grid" runat="server" class="table table-striped table-hover" />
            </div>
        </div>
    </div>
    <style type="text/css">
        #prdform {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

         #prdform_grd {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

        #SiteMaster_org_grid {
            font-size: 1.1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>

</asp:Content>