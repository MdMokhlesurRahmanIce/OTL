using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class DeliveryChallanReportEntity
    {
        public string SupplierName
        {
            get;
            set;
        }
        public string SupplierAddress
        {
            get;
            set;
        }
        public string DeliveryChallanNo
        {
            get;
            set;
        }
        public string POrderNo
        {
            get;
            set;
        }
        public string ReqReferenceNo
        {
            get;
            set;
        }
        public DateTime DeliveryChallanDate
        {
            get;
            set;
        }
        public string DestinationUnit
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public string MeasurementUnitName
        {
            get;
            set;
        }

        public string ItemTypeName
        {
            get;
            set;
        }
        public decimal ProvidedQuantity
        {
            get;
            set;
        }
        public decimal ReceivedQuantity
        {
            get;
            set;
        }
    }
}