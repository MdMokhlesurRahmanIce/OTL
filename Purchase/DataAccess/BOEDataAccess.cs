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
   public class BOEDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        #region Method
        private SqlCommand ProcedureFunction(BOEProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = provider.TransactionNo;
            command.Parameters.Add("@BOENumber", SqlDbType.VarChar).Value = provider.BOENumber;
            command.Parameters.Add("@BOEDate", SqlDbType.DateTime).Value = provider.BOEDate;
            command.Parameters.Add("@SystemLCNo", SqlDbType.VarChar).Value = provider.SystemLCNo;
            return command;
        }
        public bool Save(BOEProvider BOEProvider, List<BOEDetailProvider> BOEDetailProviderList, List<TAXInfoProvider> TAXInfoProviderList, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.BOEChallanSet;
                SqlParameter t = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@BOENumber", SqlDbType.VarChar).Value = BOEProvider.BOENumber;
                command.Parameters.Add("@ExcRate", SqlDbType.Decimal).Value = BOEProvider.ExcRate;
                command.Parameters.Add("@BOEDate", SqlDbType.DateTime).Value = BOEProvider.BOEDate;
                command.Parameters.Add("@SystemLCNo", SqlDbType.VarChar).Value = BOEProvider.SystemLCNo; 
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = BOEProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();
                transactionNo = (string)command.Parameters["@TransactionNo"].Value;
                int BOEID = (int)command.Parameters["@ID"].Value;

                foreach (BOEDetailProvider BOEDetailProvider in BOEDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.BOEChallanDetailSet;
                    command.Parameters.Add("@BOEID", SqlDbType.Int).Value = BOEID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = BOEDetailProvider.ProductID;
                    command.Parameters.Add("@HSCode", SqlDbType.VarChar).Value = BOEDetailProvider.HSCode;
                    command.Parameters.Add("@ActualQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.ActualQuantity;
                    command.Parameters.Add("@InvoiceQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.InvoiceQuantity;
                    command.Parameters.Add("@RemainingQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.RemainingQuantity;
                    command.Parameters.Add("@InvoiceValue", SqlDbType.Decimal).Value = BOEDetailProvider.InvoiceValue;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }
                foreach (TAXInfoProvider TaxInfoProvider in TAXInfoProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.TAXInfoSet;
                    command.Parameters.Add("@BOEID", SqlDbType.Int).Value = BOEID;
                    command.Parameters.Add("@BOENumber", SqlDbType.VarChar).Value = BOEProvider.BOENumber;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = TaxInfoProvider.ProductID;
                    command.Parameters.Add("@AssessmentValue", SqlDbType.VarChar).Value = TaxInfoProvider.AssessmentValue;
                    command.Parameters.Add("@CDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.CDPerc;
                    command.Parameters.Add("@CDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.CDAmt;
                    command.Parameters.Add("@SDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.SDPerc;
                    command.Parameters.Add("@SDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.SDAmt;
                    command.Parameters.Add("@RDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.RDPerc;
                    command.Parameters.Add("@RDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.RDAmt;
                    command.Parameters.Add("@VATPerc", SqlDbType.Decimal).Value = TaxInfoProvider.VATPerc;
                    command.Parameters.Add("@VATAmt", SqlDbType.Decimal).Value = TaxInfoProvider.VATAmt;
                    command.Parameters.Add("@AITPerc", SqlDbType.Decimal).Value = TaxInfoProvider.AITPerc;
                    command.Parameters.Add("@AITAmt", SqlDbType.Decimal).Value = TaxInfoProvider.AITAmt;
                    command.Parameters.Add("@ATVPerc", SqlDbType.Decimal).Value = TaxInfoProvider.ATVPerc;
                    command.Parameters.Add("@ATVAmt", SqlDbType.Decimal).Value = TaxInfoProvider.ATVAmt;
                    command.Parameters.Add("@DFCVATFPAmt", SqlDbType.Decimal).Value = TaxInfoProvider.DFCVATFPAmt;
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

        public bool Update(BOEProvider BOEProvider, List<BOEDetailProvider> LCDetailProviderList, List<TAXInfoProvider> TAXInfoProviderList)
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
                command.CommandText = StoredProcedureNames.BOEChallanSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@BOENumber", SqlDbType.VarChar).Value = BOEProvider.BOENumber;
                command.Parameters.Add("@ExcRate", SqlDbType.Decimal).Value = BOEProvider.ExcRate;
                command.Parameters.Add("@BOEDate", SqlDbType.DateTime).Value = BOEProvider.BOEDate;
                command.Parameters.Add("@SystemLCNo", SqlDbType.VarChar).Value = BOEProvider.SystemLCNo; 
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = BOEProvider.UpdateUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int BOEChallanID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.BOEChallanDetail WHERE BOEID =" + BOEChallanID + " ";
                command.ExecuteNonQuery();

                foreach (BOEDetailProvider BOEDetailProvider in LCDetailProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.BOEChallanDetailSet;
                    command.Parameters.Add("@BOEID", SqlDbType.Int).Value = BOEChallanID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = BOEDetailProvider.ProductID;
                    command.Parameters.Add("@HSCode", SqlDbType.VarChar).Value = BOEDetailProvider.HSCode;
                    command.Parameters.Add("@ActualQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.ActualQuantity;
                    command.Parameters.Add("@InvoiceQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.InvoiceQuantity;
                    command.Parameters.Add("@RemainingQuantity", SqlDbType.Decimal).Value = BOEDetailProvider.RemainingQuantity;
                    command.Parameters.Add("@InvoiceValue", SqlDbType.Decimal).Value = BOEDetailProvider.InvoiceValue;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.TAXInformation WHERE BOEID =" + BOEChallanID + " ";
                command.ExecuteNonQuery();

                foreach (TAXInfoProvider TaxInfoProvider in TAXInfoProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.TAXInfoSet;
                    command.Parameters.Add("@BOEID", SqlDbType.Int).Value = BOEChallanID;
                    command.Parameters.Add("@BOENumber", SqlDbType.VarChar).Value = BOEProvider.BOENumber;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = TaxInfoProvider.ProductID;
                    command.Parameters.Add("@AssessmentValue", SqlDbType.VarChar).Value = TaxInfoProvider.AssessmentValue;
                    command.Parameters.Add("@CDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.CDPerc;
                    command.Parameters.Add("@CDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.CDAmt;
                    command.Parameters.Add("@SDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.SDPerc;
                    command.Parameters.Add("@SDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.SDAmt;
                    command.Parameters.Add("@RDPerc", SqlDbType.Decimal).Value = TaxInfoProvider.RDPerc;
                    command.Parameters.Add("@RDAmt", SqlDbType.Decimal).Value = TaxInfoProvider.RDAmt;
                    command.Parameters.Add("@VATPerc", SqlDbType.Decimal).Value = TaxInfoProvider.VATPerc;
                    command.Parameters.Add("@VATAmt", SqlDbType.Decimal).Value = TaxInfoProvider.VATAmt;
                    command.Parameters.Add("@AITPerc", SqlDbType.Decimal).Value = TaxInfoProvider.AITPerc;
                    command.Parameters.Add("@AITAmt", SqlDbType.Decimal).Value = TaxInfoProvider.AITAmt;
                    command.Parameters.Add("@ATVPerc", SqlDbType.Decimal).Value = TaxInfoProvider.ATVPerc;
                    command.Parameters.Add("@ATVAmt", SqlDbType.Decimal).Value = TaxInfoProvider.ATVAmt;
                    command.Parameters.Add("@DFCVATFPAmt", SqlDbType.Decimal).Value = TaxInfoProvider.DFCVATFPAmt;
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

        public bool Delete(BOEProvider provider)
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
        public DataTable GetByID(string code)
        {
            string filterExpression = "LC.BankLCNumber = " + "'" + code + "'";

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.BOEChallanGet;
            command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
            command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
            sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dt);
                      
            this.ConnectionClosed();
            return dt;
        }

        public DataTable GetDataForNewBOE(string bankLCNumber)
        {
            string filterExpression = "BankLCNumber = " + "'" + bankLCNumber + "'";

            DataTable dt = new DataTable();
            SqlCommand command = new SqlCommand();            
                try
                {
                    this.ConnectionOpen();
                    command.Connection = Connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.LCChallanGet;
                    command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadForNewBOE;
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
        public DataTable GetBOENumber(string code)
        {
            DataTable bOE = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "SELECT BOEC.ID, BOEC.BOENumber FROM BOEChallan BOEC INNER JOIN LCChallan LC ON BOEC.SystemLCNo = LC.SystemLCNo WHERE LC.BankLCNumber = '" + code + " '";
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(bOE);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return bOE;
        }
        public DataTable GetByLCAndBOENumber(string lcNumber, string bOENumber)
        {
            string filterExpression = "LC.SystemLCNo = " + "'" + lcNumber + "'" + "AND BOEC.BOENumber = " + "'" + bOENumber + "'";            
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.BOEChallanGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpressionByBOE;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
            }
            catch
            {
 
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
                command.CommandText = "[BOEDateWiseReport]";
                command.Parameters.Add("@FromDate", SqlDbType.VarChar).Value = fromDate;
                command.Parameters.Add("@ToDate", SqlDbType.VarChar).Value = toDate;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = reportCategory;
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
