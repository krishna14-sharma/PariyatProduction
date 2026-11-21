<%@ Page Language="VB"  MasterPageFile="../AreaPage.master" AutoEventWireup="true" CodeFile="Daily_feedegg.aspx.vb" Inherits="dailyentryforms_Daily_feedegg" EnableEventValidation="false"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Feed/Egg Information From Farm</h3>
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
                                 <asp:TextBox ID="lblmqty" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                    </div>
                <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="heggs">Total Eggs</label>
                                <asp:TextBox ID="totaleggs" runat="server" class="form-control" placeholder="Total Eggs" ReadOnly="True"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Commercial Eggs(A)(Big Size)</label>
                                <asp:TextBox ID="commeggs" runat="server" class="form-control" placeholder="Commercial Eggs" ReadOnly="True"  />
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Commercial Eggs(B)(Small Size)</label>
                                <asp:TextBox ID="commeggsmall" runat="server" class="form-control" placeholder="Commercial Eggs" ReadOnly="True"  />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Crack Eggs</label>
                                <asp:TextBox ID="crackeggs" runat="server" class="form-control" placeholder="Crack Eggs" ReadOnly="True"  />
                            </div>
                        </div>                   
                  </div>
                    <div class="row">  
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Leakage Eggs(A)(Leaker)</label>
                                <asp:TextBox ID="leakereggs" runat="server" class="form-control" placeholder=" Leakage Eggs" ReadOnly="True"  />
                            </div>
                        </div>                                   
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Leakage Eggs(B)(Shell Less)</label>
                                <asp:TextBox ID="lekageeggs" runat="server" class="form-control" placeholder=" Leakage Eggs" ReadOnly="True"  />
                            </div>
                        </div>
                           
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Difference in Prod.</label>
                                <asp:TextBox ID="eggprevday" runat="server" class="form-control" placeholder="" ReadOnly="True"  />
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

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypef">Feed Type(Female)</label>
                                <asp:DropDownList ID="ftypef" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Feed Quantity(Female Per Bird) </label>
                                <asp:TextBox ID="fqtyfperbird" runat="server" class="form-control" placeholder="Female Feed Qty "  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Feed Quantity(Female)</label>
                                <asp:TextBox ID="fqtyf" runat="server" class="form-control" placeholder="Female Feed Qty "  />
                            </div>
                        </div>
                 
                    </div>
                    <div class="row">                  
                       <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypem">Feed Type(Male)</label>
                                <asp:DropDownList ID="ftypem" runat="server" class="form-control" />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Feed Quantity(Male Per Bird) </label>
                                <asp:TextBox ID="fqtymperbird" runat="server" class="form-control" placeholder="Female Feed Qty "  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtym">Feed Quantity(Male)</label>
                                <asp:TextBox ID="fqtym" runat="server" class="form-control" placeholder="Male Feed Qty "  />
                            </div>
                        </div>
                        
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="dbwtf">Body Weight(Female)</label>
                                <asp:TextBox ID="bodywtf" runat="server" class="form-control" placeholder="Female Body Weight"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="bodywtm">Body Weight(Male)</label>
                                <asp:TextBox ID="bodywtm" runat="server" class="form-control" placeholder="Male Body Weight"  />
                            </div>
                        </div>
                   
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="eggwt">Egg Weight</label>
                                <asp:TextBox ID="eggwt" runat="server" class="form-control" placeholder="Egg Weight"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
               <div class ="row">
                     <div class="col-sm-12">
                       <label for="eggwt">Remarks</label>
                         <asp:TextBox ID="txtremarks" runat="server" class="form-control" placeholder="Remarks"  />
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
								<asp:BoundColumn DataField="shed_no" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Feed_type_female" HeaderText="<B>Feed Type (F)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="per_bird_feed_f" HeaderText="<B>Feed/Bird(F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="feed_qty_female" HeaderText="<B>Feed (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_type_male" HeaderText="<B>Feed Type (M)</B>"></asp:BoundColumn>
                                 <asp:BoundColumn DataField="per_bird_feed_m" HeaderText="<B>Feed/Bird(M)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_qty_male" HeaderText="<B>Feed (M)</B>"></asp:BoundColumn>
								
								<asp:BoundColumn DataField="hatch_eggs" HeaderText="<B>HE</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="comm_eggs" HeaderText="<B>Comm</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="crack_eggs" HeaderText="<B>Crack</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="leakage_eggs" HeaderText="<B>Leak</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="total_eggs" HeaderText="<B>Total</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_female" HeaderText="<B>Body Wt(F)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_male" HeaderText="<B>Body Wt(M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="egg_wt" HeaderText="<B>Egg Wt</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="id" HeaderText="<B>ID</B>"></asp:BoundColumn>
                                <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
							</Columns>
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

        #SiteMaster_chikc_purchase_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">


       // $(document).ready(function () {

           

          //  document.getElementById('AreaMaster_entrydate').value = new Date().toISOString().substring(0, 10);
            function sum() {
           
                var txtFirstNumberValue = parseInt(document.getElementById('AreaMaster_totaleggs').value);
                var txtSecondNumberValue = parseInt(document.getElementById('AreaMaster_commeggs').value);
                var txtLeaker = parseInt(document.getElementById('AreaMaster_lekageeggs').value);
                var txtCrack = parseInt(document.getElementById('AreaMaster_crackeggs').value);
                var female_birds = parseInt(document.getElementById('AreaMaster_lblfqty').value);
                // alert(female_birds)
                var result = parseInt(txtFirstNumberValue) - (parseInt(txtSecondNumberValue) + parseInt(txtLeaker) + parseInt(txtCrack));
                if (!isNaN(result)) {
                    document.getElementById('AreaMaster_heggs').value = result;

                    document.getElementById('AreaMaster_hegg_per').value = ((result / parseInt(txtFirstNumberValue)) * 100).toFixed(2);
                    // 'prodeggper'
                    document.getElementById('AreaMaster_prodeggper').value = ((parseInt(txtFirstNumberValue) / parseInt(female_birds)) * 100).toFixed(2);
                }
            }

            function calctotfeed(){
                var fperbird = parseInt(document.getElementById('AreaMaster_fqtyfperbird').value);
                var female_birds = parseInt(document.getElementById('AreaMaster_lblfqty').value);
                document.getElementById('AreaMaster_fqtyf').value = (parseInt(fperbird) * parseInt(female_birds)).toFixed(3);
            
            }

            function calctotfeedm() {
                var fperbird1 = parseInt(document.getElementById('AreaMaster_fqtymperbird').value);
                var female_birds1 = parseInt(document.getElementById('AreaMaster_lblmqty').value);
               // alert(fperbird1.toString())
                document.getElementById('AreaMaster_fqtym').value = (parseInt(fperbird1) * parseInt(female_birds1)).toFixed(3);

            }
       // })//document ready function      
    </script>
</asp:Content>
