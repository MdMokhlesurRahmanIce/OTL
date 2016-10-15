<%@ Page Title="Purchase Order Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderReportUI.aspx.cs"
    Inherits="TechnoDrugs.Reports.ReportUI.PurchaseOrderReportUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UI/Purchase/ProductSearch.ascx" TagName="UC_ProductSearch" TagPrefix="UCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Content/themes/base/jquery-ui.css" rel="stylesheet" />
    <script type="text/javascript" src="../../Scripts/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            JqueryCode();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                JqueryCode();
            }
        });
        function JqueryCode() {
            $(".DatepickerInput").datepicker({ dateFormat: 'dd M yy' });
            //$("#ui-datepicker-div").hide();
            //$(".DateRangeWise").();            
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPurchase" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Purchase Order Report</h2>
                    </div>
                    <div style="width: 70%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblPurchaseOrderDivision" runat="server" Text="Purchase Order Division"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlPurchaseOrderDivision" runat="server" Width="91%" CssClass="drpwidth99per" DataValueField="ID" DataTextField="Name"
                                    AutoPostBack="True" DataSourceID="odsDivision" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlPurchaseOrderDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label6" runat="server" Height="14px" Text="PO Ref. No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlPurchaseOrderRef" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" AppendDataBoundItems="true" DataValueField="ID"
                                    DataTextField="POrderNo">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label7" runat="server" Height="14px" Text="Report Category"></asp:Label>
                            </div>
                            <div style="float: left; height: 15px; width: 56%;">
                                <asp:RadioButtonList ID="rdblReportCategory" runat="server" RepeatColumns="2"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Individual PO" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Single Product"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div style="clear: both">
                        </div>
                        <fieldset class="fieldset-panel">
                            <legend class="fieldset-legend">Select Date Range</legend>
                            <div class="lblAndTxtStyle">
                                <%--<div>
                                    <asp:RadioButtonList ID="rdblReportOption" runat="server">
                                        <asp:ListItem Value="1" Text="Date Range Wise" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Referenc No. Wise"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>--%>
                                <%--<div class="PurchaseID lblAndTxtStyle">
                                    <div class="divlblwidth100px">
                                        <asp:Label ID="Label8" runat="server" Height="14px" Text="Purchase Order No"></asp:Label>
                                    </div>
                                    <div class="div80Px">
                                        <asp:TextBox ID="txtPurchaseID" CssClass="txtwidth178px" runat="server"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="DateRangeWise lblAndTxtStyle">
                                    <div class="divlblwidth100px">
                                        <asp:Label ID="Label9" runat="server" Height="14px" Text="From Date"></asp:Label>
                                    </div>
                                    <div class="div80Px">
                                        <asp:TextBox ID="txtFromDate" runat="server"
                                            CssClass="txtwidth178px DatepickerInput"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="DateRangeWise lblAndTxtStyle">
                                    <div class="divlblwidth100px">
                                        <asp:Label ID="Label10" runat="server" Height="14px" Text="To Date"></asp:Label>
                                    </div>
                                    <div class="div80Px">
                                        <asp:TextBox ID="txtToDate" runat="server"
                                            CssClass="txtwidth178px DatepickerInput"></asp:TextBox>
                                    </div>
                                </div>
                        </fieldset>
                    </div>
                    <br /><br />
<br />
                    <div id="Div1" runat="server" style="width: 100%; float: left">
                        <div style="width: 82%; float: left">
                            <ucp:uc_productsearch id="UC_ProductSearch1" runat="server" />
                        </div>
                        <br />
                        <br />

                    </div>

                </div>
                <div style="clear: both; text-align: center;">
                    <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save"
                        OnClick="btnPreview_Click" />
                </div>
            </div>
            <div>
            </div>
            <br />
            <%--<asp:ObjectDataSource ID="odsProduct" runat="server" TypeName="SetupModule.Provider.ProductProvider"
                SelectMethod="GetAllFinishedProduct"></asp:ObjectDataSource>--%>
            <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
