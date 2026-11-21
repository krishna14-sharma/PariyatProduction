<%@ Page Language="VB"  MasterPageFile="../AreaPage.master" AutoEventWireup="true" CodeFile="DailyWeather.aspx.vb" Inherits="dailyentryforms_DailyWeather" EnableEventValidation="false"    %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Farm Weather Information</h3>
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
                   <%-- Shed Temprature Inside --%>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="heggs">Shed Temprature Inside</label>
                                <asp:TextBox ID="tempinside" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Shed Temprature Outside(Min)</label>
                                <asp:TextBox ID="tempoutsidemin" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Shed Temprature Outside(Max)</label>
                                <asp:TextBox ID="tempoutsidemax" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="crackeggs">Shed Temprature Outside(Avg)</label>
                                <asp:TextBox ID="tempoutsideavg" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                    </div>
                    

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypef">Humidity Inside</label>
                                <asp:DropDownList ID="humidityinside" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Humidity Outside</label>
                                <asp:TextBox ID="humidityoutside" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                                             

                    </div>
                    <div class="row">                  
                       <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypem">Brooder Temp</label>
                                <asp:DropDownList ID="Broodertemp" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtym">Brooder Temp(Under)</label>
                                <asp:TextBox ID="BroodertempUnder" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Brooder Temp(Close) </label>
                                <asp:TextBox ID="Broodertempclose" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="dbwtf">Near Cooling Pad </label>
                                <asp:TextBox ID="coolingpadnear" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="bodywtm">Centre Of Shed</label>
                                <asp:TextBox ID="centreofshed" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                   
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="eggwt">Near Exhaust Fan(Temp)</label>
                                <asp:TextBox ID="nearexhaustfan" runat="server" class="form-control" placeholder=" "  />
                            </div>
                        </div>
                        <div class="col-sm-3">
                        </div>
                    </div>
               <div class ="row">
                     <div class="col-sm-12">
                       <label for="eggwt">Feed Clean Up Time</label>
                         <asp:TextBox ID="feedcleanuptime" runat="server" class="form-control" placeholder=""  />
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
        document.getElementById('SiteMaster_entrydate').value = new Date().toISOString().substring(0, 10);
        function sum() {

            var txtFirstNumberValue = parseInt(document.getElementById('SiteMaster_totaleggs').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMaster_commeggs').value);
            var txtLeaker = parseInt(document.getElementById('SiteMaster_lekageeggs').value);
            var txtCrack = parseInt(document.getElementById('SiteMaster_crackeggs').value);
            var female_birds = parseInt(document.getElementById('SiteMaster_lblfqty').value);
            // alert(female_birds)
            var result = parseInt(txtFirstNumberValue) - (parseInt(txtSecondNumberValue) + parseInt(txtLeaker) + parseInt(txtCrack));
            if (!isNaN(result)) {
                document.getElementById('SiteMaster_heggs').value = result;

                document.getElementById('SiteMaster_hegg_per').value = ((result / parseInt(txtFirstNumberValue)) * 100).toFixed(2);
                // 'prodeggper'
                document.getElementById('SiteMaster_prodeggper').value = ((parseInt(txtFirstNumberValue) / parseInt(female_birds)) * 100).toFixed(2);
            }
        }

    </script>
    </asp:Content>

