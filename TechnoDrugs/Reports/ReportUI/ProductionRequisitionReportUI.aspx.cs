using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using ProductionModule.Provider;
using TechnoDrugs.Reports.ReportEntity;
using System;
using PurchaseModule.Provider;
using System.Data;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class ProductionRequisitionReportUI : PageBase
    {
        string mode = "";
        public ProductionRequisitionReportUI()
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
                string transactionNo = string.Empty;
                CheckUserAuthentication(mode);
                string fromDate = txtFromDate.Text.Trim();
                int reportOption = 0;
                string todate = txtToDate.Text.Trim();
                reportOption = Convert.ToInt32(rdblReportOption.SelectedValue);
                int reportCategory = Convert.ToInt32(rdblReportCategory.SelectedValue);
                if (txtPurchaseID.Text.ToString() != string.Empty)
                {
                    transactionNo = txtPurchaseID.Text.ToString();
                }
                else
                {
                    transactionNo = ddlPurchaseOrderRef.SelectedItem.Text;
                }                
                
                if (transactionNo != string.Empty)
                {
                    reportOption = 2;
                }
                string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.ProductionRequisitionReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&fromDate=" + fromDate + "&todate=" + todate + "&transactionNo=" + transactionNo;
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
            ProductionRequisitionProvider productionRequisitionProvider = new ProductionRequisitionProvider();
            DataSet ds = productionRequisitionProvider.GetDivisioinWisePONo(ddlProdRequiDivision.SelectedValue.Toint());
            ddlPurchaseOrderRef.DataSource = ds;
            ddlPurchaseOrderRef.DataBind();
        }
    }
}