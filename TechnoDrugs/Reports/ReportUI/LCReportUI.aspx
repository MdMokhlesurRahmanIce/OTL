<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LCReportUI.aspx.cs" 
    Inherits="TechnoDrugs.Reports.ReportUI.LCReportUI" %>
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
                        <h2>LC Report</h2>
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
                                <asp:Label ID="lblRequisitionDivision" runat="server" Text="Requisition Division"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlRequisitionDivision" runat="server" Width="91%" CssClass="drpwidth99per" DataValueField="ID" DataTextField="Name"
                                    AutoPostBack="True" DataSourceID="odsDivision" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlRequisitionDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label6" runat="server" Height="14px" Text="Requisition Ref. No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlRequistionRef" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" AppendDataBoundItems="true" DataValueField="ID" 
                                    DataTextField="ReferenceNo">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
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
                                        <asp:ListItem Value="1" Text="Date Wise" Selected="True"></asp:ListItem>
                                        <asp:ListItem Value="2" Text="Date Range Wise"></asp:ListItem>
                                    </asp:RadioButtonList>
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
                                            CssClass="txtwidth178px DatepickerInput" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="DateWise lblAndTxtStyle">
                                    <div class="divlblwidth100px">
                                        <asp:Label ID="Label11" runat="server" Height="14px" Text="Date"></asp:Label>
                                    </div>
                                    <div class="div80Px">
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="txtwidth178px DatepickerInput"></asp:TextBox>
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
            <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsProduct" runat="server" TypeName="SetupModule.Provider.ProductProvider"
                SelectMethod="GetAllFinishedProduct"></asp:ObjectDataSource>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
