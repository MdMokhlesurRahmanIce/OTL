using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using PurchaseModule.Provider;
using System.Web;

namespace PurchaseModule.DataAcess
{
    public class PurchaseOrderDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(PurchaseOrderProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@POrderNo", SqlDbType.VarChar).Value = provider.POrderNo;
            command.Parameters.Add("@PurchaseOrderDate", SqlDbType.DateTime).Value = provider.PurchaseOrderDate;
            command.Parameters.Add("@SupplierID", SqlDbType.VarChar).Value = provider.SupplierID;
            return command;
        }
        public bool Save(PurchaseOrderProvider PurchaseOrderProvider, List<PurchaseOrderDetailProvider> PurchaseOrderDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.PurchaseOrderSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@PurchaseOrderDate", SqlDbType.DateTime).Value = PurchaseOrderProvider.PurchaseOrderDate;
                command.Parameters.Add("@AppxDeliveryDate", SqlDbType.DateTime).Value = PurchaseOrderProvider.AppxDeliveryDate;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = PurchaseOrderProvider.SupplierID;
                command.Parameters.Add("@RequisitionRefID", SqlDbType.Int).Value = PurchaseOrderProvider.RequisitionRefID;
                command.Parameters.Add("@MessageValue", SqlDbType.Int).Value = PurchaseOrderProvider.MessageValue;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = PurchaseOrderProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int PurchaseOrderID = (int)command.Parameters["@ID"].Value;

                foreach (PurchaseOrderDetailProvider PurchaseOrderDetailProvider in PurchaseOrderDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.PurchaseOrderDetailSet;
                    command.Parameters.Add("@PurchaseOrderID", SqlDbType.Int).Value = PurchaseOrderID;
                    command.Parameters.Add("@RequisitionRefID", SqlDbType.Int).Value = PurchaseOrderProvider.RequisitionRefID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = PurchaseOrderDetailProvider.ProductID;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Rate;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Quantity;
                    //command.Parameters.Add("@Unit", SqlDbType.VarChar).Value = PurchaseOrderDetailProvider.Unit;
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

        public bool Update(PurchaseOrderProvider PurchaseOrderProvider, List<PurchaseOrderDetailProvider> PurchaseOrderDetailProviderList)
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
                command.CommandText = StoredProcedureNames.PurchaseOrderSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@PurchaseOrderNo", SqlDbType.VarChar).Value = PurchaseOrderProvider.POrderNo;
                command.Parameters.Add("@PurchaseOrderDate", SqlDbType.DateTime).Value = PurchaseOrderProvider.PurchaseOrderDate;
                command.Parameters.Add("@AppxDeliveryDate", SqlDbType.DateTime).Value = PurchaseOrderProvider.AppxDeliveryDate;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = PurchaseOrderProvider.SupplierID;
                command.Parameters.Add("@RequisitionRefID", SqlDbType.VarChar).Value = PurchaseOrderProvider.RequisitionRefID;
                command.Parameters.Add("@MessageValue", SqlDbType.Int).Value = PurchaseOrderProvider.MessageValue;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = PurchaseOrderProvider.UpdateUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int PurchaseOrderID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.PurchaseOrderDetail WHERE PurchaseOrderID =" + PurchaseOrderID + " ";
                command.ExecuteNonQuery();

                foreach (PurchaseOrderDetailProvider PurchaseOrderDetailProvider in PurchaseOrderDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.PurchaseOrderDetailSet;
                    command.Parameters.Add("@PurchaseOrderID", SqlDbType.Int).Value = PurchaseOrderID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = PurchaseOrderDetailProvider.ProductID;
                    command.Parameters.Add("@RequisitionRefID", SqlDbType.VarChar).Value = PurchaseOrderProvider.RequisitionRefID;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Rate;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = PurchaseOrderDetailProvider.Quantity;
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
       
        public bool Delete(PurchaseOrderProvider provider)
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
                command.CommandText = StoredProcedureNames.PurchaseOrderGet;
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
            string filterExpression = "PO.POrderNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.PurchaseOrderGet;
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
        public DataSet GetDivisioinWisePONo()
        {
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = new DataSet();
            int divisionID = Convert.ToInt32(HttpContext.Current.ApplicationInstance.Session["DivisionID"]);
            int challanTypeID = Convert.ToInt32(HttpContext.Current.ApplicationInstance.Session["ChallanTypeID"]);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                if(challanTypeID == 1)
                    command.CommandText = "SELECT PO.ID, PO.POrderNo FROM dbo.PurchaseOrder PO INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID " +
                                            " WHERE R.DivisionID = " + divisionID + " ORDER BY POrderNo DESC ";

                //command.CommandText = "SELECT PO.ID, PO.POrderNo + ' - ' + S.Name POrderNo FROM dbo.PurchaseOrder PO INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID " +
                //                            "LEFT JOIN dbo.Suppliers S ON PO.SupplierID = S.ID WHERE R.DivisionID = " + divisionID + " ORDER BY POrderNo DESC ";
                if (challanTypeID == 2)
                    command.CommandText = "select BankLCNumber POrderNo from LCChallan";
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
                command.CommandText = "SELECT PO.ID, PO.POrderNo FROM dbo.PurchaseOrder PO INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID WHERE R.DivisionID =" + divisionID + "";
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

        public DataTable GetAllData(int productID)
        {
            DataTable dt = new DataTable();
            string filterExpression = "RD.ProductID = " + productID + "";
            try
                 
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.PurchaseOrderGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithSpecialFilter;
                
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
