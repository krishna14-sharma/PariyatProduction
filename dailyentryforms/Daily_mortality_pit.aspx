<%@ Page Language="VB" MasterPageFile="../AreaPage.master" AutoEventWireup="true" CodeFile="Daily_mortality_pit.aspx.vb" Inherits="dailyentryforms_Daily_mortality_pit" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <div class="container">
            <h3>Flock Daily Entry</h3>
            <div class="row">
                <div class="col-sm-12" id="prdform">
                    <asp:Label ID="lblString" runat="server" />
                    <hr class="divider" />
                    <form>
                        <table>
                            <tr>
                                <td>Entry Date</td>
                                <td>
                                    <input type="date" id="entrydate" name="entrydate" runat="server" class="form-control" />
                                </td>
                                <td>
                                    <asp:Button ID="mordata" Text="Show" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Time</td>
                                <td>
                                    <asp:CheckBox ID="IStime" runat="server" Text="Evening" AutoPostBack="true" />
                                </td>
                            </tr>
                            <tr align="left">
                                <td width="168" bgcolor="#ffffcc" style="height: 35px">Shed Location
                                        ation
                                </td>
                                <td width="150" colspan="2" style="width: 150px; height: 35px">
                                    <asp:DropDownList ID="shedlocation" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                        Height="32px" Width="250px" AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td bgcolor="#ffffcc" class="auto-style1">Flock</td>
                                <td width="98" style="width: 98px; height: 35px">&nbsp;
						<asp:Label ID="lbltypefem" runat="server"></asp:Label></td>
                                <td width="110" style="height: 35px">
                                    <asp:Label ID="lblflockfem" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                        Width="61px" Height="16px"></asp:Label></td>
                                <td style="height: 35px">&nbsp;
						<asp:Label ID="lbllotfem" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                            Width="58px" Height="16px"></asp:Label></td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#ffffcc" style="height: 35px">Shed Name/Number</td>
                                <td style="width: 109px; height: 35px">
                                    <asp:DropDownList ID="shedname" runat="server" Width="250px" Height="32px" AutoPostBack="True"></asp:DropDownList></td>
                                <td style="width: 4px; height: 35px">&nbsp;</td>
                                <td bgcolor="#ffffcc" class="auto-style1">&nbsp;</td>
                                <td style="width: 98px; height: 35px">&nbsp;
						<asp:Label ID="lbltypemale" runat="server"></asp:Label>
                                </td>
                                <td style="height: 35px">
                                    <asp:Label ID="lblflockmale" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                        Width="58px" Height="16px"></asp:Label></td>
                                <td style="height: 35px">&nbsp;
						<asp:Label ID="lbllotmale" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                            Width="61px" Height="16px"></asp:Label></td>
                            </tr>

                            <tr align="left">
                                <td bgcolor="#ffffcc" style="height: 35px">Pen/Line Number</td>
                                <td style="width: 109px; height: 35px">
                                    <asp:DropDownList ID="lineno" runat="server" Width="250px" Height="32px" AutoPostBack="True"></asp:DropDownList></td>
                                <td style="width: 4px; height: 35px">&nbsp;</td>
                                <td bgcolor="#ffffcc" class="auto-style1">Bird Type</td>
                                <td style="width: 98px; height: 35px">&nbsp;
						<asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td style="height: 35px">
                                    <asp:Label ID="Label2" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                        Width="58px" Height="16px"></asp:Label></td>
                                <td style="height: 35px">&nbsp;
						<asp:Label ID="Label3" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                            Width="61px" Height="16px"></asp:Label></td>
                            </tr>
                            <tr align="left">
                                <td colspan="7">
                                    <hr>
                                </td>
                            </tr>
                            <tr align="left">
                                <td>&nbsp;</td>
                                <td colspan="2" style="width: 88px" bgcolor="#ffffcc">
                                    <h4>Female</h4>
                                </td>
                                <td bgcolor="#ffffcc" class="auto-style2">
                                    <h4>Male</h4>
                                </td>
                                <td colspan="3" rowspan="8" style="width: 98px"></td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#ffffcc">
                                    <h4>Till Today</h4>
                                </td>
                                <td colspan="2" style="width: 88px">
                                    <asp:Label ID="lblfem" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                                        Height="24px" Width="89px" Font-Bold="True" ForeColor="#FF3300">0</asp:Label></td>
                                <td class="auto-style2">&nbsp;
						<asp:Label ID="lblmale" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                            Height="24px" Width="90px" Font-Bold="True" ForeColor="#FF3300">0</asp:Label></td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="#00ccff" style="height: 37px">
                                    <h4>Mortality</h4>
                                </td>
                                <td colspan="2" style="width: 88px; height: 37px">
                                    <asp:TextBox ID="Txtfemalemort" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Width="238px"></asp:TextBox></td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TxtMalemort" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Width="129px"></asp:TextBox></td>
                            </tr>
                           <tr align="left">
                                <td bgcolor="#9999ff" style="height: 37px">
                                    <h4 class="style1">Sexing(Error)</h4>
                                </td>
                                <td colspan="2" style="width: 88px; height: 37px">
                                    <asp:TextBox ID="txtpitfem" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Width="235px"></asp:TextBox></td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="txtpitmal" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Width="122px"></asp:TextBox></td>
                            </tr>
                            <tr align="left">
                                <td bgcolor="teal" style="height: 37px">
                                    <h4 class="style1">Farm Sales</h4>
                                </td>
                                <td colspan="2" style="width: 88px; height: 37px">
                                    <asp:TextBox ID="TxtFemalesale" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Height="23px" Width="234px"></asp:TextBox></td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TxtMaleSale" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Height="23px" Width="121px"></asp:TextBox>
                                </td>
                            </tr>

                            <tr align="left">
                                <td bgcolor="green" style="height: 37px">
                                    <h4 class="style1">Pit</h4>
                                </td>
                                <td colspan="2" style="width: 88px; height: 37px">
                                    <asp:TextBox ID="TxtFemalepit" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Height="23px" Width="234px"></asp:TextBox></td>
                                <td class="auto-style3">
                                    <asp:TextBox ID="TxtMalePit" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Height="23px" Width="121px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr align="left">
                                <td colspan="3" style="width: 193px">
                                    <asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label>
                                </td>
                                <td class="auto-style2">&nbsp;</td>
                            </tr>
                            <tr align="left">
                                <td colspan="4" align="center">&nbsp;
						<asp:Button ID="btnSAvemortality" Style="design_time_lock: True" runat="server" Design_Time_Lock="True"
                            Width="80px" Height="32px" Text="Save"></asp:Button>
                                    <asp:Button ID="Button2" Style="design_time_lock: True" runat="server" design_time_lock="True"
                                        Width="80px" Height="32px" Text="Cancel"></asp:Button></td>
                            </tr>
                        </table>
                        &nbsp;
                    </form>
                </div>
            </div>
        </div>
        <table>
            <%--[mor_fem], [mor_male], [pit_fem], [pit_male], [sale_fem], [sale_male] , [pit_fem_ext], [pit_male_ext],id from tmp_daily_morpit where shed_loc='" & shedlocation.Text & "'  and entry_date='" & entrydate.Value & "'"--%>
            <div class="row">
                <div class="col-sm-12" id="prdform">
                    <tr>
                        <td>
                            <asp:DataGrid ID="morptdata" runat="server" GridLines="Both" class="table table-striped table-hover" Font-Size="Medium" AutoGenerateColumns="false">
                                <AlternatingItemStyle BackColor="#ccccff"></AlternatingItemStyle>

                                <Columns>
                                    <asp:BoundColumn DataField="entry_date" DataFormatString="{0:dd-MMM-yy}" HeaderText="<B>Date</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="shed_no" HeaderText="<B>Shed No.</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="flock_no" HeaderText="<B>Flock No</B>"></asp:BoundColumn>

                                    <asp:BoundColumn DataField="mor_fem" HeaderText="<B>Mort(F)</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="mor_male" HeaderText="<B>Mort(M)</B>"></asp:BoundColumn>

                                     <asp:BoundColumn DataField="pit_fem" HeaderText="<B>Sexing Error(F)</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="pit_male" HeaderText="<B>Sexing Error(M)</B>"></asp:BoundColumn>

                                    <asp:BoundColumn DataField="pit_fem_ext" HeaderText="<B>Pit(F)</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="pit_male_ext" HeaderText="<B>Pit(M)</B>"></asp:BoundColumn>

                                    <asp:BoundColumn DataField="sale_fem" HeaderText="<B>Sale(F)</B>"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="sale_male" HeaderText="<B>Sale(M)</B>"></asp:BoundColumn>

                                   <asp:BoundColumn DataField="id" HeaderText="<B>ID</B>"></asp:BoundColumn>
                                    <asp:ButtonColumn ButtonType="PushButton" CommandName="Delete" Text="Delete"></asp:ButtonColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </div>
            </div>
        </table>
        <div>
           <asp:DataGrid ID="sum_mor" runat="server" GridLines="Both" class="table table-striped table-hover" Font-Size="Large" />
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

        .auto-style1 {
            height: 35px;
            width: 135px;
        }

        .auto-style2 {
            width: 135px;
        }

        .auto-style3 {
            height: 37px;
            width: 135px;
        }
    </style>
    <script type="text/javascript">
        document.getElementById('SiteMaster_entrydate').value = new Date().toISOString().substring(0, 10);
    </script>
</asp:Content>
