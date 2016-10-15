using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using InventoryModule.Provider;

namespace InventoryModule.DataAccess
{
    public class OtherOutProductDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(OtherOutProductProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = provider.ReferenceNo;
            command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = provider.RequisitionDate;
            command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = provider.DivisionID;
            return command;
        }
        public bool Save(OtherOutProductProvider RequisitionProvider, List<OtherOutProductDetailProvider> RequisitionDetailProviderList, out string transactionNo)
        {
            bool IsSave = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.OtherOutProductSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = RequisitionProvider.RequisitionDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = RequisitionProvider.DivisionID;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = RequisitionProvider.EntryUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = RequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int RequisitionID = (int)command.Parameters["@ID"].Value;

                foreach (OtherOutProductDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.OtherOutProductDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.OtherOutQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }
                ///////////////////////////////////////////// For stock in and out /////////////////////////////////////////////

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductOtherOutSet;
                SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                id1.Direction = ParameterDirection.Output;
                command.Parameters.Add(id1);
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = transactionNo;
                command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 41; // For ProductOtherOut
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = RequisitionProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                int ParentID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM Inventory.ProductOtherOutDetail WHERE ParentID = " + ParentID + " ";
                command.ExecuteNonQuery();

                foreach (OtherOutProductDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductOtherOutDetailSet;
                    command.Parameters.Add("@ParentID", SqlDbType.Int).Value = ParentID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = 0;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.OtherOutQuantity;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }
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
        public bool Update(OtherOutProductProvider RequisitionProvider, List<OtherOutProductDetailProvider> RequisitionDetailProviderList)
        {
            bool IsUpdate = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.OtherOutProductSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = RequisitionProvider.ReferenceNo;
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = RequisitionProvider.RequisitionDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = RequisitionProvider.DivisionID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = RequisitionProvider.UpdateUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = RequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int RequisitionID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.OtherOutdProductDetail WHERE OtherOutID =" + RequisitionID + " ";
                command.ExecuteNonQuery();

                foreach (OtherOutProductDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.OtherOutProductDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.OtherOutQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                ///////////////////////////////////////////// For stock in and out /////////////////////////////////////////////

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductOtherOutSet;
                SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                id1.Direction = ParameterDirection.Output;
                command.Parameters.Add(id1);
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = RequisitionProvider.ReferenceNo;
                command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 41; // For Product Other Out
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = RequisitionProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                int ParentID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM Inventory.ProductRetRejOtherOutDetail WHERE ParentID = " + ParentID + " ";
                command.ExecuteNonQuery();

                foreach (OtherOutProductDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductOtherOutDetailSet;
                    command.Parameters.Add("@ParentID", SqlDbType.Int).Value = ParentID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = 0;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.OtherOutQuantity;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }

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

        public bool Delete(OtherOutProductProvider provider)
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
                command.CommandText = StoredProcedureNames.RequisitionSet;
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
                command.CommandText = StoredProcedureNames.RequisitionGet;
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
                command.CommandText = StoredProcedureNames.RequisitionGet;
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
        public DataTable GetByID(string code)
        {
            string filterExpression = "OOP.ReferenceNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.OtherOutProductGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
                this.ConnectionClosed();
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            return dt;
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
                command.CommandText = StoredProcedureNames.RequisitionGet;
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
        public DataSet GetDivisioinWiseRequisitionNo(string filterExpression)
        {
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandText = "SELECT R.ID, R.ReferenceNo + ' Date: '  + Convert(varchar(30),R.RequisitionDate ,105) AS ReferenceNo FROM dbo.Requisition R WHERE R.StatusID = 3 AND " + filterExpression + " ORDER BY R.RequisitionDate DESC";
                da.SelectCommand = command;
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
        public DataTable GetAllByDateWise(int productID, string transactionid, DateTime? fromDate, DateTime? todate, DateTime? adate, int reportOption)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.RequisitionReport;
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = transactionid;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
                command.Parameters.Add("@FromDate", SqlDbType.DateTime).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.DateTime).Value = todate;
                //command.Parameters.Add("@Date", SqlDbType.DateTime).Value = adate;
                command.Parameters.Add("@ReportOption", SqlDbType.Int).Value = reportOption;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
                ConnectionClosed();
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return dt;
        }
        #endregion
    }
}
