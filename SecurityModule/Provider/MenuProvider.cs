using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.DataAccess;



namespace SecurityModule.Provider
{
    public class MenuProvider
    {
        #region Proparties
        public int ID
        {
            get;
            set;
        }
        public int MenuID
        {
            get;
            set;
        }
        public int ParentID
        {
            get;
            set;
        }
        public bool IsActive
        {
            get;
            set;
        }
        public string Caption
        {
            get;
            set;
        }
        public string ToolTip
        {
            get;
            set;
        }
        public string Location
        {
            get;
            set;
        }
        public int ModuleID
        {
            get;
            set;
        }
        public int MenuOrder
        {
            get;
            set;
        }
        #endregion

        #region Methods
        public DataTable GetAll()
        {
            MenuDataAccess dataAccess = new MenuDataAccess();
            return dataAccess.GetAll();
        }
        //public DataTable GetAllByUserGroupID(int userGroupID)
        //{
        //    MenuDataAccess dataAccess = new MenuDataAccess();
        //    return dataAccess.GetAllByUserGroupID(userGroupID);
        //}
        public DataTable GetAllByUserGroupID(String userID)
        {
            MenuDataAccess dataAccess = new MenuDataAccess();
            return dataAccess.GetAllByUserGroupID(userID);
        }
        public DataSet GetParentMenu()
        {
            MenuDataAccess dataAccess = new MenuDataAccess();
            return dataAccess.GetParentMenu();
        }
        public DataSet GetSubMenuByParentMenuId(int parentID)
        {
            MenuDataAccess dataAccess = new MenuDataAccess();
            return dataAccess.GetSubMenuByParentMenuId(parentID);
        }
        #endregion
    }
}
