<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="BirdTypeMaster.aspx.vb" Inherits="masterforms_BirdTypeMaster" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Bird Breed Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
                <form>
                    <div class="form-group">
                        <label for="birdtypecode">Bird Type Code</label>
                        <asp:TextBox ID="birdtypecode" runat="server" class="form-control" placeholder="Bird Breed Type Code" />
                    </div>
                    <div class="form-group">
                        <label for="birdname">Bird Type Name</label>
                        <asp:TextBox ID="birdname" runat="server" class="form-control" placeholder="Bird Breed Type Name" />
                    </div>
                    <br />
                    <input type="date" id="sday" name="sday" >
                    <asp:Button ID="birdtype_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">
                <asp:GridView ID="birdtype_grid" runat="server" class="table table-striped table-hover" />
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

        #SiteMaster_birdtype_grid {
            font-size: 1.1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>