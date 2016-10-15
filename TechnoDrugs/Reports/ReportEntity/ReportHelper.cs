using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{

    public class ReportType
    {
        public const Int16 SupplierReport = 1;
        public const Int16 RequisitionReport = 2;
        public const Int16 PurchaseOrderReport = 3;
        public const Int16 DeliveryChallanReport = 4;
        public const Int16 ProductionRequisitionReport = 5;
        public const Int16 ProductCurrentStockReport = 6;
        public const Int16 SupplierProductReport = 7;
        public const Int16 LCReport = 8;
        public const Int16 BOEReport = 9;
        public const Int16 QAReport = 10;
        public const Int16 EngineeringInternalRequiReport = 11;
    }

    public class ReportPath
    {   
        public string SupplierReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/SupplierReportDoc.rpt");

        public string SupplierProductReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/SupplierProductReportDoc.rpt");

        public string RequisitionReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/RequisitionReportDoc.rpt");
        
        public string PurchaseOrderReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/PurchaseOrderReportDoc.rpt");

        public string PurchaseOrderDetailReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/PurchaseOrderDetailReportDoc.rpt");

        public string DeliveryChallanReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/DeliveryChallanReport.rpt");

        public string ProductionRequisitionReportPath =
        HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/ProductionRequisitionReport.rpt");

        public string ProductIndividualStockReportPath =
        HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/ProductCurrentStockReport_Single.rpt");

        public string AllProductStockReportPath =
        HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/AllProductStockReport.rpt");

        public string LCDetailReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/LCDetailReportDoc.rpt");
        public string LCSummaryReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/LCSummaryReport.rpt");
        public string BOEDetailReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/BOEDetailReport.rpt");
        public string BOESummaryReportPath =
            HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/BOESummaryReport.rpt");

        public string QAReportPath =
           HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/QAReportDoc.rpt");

        public string EngInternalRequiReportPath =
           HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/EngInternalRequiReportDoc.rpt");
    
        public string MsgCReportPath = HttpContext.Current.Request.MapPath("~/Reports/ReportDoc/MsgCReport.rpt");
    }

    public class ReportParameter
    {
        public string CompanyName = "Techno Drugs Ltd.";
        public string CompanyAddress1 = "31, Segunbagicha, Dhaka-1000";
        public string CompanyAddress2 = "Satirpara, Narsingdi, Phone: 0628-62842-43";
        public string CompanyVatRegNo = "18131018106";
        public string CompanyTelephone = "02-8356037,8355821";
        public string FaxNo = "088-02-8356038";
        public string Email = "techno_drugs@hotmail.com";

        public string PowerdBy = "Developed By On Track Link.";
        public string NotFind = "Data Not Available";       
    }

    public class Constants
    {
        #region Constants
        public const string ProductIDField = "ProductID";
        public const string FromDateField = "FromDate";
        public const string ToDateField = "ToDate";
        public const string ADateField = "ADate";
        #endregion
    }


}