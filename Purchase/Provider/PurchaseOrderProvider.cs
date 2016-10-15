using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PurchaseModule.DataAcess;

namespace PurchaseModule.Provider
{
    public class PurchaseOrderProvider : BaseProvider
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
        public string POrderNo
        {
            get;
            set;
        }
        public DateTime PurchaseOrderDate
        {
            get;
            set;
        }
        public DateTime AppxDeliveryDate
        {
            get;
            set;
        }
        public int SupplierID
        {
            get;
            set;
        }
        public int DivisionID
        {
            get;
            set;
        }
        
        public int MessageValue
        {
            get;
            set;
        }
        public int RequisitionRefID
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
        //public const string VatRegistrationIDField = "VatRegistrationID";
        //public const string WarehouseIDField = "WarehouseID";
        //public const string TarriffStatusIDField = "TarriffStatusID";
        public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        
        private PurchaseOrderDataAccess dataAccess = new PurchaseOrderDataAccess();
        public bool Save(List<PurchaseOrderDetailProvider> PurchaseOrderDetailProviderList, out string transactionNo)
        {
            if (this.SupplierID == 0)
            {
                throw new Exception("Supplier never be empty");
            }
            if (this.RequisitionRefID == 0)
            {
                throw new Exception("Reference number never be empty");
            }
            
            if (PurchaseOrderDate.GetHashCode() == 0)
            {
                throw new Exception("Date can't never be empty");
            }
            return dataAccess.Save(this, PurchaseOrderDetailProviderList, out transactionNo);
        }
        public bool Update(List<PurchaseOrderDetailProvider> RequisitionDetailsProviderList)
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
        public DataSet GetDivisioinWisePONo()
        {
            return dataAccess.GetDivisioinWisePONo();

        }
        public DataSet GetDivisioinWisePONo(int divisionID)
        {
            return dataAccess.GetDivisioinWisePONo(divisionID);

        }
        //public DataTable GetPurchaseRetaurnableDataByID()
        //{
        //    return dataAccess.GetPurchaseRetaurnableDataByID(this.TransactionNo, this.VatRegistrationID, this.DivisionID); ;
        //}
        public DataTable GetAllData(int productID)
        {
            return dataAccess.GetAllData(productID);
        }
      
        #endregion
    }
}
