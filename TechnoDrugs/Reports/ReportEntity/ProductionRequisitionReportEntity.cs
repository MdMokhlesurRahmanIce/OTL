using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class ProductionRequisitionReportEntity
    {
        public string SupplierName
        {
            get;
            set;
        }
        public string DivisionName
        {
            get;
            set;
        }

        public string ReferenceNo
        {
            get;
            set;
        }
        public DateTime RequisitionDate
        {
            get;
            set;
        }
        public DateTime MfgDate
        {
            get;
            set;
        }
        public DateTime ExpDate
        {
            get;
            set;
        }

        public string RawProductName
        {
            get;
            set;
        }
        public string FinishedProductName
        {
            get;
            set;
        }
        public string BatchNo
        {
            get;
            set;
        }
        public string BatchSize
        {
            get;
            set;
        }
        public decimal RequiredQuantity
        {
            get;
            set;
        }
        public string MeasurementUnitName
        {
            get;
            set;
        }
        public decimal EntryUserID
        {
            get;
            set;
        }
    }
}