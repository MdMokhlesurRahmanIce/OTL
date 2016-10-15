<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckIndividualPresentStockUI.aspx.cs"
    Inherits="TechnoDrugs.UI.Inventory.CheckIndividualPresentStockUI" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>--%>
<%@ Register Src="~/UI/Purchase/ProductSearch.ascx" TagName="UC_ProductSearch" TagPrefix="UCP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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
    <asp:UpdatePanel ID="updPurchase" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 40%; float: left">
                        <h2>Product Individual Stock and Requisition Information</h2>
                    </div>
                    <div style="width: 60%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div style="width: 45%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="91%" CssClass="drpwidth99per"
                                    DataValueField="ID" DataTextField="Name"
                                    AutoPostBack="True" DataSourceID="odsDivision" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>

                    </div>

                </div>
                <div id="Div1" runat="server" style="width: 100%; float: left">
                    <div style="width: 82%; float: left">
                        <UCP:UC_ProductSearch ID="UC_ProductSearch1" runat="server" />
                    </div>
                    <br />
                    <br />

                </div>
                <div class="form-details">
                </div>
                <div style="clear: both; text-align: center;">
                    <asp:Button ID="btnShowStock" runat="server" Text="Show Stock" class="btn action_save" OnClick="btnShowStock_Click" />
                </div>
                <div style="clear: both; text-align: center;">
                    <asp:Button ID="btnShowReqInfo" runat="server" Text="Show Req. Info" class="btn action_save" OnClick="btnShowReqInfo_Click" />
                </div>
            </div>
            <div class="lblAndTxtStyle">
                <div class="divlblwidth100px" style="width: 10%; text-align: right">
                    <asp:Label ID="lblProductName" runat="server" Text="Product Name :" Height="14px"></asp:Label>

                </div>
                <div class="div182Px" style="width: 50%">
                    <asp:Label ID="lblProductNameDisplay" runat="server"></asp:Label>
                </div>
                <div class="div182Px" style="width: 15%">
                    <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                </div>
                <div class="div182Px" style="width: 15%">
                    <asp:Label ID="lblUnit" runat="server"></asp:Label>
                </div>
            </div>
            <div>
            </div>
            <br />

            <div id="divGridForLSE" runat="server" visible="true">
                        <asp:GridView ID="gvReqPOInfo" runat="server" Font-Size="Small" AutoGenerateColumns="False" 
                            ShowFooter="True" CssClass="mGrid"
                            Width="100%" Align="left">
                            <Columns>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSerial" runat="server" Text='<%# Container.DataItemIndex+1 %>'
                                            Width="100%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="3%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="3%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Requisition Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPresentStock" runat="server" Text='<%# Bind("RequisitionDate") %>' ReadOnly="true" Width="90%" ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    <%--<FooterTemplate>
                                        <asp:Label runat="server" ID="lblSumTotal" Font-Bold="true" Text='<%# GetTotalQuantity() %>'></asp:Label>
                                    </FooterTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Reference No">
                                    <ItemTemplate>
                                        <asp:Label ID="txtReferenceNo" runat="server" Text='<%# Bind("ReferenceNo") %>' Width="90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                    
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Required Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtUnit" runat="server" Text='<%# Bind("RequiredQuantity") %>' Width="90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />                                    
                                </asp:TemplateField>                                
                                <asp:TemplateField HeaderText="Purchase Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPurchaseOrderDate" runat="server" Text='<%# Bind("PurchaseOrderDate") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="P Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPOrderNo" runat="server" Text='<%# Bind("POrderNo") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQuantity" runat="server" Text='<%# Bind("Quantity") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LC Opening Date">
                                    <ItemTemplate>
                                        <asp:Label ID="txtLCOpeningDate" runat="server" Text='<%# Bind("LCOpeningDate") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank LC Number">
                                    <ItemTemplate>
                                        <asp:Label ID="txtBankLCNumber" runat="server" Text='<%# Bind("BankLCNumber") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LC Quantity">
                                    <ItemTemplate>
                                        <asp:Label ID="txtLCQuantity" runat="server" Text='<%# Bind("LCQuantity") %>'
                                            Style="width: 90%"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="7%" />
                                    <HeaderStyle HorizontalAlign="Left" Width="7%" />
                                </asp:TemplateField>
                                
                            </Columns>
                        </asp:GridView>
                    </div>




            <br />
            <br />
            <br />

            <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
