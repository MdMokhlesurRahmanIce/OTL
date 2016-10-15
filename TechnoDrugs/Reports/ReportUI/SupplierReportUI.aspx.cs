using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using BaseModule;
using SetupModule.Provider;
using TechnoDrugs.Reports.ReportEntity;
using System.Data;



namespace TechnoDrugs.Reports.ReportUI
{
    public partial class SupplierReportUI : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {        
            int SupplierTypeID = ddlSupplierType.SelectedValue.ToInt();
            string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.SupplierReport + "&SupplierTypeID=" + SupplierTypeID;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "'," +
            " null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
        }

        protected void ddlSupplierType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlSupplier.Items.Clear();
            SupplierProvider supplierProvider = new SupplierProvider();
            int supplierTypeID = ddlSupplierType.SelectedValue.Toint();
            DataTable dt = supplierProvider.GetSupplierByTypeID(supplierTypeID);
            ddlSupplier.DataSource = dt;
            ddlSupplier.DataBind();
        }

        protected void btnPriviewSuppProdInfo_Click(object sender, EventArgs e)
        {
            int SupplierID = ddlSupplier.SelectedValue.ToInt();
            string page = "GeneralReportViewerUI.aspx?ReportType=" + ReportType.SupplierProductReport + "&SupplierID=" + SupplierID;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "'," +
            " null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
        }
    }
}