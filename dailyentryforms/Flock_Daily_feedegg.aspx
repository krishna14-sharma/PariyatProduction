<%@ Page Language="VB"  MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Flock_Daily_feedegg.aspx.vb" Inherits="dailyentryforms_Daily_feedegg"  EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server"  >
    <div class="container" id ="cont">
        <h3>&nbsp;</h3>
        <h3>&nbsp;</h3>
        <h3>Flock Wise Feed/Egg Information From Farm</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:Label ID="lblString" runat="server" />
                <hr class="divider" />
                <form id ="flockentry">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="entrydate">Entry Date</label>
                                <asp:TextBox ID="entrydate"  TextMode="Date" runat ="server"  class="form-control"  />                        
                            </div>
                        </div>
                      <div class="col-sm-3">
                            <div class="form-group">
                                <label for="shedloc">Select Flock</label>
                                <asp:DropDownList ID="flockno" runat="server" class="form-control"  AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            
                        </div>
                          <div class="col-sm-3">
                           
                        </div>
                       
                    </div>
                    <div class="row"> 
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Bird Breed</label>
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
                                 <asp:Label ID="lblfqty" runat="server" ForeColor="Red" class="form-control" />
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
                                <label for="heggs">Hatch Eggs</label>
                                <asp:TextBox ID="heggs" runat="server" class="form-control" placeholder="Hatch Eggs"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Commercial Eggs</label>
                                <asp:TextBox ID="commeggs" runat="server" class="form-control" placeholder="Commercial Eggs"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Crack Eggs</label>
                                <asp:TextBox ID="crackeggs" runat="server" class="form-control" placeholder="Crack Eggs"  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Leakage Eggs</label>
                                <asp:TextBox ID="lekageeggs" runat="server" class="form-control" placeholder=" Leakage Eggs"  />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="crackeggs">Total Eggs</label>
                                <asp:TextBox ID="totaleggs" runat="server" class="form-control" placeholder="Total Eggs" ReadOnly="True"  />
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
                                <label for="fqtyf">Feed Quantity(Female)</label>
                                <asp:TextBox ID="fqtyf" runat="server" class="form-control" placeholder="Female Feed Qty "  />
                            </div>
                        </div>
                       <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypem">Feed Type(Male)</label>
                                <asp:DropDownList ID="ftypem" runat="server" class="form-control" />
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
								<asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="feed_qty_female" HeaderText="<B>Feed (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_type_female" HeaderText="<B>Feed Type (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_qty_male" HeaderText="<B>Feed (M)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="Feed_type_male" HeaderText="<B>Feed Type (M)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="hatch_eggs" HeaderText="<B>HE</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="comm_eggs" HeaderText="<B>Comm</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="crack_eggs" HeaderText="<B>Crack</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="leakage_eggs" HeaderText="<B>Leak</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_female" HeaderText="<B>Body Wt(F)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="body_wt_male" HeaderText="<B>Body Wt(M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="egg_wt" HeaderText="<B>Egg Wt</B>"></asp:BoundColumn>
								
							</Columns>
						</asp:datagrid>
            </div>
        </div>
    </div>

     <style type="text/css">
          #BodyContent,#cont{
             background-color:#ecfacf;
         }
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
       // document.getElementById('SiteMaster_entrydate').value = new Date().toISOString().substring(0, 10);
        function sum() {
           
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMaster_heggs').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMaster_commeggs').value);
            var txtLeaker = parseInt(document.getElementById('SiteMaster_lekageeggs').value);
            var txtCrack = parseInt(document.getElementById('SiteMaster_crackeggs').value);
           
            var result = parseInt(txtFirstNumberValue) + parseInt(txtSecondNumberValue) + parseInt(txtLeaker) + parseInt(txtCrack);
            if (!isNaN(result)) {
                document.getElementById('SiteMaster_totaleggs').value = result;
            }
        }
      
    </script>
</asp:Content>
