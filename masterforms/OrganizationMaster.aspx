<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="OrganizationMaster.aspx.vb" Inherits="masterforms_OrganizationMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Organization Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider">
                <form>
                    <div class="row">

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgcode">Organization Code</label>
                                <asp:TextBox ID="orgcode" runat="server" class="form-control" placeholder="Entry Organization Code" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Organization Name</label>
                                <asp:TextBox ID="orgname" runat="server" class="form-control" placeholder="Organization Name" />
                            </div>
                        </div>
                    </div>

                    <div class="form-group">
                        <label for="orgaddress">Organization Address</label>
                        <asp:TextBox ID="orgaddress" runat="server" class="form-control" placeholder="Address" />
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="state">state</label>
                                <asp:TextBox ID="state" runat="server" class="form-control" placeholder="State" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="country">Country</label>
                                <asp:TextBox ID="country" runat="server" class="form-control" placeholder="Country" />
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="phone">Phone</label>
                                <asp:TextBox ID="phone" runat="server" class="form-control" placeholder="Phone" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="email">Email</label>
                                <asp:TextBox ID="email" runat="server" class="form-control" placeholder="Email" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gst">GST</label>
                                <asp:TextBox ID="gst" runat="server" class="form-control" placeholder="GST" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Production Type</label>
                                <asp:DropDownList ID="productiontype" runat="server" class="form-control"></asp:DropDownList>
                            </div>

                        </div>
                    </div>
                    <div class="form-group">
                        Is Accounting
                       <asp:CheckBox ID="isaccounting" runat="server" class="form-control" />
                    </div>
                    <br>
                        <input type="date" id="sday" name="sday">

                        <asp:Button runat="server" class="btn btn-primary" Text="Save" ID="btnsaveorg" />
                    </br>
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">
                <asp:GridView ID="org_grid" runat="server" class="table table-striped table-hover" />
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

