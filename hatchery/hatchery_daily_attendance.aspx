<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMasterHatch.master" AutoEventWireup="false" CodeFile="hatchery_daily_attendance.aspx.vb" Inherits="hatchery_hatchery_daily_attendance" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMasterHatch" runat="server">
    <div class="container">
        <h4>
            <asp:Label ID="hlabel" runat="server"> </asp:Label></h4>

        <div class="row">
            <div class="col-sm">
                Select Area: <asp:DropDownList ID ="arealist" runat="server" />
                <asp:Button ID="showemp" Text="Show Emp" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform">
                <h4>
                    <asp:TextBox ID="lbldate" TextMode="Date" runat="server" />
                </h4>
            </div>
            <div class="col-sm" id="prdform">
                <h3>
                    <asp:Label ID="Label1" runat="server" ForeColor="#ff3300"> </asp:Label></h3>
            </div>
        </div>
        <div class="row">
            <div class="col-sm" id="prdform">
                <asp:DataGrid runat="server" ID="daily_att" AutoGenerateColumns="False">
                    <AlternatingItemStyle BackColor="#ffffcc"></AlternatingItemStyle>
                    <Columns>
                         <asp:BoundColumn DataField="EmpSerial" HeaderText="&lt;B&gt;Sr No&lt;/B&gt;"></asp:BoundColumn>  
                        <asp:BoundColumn DataField="EmpCode" HeaderText="&lt;B&gt;Emp Code&lt;/B&gt;"></asp:BoundColumn>
                        <asp:BoundColumn DataField="EmpName" HeaderText="&lt;B&gt;Emp Name&lt;/B&gt;"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate><b>Absent</b></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkAbsent" BorderStyle="None" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate><b>Leave</b></HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkLeave" BorderStyle="None" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <HeaderTemplate><b>Remark</b></HeaderTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtremerk" BorderStyle="Dashed" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateColumn>
                           <asp:BoundColumn DataField="Sex" HeaderText="&lt;B&gt;M/F&lt;/B&gt;"></asp:BoundColumn>
                    </Columns>

                </asp:DataGrid>
            </div>
        </div>
        <div class="col-sm" id="prdform">
            <h4>
                <asp:Button ID="btnAttSAve" runat="server" Text="Save"></asp:Button></h4>
        </div>
    </div>
</asp:Content>