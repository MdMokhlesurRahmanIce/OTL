using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SecurityModule.DataAccess;
//using UniVATModule.Interfaces;

namespace SecurityModule.Provider
{
   public class GroupUserProvider
    {
       private PageControlsDataAccess dataAccess = new PageControlsDataAccess();
       private GroupUserDataAccess groupUserDA = new GroupUserDataAccess();

       #region Properties
       //Property for group
       public string GroupCode
       {
           get;
           set;
       }
       public string GroupName
       {
           get;
           set;
       }
       public string Description
       {
           get;
           set;
       }
       //Property for user
       public string UserID
       {
           get;
           set;
       }
       //Property for Role
       public string SecurityRuleCode
       {
           get;
           set;
       }
       #endregion

       public DataTable GetAllRole()
       {
           return dataAccess.GetAllRole();
       }
       public DataTable GetAllByGroupCode(string groupCode)
       {
           return groupUserDA.GetAllByGroupCode(groupCode);
       }
       public DataTable GetAllRoleByGroupCode(string groupCode)
       {
           return groupUserDA.GetAllRoleByGroupCode(groupCode);
       }
       public bool Save(GroupUserProvider GroupUser, List<GroupUserProvider> UserList, List<GroupUserProvider> RoleList,string mode)
       {
           return groupUserDA.Save(GroupUser, UserList, RoleList,mode);
       }
       public FormAccessRights GetFormAccessRightsByUserCodeAndFormName(string userCode, string formName)
       {
           FormAccessRights obj = new FormAccessRights();
           return obj.GetFormAccessRightsByUserCodeAndFormName(userCode, formName);
       }
    }
}
