<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMasterAttendance.master" AutoEventWireup="false" CodeFile="uploadapplicationimage.aspx.vb" Inherits="attendance_uploadapplicationimage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">

       
        <div>
            <h2>Upload Employee Application</h2>
            <asp:Label ID="lblEmpCode" runat="server" Text="Employee Code:"></asp:Label>
            <asp:TextBox ID="txtEmpCode" runat="server"></asp:TextBox>
            <br /><br />
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <br /><br />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <br /><br />
            <asp:Label ID="lblMessage" runat="server" ForeColor="Green"></asp:Label>
        </div>
    
       </div>
    </asp:Content>
