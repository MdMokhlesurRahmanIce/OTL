using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionModule.DataAccess;
using System.Data;

namespace ProductionModule.Provider
{
    public class FinishedProductProvider : BaseProvider
    {
        #region Properties
        public int ID
        {
            get;
            set;
        }
        public string ReferenceNo
        {
            get;
            set;
        }
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
        public decimal BatchQuantity
        {
            get;
            set;
        }
        public string MeasurementUnitName
        {
            get;
            set;
        }        
        public string TheoriticalYield
        {
            get;
            set;
        }
        public decimal CommercialPack
        {
            get;
            set;
        }
        public decimal ActualYield
        {
            get;
            set;
        }
        public string ActualYieldUnit
        {
            get;
            set;
        }
        public string ChallanNo
        {
            get;
            set;
        }
        public DateTime ChallanDate
        {
            get;
            set;
        }
        public DateTime ReceivedDate
        {
            get;
            set;
        }
        
        public DateTime MfgDate
        {
            get;
            set;
        }
        public DateTime ExpDate
        {
            get;
            set;
        }        
        public int DivisionID
        {
            get;
            set;
        }
        
        public decimal TradePrice
        {
            get;
            set;
        }
        public decimal TotalTradePrice
        {
            get;
            set;
        }
        public int ProductID
        {
            get;
            set;
        }
        public string ProductCode
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public decimal PresentStock
        {
            get;
            set;
        }
        public string PackSize
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
        
        #region Method
        private FinishedProductDataAccess dataAccess = new FinishedProductDataAccess();
        public bool Save(out string transactionNo)
        {
            if (this.DivisionID == 0 || ProductID == 0)
                throw new Exception("Please select division and finished product.");
            if (this.BatchNo == string.Empty || this.BatchSize == string.Empty)

                throw new Exception("Batch Information can't be blank");

            return dataAccess.Save(this, out transactionNo);
        }
        public bool SendFPToHeadOffice(List<FinishedProductProvider> FinishedProductProviderList, out string transactionNo)
        {
            if (this.DivisionID == 0)
                throw new Exception("Please select division");

            return dataAccess.SendFPToHeadOffice(this, FinishedProductProviderList, out transactionNo);
        }
        public bool UpdateFPToHeadOffice(List<FinishedProductProvider> FinishedProductProviderList)
        {
            return dataAccess.UpdateFPToHeadOffice(this, FinishedProductProviderList);
        }
        
        public bool Update()
        {
            return dataAccess.Update(this);
        }
        public DataTable GetByID(string code)
        {
            return dataAccess.GetByID(code);
        }
        public DataTable GetByBatchNRef(string batchNo, string refNo)
        {
            return dataAccess.GetByBatchNRef(batchNo, refNo);
        }
        public DataTable GetDivisionWiseBatch(int divisionID)
        {
            return dataAccess.GetDivisionWiseBatch(divisionID);
        }
        public DataTable GetFGWiseBatch(int FGID)
        {
            return dataAccess.GetFGWiseBatch(FGID);
        }
        public DataTable GetBatchWiseMFCExpDate(string batchNo)
        {
            return dataAccess.GetBatchWiseMFCExpDate(batchNo);
        }        
        #endregion
    }
}
