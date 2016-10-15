<%@ Page Title="User Information" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserUI.aspx.cs" Inherits="TechnoDrugs.UI.Security.UserUI" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".DatepickerInput").datepicker({ dateFormat: 'dd M yy' });
            $("#ui-datepicker-div").hide();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {
                $('.DatepickerInput').datepicker({ dateFormat: 'dd M yy' });
                $("#ui-datepicker-div").hide();
            }
        });

        function capLock(e) {
            alert();
            kc = e.keycode ? e.keycode : e.which;
            sk = e.shiftkey ? e.shiftkey : ((kc == 16) ? true : false);
            if (((kc >= 65 && kc <= 90) && !sk) || ((kc >= 97 && kc <= 122) && sk))
                document.getElementById('lblMsg').innerHTML = 'Warning: Caps Lock is on!';
            else
                document.getElementById('lblMsg').innerHTML = '';
        }

    </script>
    <style type="text/css">
        .form_action {
            height: 44px;
        }

        .gridTable {
        }

        .style2 {
            width: 35%; /* margin: 1px 5px;*/
            ;
            padding: 4px 3px 3px;
            float: left;
            text-align: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="updVatDeduction" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="form-wrapper">
                <div style="width: 100%; background-color: #F7F7F7">
                    <div style="width: 30%; float: left">
                        <h2>User Setup
                        </h2>
                    </div>
                    <div style="width: 70%; float: left">
                        <div id="lblMsg" class="validationbox" runat="server">
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both">
            </div>
            <div class="form-details">
                <div style="width: 33%; float: left;">
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblFullName" runat="server" Height="14px" Text="Full Name"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtFullName" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblDesignation" runat="server" Height="14px" Text="Designation"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtDesignation" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblIsLocked" runat="server" Height="14px" Text="IsLocked"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtIsLocked" runat="server" CssClass="txtwidth178px" MaxLength="100"
                                Width="80%"></asp:TextBox>
                            &nbsp&nbsp
                            <asp:CheckBox ID="cbIsLocked" runat="server" Checked="true" />
                        </div>
                    </div>
                </div>
                <div style="width: 33%; float: left;">
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblEmail" runat="server" Height="14px" Text="Email"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblUserID" runat="server" Height="14px" Text="LogIn Name"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtUserID" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblPassword" runat="server" Height="14px" Text="Password"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtPassword" runat="server" CssClass="txtwidth178px" MaxLength="100" TextMode="Password"
                                AutoComplete="off"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblLockedDate" runat="server" Height="14px" CssClass="txtwidth178px"
                                Text="Locked Date"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtLockedDate" runat="server" CssClass="DatepickerInput" MaxLength="100" Width="80%"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div style="width: 33%; float: left;">
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblSecurityQuestion" runat="server" Height="14px" Text="Security Question"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtSecurityQuestion" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblAnswer" runat="server" Height="14px" Text="Answer"></asp:Label>
                        </div>
                        <div class="div80Px">
                            <asp:TextBox ID="txtAnswer" runat="server" CssClass="txtwidth178px" MaxLength="100"></asp:TextBox>
                        </div>
                    </div>
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblUserGroupID" runat="server" Text="IsAdmin"></asp:Label>
                        </div>
                        <div class="div182Px">
                            <asp:CheckBox ID="chkIsAdmin" runat="server" Checked="false" />
                        </div>
                    </div>
                </div>
                <div style="width: 33%; float: left; height: 30px;">
                    
                    <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px">
                            <asp:Label ID="lblStatus" runat="server" Text="Status"></asp:Label>
                        </div>
                        <div class="div182Px">
                            <asp:DropDownList ID="ddlStatus" runat="server" Width="80%" DataTextField="Name" DataValueField="ID"
                                CssClass="griddrpwidth180px" AppendDataBoundItems="true">
                                <asp:ListItem Value="0" Text="--------Select--------"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Active"></asp:ListItem>
                                <asp:ListItem Value="0" Text="Inactive"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>                    
                </div>
            </div>
            <br />
            <br />
            <div>
                <div class="form_action" style="text-align: center">
                    <asp:Button ID="btnSave" runat="server" class="btn action_save" OnClick="btnSave_Click"
                        Text="Save" />
                    <asp:Button ID="btnUpdate" runat="server" class="btn action_update" OnClick="btnUpdate_Click1"
                        Text="Update" />
                    <asp:Button ID="btnDelete" runat="server" class="btn action_del" OnClick="btnDelete_Click1"
                        Text="Delete" />
                    <asp:Button ID="btnRefresh" runat="server" class="btn action_ref" OnClick="btnRefresh_Click"
                        Text="Refresh" />
                </div>
                <div style="clear: both">
                </div>
                <br />
                <br />
                <br />
                <h4>User List
                </h4>
                <div>
                    <asp:GridView ID="gvUser" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gvUser_SelectedIndexChanged"
                        GridLines="None" CssClass="gridTable" Width="99%">
                        <EmptyDataTemplate>
                            no data retrived
                        </EmptyDataTemplate>
                        <Columns>
                            <asp:CommandField SelectText="Select" ShowSelectButton="True" />
                            <asp:TemplateField HeaderText="UserID">
                                <ItemTemplate>
                                    <asp:Label ID="lblUserID" runat="server" Text='<%# Eval("UserID")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatusName" runat="server" Text='<%# Eval("StatusName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hfID" runat="server" Value='<%# Eval("ID") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfPassword" runat="server" Value='<%#Eval("Password") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfDesignation" runat="server" Value='<%# Eval("Designation") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfFullName" runat="server" Value='<%# Eval("FullName") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfEmail" runat="server" Value='<%# Eval("Email") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfSecurityQuestion" runat="server" Value='<%#Eval("SecurityQuestion") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfAnswer" runat="server" Value='<%#Eval("Answer") %>'></asp:HiddenField>
                                    <%--<asp:HiddenField ID="hfUserGroupID" runat="server" Value='<%#Eval("UserGroupID") %>'></asp:HiddenField>--%>
                                    <asp:HiddenField ID="hfIsAdmin" runat="server" Value='<%#Eval("IsAdmin") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfIsLocked" runat="server" Value='<%#Eval("IsLocked") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hfLockedDate" runat="server" Value='<%#Eval("LockedDate")%>'></asp:HiddenField>

                                    <asp:HiddenField ID="hfStatusID" runat="server" Value='<%#Eval("StatusID") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <br />
            <asp:HiddenField ID="holdID" runat="server" />
            <asp:ObjectDataSource ID="odsUserGroupID" runat="server" SelectMethod="GetAllActive"
                TypeName="SecurityModule.Provider.UserGroupProvider"></asp:ObjectDataSource>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
