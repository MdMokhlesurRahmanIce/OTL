<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ProductPackSizeUI.aspx.cs" Inherits="TechnoDrugs.UI.Setup.ProductPackSizeUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updSalesInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>Pack Size Setup
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
                                <asp:Label ID="lblPackType" runat="server" Text="Pack Type"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlPackType" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0" Text="--------Select--------"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Cartoon"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Insert"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="Label"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblName" runat="server" Height="14px" Text="Pack Name"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtName" runat="server" Width="100%" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblQuantity" runat="server" Height="14px" Text="Quantity"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtQuantity" runat="server" Width="60%" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">

                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblHeight" runat="server" Height="14px" Text="Height"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtHeight" runat="server" Width="60%" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>

                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblLength" runat="server" Height="14px" Text="Length"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtLength" runat="server" Width="60%" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="width: 33%; float: left">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblWidth" runat="server" Height="14px" Text="Width"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtWidth" runat="server" Width="60%" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
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
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                            </div>
                            <div class="div182Px">
                                <asp:DropDownList ID="ddlStatus" runat="server" DataTextField="Name" DataValueField="ID"
                                    CssClass="griddrpwidth180px" AppendDataBoundItems="true">
                                    <asp:ListItem Value="0" Text="--------Select--------"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Inactive"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div style="clear: both">
            </div>
            <br />
            <div class="form_action" style="text-align: center">
                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" OnClick="btnSave_Click" ValidationGroup="Save" />
                <asp:Button ID="btnUpdate" runat="server" Text="Update" class="btn action_update"
                    OnClick="btnUpdate_Click" />
                <%--<asp:Button ID="btnDelete" runat="server" Text="Delete" class="btn action_del" OnClick="btnDelete_Click"  OnClientClick="if(confirm('Are you sure to delete?')) return true; else return false;"/>--%>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" class="btn action_ref"
                    OnClick="btnRefresh_Click" />
            </div>
            <h4>Product Pack Size List
            </h4>
            <div>
                <div>
                    <asp:GridView ID="gvProductPackSize" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvProductPackSize_SelectedIndexChanged"
                        GridLines="None" CssClass="gridTable" Width="95%">
                        <EmptyDataTemplate>
                            no data retrived
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="Pack Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Text='<%# Eval("PackName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pack Type">
                                <ItemTemplate>
                                    <asp:Label ID="IblPackTypeName" runat="server" Text='<%#Eval("PackTypeName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Height">
                                <ItemTemplate>
                                    <asp:Label ID="lblHeight" runat="server" Text='<%#Eval("Height") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Width">
                                <ItemTemplate>
                                    <asp:Label ID="lblWidth" runat="server" Text='<%#Eval("Width") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Length">
                                <ItemTemplate>
                                    <asp:Label ID="lblLength" runat="server" Text='<%#Eval("Length") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusName" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--HiddenField--%>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfStatusID" runat="server" Value='<%# Eval("StatusID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfMesurementUnitID" runat="server" Value='<%#Eval("MeasurementUnitID") %>'></asp:HiddenField>

                                    <asp:HiddenField ID="hfPackTypeID" runat="server" Value='<%# Eval("PackTypeID") %>'></asp:HiddenField>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <br />
            <asp:HiddenField ID="holdID" runat="server" />
            <asp:ObjectDataSource ID="odsMesurementUnit" runat="server" SelectMethod="GetAllActive"
                TypeName="SetupModule.Provider.MeasurementUnitProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
