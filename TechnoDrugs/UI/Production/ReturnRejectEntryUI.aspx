<%@ Page Title="Return Reject Entry" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ReturnRejectEntryUI.aspx.cs" Inherits="TechnoDrugs.UI.Production.ReturnRejectEntryUI" %>

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
                    <div style="width: 30%; float: left">
                        <h2>Return / Reject</h2>
                    </div>
                    <div style="width: 70%; float: left">
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
                        <div class="div182Px" style="width: 60%">
                            <ajax:combobox id="ddlFinishedProduct" runat="server" autocompletemode="SuggestAppend" cssclass="griddrpwidth180px" appenddatabounditems="true"
                                width="300px" rendermode="Block" datatextfield="ProductFullName" datavaluefield="ID" datasourceid="odsFinishedProduct" Enabled="false">
                                <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                            </ajax:combobox>
                        </div>
                    </div>
                    <div style="width: 33%; float: left; height: auto">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRefNo" runat="server" Height="14px" Text="Ref. No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtRefNo" runat="server" ReadOnly="true" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionDivision" runat="server" Text="Requisition Division"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:combobox id="ddlRequisitionDivision" runat="server" autocompletemode="SuggestAppend" cssclass="griddrpwidth180px" appenddatabounditems="true"
                                    width="150px" rendermode="Block" datavaluefield="ID" datatextfield="Name" autopostback="True" datasourceid="odsDivision">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>

                                </ajax:combobox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblReturnDate" runat="server" Height="14px" Text="Ret./Rej. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtRequisitionDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtRetRejDate" runat="server" Enabled="false"/>
                                <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:calendarextender id="CalendarExtender4" runat="server" format="dd/MM/yyyy" targetcontrolid="txtRetRejDate" popupbuttonid="Image4">
                                </ajax:calendarextender>
                            </div>
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
                                <ajax:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy" targetcontrolid="txtRequisitionDate" popupbuttonid="Image1" Enabled="false">
                                </ajax:calendarextender>
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
                                <ajax:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy" targetcontrolid="txtMfgDate" popupbuttonid="Image2" Enabled="false">
                                </ajax:calendarextender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblExpDate" runat="server" Height="14px" Text="Exp. Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtRequisitionDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtExpDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:calendarextender id="CalendarExtender3" runat="server" format="dd/MM/yyyy" targetcontrolid="txtExpDate" popupbuttonid="Image3" Enabled="false">
                                </ajax:calendarextender>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchNo" runat="server" Height="14px" Text="Batch No."></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBatchNo" CssClass="txtwidth178px" MaxLength="100" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchSize" runat="server" Height="14px" Text="Batch Size"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBatchSize" CssClass="txtwidth178px" MaxLength="100" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblOption" runat="server" Height="14px" Text="Select Option:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:CheckBoxList ID="ckbOption" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Send" Value="1" onclick="MutExChkList(this);"></asp:ListItem>
                                    <asp:ListItem Text="Received" Value="2" onclick="MutExChkList(this);"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchRejection" runat="server" Height="14px" Text="Batch Rejection?"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:CheckBox ID ="ckbBatchRejection" runat ="server" />
                            </div>
                        </div>


                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <br />
                    <div id="Div1" runat="server" style="width: 100%; float: left">
                        <div style="width: 80%; float: left">
                            <ucp:uc_productsearch id="UC_ProductSearch1" runat="server" />
                        </div>
                        <div style="width: 20%; height: 95px; float: left; text-align: center">
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnAdd" runat="server" Text="ADD" Height="35px" Width="70px" Enabled ="false" />
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
                                
                                <asp:TemplateField HeaderText="Materials Taken">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtMaterialsTaken" runat="server" Text='<%# Bind("MaterialsTaken") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);" Enabled ="false"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReturnQuantity" runat="server" Text='<%# Bind("ReturnQuantity") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Reject Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRejectQuantity" runat="server" Text='<%# Bind("RejectQuantity") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
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

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Return Received">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReturnReceived" runat="server" Text='<%# Bind("ReturnReceived","{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetVATLeviable() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Reject Received">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRejectReceived" runat="server" Text='<%# Bind("RejectReceived","{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
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
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="clear: both">
                    </div>
                    <asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />
                    <asp:HiddenField ID="hfVatAmount" runat="server" />

                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click" />
                        <%--<asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update" OnClick="btnUpdate_Click" Visible ="false" />--%>
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                        <%-- <asp:Button ID="btnPrint" runat="server" class="btn action_del" Text="Print" OnClick="btnPrint_Click" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save" OnClick="btnPreview_Click" />--%>
                    </div>
                    <asp:HiddenField ID="hfTarrifStatus" runat="server" />
                    <asp:HiddenField ID="hfVAT" runat="server" />
                    <asp:HiddenField ID="hfIsSDAmountTextChanged" runat="server" />
                    <asp:HiddenField ID="hfIsVATLeviableTextChanged" runat="server" />
                    <asp:HiddenField ID="hfIsVATAmountTextChanged" runat="server" />
                    <asp:ObjectDataSource ID="odsFinishedProduct" runat="server" SelectMethod="GetAllFinishedProduct"
                        TypeName="SetupModule.Provider.ProductProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>