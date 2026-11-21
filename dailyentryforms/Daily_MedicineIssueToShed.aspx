<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="../AreaPage.master" CodeFile="Daily_MedicineIssueToShed.aspx.vb" Inherits="dailyentryforms_Daily_MedicineIssueToShed" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Medecine Issue TO Shed</h3>
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="issuedate">Issue Date</label>
                    <input type="date" id="issue_date" name="issuedate" runat="Server" class="form-control"></input>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="from-group">
                    <label for="shedloc">Shed Location</label>
                    <asp:DropDownList ID="shedloc" runat="server" class="form-control" />
                </div>
            </div>
            <div class="col-sm-3">
                <div class="from-group">
                    <label for="shedno">Shed No</label>
                    <asp:DropDownList ID="shedno" runat="server" class="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemname">Item Name</label>
                    <asp:DropDownList ID="itemnane" runat="server" class="form-control" />
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemcode"></label>
                     <asp:Button ID="lftqty" Text="Show Balance Qty" runat="server"  class="btn btn-primary form-control" />
                </div>
            </div>
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="remqty">Balance Quantity at Farm</label>
                    <asp:TextBox ID="remqty" runat="server" Class="form-control" ReadOnly="True"></asp:TextBox>
                </div>
            </div>
        </div>

         <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="itemname">Item Unit</label>
                    <asp:label ID="lblunit" runat="server" class="form-control" />
                </div>
            </div>
            
            <div class="col-sm-3">
                <div class="form-group">
                      <label for="itemname"> </label>
                    <asp:label ID="lblitemCategory" runat="server" class="form-control" />
                </div>
            </div>

             <div class="col-sm-3">
                <div class="form-group">
                      <label for="itemname"> </label>
                    <asp:label ID="lblDept" runat="server" class="form-control" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="issueqty">Issue Qty</label>
                    <asp:TextBox ID="issueqty" runat="server" CssClass="form-control" />
                </div>
            </div>
             <div class="col-sm-3">
                <div class="form-group">
                    <label for="recperson">Receive Person Name</label>
                    <asp:TextBox ID="recperson" runat="server" CssClass="form-control" />
                </div>
            </div>
        </div>
        </br>
        <div class="row">
            <div class="col-sm-3">
                <asp:Button ID="Issue" Text="Issue" runat="server"  class="btn btn-success" />
                <asp:Button ID="Issue0" Text="Show" runat="server"  class="btn btn-info" />
            </div>
        </div>
        </br>
        <div class="row" >
            <div class="col-sm-8" >
                <asp:Label ID="inf" Text ="-" runat ="server" Font-Bold="True" Font-Size="Larger" ForeColor="#FF3300"  />
            </div>  
            <asp:DataGrid runat="server" ID="issuelist">
                <AlternatingItemStyle BackColor="YellowGreen" />
            </asp:DataGrid>              
        </div>

    </div>
    <%--container--%>
</asp:Content>
