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
   public class DeliveryChallanDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(DeliveryChallanProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.DeliveryChallanGet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@POrderNo", SqlDbType.VarChar).Value = provider.POrderNo;
            command.Parameters.Add("@DeliveryChallanDate", SqlDbType.DateTime).Value = provider.DeliveryChallanDate;
            
            return command;
        }
        public bool Save(DeliveryChallanProvider DeliveryChallanProvider, List<DeliveryChallanDetailProvider> DeliveryChallanDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.DeliveryChallanSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@DeliveryChallanDate", SqlDbType.DateTime).Value = DeliveryChallanProvider.DeliveryChallanDate;
                command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = DeliveryChallanProvider.DivisionID;
                command.Parameters.Add("@VehicleInfo", SqlDbType.VarChar).Value = DeliveryChallanProvider.VehicleInfo;
                command.Parameters.Add("@ChallanTypeID", SqlDbType.Int).Value = DeliveryChallanProvider.ChallanTypeID;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = DeliveryChallanProvider.EntryUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = DeliveryChallanProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int DeliveryID = (int)command.Parameters["@ID"].Value;

                foreach (DeliveryChallanDetailProvider DeliveryChallanDetailProvider in DeliveryChallanDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.DeliveryChallanDetailSet;
                    command.Parameters.Add("@DeliveryID", SqlDbType.Int).Value = DeliveryID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = DeliveryChallanDetailProvider.ProductID;
                    command.Parameters.Add("@ProvidedQuantity", SqlDbType.Decimal).Value = DeliveryChallanDetailProvider.ProvidedQuantity;
                    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = DeliveryChallanDetailProvider.ReceivedQuantity;
                    command.Parameters.Add("@SupplierChallanDate", SqlDbType.VarChar).Value = DeliveryChallanDetailProvider.SupplierChallanDate;
                    command.Parameters.Add("@PurchaseOrderNo", SqlDbType.VarChar).Value = DeliveryChallanDetailProvider.POrderNo;
                    command.Parameters.Add("@SupplierChallanNo", SqlDbType.VarChar).Value = DeliveryChallanDetailProvider.SupplierChallanNo;
                    command.Parameters.Add("@SupplierID", SqlDbType.VarChar).Value = DeliveryChallanDetailProvider.SupplierID;
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
        public bool Update(DeliveryChallanProvider DeliveryProvider, List<DeliveryChallanDetailProvider> DeliveryDetailProviderList)
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
                command.CommandText = StoredProcedureNames.DeliveryChallanSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@DeliveryChallanNo", SqlDbType.VarChar).Value = DeliveryProvider.TransactionNo;
                command.Parameters.Add("@DeliveryChallanDate", SqlDbType.DateTime).Value = DeliveryProvider.DeliveryChallanDate;
                command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = DeliveryProvider.DivisionID;
                command.Parameters.Add("@VehicleInfo", SqlDbType.VarChar).Value = DeliveryProvider.VehicleInfo;
                command.Parameters.Add("@ChallanTypeID", SqlDbType.Int).Value = DeliveryProvider.ChallanTypeID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = DeliveryProvider.UpdateUserID;
                command.Parameters.Add("@StatusID", SqlDbType.Int).Value = DeliveryProvider.StatusID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int DeliveryID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.DeliveryDetail WHERE DeliveryID =" + DeliveryID + " ";
                command.ExecuteNonQuery();

                foreach (DeliveryChallanDetailProvider DeliveryDetailProvider in DeliveryDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.DeliveryChallanDetailSet;
                    command.Parameters.Add("@DeliveryID", SqlDbType.Int).Value = DeliveryID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = DeliveryDetailProvider.ProductID;
                    command.Parameters.Add("@PurchaseOrderNo", SqlDbType.VarChar).Value = DeliveryDetailProvider.POrderNo;
                    
                    command.Parameters.Add("@SupplierChallanDate", SqlDbType.VarChar).Value = DeliveryDetailProvider.SupplierChallanDate;
                    command.Parameters.Add("@SupplierChallanNo", SqlDbType.VarChar).Value = DeliveryDetailProvider.SupplierChallanNo;
                    command.Parameters.Add("@ProvidedQuantity", SqlDbType.Decimal).Value = DeliveryDetailProvider.ProvidedQuantity;
                    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = DeliveryDetailProvider.ReceivedQuantity;
                    
                    command.Parameters.Add("@SupplierID", SqlDbType.VarChar).Value = DeliveryDetailProvider.SupplierID;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }

                ////////////////////////////////////////////////////////////////////////////// For stock in /////////////////////

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductStockSet;
                SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                id1.Direction = ParameterDirection.Output;
                command.Parameters.Add(id1);                
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = DeliveryProvider.TransactionNo;
                command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 1;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = DeliveryProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                //command.Parameters.Add("@StatusID", SqlDbType.Int).Value = DeliveryProvider.StatusID;
                command.ExecuteNonQuery();

                int ProStockID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                command.ExecuteNonQuery();
                foreach (DeliveryChallanDetailProvider DeliveryDetailProvider in DeliveryDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.ProductStockDetailSet;
                    command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = DeliveryDetailProvider.ProductID;
                    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = DeliveryDetailProvider.ReceivedQuantity;
                    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = 0;
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
        public bool Delete(DeliveryChallanProvider provider)
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
                command.CommandText = StoredProcedureNames.DeliveryChallanSet;
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
                command.CommandText = StoredProcedureNames.DeliveryChallanGet;
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
        //public DataSet GetByDivisionwisePO(int divisionID)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.PurchaseOrderGet;
        //        command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
        //        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadByDivisionwise;
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
                command.CommandText = StoredProcedureNames.DeliveryChallanGet;
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
            string filterExpression = "D.DeliveryChallanNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.DeliveryChallanGet;
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
                command.Transaction = this.Transaction;
                command.CommandText = "SELECT DISTINCT D.DeliveryChallanNo, D.ID, PT.ID AS ItemTypeID FROM dbo.Delivery D INNER JOIN dbo.DeliveryDetail DD ON D.ID = DD.DeliveryID INNER JOIN dbo.Product AS P ON P.ID = DD.ProductID INNER JOIN dbo.ProductType AS PT ON P.ItemTypeID = PT.ID " + filterExpression +"";                
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
