using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SetupModule.DataAccess;


namespace SetupModule.Provider
{
    public class MeasurementUnitProvider:BaseProvider
    {
        #region Proparties
        private MeasurementUnitDataAccess dataAccess = new MeasurementUnitDataAccess();
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
        public string ShortName
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
        #region Methods
        //public bool Save()
        //{
        //    if (this.Name.Length == 0)
        //    {
        //        throw new Exception("Name never empty");
        //    }
        //    if (this.ShortName.Length == 0)
        //    {
        //        throw new Exception("ShortName never empty");
        //    }
        //    return dataAccess.Save(this);
        //}
        //public bool Update()
        //{
        //    if (this.Name.Length == 0)
        //    {
        //        throw new Exception("Name never empty");
        //    }
        //    if (this.ShortName.Length == 0)
        //    {
        //        throw new Exception("ShortName never empty");
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
        public DataSet GetAllActive()
        {
            return dataAccess.GetAllActive();
        }
              
        #endregion
    }
}
