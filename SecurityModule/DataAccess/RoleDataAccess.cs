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
    public class RoleDataAccess : BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        
        private SqlCommand ProcedureFunction(RoleProvider provider)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "[UserAccess].[RoleSet]";
            //command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = provider.RoleCode;
            command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = provider.RoleName;
            command.Parameters.Add("@RoleDescription", SqlDbType.NVarChar).Value = provider.RoleDescription;
            return command;
        }
        public bool Save(RoleProvider roleMasterList, List<PageControlsProvider> roleDetailList)
        {
            int roleCode = 0;
            bool isSave;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[UserAccess].[RoleSet]";

                SqlParameter RoleCode = new SqlParameter("@RoleCode", SqlDbType.Int);
                RoleCode.Direction = ParameterDirection.Output;
                command.Parameters.Add(RoleCode);

                command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = roleMasterList.RoleName;
                command.Parameters.Add("@RoleDescription", SqlDbType.NVarChar).Value = roleMasterList.RoleDescription;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                command.ExecuteNonQuery();

                roleCode = (int)command.Parameters["@RoleCode"].Value;

                foreach (PageControlsProvider objList in roleDetailList)
                {
                    command = new SqlCommand();
                    command.Connection = Connection;
                    command.Transaction = Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[UserAccess].[RoleDetailSet]";
                    command.Parameters.Add("@MenuID", SqlDbType.Int).Value = objList.MenuId;
                    command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = roleCode;
                    command.Parameters.Add("@CanSelect", SqlDbType.Bit).Value = objList.CanSelect;
                    command.Parameters.Add("@CanInsert", SqlDbType.Bit).Value = objList.CanInsert;
                    command.Parameters.Add("@CanUpdate", SqlDbType.Bit).Value = objList.CanUpdate;
                    command.Parameters.Add("@CanDelete", SqlDbType.Bit).Value = objList.CanDelete;
                    command.Parameters.Add("@CanSend", SqlDbType.Bit).Value = objList.CanSend;
                    command.Parameters.Add("@CanCheck", SqlDbType.Bit).Value = objList.CanCheck;
                    command.Parameters.Add("@CanApprove", SqlDbType.Bit).Value = objList.CanApprove;
                    command.Parameters.Add("@CanPreview", SqlDbType.Bit).Value = objList.CanPreview;
                    command.Parameters.Add("@CanReceive", SqlDbType.Bit).Value = objList.CanReceive;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                    command.ExecuteNonQuery();
                }

                this.CommitTransaction();
                this.ConnectionClosed();
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
        public bool Update(RoleProvider roleMasterList, List<PageControlsProvider> roleDetailList)
        {
            bool isUpdate = false;
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                this.BeginTransaction(true);
                command.Transaction = this.Transaction;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[UserAccess].[RoleSet]";

                command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = roleMasterList.RoleCode;
                command.Parameters.Add("@RoleName", SqlDbType.NVarChar).Value = roleMasterList.RoleName;
                command.Parameters.Add("@RoleDescription", SqlDbType.NVarChar).Value = roleMasterList.RoleDescription;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                command.ExecuteNonQuery();

                foreach (PageControlsProvider objList in roleDetailList)
                {
                    command = new SqlCommand();
                    //ConnectionOpen();
                    command.Connection = Connection;
                    command.Transaction = Transaction;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "[UserAccess].[RoleDetailSet]";
                    command.Parameters.Add("@MenuID", SqlDbType.Int).Value = objList.MenuId;
                    command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = roleMasterList.RoleCode;
                    command.Parameters.Add("@CanSelect", SqlDbType.Bit).Value = objList.CanSelect;
                    command.Parameters.Add("@CanInsert", SqlDbType.Bit).Value = objList.CanInsert;
                    command.Parameters.Add("@CanUpdate", SqlDbType.Bit).Value = objList.CanUpdate;
                    command.Parameters.Add("@CanDelete", SqlDbType.Bit).Value = objList.CanDelete;
                    command.Parameters.Add("@CanSend", SqlDbType.Bit).Value = objList.CanSend;
                    command.Parameters.Add("@CanCheck", SqlDbType.Bit).Value = objList.CanCheck;
                    command.Parameters.Add("@CanApprove", SqlDbType.Bit).Value = objList.CanApprove;
                    command.Parameters.Add("@CanPreview", SqlDbType.Bit).Value = objList.CanPreview;
                    command.Parameters.Add("@CanReceive", SqlDbType.Bit).Value = objList.CanReceive;
                    command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
                    command.ExecuteNonQuery();
                }
                CommitTransaction();
                ConnectionClosed();
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
        public DataSet GetAll()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[UserAccess].[RoleGet]";
                command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
                command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
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
            SqlDataAdapter sqlDataAdapter = null;
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[UserAccess].[RoleGet]";
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
        public DataSet GetAllMenu()
        {
            DataSet ds = new DataSet();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.Text;
                command.CommandText = "select ID,Caption from UserAccess.Menu where ParentID=0 order by Caption asc";
                //command.Parameters.Add("@FilterExpression", SqlDbType.VarChar).Value = string.Empty;
                //command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataLoadingOption.LoadAll;
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
        public DataTable GetAllMenuByParentID(int parentID)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.Text;
                if (parentID != 0)
                {
                    command.CommandText = "SELECT UM.ID as MenuID,UM.Caption,cast(0 as bit)CanSelect,cast(0 as bit)CanInsert,cast(0 as bit)CanUpdate,cast(0 as bit)CanDelete,cast(0 as bit)CanPreview,cast(0 as bit)CanPrint,cast(0 as bit)AllChk FROM [UserAccess].[Menu]AS UM WHERE Location !='' and ParentID=" + parentID;
                    //command.CommandText = "select ID as MenuID,Caption,CAST(0 as bit)chkSelectAll, CAST(0 as bit)chkSelect,CAST(0 as bit)chkInsertAll,CAST(0 as bit)chkAdd,CAST(0 as bit)chkEditAll,CAST(0 as bit)chkEdit,CAST(0 as bit)chkDelateAll,CAST(0 as bit)chkDelate,CAST(0 as bit)chkPreviewAll,CAST(0 as bit)chkPreview,CAST(0 as bit)chkPrintAll,CAST(0 as bit)chkPrint from UserAccess.Menu where ParentID=" + parentID;
                }
                else
                {
                    command.CommandText = "SELECT UM.ID as MenuID,UM.Caption,cast(0 as bit)CanSelect,cast(0 as bit)CanInsert,cast(0 as bit)CanUpdate,cast(0 as bit)CanDelete,cast(0 as bit)CanPreview,cast(0 as bit)CanPrint,cast(0 as bit)AllChk FROM [UserAccess].[Menu]AS UM WHERE Location !=''";
                    //command.CommandText = "select ID as MenuID,Caption,CAST(0 as bit)chkSelectAll, CAST(0 as bit)chkSelect,CAST(0 as bit)chkInsertAll,CAST(0 as bit)chkAdd,CAST(0 as bit)chkEditAll,CAST(0 as bit)chkEdit,CAST(0 as bit)chkDelateAll,CAST(0 as bit)chkDelate,CAST(0 as bit)chkPreviewAll,CAST(0 as bit)chkPreview,CAST(0 as bit)chkPrintAll,CAST(0 as bit)chkPrint from UserAccess.Menu";
                }
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
        public DataTable getAllMenuByParentIDandRoleCode(int parentID, int roleCode)
        {
            DataTable dt = new DataTable();
            try
            {
                SqlCommand command = new SqlCommand();
                this.ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[UserAccess].[spGetRoleByParentIDandRoleCode]";
                command.Parameters.Add("@ParentID", SqlDbType.Int).Value = parentID;
                command.Parameters.Add("@RoleCode", SqlDbType.Int).Value = roleCode;
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
    }
}
