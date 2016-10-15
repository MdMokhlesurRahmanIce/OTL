using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;

namespace SetupModule.Provider
{
    public  class ProductTypeProvider:BaseProvider
    {
        #region Properties
        private ProductTypeDataAccess dataAccess= new ProductTypeDataAccess();
        public int ID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public decimal VatRate
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

        //#region
        //public const string IDField = "ID";
        //public const string NameField = "Name";
        //public const string VatRateField = "VatRate";
        //public const string StatusIDField = "StatusID";
        //#endregion

        #region Method
        //public bool Save()
        //{
        //    if (this.Name == String.Empty)
        //    {
        //        throw new Exception(" Name is required");
        //    }
        //    if (this.VatRate <=0)
        //    {
        //        throw new Exception(" Vat rate must be greater than zero");
        //    }
        //    return dataAccess.Save(this);
        //}
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
            return dataAccess.GetAll();
        }
        public DataSet GetAllActive()
        {
            return dataAccess.GetAllActive();
        }
        public DataSet GetAllDivision()
        {
            return dataAccess.GetAllDivision();
        }
        public DataSet GetByID(int ID)
        {
            return dataAccess.GetByID(ID);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        #endregion
    }
}
