using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PurchaseModule.DataAccess;

namespace PurchaseModule.Provider
{
    public class TAXInfoProvider: BaseProvider
    {
        #region Properties
        public int ID
        {
            get;
            set;
        }
        public int ProductID
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public decimal AssessmentValue
        {
            get;
            set;
        }

        public decimal CDPerc
        {
            get;
            set;
        }
        public decimal CDAmt
        {
            get;
            set;
        }
        public decimal VATPerc
        {
            get;
            set;
        }
        public decimal VATAmt
        {
            get;
            set;
        }

        public decimal AITPerc
        {
            get;
            set;
        }
        public decimal AITAmt
        {
            get;
            set;
        }
        public decimal ATVPerc
        {
            get;
            set;
        }
        public decimal ATVAmt
        {
            get;
            set;
        }
        public decimal SDPerc
        {
            get;
            set;
        }
        public decimal SDAmt
        {
            get;
            set;
        }
        public decimal RDPerc
        {
            get;
            set;
        }
        public decimal RDAmt
        {
            get;
            set;
        }
        public decimal DFCVATFPAmt
        {
            get;
            set;
        }        
        #endregion

        #region Constant
        public const string IDField = "ID";
        public const string PurchaseLedgerIDField = "PurchaseLedgerID";
        public const string ProductIDField = "ProductID";
        public const string RateField = "Rate";
        public const string RateInDollarField = "RateInDollar";
        public const string CurrencyRateField = "CurrencyRate";
        //public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        private TAXInfoDataAccess dataAccess = new TAXInfoDataAccess();
        //public bool Save()
        //{
        //    if (this.Rate > 0)
        //    {
        //        throw new Exception("Rate must be greater than zero");
        //    }
        //    return dataAccess.Save(this);
        //}
        //public bool Update()
        //{
        //    if (this.Rate > 0)
        //    {
        //        throw new Exception("Rate must be greater than zero");
        //    }
        //    return dataAccess.Update(this);
        //}
        //public bool Delete()
        //{
        //    return dataAccess.Delete(this);
        //}
        //public DataSet GetAll()
        //{
        //    return dataAccess.GetAll();
        //}
        //public DataSet GetAllActive()
        //{
        //    return dataAccess.GetAllActive();
        //}
        //public DataSet GetByID(Int32 VATRegID, Int32 wh, int ID)
        //{
        //    return dataAccess.GetByID(VATRegID, wh, ID);
        //}
        //public DataSet GetAllByFilterExpression(string filterExpression)
        //{
        //    return dataAccess.GetAllByFilterExpression(filterExpression);
        //}
        //public bool CheckPurchaseStatus()
        //{
        //    return dataAccess.CheckPurchaseStatus(this.ProductID);
        //}
        #endregion

    }
}
