<%@ Page Title="User Permission Setup" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPageControlsUI.aspx.cs" Inherits="TechnoDrugs.UI.Security.UserPageControlsUI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
        .style4
        {
            text-align: left;
            width: 155px;
        }
        .style5
        {
            text-align: right;
            width: 258px;
        }
        .style6
        {
            text-align: left;
        }
        .style7
        {            text-align: left;
        }
        .style8
        {
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        function SelectAll(checkBoxControl) {
            var i;
            if (checkBoxControl.checked == true) {
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('Select') > -1)) {
                        document.forms[0].elements[i].checked = true;
                    }
                }
            }
            else {
                for (i = 0; i < document.forms[0].elements.length; i++) {

                    if ((document.forms[0].elements[i].type == 'checkbox') && (document.forms[0].elements[i].name.indexOf('Select') > -1)) {
                        document.forms[0].elements[i].checked = false;
                    }
                }
            }
        }


        function checkBoxClicked(cbxSelect, cbxAdd, cbxEdit, cbxDelate, cbxPreview,chkSend,chkCheck,chkApprove, cbxReceive, cbxAll, ctl) {
            var cbkSelect = document.getElementById(cbxSelect);
            var cbkAdd = document.getElementById(cbxAdd);
            var cbkEdit = document.getElementById(cbxEdit);
            var cbkDelate = document.getElementById(cbxDelate);
           var  cbkSend = document.getElementById(cbkSend);
           var cbkCheck = document.getElementById(cbkCheck);;
           var cbkApprove = document.getElementById(cbkApprove);;
            var cbkPreview = document.getElementById(cbxPreview);
            var cbkReceive = document.getElementById(cbxReceive);
            var cbkAll = document.getElementById(cbxAll);
            var itemChecked = getCheckedItem(cbxSelect, cbxAdd, cbxEdit, cbxDelate, cbxPreview, chkSend, chkCheck, chkApprove, cbxReceive, cbxAll, ctl);
            if (itemChecked == "false") {
                if (ctl == "SELECT") {
                    if (cbkSelect.checked == true) {
                        cbkSelect.checked = true;
                    }
                    else {
                        cbkSelect.checked = false;
                    }
                }
                else if (ctl == "ADD") {
                    if (cbkAdd.checked == true) {
                        cbkAdd.checked = true;
                    }
                    else {
                        cbkSelect.checked = false;
                    }
                }
                else if (ctl == "EDIT") {
                    if (cbkEdit.checked == true) {
                        cbkEdit.checked = true;
                    }
                    else {
                        cbkEdit.checked = false;
                    }
                }
                else if (ctl == "DELATE") {
                    if (cbkDelate.checked == true) {
                        cbkDelate.checked = true;
                    }
                    else {
                        cbkDelate.checked = false;
                    }
                }
                else if (ctl == "SEND") {
                    if (cbkSend.checked == true) {
                        cbkSend.checked = true;
                    }
                    else {
                        cbkSend.checked = false;
                    }
                }
                else if (ctl == "CHECK") {
                    if (cbkCheck.checked == true) {
                        cbkCheck.checked = true;
                    }
                    else {
                        cbkCheck.checked = false;
                    }
                }
                else if (ctl == "APPROVE") {
                    if (cbkApprove.checked == true) {
                        cbkApprove.checked = true;
                    }
                    else {
                        cbkApprove.checked = false;
                    }
                }
                else if (ctl == "PREVIEW") {
                    if (cbkPreview.checked == true) {
                        cbkPreview.checked = true;
                    }
                    else {
                        cbkPreview.checked = false;
                    }
                }
                else if (ctl == "RECEIVE") {
                    if (cbkReceive.checked == true) {
                        cbkReceive.checked = true;
                    }
                    else {
                        cbkReceive.checked = false;
                    }
                }
                else if (ctl == "ALL") {
                    if (cbkAll.checked == true) {
                        cbkSelect.checked = true;
                        cbkAdd.checked = true;
                        cbkEdit.checked = true;
                        cbkDelate.checked = true;
                        cbkSend.checked = true;
                        cbkCheck.checked = true;
                        cbkApprove.checked = true;
                        cbkPreview.checked = true;
                        cbkReceive.checked = true;
                        cbkAll.checked = true;
                    }
                    else {
                        cbkSelect.checked = false;
                        cbkAdd.checked = false;
                        cbkEdit.checked = false;
                        cbkDelate.checked = false;
                        cbkSend.checked = false;
                        cbkCheck.checked = false;
                        cbkApprove.checked = false;
                        
                        cbkPreview.checked = false;
                        cbkReceive.checked = false;
                        cbkAll.checked = false;
                    }
                }
            }
            else {
                if (ctl == "SELECT") {
                    cbkSelect.checked = true;
                }
                else if (ctl == "ADD") {
                    cbkAdd.checked = true;
                }
                else if (ctl == "EDIT") {
                    cbkEdit.checked = true;
                }
                else if (ctl == "DELATE") {
                    cbkDelate.checked = true;
                }
                else if (ctl == "SEND") {
                    cbkSend.checked = true;
                }
                else if (ctl == "CHECK") {
                    cbkCheck.checked = true;
                }
                else if (ctl == "APPROVE") {
                    cbkApprove.checked = true;
                }
                else if (ctl == "PREVIEW") {
                    cbkPreview.checked = true;
                }
                else if (ctl == "RECEIVE") {
                    cbkReceive.checked = true;
                }
                else if (ctl == "ALL") {
                    cbkAll.checked = true;
                }
            }
        }
        //Function getCheckedItem returns the previously selected checkboxes.
        function getCheckedItem(cbxSelect, cbxAdd, cbxEdit, cbxDelate, cbxPreview, chkSend, chkCheck, chkApprove, cbxReceive, cbxAll, ctl) {
            var cbkSelect = document.getElementById(cbxSelect);
            var cbkAdd = document.getElementById(cbxAdd);
            var cbkEdit = document.getElementById(cbxEdit);
            var cbkDelate = document.getElementById(cbxDelate);
            var cbkPreview = document.getElementById(cbxPreview);
            var cbkReceive = document.getElementById(cbxReceive);
            var cbkAll = document.getElementById(cbxAll);
            var retVal = "false";
            if (ctl == "SELECT") {
                if (cbkSelect.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "ADD") {
                if (cbkAdd.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "EDIT") {
                if (cbkEdit.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "DELATE") {
                if (cbkDelate.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "SEND") {
                if (cbkSend.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "CHECK") {
                if (cbkCheck.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "APPROVE") {
                if (cbkApprove.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "PREVIEW") {
                if (cbkPreview.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "RECEIVE") {
                if (cbkReceive.checked == true)
                { retVal = "true"; }
                else { retVal = "false"; }
            }
            else if (ctl == "ALL") {
                retVal = "false";
            }
            return retVal;
        }
    </script>
    <script type="text/javascript">
        function Search() {
            var returnResult = window.showModalDialog("../Purchase/GeneralSearch.aspx", "", "dialogWidth:525px; dialogHeight:365px; dialogTop:180px; dialogLeft:226px; center:no; status:no");
            document.getElementById("<%=hfRoleCode.ClientID %>").value = returnResult[0];
            document.getElementById("<%=roleNameTextBox.ClientID %>").value = returnResult[1];
            if (returnResult[2] == '&nbsp;') {
                document.getElementById("<%=descriptionTextBox.ClientID %>").value = '';
            }
            else {
                document.getElementById("<%=descriptionTextBox.ClientID %>").value = returnResult[2];
            }
            __doPostBack("Search", returnResult[0]);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div style="width:30%; float:left">
                <h2>
                Page Controls information
                </h2>
            </div>
            <div id="lblMsg" class="validationbox" runat="server">
            </div>
            <div style="width:50%; float:left; margin-left: 30%;">
                <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px" style="width:22%" align="left">
                            <asp:Label ID="Label2" runat="server" Text="Role Name"></asp:Label>
                        </div>
                        <div class="div182Px" style="width:60%" align="left">
                            <asp:TextBox ID="roleNameTextBox" runat="server" Width="70%"></asp:TextBox>
                            <asp:ImageButton ID="PageConFind" runat="server" CssClass="btnImageStyle" ImageUrl="~/images/Search20X20.png"
                            OnClick="btnFind_Click" Height="20px" Width="22px" />
                        </div>
                </div>
                <div class="lblAndTxtStyle">
                        <div class="divlblwidth100px" style="width:22%" align="left">
                            <asp:Label ID="Label3" runat="server" Text="Role Description"></asp:Label>
                        </div>
                        <div class="div182Px" style="width:60%" align="left">
                            <asp:TextBox ID="descriptionTextBox" runat="server" TextMode="MultiLine" 
                                Width="70%"></asp:TextBox>
                        </div>
                </div>
                <div class="lblAndTxtStyle">
                    <div class="divlblwidth100px" style="width:22%" align="left">
                        <asp:Label ID="lblVatRegNo" runat="server" Text="Select Menu Type"></asp:Label>
                    </div>
                    <div class="div182Px" style="width:60%">
                        <asp:DropDownList ID="ddlMenuType" runat="server" CssClass="drpwidth99per" OnSelectedIndexChanged="ddlMenuType_OnSelectedIndexChanged"
                            AutoPostBack="True" Width="72%">
                        </asp:DropDownList>
                    </div>
                </div>               
            </div>
                <div style="clear:both"></div>
            <div>
                            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                GridLines="None" OnRowDataBound="GridView1_RowDataBound" 
                            Width="956px" onpageindexchanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="MenuID" HeaderText="ID" SortExpression="MenuID">
                                <HeaderStyle Width="200px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ParentID" HeaderText="ParentID" Visible="False" />
                                <asp:BoundField DataField="Caption" HeaderText="PagesCaption-Name">
                                <ItemStyle Width="400px" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Select">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelectAll" runat="server" AutoPostBack="True" 
                                            CssClass="style8" OnCheckedChanged="chkSelectAll_CheckedChanged" 
                                            Text="  Select" Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelect" runat="server" Height="20px" Width="80px" Checked='<%#Bind("CanSelect")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Insert">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkInsertAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkInsertAll_CheckedChanged" 
                                            Text="  Insert " Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkAdd" runat="server" Checked='<%#Bind("CanInsert")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Update">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkEditAll" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="chkEditAll_CheckedChanged" Text="  Update" Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkEdit" runat="server" Checked='<%#Bind("CanUpdate")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Delete">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkDelateAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkDelateAll_CheckedChanged" 
                                            Text="  Delete" Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkDelate" runat="server" Checked='<%#Bind("CanDelete")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Send">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSendAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkSendAll_CheckedChanged" 
                                            Text="  Send " Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSend" runat="server" Checked='<%#Bind("CanSend")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Check">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkCheckAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkCheckAll_CheckedChanged" 
                                            Text="  Check " Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCheck" runat="server" Checked='<%#Bind("CanCheck")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approve">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkApproveAll" runat="server" AutoPostBack="True" OnCheckedChanged="chkApproveAll_CheckedChanged" 
                                            Text="  Approve " Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApprove" runat="server" Checked='<%#Bind("CanApprove")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Preview">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkPreviewAll" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="chkPreviewAll_CheckedChanged" Text="  Preview" 
                                            Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkPreview" runat="server" Checked='<%#Bind("CanPreview")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkReceiveAll" runat="server" AutoPostBack="True" 
                                            OnCheckedChanged="chkReceiveAll_CheckedChanged" Text="  Receive" Width="80px" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkReceive" runat="server" Checked='<%#Bind("CanReceive")%>'/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           </Columns>
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <PagerSettings PageButtonCount="5" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        </asp:GridView>
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="form-actions" style="text-align:center">
        <asp:HiddenField ID="hfRoleCode" runat="server" />
        <asp:Button ID="btnSubmit" runat="server" Class="btn action_save" Text="Submit" OnClick="btnSubmit_Click" />
        
        <asp:Button ID="newButton" runat="server" Class="btn action_ref" Text="New" onclick="newButton_Click" />
    </div>
    <div style="clear:both"></div>
</asp:Content>
