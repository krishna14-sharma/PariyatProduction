<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="ShedEntry.aspx.vb" Inherits="transforms_ShedEntry" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Birds Shed Entry</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
                <form>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="birdtype">Bird Breed</label>
                                <asp:DropDownList ID="birdtype" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="shedloc">Shed Location</label>
                                <asp:DropDownList ID="shedloc" runat="server" class="form-control"  AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="birdtype">Please Select Shed</label>
                                <asp:DropDownList ID="shedname" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="birdtype">Please Select Line/Pen</label>
                                <asp:DropDownList ID="lineno" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                  
                    </div>
                    <div class="row">
                              <div class="col-sm-6">
                            <div class="form-group">
                                <label for="locationname">Flock Number:</label>                            
                                <asp:DropDownList ID="flockno" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                       </div>    
                        
                         <div class="col-sm-6">
                                <div class="form-group">
                                    <label for="shedtype">Shed Entry Date</label>
                                    <input type="date" id="shedentrydate" name="shedentrydate" runat="server" class="form-control">
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
      
                               <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="fqty">Is Empty</label>
                                 <asp:CheckBox ID="isEnpty" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="mqty">Is Alive</label>
                                <asp:CheckBox ID="isaAlive" runat="server" class="form-control" />
                            </div>
                        </div>
                    </div>

                    <br>
                    <asp:Button ID="shedentrymaster_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>

           <div class="row">
                <div class="col-sm" id="divshed_grid">
                    <asp:GridView ID="shedentrymaster_grid" runat="server" class="table table-striped table-hover" />
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

        #SiteMaster_shedentrymaster_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
