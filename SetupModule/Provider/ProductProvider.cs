using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;


namespace SetupModule.Provider
{
    public class ProductProvider : BaseProvider
    {
        #region Properties
        private ProductDataAccess dataAccess = new ProductDataAccess();
        public int ID
        {
            get;
            set;
        }
        public string Code
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string Specification
        {
            get;
            set;
        }
        public string GenericName
        {
            get;
            set;
        }
        public string DARNo
        {
            get;
            set;
        }
        public string PackSize
        {
            get;
            set;
        }

        //public int CategoryID
        //{
        //    get;
        //    set;
        //}
        //public int SubCategoryID
        //{
        //    get;
        //    set;
        //}
        //public int VarietyID
        //{
        //    get;
        //    set;
        //}
        public int? GradeID
        {
            get;
            set;
        }
        public int NatureID
        {
            get;
            set;
        }
        public int DivisionID
        {
            get;
            set;
        }
        public int ItemTypeID
        {
            get;
            set;
        }
        //public int? PacksizeID
        //{
        //    get;
        //    set;
        //}
        //public int? GLHSCodeID
        //{
        //    get;
        //    set;
        //}
        //public int VatID
        //{
        //    get;
        //    set;
        //}

        //public int? SDID 
        //{
        //    get; 
        //    set; 
        //}

        public decimal SafetyStock
        {
            get;
            set;
        }
        //public decimal CostPrice
        //{
        //    get;
        //    set;
        //}
        public decimal TradePrice
        {
            get;
            set;
        }
        public decimal MRP
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }

        public int MesurementUnitID
        {
            get;
            set;
        }

        #endregion

        #region Method
        public bool Save()
        {
            if (this.ItemTypeID == 0)
            {
                throw new Exception("Please select item type");
            }
            if (this.Name.Length == 0)
            {
                throw new Exception("Name is required");
            }
            if (this.DivisionID == 0)
            {
                throw new Exception("Please select division");
            }
            if (this.MesurementUnitID == 0)
            {
                throw new Exception("Please select measurement");
            }
            if (this.StatusID == 0)
            {
                throw new Exception("Please select status");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            return dataAccess.Update(this);
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
        public bool GetMeasurementUnit(int productID, string reqRefNo)
        {
            return dataAccess.GetMeasurementUnit(productID, reqRefNo);
        }
        public string GetMeasurementUnit(int productID)
        {
            return dataAccess.GetMeasurementUnit(productID);
        }
        public string GetFinishedProductCode(int productID)
        {
            return dataAccess.GetFinishedProductCode(productID);
        }

        public decimal GetTradePrice(int productID)
        {
            return dataAccess.GetTradePrice(productID);
        }

        public string GetPackSizeName(int productID)
        {
            return dataAccess.GetPackSizeName(productID);
        }
        public decimal GetPresentStock(int productID)
        {
            return dataAccess.GetPresentStock(productID);
        }
        public string GetStockLocation(int productID)
        {
            return dataAccess.GetStockLocation(productID);
        }
        public DataSet GetByID(int ID)
        {
            return dataAccess.GetByID(ID);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        public DataSet GetAllFinishedProduct()
        {
            string filterExpression = "ItemTypeID=2";
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        public DataSet GetDivisionwiseFinishedProduct(int divisionID)
        {
            string filterExpression = "ItemTypeID=2 AND DivisionID = "+ divisionID;
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        #endregion
        public DataTable GetProductForProductSearch(int codeOrName, int productType, int divisionID)
        {
            return dataAccess.GetProductForProductSearch(codeOrName, productType, divisionID);
        }
        public DataTable GetDivisionWiseProduct(int divisionID)
        {
            return dataAccess.GetDivisionWiseProduct(divisionID);
        }
    }
}
