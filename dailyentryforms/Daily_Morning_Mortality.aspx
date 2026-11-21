<%@ Page Title="" Language="VB" MasterPageFile="~/AreaPage.master" AutoEventWireup="false" CodeFile="Daily_Morning_Mortality.aspx.vb" Inherits="dailyentryforms_Daily_Morning_Mortality" EnableEventValidation="false" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="AreaMaster" runat="server">
    <div class="container">
        <h3 class="mb-4">Morning Mortality Entry</h3>
        <form>
            <div class="row">
                <div class="col-sm-3">
                    <label for="areaName" class="form-label">Area Name</label>
                    <input type="text" class="form-control" id="areaName" name="areaName" readonly="true" runat="server" />
                </div>
                <div class="col-sm-3">
                    <label for="flockN0" class="form-label">Flock Number</label>
                    <asp:DropDownList class="form-control" ID="flockno" name="flockno" readonly="true" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <label for="mortalityFemale" class="form-label">Flock Mortality Female</label>
                    <input type="number" class="form-control" id="mortalityFemale" name="mortalityFemale" runat="server" />
                </div>
                <div class="col-sm-3">
                    <label for="mortalityMale" class="form-label">Flock Mortality Male</label>
                    <input type="number" class="form-control" id="mortalityMale" name="mortalityMale" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-3">
                    <label for="storeMale" class="form-label">Store Male</label>
                    <input type="number" class="form-control" id="storeMale" name="storeMale" runat="server" />
                </div>
                <div class="col-sm-3">
                    <label for="storeFemale" class="form-label">Store Female</label>
                    <input type="number" class="form-control" id="storeFemale" name="storeFemale" runat="server" />
                </div>
            </div>

            <div class="row">
                <div class="col-sm-3">
                    <label for="dewormingDay" class="form-label">Deworming Day</label>
                    <input type="text" class="form-control" id="dewormingDay" name="dewormingDay" runat="server" />
                </div>
                <div class="col-sm-3">
                    <label for="antibioticDay" class="form-label">Antibiotic Day</label>
                    <input type="text" class="form-control" id="antibioticDay" name="antibioticDay" runat="server" />
                </div>
            </div>

            <div class="row">
                <div class="col-md-6">
                    <label for="reamrks" class="form-label">Remarks(Medecine/Vaccine With Date)</label>
                    <input type="text" class="form-control" id="Remarks" name="Remarks" runat="server" />
                </div>
            </div>
            </br>
            <div class="row">
                <div class="col-md-6">
                     <asp:Button runat="server" ID="btnsave" Text="SAVE" class="btn btn-primary" />
                      <asp:Button runat="server" ID="btndelete" Text="DELETE" class="btn btn-primary" />
                     <asp:Label ID="lblPrint" runat="server" Text="Label"></asp:Label>
                    </div>
                </div>          
        </form>
        <asp:GridView ID="GridView1" runat="server">
            <AlternatingRowStyle BackColor="#FFFFCC" />
        </asp:GridView>
        <div class="row">
            <div class="col-md-6 mb-3">
            </div>
        </div>
    </div>
</asp:Content>
