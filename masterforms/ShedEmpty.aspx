<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup ="false" CodeFile="ShedEmpty.aspx.vb" Inherits="masterforms_ShedEmpty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Shed Empty Entry</h3>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:DropDownList ID="loc" runat="server" />
            </div>

            <div class="col-sm" id="prdform1">
                <asp:Button ID="btnshw" runat="server" Text="Get Flock" />
            </div>

            <div class="col-sm" id="prdform2">
                <asp:DropDownList ID="dflock" runat="server" />
            </div>

             <div class="col-sm" id="prdform3">
                <asp:Button ID="btnshd" runat="server" Text="Get Shed" />
            </div>
        </div>

        <div class="row">
            <div class="col-sm" id="prdform4">
                <asp:DataGrid runat="server" ID="shedepty" />
            </div>
        </div>

         <div class="row">
            <div class="col-sm" id="prdform4">
                 <asp:Button ID="btnShedEnpyu" runat="server" Text="Shed Empty" />
            </div>
        </div>

    </div>
</asp:Content>
