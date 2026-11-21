<%@ Page Language="VB" MasterPageFile="../SiteMaster.master" AutoEventWireup="false" CodeFile="EmpAdd.aspx.vb" Inherits="masterforms_EmpAdd" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="SiteMaster" runat="server">
    <div class="container">
        <h3>Add Employee</h3>
        <div class="row">
            <div class="col-6" id="prdform">
                <hr class="divider" />
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                <div class="form-group">
                Select Area:
                <asp:DropDownList ID="arealist" runat="server" class="form-control"/>
                    </div>
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                 <div class="form-group">
                Employee Code:
                <asp:TextBox ID="txtEmpCode" runat="server"  class="form-control"/>
                     </div>
            </div>
        </div>
        <div class="row">
            <div class="col-6">
                 <div class="form-group">
                Employee Name:
                <asp:TextBox ID="txtEmpName" runat="server" class="form-control" />
                     </div>
            </div>
        </div>

        <div class="row">
            <div class="col-6">
                 <div class="form-group">
                Select Gender:
                <asp:DropDownList ID="cmbGender" runat="server" class="form-control"  >
                    <asp:ListItem>M</asp:ListItem>
                    <asp:ListItem>F</asp:ListItem>
                    <asp:ListItem>OTHER</asp:ListItem>
                     </asp:DropDownList>
                     </div>
            </div>
        </div>


          <div class="row">
            <div class="col-6">
                 <div class="form-group">
                Select WorkType:
                <asp:DropDownList ID="cmbWorkType" runat="server" class="form-control" />
                     </div>
            </div>
        </div>

         

              <asp:Button id="btnaddemp" Text="Save" runat="server" class="btn btn-primary" />
            
        <div class="row">
            <div class="col-sm">
                <asp:Label ID="lblstring" runat="server" Font-Size="X-Large" ForeColor="Red" />
            </div>
        </div>
    </div>
</asp:Content>
