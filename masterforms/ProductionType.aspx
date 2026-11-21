<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="ProductionType.aspx.vb" Inherits="masterforms_ProductionType" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">

        <h3>Production Type</h3>

        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider">
                <form>
                    <div class="form-group">
                        <label for="exampleInputEmail1">Production Type Code</label>
                        <asp:TextBox ID="prdtypecode" runat="server" class="form-control" placeholder="Entry Production Type Code" />
                        <asp:RequiredFieldValidator ID="rfvcandidate" 
   runat="server" ControlToValidate ="prdtypecode"
   ErrorMessage="Please Enter Valid Code" 
   InitialValue="Please Enter Valid Code"> 
</asp:RequiredFieldValidator>
                         </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">Production Type Name</label>
                        <asp:TextBox runat="server" class="form-control" ID="prdtypename"
                            placeholder="Entry Production Type Name" />
                    </div>
                    <br>
                    <input type="date" id="sday" name="sday" v-model="sday">
                    <asp:Button class="btn btn-primary" ID="btnSave" runat="server" Text="Save"></asp:Button>
                </form>

            </div>
        </div>

        <div class="row">
            <div class="col-sm" >

                <asp:DataGrid ID ="dgprdtype" runat="server" BorderWidth="2px" GridLines ="Both" BorderStyle="Solid" class="table table-striped table-hover">
                    <HeaderStyle BackColor="#CCFFFF" />
                </asp:DataGrid>
            </div>
        </div>
    </div>
    <style type="text/css">
        #prdform {
            border: solid 1px green;
            padding: .5em;
            margin: 1em;
        }

        #SiteMaster_dgprdtype {
            font-size: 1.2em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>

