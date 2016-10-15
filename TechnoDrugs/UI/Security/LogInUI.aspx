<%@ Page Title="Log In Page" Language="C#" AutoEventWireup="true" CodeBehind="LogInUI.aspx.cs" Inherits="TechnoDrugs.UI.Security.LogIn" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../Style/LogIn.css" rel="stylesheet" type="text/css" />
    <title>Log In</title>
</head>
<body>
<div id="wrapper">

<div id="container">
<%--<img id="logo" alt="Vat Software" src="/Style/images/old-mobil-logo.jpg" />--%>
<form runat="server" id="loginform">
	
		<asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
	 <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>

	 <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
	 <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
	
	 <asp:Button ID="btnSubmit" runat="server" Text="Submit" 
                        onclick="btnSubmit_Click" />        
</form>
    
    </div>
    </div>
</body>
</html>

