using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using PurchaseModule.DataAcess;

namespace PurchaseModule.Provider
{
    public class PurchaseOrderDetailProvider:BaseProvider
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
        public decimal Rate
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
        public string Unit
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
        private PurchaseOrderDetailDataAccess dataAccess = new PurchaseOrderDetailDataAccess();
        public bool Save()
        {
            if (this.Rate > 0)
            {
                throw new Exception("Rate must be greater than zero");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            if (this.Rate > 0)
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
        public DataSet GetAllActive()
        {
            return dataAccess.GetAllActive();
        }
        public decimal GetLastUnitPrice(int productID)
        {
            return dataAccess.GetLastUnitPrice(productID);

        }
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
