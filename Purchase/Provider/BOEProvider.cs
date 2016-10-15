using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using PurchaseModule.DataAccess;

namespace PurchaseModule.Provider
{
    public class BOEProvider : BaseProvider
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
        public string SystemLCNo
        {
            get;
            set;
        }
        public string BOENumber
        {
            get;
            set;
        }
        public decimal ExcRate
        {
            get;
            set;
        }
        public DateTime BOEDate
        {
            get;
            set;
        }
        
        #endregion

        #region Constant
        public const string IDField = "ID";
        //public const string TransactionNoField = "TransactionNo";
        //public const string TransactionDateField = "TransactionDate";
        //public const string BillEntryNoField = "BillEntryNo";
        //public const string BillEntryDateField = "BillEntryDate";
        //public const string PurchaseTypeIDField = "PurchaseTypeID";
        //public const string SupplierIDField = "SupplierID";
        //public const string VatRegistrationIDField = "VatRegistrationID";
        //public const string WarehouseIDField = "WarehouseID";
        //public const string TarriffStatusIDField = "TarriffStatusID";
        public const string StatusIDField = "StatusID";
        #endregion

        #region Method

        private BOEDataAccess dataAccess = new BOEDataAccess();
        public bool Save(List<BOEDetailProvider> PurchaseOrderDetailProviderList, List<TAXInfoProvider> taxInfoProviderList, out string transactionNo)
        {
            if (this.BOENumber.Length == 0)
            {
                throw new Exception("BOE Number can not be empty");
            }
            return dataAccess.Save(this, PurchaseOrderDetailProviderList, taxInfoProviderList, out transactionNo);
        }
        public bool Update(List<BOEDetailProvider> RequisitionDetailsProviderList, List<TAXInfoProvider> taxInfoProviderList)
        {
            return dataAccess.Update(this, RequisitionDetailsProviderList, taxInfoProviderList);
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
        public DataTable GetByID(string code)
        {
            return dataAccess.GetByID(code);
        }
        public DataTable GetBOENumber(string code)
        {
            return dataAccess.GetBOENumber(code);
        }
        public DataTable GetByLCAndBOENumber(string lcNumber, string bOENumber)
        {
            return dataAccess.GetByLCAndBOENumber(lcNumber, bOENumber);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        
        #endregion

        public DataTable GetDataForNewBOE(string bankLCNumber)
        {
            return dataAccess.GetDataForNewBOE(bankLCNumber);
        }
        public DataTable GetByDateRangeWise(DateTime fromDate, DateTime toDate, int reportCategory)
        {
            return dataAccess.GetByDateRangeWise(fromDate, toDate, reportCategory);
        }
    }
}
