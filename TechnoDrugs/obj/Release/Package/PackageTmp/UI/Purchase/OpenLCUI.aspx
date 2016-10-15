<%@ Page Title="LC Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OpenLCUI.aspx.cs" Inherits="TechnoDrugs.UI.Purchase.OpenLCUI" %>

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
            var returnResult = window.showModalDialog("GeneralSearch.aspx", "", "dialogWidth:525px; dialogHeight:365px; dialogTop:180px; dialogLeft:226px; center:no; status:no");
            __doPostBack('SearchPriceSetup', returnResult[0]);
        }
        function isNumberKeyAndDot(evt) {
            if (evt.charCode > 31 && (evt.charCode < 48 || evt.charCode > 57) && (evt.charCode > 46)) {
                alert("Allow Only Numbers");
                return false;
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
                        <h2>LC Challan</h2>
                    </div>
                    <div style="width: 70%; float: left">
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
                                <asp:Label ID="lblSystemLCNo" runat="server" Height="14px" Text="System LC No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtSystemLCNo" runat="server" ReadOnly="true" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBankLCNumber" runat="server" Height="14px" Text="Bank LC Number"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBankLCNumber" runat="server" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblLCAFNumber" runat="server" Height="14px" Text="LCAF Number"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtLCAFNumber" runat="server" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                         <div class="lblAndTxtStyle">
                            <asp:Button ID ="btnCommonProducts" Text ="Common Products" runat ="server" OnClick="btnCommonProducts_Click" />
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlSupplier" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" DataSourceID="odsSupplier" AppendDataBoundItems="true" DataValueField="SupplierID" DataTextField="SupplierName">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblModeOfTransport" runat="server" Text="Mode of transport"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlModeOfTransport" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" AppendDataBoundItems="true" DataValueField="ID" DataTextField="Name">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="By Air" Value="By Air"></asp:ListItem>
                                    <asp:ListItem Text="By Sea" Value="By Sea"></asp:ListItem>
                                    <asp:ListItem Text="By Road" Value="By Road"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                            </div>
                            <div class="div182Px" style="width: 60%">
                                <ajax:ComboBox ID="ddlDivision" runat="server" AutoCompleteMode="SuggestAppend" 
                                    CssClass="griddrpwidth180px" AppendDataBoundItems="true"
                                    Width="150px" RenderMode="Block" DataValueField="ID" DataTextField="Name" AutoPostBack="True" 
                                    DataSourceID="odsDivision" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblLCOpeningDate" runat="server" Height="14px" Text="LC Opening Date"></asp:Label>
                            </div>
                            <div class="div80Px">                                
                                <asp:TextBox ID="txtLCOpeningDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtLCOpeningDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblShipmentDate" runat="server" Height="14px" Text="Shipment Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtShipmentDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtShipmentDate" PopupButtonID="Image2">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblExpiryDae" runat="server" Height="14px" Text="Expiry Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtExpiryDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate" PopupButtonID="Image3">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                    </div>
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
                    <div id="divGridForPO" runat="server" visible="true">
                        <asp:GridView ID="gvLC" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="Purchase Order Information" Width="100%" Align="left" OnRowDataBound="gvLC_OnRowDataBound">
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
                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("ProductName") %>' Width="100%"></asp:Label>
                                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblTextTotal" Text="TOTAL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Requisition Ref">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRequisitionRef" runat="server" Text='<%# Bind("RequisitionRef") %>' Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" runat="server" Text='<%# Bind("Rate") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlCurrency" runat="server" DataValueField="ID" DataTextField="Currency" Text='<%# Bind("Currency") %>'>
                                            <asp:ListItem Text="BDT" Value="BDT" Selected="True"></asp:ListItem>                                            
                                            <asp:ListItem Text="EURO" Value="EURO"></asp:ListItem>                                            
                                            <asp:ListItem Text="GBP" Value="GBP"></asp:ListItem>
                                            <asp:ListItem Text="USD" Value="USD"></asp:ListItem>
                                            <asp:ListItem Text="YEN" Value="YEN"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="4%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="4%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity","{0:F5}") %>'
                                            OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("Unit","{0:F5}") %>' ReadOnly="true"
                                            onfocus="HideWatermark(this,'0.00')" onblur="ShowWatermark(this,'0.00')" Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value", "{0:F5}") %>' 
                                            ReadOnly ="true" onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 100px"></asp:TextBox>
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
                    <asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />
                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click"
                            ValidationGroup="Save" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" class="btn action_update" ValidationGroup="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                    </div>
                    <asp:ListView ID="lvRequisitionProduct" runat="server">
                        <LayoutTemplate>
                            <table style="border: solid 2px #336699;" cellspacing="0" cellpadding="3" rules="all">
                                <tr style="background-color: #336699; color: White;">
                                    <th>Product Name</th>
                                    <th>Required Quantity</th>
                                    <th>Remarks</th>
                                </tr>
                                <tbody>
                                    <asp:PlaceHolder ID="itemPlaceHolder" runat="server" />
                                </tbody>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("ProductName")%></td>
                                <td><%# Eval("RequiredQuantity")%></td>
                                <td><%# Eval("Remarks")%></td>
                            </tr>
                        </ItemTemplate>
                        <AlternatingItemTemplate>
                            <tr style="background-color: #dadada;">
                                <td><%# Eval("ProductName")%></td>
                                <td><%# Eval("RequiredQuantity")%></td>
                                <td><%# Eval("Remarks")%></td>
                            </tr>
                        </AlternatingItemTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsSupplier" runat="server" SelectMethod="GetAll"
                        TypeName="SetupModule.Provider.SupplierProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsRequisitionRef" runat="server" SelectMethod="GetAll"
                        TypeName="PurchaseModule.Provider.RequisitionProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
