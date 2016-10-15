using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using SecurityModule.Provider;
using BaseModule;
namespace SecurityModule.DataAccess
{
    class PageControlsDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        private DataTable aTable;
        SqlCommand command = null;
        public DataTable GetAllPageControls()
        {
            aTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.UserPageControlsGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(aTable);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return aTable;
        }
        public DataTable GetAllbyRoleCode(string roleCode)
        {
            aTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.UserPageControlsGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = roleCode;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(aTable);
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
            finally
            {
                this.ConnectionClosed();
            }
            return aTable;
        }
        public DataTable GetAllRole()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT RoleCode,RoleName FROM [UserAccess].[Role]";
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
        public DataTable GetAllPageByRoleName(string roleCode)
        {
            var aTable = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = StoredProcedureNames.UserPageControlsGet;
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = roleCode;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadWithFilterExpression;
                sqlDataAdapter = new SqlDataAdapter(command);
                sqlDataAdapter.Fill(aTable);
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
            return aTable;
        }
        public bool SavePageControls(List<PageControlsProvider> aList ,string text)
        {
            try
            {
                if (text == "Submit")
                {
                    foreach (var aListObj in aList)
                    {
                        command = ProcedureFunction(aListObj);
                        command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                        command.ExecuteNonQuery();
                        CommitTransaction();
                        ConnectionClosed();
                    }
                }
            }
            catch (Exception exp)
            {
                RollbackTransaction();
                throw new Exception(exp.Message);
            }
            finally
            {
                ConnectionClosed();
            }
            return true;
        }
        public bool UpdatePageControls(List<PageControlsProvider> aList, string text)
        {
            try
            {
                foreach (var aListObj in aList)
                {
                    command = ProcedureFunction(aListObj);
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                    CommitTransaction();
                    ConnectionClosed();
                }
            }
            catch (Exception exp)
            {
                RollbackTransaction();
                throw new Exception(exp.Message);
            }
            finally
            {
                ConnectionClosed();
            }
            return true;
        }
        private SqlCommand ProcedureFunction(PageControlsProvider aRow)
        {
            command = new SqlCommand();
            ConnectionOpen();
            command.Connection = Connection;
            BeginTransaction(true);
            command.Transaction = Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = StoredProcedureNames.UserPageControlsSet;
            command.Parameters.Add("@Ser", SqlDbType.Int).Value = 0;
            command.Parameters.Add("@MenuID", SqlDbType.Int).Value = aRow.MenuId;
            command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = aRow.RoleCode;
            command.Parameters.Add("@CanSelect", SqlDbType.Bit).Value = aRow.CanSelect;
            command.Parameters.Add("@CanInsert", SqlDbType.Bit).Value = aRow.CanInsert;
            command.Parameters.Add("@CanUpdate", SqlDbType.Bit).Value = aRow.CanUpdate;
            command.Parameters.Add("@CanDelete", SqlDbType.Bit).Value = aRow.CanDelete;
            command.Parameters.Add("@CanPreview", SqlDbType.Bit).Value = aRow.CanPreview;
            command.Parameters.Add("@CanReceive", SqlDbType.Bit).Value = aRow.CanReceive;
            command.Parameters.Add("@AllChk", SqlDbType.Bit).Value = aRow.AllChk;
            return command;
        }
        //public string GetARoleCode(string text)
        //{
        //    var query = string.Format("SELECT * FROM [UniVAT].[UserAccess].[Role] Where RoleName='{0}'", text);
        //    var adapter = new SqlDataAdapter(query, GetConnectionString());
        //    aTable = new DataTable();
        //    adapter.Fill(aTable);
        //    return (from DataRow aRow in aTable.Rows select aRow[0].ToString()).FirstOrDefault();
        //}
        //public string GetAMenuId(string caption)
        //{
        //    var query = string.Format("SELECT* FROM [UniVAT].[UserAccess].[Menu] Where Caption='{0}'", caption);
        //    var adapter = new SqlDataAdapter(query, GetConnectionString());
        //    aTable = new DataTable();
        //    adapter.Fill(aTable);
        //    return (from DataRow aRow in aTable.Rows select aRow[0].ToString()).FirstOrDefault();
        //}
    }
}
