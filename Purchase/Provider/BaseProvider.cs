using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace PurchaseModule.Provider
{
    public class BaseProvider
    {
        #region Common  Properties
        private int vid;
        [Browsable(false)]
        public int VID
        {
            get { return vid; }
            internal set { vid = value; }
        }
        public int EntryUserID
        {
            get;
            set;
        }
        public int CheckUserID
        {
            get;
            set;
        }
        public int ApproveUserID
        {
            get;
            set;
        }
        public DateTime EntryDate
        {
            get;
            set;
        }
        public int UpdateUserID
        {
            get;
            set;
        }
        public DateTime UpdateDate
        {
            get;
            set;
        }
        public Int64 Ts
        {
            get;
            set;
        }
        #endregion
        #region Constants
        //public const string EntryUserIDField = "EntryUserID";
        //public const string EntryDateField = "EntryDate";
        //public const string UpdateUserIDField = "UpdateUserID";
        //public const string UpdateDateField = "UpdateDate";
        //public const string TsField = "Ts";
        #endregion
    }
}
