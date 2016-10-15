using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PurchaseModule.DataAccess;
using System.Data;

namespace PurchaseModule.Provider
{
   public class DeliveryChallanProvider:BaseProvider
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
        public DateTime DeliveryChallanDate
        {
            get;
            set;
        }
        
        public int StatusID
        {
            get;
            set;
        }
        public string VehicleInfo
        {
            get;
            set;
        }
        public string DestinationUnit
        {
            get;
            set;
        }
        public int RequisitionRefID
        {
            get;
            set;
        }
        public int DivisionID
        {
            get;
            set;
        }
        public int ChallanTypeID
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
        //public const string StatusIDField = "StatusID";
        #endregion

        #region Method

        private DeliveryChallanDataAccess dataAccess = new DeliveryChallanDataAccess();
        public bool Save(List<DeliveryChallanDetailProvider> PurchaseOrderDetailProviderList, out string transactionNo)
        {
            return dataAccess.Save(this, PurchaseOrderDetailProviderList, out transactionNo);
        }
        public bool Update(List<DeliveryChallanDetailProvider> RequisitionDetailsProviderList)
        {
            if(this.StatusID == 2)
            {
                foreach (DeliveryChallanDetailProvider deliveryChallanDetailProvider in RequisitionDetailsProviderList)
                {
                    if (deliveryChallanDetailProvider.ReceivedQuantity <= 0)
                    {
                        throw new Exception("Please input received quantity or delete that product from list."); 
                    }
                }
            }
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
        //public DataSet GetByDivisionwisePO()
        //{
        //    return dataAccess.GetByDivisionwisePO(this.DivisionID);
        //}
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
        
        #endregion
    }
}
