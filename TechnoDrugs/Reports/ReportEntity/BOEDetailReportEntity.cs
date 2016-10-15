using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class BOEDetailReportEntity
    {
        public string LCNo
        {
            get;
            set;                   
        }
        public string LCOpeningDate
        {
            get;
            set;
        }
        public string Name 
        {
            get;
            set;
        }
        public string BOEDate 
        {
            get;
            set;
        }
        public decimal AssessmentValue 
        {
            get;
            set;
        }
        public decimal Quantity
        {
            get;
            set;
        }
        public decimal BOEValue   
        {
            get;
            set;
        }
        public decimal CDAmt 
        {
            get;
            set;
        }
        public decimal VATAmt
        {
            get;
            set;
        }
        public decimal AITAmt
        {
            get;
            set;
        }
        public decimal DFCVATFPAmt
        {
            get;
            set;
        }
        public decimal OtherTax 
        {
            get;
            set;
        }
        public string TotalExpenditure 
        {
            get;
            set;
        }
    }
}