using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using InventoryModule.DataAccess;

namespace InventoryModule.Provider
{
    public class DamageProductProvider : BaseProvider
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
        public int DivisionID
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }
        #endregion

        #region Constant
        public const string IDField = "ID";
        public const string TransactionNoField = "TransactionNo";
        public const string TransactionDateField = "TransactionDate";
        public const string BillEntryNoField = "BillEntryNo";
        public const string BillEntryDateField = "BillEntryDate";
        public const string PurchaseTypeIDField = "PurchaseTypeID";
        public const string SupplierIDField = "SupplierID";
        public const string VatRegistrationIDField = "VatRegistrationID";
        public const string WarehouseIDField = "WarehouseID";
        public const string TarriffStatusIDField = "TarriffStatusID";
        public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        private DamageProductDataAccess dataAccess = new DamageProductDataAccess();
        public bool Save(List<DamageProductDetailProvider> RequisitionDetailProviderList, out string transactionNo)
        {
            if (this.DivisionID == 0)
                throw new Exception("Division can't be empty");
            return dataAccess.Save(this, RequisitionDetailProviderList, out transactionNo);
        }
        public bool Update(List<DamageProductDetailProvider> RequisitionDetailsProviderList)
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
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        public DataSet GetDivisioinWiseRequisitionNo(string filterExpression)
        {
            return dataAccess.GetDivisioinWiseRequisitionNo(filterExpression);
        }

        public DataTable GetAllByDateWise(int productID, string transactionid, DateTime? fromDate, DateTime? todate, DateTime? date, int reportOption)
        {
            return dataAccess.GetAllByDateWise(productID, transactionid, fromDate, todate, date, reportOption);
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
