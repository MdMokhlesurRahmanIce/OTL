using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class QAReportEntity
    {
        #region Properties        
        public string ProductName
        {
            get;
            set;
        }
        public decimal RequiredQuantity
        {
            get;
            set;
        }
        public string ReferenceNo
        {
            get;
            set;
        }
        public string RequisitionDate
        {
            get;
            set;
        }
        public decimal SentQuantity
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public string ReportHeader
        {
            get;
            set;
        }
        #endregion
        #region Constants
        public const string VatRegistrationIDField = "VatRegistrationID";
        public const string WarehouseIDField = "WarehouseID";
        public const string ProductIDField = "ProductID";
        public const string FromDateField = "FromDate";
        public const string ToDateField = "ToDate";
        public const string ADateField = "ADate";
        #endregion
    }
}