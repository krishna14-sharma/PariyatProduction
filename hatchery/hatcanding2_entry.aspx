<%@ Page Language="VB" MasterPageFile="~/SiteMasterHatch.master" AutoEventWireup="false" CodeFile="hatcanding2_entry.aspx.vb" Inherits="hatchery_hatcanding2_entry" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">
        <table border="1" class="table">
            <tr>
                <td>
                    <table border="1">
                        <tbody>
                            <tr>
                                <td colspan="5" valign="middle" align="center" rowspan="1">
                                    <h2>&nbsp;Transfer Eggs 
							    &nbsp;Entry</h2>
                                </td>
                            </tr>
                            <tr>
                                <td>Loading Date</td>
                                <td align="left">
                                    <asp:TextBox ID="entrydate" TextMode="Date" runat="server" /></td>
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
                                <td>Hathery </td>
                                <td>
                                    <asp:DropDownList ID="cmbloc" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="cmbshed" runat="server"></asp:DropDownList></td>

                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Show" />
                                </td>
                            </tr>
                            <tr>
                                <td>Age (Desirable)</td>
                                <td>
                                    <asp:Label ID="LblAge" runat="server" /></td>
                            </tr>
                             <tr>
                                <td>Select Hatcher </td>
                                <td>
                                    <asp:DropDownList ID="cmbhatch" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                                <td>
                                    <asp:DropDownList ID="cmbsetter" runat="server"></asp:DropDownList></td>

                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="Show" />
                                </td>
                            </tr>


                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Loaded Eggs" Font-Bold="True" ForeColor="#0033CC" /></td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text="Fertile Eggs" Font-Bold="True" ForeColor="#003399" /></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Infertile Eggs" Font-Bold="True" ForeColor="#FF5050" /></td>
                            </tr>

                            <tr>
                                <td>Fresh
                                    <asp:TextBox ID="lblfresh" runat="server" Font-Bold="True" ForeColor="#33CC33" Font-Size="Large"  ReadOnly="true" Width="100px"/>
                                    
                                </td>
                                <td>
                                    <asp:TextBox ID="fresh" runat="server" />
                                </td>
                                <td>
                                    <asp:TextBox ID="freshset" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Big Size
                                    <asp:TextBox ID="lblbigsize" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large"  ReadOnly="true" Width="100px"/></td>
                                <td>
                                    <asp:TextBox ID="bigsize" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="bigsizeset" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>MD
                                    <asp:TextBox ID="lblmd" runat="server" Font-Bold="True" ForeColor="#FF0066" Font-Size="Large"  ReadOnly="true" Width="100px"/></td>
                                <td>
                                    <asp:TextBox ID="md" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="mdset" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>

                            <tr>
                                <td>M+
                                    <asp:TextBox ID="lblmplus" runat="server" Font-Bold="True" ForeColor="#3399FF" Font-Size="Large" ReadOnly="true" Width="100px" /></td>
                                <td>
                                    <asp:TextBox ID="mplus" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="mplusset" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>M-<asp:TextBox ID="lblmminus" runat="server" Font-Bold="True" ForeColor="#3366CC" Font-Size="Large" ReadOnly="true" Width="100px" /></td>
                                <td>
                                    <asp:TextBox ID="mminus" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="mminusset" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                            <tr>
                                <td>Comm
                                    <asp:TextBox ID="lblhcomm" runat="server" Font-Bold="True" ForeColor="#CC00FF" Font-Size="Large" ReadOnly="true" Width="100px" /></td>

                                <td>
                                    <asp:TextBox ID="hcomm" runat="server" /></td>
                                <td>
                                    <asp:TextBox ID="hcommser" runat="server" /></td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
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
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblfresh').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_freshset').value = result;
        }

        function sum1() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_bigsize').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblbigsize').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_bigsizeset').value = result;
        }


        function sum2() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_md').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmd').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_mdset').value = result;
        }


        function sum3() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mplus').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmplus').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_mplusset').value = result;
        }

        function sum4() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mminus').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmminus').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_mminusset').value = result;
        }

        function sum5() {
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_hcomm').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblhcomm').value);
            var result = parseInt(txtFirstNumberValue) - parseInt(txtValue)
            document.getElementById('SiteMasterHatch_hcommser').value = result;
        }

    </script>
</asp:Content>
