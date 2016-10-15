using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class SupplierReportEntity
    {
        public string SupplierName  
        {
            get;
            set;
        }
        public string ContactName
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string Mobile
        {
            get;
            set;
        }
        //public string VATRegNo
        //{
        //    get;
        //    set;
        //}
        //public string TarrifName
        //{
        //    get;
        //    set;
        //}
        public string POrderNo
        {
            get;
            set;
        }
        public string PurchaseOrderDate
        {
            get;
            set;
        }
        //public string ProductID
        //{
        //    get;
        //    set;
        //}
        public decimal Quantity
        {
            get;
            set;
        }
        public decimal Rate
        {
            get;
            set;
        }
        public string SupplierChallanNo
        {
            get;
            set;
        }

        //public string SupplierID
        //{
        //    get;
        //    set;
        //}
        public decimal ProvidedQuantity
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        
    }
}