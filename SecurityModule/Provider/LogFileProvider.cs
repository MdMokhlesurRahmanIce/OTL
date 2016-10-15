using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.DataAccess;


namespace SecurityModule.Provider
{
    public class LogFileProvider
    {
        #region Properties
        public Int64 LogID { get; set; }
        public String UserID { get; set; }
        public DateTime LogInTime { get; set; }
        public String IPAddress { get; set; }
        public DateTime LogOutTime { get; set; }
        #endregion

        #region methods

        private LogFileDataAccess dataAccess = new LogFileDataAccess();
        private UserDataAccess udataAccess = new UserDataAccess();
        public long Save()
        {
            return dataAccess.Save(this);
        }
        public bool Update()
        {
            return dataAccess.Update(this);
        }
        //public DataSet GetByID(string UserID)
        //{
        //    return dataAccess.GetByID(UserID);
        //}
        //public UserProvider GetByUserID(string Uid)
        //{
        //    return udataAccess.GetByUserID(Uid);
        //}
        public LogFileProvider GetByIDAndIP(long LogID, string UserID, string IPAddress)
        {
            return dataAccess.GetByIDAndID(UserID, IPAddress);
        }
        public bool DatabaseBackup()
        {
            return dataAccess.DatabaseBackup(this);
        }

        #endregion
    }
}
