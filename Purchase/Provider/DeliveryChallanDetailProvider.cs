using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PurchaseModule.DataAccess;

namespace PurchaseModule.Provider
{
    public class DeliveryChallanDetailProvider:BaseProvider
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
        public string SupplierChallanNo
        {
            get;
            set;
        }
        //public string PurchaseDate
        //{
        //    get;
        //    set;
        //}
        public string SupplierChallanDate
        {
            get;
            set;
        }
        public string PackSizeName
        {
            get;
            set;
        }
        public decimal ProvidedQuantity
        {
            get;
            set;
        }
        public decimal Value
        {
            get;
            set;
        }
        public string MeasurementUnitName
        {
            get;
            set;
        }
        public decimal ReceivedQuantity
        {
            get;
            set;
        }
        public string POrderNo
        {
            get;
            set;
        }
        public string ReqReferenceNo
        {
            get;
            set;
        }
        public int SupplierID
        {
            get;
            set;
        }
        public string SupplierName
        {
            get;
            set;
        }
        #endregion

        #region Constant
        public const string IDField = "ID";
        public const string PurchaseLedgerIDField = "PurchaseLedgerID";
        public const string ProductIDField = "ProductID";
        //public const string RateField = "Rate";
        //public const string RateInDollarField = "RateInDollar";
        //public const string CurrencyRateField = "CurrencyRate";
        //public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        private DeliveryChallanDetailDataAccess dataAccess = new DeliveryChallanDetailDataAccess();
        public bool Save()
        {
            if (this.ReceivedQuantity > 0)
            {
                throw new Exception("Received Quantity must be greater than zero");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            if (this.ReceivedQuantity > 0)
            {
                throw new Exception("Received Quantity must be greater than zero");
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
        //public DataSet GetByID(Int32 VATRegID, Int32 wh, int ID)
        //{
        //    return dataAccess.GetByID(VATRegID, wh, ID);
        //}
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        //public bool CheckPurchaseStatus()
        //{
        //    return dataAccess.CheckPurchaseStatus(this.ProductID);
        //}
        #endregion
    }
}
