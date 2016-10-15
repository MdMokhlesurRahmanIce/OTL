using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using TechnoDrugs.Reports.ReportEntity;
using System;
using PurchaseModule.Provider;
using System.Data;
using SetupModule.Provider;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class PurchaseOrderReportUI : PageBase
    {
        string mode = "";
        public PurchaseOrderReportUI()
           {
               RequiresAuthorization = true;
           }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                mode = "Preview";
                string transactionNo = ddlPurchaseOrderRef.SelectedItem.Text;
                CheckUserAuthentication(mode);
                string fromDate = txtFromDate.Text.Trim();
                int reportOption = 0;
                string todate = txtToDate.Text.Trim();
                //reportOption = Convert.ToInt32(rdblReportOption.SelectedValue);
                int reportCategory = Convert.ToInt32(rdblReportCategory.SelectedValue);
                
                AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");            
                string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.PurchaseOrderReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&fromDate=" + fromDate + "&todate=" + todate + "&transactionNo=" + transactionNo + "&productID=" + ddlProductValidation.SelectedValue.Toint();
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void ddlPurchaseOrderDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPurchaseOrderRef.Items.Clear();
            PurchaseOrderProvider purchaseOrderProvider = new PurchaseOrderProvider();            
            DataSet ds = purchaseOrderProvider.GetDivisioinWisePONo(ddlPurchaseOrderDivision.SelectedValue.Toint());
            ddlPurchaseOrderRef.DataSource = ds;
            ddlPurchaseOrderRef.DataBind();

            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            Session["Value"] = ddlPurchaseOrderDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = Convert.ToInt16(ddlPurchaseOrderDivision.SelectedValue);
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
    }
}