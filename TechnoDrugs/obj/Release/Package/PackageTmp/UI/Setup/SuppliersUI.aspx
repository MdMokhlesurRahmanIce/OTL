<%@ Page Title="Supplier Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SuppliersUI.aspx.cs" Inherits="TechnoDrugs.UI.Setup.SuppliersUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .style2 {
            width: 103px;
            text-align: left;
        }

        .style3 {
            width: 100px;
            text-align: left;
        }

        .style6 {
            width: 130px;
        }

        .style7 {
            font-size: large;
            width: 199px;
        }

        .style8 {
            text-align: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updPurchase" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Suppliers Information</h2>
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
                                <asp:Label ID="Label1" runat="server" Height="14px" Text="Supplier ID"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtSupplierID" CssClass="txtwidth178px" MaxLength="100" runat="server"
                                    Enabled="false"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblName" runat="server" Height="14px" Text="Supplier Name"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtName" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblCode" runat="server" Height="14px" Text="TIN Number"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtTINNumber" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblTypeID" runat="server" Height="14px" Text="Type"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlSupplierType" runat="server" DataTextField="TypeName" DataValueField="ID"
                                    AppendDataBoundItems="true" CssClass="txtwidth178px" MaxLength="100" DataSourceID="odsSupplierType">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblContactName" runat="server" Height="14px" Text="Contact Name"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtContactName" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblVatRegNo" runat="server" Height="14px" Text="VAT Reg. No"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtVatRegNo" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblAddress" runat="server" Height="14px" Text="Address"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtAddress" CssClass="txtwidth178px" MaxLength="30" runat="server"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="clear: both">
                        </div>
                        <br />
                        <br />
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblCountryID" runat="server" Height="14px" Text="Country"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtCountry" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblPhone" runat="server" Height="14px" Text="Phone"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtPhone" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px" style="text-align: center">
                                <asp:Label ID="lblEmail" runat="server" Height="14px" Text="Email"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtEmail" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px" style="text-align: center">
                                <asp:Label ID="lblMobile" runat="server" Height="14px" Text="Mobile"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtMobile" CssClass="txtwidth178px" MaxLength="100" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px" style="text-align: center">
                                <asp:Label ID="lblNote" runat="server" Height="14px" Text="Note"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtNote" runat="server" CssClass="txtwidth178px" MaxLength="100"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div style="clear: both"></div>
                        <br />
                        <br />
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px" style="text-align: center">
                                <asp:Label ID="lblStatus" runat="server" Height="14px" Text="Status"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="txtwidth178px" MaxLength="100">
                                    <asp:ListItem Text="----------Select----------" Value="0" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Active" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Inactive" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <div class="form_action" style="text-align: center">
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" ValidationGroup="Save"
                            OnClick="btnSave_Click" />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update"
                            OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Refresh" class="btn action_ref" OnClick="btnClear_Click" />
                    </div>
                    <br />
                    <div>
                        <table style="width: 100%;" bgcolor="#EDEDED">
                            <tr>
                                <td class="style7">Supplier List
                                </td>
                                <td class="style2">
                                    <asp:Label ID="lblSearchType" runat="server" Text="Search Type"></asp:Label>
                                </td>
                                <td class="style3">
                                    <asp:DropDownList ID="ddlSearch" runat="server">
                                        <asp:ListItem Text="Supplier Name" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Contact Person" Value="2"></asp:ListItem>
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
                    <%--<div>
                        <table style="width: 100%;" bgcolor="#EDEDED">
                            <tr>
                                <td class="style7">Supplier List
                                </td>
                            </tr>
                        </table>
                    </div>--%>
                    <asp:GridView ID="gvSuppliers" runat="server" AutoGenerateColumns="False" Width="97%"
                        CssClass="gridTable" OnSelectedIndexChanged="gvSuppliers_SelectedIndexChanged"
                        GridLines="None" AllowPaging="True" PageSize="30"
                        OnPageIndexChanging="gvSuppliers_PageIndexChanging">



                        <EmptyDataTemplate>
                            No records found for this search.
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="ID">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("SupplierID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("SupplierName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact Person">
                                <ItemTemplate>
                                    <asp:Label ID="lblContactPerson" runat="server" Text='<%# Eval("ContactPerson") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Eval("Address") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:Label ID="lblMobile" runat="server" Text='<%# Eval("Mobile") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--HiddenField--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("SupplierID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfTypeID" runat="server" Value='<%# Eval("TypeID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfCountryName" runat="server" Value='<%# Eval("CountryName") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfContactName" runat="server" Value='<%# Eval("ContactPerson") %>'></asp:HiddenField>

                                    <asp:HiddenField ID="hfPhone" runat="server" Value='<%# Eval("Phone") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfEmail" runat="server" Value='<%# Eval("Email") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfNote" runat="server" Value='<%#Eval("Note") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfTarriffID" runat="server" Value='<%#Eval("TarriffID") %>' />
                                    <asp:HiddenField ID="hfStatusID" runat="server" Value='<%#Eval("StatusID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="holdID" runat="server" />
                    <asp:ObjectDataSource ID="odsSupplierType" runat="server" SelectMethod="GetSupplierType"
                        TypeName="SetupModule.Provider.SupplierProvider"></asp:ObjectDataSource>
                </div>
                <div style="clear: both">
                </div>
                <br />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
