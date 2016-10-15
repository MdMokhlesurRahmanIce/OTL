using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using BaseModule;
using SecurityModule.DataAccess;
using ProductionModule.Provider;


namespace ProductionModule.DataAccess
{
    public class QAQCRequisitionDetailDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(QAQCRequisitionDetailProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionDetailSet;
            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = provider.ProductID;
            command.Parameters.Add("@Rate", SqlDbType.VarChar).Value = provider.RawProductName;
            command.Parameters.Add("@RateInDollar", SqlDbType.Decimal).Value = provider.RequiredQuantity;
            command.Parameters.Add("@CurrencyRate", SqlDbType.Decimal).Value = provider.SentQuantity;
            command.Parameters.Add("@CurrencyRate", SqlDbType.VarChar).Value = provider.Remarks;
            return command;
        }
        public bool Save(QAQCRequisitionDetailProvider provider)
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
        public bool Update(QAQCRequisitionDetailProvider provider)
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
        public bool Delete(QAQCRequisitionDetailProvider provider)
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
        //public DataSet GetAll()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.RequisitionDetailGet;
        //        command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
        //        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
        //        sqlDataAdapter = new SqlDataAdapter(command);
        //        sqlDataAdapter.Fill(ds);
        //        this.ConnectionClosed();
        //    }
        //    catch (Exception exp)
        //    {
        //        throw new Exception(exp.Message);
        //    }
        //    finally
        //    {
        //        this.ConnectionClosed();
        //    }
        //    return ds;
        //}
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
              //  command.CommandText = StoredProcedureNames.RequisitionDetailGet;
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
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            SqlDataAdapter sqlDataAdapter = null;
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
