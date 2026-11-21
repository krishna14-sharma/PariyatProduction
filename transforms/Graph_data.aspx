<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="../SiteMaster.master" CodeFile="Graph_data.aspx.vb" Inherits="transforms_Graph_data" enableEventValidation="false"  %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Production Data Weekly</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
                <form>
                    <div class="row">
                         <div class="col-sm-6">
                            <div class="form-group">
                                <label for="locationname">Flock Number:</label>                            
                                 <asp:TextBox ID="flockno" runat="server" class="form-control" placeholder="Flock Number" />
                            </div>
                        </div>

                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="locationname">Age(Weeks):</label>                            
                                 <asp:TextBox ID="age" runat="server" class="form-control" placeholder="Age" />
                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Female Stock</label>
                                <asp:TextBox ID="fqty" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="hatchdate">Weekly Egg Stock</label>
                               <asp:TextBox ID="egstock" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                    </div>
          
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Weekly Hatching Egg Stock</label>
                                <asp:TextBox ID="heggstock" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="hatchdate">Weekly Mortality</label>
                               <asp:TextBox ID="mortality" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                    </div>

                      <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Weekly Crack Egg Stock</label>
                                <asp:TextBox ID="crackegg" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="hatchdate">Weekly Leaker Egg</label>
                               <asp:TextBox ID="leaker" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                    </div>

                     <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Weekly Commercial Egg </label>
                                <asp:TextBox ID="commercial" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="hatchdate"></label>
                               
                            </div>
                        </div>
                    </div>


                     <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Total Loading</label>
                                <asp:TextBox ID="txttotal_loading" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Total Fertile</label>
                                <asp:TextBox ID="txttotal_fertile" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                    </div>

                     <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Total InFertile</label>
                                <asp:TextBox ID="txttotal_infertile" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="shedcode">Total Chicks</label>
                                <asp:TextBox ID="txttotalchicks" runat="server" class="form-control" placeholder="" />
                            </div>
                        </div>
                    </div>
                   
                    </div>
      
                    <br>
                    <asp:Button ID="flockmaster_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <br>
                    <asp:Button ID="Button1" Text="Show Data" runat="server" class="btn btn-primary" />
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