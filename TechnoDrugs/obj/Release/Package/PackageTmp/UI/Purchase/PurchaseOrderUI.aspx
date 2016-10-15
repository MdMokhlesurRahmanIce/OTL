<%@ Page Title="Purchase Order" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderUI.aspx.cs" Inherits="TechnoDrugs.UI.Purchase.PurchaseOrderUI" %>

<%@ Register Src="~/UI/Purchase/ProductSearch.ascx" TagName="UC_ProductSearch" TagPrefix="UCP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">    
    <link href="../../Style/CSS.css" rel="stylesheet" />
    <script src="../../Scripts/jquery-1.3.2.min.js"></script>
    <script src="../../Scripts/jquery.blockUI.js"></script>

    <script type = "text/javascript">

        function BlockUI(elementID) {

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_beginRequest(function () {
                $("#" + elementID).block({
                    message: '<table align = "center"><tr><td>' +
             '<img src="../../Images/loadingAnim.gif" /></td></tr></table>',
                    css: {},
                    overlayCSS: {
                        backgroundColor: '#000000', opacity: 0.6
                    }
                });
            });
            prm.add_endRequest(function () {
                $("#" + elementID).unblock();
            });

        }
        $(document).ready(function () {

            BlockUI("<%=pnlAddEdit.ClientID %>");
            $.blockUI.defaults.css = {};
        });

        function Hidepopup() {

            $find("popup1").hide();
            return false;
        }

        function mdlsSearch(searchval) {

            var gridLenght = $('#MainContent_GridView2 tbody tr').length;
            var labeltotal = 0;
            for (var i = 1; i < gridLenght; i++) {

                var trgrid=$('#MainContent_GridView2 tbody tr:eq('+i+')').find('td:eq(1)').text();
                if (trgrid.indexOf(searchval)>0)
                {
                    $('#MainContent_GridView2 tbody tr:eq(' + i + ')').show();
                }

                else if (trgrid.indexOf(searchval)<0)
                {
                    $('#MainContent_GridView2 tbody tr:eq('+i+')').hide();
                }
                //"PRAW20140056"
                //alert(gridLenght+">>>"+trgrid);
            }
        }
    </script> 

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
        function ShowWatermark(txtQty, txtRate) {
            var gridLenght = $('#MainContent_gvPurchaseOrder tbody tr').length - 2;
            var labeltotal = 0;
            for (var i = 0; i < gridLenght; i++) {
                var tRate = "MainContent_gvPurchaseOrder_txtRate_" + i;
                var tQuantity = "MainContent_gvPurchaseOrder_txtQuantity_" + i;
                var tValue = "MainContent_gvPurchaseOrder_txtValue_" + i;

                var vRate = $('#' + tRate).val();
                var vQuantity = $('#' + tQuantity).val();
                var totalValue = parseFloat(vRate) * parseFloat(vQuantity);
                $('#' + tValue).val(parseFloat(totalValue).toFixed(2));
                labeltotal = parseFloat(labeltotal + totalValue);
                $('#MainContent_gvPurchaseOrder_lblSumTotal').text(labeltotal.toFixed(2));
                //alert(tRate + "ggggg" + tQuantity + "" + totalValue);
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
                        <h2>Purchase Order</h2>
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
                                <asp:Label ID="lblPOrderNo" runat="server" Height="14px" Text="P Order No:"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtPOrderNo" runat="server" ReadOnly="true" CssClass="txtwidth178px "
                                    MaxLength="100"></asp:TextBox>
                            </div>
                            <div style="width: 4%; float: right">
                                <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/Images/Search20X20.png"
                                    ToolTip="Search" OnClick="btnFind_Click" />
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRequisitionRef" runat="server" Text="Requisition Ref."></asp:Label>
                            </div>
                            <div class="div182Px">
                                <%--<asp:DropDownList ID="ddlRequistionRef" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" DataSourceID="odsRequisitionRef" AppendDataBoundItems="true" DataValueField="ID"
                                    DataTextField="ReqReferenceNo" OnSelectedIndexChanged="ddlRequistionRef_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>--%>
                                <asp:DropDownList ID="ddlRequistionRef" runat="server" Width="91%" CssClass="drpwidth99per"
                                    AutoPostBack="True" DataValueField="ID" DataTextField="ReferenceNo" OnSelectedIndexChanged="ddlRequistionRef_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <asp:Button ID="btnCommonProducts" Text="Common Products" runat="server" OnClick="btnCommonProducts_Click" />
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblSupplier" runat="server" Text="Supplier"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <ajax:ComboBox ID="ddlSupplier" runat="server" AutoCompleteMode="SuggestAppend" CssClass="drpwidth99per" AppendDataBoundItems="true" Width="96%"
                                    RenderMode="Block" DataValueField="SupplierID" DataTextField="SupplierName" AutoPostBack="True"
                                    DataSourceID="odsSupplier">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </ajax:ComboBox>
                            </div>
                        </div>
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

                    </div>
                    <div style="width: 33%; float: left">
                        <%--<div class="lblAndTxtStyle">--%>
                        <div style="float: left;height: auto;margin-top: 0;padding: 3px 0;width: 109%;">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblPurchaseOrderDate" runat="server" Height="14px" Text="P Order Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtPurchaseOrderDate" runat="server" Enabled="false" Width="120px" />
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPurchaseOrderDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <%--<div class="lblAndTxtStyle">--%>
                        <div style="float: left;height: auto;margin-top: 0;padding: 3px 0;width: 109%;">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblIntDeliveryDate" runat="server" Height="14px" Text="Appx. Delivery Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtIntDeliveryDate" runat="server" Enabled="false" Width="120px"/>
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtIntDeliveryDate" PopupButtonID="Image2">
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
                    <div style="width: 25%; height: 10px; float: left; text-align: left">
                        <br />
                        <asp:CheckBoxList ID="ckbVATTAXMessage" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Text="15% VAT" Value="1"></asp:ListItem>
                            <asp:ListItem Text="TAX" Value="2"></asp:ListItem>
                        </asp:CheckBoxList>
                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <div id="divGridForPO" runat="server" visible="true">
                        <asp:GridView ID="gvPurchaseOrder" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="Purchase Order Information" Width="100%" Align="left" OnRowDataBound="gvPurchaseOrder_OnRowDataBound">
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" runat="server" Text='<%# Bind("Rate") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <%--OnTextChanged="txtRate_TextChanged" AutoPostBack="true" --%>
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity","{0:F5}") %>'
                                            onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <%--OnTextChanged="txtQuantity_TextChanged" AutoPostBack="true" --%>
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
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblTotal" Font-Bold="true" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtValue" runat="server" Text='<%# Bind("Value", "{0:F5}") %>' onfocus="HideWatermark(this,'0.00')"
                                            ReadOnly="true" onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 75%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="5%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="5%" />
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalAmount() %>'></asp:Label>
                                    </FooterTemplate>
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
                    <asp:HiddenField id="hfDoubleSaveBtn" runat="server" Value=""/>
                    <div class="form_action" style="text-align: center">
                        <%--<div id="imagesh" runat="server" visible="false">
                            <img src="../../Images/loading.gif" />
                        </div>--%>
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click"
                            ValidationGroup="Save"/>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Visible="false" class="btn action_update" ValidationGroup="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                        <%--<asp:Button ID="btnPrint" runat="server" class="btn action_del" Text="Print" OnClick="btnPrint_Click" />--%>
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" class="btn action_save" OnClick="btnPreview_Click" />
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


 <br />
    <br />

<asp:Panel ID="pnlAddEdit" runat="server" CssClass="modalPopup" style = "display:none">
<div>
    <asp:Label ID="mdlSearch" runat="server" Text="Search By Id :"></asp:Label>
   <asp:TextBox ID="txtMdlSearch" runat="server" onblur="mdlsSearch(this.value)"></asp:TextBox>
  <%--<asp:TextBox ID="txtMdlSearch" runat="server"></asp:TextBox>--%>

</div>
    <br>
    </br>
    <br>
    </br>
    <div id="dvMdlShwo" runat="server" style="height:350px; overflow:auto">
<asp:GridView ID="GridView2" runat="server"  Width = "550px"
AutoGenerateColumns = "false"  AlternatingRowStyle-BackColor = "#C2D69B"  
HeaderStyle-BackColor = "green"  
>
<Columns>
    <asp:TemplateField ItemStyle-Width = "30px"  HeaderText = "">
   <ItemTemplate>
       <asp:LinkButton ID="lnkSelect" runat="server" Text = "Select" OnClick = "Select"></asp:LinkButton>
   </ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField = "PurchaseOrderNo" HeaderText = "Purchase Order No" HtmlEncode = "true" />
<asp:BoundField DataField = "PurchaseOrderDate" HeaderText = "Purchase Order Date"  HtmlEncode = "true" />
<asp:BoundField DataField = "SupplierName" HeaderText = "Supplier Name"  HtmlEncode = "true"/> 

</Columns> 
<AlternatingRowStyle BackColor="#C2D69B"  />
</asp:GridView> 
      </div>



<asp:Label Font-Bold = "true" ID = "lblNoDataFound" runat = "server" Text = "" ></asp:Label>
<br />
<table align = "center">
    

<tr>

<td>
<asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick = "return Hidepopup()"/>
</td>
</tr>
</table>
</asp:Panel>

<asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>

<cc1:ModalPopupExtender ID="popup1" runat="server" DropShadow="false"
PopupControlID="pnlAddEdit" TargetControlID = "lnkFake"
BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
