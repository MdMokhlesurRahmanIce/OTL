using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SecurityModule.Provider;


namespace SecurityModule.DataAccess
{
    [Serializable]
    public class FormAccessRights : BaseDataAccess
    {
        #region Properties
        private int vid;
        [Browsable(false)]
        public int VID
        {
            get { return vid; }
            internal set { vid = value; }
        }
        [Browsable(false), DisplayName("CanSelect")]
        public System.Boolean CanSelect
        {
            get
            {
                return _CanSelect;
            }
            set
            {
                _CanSelect = value;
            }
        }
        private System.Boolean _CanSelect;
        [Browsable(false), DisplayName("CanInsert")]
        public System.Boolean CanInsert
        {
            get
            {
                return _CanInsert;
            }
            set
            {
                _CanInsert = value;
            }
        }
        private System.Boolean _CanInsert;

        [Browsable(false), DisplayName("CanSend")]
        public System.Boolean CanSend
        {
            get
            {
                return _CanSend;
            }
            set
            {
                _CanSend = value;
            }
        }
        private System.Boolean _CanSend;

        [Browsable(false), DisplayName("CanCheck")]
        public System.Boolean CanCheck
        {
            get
            {
                return _CanCheck;
            }
            set
            {
                _CanCheck = value;
            }
        }
        private System.Boolean _CanCheck;

        [Browsable(false), DisplayName("CanApprove")]
        public System.Boolean CanApprove
        {
            get
            {
                return _CanApprove;
            }
            set
            {
                _CanApprove = value;
            }
        }
        private System.Boolean _CanApprove;



        [Browsable(false), DisplayName("CanUpdate")]
        public System.Boolean CanUpdate
        {
            get
            {
                return _CanUpdate;
            }
            set
            {
                _CanUpdate = value;
            }
        }
        private System.Boolean _CanUpdate;
        [Browsable(false), DisplayName("CanDelete")]
        public System.Boolean CanDelete
        {
            get
            {
                return _CanDelete;
            }
            set
            {
                _CanDelete = value;
            }
        }
        private System.Boolean _CanDelete;

        [Browsable(false), DisplayName("CanPreview")]
        public System.Boolean CanPreview
        {
            get
            {
                return _CanPreview;
            }
            set
            {
                _CanPreview = value;
            }
        }
        private System.Boolean _CanPreview;

        [Browsable(false), DisplayName("CanReceive")]
        public System.Boolean CanReceive
        {
            get
            {
                return _CanReceive;
            }
            set
            {
                _CanReceive = value;
            }
        }
        private System.Boolean _CanReceive;

        #endregion
        public FormAccessRights GetFormAccessRightsByUserCodeAndFormName(string userCode, string formName)
        {
            FormAccessRights newFormAccessRights = new FormAccessRights();
            try
            {
                SqlCommand command = new SqlCommand();
                ConnectionOpen();
                command.Connection = Connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetFormAccessRights]";
                command.Parameters.Add("@UserCode", SqlDbType.VarChar).Value = userCode;
                command.Parameters.Add("@FormName", SqlDbType.VarChar).Value = formName;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    newFormAccessRights.CanSelect = Convert.ToBoolean(reader[0]);
                    newFormAccessRights.CanInsert = Convert.ToBoolean(reader[1]);
                    newFormAccessRights.CanUpdate = Convert.ToBoolean(reader[2]);
                    newFormAccessRights.CanDelete = Convert.ToBoolean(reader[3]);
                    newFormAccessRights.CanSend = Convert.ToBoolean(reader[4]);
                    newFormAccessRights.CanCheck = Convert.ToBoolean(reader[5]);
                    newFormAccessRights.CanApprove = Convert.ToBoolean(reader[6]);
                    newFormAccessRights.CanPreview = Convert.ToBoolean(reader[7]);
                    newFormAccessRights.CanReceive = Convert.ToBoolean(reader[8]);
                }
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
            return newFormAccessRights;
        }
    }
}
