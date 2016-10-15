<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProductSearch.ascx.cs" Inherits="TechnoDrugs.UI.Purchase.ProductSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<div>
    <div style="width: 100%; float: left">
        <fieldset>
            <legend style="font-weight: bold">Search Product</legend>
            <div style="width: 20%; float: left">
                <fieldset>
                    <legend>Started With</legend>
                    <asp:RadioButtonList ID="rbProductCodeName" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rbProductCodeName_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Code" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Name"></asp:ListItem>
                    </asp:RadioButtonList>
                </fieldset>
            </div>
            <div style="width: 80%; float: left">
                <fieldset>
                    <legend>Type</legend>
                    <asp:RadioButtonList ID="rbProductType" runat="server" RepeatColumns="7" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rbProductType_OnSelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="1" Text="Raw & Excipients" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Finished"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Packing"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Engineering"></asp:ListItem>
                        <asp:ListItem Value="5" Text="QA/QC"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Admin"></asp:ListItem>
                        <asp:ListItem Value="7" Text="Production"></asp:ListItem>
                    </asp:RadioButtonList>
                </fieldset>
            </div>
            <div style="width: 80%; float: left">
                <div class="lblAndTxtStyle">
                    <div class="divlblwidth100px" style="width: 37%; text-align: right">
                        <asp:Label ID="Label4" runat="server" Text="Product/Material"></asp:Label>
                    </div>
                    <div class="div182Px" style="width: 60%">
                        <ajax:ComboBox ID="ddlProduct" runat="server" AutoCompleteMode="SuggestAppend" CssClass="griddrpwidth180px"
                            Width="350px" RenderMode="Block">
                        </ajax:ComboBox>
                    </div>
                </div>
            </div>
        </fieldset>
    </div>
</div>
