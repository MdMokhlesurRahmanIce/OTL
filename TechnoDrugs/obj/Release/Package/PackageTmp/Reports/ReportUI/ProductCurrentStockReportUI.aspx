﻿<%@ Page Title="Product Current Stock Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="ProductCurrentStockReportUI.aspx.cs" Inherits="TechnoDrugs.Reports.ReportUI.ProductCurrentStockReportUI" %>
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
            $("#ui-datepicker-div").hide();
            $(".DateRangeWise").show();
            $(".DateWise").hide();
            if ($("#MainContent_rdblReportOption_1").is(":checked")) {
                $(".DateRangeWise").show();
                $(".PurchaseID").hide();
                $(".DateWise").hide();
                $("#MainContent_txtPurchaseID").val("");
                $("#MainContent_txtDate").val("");
            }
            if ($("#MainContent_rdblReportOption_0").is(":checked")) {
                $(".DateRangeWise").hide();
                $(".PurchaseID").hide();
                $(".DateWise").show();
                $("#MainContent_txtFromDate").val("");
                $("#MainContent_txtToDate").val("");
                $("#MainContent_txtPurchaseID").val("");
            }
            $("#MainContent_rdblReportOption_1").live("click", function () {
                $(".DateRangeWise").show();
                $(".PurchaseID").hide();
                $(".DateWise").hide();
                $("#MainContent_txtPurchaseID").val("");
                $("#MainContent_txtDate").val("");

                $(".productCategory").show();
                $("#MainContent_ddlProductID_TextBox").attr("disabled", false);
            });
            $("#MainContent_rdblReportOption_2").live("click", function () {
                $(".DateRangeWise").hide();
                $(".PurchaseID").show();
                $(".DateWise").hide();
                $("#MainContent_txtFromDate").val("");
                $("#MainContent_txtToDate").val("");
                $("#MainContent_txtDate").val("");

                $(".productCategory").hide();
                $("#MainContent_ddlProductID_TextBox").attr("disabled", true);
                $("#MainContent_ddlProductID_TextBox").val("");
            });
            $("#MainContent_rdblReportOption_0").live("click", function () {
                $(".DateRangeWise").hide();
                $(".PurchaseID").hide();
                $(".DateWise").show();
                $("#MainContent_txtFromDate").val("");
                $("#MainContent_txtToDate").val("");
                $("#MainContent_txtPurchaseID").val("");

                $(".productCategory").show();
                $("#MainContent_ddlProductID_TextBox").attr("disabled", false);
            });
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPurchase" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Product Current Stock Report</h2>
                    </div>
                    <div style="width: 70%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div style="width: 35%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="91%" CssClass="drpwidth99per"
                                    DataValueField="ID" DataTextField="Name"
                                    AutoPostBack="True" DataSourceID="odsDivision" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>
                    <div style="width: 45%; float: right">
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
                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="txtwidth178px DatepickerInput"></asp:TextBox>
                                    </div>
                                </div>
                                
                        </div>
                </div>
                <br /><br /><br />
                <div id="Div1" runat="server" style="width: 100%; float: left">
                    <div style="width: 82%; float: left">
                        <UCP:UC_ProductSearch ID="UC_ProductSearch1" runat="server" />
                    </div>
                    <br /><br />

                </div>
                <div class="form-details">
                </div>
                <div style="clear: both; text-align: center;">
                    <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save"
                        OnClick="btnPreview_Click" />
                </div>
            </div>
            <div>
            </div>
            <br />
            <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                    TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
