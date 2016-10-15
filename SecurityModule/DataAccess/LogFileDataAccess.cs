using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.Provider;
using BaseModule;

namespace SecurityModule.DataAccess
{
    public class LogFileDataAccess : BaseDataAccess
    {
        private SqlDataAdapter sqlDataAdapter = null;

        private SqlCommand ProcedureFunction(LogFileProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.LogFileSet;
            command.Parameters.Add("@LogID", SqlDbType.BigInt).Value = 1;
            command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = provider.UserID;
            command.Parameters.Add("@LogInTime", SqlDbType.DateTime).Value = provider.LogInTime;
            command.Parameters.Add("@IPAddress", SqlDbType.VarChar).Value = provider.IPAddress;
            command.Parameters.Add("@LogOutTime", SqlDbType.DateTime).Value = provider.LogOutTime;
            return command;
        }
        public Int32 Save(LogFileProvider provider)
        {
            bool IsSave = false;
            Int32 longVal = 0;
            try
            {
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                object obj = command.ExecuteScalar();
                this.CommitTransaction();
                this.ConnectionClosed();
                IsSave = true;
            }
            catch (Exception exp)
            {
                this.RollbackTransaction();
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return longVal;
        }
        public bool Update(LogFileProvider provider)
        {
            bool IsUpdate = false;
            try
            {
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();
                this.CommitTransaction();
                this.ConnectionClosed();
                IsUpdate = true;
            }
            catch (Exception exp)
            {
                this.RollbackTransaction();
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return IsUpdate;
        }
        public LogFileProvider GetByIDAndID(string UserID, string IPAdd)
        {
            string filterExpression = String.Format("F.UserID='{0}' and F.IPAddress='{1}'", UserID, IPAdd);
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.LogFileGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
                this.ConnectionClosed();
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            if ((ds == null) || (ds.Tables[0].Rows.Count == 0))
            {
                return null;
            }
            else
            {
                return DataTableConversion.ToCollection<LogFileProvider>(ds.Tables[0])[0];
            }
        }
        public bool DatabaseBackup(LogFileProvider provider)
        {
            bool IsUpdate = false;
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command = new SqlCommand("backup database TechnoDrugsLive to disk='" + "D:\\LiveDBBackup" + "\\" + "TechnoDrugsLive" + "_" + DateTime.Now.ToLongDateString() + "_" + DateTime.Now.ToString("HHmmss") + ".Bak'", Connection);
                command.ExecuteNonQuery();
                this.CommitTransaction();
                this.ConnectionClosed();
                IsUpdate = true;
            }
            catch (Exception exp)
            {
                this.RollbackTransaction();
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return IsUpdate;
        }
    }
}
