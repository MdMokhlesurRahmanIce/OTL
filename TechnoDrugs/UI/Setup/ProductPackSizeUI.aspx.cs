using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoDrugs.Helper;
using SecurityModule.Provider;
using BaseModule;
using SetupModule.Provider;
using System.Data;

namespace TechnoDrugs.UI.Setup
{
    public partial class ProductPackSizeUI : PageBase
    {
        string mode = "";
        public ProductPackSizeUI()
        {
            RequiresAuthorization = true;
        }
        private ProductPackSizeProvider provider = new ProductPackSizeProvider();

        #region PaseLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion

        #region Method
        private void LoadData()
        {
            DataSet ds = provider.GetAll();
            gvProductPackSize.DataSource = ds;
            gvProductPackSize.DataBind();
            btnUpdate.Visible = false;
        }
        private void SetProviderValue()
        {
            provider.PackName = txtName.Text;
            provider.PackTypeID = ddlPackType.SelectedValue.Toint();
            provider.Height = float.Parse(txtHeight.Text);
            provider.StatusID = ddlStatus.SelectedValue.ToInt();
            provider.MesurementUnitID = ddlMesurementUnit.SelectedValue.Toint();
            provider.Quantity = txtQuantity.Text.ToDecimal();
            provider.Width = float.Parse(txtWidth.Text);
            provider.Lenght = float.Parse(txtLength.Text);

            provider.EntryUserID = Convert.ToInt16(Session["ID"]);
            provider.UpdateUserID = Convert.ToInt16(Session["ID"]); 
        }
        private void Clear()
        {
            lblMsg.InnerText = string.Empty;
            holdID.Value = string.Empty;
            txtName.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtWidth.Text = string.Empty;
            txtLength.Text = string.Empty;
            txtHeight.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlMesurementUnit.SelectedIndex = 0;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
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
        #endregion

        #region Event
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
        protected void btnUpdate_Click(object sender, EventArgs e)
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
        protected void btnDelete_Click(object sender, EventArgs e)
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
        protected void gvProductPackSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            try
            {
                if (gvProductPackSize.SelectedIndex != -1)
                {
                    GridViewRow row = gvProductPackSize.SelectedRow;
                    Label lblName = (Label)row.FindControl("lblName");
                    Label lblQuantity = (Label)row.FindControl("lblQuantity");
                    
                    Label lblLength = (Label)row.FindControl("lblLength");
                    Label lblWidth = (Label)row.FindControl("lblWidth");
                    Label lblHeight = (Label)row.FindControl("lblHeight");
                    
                    //---------------------------HiddenField select---------------------------------------------
                    HiddenField hfID = (HiddenField)row.FindControl("hfID");
                    HiddenField hfPackTypeID = (HiddenField)row.FindControl("hfPackTypeID");
                    HiddenField hfMesurementUnitID = (HiddenField)row.FindControl("hfMesurementUnitID");
                    HiddenField hfStatusID = (HiddenField)row.FindControl("hfStatusID");
                    //-----------------------Selected item Display To Text or Dropdown List ------------------------------------
                    holdID.Value = hfID.Value;
                    txtName.Text = lblName.Text;
                    ddlPackType.SelectedValue = hfPackTypeID.Value;
                    ddlStatus.SelectedValue = hfStatusID.Value;
                    ddlMesurementUnit.SelectedValue = hfMesurementUnitID.Value;
                    txtQuantity.Text = lblQuantity.Text;
                    txtLength.Text = lblLength.Text;
                    txtWidth.Text = lblWidth.Text;
                    txtHeight.Text = lblHeight.Text;
                    
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
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