using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryModule.Provider
{
    public class OtherOutProductDetailProvider:BaseProvider
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
        public decimal OtherOutQuantity
        {
            get;
            set;
        }
        public decimal PresentStock
        {
            get;
            set;
        }
        public decimal MonthlyConsumeQty
        {
            get;
            set;
        }
        public string MeasurementUnitName
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        #endregion

        #region Method
        //private RequisitionDetailDataAccess dataAccess = new RequisitionDetailDataAccess();
        //public bool Save()
        //{
        //    if (this.RequiredQuantity > 0)
        //    {
        //        throw new Exception("Rate must be greater than zero");
        //    }
        //    return dataAccess.Save(this);
        //}
        //public bool Update()
        //{
        //    if (this.RequiredQuantity > 0)
        //    {
        //        throw new Exception("Rate must be greater than zero");
        //    }
        //    return dataAccess.Update(this);
        //}
        //public bool Delete()
        //{
        //    return dataAccess.Delete(this);
        //}
        //public DataSet GetAll()
        //{
        //    return dataAccess.GetAll();
        //}
        //public DataSet GetAllActive()
        //{
        //    return dataAccess.GetAllActive();
        //}
        //public DataSet GetByID(Int32 VATRegID,Int32 wh, int ID)
        //{
        //    return dataAccess.GetByID(VATRegID,wh,ID);
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
