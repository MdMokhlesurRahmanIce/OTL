using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using ProductionModule.Provider;

namespace ProductionModule.DataAccess
{
    public class QAQCRequisitionDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(QAQCRequisitionProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = provider.RequisitionDate;
            command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = provider.DivisionID;
            return command;
        }
        public bool Save(QAQCRequisitionProvider QAQCRequisitionProvider, List<QAQCRequisitionDetailProvider> ProductionRequisitionDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.QAQCRequisitionSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = QAQCRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = QAQCRequisitionProvider.EntryUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = QAQCRequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int RequisitionID = (int)command.Parameters["@ID"].Value;

                foreach (QAQCRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.QAQCRequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@UsedForProduct", SqlDbType.VarChar).Value = RequisitionDetailsProvider.UsedForProduct;
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
        public bool Update(QAQCRequisitionProvider QAQCRequisitionProvider, List<QAQCRequisitionDetailProvider> RequisitionDetailProviderList)
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
                command.CommandText = StoredProcedureNames.QAQCRequisitionSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = QAQCRequisitionProvider.TransactionNo;
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = QAQCRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = QAQCRequisitionProvider.UpdateUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = QAQCRequisitionProvider.StatusID;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int RequisitionID = (int)command.Parameters["@ID"].Value;


                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.QARequisitionDetail WHERE RequisitionID =" + RequisitionID + " ";
                command.ExecuteNonQuery();

                foreach (QAQCRequisitionDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.QAQCRequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@UsedForProduct", SqlDbType.VarChar).Value = RequisitionDetailsProvider.UsedForProduct;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;

                    command.ExecuteNonQuery();
                }
                ////////////////////////////////////////////////////////////////////////////// For stock out /////////////////////

                if (QAQCRequisitionProvider.StatusID == 2)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductStockSet;
                    SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                    id1.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id1);
                    command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = QAQCRequisitionProvider.TransactionNo;
                    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 4;
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = QAQCRequisitionProvider.EntryUserID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();

                    int ProStockID = (int)command.Parameters["@ID"].Value;

                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                    command.ExecuteNonQuery();
                    foreach (QAQCRequisitionDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
                    {
                        command = new SqlCommand();
                        command.Connection = Connection;
                        command.Transaction = this.Transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StoredProcedureNames.ProductStockDetailSet;
                        command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                        command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = 0;
                        command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.SentQuantity;
                        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                        command.ExecuteNonQuery();
                    }
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
        public bool Delete(QAQCRequisitionProvider provider)
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
            string filterExpression = "QR.ReferenceNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.QAQCRequisitionGet;
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
        public DataTable GetByIDForRetRej(string code)
        {
            string filterExpression = "PR.ReferenceNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductionRequisitionGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpressionForRetRej;
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
                //command.CommandText = StoredProcedureNames.VatInformationGet;
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
        public DataSet GetDivisioinWisePONo(int divisionID)
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
                command.CommandText = "SELECT PR.ID, PR.ReferenceNo FROM dbo.ProductionRequisition PR WHERE PR.DivisionID = " + divisionID + "";
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
        public DataTable GetDateWiseProductInfo(DateTime? fromDate, DateTime? toDate, DateTime? Date, int? productID)
        {
            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            try
            {
                command = new SqlCommand();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[QADateWiseReport]";
                command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                command.Parameters.Add("@Date", SqlDbType.VarChar).Value = Date;
                command.Parameters.Add("@ProductID", SqlDbType.VarChar).Value = productID;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            this.ConnectionClosed();
            return dt;
        }
        #endregion
    }
}
