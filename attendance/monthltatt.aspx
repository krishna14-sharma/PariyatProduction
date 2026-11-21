<%@ Page Title="" Language="VB" MasterPageFile="~/SiteMasterAttendance.master" AutoEventWireup="false" CodeFile="monthltatt.aspx.vb" Inherits="attendance_monthltatt" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="PlaceHolder_Attendance" runat="server">
    <div class="container">
        <h4>Weekly Staff Attandence Report
        </h4>
        <div class="row">
            <div class="col-sm">
                Select Area:
                <asp:DropDownList ID="warealist" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="col-sm">
                <table>
                    <tr>
                        <td>
                            <label for="entrydate">Select Date</label></td>
                        <td>
                            <input type="date" id="entrydate" name="entrydate" runat="server"/></td>
                          <td>
                            <input type="date" id="entrydate1" name="entrydate1" runat="server"/><asp:Button ID="Button2" runat="server" Text="Show Attendance" /></td>
                        <td>
                            <asp:Button ID="Button1" runat="server" Text="Delete Attendance" /></td>
                    </tr>
                </table>
            </div>
            <div class="form-group">
            </div>
        </div>
          <div class="row">
            <div class="col-sm">
              
        <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="id" AutoGenerateEditButton="True"
            OnRowEditing="gvAttendance_RowEditing"
            OnRowCancelingEdit="gvAttendance_RowCancelingEdit"
            OnRowUpdating="gvAttendance_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="Both"  CssClass="gvCenterText"  AllowPaging="True"
    PageSize="50">
            
            <AlternatingRowStyle BackColor="White"  />
            
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True"  />
                <asp:BoundField DataField="empcode" HeaderText="Emp Code" ReadOnly="True" />
                 <asp:BoundField DataField="EmpName" HeaderText="Emp Name" ReadOnly="True" />
                <asp:BoundField DataField="loc" HeaderText="Location" ReadOnly="True" />
                <asp:BoundField DataField="month" HeaderText="Month" ReadOnly="True" />
                <asp:BoundField DataField="year" HeaderText="Year" ReadOnly="True"  />
                <asp:BoundField DataField="P" HeaderText="Present Days" ReadOnly="True" />
                <asp:BoundField DataField="L" HeaderText="Leave" ReadOnly="True" />
                <asp:BoundField DataField="A" HeaderText="Absent" ReadOnly="True" />

                <asp:TemplateField HeaderText="Passed Leave">
                    <ItemTemplate>
                        <%# Eval("S") %>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtS" runat="server" Text='<%# Bind("S") %>' Width="50px" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="AL" HeaderText="Unpaid Leave" ReadOnly="True" />
            </Columns>
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
  
            </div>
        </div>
    </div>

     <style type="text/css">
         .gvCenterText td, .gvCenterText th {
    text-align: center !important;
}
         </style>
</asp:Content>
