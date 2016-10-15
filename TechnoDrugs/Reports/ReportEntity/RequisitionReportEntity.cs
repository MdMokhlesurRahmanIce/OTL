using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class RequisitionReportEntity
    {
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
        public string MonthlyConsumeQty
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
        public string CheckUserName
        {
            get;
            set;
        }
        public string ApproveUserName
        {
            get;
            set;
        }
    }
}