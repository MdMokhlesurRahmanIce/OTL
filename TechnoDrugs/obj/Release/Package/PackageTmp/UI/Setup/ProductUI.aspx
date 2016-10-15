<%@ Page Title="Product Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductUI.aspx.cs" Inherits="TechnoDrugs.UI.Setup.ProductUI" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updSalesInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Product Setup
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
                                <asp:Label ID="lblCode" runat="server" Height="14px" Text="Code"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblItemTypeID" runat="server" Text="Item Type"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="dllItemTypeID" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AutoPostBack="True" DataSourceID="odsProductType"
                                    AppendDataBoundItems="true" OnSelectedIndexChanged="dllItemTypeID_SelectedIndexChanged">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblName" runat="server" Text="Name"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtName" runat="server" CssClass="txtwidth178px" Enabled="True"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblLocation" runat="server" Text="Location"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtLocation" runat="server" Width="160px"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle" id="DARNo" runat="server">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDARNo" runat="server" Text="D. A. R No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtDARNo" runat="server" CssClass="txtwidth178px" Enabled="True"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div style="width: 33%; float: left">

                        <%--<div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblNatureID" runat="server" Text="Nature"></asp:Label>
                            </div>
                            <div class="div182Px">

                                <asp:DropDownList ID="ddlNatureID" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AutoPostBack="True" AppendDataBoundItems="true">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Inventory Product"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Service Product"></asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>--%>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblDivision" runat="server" Text="Division"></asp:Label>
                            </div>
                            <div class="div182Px">

                                <asp:DropDownList ID="ddlDivision" runat="server" DataTextField="Name" DataValueField="ID" CssClass="griddrpwidth180px" DataSourceID="odsDivision"
                                    AutoPostBack="True" AppendDataBoundItems="true">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblMesurementUnitID" runat="server" Text="Measurement Unit"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlMesurementUnit" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" DataSourceID="odsMesurementUnit"
                                    AppendDataBoundItems="true">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>

                        </div>
                        <div class="lblAndTxtStyle" id="TradePrice" runat="server">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblTradePrice" runat="server" Height="14px" Text="Trade Price(without VAT-DA)"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtTradePrice" runat="server" CssClass="txtwidth178px" MaxLength="100"
                                    Text='0.00000' onfocus="HideWatermark(this,'0.00000')"
                                    onblur="ShowWatermark(this,'0.00000')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>

                            </div>
                        </div>



                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            </div>
                            <div class="div182Px">

                                <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AutoPostBack="True" AppendDataBoundItems="true">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Inctive"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblSafetyStock" runat="server" Height="14px" Text="Safety Stock"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtSafetyStock" runat="server" CssClass="txtwidth178px" MaxLength="100"
                                    Text='0.00000' onfocus="HideWatermark(this,'0.00000')"
                                    onblur="ShowWatermark(this,'0.00000')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>

                            </div>
                        </div>
                        <div class="lblAndTxtStyle" id="Specification" runat="server">
                            <div class="divlblwidth100px">
                                <%--<asp:Label ID="lblPacksizeID" runat="server" Text="Pack size"></asp:Label>--%>
                                <asp:Label ID="lblSpecification" runat="server" Text="Specification"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:TextBox ID="txtSpecification" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>

                            </div>
                        </div>
                        <div class="lblAndTxtStyle" runat="server">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblPacksize" runat="server" Text="Pack size" Visible="false"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:TextBox ID="txtPackSize" runat="server" CssClass="txtwidth178px" MaxLength="100" Visible="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle" runat="server">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblGenericName" runat="server" Text="Generic Name" Visible="false"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:TextBox ID="txtGenericName" runat="server" CssClass="txtwidth178px" MaxLength="100" Visible="false"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle" id="MRP" runat="server">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblMRP" runat="server" Height="14px" Text="MRP(including VAT - DA)"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtMRP" runat="server" CssClass="txtwidth178px" MaxLength="100"
                                    Text='0.00000' onfocus="HideWatermark(this,'0.00000')"
                                    onblur="ShowWatermark(this,'0.00000')" onkeypress="return isNumberKeyAndDot(event,value);"></asp:TextBox>

                            </div>
                        </div>

                    </div>
                </div>
                <div>
                </div>
            </div>
            <asp:HiddenField ID="hfPackSizeGradeSD" runat="server" />
            <div style="clear: both">
            </div>
            <br />
            <div class="form_action" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click"
                    ValidationGroup="Save" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update"
                    OnClick="btnUpdate_Click" />
                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn action_del" OnClick="btnDelete_Click"
                    OnClientClick="if(confirm('Are you sure to delete?')) return true; else return false;" />--%>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn action_ref"
                    OnClick="btnRefresh_Click" />
            </div>
            <div>
                <table style="width: 100%;" bgcolor="#EDEDED">
                    <tr>
                        <td class="style7">Product List
                        </td>
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
                            <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" Text="Search" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <div>
                    <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvProduct_SelectedIndexChanged"
                        GridLines="None" CssClass="gridTable" Width="100%" AllowPaging="True" PageSize="30"
                        DataKeyNames="ID" OnPageIndexChanging="gvProduct_PageIndexChanging">
                        <EmptyDataTemplate>
                            No records found for this search.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblCode" runat="server" Text='<%# Eval("Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("CategoryName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="M-Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusName" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfSpecifiation" runat="server" Value='<%# Eval("Specification") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfMesurementUnitID" runat="server" Value='<%# Eval("MesurementUnitID") %>'></asp:HiddenField>
                                    
                                    <asp:HiddenField ID="hfItemTypeID" runat="server" Value='<%# Eval("ItemTypeID") %>'></asp:HiddenField>
                                    <%--<asp:HiddenField ID="hfPacksizeID" runat="server" Value='<%# Eval("PacksizeID") %>'></asp:HiddenField>--%>
                                    <asp:HiddenField ID="hfDivisionID" runat="server" Value='<%# Eval("DivisionID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfLocation" runat="server" Value='<%# Eval("Location") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfMRP" runat="server" Value='<%# Eval("MRP") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfTradePrice" runat="server" Value='<%# Eval("TradePrice") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfDARNo" runat="server" Value='<%# Eval("DARNo") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfGenericName" runat="server" Value='<%# Eval("GenericName") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfPackSize" runat="server" Value='<%# Eval("PackSize") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfSafetyStock" runat="server" Value='<%#Eval("SafetyStock") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfStatusID" runat="server" Value='<%#Eval("StatusID") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <asp:HiddenField ID="holdID" runat="server" />

            <asp:ObjectDataSource ID="odsProductType" runat="server" SelectMethod="GetAllActive"
                TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsMesurementUnit" runat="server" SelectMethod="GetAllActive"
                TypeName="SetupModule.Provider.MeasurementUnitProvider"></asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsDivision" runat="server" SelectMethod="GetAllDivision"
                TypeName="SetupModule.Provider.ProductTypeProvider"></asp:ObjectDataSource>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
