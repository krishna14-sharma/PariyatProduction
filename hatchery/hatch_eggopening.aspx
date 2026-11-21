<%@ Page Language="VB" AutoEventWireup="true" MasterPageFile="~/SiteMasterHatch.master"  CodeFile="hatch_eggopening.aspx.vb" Inherits="hatchery_hatch_eggopening" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">
        <TABLE id="TABLE1" border="1" runat="server" style="WIDTH: 504px; HEIGHT: 295px">
				<TR>
					<TD colspan="2">
						<H2>&nbsp;Hatching Eggs Opening Balance Entry</H2>
					</TD>
				</TR>
				<TR>
					<TD>&nbsp;Date</TD>
					<TD>&nbsp;
                        <asp:TextBox ID="entrydate" TextMode="Date" runat="server" />
						</TD>
				</TR>
				<TR>
					<TD>&nbsp;Flock Bird Type</TD>
					<TD>&nbsp;
						<asp:dropdownlist id="birdtype" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
				<TR>
					<TD>&nbsp;Flock No</TD>
					<TD>&nbsp;
						<asp:dropdownlist id="flockno" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
                <TR>
					<TD>&nbsp;Age</TD>
					<TD>&nbsp;
						<asp:TextBox ID ="Age" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD>Location</TD>
					<TD>&nbsp;
						<asp:dropdownlist id="cmbloc" runat="server" AutoPostBack="True"></asp:dropdownlist></TD>
				</TR>
                <TR>
					<TD>Cold Room Code</TD>
					<TD>&nbsp;
						<asp:dropdownlist id="coldroomname" runat="server"></asp:dropdownlist></TD>
				</TR>

				<TR>
					<TD>&nbsp;Total&nbsp; Hatching Eggs</TD>
					<TD>&nbsp;
						<asp:TextBox id="txteggs" runat="server"></asp:TextBox></TD>
				</TR>
				<TR>
					<TD colspan="2" align="center">
						<asp:button id="btnSave" runat="server" Width="64px" Text="SAVE"></asp:button>&nbsp;&nbsp;
						<asp:Button id="Button3" runat="server" Text="Delete"></asp:Button>&nbsp;
						<asp:button id="Button2" runat="server" Width="77px" Text="Close"></asp:button>&nbsp;</TD>
				</TR>
            <tr>
                <td colspan="3" align="center"><asp:Label ID="lblprint" runat="server" Font-Bold="True" Font-Size="Large" ForeColor="#FF3300"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                <asp:DataGrid ID="eggopening_grid" runat="server" AutoGenerateColumns="true" GridLines="Both" class="table table-striped table-hover">
                    <AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
                    </asp:DataGrid>
                     </td>
            </tr>
				</TABLE>
    </div>
</asp:Content>
