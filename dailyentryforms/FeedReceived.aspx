<%@ Page Title="" Language="VB" MasterPageFile="~/AreaPage.master" AutoEventWireup="false" CodeFile="FeedReceived.aspx.vb" Inherits="dailyentryforms_FeedReceived"   EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
       <form >
        <h2>Feed Received Entry</h2>

        <div class="insert-panel">
            <h3>Add New Record</h3>
            <div class="form-row">
                <label>FeedRecDate</label>
                <asp:TextBox ID="tbFeedRecDate" runat="server" TextMode="Date"></asp:TextBox>
                <asp:Button ID="Button1" runat="server" Text="Show Feed" />
            </div>
            <div class="form-row">
                <label>FeedName</label>
                <asp:DropDownList ID="ddlFeedName" runat="server"  Width="452px" ></asp:DropDownList>
                <asp:Button ID="Button2" runat="server" Text="Show" />
            </div>
            <div class="form-row">
                <label>Received Bags</label>
                <asp:TextBox ID="tbNoOfBags" runat="server" TextMode="Number"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>Received Qty </label>
&nbsp;<asp:TextBox ID="tbQry" runat="server"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>BagsDispatch</label>
                <asp:TextBox ID="tbBagsDispatch" runat="server" TextMode="Number" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="form-row">
                <label>QtyDispatch</label>
                <asp:TextBox ID="tbQtyDispatch" runat="server" ReadOnly="True"></asp:TextBox>
            </div>
            <div class="form-row">
                &nbsp;<asp:Label ID="lblMsg" runat="server"></asp:Label>
            </div>
            <div class="form-row">
                &nbsp;</div>
            <asp:Button ID="btnInsert" runat="server" Text="Save"  />
        </div>



        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            DataKeyNames="id"
            AllowPaging="true" PageSize="10"
            AllowSorting="true"
            OnPageIndexChanging="GridView1_PageIndexChanging"
            OnRowEditing="GridView1_RowEditing"
            OnRowCancelingEdit="GridView1_RowCancelingEdit"
            OnRowUpdating="GridView1_RowUpdating"
            OnRowDeleting="GridView1_RowDeleting">
            <Columns>
                <asp:BoundField DataField="id" HeaderText="ID" ReadOnly="True" SortExpression="id" />
                <asp:BoundField DataField="FeedRecDate" HeaderText="FeedRecDate" DataFormatString="{0:yyyy-MM-dd}" SortExpression="FeedRecDate" />
                <asp:BoundField DataField="FeedName" HeaderText="FeedName" SortExpression="FeedName" />
                <asp:BoundField DataField="NoOfBags" HeaderText="RecBags" SortExpression="NoOfBags" />
                <asp:BoundField DataField="Qry" HeaderText="RecQty" DataFormatString="{0:N3}" SortExpression="Qry" />
                <asp:BoundField DataField="BagsDispatch" HeaderText="BagsDispatch" SortExpression="BagsDispatch" />
                <asp:BoundField DataField="QtyDispatch" HeaderText="QtyDispatch" DataFormatString="{0:N3}" SortExpression="QtyDispatch" />
                <asp:BoundField DataField="Users" HeaderText="Users" SortExpression="Users" />
                <asp:BoundField DataField="entrtdate" HeaderText="Entry Date" DataFormatString="{0:yyyy-MM-dd}" SortExpression="entrtdate" />
                <asp:CommandField ShowEditButton="True" ShowDeleteButton="True" />
            </Columns>
        </asp:GridView> 
    </form>

    </div>

    <style>
        body { font-family: Segoe UI, Arial, sans-serif; margin: 24px; }
        .form-row { margin-bottom: 8px; }
        label { display: inline-block; width: 140px; }
        input { width: 220px; }
        .insert-panel { border: 1px solid #ddd; padding: 12px; margin-bottom: 18px; border-radius: 6px; }
        .error { color: #b00020; margin-top: 8px; }
        .success { color: #006400; margin-top: 8px; }
    </style>
</asp:Content>
