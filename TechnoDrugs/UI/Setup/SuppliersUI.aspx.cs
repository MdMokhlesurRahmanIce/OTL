using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using SetupModule.Provider;
using System.Data;
using BaseModule;
using System.Globalization;
using TechnoDrugs.Helper;

namespace TechnoDrugs.UI.Setup
{
    public partial class SuppliersUI : PageBase
    {
        string mode = "";

        #region Properties
        public SuppliersUI()
        {
            RequiresAuthorization = true;
        }
        private SupplierProvider provider = new SupplierProvider();
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion

        #region Method
        private void Clear()
        {
            lblMsg.InnerText = string.Empty;
            holdID.Value = string.Empty;
            txtSupplierID.Text = string.Empty;
            txtTINNumber.Text = string.Empty;
            txtName.Text = string.Empty;
            txtContactName.Text = string.Empty;
            ddlSupplierType.SelectedIndex = 0;
            txtVatRegNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCountry.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtNote.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        private void LoadData()
        {
            DataSet ds = provider.GetAll();
            gvSuppliers.DataSource = ds;
            gvSuppliers.DataBind();
            btnUpdate.Visible = false;
        }
        private void SetProviderValue()
        {
            provider.TINNumber = txtTINNumber.Text;
            provider.Name = txtName.Text;
            provider.StatusID = ddlStatus.SelectedValue.ToInt();
            provider.ContactName = txtContactName.Text;
            provider.TypeID = ddlSupplierType.SelectedValue.ToInt();
            provider.VatRegNo = txtVatRegNo.Text;
            provider.Address = txtAddress.Text;
            provider.CountryName = txtCountry.Text;
            provider.Phone = txtPhone.Text;
            provider.Mobile = txtMobile.Text;
            provider.Email = txtEmail.Text;
            provider.Note = txtNote.Text;
            provider.EntryUserID = (Int32)HttpContext.Current.Session["ID"];
            provider.UpdateUserID = (Int32)HttpContext.Current.Session["ID"];
        }
        private void Save()
        {
            SetProviderValue();
            if (provider.Save())
            {
                Clear();
                this.AlertSuccess(lblMsg, MessageConstants.Saved);
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
                this.AlertSuccess(lblMsg, MessageConstants.Updated);
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage(MessageConstants.UpdateWarning);
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
                Button btnDelete = sender as Button;
                GridViewRow deleteRow = (GridViewRow)btnDelete.NamingContainer;
                int rowIndex = deleteRow.RowIndex;
                foreach (GridViewRow row in gvSuppliers.Rows)
                {
                    if (row.RowIndex == rowIndex)
                    {
                        HiddenField hfId = (HiddenField)row.FindControl("hfID");
                        provider.ID = hfId.Value.Toint();
                    }
                }
                if (provider.Delete())
                {
                    Clear();
                    this.AlertSuccess(lblMsg, MessageConstants.Deleted);
                    LoadData();
                }
                else
                {
                    MessageHelper.ShowAlertMessage(MessageConstants.DeletedWarning);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Clear();
            this.AlertNone(lblMsg);
        }
        protected void gvSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            try
            {
                if (gvSuppliers.SelectedIndex != -1)
                {
                    GridViewRow row = gvSuppliers.SelectedRow;
                    Label lblName = (Label)row.FindControl("lblName");
                    Label lblVatRegNo = (Label)row.FindControl("lblVatRegNo");
                    Label lblMobile = (Label)row.FindControl("lblMobile");
                    Label lblAddress = (Label)row.FindControl("lblAddress");

                    //---------------------------HiddenField  select---------------------------------------------
                    HiddenField hfID = (HiddenField)row.FindControl("hfID");
                    HiddenField hfTarriffID = (HiddenField)row.FindControl("hfTarriffID");
                    HiddenField hfTypeID = (HiddenField)row.FindControl("hfTypeID");

                    HiddenField hfCountryName = (HiddenField)row.FindControl("hfCountryName");
                    HiddenField hfContactName = (HiddenField)row.FindControl("hfContactName");
                    HiddenField hfPhone = (HiddenField)row.FindControl("hfPhone");
                    HiddenField hfEmail = (HiddenField)row.FindControl("hfEmail");
                    HiddenField hfNote = (HiddenField)row.FindControl("hfNote");
                    HiddenField hfStatusID = (HiddenField)row.FindControl("hfStatusID");

                    //-----------------------Selected item Display To Text or Dropdown List ------------------------------------
                    holdID.Value = hfID.Value;
                    txtSupplierID.Text = hfID.Value;
                    txtName.Text = lblName.Text;
                    txtContactName.Text = hfContactName.Value;

                   // txtVatRegNo.Text = lblVatRegNo.Text;
                    txtAddress.Text = lblAddress.Text;
                    ddlSupplierType.SelectedValue = hfTypeID.Value;

                    txtPhone.Text = hfPhone.Value;
                    txtEmail.Text = hfEmail.Value;
                    txtMobile.Text = lblMobile.Text;
                    txtNote.Text = hfNote.Value;
                    txtCountry.Text = hfCountryName.Value;
                    ddlStatus.SelectedValue = hfStatusID.Value;

                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        protected void gvSuppliers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSuppliers.PageIndex = e.NewPageIndex;
            DataSet ds = provider.GetAll();
            gvSuppliers.DataSource = ds;
            gvSuppliers.DataBind();
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            this.AlertNone(lblMsg);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText;
                if (ddlSearch.SelectedValue == "1")
                {
                    searchText = "A.[SupplierName] like '%" + txtSearch.Text.Trim() + "%'";
                }
                else
                {
                    searchText = "A.[ContactPerson] like '%" + txtSearch.Text.Trim() + "%'";
                }
                DataSet dSet = provider.GetAllByFilterExpression(searchText);
                if (dSet != null && dSet.Tables[0].Columns.Count > 0)
                {
                    gvSuppliers.DataSource = dSet.Tables[0];
                    gvSuppliers.DataBind();
                }
                else
                {
                    MessageHelper.ShowAlertMessage("Data Not Found.");
                    LoadData();
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