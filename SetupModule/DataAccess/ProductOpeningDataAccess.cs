using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SecurityModule.DataAccess;
using SetupModule.Provider;


namespace SetupModule.DataAccess
{
    public class ProductOpeningDataAccess : BaseDataAccess
    {
        //private SqlDataAdapter sqlDataAdapter = null;
        public bool Save(ProductOpeningProvider provider)
        {
            string query = "EXEC [dbo].[spOpeningStock]"
                                       + "@Option = 'Save',"
                                       + "@ProductID  = '" + provider.ProductId + "',"
                                       + "@OpeningQuantity = '" + provider.OpeningQty + "',"
                                       + "@OpeningAmount = '" + provider.OpeningAmount + "',"
                                       + "@Location = '" + provider.Locatioin + "',"
                                       + "@UserId = '" + provider.EntryUserID + "'";
            return ExecuteTextQuery(query);
        }

        private bool ExecuteTextQuery(string query)
        {
            DataSet ds = new DataSet();
            bool IsSave = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = query;
                new SqlDataAdapter(command).Fill(ds);
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

        internal bool Delete(ProductOpeningProvider provider)
        {
            string query = "EXEC [dbo].[spOpeningStock]"
                                       + "@Option = 'Delete',"
                                       + "@ID = '" + provider.ID + "'";
            return ExecuteTextQuery(query);
        }

        internal DataSet GetAll(ProductOpeningProvider provider)
        {
            DataSet ds = new DataSet();
            try
            {
                string sql = "EXEC [dbo].[spOpeningStock] @Option = 'Select'";                
                if (!provider.ProductId.Equals(0))
                {
                    sql += ",@ProductID = " + provider.ProductId;
                }

                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.Text;
                command.CommandText = sql;
                new SqlDataAdapter(command).Fill(ds);
                this.CommitTransaction();
                this.ConnectionClosed();
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
            return ds;
        }
        internal DataSet GetAll(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
