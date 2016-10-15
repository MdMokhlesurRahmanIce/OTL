<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FinishedProductsUI.aspx.cs" Inherits="TechnoDrugs.UI.Production.FinishedProductsUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".DatepickerInput").datepicker({ dateFormat: 'dd M yy' });
            $("#ui-datepicker-div").hide();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.DatepickerInput').datepicker({ dateFormat: 'dd M yy' });
                $("#ui-datepicker-div").hide();
            }
        });
        function Search() {
            var returnResult = window.showModalDialog("../Purchase/GeneralSearch.aspx", "", "dialogWidth:525px; dialogHeight:365px; dialogTop:180px; dialogLeft:226px; center:no; status:no");
            var arrayList = [];
            var strCodeNDate = "";
            // for (var i = 0; i < returnResult.length; i++)
            // {
            strCodeNDate = returnResult[0] + "," + returnResult[1];

            //}
            arrayList[0] = strCodeNDate;
            //
            //__doPostBack('SearchPriceSetup', returnResult[0]);
            __doPostBack('SearchPriceSetup', arrayList[0]);
        }
        function MutExChkList(chk) {
            var chkList = chk.parentNode.parentNode.parentNode;
            var chks = chkList.getElementsByTagName("input");
            for (var i = 0; i < chks.length; i++) {
                if (chks[i] != chk && chk.checked) {
                    chks[i].checked = false;
                }
            }
        }
    </script>
    <style type="text/css">
        .mGrid {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                text-align: center;
            }

            .mGrid th {
                padding: 4px 2px;
                color: #fff;
                background: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
                text-align: center;
            }

            .mGrid .alt {
                background: #fcfcfc;
            }

            .mGrid .pgr {
                background: #424242;
            }

                .mGrid .pgr table {
                    margin: 5px 0;
                }

                .mGrid .pgr td {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover {
                        color: #000;
                        text-decoration: none;
                    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updRequisition" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 35%; float: left">
                        <h2>Finished Products - Received From Production</h2>
                    </div>
                    <div style="width: 65%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px" style="width: 35%; text-align: right">
                            <asp:Label ID="lblFinishedProduct" runat="server" Text="Name of Finished Product"></asp:Label>
                        </div>
                        <div class="div182Px" style="width: 45%">
                            <ajax:ComboBox ID="ddlFinishedProduct" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px" AppendDataBoundItems="true"
                                Width="300px" RenderMode="Block" DataTextField="ProductFullName" Enabled="false" DataValueField="ID" DataSourceID="odsFinishedProduct">
                                <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                            </ajax:ComboBox>
                        </div>
                        <div class="div182Px" style="width: 15%">
                            <asp:Button ID="btnAddNewChallan" runat="server" Text="New Entry" OnClick="btnAddNewChallan_Click" />
                        </div>
                    </div>
                    <div style="width: 33%; float: left; height: auto">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchNo" runat="server" Height="14px" Text="Batch No."></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBatchNo" CssClass="txtwidth178px" MaxLength="100" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionDivision" runat="server" Text="Division"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlDivision" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px" AppendDataBoundItems="true"
                                    Width="150px" RenderMode="Block" DataValueField="ID" DataTextField="Name" DataSourceID="odsDivision">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblTheoriticalYield" runat="server" Height="14px" Text="Theoritical yield"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtTheoriticalYield" CssClass="txtwidth178px" MaxLength="100" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblReceivedDate" runat="server" Height="14px" Text="Received Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtReceivedDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtReceivedDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblMfgDate" runat="server" Height="14px" Text="Mfg. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtMfgDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMfgDate" PopupButtonID="Image2" Enabled="false">
                                </ajax:CalendarExtender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblExpDate" runat="server" Height="14px" Text="Exp. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtExpDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpDate" PopupButtonID="Image3" Enabled="false">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblReferenceNo" runat="server" Height="14px" Text="Reference No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtReferenceNo" CssClass="txtwidth178px" MaxLength="100" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblCommPack" runat="server" Height="14px" Text="Com. Pack Recv."></asp:Label>
                            </div>
                            <div style="float: left; height: 15px; width: 30%;">
                                <asp:TextBox ID="txtCommPackReceived" runat="server" CssClass="txtwidth178px" AutoPostBack="true"
                                    MaxLength="100" OnTextChanged="txtCommPackReceived_TextChanged"></asp:TextBox>
                            </div>
                            <div>
                                <asp:Label ID="lblPackSize" runat="server"></asp:Label>
                                <asp:Label ID="lblMeasurementUnit" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchRecvUnit" runat="server" Height="14px" Text="Batch Recv. Unit"></asp:Label>
                            </div>
                            <div class="divlblwidth100px">
                                <div style="float: left">
                                    <asp:TextBox ID="txtActualYield" runat="server" Width="80px" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                </div>
                                <div style="text-align: right">
                                    <ajax:ComboBox ID="ddlActualYieldUnit" runat="server" RenderMode="Block" DataValueField="ID" DataTextField="Name"  Width="50px">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="tab" Value="tab"></asp:ListItem>
                                        <asp:ListItem Text="bolus" Value="bolus"></asp:ListItem>
                                        <asp:ListItem Text="capsule" Value="capsule"></asp:ListItem>
                                        <asp:ListItem Text="ampoule" Value="ampoule"></asp:ListItem>
                                        <asp:ListItem Text="vial" Value="vial"></asp:ListItem>
                                        <asp:ListItem Text="sachet" Value="sachet"></asp:ListItem>
                                        <asp:ListItem Text="container" Value="container"></asp:ListItem>
                                        <asp:ListItem Text="bottle" Value="bottle"></asp:ListItem>
                                        <asp:ListItem Text="syringe" Value="syringe"></asp:ListItem>
                                        <asp:ListItem Text="bucket" Value="bucket"></asp:ListItem>
                                        <asp:ListItem Text="box" Value="box"></asp:ListItem>
                                        <asp:ListItem Text="liter" Value="liter"></asp:ListItem>
                                        <asp:ListItem Text="shell" Value="shell"></asp:ListItem>
                                        <asp:ListItem Text="kg" Value="kg"></asp:ListItem>
                                        <asp:ListItem Text="pcs" Value="pcs"></asp:ListItem>
                                    </ajax:ComboBox>
                                </div>
                            </div>
                        </div>



                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchSize" runat="server" Height="14px" Text="Batch Size"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBatchSize" CssClass="txtwidth178px" MaxLength="100" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblTotalReceived" runat="server" Height="14px" Text="Total Received."></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:Label ID="lblTotalReceivedAmount" runat="server" Height="14px"></asp:Label>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblActualYieldCal" runat="server" Height="14px" Text="Actual Yield (Cal.)"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:Label ID="lblActualYieldCalAmount" runat="server" Height="14px"></asp:Label>
                            </div>
                        </div>

                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <br />

                    <div style="clear: both">
                    </div>
                    <br />

                    <div style="clear: both">
                    </div>
                    <%--<asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />
                    <asp:HiddenField ID="hfVatAmount" runat="server" />--%>

                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnReceived" runat="server" Text="Received" class="btn action_save" ValidationGroup="Save" OnClick="btnReceived_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update" Visible="false"
                            ValidationGroup="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                        <%-- <asp:Button ID="btnPrint" runat="server" class="btn action_del" Text="Print" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save" OnClick="btnPreview_Click" />--%>
                    </div>
                    <asp:ObjectDataSource ID="odsFinishedProduct" runat="server" SelectMethod="GetAllFinishedProduct"
                        TypeName="SetupModule.Provider.ProductProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
