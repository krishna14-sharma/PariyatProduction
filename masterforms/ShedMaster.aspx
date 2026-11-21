<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="ShedMaster.aspx.vb" Inherits="masterforms_ShedMaster" EnableEventValidation="false"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Shed inforamtion Master</h3>
          <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider"/>
                <form >
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
                                 <asp:DropDownList ID="locationname" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Shed Code</label>
                               <asp:TextBox ID="shedcode" runat="server" class="form-control" placeholder="Entry Shed Code" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedname">Shed Name</label>
                                 <asp:TextBox ID="shedname" runat="server" class="form-control" placeholder="Entry Shed Name" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcapacity">Shed Capacity</label>
                                 <asp:TextBox ID="shedcapacity" runat="server" class="form-control" placeholder="Entry Shed Capacity" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedtype">Shed Type</label>
                                 <asp:TextBox ID="shedtype" runat="server" class="form-control" placeholder="Entry Shed Type" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="noofpen">No. of PEN/Line</label>
                               <asp:TextBox ID="noofpen" runat="server" class="form-control" placeholder="Entry Shed Pen/Line" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="exampleInputPassword1"></label>
                                <asp:CheckBox ID="ISSTORE" runat="server" Text="Is Store" />
                            </div>
                        </div>
                    </div>


                    <br>
                    <input type="date" id="sday" name="sday">
                      <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                   <br/> 
                     <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300" ></asp:Label>
            
                </form>

            </div>
        </div>
          <div class="row">
            <div class="col-sm" id="divshed_grid">
                <asp:GridView ID="shed_grid" runat="server"  class="table table-striped table-hover"/>
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
