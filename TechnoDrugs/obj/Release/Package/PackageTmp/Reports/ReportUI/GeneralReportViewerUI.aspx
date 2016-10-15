<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="GeneralReportViewerUI.aspx.cs" Inherits="TechnoDrugs.Reports.ReportUI.GeneralReportViewerUI" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <CR:CrystalReportViewer ID="technoCrystalReport" runat="server" AutoDataBind="true" />
    </form>
</body>
</html>
