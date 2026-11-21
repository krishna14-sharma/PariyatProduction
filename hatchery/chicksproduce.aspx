<%@ Page Language="VB" MasterPageFile="~/SiteMasterHatch.master" AutoEventWireup="false" CodeFile="chicksproduce.aspx.vb" Inherits="hatchery_chicksproduce" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">
        <table border="1" class="table">
            <tr>
                <td>
                    <table border="1">
                        <tbody>
                            <tr>
                                <td colspan="5" valign="middle" align="center" rowspan="1">
                                    <h2>&nbsp;Production Entry</h2>
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
                                    <asp:Label ID="Label1" runat="server" Text="Fertile Eggs" Font-Bold="True" ForeColor="#0033CC" /></td>
                                <td>
                                    <asp:TextBox ID="TextBox1" runat="server" Width="80px" BorderStyle="None" ForeColor="White" ReadOnly="True" />
                                    <asp:Label ID="Label2" runat="server" Text="Chicks" Font-Bold="True" ForeColor="#003399" /></td>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Text="Hatcher Loss" Font-Bold="True" ForeColor="#FF5050" /></td>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Text="Supply" Font-Bold="True" ForeColor="#FF5050" /></td>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Culls" Font-Bold="True" ForeColor="#FF5050" /></td>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Text="Hatch(%)" Font-Bold="True" ForeColor="#FF5050" /></td>
                            </tr>

                            <tr>
                                <td>Fresh
                                    <asp:TextBox ID="lblfresh" runat="server" Font-Bold="True" ForeColor="#33CC33" Font-Size="Large" MaxLength="100" Width="75px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                    <asp:TextBox ID="fresh_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="White" ReadOnly="True" />
                                    <asp:TextBox ID="fresh_chicks" runat="server" Width="80px" > </asp:TextBox>
                                </td>
                                <td>
                                    <asp:TextBox ID="fresh_hatcherloss" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="fresh_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="fresh_culls" runat="server" Width="80px" /></td>
                                 <td>
                                    <asp:TextBox ID="fresh_hatch_per" runat="server" Width="80px" /></td>
                            </tr>
                            <tr>
                                <td>Big Size
                                    <asp:TextBox ID="lblbigsize" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" Width="65px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                     <asp:TextBox ID="bigsize_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="Black" ReadOnly="True" />
                                    <asp:TextBox ID="bigsize_chicks" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="bigsize_hatcherloss" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="bigsize_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="bigsize_culls" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="big_hatch_per" runat="server" Width="80px" /></td>
                            </tr>
                            <tr>
                                <td>MD
                                    <asp:TextBox ID="lblmd" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" Width="65px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                     <asp:TextBox ID="md_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="White" ReadOnly="True" />
                                    <asp:TextBox ID="md_chicks" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="md_hatcherloss" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="md_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="md_culls" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="md_hatch_per" runat="server" Width="80px" /></td>
                            </tr>

                           <tr>
                                <td>M+
                                    <asp:TextBox ID="lblmplus" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" Width="65px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                     <asp:TextBox ID="mplus_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="White" ReadOnly="True" />
                                    <asp:TextBox ID="mplus_chicks" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mplus_hatcherloss" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mplus_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mplus_culls" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mplus_hatch_per" runat="server" Width="80px" /></td>
                            </tr>
                            <tr>
                                <td>M-
                                    <asp:TextBox ID="lblmminus" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" Width="65px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                     <asp:TextBox ID="mminus_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="White" ReadOnly="True" />
                                    <asp:TextBox ID="mminus_chicks" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mminus_hatcherloss" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mminus_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mminus_culls" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="mminus_hatch_per" runat="server" Width="80px" /></td>
                            </tr>
                           <tr>
                                <td>Comm
                                    <asp:TextBox ID="lblhcomm" runat="server" Font-Bold="True" ForeColor="#FF6600" Font-Size="Large" Width="65px" BorderStyle="None" ReadOnly="True" /></td>
                                <td>
                                     <asp:TextBox ID="hcomm_infer" runat="server" Width="80px" BorderStyle="None" ForeColor="Black" ReadOnly="True" />
                                    <asp:TextBox ID="hcomm_chicks" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="hcomm_hatcherloss" runat="server" Width="80px" Height="27px" /></td>
                                <td>
                                    <asp:TextBox ID="hcomm_supply" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="hcomm_culls" runat="server" Width="80px" /></td>
                                <td>
                                    <asp:TextBox ID="hcomm_hatch_per" runat="server" Width="80px" /></td>
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
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_fresh_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_fresh_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_fresh_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblfresh').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_fresh_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblfresh').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_fresh_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_fresh_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_fresh_culls').value = result2
            document.getElementById('SiteMasterHatch_fresh_hatch_per').value = result1.toFixed(2)
        }

         function sum2() {
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_bigsize_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_bigsize_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_bigsize_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblbigsize').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_bigsize_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblbigsize').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_bigsize_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_bigsize_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_bigsize_culls').value = result2
            document.getElementById('SiteMasterHatch_big_hatch_per').value = result1.toFixed(2)
        }


        function sum1() {
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_md_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_md_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_md_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmd').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_md_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblmd').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_md_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_md_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_md_culls').value = result2
            document.getElementById('SiteMasterHatch_md_hatch_per').value = result1.toFixed(2)
        }

        function sum3() {         
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_mplus_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mplus_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_mplus_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmplus').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_mplus_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblmplus').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_mplus_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_mplus_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_mplus_culls').value = result2
            document.getElementById('SiteMasterHatch_mplus_hatch_per').value = result1.toFixed(2)
        }

        function sum4() {          
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_mminus_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_mminus_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_mminus_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblmminus').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_mminus_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblmminus').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_mminus_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_mminus_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_mminus_culls').value = result2
            document.getElementById('SiteMasterHatch_mminus_hatch_per').value = result1.toFixed(2)
        }

        function sum5() {
            var txtChicks = parseInt(document.getElementById('SiteMasterHatch_hcomm_chicks').value);
            var txtValue = parseInt(document.getElementById('SiteMasterHatch_hcomm_hatcherloss').value);
            var txtFirstNumberValue = parseInt(document.getElementById('SiteMasterHatch_hcomm_culls').value);
            var txtSecondNumberValue = parseInt(document.getElementById('SiteMasterHatch_lblhcomm').value);
            var txtinfer = parseInt(document.getElementById('SiteMasterHatch_hcomm_infer').value);
            var txtfer = parseInt(document.getElementById('SiteMasterHatch_lblhcomm').value);
            var txtSupply = parseInt(document.getElementById('SiteMasterHatch_hcomm_supply').value);
            var result = parseInt(txtfer) - parseInt(txtChicks)
            var result2 = parseInt(txtChicks) - parseInt(txtSupply)
            var result1 = (parseInt(txtChicks) / (txtSecondNumberValue + txtinfer)) * 100
            document.getElementById('SiteMasterHatch_hcomm_hatcherloss').value = result;
            document.getElementById('SiteMasterHatch_hcomm_culls').value = result2
            document.getElementById('SiteMasterHatch_hcomm_hatch_per').value = result1.toFixed(2)
        }

    </script>
</asp:Content>
