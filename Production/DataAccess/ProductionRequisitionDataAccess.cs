using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using ProductionModule.Provider;

namespace ProductionModule.DataAccess
{
   public class ProductionRequisitionDataAccess:BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(ProductionRequisitionProvider provider)
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
        public bool Save(ProductionRequisitionProvider ProductionRequisitionProvider, List<ProductionRequisitionDetailProvider> ProductionRequisitionDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.ProductionRequisitionSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@MfgDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.MfgDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.ExpiryDate;
                command.Parameters.Add("@FinishedProductID", SqlDbType.VarChar).Value = ProductionRequisitionProvider.FinishedProductID;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = ProductionRequisitionProvider.DivisionID;
                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchNo;
                command.Parameters.Add("@BatchSize", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchSize;
                command.Parameters.Add("@BatchSizeUnit", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchSizeUnit;
                command.Parameters.Add("@TheoriticalYield", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TheoriticalYield;
                command.Parameters.Add("@TheoriticalYieldUnit", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TheoriticalYieldUnit;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;               
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int RequisitionID = (int)command.Parameters["@ID"].Value;

                foreach (ProductionRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductionRequisitionDetailSet;
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
        public bool Update(ProductionRequisitionProvider ProductionRequisitionProvider, List<ProductionRequisitionDetailProvider> RequisitionDetailProviderList)
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
                command.CommandText = StoredProcedureNames.ProductionRequisitionSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TransactionNo;
                command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RequisitionDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = ProductionRequisitionProvider.DivisionID;                
                command.Parameters.Add("@MfgDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.MfgDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.ExpiryDate;
                command.Parameters.Add("@FinishedProductID", SqlDbType.VarChar).Value = ProductionRequisitionProvider.FinishedProductID;
                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchNo;
                command.Parameters.Add("@BatchSize", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchSize;
                command.Parameters.Add("@BatchSizeUnit", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchSizeUnit;
                command.Parameters.Add("@TheoriticalYield", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TheoriticalYield;
                command.Parameters.Add("@TheoriticalYieldUnit", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TheoriticalYieldUnit;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.UpdateUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int RequisitionID = (int)command.Parameters["@ID"].Value;


                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.ProductionRequisitionDetail WHERE RequisitionID =" + RequisitionID + " ";
                command.ExecuteNonQuery();

                foreach (ProductionRequisitionDetailProvider RequisitionDetailsProvider in RequisitionDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductionRequisitionDetailSet;
                    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                ////////////////////////////////////////////////////////////////////////////// For stock out /////////////////////

                int TransactionTypeID = 0;
                switch(ProductionRequisitionProvider.DivisionID)
                {
                    case 1:
                            TransactionTypeID = 20;
                            break;
                    case 2:
                            TransactionTypeID = 21;
                            break;
                    case 3:
                            TransactionTypeID = 22;
                            break;
                    case 4:
                            TransactionTypeID = 23;
                            break;
                    case 5:
                            TransactionTypeID = 24;
                            break;                    
                    default:
                            break;

                }

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
                    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = TransactionTypeID;
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();

                    int ProStockID = (int)command.Parameters["@ID"].Value;

                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                    command.ExecuteNonQuery();


                    foreach (ProductionRequisitionDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
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
        public bool Delete(ProductionRequisitionProvider provider)
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
        public bool Return(ProductionRequisitionProvider ProductionRequisitionProvider, List<ProductionRequisitionDetailProvider> ProductionRequisitionDetailProviderList)
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
                command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TransactionNo;
                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchNo;
                command.Parameters.Add("@RetRejDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RetRejDate;
                foreach (ProductionRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                {
                    //command = new SqlCommand();
                    //command.Connection = Connection;
                    //command.Transaction = this.Transaction;
                    //command.CommandType = CommandType.StoredProcedure;
                    //command.CommandText = StoredProcedureNames.ProductionRetRejSet;
                    
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                    command.Parameters.Add("@ReturnQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnQuantity;
                    command.Parameters.Add("@RejectQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectQuantity;
                    command.Parameters.Add("@ReturnReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnReceived;
                    command.Parameters.Add("@RejectReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectReceived;
                    
                    command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;
                    command.Parameters.Add("@IsBatchRejcted", SqlDbType.Bit).Value = ProductionRequisitionProvider.IsBatchRejcted;
                    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;

                    command.ExecuteNonQuery();
                }

                ///////////////////////////////////////////// For stock in and out /////////////////////////////////////////////

                if (ProductionRequisitionProvider.StatusID == 2)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductRetRejDamageSet;
                    SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                    id1.Direction = ParameterDirection.Output;
                    command.Parameters.Add(id1);
                    command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = ProductionRequisitionProvider.TransactionNo;
                    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 37; // For ProductRetRejDamage
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();

                    int ParentID = (int)command.Parameters["@ID"].Value;

                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandText = "DELETE FROM Inventory.ProductRetRejDamageDetail WHERE ParentID = " + ParentID + " ";
                    command.ExecuteNonQuery();

                    foreach (ProductionRequisitionDetailProvider productionRequisitionDetailProvider in ProductionRequisitionDetailProviderList)
                    {
                        command = new SqlCommand();
                        command.Connection = Connection;
                        command.Transaction = this.Transaction;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = StoredProcedureNames.ProductRetRejDamageDetailSet;
                        command.Parameters.Add("@ParentID", SqlDbType.Int).Value = ParentID;
                        command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                        command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.ReturnReceived;
                        command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = 0;
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
        public DataTable GetByBatchNRef(string batchNo, string refNo)
        {
            string filterExpression = "PR.BatchNo = " + "'" + batchNo + "'" + " AND PR.ReferenceNo = " + "'" + refNo + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductionRequisitionGet;
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
        public DataTable GetByIDForRetRej(string batchNo, string refNo)
        {
            string filterExpression = "PR.BatchNo = " + "'" + batchNo + "'" + " AND PR.ReferenceNo = " + "'" + refNo + "'";
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
