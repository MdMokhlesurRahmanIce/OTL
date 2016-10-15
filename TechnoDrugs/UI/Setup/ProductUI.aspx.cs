using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseModule;
using SetupModule.Provider;
using SecurityModule.Provider;
using TechnoDrugs.Helper;

namespace TechnoDrugs.UI.Setup
{
    public partial class ProductUI : PageBase
    {
        string mode = "";
       
        #region Properties
        public ProductUI()
        {
            RequiresAuthorization = true;
        }
        #endregion

        #region PageLoad
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    gvProduct.PageIndex = 0;
                    DARNo.Visible = false;
                    TradePrice.Visible = false;
                    MRP.Visible = false;
                    LoadData();
                }
                else
                {
                    Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                    String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                    if (Request["__EVENTTARGET"] == "NewValInsert")
                    {
                        string code = Request["__EVENTARGUMENT"];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion

        #region Method
        private ProductProvider provider = new ProductProvider();
        private void Clear()
        {
            this.AlertNone(lblMsg);
            holdID.Value = string.Empty;
            txtCode.Text = string.Empty;
            txtName.Text = string.Empty;
            txtLocation.Text = string.Empty;

            lblSpecification.Visible = false;
            txtSpecification.Visible = false;
            txtSpecification.Text = string.Empty;

            txtGenericName.Text = string.Empty;
            lblGenericName.Visible = false;
            txtGenericName.Visible = false;

            lblPacksize.Visible = false;
            txtPackSize.Visible = false;
            txtPackSize.Text = string.Empty;
            
            DARNo.Visible = false;
            txtDARNo.Text = string.Empty;

            MRP.Visible = false;
            txtMRP.Text = string.Empty;

            TradePrice.Visible = false;
            txtTradePrice.Text = string.Empty;

            ddlMesurementUnit.SelectedIndex = 0;
           // ddlNatureID.SelectedIndex = 0;
            ddlDivision.SelectedIndex = 0;
            dllItemTypeID.SelectedIndex = 0;
            txtSafetyStock.Text = string.Empty;
            
            ddlStatus.SelectedIndex = 0;
            btnSave.Visible = true;
            btnUpdate.Visible = false;         
        }
        private void LoadData()
        {
            DataSet ds = provider.GetAll();
            gvProduct.DataSource = ds;
            gvProduct.DataBind();
            btnUpdate.Visible = false;
        }
        private void SetProviderValue()
        {
            provider.Code = txtCode.Text;
            provider.Name = txtName.Text;
            provider.Specification = txtSpecification.Text;
            provider.GenericName = txtGenericName.Text;
            provider.DARNo = txtDARNo.Text;
            provider.PackSize = txtPackSize.Text;
            provider.MesurementUnitID = ddlMesurementUnit.SelectedValue.ToInt();
           // provider.NatureID = ddlNatureID.SelectedValue.ToInt();
            provider.DivisionID = ddlDivision.SelectedValue.Toint();
            provider.ItemTypeID = dllItemTypeID.SelectedValue.ToInt();
            provider.SafetyStock = txtSafetyStock.Text.ToDecimal();
            provider.MRP = txtMRP.Text.ToDecimal();
            provider.TradePrice = txtTradePrice.Text.ToDecimal();
            provider.Location = txtLocation.Text;
            provider.EntryUserID = Convert.ToInt16(Session["ID"]);
            provider.UpdateUserID = Convert.ToInt16(Session["ID"]); 
            provider.StatusID = ddlStatus.SelectedValue.ToInt();
        }
        private void Save()
        {
            SetProviderValue();
            if (provider.Save())
            {
                Clear();
                this.AlertSuccess(lblMsg, "Data saved successfully");
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage("Data not saved");
            }
        }
        private void Update()
        {
            provider.ID = holdID.Value.ToInt();
            SetProviderValue();
            if (provider.Update())
            {
                Clear();
                this.AlertSuccess(lblMsg, "Data Updated successfully");
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage("No data Updated");
            }
        }
        private void Delete()
        {
            provider.ID = holdID.Value.ToInt();
            if (provider.Delete())
            {
                Clear();
                this.AlertSuccess(lblMsg, "Data Deleted successfully");
                LoadData();
            }
            else
            {
                MessageHelper.ShowAlertMessage("No data deleted");
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
                Delete();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            Clear();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string searchText;
                if (ddlSearch.SelectedValue == "1")
                {
                    searchText = "A.[Code] like '%" + txtSearch.Text.Trim() + "%'";
                }
                else
                {
                    searchText = "A.[Name] like '%" + txtSearch.Text.Trim() + "%'";
                }
                DataSet dSet = provider.GetAllByFilterExpression(searchText);
                if (dSet != null && dSet.Tables[0].Columns.Count > 0)
                {
                    gvProduct.DataSource = dSet.Tables[0];
                    gvProduct.DataBind();
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
        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            try
            {
                if (gvProduct.SelectedIndex != -1)
                {
                    GridViewRow row = gvProduct.SelectedRow;
                    Label lblCode = (Label)row.FindControl("lblCode");
                    Label lblName = (Label)row.FindControl("lblName");
                    Label lblCostPrice = (Label)row.FindControl("lblCostPrice");
                    //---------------------------HiddenField  select---------------------------------------------
                    HiddenField hfID = (HiddenField)row.FindControl("hfID");
                    HiddenField hfMesurementUnitID = (HiddenField)row.FindControl("hfMesurementUnitID");
                    HiddenField hfNatureID = (HiddenField)row.FindControl("hfNatureID");
                    HiddenField hfSpecifiation = (HiddenField)row.FindControl("hfSpecifiation");
                    HiddenField hfLocation = (HiddenField)row.FindControl("hfLocation");
                    HiddenField hfItemTypeID = (HiddenField)row.FindControl("hfItemTypeID");
                    HiddenField hfDivisionID = (HiddenField)row.FindControl("hfDivisionID");
                    HiddenField hfSafetyStock = (HiddenField)row.FindControl("hfSafetyStock");
                    HiddenField hfStatusID = (HiddenField)row.FindControl("hfStatusID");
                    HiddenField hfDARNo = (HiddenField)row.FindControl("hfDARNo");
                    HiddenField hfMRP = (HiddenField)row.FindControl("hfMRP");
                    HiddenField hfTradePrice = (HiddenField)row.FindControl("hfTradePrice");
                    HiddenField hfPackSize = (HiddenField)row.FindControl("hfPackSize");
                    HiddenField hfGenericName = (HiddenField)row.FindControl("hfGenericName");
                    //-----------------------Selected item Display To Text or Dropdown List ------------------------------------
                    holdID.Value = hfID.Value;
                    txtCode.Text = lblCode.Text;
                    txtName.Text = lblName.Text;
                    txtLocation.Text = hfLocation.Value;
                    txtSpecification.Text = hfSpecifiation.Value;
                    if (txtSpecification.Text != "")
                    {
                        Specification.Visible = true;
                        txtSpecification.Text = hfSpecifiation.Value;
                    }
                    if (hfDARNo.Value != "")
                    {
                        Specification.Visible = false;
                        DARNo.Visible = true;
                        MRP.Visible = true;
                        TradePrice.Visible = true;
                        lblGenericName.Visible = true;
                        txtGenericName.Visible = true;
                        lblPacksize.Visible = true;
                        txtPackSize.Visible = true;
                        txtDARNo.Text = hfDARNo.Value;
                        txtMRP.Text = hfMRP.Value;
                        txtTradePrice.Text = hfTradePrice.Value;
                        txtGenericName.Text = hfGenericName.Value;
                        txtPackSize.Text = hfPackSize.Value;
                    }
                    else
                    {
                        DARNo.Visible = false;
                        MRP.Visible = false;
                        TradePrice.Visible = false;
                        lblGenericName.Visible = false;
                        txtGenericName.Visible = false;
                        lblPacksize.Visible = false;
                        txtPackSize.Visible = false;
                        
                    }
                    ddlMesurementUnit.SelectedValue = hfMesurementUnitID.Value;
                    //ddlNatureID.SelectedValue = hfNatureID.Value;
                    dllItemTypeID.SelectedValue = hfItemTypeID.Value;
                    ddlDivision.SelectedValue = hfDivisionID.Value;
                    txtSafetyStock.Text = hfSafetyStock.Value;
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
        protected void txtCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }
        #endregion

        protected void gvProduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvProduct.PageIndex = e.NewPageIndex;
            DataSet ds = provider.GetAll();
            gvProduct.DataSource = ds;
            gvProduct.DataBind();
        }
        protected void dllItemTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dllItemTypeID.SelectedIndex == 4)
            {
                Specification.Visible = true;
                lblPacksize.Visible = false;
                txtPackSize.Visible = false;
                lblGenericName.Visible = false;
                txtGenericName.Visible = false;
                DARNo.Visible = false;
                TradePrice.Visible = false;
                MRP.Visible = false;
            }
            else if (dllItemTypeID.SelectedIndex == 2)
            {
                lblPacksize.Visible = true;
                txtPackSize.Visible = true;
                lblGenericName.Visible = true;
                txtGenericName.Visible = true;
                DARNo.Visible = true;
                TradePrice.Visible = true;
                MRP.Visible = true;
                Specification.Visible = false;
            }
            else
            {
                Specification.Visible = false;
                lblPacksize.Visible = false;
                txtPackSize.Visible = false;
                lblGenericName.Visible = false;
                txtGenericName.Visible = false;
                DARNo.Visible = false;
                TradePrice.Visible = false;
                MRP.Visible = false;
            }
        }
    }
}