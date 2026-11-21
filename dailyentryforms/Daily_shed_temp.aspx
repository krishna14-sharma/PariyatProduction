<%@ Page Language="VB" MasterPageFile="../AreaPage.master" AutoEventWireup="true" CodeFile="Daily_shed_temp.aspx.vb" Inherits="dailyentryforms_Daily_shed_temp" EnableEventValidation="false"   %>

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
                                <asp:DropDownList ID="shedloc" runat="server" class="form-control"  AutoPostBack="false"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="shedname">Time</label>
                                <asp:DropDownList ID="time6" runat="server" class="form-control">
                                    <asp:ListItem>8</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>2</asp:ListItem>
                                    <asp:ListItem>4</asp:ListItem>
                                    <asp:ListItem>6</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                          <div class="col-sm-3">
                            <div class="form-group">
                                <label for="birdtype">Day/Night</label>
                                <asp:DropDownList ID="day_night" runat="server" class="form-control">
                                    <asp:ListItem>Day</asp:ListItem>
                                    <asp:ListItem>Night</asp:ListItem>
                                </asp:DropDownList>
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
                                <label for="commeggs">Shed Temprature Outside</label>
                                <asp:TextBox ID="tempoutside" runat="server" class="form-control" placeholder=""  />
                            <asp:TextBox ID="tempoutsideavg" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>
                    </div>
                    

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="ftypef">Humidity Insid  <label/>
                                <asp:TextBox ID="humidityinside" runat="server" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="fqtyf">Humidity Outside</label>
                                <asp:TextBox ID="humidityoutside" runat="server" class="form-control" placeholder=""  />
                            </div>
                        </div>                                   
               </div>           
   
               <div class ="row">
                     <div class="col-sm-12">
                       <label for="eggwt">Feed Clean Up Time</label>
                         <asp:TextBox ID="feedcleanuptime" runat="server" class="form-control" placeholder=""  />
                        </div>
               </div>
                    
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <asp:Button ID="btnShow" Text="Show" runat="server" class="btn btn-info" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
                <%--[entry_date], [entry_time], [shed_in], [shed_out], [location], [id],--%> 
                <%--[humidiy_inside], [humudity_outside], [day_night], [feed_cleanup_time]--%>
                <div class ="row">
                     <div class="col-sm-12">
                         <asp:DataGrid runat="server" ID ="dgtemerpatue"  PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover">
                             <AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
                             <Columns>
                                <asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="day_night" HeaderText="<B>Day/Night</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="entry_time" HeaderText="<B>Time</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="shed_in" HeaderText="<B>Temp. In</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="shed_out" HeaderText="<B>Temp. Out</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="humidiy_inside" HeaderText="<B>Humidiy In</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="humudity_outside" HeaderText="<B>Humidiy Out</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="id" HeaderText="<B>ID</B>"></asp:BoundColumn>
                                <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                             </Columns>
                          </asp:DataGrid>  
                         </div>
                    </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">
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
    </label>
    </label>
    </asp:Content>