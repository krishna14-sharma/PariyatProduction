<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../AreaPage.master" CodeFile="Daily_bird_sepration_entry.aspx.vb" Inherits="dailyentryforms_Daily_bird_sepration_entry" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Fram Sepration/Chattan</h3>
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
                                <label for="">Reason Female</label>
                                <asp:DropDownList ID="reasonfem" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Female Quantity</label>
                                <asp:TextBox ID="fqty" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label for="">Reason Male</label>
                                <asp:DropDownList ID="reasonmale" runat="server" ForeColor="Red" class="form-control" />
                            </div>
                        </div>
                        <div class="col-sm-3">

                            <div class="form-group">
                                <label for="">Male Quantity</label>
                                <asp:TextBox ID="mqty" runat="server" ForeColor="Red" class="form-control" />
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-3">

                            <div class="form-group">
                                <label for="">Reason</label>
                                <asp:TextBox ID="reason" runat="server" ForeColor="Red" class="form-control" />
                            </div>

                        </div>
                    </div>


                    &nbsp;<asp:Button ID="loc_btnSave" Text="Save" runat="server" class="btn btn-primary" />
                    <asp:Button ID="Button1" Text="Show" runat="server" class="btn btn-secondary" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd1">
                <asp:DataGrid ID="dgchattan" runat="server" GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
                    <Columns>
                         <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                    </Columns>
                </asp:DataGrid>
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
