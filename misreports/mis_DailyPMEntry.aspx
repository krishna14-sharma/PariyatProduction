<%@ Page Title="" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="mis_DailyPMEntry.aspx.vb" Inherits="misreports_mis_DailyPMEntry" EnableEventValidation="false"  %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>Daily PM Entry</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <%--<asp:Label ID="lblString" runat="server" />
                <hr class="divider" />--%>
                <form>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="entrydate">Morning/Evening</label>
                                <asp:DropDownList id="ddlME" name="entrydate" runat="server" class="form-control" >
                                    <asp:ListItem>Morning</asp:ListItem>
                                    <asp:ListItem>Evening</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        </div>
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
                                <label for="heggs">Disease Names</label>
                               <asp:DropDownList ID="diseaselist" runat="server" class="form-control"   />
                                
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Female Mortality Qty</label>
                                <asp:TextBox ID="morqtyf" runat="server" class="form-control" placeholder="Mortality Qty"  />
                            </div>
                        </div>    
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Male Mortality Qty</label>
                                <asp:TextBox ID="morqtym" runat="server" class="form-control" placeholder="Mortality Qty"  />
                            </div>
                        </div>               
                </div>                        
            
              <div class ="row">
                     <div class="col-sm-12">
                       <label for="eggwt">Remarks</label>
                         <asp:TextBox ID="txtremarks" runat="server" class="form-control" placeholder="Remarks"  />
                        </div>
               </div>
                    
                    <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                     <asp:Button ID="Button1" Text="Show" runat="server" class="btn btn-primary" />
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
                                <asp:BoundColumn DataField="pmtime" HeaderText="<B>Morn/Eve</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_loc" HeaderText="<B>Shed Location</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="shed_name" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="line_pen" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="disease_name" HeaderText="<B>Disease Name</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pm_qtyf" HeaderText="<B>Qty (F)</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="pm_qtym" HeaderText="<B>Qty (M)</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="id" HeaderText="<B>ID</B>"></asp:BoundColumn>
								<asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
							</Columns>
						</asp:datagrid>
                <asp:DataGrid ID="showsum" runat="server" />
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