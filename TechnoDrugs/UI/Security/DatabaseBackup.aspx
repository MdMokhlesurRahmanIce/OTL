<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DatabaseBackup.aspx.cs" 
    Inherits="TechnoDrugs.UI.Security.DatabaseBackup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <div>
            <div style ="width: 40%; text-align: left"> Location:</div>
            <div style ="width: 40%; text-align: right"> <asp:TextBox ID="txtlocation" runat="server">D:\\LiveDBBackup</asp:TextBox> </div>            
        </div>
        
        <div>
            <div style ="width: 40%; text-align: left"> BackUp DataBase Name</div>
            <div style ="width: 40%; text-align: right"> <asp:TextBox ID="txtfilename" runat="server" Text="TechnoDrugsLive"></asp:TextBox></div>           
        </div>
        <div>
            <div>
                <asp:Button ID="btnBackup" runat="server" Text="BackUp" 
                            onclick="btnBackup_Click" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnclose" runat="server" Text="Close" 
                            onclick="btnclose_Click" />
                <br />
                <br />
                <br />
                <asp:Label ID="lblsuccessfull" runat="server" Font-Bold="True" 
                           Font-Italic="False" Font-Size="large" ForeColor="#000066"></asp:Label>
            </div>
            <div>
                &nbsp;</div>
        </div>
    </div>
</asp:Content>
