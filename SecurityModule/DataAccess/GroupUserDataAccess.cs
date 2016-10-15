using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using SecurityModule.Provider;
using System.Data;
using BaseModule;

namespace SecurityModule.DataAccess
{
   public class GroupUserDataAccess:BaseDataAccess
    {
        SqlDataAdapter sqlDataAdapter = null;
        DataTable dt;
       public bool Save(GroupUserProvider GroupUser, List<GroupUserProvider> userList, List<GroupUserProvider> RoleList,string mode)
       {
           bool IsSave = false;
           String GroupCode = string.Empty;
           try
           {
               SqlCommand command = new SqlCommand();
               ConnectionOpen();
               command.Connection = Connection;
               this.BeginTransaction(true);
               command.Transaction = this.Transaction;
               command.CommandType = CommandType.StoredProcedure;
               command.CommandText = StoredProcedureNames.GroupSet;
               if (mode == "Save")
               {
                   SqlParameter t = new SqlParameter("@GroupCode", SqlDbType.VarChar);
                   t.Direction = ParameterDirection.Output;
                   t.Size = 16;
                   command.Parameters.Add(t);
                   command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
               }
               else
               {
                   command.Parameters.Add("@GroupCode", SqlDbType.VarChar, 100).Value = GroupUser.GroupCode;
                   command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Update;
               }
               command.Parameters.Add("@GroupName", SqlDbType.VarChar,100).Value = GroupUser.GroupName;
               command.Parameters.Add("@Description", SqlDbType.VarChar, 200).Value = GroupUser.Description;
               command.ExecuteNonQuery();

               if (mode == "Save")
               {
                   GroupCode = (string)command.Parameters["@GroupCode"].Value;
               }
               else
               {
                   GroupCode = GroupUser.GroupCode;
               }
               //-------------------------------------Group User--------------------------------------
               int update = 1;
               foreach (GroupUserProvider groupUser in userList)
               {
                   command = new SqlCommand();
                   command.Connection = Connection;
                   command.Transaction = this.Transaction;
                   command.CommandType = CommandType.StoredProcedure;
                   command.CommandText = StoredProcedureNames.GroupUserSet;
                   command.Parameters.Add("@UserID", SqlDbType.VarChar).Value = groupUser.UserID;
                   command.Parameters.Add("@GroupCode", SqlDbType.VarChar).Value = GroupCode;

                   command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                   command.Parameters.Add("@Update", SqlDbType.Int).Value = update;
                   command.ExecuteNonQuery();
                   update++;
               }
               update = 1;
               foreach (GroupUserProvider groupRole in RoleList)
               {
                   command = new SqlCommand();
                   command.Connection = Connection;
                   command.Transaction = this.Transaction;
                   command.CommandType = CommandType.StoredProcedure;
                   command.CommandText = StoredProcedureNames.GroupRoleSet;
                   command.Parameters.Add("@RoleCode", SqlDbType.VarChar).Value = groupRole.SecurityRuleCode;
                   command.Parameters.Add("@GroupCode", SqlDbType.VarChar).Value = GroupCode;

                   command.Parameters.Add("@Option", SqlDbType.Int).Value = DBConstants.DataModificationOption.Insert;
                   command.Parameters.Add("@Update", SqlDbType.Int).Value = update;
                   command.ExecuteNonQuery();
                   update++;
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
       public DataTable GetAllByGroupCode(string groupCode)
       {
           try
           {
               SqlCommand command = new SqlCommand();
               dt = new DataTable();
               this.ConnectionOpen();
               command.Connection = Connection;
               command.CommandType = CommandType.Text;
               command.CommandText = "select *from [UserAccess].[GroupUsers] Where GroupCode='" + groupCode + "'";
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
       public DataTable GetAllRoleByGroupCode(string groupCode)
       {
           try
           {
               SqlCommand command = new SqlCommand();
               dt = new DataTable();
               this.ConnectionOpen();
               command.Connection = Connection;
               command.CommandType = CommandType.Text;
               command.CommandText = "select *from [UserAccess].[GroupRole] Where GroupCode='" + groupCode + "'";
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
