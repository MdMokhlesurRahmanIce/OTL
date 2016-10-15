<%@ Page Title="Supplier Report" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierReportUI.aspx.cs"
    Inherits="TechnoDrugs.Reports.ReportUI.SupplierReportUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="upSupplierReport" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Supplier Report
                        </h2>
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
                                <asp:Label ID="lblSupplierTypeID" runat="server" Height="14px" Text="Supplier Type"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlSupplierType" runat="server" DataTextField="TypeName" DataValueField="ID"
                                    AutoPostBack="true"
                                    AppendDataBoundItems="true" CssClass="txtwidth178px" MaxLength="100" DataSourceID="odsSupplierType" OnSelectedIndexChanged="ddlSupplierType_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <br />
                            <br />
                            <br />
                            <br />
                            <div style="text-align: right">
                                <asp:Button ID="btnPreview" runat="server" Text="Preview Supp. Info" class="btn action_save"
                                    OnClick="btnPreview_Click" />
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="div182Px">
                            <ajax:ComboBox ID="ddlSupplier" runat="server" AutoCompleteMode="SuggestAppend" CssClass="drpwidth99per"
                                AppendDataBoundItems="true" Width="96%"
                                RenderMode="Block" DataValueField="ID" DataTextField="Name" AutoPostBack="True">
                                <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                            </ajax:ComboBox>
                        </div>
                        <br />
                        <br />
                        <br />
                        <br />
                        <div style="text-align: left">
                            <asp:Button ID="btnPriviewSuppProdInfo" runat="server" Text="Preview Supp. Product Info" class="btn action_save" OnClick="btnPriviewSuppProdInfo_Click" />
                        </div>
                    </div>

                </div>

                <div style="clear: both"></div>


            </div>
            <br />
            <asp:ObjectDataSource ID="odsSupplierType" runat="server" SelectMethod="GetSupplierType"
                TypeName="SetupModule.Provider.SupplierProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
