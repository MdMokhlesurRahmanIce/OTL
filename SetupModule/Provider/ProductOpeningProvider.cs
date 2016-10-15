using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;

namespace SetupModule.Provider
{
    public class ProductOpeningProvider : BaseProvider
    {
        #region Properties
        private ProductOpeningDataAccess dataAccess = new ProductOpeningDataAccess();
        public int ID { get; set; }
        //public int WarehouseId { get; set; }
        //public string WarehouseName { get; set; }
        //public int VatRegistrationId { get; set; }
        //public string VatRegistrationNo { get; set; }
        public string Locatioin { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal OpeningQty { get; set; }
        public decimal OpeningAmount { get; set; }

        #endregion

        #region Method
        public bool Save()
        {
            if (this.ProductId== 0)
            {
                throw new Exception("Please select a product");
            }
           
            return dataAccess.Save(this);
        }
        //public bool Update()
        //{
        //    return dataAccess.Update(this);
        //}
        public bool Delete()
        {
            return dataAccess.Delete(this);
        }
        public DataSet GetAll()
        {
            return dataAccess.GetAll(this);
        }
        //public DataSet GetAllActive()
        //{
        //    return dataAccess.GetAllActive();
        //}
        //public DataSet GetByID(int ID)
        //{
        //    return dataAccess.GetByID(ID);
        //}
        //public DataSet GetAllByFilterExpression(string filterExpression)
        //{
        //    return dataAccess.GetAllByFilterExpression(filterExpression);
        //}
        //public DataSet GetAllRawAndPackingProduct()
        //{
        //    string filterExpression = "ItemTypeID=1";
        //    return dataAccess.GetAllByFilterExpression(filterExpression);
        //}
        //public DataSet GetAllFinishedProduct()
        //{
        //    string filterExpression = "ItemTypeID=2";
        //    return dataAccess.GetAllByFilterExpression(filterExpression);
        //}
        //public DataSet GetByWarehouseIDAndProductID(int warehouseID, int productID)
        //{
        //    return dataAccess.GetByWarehouseIDAndProductID(warehouseID, productID);
        //}
        #endregion


    }
}
