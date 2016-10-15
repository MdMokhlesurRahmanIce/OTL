using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using SetupModule.Provider;

namespace TechnoDrugs.UI.Purchase
{
    public partial class ProductSearch : System.Web.UI.UserControl
    {
        int divisionID = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadProduct(1, 1, 1);
            }            
        }
        protected void rbProductCodeName_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Value"] != null)
            {
                divisionID = Convert.ToInt16(Session["Value"]);
            }
            int codeOrName = 0;
            int productType = 0;

            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            LoadProduct(codeOrName, productType, divisionID);
        }
        protected void rbProductType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (Session["Value"] != null)
            {
                divisionID = Convert.ToInt16(Session["Value"]);
            }

            int codeOrName = 0;
            int productType = 0;

            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            LoadProduct(codeOrName, productType, divisionID);
        }

        private void LoadProduct(int codeName, int productType, int divisionID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new ProductProvider().GetProductForProductSearch(codeName, productType, divisionID);

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
        public void Clear()
        {
            rbProductCodeName.SelectedValue = "1";
            rbProductType.SelectedValue = "1";
            LoadProduct(1, 1, 1);
        }
        public void ExternalCall()
        {
            int codeOrName = 0;
            int productType = 0;

            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);

            if (Session["Value"] != null)
            {
                divisionID = Convert.ToInt16(Session["Value"]);
            }
            LoadProduct(codeOrName, productType, divisionID);          
        }
    }
}
