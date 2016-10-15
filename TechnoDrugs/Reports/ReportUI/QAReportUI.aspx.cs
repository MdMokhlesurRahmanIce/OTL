using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SetupModule.Provider;
using SecurityModule.Provider;
using PurchaseModule.Provider;
using BaseModule;
using TechnoDrugs.Reports.ReportEntity;
using System.Data;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class QAReportUI : PageBase
    {
        string mode = "";
        public QAReportUI()
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
                CheckUserAuthentication(mode);
                string fromDate = txtFromDate.Text.Trim();
                string todate = txtToDate.Text.Trim();
                string date = txtDate.Text.Trim();
                int productID = ddlProduct.SelectedValue.Toint();
                int reportOption = Convert.ToInt32(rdblReportOption.SelectedValue);

                string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.QAReport + "&reportOption=" + reportOption + "&productID=" + productID + "&fromDate=" + fromDate + "&todate=" + todate + "&aDate=" + date;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int divisionID = 0;
            divisionID = Convert.ToInt16(ddlProductDivision.SelectedValue);
            LoadProduct(divisionID);
        }

        private void LoadProduct(int divisionID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new ProductProvider().GetDivisionWiseProduct(divisionID);

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