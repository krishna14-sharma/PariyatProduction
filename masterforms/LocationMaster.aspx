<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="LocationMaster.aspx.vb" Inherits="images_LocationMaster" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Location/Area Master</h3>
            <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider">
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
                                <label for="loccode">Location Code</label>
                                <asp:TextBox ID="loccode" runat="server" class="form-control" placeholder="Entry Location Code" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="exampleInputEmail1">Location Name</label>
                              <asp:TextBox ID="locname" runat="server" class="form-control" placeholder="Location Name" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="exampleInputPassword1">Location Production Type</label>
                                <asp:DropDownList ID="productiontype" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <br/>
                    <input type="date" id="sday" name="sday">
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                   <br/> 
                     <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300" ></asp:Label>
                </form>

            </div>
        </div>
           <div class="row">
            <div class="col-sm" id ="prdform_grd">
                <asp:GridView ID ="loc_grid" runat="server" class="table table-striped table-hover"/>
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

        #SiteMaster_loc_grid {
            font-size: 1.1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
    </asp:Content>