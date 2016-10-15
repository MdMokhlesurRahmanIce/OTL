<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendToHeadOfficeUI.aspx.cs"
    Inherits="TechnoDrugs.UI.Production.SendToHeadOfficeUI" %>

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
            __doPostBack('SearchPriceSetup', returnResult[0]);
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
        .mGrid
        {
            width: 100%;
            background-color: #fff;
            margin: 5px 0 10px 0;
            border: solid 1px #525252;
            border-collapse: collapse;
        }

            .mGrid td
            {
                padding: 2px;
                border: solid 1px #c1c1c1;
                color: #717171;
                text-align: center;
            }

            .mGrid th
            {
                padding: 4px 2px;
                color: #fff;
                background: #424242;
                border-left: solid 1px #525252;
                font-size: 0.9em;
                text-align: center;
            }

            .mGrid .alt
            {
                background: #fcfcfc;
            }

            .mGrid .pgr
            {
                background: #424242;
            }

                .mGrid .pgr table
                {
                    margin: 5px 0;
                }

                .mGrid .pgr td
                {
                    border-width: 0;
                    padding: 0 6px;
                    border-left: solid 1px #666;
                    font-weight: bold;
                    color: #fff;
                    line-height: 12px;
                }

                .mGrid .pgr a
                {
                    color: #666;
                    text-decoration: none;
                }

                    .mGrid .pgr a:hover
                    {
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
                        <h2>Finished Products - Send To HO</h2>
                    </div>
                    <div style="width: 65%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">

                    <div style="width: 33%; float: left; height: auto">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblChallanNo" runat="server" Height="14px" Text="Challan No."></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtChallanNo" CssClass="txtwidth178px" MaxLength="100" runat="server" Enabled="false"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>

                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionDivision" runat="server" Text="FG Division"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlDivision" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px"
                                    AppendDataBoundItems="true" AutoPostBack="true"
                                    Width="150px" RenderMode="Block" DataValueField="ID" DataTextField="Name" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Human" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Veterinary" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Anticancer" Value="4"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                        </div>


                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblReceivedDate" runat="server" Height="14px" Text="Challan Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtChallanDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtChallanDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px" style="width: 35%; text-align: right">
                                <asp:Label ID="lblFinishedProduct" runat="server" Text="Name of FG"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlFinishedProduct" runat="server" CssClass="griddrpwidth180px" Width="300px" RenderMode="Block"
                                    DataTextField="ProductFullName" DataValueField="ID" OnSelectedIndexChanged="ddlFinishedProduct_SelectedIndexChanged" AutoPostBack="true">
                                    <%--<asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>--%>
                                </ajax:ComboBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBatchNo" runat="server" Height="14px" Text="Batch No."></asp:Label>
                            </div>
                           <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlBatchNo" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px" 
                                    AutoPostBack ="true"
                                    Width="150px" RenderMode="Block" DataValueField="FinishedProductID" DataTextField="BatchNo">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                            
                        </div>



                    </div>
                    <div style="width: 33%; float: right">
                        <div id="Div1" runat="server" style="width: 100%; float: left">

                            <div style="height: 25px; float: right; text-align: right">

                                <asp:Button ID="btnAdd" runat="server" Text="ADD" OnClick="btnAdd_OnClick" Height="35px" Width="70px" />
                            </div>
                        </div>

                    </div>

                    <br />

                    <div style="clear: both">
                    </div>
                    <br />
                    <div id="divGridForLSE" runat="server" visible="true">
                        <asp:GridView ID="gvRequisition" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="Product Information" Width="100%" Align="left" OnRowDataBound="gvRequisition_OnRowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# Container.DataItemIndex+1 %>'
                                            Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductCode" runat="server" Text='<%# Bind("ProductCode") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("ProductName") %>' Width="100%"></asp:Label>
                                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Present Stock">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentStock" runat="server" Text='<%# Bind("PresentStock") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PackSize">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPackSize" runat="server" Text='<%# Bind("PackSize") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("MeasurementUnitName") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Batch No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatchNo" runat="server" Text='<%# Bind("BatchNo") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="MFC Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMFCDate" runat="server" Text='<%# Bind("MfgDate") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Exp Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpDate" runat="server" Text='<%# Bind("ExpDate") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Batch Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBatchQuantity" runat="server" Text='<%# Bind("BatchQuantity","{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Trade Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTradePrice" runat="server" Text='<%# Bind("TradePrice") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Trade Price">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalTradePrice" runat="server" Text='<%# Bind("TotalTradePrice") %>' Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
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

                    <%--   <asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />
                    <asp:HiddenField ID="hfVatAmount" runat="server" />--%>

                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click" />
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
