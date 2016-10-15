using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using BaseModule;
using System.Threading.Tasks;
using SecurityModule.Provider;

namespace SecurityModule.DataAccess
{
    [Serializable]
    public class BaseDataAccess
    {
        [NonSerialized]
        private SqlConnection _connection;
        [NonSerialized]
        private SqlTransaction _transaction;

        public SqlConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
        public SqlTransaction Transaction
        {
            get { return _transaction; }
            set { _transaction = value; }
        }
        public bool UseTransaction
        {
            get;
            set;
        }
        public string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["technoConnectionString"].ConnectionString;
            return connectionString;
        }
        public BaseDataAccess()
        {
        }
        public void ConnectionOpen()
        {
            _connection = new SqlConnection { ConnectionString = GetConnectionString() };
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            if (_connection.State != ConnectionState.Open)
            {
                throw new Exception("Connection Failed");
            }
        }

        public void ConnectionClosed()
        {
            if (_connection != null)
            {
                if (_connection.State == ConnectionState.Open || _connection.State == ConnectionState.Fetching || _connection.State == ConnectionState.Executing)
                {
                    _connection.Close();
                    UseTransaction = false;
                }
            }
        }
        protected void BeginTransaction(bool isTransaction)
        {
            if (isTransaction)
            {
                _transaction = _connection.BeginTransaction();
                UseTransaction = true;
            }
        }
        protected void CommitTransaction()
        {
            if (UseTransaction && (_transaction != null))
            {
                _transaction.Commit();
            }
        }
        protected void RollbackTransaction()
        {
            if (UseTransaction)
                _transaction.Rollback();

        }
        private void AddParam(SqlCommand command, string columnVame, object value, SqlDbType sqlDbType)
        {
            command.Parameters.Add("@" + columnVame, sqlDbType).Value = value;
        }
        public SqlCommand ProcedureFunction(object provider, string commandText)
        {
            SqlCommand command = new SqlCommand();
            this.ConnectionOpen();
            command.Connection = Connection;
            this.BeginTransaction(true);
            command.Transaction = this.Transaction;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = commandText;
            ObjectReflector objectReflector = new ObjectReflector();
            var propertyList = objectReflector.GetPropertyList(provider);
            foreach (KeyValuePair<string, ObjectReflector.ProviderPropertyInfo> providerPropertyInfo in propertyList)
            {
                if (providerPropertyInfo.Key.ToUpper() == "EntryDate".ToUpper() ||
                 providerPropertyInfo.Key.ToUpper() == "TS" ||
                 providerPropertyInfo.Key.ToUpper() == "UpdateDate".ToUpper())
                {
                    continue;
                }
                AddParam(command, providerPropertyInfo.Key, providerPropertyInfo.Value.Value, providerPropertyInfo.Value.SqlDbType);
            }

            return command;
        }
    }
}
