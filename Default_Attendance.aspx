<%@ Page Language="VB" AutoEventWireup="false"  MasterPageFile="SiteMasterAttendance.master" CodeFile="Default_Attendance.aspx.vb" Inherits="Default_Attendance" %>

<asp:Content ID="BodyContent_Attendance" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <section class="app-hero">
        <div>
            <div class="app-eyebrow">Attendance Management</div>
            <h1>Welcome to Attendance</h1>
            <p><asp:Label ID="lblString" runat="server" /></p>
            <p>Daily, weekly and monthly staff attendance and leave reports.</p>
        </div>
        <div>
            <img src="~/images/attendance_img.png" alt="" runat="server" />
        </div>
    </section>

    <p class="app-section-title">Quick links</p>
    <div class="app-tiles">
            <a class="app-tile" href="~/attendance/Daily_atttendancearea.aspx" runat="server"><strong>Daily Attendance</strong><span>Mark today&rsquo;s attendance</span></a>
            <a class="app-tile" href="~/attendance/w_att_all.aspx" runat="server"><strong>Weekly Attendance</strong><span>Weekly staff</span></a>
            <a class="app-tile" href="~/attendance/monthltatt.aspx" runat="server"><strong>Monthly Attendance</strong><span>Monthly staff</span></a>
            <a class="app-tile" href="~/attendance/Daily_attandence_report.aspx" runat="server"><strong>Daily Attendance Report</strong><span>Report</span></a>
            <a class="app-tile" href="~/attendance/Monthlyattendancereport.aspx" runat="server"><strong>Monthly Leave Report</strong><span>Report</span></a>
            <a class="app-tile" href="~/attendance/Muster_Report.aspx" runat="server"><strong>Muster Roll</strong><span>Export to Excel</span></a>
    </div>
</asp:Content>
