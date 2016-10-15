using System;
using System.Collections;
using System.Data;
using System.Reflection;
//using ASL.DAL;
//using DATA;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Web;

namespace BaseModule
{
    public class StaticInfo
    {
        public const String LoadCritria = "LoadCritria";
        public const String Query = "SearchString";
        public const String QueryPage = "SearchString";
        public const String SearchCriteria = "SearchDictionary";
        public const String SearchSessionVarName = "ctl00_grdSearchGrid";
        public const String SearchColumnConfigSessionVarName = "SearchColumnConfig";
        public const String SearchArg = "SearchArg";
        public const String GlobalSearchType = "GlobalSearchType";
        public const String GridDateFormat = "MM/dd/yyyy";
        public static void SearchItem(IList List, String SearchTitle, String SearchFor, IList columnConfigList, int WWidth)
        {
            try
            {
                System.Web.HttpContext.Current.Session[GlobalSearchType] = null;
                System.Web.HttpContext.Current.Session[SearchColumnConfigSessionVarName] = columnConfigList;
                SearchItem(List, SearchTitle, SearchFor, String.Empty, false, String.Empty, WWidth, String.Empty);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static void SearchItem(IList List, String SearchTitle, String SearchFor, String HiddenColumns, bool MultipleSelect, String Selected_VID, int WWidth, String MustSelectedVids)
        {
            try
            {
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;

                System.Web.HttpContext.Current.Session[SearchSessionVarName] = List;
                String script = "javascript:LoadSearchGrid('" + SearchTitle + "','" + SearchFor + "','" + HiddenColumns + "'," + MultipleSelect.ToString().ToLower() + ",'" + Selected_VID + "'," + WWidth + ",'" + MustSelectedVids + "');";
                if (page != null && !page.ClientScript.IsClientScriptBlockRegistered("clientScript"))
                {
                    page.ClientScript.RegisterStartupScript(typeof(StaticInfo), "clientScript", script, true);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
