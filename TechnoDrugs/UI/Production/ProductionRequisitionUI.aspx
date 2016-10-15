<%@ Page Title="Production Requisition" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ProductionRequisitionUI.aspx.cs" Inherits="TechnoDrugs.UI.Production.ProductionRequisitionUI" %>

<%@ Register Src="~/UI/Purchase/ProductSearch.ascx" TagName="UC_ProductSearch" TagPrefix="UCP" %>
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
            strCodeNDate = returnResult[0] + "," + returnResult[1];
            arrayList[0] = strCodeNDate;            
            __doPostBack('SearchPriceSetup', arrayList[0]);
        }

        function ShowWatermark() {
            var xy, yz = 0;
            if ($('#MainContent_ckbOption_0').is(':checked')) {
                xy = 1;
            }
            if ($('#MainContent_ckbOption_1').is(':checked')) {
                yz = 1;
            }
            var gridLenght = $('#MainContent_divGridForLSE tbody tr').length - 2;
            
            for (var i = 0; i < gridLenght; i++) {
                var reqty = "MainContent_gvPurchaseForLSE_txtRequiredQuantity_" + i;
                var seqty = "MainContent_gvPurchaseForLSE_txtSentQuantity_" + i;

                var vreqty = $('#' + reqty).val();
                var vseqty = $('#' + seqty).val();
                if (vreqty <= 0)
                {
                    alert('Required Quantity field can not be zero');
                    $('#' + reqty).val('');
                    return false;
                }
                if ((yz == 1) && (vseqty <= 0)) {
                    alert('Send Quantity field can not be zero');
                    $('#' + seqty).val('');
                    return false;
                }               
                
            }
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
                        <h2>Requisition from Production Department</h2>
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
                            <asp:Label ID="lblFinishedProduct" runat="server" Text="Finished Product"></asp:Label>
                        </div>
                        <div class="div182Px" style="width: 40%">
                            <ajax:ComboBox ID="ddlFinishedProduct" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px" AppendDataBoundItems="true"
                                Width="300px" RenderMode="Block" AutoPostBack="true" DataTextField="ProductFullName" DataValueField="ID" DataSourceID="odsFinishedProduct" OnSelectedIndexChanged="ddlFinishedProduct_SelectedIndexChanged">
                                <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                            </ajax:ComboBox>
                        </div>
                        <div style="text-align: left">
                            <asp:Label ID="lblPackSize" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div style="width: 33%; float: left; height: auto">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchNo" runat="server" Height="14px" Text="Batch No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBatchNo" runat="server" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionDivision" runat="server" Text="Product Division"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlRequisitionDivision" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px" AppendDataBoundItems="true"
                                    Width="150px" RenderMode="Block" DataValueField="ID" DataTextField="Name" AutoPostBack="True" DataSourceID="odsDivision" OnSelectedIndexChanged="ddlRequisitionDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblTheoriticalYield" runat="server" Height="14px" Text="Theoritical yield"></asp:Label>
                            </div>
                            <%--<div style="float: left; height: 15px; width: 30%;">--%>
                            <div class="divlblwidth100px">
                                <div style="float: left">
                                    <asp:TextBox ID="txtTheoriticalYield" Width="80px" runat="server" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                </div>
                                <div style="text-align: right">
                                    <ajax:ComboBox ID="ddlTheroicalUnit" runat="server" RenderMode="Block" DataValueField="ID" DataTextField="Name" Width="50px">
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
                            <asp:Button ID="btnCommonProducts" Text="Common Products" runat="server" OnClick="btnCommonProducts_Click" />
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionDate" runat="server" Height="14px" Text="Requisition Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtRequisitionDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtRequisitionDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRequisitionDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblMfgDate" runat="server" Height="14px" Text="Mfg. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtRequisitionDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtMfgDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtMfgDate" PopupButtonID="Image2">
                                </ajax:CalendarExtender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblExpDate" runat="server" Height="14px" Text="Exp. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtRequisitionDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtExpDate" runat="server" Enabled="false"/>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpDate" PopupButtonID="Image3">
                                </ajax:CalendarExtender>
                            </div>
                        </div>                        
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRefNo" runat="server" Height="14px" Text="Ref. No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtRefNo" CssClass="txtwidth178px" MaxLength="100" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchSize" runat="server" Height="14px" Text="Batch Size"></asp:Label>
                            </div>
                            <div class="divlblwidth100px">
                                <div style="float: left">
                                    <asp:TextBox ID="txtBatchSize" runat="server" Width="80px" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                </div>
                                <div style="text-align: right">
                                    <ajax:ComboBox ID="ddlBatchSizeUnit" runat="server" RenderMode="Block" DataValueField="ID" DataTextField="Name" Width="50px">
                                        <asp:ListItem Text="--Select--" Value="0" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="kg" Value="kg"></asp:ListItem>
                                        <asp:ListItem Text="liter" Value="liter"></asp:ListItem>
                                        <asp:ListItem Text="gm" Value="gm"></asp:ListItem>
                                        <asp:ListItem Text="vial" Value="vial"></asp:ListItem>
                                        <asp:ListItem Text="piece" Value="piece"></asp:ListItem>
                                        <asp:ListItem Text="capsule" Value="capsule"></asp:ListItem>
                                        <asp:ListItem Text="ml" Value="ml"></asp:ListItem>
                                        <asp:ListItem Text="box" Value="box"></asp:ListItem>
                                        <asp:ListItem Text="ampoule" Value="ampoule"></asp:ListItem>
                                    </ajax:ComboBox>
                                </div>
                            </div>                            
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblOption" runat="server" Height="14px" Text="Select Option:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:CheckBoxList ID="ckbOption" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Send" Value="1" onclick="MutExChkList(this);"></asp:ListItem>
                                    <asp:ListItem Text="Dispatch" Value="2" onclick="MutExChkList(this);"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <br />
                    <div id="Div1" runat="server" style="width: 100%; float: left">
                        <div style="width: 80%; float: left">
                            <UCP:UC_ProductSearch ID="UC_ProductSearch1" runat="server" />
                        </div>
                        <div style="width: 20%; height: 95px; float: left; text-align: center">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnAdd" runat="server" Text="ADD" OnClick="btnAdd_OnClick" Height="35px" Width="70px" />
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <div id="divGridForLSE" runat="server" visible="true">
                        <asp:GridView ID="gvPurchaseForLSE" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="Product Information" Width="100%" Align="left" OnRowDataBound="gvPurchaseForLSE_OnRowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# Container.DataItemIndex+1 %>'
                                            Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name of the Product">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("RawProductName") %>' Width="100%"></asp:Label>
                                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblTextTotal" Text="TOTAL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Stock">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPresentStock" runat="server" Text='<%# Bind("PresentStock") %>' ReadOnly="true" Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Required Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRequiredQuantity" runat="server" Text='<%# Bind("RequiredQuantity") %>' Width="90%" 
                                            
                                            onblur="ShowWatermark()" ></asp:TextBox>
                                        <%--onkeypress="return isNumberKeyAndDot(event,value);"--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("MeasurementUnitName","{0:F5}") %>' ReadOnly="true"
                                            onfocus="HideWatermark(this,'0.00')" onblur="ShowWatermark(this,'0.00')" Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sent Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSentQuantity" runat="server" Text='<%# Bind("SentQuantity","{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" Width="90%"></asp:TextBox>
                                         <%--onkeypress="return isNumberKeyAndDot(event,value);"--%>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetVATLeviable() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Bind("Remarks") %>'
                                            Style="width: 94%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalAmount() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteSelectedRowLSE" runat="server" ImageUrl="../../images/CancelIco.png" Width="40%"
                                            OnClick="btnDeleteSelectedRowLSE_Click" ToolTip="Delete Row" />
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="clear: both">
                    </div>
                    <asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />
                    <asp:HiddenField ID="hfVatAmount" runat="server" />

                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click"
                            ValidationGroup="Save" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update" Visible="false"
                            ValidationGroup="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                        <%-- <asp:Button ID="btnPrint" runat="server" class="btn action_del" Text="Print" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save" OnClick="btnPreview_Click" />--%>
                    </div>
                    <%--<asp:HiddenField ID="hfTarrifStatus" runat="server" />
                    <asp:HiddenField ID="hfVAT" runat="server" />
                    <asp:HiddenField ID="hfIsSDAmountTextChanged" runat="server" />
                    <asp:HiddenField ID="hfIsVATLeviableTextChanged" runat="server" />
                    <asp:HiddenField ID="hfIsVATAmountTextChanged" runat="server" />--%>
                    <asp:ObjectDataSource ID="odsFinishedProduct" runat="server" SelectMethod="GetAllFinishedProduct"
                        TypeName="SetupModule.Provider.ProductProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
