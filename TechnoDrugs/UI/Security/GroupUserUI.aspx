<%@ Page Title="Group User Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GroupUserUI.aspx.cs" Inherits="TechnoDrugs.UI.Security.GroupUserUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        input[type='checkbox'].mycheckbox {
            height: 12px;
            width: 12px;
        }

        .style3 {
            text-align: center;
        }

        .style4 {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function Search() {
            var returnResult = window.showModalDialog("../Purchase/GeneralSearch.aspx", "", "dialogWidth:525px; dialogHeight:365px; dialogTop:180px; dialogLeft:226px; center:no; status:no");
            document.getElementById("<%=txtGroupName.ClientID %>").value = returnResult[1];
            if (returnResult[2] == '&nbsp;') {
                document.getElementById("<%=txtDescription.ClientID %>").value = '';
            }
            else {
                document.getElementById("<%=txtDescription.ClientID %>").value = returnResult[2];
            }
            __doPostBack('SearchGroup', returnResult[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updSalesInvoice" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>User Role Assign</h2>
                    </div>
                    <div style="width: 70%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <div class="form-details">
                    <div style="width: 33%;">
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="lblGroupName" runat="server" Text="Group Name"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <div style="width: 85%; float: left">
                                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="txtwidth178px"></asp:TextBox>
                                </div>
                                <div style="width: 15%; float: left">
                                    <asp:ImageButton ID="btnFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/images/Search20X20.png"
                                        OnClick="btnFind_Click" />
                                </div>                                
                            </div>
                        </div>
                        <div class="lblAndTxtStyle">
                            <div class="divlblwidth100px">
                                <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
                            </div>
                            <div class="div80Px">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="txtwidth178px" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div style="clear: both">
                    </div>
                    <br />
                    <br />
                    <div style="width: 100%">
                        <div style="width: 50%; float: left">
                            <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" Width="95%"
                                Caption="User List" CssClass="gridTable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <HeaderStyle HorizontalAlign="Left" Width="10%" VerticalAlign="Middle" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkUser" runat="server" CssClass="mycheckbox" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="User ID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div style="width: 50%; float: left">
                            <asp:GridView ID="gvRole" runat="server" AutoGenerateColumns="False"
                                Caption="Role List" Width="95%" CssClass="gridTable">
                                <Columns>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="ChkRole" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleCode" runat="server" Text='<%# Eval("RoleCode") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Role Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRoleName" runat="server" Text='<%# Eval("RoleName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div style="clear: both">
                </div>
                <br />
                <div class="form_action" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn action_save" ValidationGroup="Save"
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Refresh" CssClass="btn action_ref" OnClick="btnClear_Click" />
                </div>
            </div>
            <asp:HiddenField ID="hfUserCode" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
