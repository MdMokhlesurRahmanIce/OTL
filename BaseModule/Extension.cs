using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;

namespace BaseModule
{
    public static class Extension
    {
        public static Boolean IsFalse(this Boolean val)
        {
            return !val;
        }
        public static Boolean IsZero(this int val)
        {
            return val.Equals(0);
        }
        public static Boolean NotEquals(this Object val, Object compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this String val, String compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Decimal val, Decimal compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Double val, Double compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Int32 val, Int32 compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Int16 val, Int16 compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Int64 val, Int64 compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this DateTime val, DateTime compVal)
        {
            return val != compVal;
        }
        public static Boolean NotEquals(this Boolean val, Boolean compVal)
        {
            return val != compVal;
        }
        public static Int32 GetInt32(this IDataRecord reader, String columnName)
        {
            return reader[columnName].Equals(DBNull.Value) ? 0 : (Int32)reader[columnName];
        }
        public static String GetString(this IDataRecord reader, String columnName)
        {
            return reader[columnName].ToString();
        }
        public static Boolean IsNull(this Object obj)
        {
            return obj == null;
        }
        public static string ToDateString(this DateTime dt)
        {
            return dt.ToString("dd MMM yyyy");
        }
        public static Boolean IsNullOrEmpty(this String str)
        {
            return String.IsNullOrEmpty(str);
        }
        public static Boolean IsNotNullOrEmpty(this String str)
        {
            return !String.IsNullOrEmpty(str);
        }
        public static Boolean IsZero(this Decimal val)
        {
            return val.Equals(Decimal.Zero);
        }
        public static Boolean IsNotZero(this int val)
        {
            return !val.Equals(0);
        }
        public static Boolean IsNotNull(this Object obj)
        {
            return obj != null;
        }
        public static String IfEmptyThenZero(this String str)
        {
            if (String.IsNullOrEmpty(str))
                return "0";
            return str;
        }
        public static string ToDateString(this DateTime? dt)
        {
            if (dt == null)
            {
                return string.Empty;
            }
            else
            {
                DateTime dt1 = (DateTime)dt;
                return dt1.ToString("dd MMM yyyy");
            }
        }
        public static decimal ToDecimal(this string d)
        {
            bool i = false;
            decimal number;
            if (d == string.Empty)
            {
                return 0;
            }
            else
            {
                i = decimal.TryParse(d, out number);
                if (i)
                {
                    return number;
                }
                else
                {
                    throw new Exception("The number is not valid decimal");
                }
            }
        }
        public static Double ToDouble(this string d)
        {
            bool i = false;
            Double number;
            if (d == string.Empty)
            {
                return 0;
            }
            else
            {
                i = Double.TryParse(d, out number);
                if (i)
                {
                    return number;
                }
                else
                {
                    throw new Exception("The number is not valid decimal");
                }
            }
        }
        public static Boolean ToBoolean(this String str)
        {
            if (str.Trim().Length > 0)
            {
                try
                {
                    return Boolean.Parse(str.Trim());
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public static int ToInt(this string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                throw new Exception("The  number is not valid integer");
            }
            int number;
            bool i = int.TryParse(d, out number);
            if (i)
            {
                return number;
            }
            throw new Exception("The number is not valid integer");
        }
        public static int Toint(this string d)
        {
            int number = int.TryParse(d, out number) ? number : number;
            return number;
        }
        public static int? ToNullableInt(this string d)
        {
            if (d == string.Empty)
            {
                return null;
            }
            else
            {
                int number;
                bool i = int.TryParse(d, out number);
                if (i)
                {
                    return number;
                }
                else
                {
                    return null;
                }
            }
        }
        public static int IfEmptyOrNullThenZero(this string d)
        {
            if (d == string.Empty)
            {
                return 0;
            }
            else
            {
                int number;
                bool i = int.TryParse(d, out number);
                if (i)
                {
                    return number;
                }
                else
                {
                    return 0;
                }
            }
        }
        public static string IfEmptyOrNullThenStringZero(this string d)
        {
            if (d == string.Empty)
            {
                return "0";
            }
            else
            {
                return d;
            }
        }
        public static DateTime ToDateTime(this string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                throw new Exception("Please insert valid datetime");
            }
            else
            {
                DateTime dt;
                bool i = DateTime.TryParse(d, out dt);
                if (i)
                {
                    return dt;
                }
                else
                {
                    throw new Exception("This is not valid datetime");
                }
            }
        }
        public static DateTime ToDate(this string d)
        {
            if (!string.IsNullOrEmpty(d))
            {
                return Convert.ToDateTime(d);
            }
            else
            {
                return Convert.ToDateTime(DateTime.Now);
            }
        }
        public static DateTime ToDateisNull(this string d)
        {
            if (!string.IsNullOrEmpty(d))
            {
                return Convert.ToDateTime(d);
            }
            else
            {
                return new DateTime();
            }
        }
        public static DateTime? ToNullableDateTime(this string d)
        {
            if (string.IsNullOrEmpty(d))
            {
                return null;
            }
            else
            {
                DateTime dt;
                bool i = DateTime.TryParse(d, out dt);
                if (i)
                {
                    return dt;
                }
                else
                {
                    throw new Exception("This is not valid datetime");
                }
            }
        }
    }
    public static class WebPageExtension
    {

        #region common method to impliment in all form

        public static void AlertSuccess(this System.Web.UI.Page page, HtmlGenericControl lblMsg, string msg)
        {
            lblMsg.InnerText = msg;
            lblMsg.Attributes.Remove("errorstyle");
            lblMsg.Attributes.Add("class", "successstyle");
        }
        public static void AlertWarning(this System.Web.UI.Page page, HtmlGenericControl lblMsg, string msg)
        {
            lblMsg.InnerText = msg;
            lblMsg.Attributes.Remove("errorstyle");
            lblMsg.Attributes.Remove("successstyle");
            lblMsg.Attributes.Add("class", "warningstyle");
        }
        public static void AlertError(this System.Web.UI.Page page, HtmlGenericControl lblMsg, string msg)
        {
            lblMsg.InnerText = msg;
            lblMsg.Attributes.Remove("successstyle");
            lblMsg.Attributes.Add("class", "errorstyle");
        }
        public static void AlertNone(this System.Web.UI.Page page, HtmlGenericControl lblMsg)
        {
            lblMsg.InnerText = string.Empty;
            lblMsg.Attributes.Remove("class");
            lblMsg.Attributes.Add("class", "validationbox");
        }
        #endregion
    }
    public class StringEqualityComparer : IEqualityComparer<String>
    {
        public bool Equals(String str1, String str2)
        {
            if (str1.Trim().ToLower().CompareTo(str2.Trim().ToLower()).Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int GetHashCode(String str)
        {
            return str.GetHashCode();
        }
    }
    public static class DataTableConversion
    {
        public static List<T> ToCollection<T>(this DataTable dt)
        {
            List<T> lst = new System.Collections.Generic.List<T>();
            Type tClass = typeof(T);
            PropertyInfo[] pClass = tClass.GetProperties();
            List<DataColumn> dc = dt.Columns.Cast<DataColumn>().ToList();
            T cn;
            foreach (DataRow item in dt.Rows)
            {
                cn = (T)Activator.CreateInstance(tClass);
                foreach (PropertyInfo pc in pClass)
                {
                    // Can comment try catch block. 
                    try
                    {
                        DataColumn d = dc.Find(c => c.ColumnName == pc.Name);
                        if (d != null)
                            pc.SetValue(cn, item[pc.Name], null);
                    }
                    catch
                    {
                    }
                }
                lst.Add(cn);
            }
            return lst;
        }
    }
}
