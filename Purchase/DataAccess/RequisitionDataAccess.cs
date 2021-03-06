﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using PurchaseModule.Provider;

namespace PurchaseModule.DataAccess
{
    public class RequisitionDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(RequisitionProvider provider)
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
        public bool Save(RequisitionProvider RequisitionProvider, List<RequisitionDetailProvider> RequisitionDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.RequisitionSet;
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
                command.Parameters.Add("@CheckUserID", SqlDbType.Int).Value = RequisitionProvider.CheckUserID;
                command.Parameters.Add("@ApproveUserID", SqlDbType.Int).Value = RequisitionProvider.ApproveUserID;                
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = RequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int RequisitionID = (int)command.Parameters["@ID"].Value;
                
                foreach (RequisitionDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.RequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@MonthlyConsumeQty", SqlDbType.Decimal).Value = RequisitionDetailsProvider.MonthlyConsumeQty;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
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
        public bool Update(RequisitionProvider RequisitionProvider, List<RequisitionDetailProvider> RequisitionDetailProviderList)
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
                command.CommandText = StoredProcedureNames.RequisitionSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = RequisitionProvider.ReferenceNo;
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = RequisitionProvider.RequisitionDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = RequisitionProvider.DivisionID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = RequisitionProvider.UpdateUserID;
                command.Parameters.Add("@CheckUserID", SqlDbType.Int).Value = RequisitionProvider.CheckUserID;
                command.Parameters.Add("@ApproveUserID", SqlDbType.Int).Value = RequisitionProvider.ApproveUserID;                
                
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = RequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int RequisitionID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.RequisitionDetail WHERE RequisitionID =" + RequisitionID + " ";
                command.ExecuteNonQuery();

                foreach (RequisitionDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.RequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@MonthlyConsumeQty", SqlDbType.Decimal).Value = RequisitionDetailsProvider.MonthlyConsumeQty;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
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

        public bool Delete(RequisitionProvider provider)
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
            string filterExpression = "R.ReferenceNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
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
        public DataTable GetRequisitionwisePOInfo(int productID)
        {
            string filterExpression = productID.ToString();
            SqlDataAdapter sqlDataAdapter = null;
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.RequisitionGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithSpecialFilter;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
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
            return dt;
        }
        #endregion
    }
}
