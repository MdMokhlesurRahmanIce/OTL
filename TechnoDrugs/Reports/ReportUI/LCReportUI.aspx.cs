using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using PurchaseModule.Provider;
using BaseModule;
using TechnoDrugs.Reports.ReportEntity;
using System.Data;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class LCReportUI : PageBase
    {
        string mode = "";
        public LCReportUI()
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

                string transactionNo = ddlRequistionRef.SelectedItem.ToString();
                string adate = txtDate.Text.Trim().Trim();
                int reportOption = Convert.ToInt32(rdblReportOption.SelectedValue);
                int reportCategory = Convert.ToInt32(rdblReportCategory.SelectedValue);
                if (transactionNo != string.Empty)
                {
                    reportOption = 3;
                }
                string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.LCReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&fromDate=" + fromDate + "&todate=" + todate + "&adate=" + adate + "&transactionNo=" + transactionNo;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        protected void ddlRequisitionDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlRequistionRef.Items.Clear();
            RequisitionProvider requisitionProvider = new RequisitionProvider();
            int divisionID = ddlRequisitionDivision.SelectedValue.Toint();
            //string filterExpression = "R.DivisionID = " + "'" + divisionID + "'";//+ "ORDER BY ProductName";
            string filterExpression = "R.DivisionID = " + "" + divisionID + "";//+ "ORDER BY ProductName";
            DataSet ds =  requisitionProvider.GetDivisioinWiseRequisitionNo(filterExpression);
            ddlRequistionRef.DataSource = ds;
            ddlRequistionRef.DataBind();
        }
    }
}