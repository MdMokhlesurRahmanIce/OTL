using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SecurityModule.DataAccess;
using System.ComponentModel;

namespace SecurityModule.Provider
{
    public class PageControlsProvider
    {
        #region Properties
        //private int _Age;
        //public int Age
        //{
        //    get { return _Age; }
        //    set { _Age = value; }
        //}
        private string _MenuId;
        public string MenuId {
            get { return _MenuId; }
            set { _MenuId = value; }
        }
        private string _Caption;
        public string Caption
        {
            get { return _Caption; }
            set { _Caption = value; }
        }
        private int _RoleCode;
        public int RoleCode
        {
            get { return _RoleCode; }
            set { _RoleCode = value; }
        }
        private bool _CanSelect;
        public bool CanSelect
        {
            get { return _CanSelect; }
            set { _CanSelect = value; }
        }
        private bool _CanInsert;
        public bool CanInsert
        {
            get { return _CanInsert; }
            set { _CanInsert = value; }
        }
        private bool _CanDelete;
        public bool CanDelete
        {
            get { return _CanDelete; }
            set { _CanDelete = value; }
        }
        private bool _CanUpdate;
        public bool CanUpdate
        {
            get { return _CanUpdate; }
            set { _CanUpdate = value; }
        }
        private bool _CanSend;
        public bool CanSend
        {
            get { return _CanSend; }
            set { _CanSend = value; }
        }
        private bool _CanCheck;
        public bool CanCheck
        {
            get { return _CanCheck; ; }
            set { _CanCheck = value; }
        }
        private bool _CanApprove;
        public bool CanApprove
        {
            get { return _CanApprove; }
            set { _CanApprove = value; }
        }
        private bool _CanPreview;
        public bool CanPreview
        {
            get { return _CanPreview; }
            set { _CanPreview = value; }
        }
        private bool _CanReceive;
        public bool CanReceive
        {
            get { return _CanReceive; }
            set { _CanReceive = value; }
        }

        private bool _allChk;
        public bool AllChk
        {
            get { return _allChk; }
            set { _allChk = value; }
        }
        //public string Caption { get; set; }
       
        //public string RoleCode { get; set; }
       
        //public bool CanSelect { get; set; }
       
        //public bool CanInsert { get; set; }
        
        //public bool CanUpdate { get; set; }
       
        //public bool CanDelete { get; set; }
       
        //public bool CanPreview { get; set; }
       
        //public bool CanPrint { get; set; }
       
        //public bool All { get; set; }
        #endregion
        #region Constants
        public const string RoleNameField = "RoleName";

        #endregion

        #region Method

        private PageControlsDataAccess dataAccess;
        #endregion
        public DataTable GetAllPageControls()
        {
            dataAccess = new PageControlsDataAccess();
            return dataAccess.GetAllPageControls();
        }
       
        public bool SavePageControls(List<PageControlsProvider> aList,string text)
        {
            dataAccess = new PageControlsDataAccess();
            return dataAccess.SavePageControls(aList, text);
        }

        public bool UpdatePageControls(List<PageControlsProvider> aList, string text)
        {
            dataAccess = new PageControlsDataAccess();
            return dataAccess.UpdatePageControls(aList, text);
        }

        //public DataTable GetAllPageByRoleName(string roleName)
        //{
        //    dataAccess = new PageControlsDataAccess();
        //    return dataAccess.GetAllbyRoleCode(roleName);
        //}
       
        //public object GetAllPageForUpdate(string roleName)
        //{
        //    dataAccess = new PageControlsDataAccess();
        //    var aTable = dataAccess.GetAllbyRoleCode(roleName);
        //    var newTable = new DataTable();
           
        //    newTable.Columns.Add("MenuID");
        //    newTable.Columns.Add("Caption");
        //    newTable.Columns.Add("CanSelect");
        //    //newTable.Columns.Add("CanInsert");
        //    //newTable.Columns.Add("CanUpdate");
        //    //newTable.Columns.Add("CanDelete");
        //    //newTable.Columns.Add("CanPreview");
        //    //newTable.Columns.Add("CanPrint");
        //    dataAccess = new PageControlsDataAccess();
        //    var aTable = dataAccess.GetAllbyRoleCode(roleName);
        //    var newTable = new DataTable();

        //    foreach (DataRow aRow in aTable.Rows)
        //    {
        //         var row = newTable.NewRow();
        //        row["MenuID"] = aRow[1].ToString();
        //        row["Caption"] = aRow[1].ToString();
        //        row["CanSelect"] = aRow[3];
        //        //row["CanInsert"] = aRow[4];
        //        //row["CanUpdate"] = aRow[5];
        //        //row["CanDelete"] = aRow[6];
        //        //row["CanPreview"] = aRow[7];
        //        //row["CanPrint"] = aRow[8];
        //        newTable.Rows.Add(row);
        //    }
        //    return newTable;
        //}
        public DataTable CreateDataSource(string roleCode)
        {
            DataTable dataTable = new DataTable("Users");
            DataColumn Caption = new DataColumn("Caption", Type.GetType("System.String"));
            DataColumn select = new DataColumn("Select", Type.GetType("System.Boolean"));
            DataColumn add = new DataColumn("Add", Type.GetType("System.Boolean"));
            DataColumn edit = new DataColumn("Edit", Type.GetType("System.Boolean"));
            DataColumn delate = new DataColumn("Delate", Type.GetType("System.Boolean"));
            DataColumn preview = new DataColumn("Preview", Type.GetType("System.Boolean"));
            DataColumn receive = new DataColumn("Receive", Type.GetType("System.Boolean"));
            //Create a column All to store the all permission access
            DataColumn all = new DataColumn("All", Type.GetType("System.Boolean"));

            //Add the columns to the table
            dataTable.Columns.Add(Caption);
            dataTable.Columns.Add(select);
            dataTable.Columns.Add(add);
            dataTable.Columns.Add(edit);
            dataTable.Columns.Add(delate);
            dataTable.Columns.Add(preview);
            dataTable.Columns.Add(receive);
            dataTable.Columns.Add(all);
           
            dataAccess = new PageControlsDataAccess();
            var aTable = dataAccess.GetAllbyRoleCode(roleCode);
            //var newTable = new DataTable();

            foreach (DataRow aRow in aTable.Rows)
            {
                var row = dataTable.NewRow();
                row["Caption"] = aRow[11].ToString();
                row["Select"] = aRow[3];
                row["Add"] = aRow[4];
                row["Edit"] = aRow[5];
                row["Delate"] = aRow[6];
                row["Preview"] = aRow[7];
                row["Receive"] = aRow[8];
                dataTable.Rows.Add(row);
            }
            return dataTable;
        }
        //public string GetMenuId(string caption)
        //{
        //    if (caption != string.Empty)
        //    {
        //        dataAccess = new PageControlsDataAccess();
        //        return dataAccess.GetAMenuId(caption);
        //    }
        //    return null;
        //}
        //public string GetRoleCode(string text)
        //{
        //    if (text != string.Empty)
        //    {
        //        dataAccess = new PageControlsDataAccess();
        //        return dataAccess.GetARoleCode(text);
        //    }
        //    return null;
        //}

        public DataTable GetAllbyRoleCode(string rolecode)
        {
            dataAccess = new PageControlsDataAccess();
            return dataAccess.GetAllbyRoleCode(rolecode);
        }
    }
}
