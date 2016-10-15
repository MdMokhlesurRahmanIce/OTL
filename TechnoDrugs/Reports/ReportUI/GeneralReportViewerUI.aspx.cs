using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using SecurityModule.DataAccess;
using SetupModule.Provider;
using BaseModule;
using PurchaseModule.Provider;
using ProductionModule.Provider;
using TechnoDrugs.Reports.ReportEntity;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;
using TechnodrugsModule.ReportProvider;


namespace TechnoDrugs.Reports.ReportUI
{
    public partial class GeneralReportViewerUI : System.Web.UI.Page
    {
        ReportDocument rpt = new ReportDocument();
        protected override void OnUnload(EventArgs e)
        {
            rpt.Close();
            rpt.Dispose();
            base.OnUnload(e);
        }
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
            int productID = Request.QueryString["productID"].Toint();
            var transactionID = Request.QueryString["transactionNo"];
            string printOption = Request.QueryString["printOption"];

            //******** Updated Datetime ********//
            DateTime? fDate;
            DateTime? tDate;
            DateTime? Date;            
            
            switch (reportType)
            {
                #region  Supplier Report
                case ReportType.SupplierReport:      //////////// For Supplier Info report
                    var supplierProvider = new SupplierProvider();
                    int supplierTypeID = Request.QueryString["SupplierTypeID"].ToInt();
                    dt = supplierProvider.GetSupplierByTypeID(supplierTypeID);
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

                case ReportType.SupplierProductReport:      //////////// For Supplier Product report
                    supplierProvider = new SupplierProvider();
                    int supplierID = Request.QueryString["SupplierID"].ToInt();
                    dt = supplierProvider.GetByID(supplierID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        reportDocument.Load(reportPath.SupplierProductReportPath);
                        GetValue(dt, reportParameter, reportDocument);
                    }
                    else
                    {
                        GetMsg(reportParameter, reportDocument, reportPath);
                    }
                    break;
                #endregion

                #region  Requisition Report
                case ReportType.RequisitionReport:   //////////// For Requisition report
                    var requisitionProvider = new RequisitionProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    Date = GetNullaDatetime();
                    if (reportOption == 3)
                    {
                        var requisitionNo = Request.QueryString["transactionNo"].Trim();
                        dt = requisitionProvider.GetByID(requisitionNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.RequisitionReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else
                    {
                        dt = requisitionProvider.GetAllByDateWise(productID, transactionID, fDate, tDate, Date, reportOption);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            if (reportOption == 2)
                            {
                                reportDocument.Load(reportPath.RequisitionReportPath);
                                GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                            }
                            else if (reportOption == 1)
                            {
                                reportDocument.Load(reportPath.RequisitionReportPath);
                                GetValueAGetDate(dt, reportParameter, reportDocument, Date);
                            }
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    break;
                #endregion

                #region Purchase Order Report
                case ReportType.PurchaseOrderReport:   //////////// For Purchase Order report
                    var purchaseOrderProvider = new PurchaseOrderProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();                   
                    if (reportCategory == 1)
                    {                        
                        var purchaseOrderNo = Request.QueryString["transactionNo"].Trim();
                        dt = purchaseOrderProvider.GetByID(purchaseOrderNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            decimal value = 0.00M;
                            decimal totalValue = 0.00M;
                            foreach(DataRow dr in dt.Rows)
                            {
                                value = Convert.ToDecimal(dr["Value"]);
                                totalValue = totalValue +  value;
                            }
                            string textValue = NumberToText(Convert.ToInt32(totalValue), true);

                            System.Data.DataColumn newColumn = new System.Data.DataColumn("TotalAmountInText", typeof(System.String));
                            newColumn.DefaultValue = textValue;
                            dt.Columns.Add(newColumn);


                            reportDocument.Load(reportPath.PurchaseOrderReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else if (reportCategory == 2)  //// Purchase Order Detail Report - Single Product/////////////////////
                    {
                        dt = purchaseOrderProvider.GetAllData(productID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.PurchaseOrderDetailReportPath);
                            GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    break;
                #endregion

                #region Delivery Challan Report
                case ReportType.DeliveryChallanReport:   //////////// For Delivery Challan Report
                    var deliveryChallanProvider = new DeliveryChallanProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    if (reportOption == 2)
                    {
                        var deliveryChallanNo = Request.QueryString["transactionNo"].Trim();
                        dt = deliveryChallanProvider.GetByID(deliveryChallanNo);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.DeliveryChallanReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else
                    {
                        //dt = purchaseOrderProvider.GetAllByDateWise(productID, transactionID, fDate, tDate, adate, reportOption);
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //    if (reportOption == 2)
                        //    {
                        //        reportDocument.Load(reportPath.RequisitionReportPath);
                        //        GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                        //    }
                        //    else if (reportOption == 1)
                        //    {
                        //        reportDocument.Load(reportPath.RequisitionReportPath);
                        //        GetValueAGetDate(dt, reportParameter, reportDocument, adate);
                        //    }
                        //}
                        //else
                        //{
                        //    GetMsg(reportParameter, reportDocument, reportPath);
                        //}
                    }
                    break;
                #endregion

                #region Production Requisition Report
                case ReportType.ProductionRequisitionReport:   
                    var productionRequisitionProvider = new ProductionRequisitionProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    if (reportOption == 2)
                    {
                        //var prodReqNo = Request.QueryString["transactionNo"].Trim();
                        //dt = productionRequisitionProvider.GetByID(prodReqNo);
                        //if (dt != null && dt.Rows.Count > 0)
                        //{
                        //    reportDocument.Load(reportPath.ProductionRequisitionReportPath);
                        //    GetValueWithDate(dt, reportParameter, reportDocument);
                        //}
                        //else
                        //{
                        //    GetMsg(reportParameter, reportDocument, reportPath);
                        //}
                    }
                    else
                    {
                        
                    }
                    break;
                #endregion 

                #region Prodct Stock Report
                case ReportType.ProductCurrentStockReport:
                    var productCurrentStockProvider = new ProductCurrentStockProvider
                                                   {
                                                       ProductID = Request.QueryString["ProductID"].ToInt(),
                                                       DivisionID = Request.QueryString["DivisionID"].ToInt(),
                                                       ProductType = Request.QueryString["ProductType"].ToInt(),
                                                       FromDate =  Request.QueryString["fromDate"].ToString(),
                                                       Todate = Request.QueryString["todate"].ToString()
                                                   };
                    if (productCurrentStockProvider.ProductID == 0)
                    {
                        dt = productCurrentStockProvider.GetDivisionAndItemwise();
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.AllProductStockReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else
                    {
                        var mushak16ProviderList = productCurrentStockProvider.GetAll();
                        if (mushak16ProviderList != null)
                        {
                            reportDocument.Load(reportPath.ProductIndividualStockReportPath);
                            reportDocument.SetDataSource(mushak16ProviderList);
                            GetAddressValue(reportParameter, reportDocument);
                            technoCrystalReport.ReportSource = reportDocument;
                            technoCrystalReport.DataBind();
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }           
                    break;
                #endregion 

                #region LC Report
                case ReportType.LCReport:   //////////// For LC report
                    var lCProvider = new LCProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    if (reportCategory == 2)   /////////////// LC Detail Report ///////////////
                    {
                        dt = lCProvider.GetByDateRangeWise(fromDate, toDate, reportCategory);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.LCDetailReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else if (reportCategory == 1)  //// LC Summary Report /////////////////////
                    {
                        dt = lCProvider.GetByDateRangeWise(fromDate, toDate, reportCategory);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.LCSummaryReportPath);
                            GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    break;
                #endregion

                #region BOE Report
                case ReportType.BOEReport:   //////////// For BOE report
                    var bOEProvider = new BOEProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    if (reportCategory == 2)
                    {
                        dt = bOEProvider.GetByDateRangeWise(fromDate, toDate, reportCategory);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.BOEDetailReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    else if (reportCategory == 1)  //// BOE Summary Report /////////////////////
                    {
                        dt = bOEProvider.GetByDateRangeWise(fromDate, toDate, reportCategory);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            reportDocument.Load(reportPath.BOESummaryReportPath);
                            GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }
                    }
                    break;
                #endregion

                #region QA Report
                case ReportType.QAReport:   //////////// For QA report
                    var qAQCRequisitionProvider = new QAQCRequisitionProvider();
                    fDate = GetNullfDatetime();
                    tDate = GetNulltDatetime();
                    Date = GetNullaDatetime();

                    dt = qAQCRequisitionProvider.GetDateWiseProductInfo(fDate, tDate, Date, productID);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            {
                                //if (reportOption == 1 && adate == null) //All product summary report
                                //{
                                //    reportDocument.Load(reportPath.QAReportPath);
                                //    GetValueWithDate(dt, reportParameter, reportDocument);
                                //}
                                //if (reportOption == 1 && adate == null) //All product summary report
                                //{
                                //    reportDocument.Load(reportPath.QAReportPath);
                                //    GetValueWithDate(dt, reportParameter, reportDocument);
                                //}
                                if (reportOption == 1)
                                {
                                    reportDocument.Load(reportPath.QAReportPath);
                                    GetValueAGetDate(dt, reportParameter, reportDocument, Date);
                                }
                                else if (reportOption == 2)
                                {
                                    reportDocument.Load(reportPath.QAReportPath);
                                    GetValueWithFromToDate(reportParameter, reportDocument, dt, fDate, tDate);
                                }
                                //else if (reportOption == 3) // Purchase ID wise report (showing product details).
                                //{
                                //    reportDocument.Load(reportPath.QAReportPath);
                                //    GetValueWithDate(dt, reportParameter, reportDocument, printOption);
                                //}
                            }
                        }
                        else
                        {
                            GetMsg(reportParameter, reportDocument, reportPath);
                        }                    
                    break;
                #endregion

                #region Eng. Internal Requisition Report
                case ReportType.EngineeringInternalRequiReport:   //////////// For Eng Requi report
                    var engRequisitionProvider = new EngineeringRequisitionProvider();
                    var engRequisitionNo = Request.QueryString["transactionNo"].Trim();
                    dt = engRequisitionProvider.GetByID(engRequisitionNo);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        {
                            reportDocument.Load(reportPath.EngInternalRequiReportPath);
                            GetValueWithDate(dt, reportParameter, reportDocument);
                        }
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
        private void GetValueAGetDate(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument, DateTime? adate)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            //reportDocument.SetParameterValue("ADate", String.Format("{0: dd MMM yyyy}", adate));
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
        }
        private void GetValueWithFromToDate(ReportParameter reportParameter, ReportDocument reportDocument, DataTable dt, DateTime? fromdate, DateTime? todate)
        {
            object dateBetween = GetDateBetween(fromdate, todate);
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
          //  reportDocument.SetParameterValue("ADate", dateBetween);
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
        }
        private void GetValue(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
        }
        private void GetValue(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument, string printOption)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
            //SetPrintValue(reportDocument, printOption);
            // Response.Redirect("../../UI/UniVAT/CreditNoteUI.aspx?");

        }
        private void GetValueWithDate(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            //reportDocument.SetParameterValue("ADate", string.Format("{}", adat);

            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
        }
        private DateTime? GetNullfDatetime()
        {   
            return Request.QueryString[QAReportEntity.FromDateField].ToString().ToNullableDateTime();
        }
        private DateTime? GetNulltDatetime()
        {
            return Request.QueryString[QAReportEntity.ToDateField].ToString().ToNullableDateTime();
        }
        private DateTime? GetNullaDatetime()
        {
            return Request.QueryString[QAReportEntity.ADateField].ToString().ToNullableDateTime();
        }
        private void GetValueWithDate(DataTable dt, ReportParameter reportParameter, ReportDocument reportDocument, string printOption)
        {
            reportDocument.SetDataSource(dt);
            GetAddressValue(reportParameter, reportDocument);
            reportDocument.SetParameterValue("ADate", "");
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
            //SetPrintValue(reportDocument, printOption);
            // Response.Redirect("../../UI/UniVAT/PurchaseUI.aspx?");
        }        
        private void GetMsg(ReportParameter reportParameter, ReportDocument reportDocument, ReportPath reportPath)
        {
            reportDocument.Load(reportPath.MsgCReportPath);
            GetAddressValue(reportParameter, reportDocument);
            reportDocument.SetParameterValue("Notfind", reportParameter.NotFind);
            technoCrystalReport.ReportSource = reportDocument;
            technoCrystalReport.DataBind();
        }
        private void GetAddressValue(ReportParameter reportParameter, ReportDocument reportDocument)
        {
            // if (GetByFilterExpression()=="0")
            // {
            reportDocument.SetParameterValue("CompanyName", reportParameter.CompanyName);
            reportDocument.SetParameterValue("CompanyAddress1", reportParameter.CompanyAddress1);
            reportDocument.SetParameterValue("CompanyAddress2", reportParameter.CompanyAddress2);
            reportDocument.SetParameterValue("CompanyTelephone", reportParameter.CompanyTelephone);
            reportDocument.SetParameterValue("FaxNo", reportParameter.FaxNo);
            reportDocument.SetParameterValue("Email", reportParameter.Email);
            reportDocument.SetParameterValue("PowerdBy", reportParameter.PowerdBy);
        }
        private object GetDateBetween(DateTime? fromdate, DateTime? todate)
        {
            return "From : " + String.Format("{0: dd MMM yyyy}", fromdate) + " - To : " + String.Format("{0: dd MMM yyyy}", todate);
        }
        public static string NumberToText(int number, bool isUK)
        {
            if (number == 0) return "Zero";
           string and = isUK ? "and " : ""; // deals with UK or US numbering
            //if (number == -2147483648) return "Minus Two Billion One Hundred " + and +
            //"Forty Seven Million Four Hundred " + and + "Eighty Three Thousand " +
            //"Six Hundred " + and + "Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Million ", "Billion " };
            num[0] = number % 1000;           // units
            num[1] = number / 1000;
            num[2] = number / 1000000;
            num[1] = num[1] - 1000 * num[2];  // thousands
            num[3] = number / 1000000000;     // billions
            num[2] = num[2] - 1000 * num[3];  // millions
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10;              // ones
                t = num[i] / 10;
                h = num[i] / 100;             // hundreds
                t = t - 10 * h;               // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i < first) sb.Append(and);
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();
        }

        //private SqlDataAdapter _da;
        //private DataTable dt;
        //private SqlCommand command;
        //private BaseDataAccess access;       
    }
}