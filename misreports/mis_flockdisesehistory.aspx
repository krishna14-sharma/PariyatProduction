<%@ Page Title="" Language="VB" MasterPageFile="~/ReportPage.master" AutoEventWireup="false" CodeFile="mis_flockdisesehistory.aspx.vb" Inherits="misreports_mis_flockdisesehistory" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">
    <div class="container">
        <h3>FLOCK MEDICATION / VACCINATION/DISEASE HISTORY</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <%--<asp:Label ID="lblString" runat="server" />
                <hr class="divider" />--%>
                <form>
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="entrydate">Entry Date</label>
                                <input type="date" id="entrydate" name="entrydate" runat="server"  onchange="bindGridFromDate()" class="form-control">
                                <asp:Button ID="btnBindGrid" runat="server" OnClick="btnBindGrid_Click" Style="display:none;" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="shedloc">Shed Location</label>
                                <asp:DropDownList ID="shedloc" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
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
                                <label for="Symptoms">Symptoms</label>
                                <asp:DropDownList ID="Symptoms" runat="server" class="form-control" />
                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="Symptoms">PM Leasing</label>
                                <asp:DropDownList ID="pmleasing" runat="server" class="form-control" />
                            </div>
                        </div>

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="heggs">Disease Diagnose</label>
                                <asp:DropDownList ID="diseaselist" runat="server" class="form-control" />
                            </div>
                        </div>
                                      
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Medication</label>
                                <asp:TextBox ID="medication" runat="server" class="form-control" placeholder="Medication" />
                            </div>
                        </div>

                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Breed</label>
                                <asp:Label ID="lblbreed" runat="server" class="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                              <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">No of Infected Birds(FEMALE)</label>
                                <asp:TextBox ID="noofinfectedbirds" runat="server" class="form-control" placeholder="No of Infected Birds" />
                            </div>
                        </div>
                               <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">No of Infected Birds(MALE)</label>
                                <asp:TextBox ID="noofinfectedbirdsmale" runat="server" class="form-control" placeholder="No of Infected Birds" />
                            </div>
                        </div>
                     </div>
                    <div class="row">

                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="heggs">Name of Vaccination Schedule</label>
                                <asp:DropDownList ID="Vaccination" runat="server" class="form-control" />

                            </div>
                        </div>
                         <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Due On</label>
                                <input type="date" id="txtdueon" name="txtdueon" runat="server" class="form-control">
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="commeggs">Given On</label>
                                <input type="date" id="txtgivenon" name="txtgivenon" runat="server" class="form-control">
                            </div>
                        </div>
                       
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <label for="eggwt">Remarks</label>
                            <asp:TextBox ID="txtremarks" runat="server" class="form-control" placeholder="Remarks" />
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
            <div class="col-12" id="prdform_grd">
                <asp:GridView ID="gvMedical" runat="server"
    AutoGenerateColumns="False"
    DataKeyNames="id"
    OnRowEditing="gvMedical_RowEditing"
    OnRowCancelingEdit="gvMedical_RowCancelingEdit"
    OnRowUpdating="gvMedical_RowUpdating"
    OnRowDeleting="gvMedical_RowDeleting"
    CssClass="table">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" />
        <asp:BoundField DataField="entry_date" HeaderText="Entry Date" />
        <asp:BoundField DataField="shed_loc" HeaderText="Location" />
        <asp:BoundField DataField="shed_name" HeaderText="Shed" />
        <asp:BoundField DataField="line_pen" HeaderText="Line" />
        <asp:BoundField DataField="age" HeaderText="Age" />
        <asp:BoundField DataField="flock_no" HeaderText="Flock No" />
        <asp:BoundField DataField="disease_name" HeaderText="Disease" />
         <asp:BoundField DataField="pmLesions" HeaderText="pmLesions" />
        <asp:BoundField DataField="stockF" HeaderText="Stock F" />
        <asp:BoundField DataField="stockM" HeaderText="Stock M" />
        <asp:BoundField DataField="symptoms" HeaderText="Symptoms" />
        <asp:BoundField DataField="medecine" HeaderText="Medicine" />
        <asp:BoundField DataField="vaccination" HeaderText="Vaccination" />
        <asp:BoundField DataField="givenon" HeaderText="Given On" />
        <asp:BoundField DataField="dueon" HeaderText="Due On" />
        <asp:BoundField DataField="noofbirds" HeaderText="Birds" />
        <asp:BoundField DataField="remarks" HeaderText="Remarks" />
        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />

    </Columns>
</asp:GridView>
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
        function bindGridFromDate() {
            document.getElementById('<%= btnBindGrid.ClientID %>').click();
       }

    </script>
</asp:Content>

