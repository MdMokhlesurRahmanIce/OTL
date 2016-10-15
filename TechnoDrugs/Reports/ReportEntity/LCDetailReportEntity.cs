using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechnoDrugs.Reports.ReportEntity
{
    public class LCDetailReportEntity
    {
        public string SystemLCNo
        {
            get;
            set;
        }
        public int NoOfAmendment
        {
            get;
            set;
        }
        public string  LCOpeningDate
        {
            get;
            set;
        }        
        public string ProductID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string MeasurementUnit
        {
            get;
            set;
        }
        public decimal Quantity
        {
            get;
            set;
        }
        public decimal Value
        {
            get;
            set;
        }
        public string ShipmentDate
        {
            get;
            set;
        }
        
    }
}