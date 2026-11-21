<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="FlockMaster.aspx.vb" Inherits="transforms_FlockMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Flock Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
                <form>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="birdtype">Bird Breed</label>
                                <asp:DropDownList ID="birdtype" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="locationname">Flock Number:</label>                            
                                 <asp:TextBox ID="flockno" runat="server" class="form-control" placeholder="Flock Number" />
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
                                <label for="hatchdate">Hatch Date </label>
                                <input type="date" id="hatchdate" name="hatchdate" runat="server" class="form-control">
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
      
                    <br>
                    <asp:Button ID="flockmaster_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>

           <div class="row">
                <div class="col-sm" id="divshed_grid">
                    <asp:GridView ID="flockmaster_grid" runat="server" class="table table-striped table-hover" />
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

        #SiteMaster_flockmaster_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
