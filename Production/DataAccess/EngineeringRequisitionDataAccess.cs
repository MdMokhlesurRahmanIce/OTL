using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using ProductionModule.Provider;

namespace ProductionModule.DataAccess
{
    class EngineeringRequisitionDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        public bool Save(EngineeringRequisitionProvider ProductionRequisitionProvider, List<EngineeringRequisitionDetailProvider> ProductionRequisitionDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.EngineeringRequisitionSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int RequisitionID = (int)command.Parameters["@ID"].Value;

                foreach (EngineeringRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.EngineeringRequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
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
        public bool Update(EngineeringRequisitionProvider ProductionRequisitionProvider, List<EngineeringRequisitionDetailProvider> RequisitionDetailProviderList)
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
                command.CommandText = StoredProcedureNames.EngineeringRequisitionSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TransactionNo;
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.UpdateUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int RequisitionID = (int)command.Parameters["@ID"].Value;
                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.EngineeringRequisitionDetail WHERE RequisitionID =" + RequisitionID + " ";
                command.ExecuteNonQuery();

                foreach (EngineeringRequisitionDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.EngineeringRequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                ////////////////////////////////////////////////////////////////////////////// For stock out /////////////////////

                if (ProductionRequisitionProvider.StatusID == 2)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductStockSet;
                    SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                    id1.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id1);
                    command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = ProductionRequisitionProvider.TransactionNo;
                    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 5;
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();

                    int ProStockID = (int)command.Parameters["@ID"].Value;

                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                    command.ExecuteNonQuery();

                    foreach (EngineeringRequisitionDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
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

        public bool Delete(EngineeringRequisitionProvider provider)
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
        public bool Return(EngineeringRequisitionProvider ProductionRequisitionProvider, List<EngineeringRequisitionDetailProvider> ProductionRequisitionDetailProviderList)
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
                command.CommandText = StoredProcedureNames.ProductionRetRejSet;
                
                foreach (EngineeringRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductionRetRejSet;
                    command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TransactionNo;
                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchNo;
                    command.Parameters.Add("@RetRejDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RetRejDate;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@ReturnQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnQuantity;
                    command.Parameters.Add("@RejectQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectQuantity;
                    command.Parameters.Add("@ReturnReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnReceived;
                    command.Parameters.Add("@RejectReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectReceived;

                    command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.ExecuteNonQuery();
                }

                ////////////////////////////////////////////////////////////////////////////// For stock in and out /////////////////////

                if (ProductionRequisitionProvider.StatusID == 2)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductStockSet;
                    SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                    id1.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id1);
                    command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = ProductionRequisitionProvider.TransactionNo;
                    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 3;
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();

                    int ProStockID = (int)command.Parameters["@ID"].Value;

                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                    command.ExecuteNonQuery();

                    foreach (EngineeringRequisitionDetailProvider productionRequisitionDetailProvider in ProductionRequisitionDetailProviderList)
                    {
                        command = new SqlCommand();
                        command.Connection = Connection;
                        command.Transaction = this.Transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StoredProcedureNames.ProductStockDetailSet;
                        command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                        command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.ReturnReceived;
                        command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.RejectReceived;
                        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                        command.ExecuteNonQuery();
                    }
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
            string filterExpression = "ER.ReferenceNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.EngineeringRequisitionGet;
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
        #endregion
    }
}
