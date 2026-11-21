<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="false" CodeFile="Sepreation_reason_master.aspx.vb" Inherits="masterforms_Sepreation_reason_master"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Sepration Reason Master</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:Label ID="lblString" runat="server" />
                <hr class="divider">
                <form>
                    <div class="row">                    
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="loccode">Sepration Reason Master</label>
                                <asp:TextBox ID="sepreason" runat="server" class="form-control" placeholder="" />
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
                <asp:GridView ID="sepreason_grid" runat="server" class="table table-striped table-hover" />
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
