using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class BOESummaryReportEntity
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
        public string ExcRate
        {
            get;
            set;
        }
        public decimal LcValue
        {
            get;
            set;
        }
        public string BOEDate
        {
            get;
            set;
        }
        public string BillOfEntryNo
        {
            get;
            set;
        }
        public decimal AssmntValueAsPerBOE
        {
            get;
            set;
        }
        public decimal DutyNCharge
        {
            get;
            set;
        }
        public decimal AitDeducted
        {
            get;
            set;
        }

    }
}