<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmpTest.aspx.cs" Inherits="TechnoDrugs.UI.Setup.EmpTest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
    <asp:Label ID="Label1" runat="server" Text="EmpID"></asp:Label>

    </div>
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
    <div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />

    </div>
</asp:Content>
