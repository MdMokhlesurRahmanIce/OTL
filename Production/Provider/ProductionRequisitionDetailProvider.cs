using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionModule.DataAccess;
using System.Data;

namespace ProductionModule.Provider
{
    public class ProductionRequisitionDetailProvider : BaseProvider 
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
        public string RawProductName
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
        public decimal RequiredQuantity
        {
            get;
            set;
        }
        public decimal ReturnQuantity
        {
            get;
            set;
        }
        public decimal RejectQuantity
        {
            get;
            set;
        }
        public decimal ReturnReceived
        {
            get;
            set;
        }
        public decimal RejectReceived
        {
            get;
            set;
        }
        
        public decimal ReceivedQuantity
        {
            get;
            set;
        }
        public decimal PresentStock
        {
            get;
            set;
        }

        public decimal SentQuantity
        {
            get;
            set;
        }
        public string Remarks
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
        private ProductionRequisitionDetailDataAccess dataAccess = new ProductionRequisitionDetailDataAccess();
        public bool Save()
        {
            if (this.RequiredQuantity > 0)
            {
                throw new Exception("Rate must be greater than zero");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            if (this.RequiredQuantity > 0)
            {
                throw new Exception("Rate must be greater than zero");
            }
            return dataAccess.Update(this);
        }
        public bool Delete()
        {
            return dataAccess.Delete(this);
        }
        public DataSet GetAll()
        {
            return dataAccess.GetAll();
        }
        //public DataSet GetAllActive()
        //{
        //    return dataAccess.GetAllActive();
        //}
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        #endregion
    }
}
