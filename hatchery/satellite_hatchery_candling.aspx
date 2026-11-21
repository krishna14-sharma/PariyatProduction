<%@ Page Language="VB" AutoEventWireup="false" CodeFile="satellite_hatchery_candling.aspx.vb" MasterPageFile="~/SiteMasterHatch.master" Inherits="hatchery_satellite_hatchery_candling" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server" >
    <div class="container" style="background-color:honeydew;">
        <table border="1" class="table" >
            <tr>
                <td>
                    <table border="1">
                        <tbody>
                             <tr>
                                <td colspan="5" valign="middle" align="center" rowspan="1">
                                    <h3>&nbsp;Satellite Hatchery Eggs 
							Setter &nbsp;Entry</h3>
                                </td>
                            </tr>
                            <tr>
                                <td>Loading Date</td>
                                <td align="left">
                                    <asp:TextBox ID="entrydate" TextMode="Date" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Transfer Date</td>
                                <td>
                                    <asp:TextBox ID="eggcolldate" TextMode="Date" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="toeggcolldate" TextMode="Date" runat="server" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td>Flock Type</td>
                                <td>
                                    <asp:DropDownList ID="ftype" runat="server" /></td>
                            </tr>
                              <tr>
                                <td>Area Name</td>
                                <td>
                                    <asp:DropDownList ID="cmbAreaName" runat="server" />
                                    <asp:Button ID="btnShowArea" runat="server" Text="Show Flock" />
                                </td>
                            </tr> 
                            <tr>
                                <td>Flock No</td>
                                <td>
                                    <asp:DropDownList ID="flockno" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Age (Desirable)</td>
                                <td>
                                    <asp:Label ID="LblAge" runat="server" /></td>
                            </tr>
                            <tr>
                                <td>Hathery </td>
                                <td>
                                    <asp:DropDownList ID="cmbloc" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="cmbshed" runat="server"></asp:DropDownList></td>

                                <td>
                                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                                    <asp:Button ID="Button1" runat="server" Text="Show" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Stock Eggs" Font-Bold="True" ForeColor="#0033CC" /></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Loading Eggs" Font-Bold="True" ForeColor="#003399" /></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Loadable" Font-Bold="True" ForeColor="#FF5050" /></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Crack" Font-Bold="True" ForeColor="#CC0066" /></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Leaker" Font-Bold="True" ForeColor="#CC0066" /></td>
                            </tr>

                            <tr>
                                <td>Fresh
                                    <asp:Label ID="lblfresh" runat="server" Font-Bold="True" ForeColor="#33CC33" Font-Size="Large" /></td>
                                <td>
                                    <asp:TextBox ID="fresh" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="freshset" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="freshrej" runat="server" ReadOnly="false" /></td>
                                <td>
                                    <asp:TextBox ID="fresh_leaker" runat="server" ReadOnly="True" /></td>
                            </tr>
                            <tr>
                                <td>Big Size
                                    <asp:Label ID="lblbigsize" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" /></td>
                                <td>
                                    <asp:TextBox ID="bigsize" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="bigsizeset" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="bigsizerej" runat="server" ReadOnly="false" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="bigsize_leaker" runat="server" ReadOnly="True" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td>MD
                                    <asp:Label ID="lblmd" runat="server" Font-Bold="True" ForeColor="#FF0066" Font-Size="Large" /></td>
                                <td>
                                    <asp:TextBox ID="md" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="mdset" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="mdrej" runat="server" ReadOnly="false" /></td>
                                <td>
                                    <asp:TextBox ID="md_leaker" runat="server" ReadOnly="true" /></td>
                            </tr>

                            <tr>
                                <td>M+
                                    <asp:Label ID="lblmplus" runat="server" Font-Bold="True" ForeColor="#3399FF" Font-Size="Large" /></td>
                                <td>
                                    <asp:TextBox ID="mplus" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mplusset" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mplusrej" runat="server" ReadOnly="false" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mplus_leaker" runat="server" ReadOnly="True" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td>M-<asp:Label ID="lblmminus" runat="server" Font-Bold="True" ForeColor="#3366CC" Font-Size="Large" /></td>
                                <td>
                                    <asp:TextBox ID="mminus" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mminusset" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mminusrej" runat="server" ReadOnly="false" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="mminus_leaker" runat="server" ReadOnly="True" Visible="False" /></td>
                            </tr>
                            <tr>
                                <td>Comm
                                    <asp:Label ID="lblhcomm" runat="server" Font-Bold="True" ForeColor="#CC00FF" Font-Size="Large" /></td>

                                <td>
                                    <asp:TextBox ID="hcomm" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="hcommser" runat="server" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="hcommrej" runat="server" ReadOnly="false" Visible="False" /></td>
                                <td>
                                    <asp:TextBox ID="hcomm_leaker" runat="server" ReadOnly="True" Visible="False" /></td>
                            </tr>
                            <tr>
                                 <td>Egg Collectiondate</td>
                                <td>
                                    
                                    <asp:Label ID="eggf" runat="server" ForeColor="#FF3300" />
                                </td>
                                <td>
                                   <asp:Label ID="eggto" runat="server" ForeColor="#FF3300" /></td>
                            </tr>
                            <tr>
                                <td colspan="5">&nbsp;
                                    <asp:Label ID="lblString" runat="server" Font-Bold="True" ForeColor="#FF3300" />
                                </td>
                            </tr>
                            <tr>
                                <td valign="centre">
                                    <asp:Button runat="server" ID="btnsave" Text="OK(SAVE)" /></td>
                                <td>
                                    <asp:Button runat="server" ID="cancel" Text="Show Data" />
                                    <asp:Button runat="server" ID="cancel0" Text="Cancel" /></td>
                            </tr>
                            <tr>
                                <td colspan="5"></td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td></td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <asp:DataGrid ID="hatch_setter" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                        <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                    </asp:DataGrid>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        //document.getElementById('AreaMaster_entrydate').value = new Date().toISOString().substring(0, 10);

        // $(document).ready(function () {

        function sum() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_fresh').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_freshset').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_freshrej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_fresh_leaker').value = result;
        }

        function sum1() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_bigsize').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_bigsizeset').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_bigsizerej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_bigsize_leaker').value = result;
        }


        function sum2() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_md').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_mdset').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_mdrej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_md_leaker').value = result;
        }


        function sum3() {

            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mplus').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_mplusset').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_mplusrej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_mplus_leaker').value = result;
        }

        function sum4() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mminus').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_mminusset').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_mminusrej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_mminus_leaker').value = result;
        }

        function sum5() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_hcomm').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_hcommser').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_hcommrej').value);
            var result = parseInt(txtValue) - parseInt(txtFirstNumberValue) - parseInt(txtSecondNumberValue)
            document.getElementById('SiteMasterHatch_hcomm_leaker').value = result;
        }

    </script>
</asp:Content>