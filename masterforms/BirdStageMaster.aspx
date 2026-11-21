<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="BirdStageMaster.aspx.vb" Inherits="masterforms_BirdStageMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Bird Stage Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:Label ID="lblString" runat="server" />
                <hr class="divider">
                <form>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Bird Stage Code</label>
                                <asp:TextBox ID="birdstagecode" runat="server" class="form-control" placeholder="Entry Bird Stage Code" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="loccode">Bird Stage Name</label>
                                <asp:TextBox ID="birdstagename" runat="server" class="form-control" placeholder="Entry Bird Stage Name" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="startage">Start Age(Week)</label>
                                <asp:TextBox ID="startage" runat="server" class="form-control" placeholder="Start Age(Week)" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="endage">End Age(Week)</label>
                                <asp:TextBox ID="endage" runat="server" class="form-control" placeholder="End Age(Week)" />
                            </div>
                        </div>
                          <div class="col-sm-6">
                            <div class="form-group">
                                <label for="endage">Age(Week)</label>
                                <asp:TextBox ID="age" runat="server" class="form-control" placeholder="Age(Week)" />
                            </div>
                        </div>
                          <div class="col-sm-6">
                            <div class="form-group">
                                
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
              <div class="row">
            <div class="col-sm" id="prdform_grd">
                <asp:GridView ID="birdstage_grid" runat="server" class="table table-striped table-hover" />
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

        #SiteMaster_birdstage_grid {
            font-size: 1.1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
