using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductionModule.DataAccess;
using System.Data;

namespace ProductionModule.Provider
{
    public class QAQCRequisitionProvider : BaseProvider
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
        //public string BatchNo
        //{
        //    get;
        //    set;
        //}
        //public string BatchSize
        //{
        //    get;
        //    set;
        //}
        //public string TheoriticalYield
        //{
        //    get;
        //    set;
        //}
        public DateTime RequisitionDate
        {
            get;
            set;
        }
        //public DateTime MfgDate
        //{
        //    get;
        //    set;
        //}
        //public DateTime ExpiryDate
        //{
        //    get;
        //    set;
        //}
        //public DateTime RetRejDate
        //{
        //    get;
        //    set;
        //}
        public int DivisionID
        {
            get;
            set;
        }
        //public int FinishedProductID
        //{
        //    get;
        //    set;
        //}
        public int StatusID
        {
            get;
            set;
        }
        #endregion

        #region Method
        private QAQCRequisitionDataAccess dataAccess = new QAQCRequisitionDataAccess();
        public bool Save(List<QAQCRequisitionDetailProvider> RequisitionDetailProviderList, out string transactionNo)
        {          
            return dataAccess.Save(this, RequisitionDetailProviderList, out transactionNo);
        }
        public bool Update(List<QAQCRequisitionDetailProvider> RequisitionDetailsProviderList)
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
        //public DataTable GetByIDForRetRej(string code)
        //{
        //    return dataAccess.GetByIDForRetRej(code);
        //}
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }      
        public DataSet GetDivisioinWisePONo(int divisionID)
        {
            return dataAccess.GetDivisioinWisePONo(divisionID);
        }
        public DataTable GetDateWiseProductInfo(DateTime? fromDate, DateTime? toDate, DateTime? Date, int? productID)
        {
            return dataAccess.GetDateWiseProductInfo(fromDate, toDate, Date, productID);
        }
        #endregion
    }
}
