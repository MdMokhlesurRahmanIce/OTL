using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class ProductCurrentStockReportEntity
    {
        public string ProductName
        {
            get;
            set;
        }
        public decimal OpeningQuantity
        {
            get;
            set;
        }
        public decimal ClosingQuantity
        {
            get;
            set;
        }
        public decimal StockInQunatity
        {
            get;
            set;
        }
        public string MeasurementUnit
        {
            get;
            set;
        }
        public decimal TradePrice
        {
            get;
            set;
        }
        public decimal OtherReceive
        {
            get;
            set;
        }
        public decimal GeneralSample
        {
            get;
            set;
        }
        public decimal SpecialSample
        {
            get;
            set;
        }
        public decimal ReturnQty
        {
            get;
            set;
        }
        public decimal OtherOutQty
        {
            get;
            set;
        }
        public decimal BrokenOrDamageQty
        {
            get;
            set;
        }
                
        public string DeliveryChallanNo
        {
            get;
            set;
        }
        public decimal StockOutQuantity
        {
            get;
            set;
        }
        public string SupplierName
        {
            get;
            set;
        }
        public string BatchNo
        {
            get;
            set;
        }
        public string PackSize
        {
            get;
            set;
        }
        public string ProductCode
        {
            get;
            set;
        }
        public DateTime TransactionDate
        {
            get;
            set;
        }
        public DateTime FromDate
        {
            get;
            set;
        }
        public DateTime ToDate
        {
            get;
            set;
        }
    }
}