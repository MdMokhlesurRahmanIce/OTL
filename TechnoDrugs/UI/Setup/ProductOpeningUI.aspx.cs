using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SetupModule.Provider;
using System.Data;
using BaseModule;
using TechnoDrugs.Helper;

namespace TechnoDrugs.UI.Setup
{
    public partial class ProductOpeningUI : System.Web.UI.Page
    {
        #region Page_Load
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
        private ProductOpeningProvider provider = new ProductOpeningProvider();
        
        private void Clear()
        {
            lblMsg.InnerText = string.Empty;
            holdID.Value = string.Empty;
            tbOpeningQty.Text = "0";
            //tbOpeningAmount.Text = "0";
            txtLocation.Text = string.Empty;
            ddlDivision.SelectedIndex = 0;
            provider = new ProductOpeningProvider();
            LoadData();
        }
        private bool SetProviderValue()
        {
            decimal qty;
            if (!decimal.TryParse(tbOpeningQty.Text, out qty) | qty.Equals(0))
            {
                MessageHelper.ShowAlertMessage("Invalid Opening Quatity");
                return false;
            }
            //decimal amount;
            //if (!decimal.TryParse(tbOpeningAmount.Text, out amount))
            //{
            //    MessageHelper.ShowAlertMessage("Invalid Opening Amount");
            //    return false;
            //}
            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            provider.ProductId = ddlProduct.SelectedValue.ToInt();
            provider.OpeningQty = qty;
            //provider.OpeningAmount = amount;
            provider.Locatioin = txtLocation.Text;
            
            return true;
        }
        private DataSet LoadData()
        {
            DataSet ds = null;// new DataSet();
                ds = provider.GetAll();
            lvProductOpening.DataSource = ds;
            lvProductOpening.DataBind();
            return ds;
        }
        private void Save()
        {
            if (!SetProviderValue()) return;
            {
                if (provider.Save())
                {
                    this.AlertSuccess(lblMsg, "Data saved successfully");
                }
                else
                {
                    MessageHelper.ShowAlertMessage("No Data saved");
                }
            }
        }
        #endregion

        #region Event
        protected void btnUnit_Click(object sender, EventArgs e)
        {
            ProductProvider productProvider = new ProductProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string measurementUnit = productProvider.GetMeasurementUnit(ddlProductValidation.SelectedValue.Toint());
            lblUnit.Text = measurementUnit;           
        }  
        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            try
            {
                Save();
                provider = new ProductOpeningProvider();
                LoadData();
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
        #endregion
               
        protected void btnDelete2_Click(object sender, EventArgs e)
        {
            Button btnDelete = sender as Button;
            GridViewRow deleteRow = (GridViewRow)btnDelete.NamingContainer;
            int rowIndex = deleteRow.RowIndex;
            foreach (GridViewRow row in lvProductOpening.Rows)
            {
                if (row.RowIndex == rowIndex)
                {
                    HiddenField hfId = (HiddenField)row.FindControl("hfID");
                    provider.ID = hfId.Value.Toint();
                    provider.Delete();
                }
            }
            LoadData();
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            Session["Value"] = ddlDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = Convert.ToInt16(ddlDivision.SelectedValue);
            LoadProduct(codeOrName, productType, divisionID);
        }
        private void LoadProduct(int codeName, int productType, int divisionID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new ProductProvider().GetProductForProductSearch(codeName, productType, divisionID);

                AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");

                ddlProduct.DataSource = null;
                ddlProduct.DataBind();

                ddlProduct.DataSource = dt;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ID";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("----------Select Product----------", "0"));
                ddlProduct.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }
        protected void lvProductOpening_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            lvProductOpening.PageIndex = e.NewPageIndex;
            DataSet ds = provider.GetAll();
            lvProductOpening.DataSource = ds;
            lvProductOpening.DataBind();
        }
        protected void btnCommonProducts_Click(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            // Session["Value"] = ddlProductDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = 30;
            LoadProduct(codeOrName, productType, divisionID);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                ProductProvider provider = new ProductProvider();
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
                    lvProductOpening.DataSource = dSet.Tables[0];
                    lvProductOpening.DataBind();
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
        
    }
}