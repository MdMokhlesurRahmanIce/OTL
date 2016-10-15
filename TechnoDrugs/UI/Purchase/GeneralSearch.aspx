<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralSearch.aspx.cs" Inherits="TechnoDrugs.UI.Purchase.GeneralSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        var item = new Object();
        function ReturnInformation(arguments) {
            item = new items(arguments);
            window.returnValue = item;
            window.close();
        }
        function items(retVal) {
            var test = retVal.split(":");
            lgth = test.length;
            for (i = 0; i < test.length; i++) {
                this[i] = test[i];
            }
        }
    </script>
    <style type="text/css">
        .gridTable
        {
            margin: 0 auto;
            border: 1px solid #dddddd;
            text-align: left;
        }

    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblName" runat="server" Text="Enter Batch No. :"></asp:Label>
        <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
        <br />
        <br />
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
       
        <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="GridView1_PageIndexChanging"
            OnSelectedIndexChanged="GridView1_SelectedIndexChanged" AllowPaging="True" 
            CellPadding="4" ForeColor="#333333" GridLines="None" Width="500px" 
            Height="250px" >
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:CommandField SelectText="Select" ShowSelectButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"/>
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" HorizontalAlign="Left" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </div>
    </form>
</body>
</html>
