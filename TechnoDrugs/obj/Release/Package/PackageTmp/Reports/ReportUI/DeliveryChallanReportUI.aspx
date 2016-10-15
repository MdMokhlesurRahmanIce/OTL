<%@ Page Title="Delivery Challan Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryChallanReportUI.aspx.cs"
     Inherits="TechnoDrugs.Reports.ReportUI.DeliveryChallanReportUI" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

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
            $(".DateRangeWise").hide();
            if ($("#MainContent_rdblReportOption_1").is(":checked")) {
                $(".DateRangeWise").show();
                $(".PurchaseID").hide();
                $("#MainContent_txtPurchaseID").val("");
            }
            if ($("#MainContent_rdblReportOption_0").is(":checked")) {
                $(".DateRangeWise").hide();
                $(".PurchaseID").hide();
                $("#MainContent_txtFromDate").val("");
                $("#MainContent_txtToDate").val("");
                $("#MainContent_txtPurchaseID").val("");
            }
            $("#MainContent_rdblReportOption_1").live("click", function () {
                $(".DateRangeWise").show();
                $(".PurchaseID").hide();
                $("#MainContent_txtPurchaseID").val("");

                $(".productCategory").show();
                $("#MainContent_ddlProductID_TextBox").attr("disabled", false);
            });
            $("#MainContent_rdblReportOption_2").live("click", function () {
                $(".DateRangeWise").hide();
                $(".PurchaseID").show();
                $("#MainContent_txtFromDate").val("");
                $("#MainContent_txtToDate").val("");

                $(".productCategory").hide();
                $("#MainContent_ddlProductID_TextBox").attr("disabled", true);
                $("#MainContent_ddlProductID_TextBox").val("");
            });
            $("#MainContent_rdblReportOption_0").live("click", function () {
                $(".DateRangeWise").hide();
                $(".PurchaseID").hide();
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
                        <h2>Delivery Challan Report</h2>
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
                                <asp:Label ID="lblProductType" runat="server" Text="Product Type"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="dllItemTypeID" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AutoPostBack="True" DataSourceID="odsProductType"
                                    AppendDataBoundItems="true" OnSelectedIndexChanged="dllItemTypeID_SelectedIndexChanged" >
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDelChallanNo" runat="server" Height="14px" Text="Delivery Challan No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlDeliveryChallanNo" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" DataValueField="ID" 
                                    DataTextField="DeliveryChallanNo">
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                        <div class="lblAndTxtStyle productCategory">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label7" runat="server" Height="14px" Text="Report Category"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:RadioButtonList ID="rdblReportCategory" runat="server" RepeatColumns="2"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1" Text="Summary" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Details"></asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div style="clear: both">
                        </div>
                        <fieldset class="fieldset-panel">
                            <legend class="fieldset-legend">Report Option</legend>
                            <div class="lblAndTxtStyle">
                                <div>
                                    <asp:RadioButtonList ID="rdblReportOption" runat="server">
                                        <asp:ListItem Value="1" Text="Date Range Wise" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Referenc No. Wise"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                                <div class="PurchaseID lblAndTxtStyle">
                                    <div class="divlblwidth100px">
                                        <asp:Label ID="lblDeliveryChallanNo" runat="server" Height="14px" Text="Delivery Challan No"></asp:Label>
                                    </div>
                                    <div class="div80Px">
                                        <asp:TextBox ID="txtDeliveryChallanNo" CssClass="txtwidth178px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
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
                </div>
                <div style="clear: both; text-align: center;">
                    <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save"
                        OnClick="btnPreview_Click" />
                </div>
            </div>
            <div>
            </div>
            <br />
            <asp:ObjectDataSource ID="odsProduct" runat="server" TypeName="SetupModule.Provider.ProductProvider"
                SelectMethod="GetAllFinishedProduct"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsProductType" runat="server" SelectMethod="GetAllActive"
                TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
