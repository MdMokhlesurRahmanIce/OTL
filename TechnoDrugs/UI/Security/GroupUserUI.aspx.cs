using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SecurityModule.Provider;
using TechnoDrugs.Helper;
using BaseModule;

namespace TechnoDrugs.UI.Security
{
    public partial class GroupUserUI : PageBase
    {
        string mode = "";
        public GroupUserUI()
        {
            RequiresAuthorization = true;
        }
        private UserProvider providerUser = new UserProvider();
        private GroupUserProvider provider = new GroupUserProvider();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadGrids();
            }
            else
            {
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (eventTarget == "SearchGroup")
                {
                    string groupCode = Request["__EVENTARGUMENT"];
                    hfUserCode.Value = Request["__EVENTARGUMENT"];

                    LoadGrids();

                    DataTable dtUser = provider.GetAllByGroupCode(groupCode);
                    foreach (GridViewRow row in gvUser.Rows)
                    {
                        Label UserID = (Label)row.FindControl("lblUserID");
                        foreach (DataRow aRowobj in dtUser.Rows.Cast<DataRow>().Where(aRow => aRow[0].ToString() == UserID.Text))
                        {
                            CheckBox chkUser = (CheckBox)row.FindControl("chkUser");
                            chkUser.Checked = true;
                        }
                    }
                    DataTable dtRole = provider.GetAllRoleByGroupCode(groupCode);
                    foreach (GridViewRow row in gvRole.Rows)
                    {
                        Label RoleCode = (Label)row.FindControl("lblRoleCode");
                        foreach (DataRow aRowobj in dtRole.Rows.Cast<DataRow>().Where(aRow => aRow[1].ToString() == RoleCode.Text))
                        {
                            CheckBox chkRole = (CheckBox)row.FindControl("ChkRole");
                            chkRole.Checked = true;
                        }
                    }
                    btnSave.Text = "Update";
                }
            }
        }
        #region All Methods
        private void LoadGrids()
        {
            try
            {
                //User
                DataSet dsUser = providerUser.GetAll();
                gvUser.DataSource = dsUser;
                gvUser.DataBind();

                //Role
                DataTable dsPageControls = provider.GetAllRole();
                gvRole.DataSource = dsPageControls;
                gvRole.DataBind();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        private GroupUserProvider GenerateGroupUserProvider()
        {
            GroupUserProvider gUPObj = new GroupUserProvider();
            if (hfUserCode.Value.IsNotNullOrEmpty())
            {
                gUPObj.GroupCode = hfUserCode.Value;
            }
            gUPObj.GroupName = txtGroupName.Text;
            gUPObj.Description = txtDescription.Text;
            return gUPObj;
        }
        private List<GroupUserProvider> GenerateUserList()
        {
            List<GroupUserProvider> userList = new List<GroupUserProvider>();
            foreach (GridViewRow row in gvUser.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("ChkUser");
                if (chkSelected.Checked)
                {
                    GroupUserProvider user = new GroupUserProvider();
                    Label userID = (Label)row.FindControl("lblUserID");
                    user.UserID = userID.Text;
                    userList.Add(user);
                }
            }
            return userList;
        }
        private List<GroupUserProvider> GenerateRoleList()
        {
            List<GroupUserProvider> RoleList = new List<GroupUserProvider>();
            foreach (GridViewRow row in gvRole.Rows)
            {
                CheckBox chkSelected = (CheckBox)row.FindControl("ChkRole");
                if (chkSelected.Checked)
                {
                    GroupUserProvider role = new GroupUserProvider();
                    Label roleCode = (Label)row.FindControl("lblRoleCode");
                    role.SecurityRuleCode = roleCode.Text;
                    RoleList.Add(role);
                }
            }
            return RoleList;
        }
        private void Clear()
        {
            try
            {
                hfUserCode.Value = String.Empty;
                txtGroupName.Text = String.Empty;
                txtDescription.Text = String.Empty;
                btnSave.Text = "Save";
                LoadGrids();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }
        #endregion
        #region Button Event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Save")
            {
                mode = "Save";
                CheckUserAuthentication(mode);
            }
            else
            {
                mode = "Update";
                CheckUserAuthentication(mode);
            }
            bool msg = false;
            string message = string.Empty;
            try
            {
                GroupUserProvider GroupUserProvider = GenerateGroupUserProvider();
                List<GroupUserProvider> UserList = GenerateUserList();
                List<GroupUserProvider> RoleList = GenerateRoleList();
                msg = provider.Save(GroupUserProvider, UserList, RoleList, btnSave.Text);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            if (msg)
            {
                MessageHelper.ShowAlertMessage("Data saved successfully");
            }
            else
            {
                MessageHelper.ShowAlertMessage(MessageConstants.SavedWarning);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("GroupCode", "Group Code");
                columns.Add("GroupName", "Group Name");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "select GroupCode,GroupName,[Description] from UserAccess.[Group]";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }
        #endregion
    }
}