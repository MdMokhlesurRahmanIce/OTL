using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionModule.DataAccess;
using System.Data;

namespace ProductionModule.Provider
{
   public class ProductionRequisitionProvider : BaseProvider
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
        //public string ReferenceNo
        //{
        //    get;
        //    set;
        //}
        public string BatchNo
        {
            get;
            set;
        }
        public string BatchSize
        {
            get;
            set;
        }
        public string TheoriticalYield
        {
            get;
            set;
        }
        public string TheoriticalYieldUnit
        {
            get;
            set;
        }
        public string BatchSizeUnit
        {
            get;
            set;
        } 
       public DateTime RequisitionDate
        {
            get;
            set;
        }
        public DateTime MfgDate
        {
            get;
            set;
        }
        public DateTime ExpiryDate
        {
            get;
            set;
        }
        public DateTime RetRejDate
        {
            get;
            set;
        }
        public int DivisionID
        {
            get;
            set;
        }
        public int FinishedProductID
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }
        public bool IsBatchRejcted
        {
            get;
            set;
        }

        #endregion        

        #region Method
        private ProductionRequisitionDataAccess dataAccess = new ProductionRequisitionDataAccess();
        public bool Save(List<ProductionRequisitionDetailProvider> RequisitionDetailProviderList, out string transactionNo)
        {
            if(this.DivisionID ==0 || FinishedProductID == 0)
                throw new Exception("Please select division and finished product.");

            if (this.BatchNo == string.Empty || this.BatchSize == string.Empty || this.BatchSizeUnit == "0")            
                throw new Exception("Please put Batch Information properly.");

            if (this.StatusID == 2)
                throw new Exception("You need to send the materials first.");

            if (this.TheoriticalYield == string.Empty || this.TheoriticalYieldUnit == "0")
                throw new Exception("Please input theoritical information properly.");
            
            return dataAccess.Save(this, RequisitionDetailProviderList, out transactionNo);
        }
        public bool Update(List<ProductionRequisitionDetailProvider> RequisitionDetailsProviderList)
        {
            return dataAccess.Update(this, RequisitionDetailsProviderList);
        }
        public bool Return(List<ProductionRequisitionDetailProvider> RequisitionDetailProviderList)
        {            
            return dataAccess.Return(this, RequisitionDetailProviderList);
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
        public DataTable GetByBatchNRef(string batchNo, string refNo)
        {
            return dataAccess.GetByBatchNRef(batchNo, refNo);
        }
        public DataTable GetByIDForRetRej(string batchNo, string refNo)
        {
            return dataAccess.GetByIDForRetRej(batchNo, refNo);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        public DataSet GetDivisioinWisePONo(int divisionID)
        {
            return dataAccess.GetDivisioinWisePONo(divisionID);
        }
 
        #endregion
    }
}
