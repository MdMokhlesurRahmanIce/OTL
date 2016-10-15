using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class LCSummaryReportEntity
    {
        public string BankLCNumber
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

        public decimal Quantity
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public decimal LCValue
        {
            get;
            set;
        }

        public string BOEDate
        {
            get;
            set;
        }

        public string DeliveryDate
        {
            get;
            set;
        }
    }
}