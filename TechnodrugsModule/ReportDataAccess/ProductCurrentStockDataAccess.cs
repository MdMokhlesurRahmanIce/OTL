using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SecurityModule.DataAccess;
using TechnodrugsModule.ReportProvider;
//using SetupModule.Provider;
using BaseModule;

namespace TechnodrugsModule.ReportDataAccess
{
    internal class ProductCurrentStockDataAccess : BaseDataAccess
    {
        private SqlDataAdapter sqlDataAdapter = null;
        internal DataTable GetAll(ProductCurrentStockProvider provider)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetAllTypeProductStock;
                command.Parameters.Add("@ProductID", SqlDbType.Int).Value = provider.ProductID;
                command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = provider.FromDate;
                command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = provider.Todate;
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
        internal DataTable GetDivisionAndItemwise(ProductCurrentStockProvider provider)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.GetAllProductSummaryStock;
                command.Parameters.Add("@DivisionID", SqlDbType.Int).Value = provider.DivisionID;
                command.Parameters.Add("@ProductType", SqlDbType.Int).Value = provider.ProductType;
                command.Parameters.Add("@fromDate", SqlDbType.DateTime).Value = provider.FromDate;
                command.Parameters.Add("@toDate", SqlDbType.DateTime).Value = provider.Todate;
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
    }
    
}
