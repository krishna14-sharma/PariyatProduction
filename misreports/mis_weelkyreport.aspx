<%@ Page Language="VB" AutoEventWireup="false"
    MasterPageFile="~/ReportPage.master"
    CodeFile="mis_weelkyreport.aspx.vb"
    Inherits="misreports_mis_weelkyreport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="ReportMaster" runat="server">

    <!-- ===================== CSS ===================== -->
    <style>
        body {
            background-color: #f4f6f9;
            font-family: 'Segoe UI', Tahoma, Arial, sans-serif;
        }

        .container {
            background: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 12px rgba(0,0,0,0.08);
            margin-top: 20px;
        }

        h3 {
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 20px;
            border-left: 5px solid #007bff;
            padding-left: 10px;
        }

        .filter-card {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 8px;
            margin-bottom: 20px;
            box-shadow: inset 0 0 5px rgba(0,0,0,0.05);
        }

        table {
            width: 100%;
        }

        table td {
            padding: 8px 10px;
            vertical-align: middle;
            white-space: nowrap;
        }

        select,
        input[type="submit"],
        input[type="button"] {
            height: 38px;
            padding: 5px 10px;
            border-radius: 5px;
            border: 1px solid #ced4da;
            font-size: 14px;
        }

        select:focus,
        input[type="submit"]:focus {
            outline: none;
            border-color: #007bff;
            box-shadow: 0 0 0 0.15rem rgba(0,123,255,.25);
        }

        input[type="submit"],
        input[type="button"] {
            background-color: #007bff;
            color: #fff;
            border: none;
            cursor: pointer;
            transition: background 0.3s ease;
        }

        input[type="submit"]:hover,
        input[type="button"]:hover {
            background-color: #0056b3;
        }

        .report-container {
            background: #ffffff;
            padding: 15px;
            border-radius: 8px;
            box-shadow: 0 1px 10px rgba(0,0,0,0.06);
        }

        #CrystalReportViewer1 {
            width: 100% !important;
            min-height: 600px;
            border: 1px solid #dee2e6;
            border-radius: 6px;
        }

        @media (max-width: 768px) {
            table td {
                display: block;
                width: 100%;
            }

            select,
            input[type="submit"] {
                width: 100%;
                margin-bottom: 10px;
            }
        }
    </style>
    <!-- ===================== END CSS ===================== -->

    <div class="container">

        <h3>Weekly Report</h3>

        <!-- ===================== FILTER SECTION ===================== -->
        <div class="filter-card">
            <table>
                <tr>
                    <td>
                        <asp:DropDownList ID="areamaster" runat="server" AutoPostBack="True" />
                    </td>

                    <td>
                        <asp:DropDownList ID="cmbFlock" runat="server" />
                    </td>

                    <td>
                        <strong>Select Age</strong>
                    </td>

                    <td>
                        <asp:DropDownList ID="cmbAge" runat="server" />
                    </td>

                    <td>
                        <asp:Button ID="Button1" runat="server" Text="Show Report" />
                    </td>
                </tr>
            </table>
        </div>
        <!-- ===================== END FILTER ===================== -->

        <!-- ===================== REPORT VIEWER ===================== -->
        <div class="report-container">
            <div class="app-report"><CR:CrystalReportViewer
                ID="CrystalReportViewer1"
                runat="server"
                AutoDataBind="true"
                HasToggleGroupTreeButton="true"
                HasExportButton="true"
                HasPrintButton="true"
                ToolPanelView="None" /></div>
        </div>
        <!-- ===================== END REPORT ===================== -->

    </div>

</asp:Content>
