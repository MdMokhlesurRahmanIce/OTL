<%@ Page Title="BOE Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BOEInfoUI.aspx.cs" Inherits="TechnoDrugs.UI.Purchase.BOEInfoUI" %>

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
                        <h2>BOE Information</h2>
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
                                <asp:Label ID="lblBOENumber" runat="server" Height="14px" Text="BOE Number"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtBOENumber" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRefLCNumber" runat="server" Height="14px" Text="Ref. LC Number"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtRefLCNumber" runat="server" CssClass="txtwidth178px" ReadOnly="true"
                                    MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblBOEDate" runat="server" Height="14px" Text="BOE Date"></asp:Label>
                            </div>
                            <div class="div80Px">                              
                                <asp:TextBox ID="txtBOEDate" runat="server" Enabled="false" Width="120px"/>
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBOEDate" PopupButtonID="Image1">
                                </ajax:CalendarExtender>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblRefLCDate" runat="server" Height="14px" Text="Ref. LC Date"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtRefLCDate" runat="server" Enabled="false"   Width="120px"/>
                                <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/calendar-schedulehs.png" />
                                <ajax:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtRefLCDate" PopupButtonID="Image3" Enabled="false">
                                </ajax:CalendarExtender>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblPreviousBOE" runat="server" Height="14px" Text="Previous BOE"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlPreviousBOE" runat="server" AppendDataBoundItems="true" AutoPostBack="true" DataTextField="BOENumber"
                                    DataValueField="ID" OnSelectedIndexChanged="ddlPreviousBOE_SelectedIndexChanged">                                   
                                </asp:DropDownList>
                            </div>
                        </div>                        
                    </div>
                    <div style="width: 33%; float: left; height: auto">                        
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblExcRate" runat="server" Height="14px" Text="Exc. Rate"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtExcRate" runat="server" width="80%"  MaxLength="100"></asp:TextBox>Taka
                            </div>
                        </div> 
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Button ID ="btnNewBOE" runat ="server" Text ="New BOE" OnClick="btnNewBOE_Click"/>
                            </div>                            
                        </div>                      
                    </div>
                    <br />
                    <br />
                    <div style="clear: both">
                    </div>
                    <br />
                    <div id="divGridForPO" runat="server" visible="true">
                        <asp:GridView ID="gvPurchaseOrder" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="BOE Information" Width="100%" Align="left" OnRowDataBound="gvPurchaseOrder_OnRowDataBound">
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="HS Code">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtHSCode" runat="server" Text='<%# Bind("HSCode") %>' Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRate" runat="server" Text='<%# Bind("Rate") %>' Width="90%" onfocus="HideWatermark(this,'0')"
                                            ReadOnly="true" onblur="ShowWatermark(this,'0')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Actual Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtActualQuantity" runat="server" Text='<%# Bind("ActualQuantity","{0:F5}") %>'
                                            ReadOnly="true" onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Invoice Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInvoiceQuantity" runat="server" Text='<%# Bind("InvoiceQuantity","{0:F5}") %>'
                                            OnTextChanged="txtInvoiceQuantity_TextChanged" AutoPostBack="true" onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Remaining Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemainingQuantity" runat="server" Text='<%# Bind("RemainingQuantity","{0:F5}") %>'
                                            onfocus="HideWatermark(this,'0.00')" ReadOnly="true"
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

                                <asp:TemplateField HeaderText="Invoice Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInvoiceValue" runat="server" Text='<%# Bind("InvoiceValue", "{0:F5}") %>'
                                            ReadOnly="true" onfocus="HideWatermark(this,'0.00')"
                                            onblur="ShowWatermark(this,'0.00')" onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>                                
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div id="div2" runat="server" visible="true">
                        <asp:GridView ID="gvTaxInformation" runat="server" Font-Size="Small" AutoGenerateColumns="False" ShowFooter="True" CssClass="mGrid"
                            Caption="TAX Information" Width="100%" Align="left" OnRowDataBound="gvTaxInformation_OnRowDataBound" EditRowStyle-BorderColor="Yellow" BorderColor="YellowGreen">
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
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Assessment Value">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAssessmentValue" runat="server" Text='<%# Bind("AssessmentValue") %>' Width="90%"
                                            onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="CD(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtCDPerc" runat="server" Text='<%# Bind("CDPerc") %>'
                                            Width="90%" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                        <asp:TextBox ID="txtCDAmt" runat="server" Text='<%# Bind("CDAmt") %>' Width="90%"
                                            onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="VAT(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVATPerc" runat="server" Text='<%# Bind("VATPerc") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                        <asp:TextBox ID="txtVATAmt" runat="server" Text='<%# Bind("VATAmt") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="AIT(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAITPerc" runat="server" Text='<%# Bind("AITPerc") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                        <asp:TextBox ID="txtAITAmt" runat="server" Text='<%# Bind("AITAmt") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ATV(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtATVPerc" runat="server" Text='<%# Bind("ATVPerc") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                        <asp:TextBox ID="txtATVAmt" runat="server" Text='<%# Bind("ATVAmt") %>'
                                            onkeypress="return isNumberKeyAndDot(event,value);"
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SD(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSDPerc" runat="server" Text='<%# Bind("SDPerc") %>'
                                            Width="90%"></asp:TextBox>
                                        <asp:TextBox ID="txtSDAmt" runat="server" Text='<%# Bind("SDAmt") %>'
                                            Width="90%"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="RD(%/Amount)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRDPerc" runat="server" Text='<%# Bind("RDPerc") %>' onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 100px"></asp:TextBox>
                                        <asp:TextBox ID="txtRDAmt" runat="server" Text='<%# Bind("RDAmt") %>' onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DF/CVAT/FP">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDFCVATFPAmt" runat="server" Text='<%# Bind("DFCVATFPAmt") %>' onkeypress="return isNumberKeyAndDot(event,value);"
                                            Style="width: 100px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
