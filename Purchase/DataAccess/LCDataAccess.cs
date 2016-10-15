using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BaseModule;
using PurchaseModule.Provider;

namespace PurchaseModule.DataAccess
{
    public class LCDataAccess:BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(LCProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@PurchaseOrderDate", SqlDbType.DateTime).Value = provider.LCOpeningDate;
            command.Parameters.Add("@SupplierID", SqlDbType.VarChar).Value = provider.SupplierID;
            return command;
        }
        public bool Save(LCProvider LCProvider, List<LCDetailProvider> LCDetailProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.LCChallanSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@BankLCNumber", SqlDbType.VarChar).Value = LCProvider.BankLCNumber;
                command.Parameters.Add("@LCAFNumber", SqlDbType.VarChar).Value = LCProvider.LCAFNumber;
                command.Parameters.Add("@ModeOfTransport", SqlDbType.VarChar).Value = LCProvider.ModeOfTransport;
                command.Parameters.Add("@LCOpeningDate", SqlDbType.DateTime).Value = LCProvider.LCOpeningDate;
                command.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = LCProvider.ShipmentDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = LCProvider.ExpiryDate;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = LCProvider.SupplierID;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = LCProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();              
                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int LCChallanID = (int)command.Parameters["@ID"].Value;

                foreach (LCDetailProvider LCDetailProvider in LCDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.LCChallanDetailSet;
                    command.Parameters.Add("@LCChallanID", SqlDbType.Int).Value = LCChallanID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = LCDetailProvider.ProductID;
                    command.Parameters.Add("@RequisitionRef", SqlDbType.VarChar).Value = LCDetailProvider.RequisitionRef;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = LCDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = LCDetailProvider.Rate;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = LCDetailProvider.Currency;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = LCDetailProvider.Quantity;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }

                //////////////////////////////////// LC Amendment ///////////////////////////////////////////
                foreach (LCDetailProvider LCDetailProvider in LCDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.LCAmendmentSet;
                    command.Parameters.Add("@BankLCNumber", SqlDbType.VarChar).Value = LCProvider.BankLCNumber;
                    command.Parameters.Add("@SystemLCNo", SqlDbType.VarChar).Value = transactionNo;
                    command.Parameters.Add("@LCAFNumber", SqlDbType.VarChar).Value = LCProvider.LCAFNumber;
                    command.Parameters.Add("@ModeOfTransport", SqlDbType.VarChar).Value = LCProvider.ModeOfTransport;
                    command.Parameters.Add("@LCOpeningDate", SqlDbType.DateTime).Value = LCProvider.LCOpeningDate;
                    command.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = LCProvider.ShipmentDate;
                    command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = LCProvider.ExpiryDate;
                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = LCProvider.SupplierID;
                    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = LCProvider.EntryUserID;
                    command.Parameters.Add("@EntryDate", SqlDbType.DateTime).Value = DateTime.Now;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = LCDetailProvider.ProductID;
                    command.Parameters.Add("@RequisitionRef", SqlDbType.VarChar).Value = LCDetailProvider.RequisitionRef;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = LCDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = LCDetailProvider.Rate;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = LCDetailProvider.Currency;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = LCDetailProvider.Quantity;
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

        public bool Update(LCProvider LCProvider, List<LCDetailProvider> LCDetailProviderList)
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
                command.CommandText = StoredProcedureNames.LCChallanSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = LCProvider.TransactionNo;
                command.Parameters.Add("@BankLCNumber", SqlDbType.VarChar).Value = LCProvider.BankLCNumber;
                command.Parameters.Add("@LCAFNumber", SqlDbType.VarChar).Value = LCProvider.LCAFNumber;
                command.Parameters.Add("@ModeOfTransport", SqlDbType.VarChar).Value = LCProvider.ModeOfTransport;
                command.Parameters.Add("@LCOpeningDate", SqlDbType.DateTime).Value = LCProvider.LCOpeningDate;
                command.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = LCProvider.ShipmentDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = LCProvider.ExpiryDate;
                command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = LCProvider.SupplierID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = LCProvider.UpdateUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int LCChallanID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.LCChallanDetail WHERE LCChallanID =" + LCChallanID + " ";
                command.ExecuteNonQuery();

                foreach (LCDetailProvider LCDetailProvider in LCDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.LCChallanDetailSet;
                    command.Parameters.Add("@LCChallanID", SqlDbType.Int).Value = LCChallanID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = LCDetailProvider.ProductID;
                    command.Parameters.Add("@RequisitionRef", SqlDbType.VarChar).Value = LCDetailProvider.RequisitionRef;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = LCDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = LCDetailProvider.Rate;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = LCDetailProvider.Currency;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = LCDetailProvider.Quantity;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                //////////////////////////////////////////// LC Amendment ///////////////////////////////////////////////////               
                
                foreach (LCDetailProvider LCDetailProvider in LCDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.LCAmendmentSet;
                    command.Parameters.Add("@BankLCNumber", SqlDbType.VarChar).Value = LCProvider.BankLCNumber;
                    command.Parameters.Add("@SystemLCNo", SqlDbType.VarChar).Value = LCProvider.TransactionNo;
                    command.Parameters.Add("@LCAFNumber", SqlDbType.VarChar).Value = LCProvider.LCAFNumber;
                    command.Parameters.Add("@ModeOfTransport", SqlDbType.VarChar).Value = LCProvider.ModeOfTransport;
                    command.Parameters.Add("@LCOpeningDate", SqlDbType.DateTime).Value = LCProvider.LCOpeningDate;
                    command.Parameters.Add("@ShipmentDate", SqlDbType.DateTime).Value = LCProvider.ShipmentDate;
                    command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = LCProvider.ExpiryDate;
                    command.Parameters.Add("@SupplierID", SqlDbType.Int).Value = LCProvider.SupplierID;
                    command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = LCProvider.UpdateUserID;
                    command.Parameters.Add("@UpdateDate", SqlDbType.DateTime).Value = DateTime.Now;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = LCDetailProvider.ProductID;
                    command.Parameters.Add("@RequisitionRef", SqlDbType.VarChar).Value = LCDetailProvider.RequisitionRef;
                    command.Parameters.Add("@Value", SqlDbType.Decimal).Value = LCDetailProvider.Value;
                    command.Parameters.Add("@Rate", SqlDbType.Decimal).Value = LCDetailProvider.Rate;
                    command.Parameters.Add("@Currency", SqlDbType.VarChar).Value = LCDetailProvider.Currency;
                    command.Parameters.Add("@Quantity", SqlDbType.Decimal).Value = LCDetailProvider.Quantity;
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

        public bool Delete(LCProvider provider)
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
            string filterExpression = "LC.BankLCNumber = " + "'" + code + "'";

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            try
            {
                command = new SqlCommand();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.LCChallanGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
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
        public DataTable GetByDateRangeWise(DateTime fromDate, DateTime toDate, int reportCategory)
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
                command.CommandText = "[LCDateWiseReport]";
                command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                command.Parameters.Add("@Option", SqlDbType.VarChar).Value = reportCategory;
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
        #endregion
    }
}
