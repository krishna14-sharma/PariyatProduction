<%@ Page Language="VB"  MasterPageFile="../AreaPage.master" AutoEventWireup="false" CodeFile="Grading_Entry.aspx.vb" Inherits="dailyentryforms_Grading_Entry" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3>Feed/Egg Information From Farm</h3>

         <div class="row">
             <div class="col-sm-3">
                <div class="form-group">
                <asp:RadioButton ID="rdFeMale" Text="FeMale" runat="server" AutoPostBack="True" /> 
                </div>
            </div>    
             
              <div class="col-sm-3">
                <div class="form-group">
                <asp:RadioButton ID="rdMale" Text="Male" runat="server" AutoPostBack="True" /> 
                </div>
            </div>        
         </div>

        <div class="row">
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="entrydate">Grading Date</label>
                    <input type="date" id="entrydate" name="gradingdate" runat="server" class="form-control">
                </div>
            </div>
            <div class="col-sm-3">
                 <div class="form-group">
                <label for="entrydate">Grading Week</label>
                <asp:DropDownList ID="gradingweek" runat="server" class="form-control">
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>12</asp:ListItem>
                    <asp:ListItem>16</asp:ListItem>
                    <asp:ListItem>20</asp:ListItem>
                    <asp:ListItem>24</asp:ListItem>
                </asp:DropDownList>
                     </div>
            </div>

            <div class="col-sm-3">

                <div class="form-group">
                    <label for="entrydate">Grading Number</label>
                    <asp:TextBox ID="gradingon" runat="server" />
                </div>
            </div>

        </div>

        <div class="row">
             <div class="col-sm-3">
                <div class="form-group">
                    <label for="">Flock Number</label>
                    <asp:DropDownList ID="lblflockno" runat="server" ForeColor="Red" class="form-control" />
                </div>
            </div>


            <div class="col-3">
                <div class="form-group">
                    <label for="">Flock Age</label>
                    <asp:Label ID="lblbirdbreed" runat="server" ForeColor="Red" class="form-control" />
                </div>
            </div>

            <div class="col-3">
                <div class="form-group">
                    <label for=""></label>
                    <asp:Button ID="showAge" runat="server" ForeColor="Red" Text="Get Age" class="form-control" />
                </div>
            </div>

           
            
            <div class="col-sm-3">
                <div class="form-group">
                    <label for="Standard Body Weight">Standard Body Weight</label>
                    <asp:Label ID="stdbodywt" runat="server" ForeColor="Red" class="form-control" />
                </div>
            </div>
        </div>

        <div class="row">
             <div class="col-3">
                <div class="form-group">
                    <label for="">Grading Weight</label>
                    <asp:TextBox ID="gradingwt" runat="server" ForeColor="Red" class="form-control" />
                </div>
            </div>

             <div class="col-3">
                <div class="form-group">
                    <label for="">Birds Qty</label>
                    <asp:TextBox ID="gradingqtybirds" runat="server" ForeColor="Red" class="form-control" />
                </div>
            </div>
        </div>

        <div class="row">
            <asp:Button ID="btngradingsave" Text="Save" runat="server" />
        </div>


        <div class="row">
            <div class="col-3">
            <asp:datagrid id="gradingdata" runat="server" PageSize="5"  GridLines="Both" class="table table-striped table-hover" Font-Size="Small">
			<AlternatingItemStyle BackColor="#99ccff"></AlternatingItemStyle>
			</asp:datagrid>
            </div>
          </div>

    </div>
</asp:Content>
