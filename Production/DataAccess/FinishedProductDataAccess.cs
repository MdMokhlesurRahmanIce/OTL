using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using SecurityModule.DataAccess;
using BaseModule;
using ProductionModule.Provider;

namespace ProductionModule.DataAccess
{
    public class FinishedProductDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;

        #region Method
        private SqlCommand ProcedureFunction(FinishedProductProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.RequisitionSet;
            command.Parameters.Add("@RequisitionDate", SqlDbType.DateTime).Value = provider.ReceivedDate;
            command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = provider.DivisionID;
            return command;
        }
        public bool Save(FinishedProductProvider FinishedProductProvider, out string transactionNo)
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
                command.CommandText = StoredProcedureNames.FinishedProductSet;
                SqlParameter tranNo = new SqlParameter("@TransactionNo", SqlDbType.VarChar);
                tranNo.Direction = ParameterDirection.Output;
                tranNo.Size = 16;
                command.Parameters.Add(tranNo);
                command.Parameters.Add("@ReceivedDate", SqlDbType.DateTime).Value = FinishedProductProvider.ReceivedDate;
                command.Parameters.Add("@MfgDate", SqlDbType.DateTime).Value = FinishedProductProvider.MfgDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = FinishedProductProvider.ExpDate;
                command.Parameters.Add("@FinishedProductID", SqlDbType.VarChar).Value = FinishedProductProvider.ProductID;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = FinishedProductProvider.DivisionID;
                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = FinishedProductProvider.BatchNo;
                command.Parameters.Add("@BatchSize", SqlDbType.VarChar).Value = FinishedProductProvider.BatchSize;
                command.Parameters.Add("@TheoriticalYield", SqlDbType.VarChar).Value = FinishedProductProvider.TheoriticalYield;
                command.Parameters.Add("@CommercialPackRec", SqlDbType.Decimal).Value = FinishedProductProvider.CommercialPack;
                command.Parameters.Add("@ActualYield", SqlDbType.Decimal).Value = FinishedProductProvider.ActualYield;
                command.Parameters.Add("@ActualYieldUnit", SqlDbType.VarChar).Value = FinishedProductProvider.ActualYieldUnit;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = FinishedProductProvider.EntryUserID;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                transactionNo = (string)command.Parameters["@TransactionNo"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductStockSet;
                SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                id1.Direction = ParameterDirection.Output;
                command.Parameters.Add(id1);
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = transactionNo;
                command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 2;  //// think later about this.
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = FinishedProductProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                int ProStockID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                command.ExecuteNonQuery();

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductStockDetailSet;
                command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = FinishedProductProvider.ProductID;
                command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = FinishedProductProvider.CommercialPack;
                command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = 0;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();



                //int RequisitionID = (int)command.Parameters["@ID"].Value;

                //foreach (ProductionRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
                //{
                //    command = new SqlCommand();
                //    command.Connection = Connection;
                //    command.Transaction = this.Transaction;
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.CommandText = StoredProcedureNames.ProductionRequisitionDetailSet;
                //    command.Parameters.Add("@RequisitionID", SqlDbType.Int).Value = RequisitionID;
                //    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
                //    command.Parameters.Add("@RequiredQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RequiredQuantity;
                //    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.SentQuantity;
                //    command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
                //    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                //    command.ExecuteNonQuery();
                //}

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
        public bool Update(FinishedProductProvider FinishedProductProvider)
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
                command.CommandText = StoredProcedureNames.FinishedProductSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ReceivedDate", SqlDbType.DateTime).Value = FinishedProductProvider.ReceivedDate;
                command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = FinishedProductProvider.DivisionID;
                command.Parameters.Add("@MfgDate", SqlDbType.DateTime).Value = FinishedProductProvider.MfgDate;
                command.Parameters.Add("@ExpiryDate", SqlDbType.DateTime).Value = FinishedProductProvider.ExpDate;
                command.Parameters.Add("@FinishedProductID", SqlDbType.Int).Value = FinishedProductProvider.ProductID;
                command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = FinishedProductProvider.BatchNo;
                command.Parameters.Add("@BatchSize", SqlDbType.VarChar).Value = FinishedProductProvider.BatchSize;
                command.Parameters.Add("@TheoriticalYield", SqlDbType.VarChar).Value = FinishedProductProvider.TheoriticalYield;
                command.Parameters.Add("@CommercialPackRec", SqlDbType.VarChar).Value = FinishedProductProvider.CommercialPack;
                command.Parameters.Add("@ActualYield", SqlDbType.Decimal).Value = FinishedProductProvider.ActualYield;
                command.Parameters.Add("@ActualYieldUnit", SqlDbType.VarChar).Value = FinishedProductProvider.ActualYieldUnit; 
                command.Parameters.Add("@TransactionNo", SqlDbType.VarChar).Value = FinishedProductProvider.ReferenceNo;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = FinishedProductProvider.UpdateUserID;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                ////////////////////////////////////////////////////////////////////////////// For stock out /////////////////////

                //if (ProductionRequisitionProvider.StatusID == 2)
                //{
                //    command = new SqlCommand();
                //    command.Connection = Connection;
                //    command.Transaction = this.Transaction;
                //    command.CommandType = CommandType.StoredProcedure;
                //    command.CommandText = StoredProcedureNames.ProductStockSet;
                //    SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
                //    id1.Direction = ParameterDirection.Output;
                //    command.Parameters.Add(id1);
                //    command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = ProductionRequisitionProvider.TransactionNo;
                //    command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 2;
                //    command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
                //    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                //    command.ExecuteNonQuery();

                //    int ProStockID = (int)command.Parameters["@ID"].Value;

                //    command = new SqlCommand();
                //    command.Connection = Connection;
                //    command.Transaction = this.Transaction;
                //    command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
                //    command.ExecuteNonQuery();


                //    //foreach (ProductionRequisitionDetailProvider productionRequisitionDetailProvider in RequisitionDetailProviderList)
                //    //{
                //    //    command = new SqlCommand();
                //    //    command.Connection = Connection;
                //    //    command.Transaction = this.Transaction;
                //    //    command.CommandType = CommandType.StoredProcedure;
                //    //    command.CommandText = StoredProcedureNames.ProductStockDetailSet;
                //    //    command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
                //    //    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
                //    //    command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = 0;
                //    //    command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.SentQuantity;
                //    //    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                //    //    command.ExecuteNonQuery();
                //    //}
                //}
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
        public bool SendFPToHeadOffice(FinishedProductProvider SendToHOProvider, List<FinishedProductProvider> FinishedProductProviderList, out string challanNo)
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
                command.CommandText = StoredProcedureNames.SendFPToHoSet;
                SqlParameter t = new SqlParameter("@ChallanNo", SqlDbType.VarChar);
                t.Direction = ParameterDirection.Output;
                t.Size = 16;
                command.Parameters.Add(t);
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);
                command.Parameters.Add("@ChallanDate", SqlDbType.DateTime).Value = SendToHOProvider.ChallanDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = SendToHOProvider.DivisionID;
                command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = SendToHOProvider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                challanNo = (string)command.Parameters["@ChallanNo"].Value;
                int ChallanID = (int)command.Parameters["@ID"].Value;

                foreach (FinishedProductProvider FinishedProductDetailProvider in FinishedProductProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.SendFPToHoDetailSet;
                    command.Parameters.Add("@ChallanID", SqlDbType.Int).Value = ChallanID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = FinishedProductDetailProvider.ProductID;
                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = FinishedProductDetailProvider.BatchNo;
                    command.Parameters.Add("@BatchQuantity", SqlDbType.Decimal).Value = FinishedProductDetailProvider.BatchQuantity;
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
        public bool UpdateFPToHeadOffice(FinishedProductProvider SendToHOProvider, List<FinishedProductProvider> FinishedProductProviderList)
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
                command.CommandText = StoredProcedureNames.SendFPToHoSet;
                SqlParameter id = new SqlParameter("@ID", SqlDbType.Int);
                id.Direction = ParameterDirection.Output;
                command.Parameters.Add(id);

                command.Parameters.Add("@ChallanNo", SqlDbType.VarChar).Value = SendToHOProvider.ChallanNo;
                command.Parameters.Add("@ChallanDate", SqlDbType.DateTime).Value = SendToHOProvider.ChallanDate;
                command.Parameters.Add("@DivisionID", SqlDbType.VarChar).Value = SendToHOProvider.DivisionID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = SendToHOProvider.UpdateUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                int ChallanID = (int)command.Parameters["@ID"].Value;

                command = new SqlCommand();
                command.Connection = Connection;
                command.Transaction = this.Transaction;
                command.CommandText = "DELETE FROM dbo.SendFPToHeadOfficeDetail WHERE ChallanID =" + ChallanID + " ";
                command.ExecuteNonQuery();

                foreach (FinishedProductProvider FinishedProductDetailProvider in FinishedProductProviderList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = this.Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = StoredProcedureNames.SendFPToHoDetailSet;
                    command.Parameters.Add("@ChallanID", SqlDbType.Int).Value = ChallanID;
                    command.Parameters.Add("@ProductID", SqlDbType.Int).Value = FinishedProductDetailProvider.ProductID;
                    command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = FinishedProductDetailProvider.BatchNo;
                    command.Parameters.Add("@BatchQuantity", SqlDbType.Decimal).Value = FinishedProductDetailProvider.BatchQuantity;
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
        //public bool Delete(ProductionRequisitionProvider provider)
        //{
        //    bool IsDelete = false;
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        this.BeginTransaction(true);
        //        command.Transaction = this.Transaction;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.RequisitionSet;
        //        command.Parameters.Add("@ID", SqlDbType.Int).Value = provider.ID;
        //        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Delete;
        //        command.ExecuteNonQuery();
        //        this.CommitTransaction();
        //        this.ConnectionClosed();
        //        IsDelete = true;
        //    }
        //    catch (Exception exp)
        //    {
        //        this.RollbackTransaction();
        //        throw new Exception(exp.Message);
        //    }
        //    finally
        //    {
        //        this.ConnectionClosed();
        //    }
        //    return IsDelete;
        //}
        //public bool Return(ProductionRequisitionProvider ProductionRequisitionProvider, List<ProductionRequisitionDetailProvider> ProductionRequisitionDetailProviderList)
        //{
        //    bool IsSave = false;
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        this.BeginTransaction(true);
        //        command.Transaction = this.Transaction;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.ProductionRetRejSet;
        //        foreach (ProductionRequisitionDetailProvider RequisitionDetailsProvider in ProductionRequisitionDetailProviderList)
        //        {
        //            command = new SqlCommand();
        //            command.Connection = Connection;
        //            command.Transaction = this.Transaction;
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = StoredProcedureNames.ProductionRetRejSet;
        //            command.Parameters.Add("@ReferenceNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.TransactionNo;
        //            command.Parameters.Add("@BatchNo", SqlDbType.VarChar).Value = ProductionRequisitionProvider.BatchNo;
        //            command.Parameters.Add("@RetRejDate", SqlDbType.DateTime).Value = ProductionRequisitionProvider.RetRejDate;
        //            command.Parameters.Add("@ProductID", SqlDbType.Int).Value = RequisitionDetailsProvider.ProductID;
        //            command.Parameters.Add("@ReturnQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnQuantity;
        //            command.Parameters.Add("@RejectQuantity", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectQuantity;
        //            command.Parameters.Add("@ReturnReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.ReturnReceived;
        //            command.Parameters.Add("@RejectReceived", SqlDbType.Decimal).Value = RequisitionDetailsProvider.RejectReceived;

        //            command.Parameters.Add("@StatusID", SqlDbType.Int).Value = ProductionRequisitionProvider.StatusID;
        //            command.Parameters.Add("@Remarks", SqlDbType.VarChar).Value = RequisitionDetailsProvider.Remarks;
        //            command.ExecuteNonQuery();
        //        }

        //        ////////////////////////////////////////////////////////////////////////////// For stock in and out /////////////////////

        //        if (ProductionRequisitionProvider.StatusID == 2)
        //        {
        //            command = new SqlCommand();
        //            command.Connection = Connection;
        //            command.Transaction = this.Transaction;
        //            command.CommandType = CommandType.StoredProcedure;
        //            command.CommandText = StoredProcedureNames.ProductStockSet;
        //            SqlParameter id1 = new SqlParameter("@ID", SqlDbType.Int);
        //            id1.Direction = ParameterDirection.Output;
        //            command.Parameters.Add(id1);
        //            command.Parameters.Add("@TransactionNo", SqlDbType.VarChar, 16).Value = ProductionRequisitionProvider.TransactionNo;
        //            command.Parameters.Add("@TransactionTypeID", SqlDbType.Int).Value = 3;
        //            command.Parameters.Add("@EntryUserID", SqlDbType.Int).Value = ProductionRequisitionProvider.EntryUserID;
        //            command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
        //            command.ExecuteNonQuery();

        //            int ProStockID = (int)command.Parameters["@ID"].Value;

        //            command = new SqlCommand();
        //            command.Connection = Connection;
        //            command.Transaction = this.Transaction;
        //            command.CommandText = "DELETE FROM Inventory.ProductCurrentStockDetail WHERE ProdStockID = " + ProStockID + " ";
        //            command.ExecuteNonQuery();

        //            foreach (ProductionRequisitionDetailProvider productionRequisitionDetailProvider in ProductionRequisitionDetailProviderList)
        //            {
        //                command = new SqlCommand();
        //                command.Connection = Connection;
        //                command.Transaction = this.Transaction;
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.CommandText = StoredProcedureNames.ProductStockDetailSet;
        //                command.Parameters.Add("@ProStockID", SqlDbType.Int).Value = ProStockID;
        //                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productionRequisitionDetailProvider.ProductID;
        //                command.Parameters.Add("@ReceivedQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.ReturnReceived;
        //                command.Parameters.Add("@SentQuantity", SqlDbType.Decimal).Value = productionRequisitionDetailProvider.RejectReceived;
        //                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
        //                command.ExecuteNonQuery();
        //            }
        //        }
        //        this.CommitTransaction();
        //        this.ConnectionClosed();
        //        IsSave = true;
        //    }
        //    catch (Exception exp)
        //    {
        //        this.RollbackTransaction();
        //        throw new Exception(exp.Message);
        //    }
        //    finally
        //    {
        //        this.ConnectionClosed();
        //    }
        //    return IsSave;
        //}
        //public DataSet GetAll()
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.RequisitionGet;
        //        command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
        //        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
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
        //public DataSet GetAllActive()
        //{
        //    string filterExpression = "StatusID=1";
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.RequisitionGet;
        //        command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
        //        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
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
        public DataTable GetByBatchNRef(string batchNo, string refNo)
        {
            string filterExpression = "PR.BatchNo = " + "'" + batchNo + "'" + " OR FPAP.ReferenceNo = " + "'" + refNo +"'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.FinishedProductGet;
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
        public DataTable GetDivisionWiseBatch(int divisionID)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
           // command.CommandText = "SELECT ID, BatchNO + ' - ' + CONVERT(VARCHAR, ReceivedDate, 103) BatchNo FROM [dbo].FinishedProductAfterProduction FPAP WHERE FPAP.DivisionID = " + divisionID + "  ORDER BY ReceivedDate DESC";
            command.CommandText = "SELECT ID, BatchNO FROM [dbo].FinishedProductAfterProduction FPAP WHERE FPAP.DivisionID = " + divisionID + "  ORDER BY ReceivedDate DESC";
            da.SelectCommand = command;
            da.Fill(dt);
            this.CommitTransaction();
            this.ConnectionClosed();
            return dt;
        }
        public DataTable GetFGWiseBatch(int FGID)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            // command.CommandText = "SELECT ID, BatchNO + ' - ' + CONVERT(VARCHAR, ReceivedDate, 103) BatchNo FROM [dbo].FinishedProductAfterProduction FPAP WHERE FPAP.DivisionID = " + divisionID + "  ORDER BY ReceivedDate DESC";
            command.CommandText = "select distinct FPAP.BatchNo, FPAP.FinishedProductID from [dbo].FinishedProductAfterProduction FPAP WHERE FPAP.FinishedProductID = " + FGID + "  ORDER BY BatchNo";
            da.SelectCommand = command;
            da.Fill(dt);
            this.CommitTransaction();
            this.ConnectionClosed();
            return dt;
        }
        public DataTable GetBatchWiseMFCExpDate(string batchNo)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "select CONVERT(VARCHAR, MfgDate, 103) MfgDate, CONVERT(VARCHAR, ExpDate, 103) ExpDate from ProductionRequisition where BatchNo = '" + batchNo + "' ";
            da.SelectCommand = command;
            da.Fill(dt);
            this.CommitTransaction();
            this.ConnectionClosed();
            return dt;
        }
        public DataTable GetByID(string code)
        {
            string filterExpression = "SHO.ChallanNo = " + "'" + code + "'";
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.SendFPToHoGet;
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
        #endregion
    }
}
