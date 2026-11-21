<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="~/AreaPage.master" CodeFile="Default_Pvctray.aspx.vb" Inherits="Default_Pvctray" %>

<asp:Content ID="BodyContent_hatch" ContentPlaceHolderID="AreaMaster" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Tray Unit</div>
            <h1>Welcome to the Tray Unit</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Tray unit operations.</p>
        </div>
        <div>
            <img src="~/images/trayimage.jpg" alt="" runat="server" />
        </div>
    </section>
</asp:Content>
