using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule
{
    public class ObjectReflector
    {
        //Dictionary<string,ProviderPropertyInfo> 
        public Dictionary<string, ProviderPropertyInfo> GetPropertyList(object pModel)
        {
            Dictionary<string, ProviderPropertyInfo> columnValueCol = GetColumnValueCol(pModel);
            //return BuildCREATEQuery(vHT, null).ToString();
            return columnValueCol;
        }

        public class ProviderPropertyInfo
        {
            public string Name { get; set; }
            public object Value { get; set; }
            public SqlDbType SqlDbType { get; set; }
        }

        internal Dictionary<string, ProviderPropertyInfo> GetColumnValueCol(object pModel)
        {
            Dictionary<string, ProviderPropertyInfo> columnvaluetypeList = new Dictionary<string, ProviderPropertyInfo>();
            PropertyInfo[] vPI = pModel.GetType().GetProperties();

            foreach (PropertyInfo pi in vPI)
            {
                ProviderPropertyInfo providerPropertyInfo = new ProviderPropertyInfo();
                providerPropertyInfo.Name = pi.Name;
                providerPropertyInfo.Value = pi.GetValue(pModel, null);


                Type stype = null;
                if (pi.PropertyType.IsGenericType)
                {
                    stype = Nullable.GetUnderlyingType(pi.PropertyType);
                }
                else
                {
                    stype = pi.PropertyType;
                }
                providerPropertyInfo.SqlDbType = GetDBType(stype);

                columnvaluetypeList.Add(pi.Name, providerPropertyInfo);
            }
            return columnvaluetypeList;
        }
        private SqlDbType GetDBType(System.Type theType)
        {
            System.Data.SqlClient.SqlParameter p1 = default(System.Data.SqlClient.SqlParameter);
            System.ComponentModel.TypeConverter tc = default(System.ComponentModel.TypeConverter);
            p1 = new System.Data.SqlClient.SqlParameter();
            tc = System.ComponentModel.TypeDescriptor.GetConverter(p1.DbType);
            if (tc.CanConvertFrom(theType))
            {
                p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
            }
            else
            {
                //Try brute force
                try
                {
                    p1.DbType = (DbType)tc.ConvertFrom(theType.Name);
                }
                catch (Exception ex)
                {
                    //Do Nothing
                }
            }
            return p1.SqlDbType;
        }
    }
}
