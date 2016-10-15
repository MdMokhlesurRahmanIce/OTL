<%@ Page Title="Delivery Challan Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DeliveryChallanUI.aspx.cs" Inherits="TechnoDrugs.UI.Purchase.DeliveryChallanUI" %>

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
                        <h2>Delivery Challan (Local/LC)</h2>
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
                                <asp:Label ID="lblDeliveryChallanNo" runat="server" Height="14px" Text="Delivery Challan No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtDeliveryChallanNo" runat="server" ReadOnly="true" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: left">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblVehicleInfo" runat="server" Height="14px" Text="Vehicle Info"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtVehicleInfo" runat="server" Width="162px" Height="23px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <asp:Button ID ="btnCommonProducts" Text ="Common Products" runat ="server" OnClick="btnCommonProducts_Click" />
                        </div>
                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblProductDivision" runat="server" Text="Product Division"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlProductDivision" runat="server" Width="91%" CssClass="drpwidth99per"
                                    DataValueField="ID" DataTextField="Name"
                                    AutoPostBack="True" DataSourceID="odsDivision" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProductDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>                        
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label2" runat="server" Text="Challan Type"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlChallanType" runat="server" Width="91%" CssClass="drpwidth99per">
                                    <asp:ListItem Text="Local" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="LC" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDeliveryChallanDate" runat="server" Height="14px" Text="Delivery Challan Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <%--<asp:TextBox ID="txtDeliveryChallanDate" runat="server" CssClass="txtwidth178px DatepickerInput"
                                    MaxLength="100"></asp:TextBox>--%>

                                <asp:TextBox ID="txtDeliveryChallanDate" runat="server" Enabled="false" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDeliveryChallanDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label1" runat="server" Height="14px" Text="Select Option:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:CheckBoxList ID="ckbOption" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Send" Value="1" onclick="MutExChkList(this);"></asp:ListItem>
                                    <asp:ListItem Text="Received" Value="2" onclick="MutExChkList(this);"></asp:ListItem>
                                </asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div style="clear: both"></div>
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
                        <%--OnRowDataBound="gvDeliveryChallan_OnRowDataBound"--%>
                        <asp:GridView ID="gvDeliveryChallan" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="Purchase Order Information" Width="100%" Align="left" >
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Sl. No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# Container.DataItemIndex+1 %>'
                                            Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="2%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Name of the Product">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProduct" runat="server" Text='<%# Bind("ProductName") %>' Width="90%"></asp:Label>
                                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%# Eval("ProductID") %>'></asp:HiddenField>
                                    </ItemTemplate>
                                    <ItemStyle Width="12%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="12%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblTextTotal" Text="TOTAL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>                              

                                <asp:TemplateField HeaderText="LC/P. Order No">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlPurchaseOrderNo" runat="server" Width="100%" CssClass="drpwidth99per"
                                           DataSourceID="odsPurchaseOrderNo" SelectedValue='<%# Bind("POrderNo") %>'  AppendDataBoundItems="true" 
                                            DataValueField="POrderNo"
                                            DataTextField="POrderNo">
                                            <asp:ListItem Text="-----No PO Issued-----" Value="" Selected="True"></asp:ListItem>
                                        </asp:DropDownList>
                                        <%-- <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" CssClass="drpwidth99per"
                                          SelectedValue='<%# DataBinder.Eval(Container.DataItem, "POrderNo")%>' AppendDataBoundItems="true"
                                            DataValueField="POrderNo" DataTextField="POrderNo">       
                                            <asp:ListItem Text="-----No PO Issued-----" Value="-----No PO Issued-----" Selected="True"></asp:ListItem>                                     
                                        </asp:DropDownList>--%>


                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Supplier Name">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" Width="100%" CssClass="drpwidth99per"
                                            SelectedValue='<%# Bind("SupplierName") %>' DataSourceID="odsSupplier" AppendDataBoundItems="true"
                                            DataValueField="SupplierID"
                                            DataTextField="SupplierName">
                                            <asp:ListItem Text="-----Select-----" Value=""></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Challan No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSupplierChallanNo" runat="server" Text='<%# Bind("SupplierChallanNo") %>' Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Challan Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSupplierChallanDate" runat="server" Text='<%# Bind("SupplierChallanDate") %>' Width="90%" Enabled="false" />
                                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                        <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtSupplierChallanDate" 
                                          PopupButtonID="Image2">
                                        </ajax:CalendarExtender>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Provided Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtProvidedQuantity" runat="server" Text='<%# Bind("ProvidedQuantity","{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtUnit" runat="server" Text='<%# Bind("MeasurementUnitName") %>' ReadOnly="true"
                                           Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                </asp:TemplateField>                                                          
                                
                                <asp:TemplateField HeaderText="Received Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtReceivedQuantity" runat="server" Text='<%# Bind("ReceivedQuantity", "{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDeleteSelectedRowLSE" runat="server" ImageUrl="../../images/CancelIco.png" Width="40%"
                                            OnClick="btnDeleteSelectedRowLSE_Click" ToolTip="Delete Row" />
                                    </ItemTemplate>
                                    <ItemStyle Width="2%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="2%" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>
                    </div>
                    <div style="clear: both">
                    </div>
                  <%--  <asp:HiddenField ID="hfProductID" runat="server" />
                    <asp:HiddenField ID="hfProductName" runat="server" />--%>
                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click"
                            ValidationGroup="Save" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" class="btn action_update" ValidationGroup="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnRefresh_Click" />
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save" OnClick="btnPreview_Click" />
                    </div>
                    <asp:ObjectDataSource ID="odsSupplier" runat="server" SelectMethod="GetAll"
                        TypeName="SetupModule.Provider.SupplierProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsPurchaseOrderNo" runat="server" SelectMethod="GetDivisioinWisePONo"
                        TypeName="PurchaseModule.Provider.PurchaseOrderProvider"></asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                        TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
                    <%--<asp:ObjectDataSource ID="odsRequisitionRef" runat="server" SelectMethod="GetAll"
                        TypeName="PurchaseModule.Provider.RequisitionProvider"></asp:ObjectDataSource>--%>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
