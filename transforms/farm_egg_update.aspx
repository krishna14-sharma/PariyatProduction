<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="../SiteMaster.master" CodeFile="farm_egg_update.aspx.vb" Inherits="transforms_farm_egg_update" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Egg Updation</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <%--<asp:Label ID="lblString" runat="server" />
                <hr class="divider" />--%>
                <form>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="entrydate">Entry Date</label>
                                <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control">                              
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
                                <label for="shedname">Shed Name/Number</label>
                                <asp:DropDownList ID="shedname" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                          <div class="col-sm-3">
                            <div class="form-group">
                                <label for="birdtype">Please Select Line/Pen</label>
                                <asp:DropDownList ID="lineno" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                       
                    </div>
                    <div class="row"> 
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Bird Age</label>
                                <asp:Label ID="lblbirdbreed" runat="server" ForeColor="Red" class="form-control"/>
                            </div>
                        </div>       
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Flock Number</label>
                                <asp:Label ID="lblflockno" runat="server" ForeColor="Red" class="form-control"/>
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Female Quantity</label>
                                 <asp:TextBox ID="lblfqty" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                            <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Male Quantity</label>
                                 <asp:Label ID="lblmqty" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="row">  
                        <div class="col-sm-3">
                             <div class="form-group">
                                <label for="heggs">Collection Number</label>
                                <asp:TextBox ID="collection_no" runat="server" class="form-control" placeholder="-"  />
                            </div>
                        </div>
                         <div class="col-sm-3"></div>
                         <div class="col-sm-3"></div>
                         <div class="col-sm-3"></div>
                    </div>

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="heggs">Total Eggs</label>
                                <asp:TextBox ID="totaleggs" runat="server" class="form-control" placeholder="Total Eggs"  />
                            &nbsp;Hatching Eggs<asp:TextBox ID="heggs" runat="server" class="form-control" placeholder="Commercial Eggs"  />
                            </div>
                        </div>
         
                             
                  </div>
         
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                            </div>
                        </div>
                         <div class="col-sm-3">
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>

       
                    <asp:Button ID="loc_btnSave" Text="Updtae" runat="server" class="btn btn-danger" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>  
  
    </div>
</asp:Content>