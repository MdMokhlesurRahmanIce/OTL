<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CommonReportViewer.aspx.cs" Inherits="TechnoDrugs.Reports.ReportUI.CommonReportViewer" %>
<%@ Register Assembly ="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <CR:CrystalReportViewer ID="uniVatCrystalReport" runat="server" 
        AutoDataBind="true" ToolPanelView="None"  />
         
    
</asp:Content>
