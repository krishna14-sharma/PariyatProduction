<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="ShedLinesMaster.aspx.vb" Inherits="masterforms_ShedLinesMaster" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Shed Pen/Line Information Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
                <form>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Organization Name</label>
                                <asp:DropDownList ID="orgname" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="locationname">Location Name</label>
                                <asp:DropDownList ID="locationname" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedname">Shed Name</label>
                                <asp:DropDownList ID="shedname" runat="server" class="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcapacity">Pen/Line Code</label>
                                <asp:TextBox ID="linecode" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedtype">Pen/Line Name</label>
                                <asp:TextBox ID="linename" runat="server" class="form-control"  />
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="divshed_grid">
                <asp:GridView ID="shed_grid" runat="server" class="table table-striped table-hover" />
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

        #SiteMaster_shed_grid {
            font-size: 1.1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
