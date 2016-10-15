using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SecurityModule.DataAccess;
using System.ComponentModel;

namespace SecurityModule.Provider
{
    public class RoleProvider : BaseProvider
    {
        #region properties
        public int RoleCode
        {
            get;
            set;
        }
        public string RoleName
        {
            get;
            set;
        }
        public string RoleDescription
        {
            get;
            set;
        }
        #endregion

        #region constants
        public const string RoleNameField = "RoleName";
        #endregion

        #region methods
        RoleDataAccess dataAccess=new RoleDataAccess();

        public bool Save(List<PageControlsProvider> roleDetailsList)
        {
            if (this.RoleName.Length == 0)
            {
                throw new Exception("Role Name never empty");
            }
            return dataAccess.Save(this, roleDetailsList);
        }
        public bool Update(List<PageControlsProvider> roleDetailsList)
        {
            if (this.RoleName.Length == 0)
            {
                throw new Exception("Role Name never empty");
            }
            return dataAccess.Update(this, roleDetailsList);
        }

        public DataSet GetAll()
        {
            return dataAccess.GetAll();
        }
        public DataSet GetAllByFilterExpression(string filterExpression)
        {
            return dataAccess.GetAllByFilterExpression(filterExpression);
        }
        public DataSet GetAllMenu()
        {
            return dataAccess.GetAllMenu();
        }
        public DataTable GetAllMenuByParenID(int pId)
        {
            return dataAccess.GetAllMenuByParentID(pId);
        }
        public DataTable GetAllMenuByParentIDandRoleCode(int pID, int roleCode)
        {
            return dataAccess.getAllMenuByParentIDandRoleCode(pID, roleCode);
        }
        #endregion
    }
}
