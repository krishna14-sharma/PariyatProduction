<%@ Page Language="VB" MasterPageFile="~/AreaPage.master" AutoEventWireup="true" CodeFile="BirdTransfer.aspx.vb" Inherits="transforms_BirdTransfer" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Bird Transfer Entry Shed Wise</h3>
        <div class="row">
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="entrydate">Entry Date</label>
                        <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control">
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="shedloc">From Shed Location</label>
                        <asp:DropDownList ID="shedloc" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="shedname">From Shed Name/Number</label>
                        <asp:DropDownList ID="shedname" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="birdtype">From Line/Pen</label>
                        <asp:DropDownList ID="lineno" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="row">
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Bird Age</label>
                        <asp:Label ID="lblbirdbreed" runat="server" ForeColor="Red" class="form-control" />
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Flock Number</label>
                        <asp:Label ID="lblflockno" runat="server" ForeColor="Red" class="form-control" />
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
                        <label for="shedloc">To Shed Location</label>
                        <asp:DropDownList ID="toshedloc" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="shedname">To Shed Name/Number</label>
                        <asp:DropDownList ID="toshedno" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="birdtype">To Line/Pen</label>
                        <asp:DropDownList ID="toline" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Flock Number To</label>
                        <asp:Label ID="lblFlockNoTo" runat="server" ForeColor="Red" class="form-control" />
                    </div>
                </div>
            </div>
            <div class="row">
                
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Female Quantity</label>
                        <asp:TextBox ID="ftransferqty" runat="server" ForeColor="Red" class="form-control" />
                    </div>
                </div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Male Quantity</label>
                        <asp:TextBox ID="mtransferqty" runat="server" ForeColor="Red" class="form-control" />
                    </div>
                </div>

                 <div class="col-sm-3">
                    <div class="form-group">
                        <label for="">Bird Breed</label>
                        <asp:Label ID="lblbreed1" runat="server" ForeColor="Red" class="form-control" />
                    </div>
                </div>

            </div>
        </div>
      <asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
      <br />
      <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>

        <div class="row">
            <div class="col-sm" id="prdform_grd">
          

                <asp:datagrid id="transfer_grid" runat="server" PageSize="5" AutoGenerateColumns="False" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
							<AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
							<Columns>
								<asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="from_shed_loc" HeaderText="<B>From Shed </B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="from_shed_no" HeaderText="<B>From Shed No.</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="from_line_no" HeaderText="<B>Pen/Line</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="to_shed_loc" HeaderText="<B>To Location</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="to_shed_no" HeaderText="<B>To Shed</B>"></asp:BoundColumn>
								<asp:BoundColumn DataField="to_line_no" HeaderText="<B>To Line/Pen</B>"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="fqty" HeaderText="<B>F Qty</B>"></asp:BoundColumn>
                                <asp:BoundColumn DataField="mqty" HeaderText="<B>M Qty</B>"></asp:BoundColumn>
									
							</Columns>
						</asp:datagrid>
            </div>
        </div>


     </div>
    <style type="text/css">
        #prdform1,#prdform2,#prdform3 {
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
</asp:Content>


