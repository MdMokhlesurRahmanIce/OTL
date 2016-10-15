using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class EngInternalRequiReportEntity
    {
        #region Properties
        public string ProductName
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
        public string MeasurementUnitName
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public string RequiredQuantity
        {
            get;
            set;
        }
        public string PresentStock
        {
            get;
            set;
        }
        public string EntryUserName
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
    }
}