<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="true" CodeFile="Standards.aspx.vb" Inherits="masterforms_Standards" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Standard Entry</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider">
                <form>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="season">Season</label>
                                <asp:DropDownList ID="seasonlist" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="birdtype">Bird Type</label>
                                <asp:DropDownList ID="birdtype" runat="server" class="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="orgname">Age Weeks</label>
                                <asp:TextBox ID="ageweeks" runat="server" class="form-control" placeholder="Age Weeks" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="state">Cumulative Depletion % Female</label>
                                <asp:TextBox ID="cumdepperfemale" runat="server" class="form-control" placeholder="Cumulative Depletion % Female" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="country">Cumulative Depletion % Male</label>
                                <asp:TextBox ID="cumdeppermale" runat="server" class="form-control" placeholder="Cumulative Depletion % Male" />
                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="phone">Body Weight Female in Grams</label>
                                <asp:TextBox ID="bodywtfemalegrams" runat="server" class="form-control" placeholder="Body Weight Female in Grams" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="email">Body Weight Male in Grams</label>
                                <asp:TextBox ID="bodywtmalegrams" runat="server" class="form-control" placeholder="Body Weight Male in Grams" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="feedcomsp">Feed Consumption Female Per Day Grams</label>
                                <asp:TextBox ID="feedcompperday" runat="server" class="form-control" placeholder="Feed Consumption Female Per Day Grams" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="malefeedcompperday">Feed Consumption Male Per Day Grams</label>
                                <asp:TextBox ID="malefeedcompperday" runat="server" class="form-control" placeholder="Feed Consumption Male" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gst">Egg Weight in Grams</label>
                                <asp:TextBox ID="eggwt" runat="server" class="form-control" placeholder="Egg Weight in Grams" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gst">Mortality Per Female</label>
                                <asp:TextBox ID="morperfemale" runat="server" class="form-control" placeholder="Mortality Per Female" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="feedcomsp">HDP %</label>
                                <asp:TextBox ID="hdpper" runat="server" class="form-control" placeholder="HDP %" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gst">Egg Production Hen Housed Weekly</label>
                                <asp:TextBox ID="eggprdhenhouesdweekly" runat="server" class="form-control" placeholder="Egg Production Hen Housed Weekly" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="feedcomsp">Egg Production Hen Housed Cumulative</label>
                                <asp:TextBox ID="eggprdhenhouescumulative" runat="server" class="form-control" placeholder="Egg Production Hen Housed Cumulative" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="gst">Hatching Eggs %</label>
                                <asp:TextBox ID="hatdhingegg" runat="server" class="form-control" placeholder="Hatching Eggs %" />
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label for="feedcomsp">Hatching Eggs % Hen Hosed</label>
                                <asp:TextBox ID="hatdhingegghenhosed" runat="server" class="form-control" placeholder="Hatching Eggs % Hen Housed " />
                            </div>
                        </div>
                    </div>
                    <br />
                    <asp:Button runat="server" class="btn btn-primary" Text="Save" ID="btnsavestandard" />
                    <br />
                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                </form>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform_grd">
                <asp:GridView ID="standard_grid" runat="server" class="table table-striped table-hover" Font-Size="Small" />
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

        #SiteMaster_standard_grid {
            font-size: 1em;
            text-align: center;
            align-content: center;
            font-weight: bold;
        }
    </style>
</asp:Content>
