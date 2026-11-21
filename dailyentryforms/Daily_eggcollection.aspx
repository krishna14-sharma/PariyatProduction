<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="../AreaPage.master" CodeFile="Daily_eggcollection.aspx.vb" Inherits="dailyentryforms_Daily_eggcollection" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Egg Collection</h3>
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
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Commercial Eggs(A)(Big Size)</label>
                                <asp:TextBox ID="commeggs" runat="server" class="form-control" placeholder="Commercial Eggs"  />
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Commercial Eggs(B)(Small Size)</label>
                                <asp:TextBox ID="commeggsmall" runat="server" class="form-control" placeholder="Commercial Eggs"  />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Crack Eggs</label>
                                <asp:TextBox ID="crackeggs" runat="server" class="form-control" placeholder="Crack Eggs"  />
                            </div>
                        </div>                   
                  </div>
                    <div class="row">    
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Leakage Eggs(A)(Leaker)</label>
                                <asp:TextBox ID="leakereggs" runat="server" class="form-control" placeholder=" Leakage Eggs"  />
                            </div>
                        </div>                                 
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Leakage Eggs(B)(Shell Less)</label>
                                <asp:TextBox ID="lekageeggs" runat="server" class="form-control" placeholder=" Leakage Eggs"  />
                            </div>
                        </div>
                       
                     </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Hatching Eggs</label>
                                <asp:TextBox ID="heggs" runat="server" class="form-control" placeholder="Hatching Eggs" ReadOnly="True"  />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Hatching Eggs(%)</label>
                                <asp:TextBox ID="hegg_per" runat="server" class="form-control" placeholder="Hatching Eggs(%)" ReadOnly="True"  />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Prod Eggs(%)</label>
                                <asp:TextBox ID="prodeggper" runat="server" class="form-control" placeholder="" ReadOnly="True"  />
                            </div>
                        </div>
                    </div>
      
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">       
                  <asp:datagrid id="dailyentry_grid" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
							<AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_loc" HeaderText="<B>Shed Location</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_no" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="line_no" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Collection_no" HeaderText="<B>Collection No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="hatch_eggs" HeaderText="<B>HE</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="comm_eggs" HeaderText="<B>Comm(Big)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="comm_eggs_small" HeaderText="<B>Comm(Small)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="crack_eggs" HeaderText="<B>Crack</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="leakage_eggs" HeaderText="<B>Leak(Shell Less)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="leakage_eggs_leaker" HeaderText="<B>Leak</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="total_eggs" HeaderText="<B>Total Eggs</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="id" HeaderText="<B>ID</B>"></asp:BoundColumn>
                                <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
								
							</Columns>
						</asp:datagrid>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd1"> 
                <asp:datagrid id="dgsum" runat="server" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
               </asp:datagrid>
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

        #AreaMaster_chikc_purchase_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        //document.getElementById('AreaMaster_entrydate').value = new Date().toISOString().substring(0, 10);

       // $(document).ready(function () {
          
            function sum() {

                var txtFirstNumberValue = parseInt(document.getElementById('AreaMaster_totaleggs').value);
                var txtSecondNumberValue = parseInt(document.getElementById('AreaMaster_commeggs').value);
                var txtLeaker = parseInt(document.getElementById('AreaMaster_lekageeggs').value);
                var txtCrack = parseInt(document.getElementById('AreaMaster_crackeggs').value);
                var female_birds = parseInt(document.getElementById('AreaMaster_lblfqty').value);
                var commsmall = parseInt(document.getElementById('AreaMaster_commeggsmall').value);
                var leaker = parseInt(document.getElementById('AreaMaster_leakereggs').value);
                // alert(female_birds)
                var result = parseInt(txtFirstNumberValue) - (parseInt(txtSecondNumberValue) + parseInt(txtLeaker) + parseInt(txtCrack) + parseInt(commsmall) + parseInt(leaker));
                if (!isNaN(result)) {
                    document.getElementById('AreaMaster_heggs').value = result;

                    document.getElementById('AreaMaster_hegg_per').value = ((result / parseInt(txtFirstNumberValue)) * 100).toFixed(2);
                    // 'prodeggper'
                    document.getElementById('AreaMaster_prodeggper').value = ((parseInt(txtFirstNumberValue) / parseInt(female_birds)) * 100).toFixed(2);
                }
            }
               
      //  }
     

    </script>
</asp:Content>