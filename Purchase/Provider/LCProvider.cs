using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchaseModule.DataAccess;
using System.Data;

namespace PurchaseModule.Provider
{
    public class LCProvider : BaseProvider
    {
        #region Properties
        public int ID
        {
            get;
            set;
        }
        public string TransactionNo
        {
            get;
            set;
        }
        public DateTime TransactionDate
        {
            get;
            set;
        }
        public string BankLCNumber
        {
            get;
            set;
        }
        public string LCAFNumber
        {
            get;
            set;
        }
        
        public DateTime LCOpeningDate
        {
            get;
            set;
        }
        public DateTime ShipmentDate
        {
            get;
            set;
        }
        public DateTime ExpiryDate
        {
            get;
            set;
        }
        public int SupplierID
        {
            get;
            set;
        }
        public string ModeOfTransport
        {
            get;
            set;
        }
        
        #endregion

        #region Constant
        public const string IDField = "ID";
        public const string TransactionNoField = "TransactionNo";
        public const string TransactionDateField = "TransactionDate";
        //public const string BillEntryNoField = "BillEntryNo";
        //public const string BillEntryDateField = "BillEntryDate";
        public const string PurchaseTypeIDField = "PurchaseTypeID";
        public const string SupplierIDField = "SupplierID";
        //public const string VatRegistrationIDField = "VatRegistrationID";
        //public const string WarehouseIDField = "WarehouseID";
        public const string TarriffStatusIDField = "TarriffStatusID";
        public const string StatusIDField = "StatusID";
        #endregion

        #region Method

        private LCDataAccess dataAccess = new LCDataAccess();
        public bool Save(List<LCDetailProvider> PurchaseOrderDetailProviderList, out string transactionNo)
        {
            if (this.SupplierID == 0)
            {
                throw new Exception("Supplier name never be empty");
            }
            if (this.BankLCNumber.Length == 0)
            {
                throw new Exception("Bank LC number never be empty");
            }
            return dataAccess.Save(this, PurchaseOrderDetailProviderList, out transactionNo);
        }
        public bool Update(List<LCDetailProvider> RequisitionDetailsProviderList)
        {
            return dataAccess.Update(this, RequisitionDetailsProviderList);
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
        public DataTable GetByID(string code)
        {
            return dataAccess.GetByID(code);
        }
        public DataTable GetByDateRangeWise(DateTime fromDate, DateTime toDate, int reportCategory)
        {
            return dataAccess.GetByDateRangeWise(fromDate, toDate, reportCategory);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        //public DataTable GetPurchaseRetaurnableDataByID()
        //{
        //    return dataAccess.GetPurchaseRetaurnableDataByID(this.TransactionNo, this.VatRegistrationID, this.DivisionID); ;
        //}
        //public DataTable GetProductHSCodeDetails(int HSCodeID)
        //{
        //    return dataAccess.GetProductHSCodeDetails(HSCodeID);
        //}
        #endregion
    }
}
