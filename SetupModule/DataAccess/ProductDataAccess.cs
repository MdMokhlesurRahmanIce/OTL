using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SecurityModule.DataAccess;
using SetupModule.Provider;
using BaseModule;


namespace SetupModule.DataAccess
{
    public class ProductDataAccess:BaseDataAccess
    {
        private SqlDataAdapter sqlDataAdapter = null;

        #region Method
        private SqlCommand ProcedureFunction(ProductProvider provider)
        {
            SqlCommand command = new SqlCommand();
            ConnectionOpen();
            command.Connection = Connection;
            BeginTransaction(true);
            command.Transaction = Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.ProductSet;
            command.Parameters.Add("@Code", SqlDbType.VarChar).Value = provider.Code;
            command.Parameters.Add("@Name", SqlDbType.VarChar).Value = provider.Name;
            command.Parameters.Add("@Location", SqlDbType.VarChar).Value = provider.Location;
            command.Parameters.Add("@Specification", SqlDbType.VarChar).Value = provider.Specification;
            command.Parameters.Add("@PackSize", SqlDbType.VarChar).Value = provider.PackSize;
            command.Parameters.Add("@GenericName", SqlDbType.VarChar).Value = provider.GenericName;
            command.Parameters.Add("@DARNo", SqlDbType.VarChar).Value = provider.DARNo;
            command.Parameters.Add("@MesurementUnitID", SqlDbType.Int).Value = provider.MesurementUnitID; 
            command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = provider.DivisionID;
            command.Parameters.Add("@ItemTypeID", SqlDbType.Int).Value = provider.ItemTypeID;
            command.Parameters.Add("@SafetyStock", SqlDbType.Decimal).Value = provider.SafetyStock;
            command.Parameters.Add("@TradePrice", SqlDbType.Decimal).Value = provider.TradePrice;
            command.Parameters.Add("@MRP", SqlDbType.Decimal).Value = provider.MRP;
            command.Parameters.Add("@StatusID", SqlDbType.Int).Value = provider.StatusID;
            return command;
        }       
        public bool Save(ProductProvider provider)
        {
            bool isSave = false;
            try
            {   
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@EntryUserID",SqlDbType.Int).Value=provider.EntryUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();
                CommitTransaction();
                ConnectionClosed();
                isSave = true;
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
            return isSave;
        }
        public bool Update(ProductProvider provider)
        {
            bool isUpdate = false;
            try
            {
                SqlCommand command = null;
                command = ProcedureFunction(provider);
                command.Parameters.Add("@ID",SqlDbType.Int).Value=provider.ID;
                command.Parameters.Add("@UpdateUserID", SqlDbType.Int).Value = provider.UpdateUserID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();
                this.CommitTransaction();
                this.ConnectionClosed();
                isUpdate = true;
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
            return isUpdate;
        }
        public bool Delete(ProductProvider provider)
        {
            bool isDelete = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductSet;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = provider.ID;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Delete;
                command.ExecuteNonQuery();
                this.CommitTransaction();
                this.ConnectionClosed();
                isDelete = true;
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
            return isDelete;
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
                command.CommandText = StoredProcedureNames.ProductGet;
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
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductGet;
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
        public DataSet GetByID(int ID)
        {
            string filterExpression = "ID=" + ID.ToString();
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;

                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
                this.ConnectionClosed();
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            return ds;
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.ProductGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = filterExpression;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(ds);
                this.ConnectionClosed();
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            return ds;
        }
        public string GetMeasurementUnit(int productID)
        {
            string measurementUnit = string.Empty;
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT MU.Name FROM dbo.Product AS P INNER JOIN dbo.MeasurementUnit AS MU ON P.MesurementUnitID = MU.ID WHERE P.ID = " + productID + " ";
            measurementUnit = command.ExecuteScalar().ToString();
            this.CommitTransaction();
            this.ConnectionClosed();

            return measurementUnit;

        }
        public string GetFinishedProductCode(int productID)
        {
            string measurementUnit = string.Empty;
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT P.Code FROM dbo.Product AS P WHERE P.ID = " + productID + " ";
            measurementUnit = command.ExecuteScalar().ToString();
            this.CommitTransaction();
            this.ConnectionClosed();

            return measurementUnit;

        }
        public decimal GetTradePrice(int productID)
        {
            decimal tradePrice = 0.0M;
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT ISNULL(P.TradePrice, 0) TradePrice FROM dbo.Product AS P WHERE P.ID = " + productID + " ";
            tradePrice = Convert.ToDecimal(command.ExecuteScalar());
            this.CommitTransaction();
            this.ConnectionClosed();

            return tradePrice;

        }
        public bool GetMeasurementUnit(int productID, string reqRefNo)
        {
            bool isExist = false;

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT RD.ProductID FROM dbo.Requisition AS R INNER JOIN dbo.RequisitionDetail AS RD ON R.ID = RD.RequisitionID where ReferenceNo = '" + reqRefNo + "' AND RD.ProductID = " + productID + " ";
            da.SelectCommand = command;            
            da.Fill(dt);            
            this.CommitTransaction();
            this.ConnectionClosed();
            if(dt.Rows.Count > 0 && dt !=null)
            isExist = true;
            return isExist;            
        }
        public string GetPackSizeName(int productID)
        {
            DataTable dt = new DataTable();
            string packSize = string.Empty;
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT P.PackSize FROM dbo.Product AS P WHERE P.ID = " + productID + " ";
            sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dt);
            this.CommitTransaction();
            this.ConnectionClosed();
            if (dt.Rows.Count > 0)
            {
                return packSize = dt.Rows[0]["PackSize"].ToString();
            }
            else
            return packSize = string.Empty;
        }
        public string GetStockLocation(int productID)
        {
            DataTable dt = new DataTable();
            string stockLocation = string.Empty;
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandText = "SELECT P.Location FROM dbo.Product AS P WHERE P.ID = " + productID + " ";
            sqlDataAdapter = new SqlDataAdapter(command);
            sqlDataAdapter.Fill(dt);
            this.CommitTransaction();
            this.ConnectionClosed();
            if (dt.Rows.Count > 0)
            {
                return stockLocation = dt.Rows[0]["Location"].ToString();
            }
            else
                return stockLocation = string.Empty;
        }
        public decimal GetPresentStock(int productID)
        {
            DataTable dt = new DataTable();
            decimal packSize = 0.0M;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetProductCurrentStock;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = productID;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
                this.ConnectionClosed();
            }
            catch (SqlException exp)
            {
                throw new Exception(exp.Message);
            }
            if (dt.Rows.Count > 0)
            {
                return packSize = Convert.ToDecimal(dt.Rows[0]["PresentStock"]);
            }
            else
                return packSize = 0.00M;
        }
        public DataTable GetProductForProductSearch(int codeOrName, int productType, int divisionID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetProductForProductSearch]";
                command.Parameters.Add("@CodeOrName", SqlDbType.Int).Value = codeOrName;
                command.Parameters.Add("@ProductType", SqlDbType.Int).Value = productType;
                command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = divisionID;
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
        public DataTable GetDivisionWiseProduct(int divisionID)
        {
            DataTable dt = new DataTable();                       
            try
            {
                string stockLocation = string.Empty;
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandText = "SELECT ID, P.Code + ' - ' +P.Name AS ProductName FROM dbo.Product AS P WHERE DivisionID = " + divisionID + " ";
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(dt);
                this.CommitTransaction();            
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

        //internal DataSet GetAllProductByCode(string filterExpression)
        //{
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        SqlCommand command = new SqlCommand();
        //        this.ConnectionOpen();
        //        command.Connection = Connection;
        //        command.CommandType = CommandType.StoredProcedure;
        //        command.CommandText = StoredProcedureNames.ProductGet;
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
    }
}
