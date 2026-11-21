<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="false" CodeFile="EmpAreaUpdate.aspx.vb" Inherits="masterforms_EmpAreaUpdate" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Employee Area And Attendance Updation</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <hr class="divider" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm">
                Select Area:
                <asp:DropDownList ID="arealist" runat="server" />
                <asp:Button ID="showemp" Text="Show Emp" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm">
                Select Employee:
                <asp:DropDownList ID="emplist" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm">
                Transfer To  Area:
                <asp:DropDownList ID="areato" runat="server" />
                <asp:Button ID="Button1" Text="Transfer" runat="server" />
                <asp:Button ID="Button2" Text="Left" runat="server" />
                <asp:Button ID="Button3" Text="Re-Join" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm">
                <asp:Label ID="lblstring" runat="server" Font-Size="X-Large" ForeColor="Red" />
            </div>
        </div>
                <div class="row">
            <div class="col-sm" id="prdform1">
                <hr class="divider" />
            </div>
        </div>
        <div class="row" >
            <div class="row">
            <div class="col-12">
                Select Attendance Date:
              <input type="date" id="att_date" runat="server" />
             
            </div>
        </div>
            <div class="row">
            <div class="col-12">
                <asp:CheckBox ID="chkPresent" runat="server" Text="Present" />
                <asp:CheckBox ID="chkAbsent" runat="server" Text="Absent" />
                 <asp:CheckBox ID="chkLeave" runat="server" Text="Leave" />
            </div>
        </div>
            <div class="row">
            <div class="col-12">
                <asp:Button Text="Update" runat ="server" id="att_update" />
            </div>
        </div>
        </div>
    </div>
</asp:Content>
