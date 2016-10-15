using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using System.Data;
using TechnoDrugs.Helper;

namespace TechnoDrugs.UI.Security
{
    public partial class UserUI : PageBase
    {
        string mode = "";
        public UserUI()
        {
            RequiresAuthorization = true;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        #region Method
        
        private UserProvider provider = new UserProvider();
        private void Clear()
        {
            lblMsg.InnerText = string.Empty;
            holdID.Value = string.Empty;
            txtUserID.Text = string.Empty;
            txtPassword.Attributes.Add("value", "");
            txtEmail.Text = string.Empty;
            txtSecurityQuestion.Text = string.Empty;
            txtAnswer.Text = string.Empty;
            chkIsAdmin.Checked = false;
            cbIsLocked.Visible = true;
            txtLockedDate.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            btnSave.Visible = true;
            btnUpdate.Visible = false;        
        }
        private void SetProviderValue()
        {
            holdID.Value = string.Empty;
            provider.UserID = txtUserID.Text;
            provider.FullName = txtFullName.Text;
            provider.Designation = txtDesignation.Text;
            provider.Password = txtPassword.Text.Trim();
            provider.Email = txtEmail.Text;
            provider.SecurityQuestion = txtSecurityQuestion.Text;
            provider.Answer = txtAnswer.Text;
            provider.IsAdmin = chkIsAdmin.Checked;
            provider.IsLocked = cbIsLocked.Checked;
            provider.LockedDate = txtLockedDate.Text.ToNullableDateTime();
            provider.EntryUserID = (Int32)HttpContext.Current.Session["ID"];
            provider.UpdateUserID = (Int32)HttpContext.Current.Session["ID"];
            provider.StatusID = ddlStatus.SelectedValue.ToInt();
        }
        private void Save()
        {
            SetProviderValue();
            if (provider.Save())
            {
                Clear();
                MessageHelper.ShowAlertMessage(MessageConstants.Saved);
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage(MessageConstants.SavedWarning);
            }
        }
        private void Update()
        {
            provider.ID = holdID.Value.ToInt();
            SetProviderValue();
            if (provider.Update())
            {
                Clear();
                MessageHelper.ShowAlertMessage(MessageConstants.Updated);
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage(MessageConstants.UpdateWarning);
            }
        }
        private void Delete()
        {
            provider.ID = holdID.Value.ToInt();
            SetProviderValue();
            if (provider.Delete())
            {
                Clear();
                MessageHelper.ShowAlertMessage(MessageConstants.Deleted);
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage(MessageConstants.DeletedWarning);
            }
        }

        public void LoadData()
        {
            try
            {
                DataSet ds = provider.GetAll();
                gvUser.DataSource = ds;
                gvUser.DataBind();
                btnUpdate.Visible = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion
        #region Events
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                mode = "Save";
                CheckUserAuthentication(mode);
                Save();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void btnUpdate_Click1(object sender, EventArgs e)
        {
            try
            {
                mode = "Update";
                CheckUserAuthentication(mode);
                Update();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void btnDelete_Click1(object sender, EventArgs e)
        {
            try
            {
                mode = "Delete";
                CheckUserAuthentication(mode);
                Delete();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void gvUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            try
            {
                if (gvUser.SelectedIndex != -1)
                {
                    GridViewRow row = gvUser.SelectedRow;
                    Label lblUserID = (Label)row.FindControl("lblUserID");
                    Label lblStatusName = (Label)row.FindControl("lblStatusName");

                    //---------------------------HiddenField select---------------------------------------------

                    HiddenField hfID = (HiddenField)row.FindControl("hfID");
                    HiddenField hfPassword = (HiddenField)row.FindControl("hfPassword");
                    HiddenField hfFullName = (HiddenField)row.FindControl("hfFullName");
                    HiddenField hfDesignation = (HiddenField)row.FindControl("hfDesignation");
                    //HiddenField hfEmploeeID = (HiddenField)row.FindControl("hfEmploeeID");
                    HiddenField hfEmail = (HiddenField)row.FindControl("hfEmail");
                    HiddenField hfSecurityQuestion = (HiddenField)row.FindControl("hfSecurityQuestion");
                    HiddenField hfAnswer = (HiddenField)row.FindControl("hfAnswer");
                    HiddenField hfUserGroupID = (HiddenField)row.FindControl("hfUserGroupID");
                    HiddenField hfIsAdmin = (HiddenField)row.FindControl("hfIsAdmin");
                    HiddenField hfIsLocked = (HiddenField)row.FindControl("hfIsLocked");
                    HiddenField hfLockedDate = (HiddenField)row.FindControl("hfLockedDate");
                    HiddenField hfStatusID = (HiddenField)row.FindControl("hfStatusID");

                    //-----------------------Selected item Display To Text or Dropdown List ------------------------------------

                    holdID.Value = hfID.Value;
                    txtUserID.Text = lblUserID.Text;
                    txtFullName.Text = hfFullName.Value;
                    txtDesignation.Text = hfDesignation.Value;
                    txtPassword.Attributes.Add("value", hfPassword.Value.Trim());                   
                    txtEmail.Text = hfEmail.Value;
                    txtSecurityQuestion.Text = hfSecurityQuestion.Value;
                    txtAnswer.Text = hfAnswer.Value;
                    chkIsAdmin.Checked = hfIsAdmin.Value.ToBoolean();
                    cbIsLocked.Visible = Convert.ToBoolean(hfIsLocked.Value);
                    txtLockedDate.Text = hfLockedDate.Value;
                    ddlStatus.SelectedValue = hfStatusID.Value;
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion        
    }
}