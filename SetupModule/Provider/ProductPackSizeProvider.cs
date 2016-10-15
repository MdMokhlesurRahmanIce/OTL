using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;


namespace SetupModule.Provider
{
    public class ProductPackSizeProvider:BaseProvider
    {
        #region Proparties
        private ProductPackSizeDataAccess dataAccess = new ProductPackSizeDataAccess();
        public int ID
        {
            get;
            set;
        }
        public string PackName
        {
            get;
            set;
        }
        public decimal Quantity
        {
            get;
            set;
        }
        public float Height
        {
            get;
            set;
        }
        public int MesurementUnitID
        {
            get;
            set;
        }
        public int ContainerID
        {
            get;
            set;
        }
        public int StatusID
        {
            get;
            set;
        }
        public int PackTypeID
        {
            get;
            set;
        }
        public string  SizeName
        {
            get;
            set;
        }
        public float  Lenght
        {
            get;
            set;
        }
        public float Width
        {
            get;
            set;
        }

        #endregion

        #region Constants
        public const string IDField = "ID";
        public const string NameField = "Name";
        public const string QuantityField = "Quantity";
        public const string MesurementUnitIDField = "MesurementUnitID";
        public const string ContainerIDField = "ContainerID";
        public const string StatusIDField = "StatusID";
        #endregion

        #region Method
        public bool Save()
        {
            if (this.PackName.Length == 0)
            {
                throw new Exception("Name never empty");
            }
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            if (this.PackName.Length == 0)
            {
                throw new Exception("Name never empty");
            }
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
        //public DataSet GetAllActive(Int32 SubCategoryID, Int32 GradeID)
        //{
        //    return dataAccess.GetAllActive(SubCategoryID, GradeID);
        //}
        //public DataSet GetAllActiveConsumed(Int32 SubCategoryID, Int32 GradeID)
        //{
        //    return dataAccess.GetAllActiveConsumed(SubCategoryID, GradeID);
        //}
        public DataSet GetByID(int id)
        {
            return dataAccess.GetByID(id);
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        #endregion
    }
}
