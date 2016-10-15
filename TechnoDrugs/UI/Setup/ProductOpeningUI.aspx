<%@ Page Title="Product Opening Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductOpeningUI.aspx.cs" Inherits="TechnoDrugs.UI.Setup.ProductOpeningUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<%@ Register Src="~/UI/Purchase/ProductSearch.ascx" TagName="UC_ProductSearch" TagPrefix="UCP" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:UpdatePanel ID="updVatDeduction" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Opening Stock Setup
                        </h2>
                    </div>
                    <div style="width: 70%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
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
                    <div style="width: 25%; float: right">
                            <asp:Button ID ="btnCommonProducts" Text ="Common Products" runat ="server" OnClick="btnCommonProducts_Click" />
                        </div>

                </div>
                <div id="Div1" runat="server" style="width: 100%; float: left">
                    <div style="width: 82%; float: left">
                        <UCP:UC_ProductSearch ID="UC_ProductSearch1" runat="server" />
                    </div>
                    <br /><br />
                    <div style="width: 16%; float: right">
                        <asp:Button ID="btnUnit" runat="server" Text="Unit ?" OnClick="btnUnit_Click" Width="100px" Height="30px" />
                    </div>

                </div>

                <br />
                <br />
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div style="width: 45%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtLocation" runat="server" Width="200px"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 45%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblOpeningQuantity" runat="server" Text="Opening Qty :"></asp:Label>
                            </div>
                            <div style="text-align: left; float: left;     height: 15px;    width: 50%;">
                                <asp:TextBox ID="tbOpeningQty" runat="server" Width="200px" Text="0"></asp:TextBox>
                            </div>
                            <div style="text-align:right">
                                <asp:Label ID="lblUnit" runat="server" />
                            </div>
                        </div>
                        <%--<div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label8" runat="server" Text="Opening Amount :"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="tbOpeningAmount" runat="server" Width="200px" Text="0"></asp:TextBox>
                            </div>
                        </div>--%>
                    </div>
                </div>

                <div style="clear: both">
                </div>
                <br />

                <div class="clear">
                    <br />
                </div>
            </div>
            <div class="form_action" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" ValidationGroup="Save" Text="Save" OnClick="btnSave_Click"
                    class="btn action_save" />
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" OnClick="btnRefresh_Click"
                    class="btn action_ref" />
            </div>
            <div>
                <table style="width: 100%;" bgcolor="#EDEDED">
                    <tr>                        
                        <td class="style2">
                            <asp:Label ID="lblSearchType" runat="server" Text="Search Type"></asp:Label>
                        </td>
                        <td class="style3">
                            <asp:DropDownList ID="ddlSearch" runat="server">
                                <asp:ListItem Text="Product Code" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Product Name" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td class="style6">
                            <asp:TextBox ID="txtSearch" runat="server" Width="350px"></asp:TextBox>
                        </td>
                        <td class="style8">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <h4>Opening Product List</h4>
            <asp:GridView ID="lvProductOpening" runat="server" AutoGenerateColumns="False" GridLines="None"
                Width="97%" CssClass="gridTable" AllowPaging="True" PageSize="30"
                        OnPageIndexChanging="lvProductOpening_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <ItemTemplate>
                            <asp:Label ID="lblProductId" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Product">
                        <ItemTemplate>
                            <asp:Label ID="lblProduct" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Location">
                        <ItemTemplate>
                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Opening Qty">
                        <ItemTemplate>
                            <asp:Label ID="lblOpeningQuantity" runat="server" Text='<%# Eval("OpeningQuantity") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:TemplateField HeaderText="Opening Amount">
                        <ItemTemplate>
                            <asp:Label ID="lblOpeningAmount" runat="server" Text='<%# Eval("OpeningAmount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Entry Date">
                        <ItemTemplate>
                            <asp:Label ID="lblEntryDate" runat="server" Text='<%# Eval("EntryDate") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Button ID="btnDelete2" runat="server" Text="Delete" OnClick="btnDelete2_Click"
                                OnClientClick="if(confirm('Are you sure to delete?')) return true; else return false;"
                                Style="width: 50px;" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:HiddenField ID="hfID" runat="server" Value='<%#Eval("ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div>
                <br />
                <asp:HiddenField ID="holdID" runat="server" />
                <asp:HiddenField ID="hfUserID" runat="server" />
                <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                    TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
