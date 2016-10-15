using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using TechnoDrugs.Reports.ReportEntity;
using BaseModule;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using SetupModule.Provider;

namespace TechnoDrugs.Reports.ReportUI
{
    public partial class CommonReportViewer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable dt;
            var reportPath = new ReportPath();
            var reportParameter = new ReportParameter();
            var reportDocument = new ReportDocument();
            int reportType = Request.QueryString["ReportType"].Toint();
            DateTime fromDate = Request.QueryString["fromDate"].ToDate();
            DateTime toDate = Request.QueryString["toDate"].ToDate();
            DateTime aDate = Request.QueryString["aDate"].ToDate();
            int reportOption = Request.QueryString["reportOption"].Toint();
            int reportCategory = Request.QueryString["reportCategory"].Toint();
            int vatRegID = Request.QueryString["VatRegID"].Toint();
            int productID = Request.QueryString["productID"].Toint();
            int warehouseID = Request.QueryString["warehouseID"].Toint();
            var transactionID = Request.QueryString["transactionNo"];
            string printOption = Request.QueryString["printOption"];

            //******** Updated Datetime ********//
            DateTime? fDate;
            DateTime? tDate;
            DateTime? adate;
            int? vatRegiID;
            int? warehouseid;
            int? productid;
            int? reportoption;
            int? reportcategory;

            string transactionid;

            switch (reportType)
            {
                #region  Supplier Report
                case ReportType.SupplierReport:
                    var supplierProvider = new SuppliersProvider();
                    int supplierTypeID = Request.QueryString["SupplierTypeID"].ToInt();
                    dt = supplierProvider.GetSupplierInfoByTypeID(supplierTypeID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        reportDocument.Load(reportPath.SupplierReportPath);
                        GetValue(dt, reportParameter, reportDocument);
                    }
                    else
                    {
                        GetMsg(reportParameter, reportDocument, reportPath);
                    }
                    break;
                #endregion                
            }
        }    
        private string ToZero()
        {
            return Request.QueryString["transactionNo"].IfEmptyOrNullThenStringZero();
        }
        private void GetValue(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            uniVatCrystalReport.ReportSource = reportDocument;
            uniVatCrystalReport.DataBind();
        }
        private void GetAddressValue(ReportParameter reportParameter, ReportDocument reportDocument)
        {
            // if (GetByFilterExpression()=="0")
            // {
            reportDocument.SetParameterValue("CompanyName", reportParameter.CompanyName);
            reportDocument.SetParameterValue("CompanyAddress1", reportParameter.CompanyAddress1);
            reportDocument.SetParameterValue("CompanyAddress2", reportParameter.CompanyAddress2);
            reportDocument.SetParameterValue("CompanyTelephone", reportParameter.CompanyTelephone);
            reportDocument.SetParameterValue("PowerdBy", reportParameter.PowerdBy);

        }
        private void GetMsg(ReportParameter reportParameter, ReportDocument reportDocument, ReportPath reportPath)
        {
            reportDocument.Load(reportPath.MsgCReportPath);
            GetAddressValue(reportParameter, reportDocument);
            reportDocument.SetParameterValue("Notfind", reportParameter.NotFind);
            uniVatCrystalReport.ReportSource = reportDocument;
            uniVatCrystalReport.DataBind();
        }
        private SqlDataAdapter _da;
        private DataTable dt;
        private SqlCommand command;  
    }
}