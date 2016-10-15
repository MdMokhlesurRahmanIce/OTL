using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.DataAccess;
using System.Data;
using System.Data.SqlClient;
using PurchaseModule.Provider;
using BaseModule;


namespace PurchaseModule.DataAccess
{
    public class LCDetailDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(LCDetailProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionDetailSet;
            return command;
        }
        public bool Save(LCDetailProvider provider)
        {
            bool IsSave = false;
            try
            {
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = provider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();
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
            return IsSave;
        }
        public bool Update(LCDetailProvider provider)
        {
            bool IsUpdate = false;
            try
            {
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@ID", SqlDbType.Int).Value = provider.ID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = provider.UpdateUserID;
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
        public bool Delete(LCDetailProvider provider)
        {
            bool IsDelete = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.RequisitionDetailSet;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = provider.ID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Delete;
                command.ExecuteNonQuery();
                this.CommitTransaction();
                this.ConnectionClosed();
                IsDelete = true;
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
            return IsDelete;
        }
        public DataSet GetAll()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
               // command.CommandText = StoredProcedureNames.RequisitionDetailGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
                this.ConnectionClosed();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return ds;
        }
        public DataSet GetAllActive()
        {
            string filterExpression = "StatusID=1";
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                //command.CommandText = StoredProcedureNames.RequisitionDetailGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
                this.ConnectionClosed();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return ds;
        }        
        #endregion
    }
}
