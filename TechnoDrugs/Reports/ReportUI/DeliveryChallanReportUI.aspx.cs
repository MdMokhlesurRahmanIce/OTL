using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using TechnoDrugs.Reports.ReportEntity;
using System;
using PurchaseModule.Provider;
using System.Data;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class DeliveryChallanReportUI : PageBase
    {
        string mode = "";
        public DeliveryChallanReportUI()
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
                string transactionNo = string.Empty;
                string fromDate = txtFromDate.Text.Trim();
                string todate = txtToDate.Text.Trim();

                int reportOption = Convert.ToInt32(rdblReportOption.SelectedValue);
                if (txtDeliveryChallanNo.Text != string.Empty)
                {
                    transactionNo = txtDeliveryChallanNo.Text;
                }
                else
                {
                    transactionNo = ddlDeliveryChallanNo.SelectedItem.Text;
                    reportOption = 2;
                }                                
                int reportCategory = Convert.ToInt32(rdblReportCategory.SelectedValue);
                string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.DeliveryChallanReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&fromDate=" + fromDate + "&todate=" + todate + "&transactionNo=" + transactionNo;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        protected void dllItemTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlDeliveryChallanNo.Items.Clear();
            DeliveryChallanProvider deliveryChallanProvider = new DeliveryChallanProvider();
            string filterExpression = " WHERE ItemTypeID = " + dllItemTypeID.SelectedIndex.ToString();
            DataSet ds = deliveryChallanProvider.GetAllByFilterExpression(filterExpression);
            ddlDeliveryChallanNo.DataSource = ds;
            ddlDeliveryChallanNo.DataBind();
        }
    }
}