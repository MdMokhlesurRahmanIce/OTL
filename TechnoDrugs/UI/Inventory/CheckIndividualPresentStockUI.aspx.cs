using SetupModule.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using PurchaseModule.Provider;
using System.Data;

namespace TechnoDrugs.UI.Inventory
{
    public partial class CheckIndividualPresentStockUI : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnShowStock_Click(object sender, EventArgs e)
        {
            ProductProvider productProvider = new ProductProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string measurementUnit = productProvider.GetMeasurementUnit(ddlProductValidation.SelectedValue.Toint());
            Decimal productQuantity = productProvider.GetPresentStock(ddlProductValidation.SelectedValue.Toint());
            lblProductNameDisplay.Text = ddlProductValidation.SelectedItem.Text;
            lblUnit.Text = measurementUnit;
            lblQuantity.Text = productQuantity.ToString();

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

        protected void btnShowReqInfo_Click(object sender, EventArgs e)
        {
            RequisitionProvider requisitionProvider = new RequisitionProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            DataTable dt = requisitionProvider.GetRequisitionwisePOInfo(ddlProductValidation.SelectedValue.Toint());
            lblProductNameDisplay.Text = ddlProductValidation.SelectedItem.Text;
            gvReqPOInfo.DataSource = dt;
            gvReqPOInfo.DataBind();
            
        }
    }
}