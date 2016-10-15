using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BaseModule
{
    public class MenuHelper
    {
        public static string GetHtml(DataTable menuTable, string menuIdColumn, string parentIdColumn, string textColumn, string urlColumn, string cssClass)
        {
            // add css class to menu
            #region Fields
            string root = ConfigurationManager.AppSettings["urlRoot"];
            #endregion
            string html = @"<ul class=""" + cssClass + @""">";

            // get all parent menu items
            DataRow[] menuParents = menuTable.Select("[" + parentIdColumn + "]='0'");
            
            foreach (DataRow parent in menuParents)
            {
                html += "<li>";

                // get parent menu id
                string menuId = parent[menuIdColumn].ToString();

                // get available child menu items(if any)
                DataRow[] menuChilds = menuTable.Select("[" + parentIdColumn + "]='" + menuId + "'");
                var href = parent[urlColumn].ToString() == "" ? "#" : parent[urlColumn].ToString();
                // generate sub menu if child menu items exists
                if (menuChilds.Length > 0)
                {
                    html += @"<a href=""" + href + @""">" + parent[textColumn].ToString() + "</a>";

                    html += "<ul>";

                    foreach (DataRow child in menuChilds)
                    {
                        href = child[urlColumn].ToString() == "" ? "#" : child[urlColumn].ToString();
                        html += "<li>";
                        html += @"<a href=""" + root + href + @""">" + child[textColumn].ToString() + "</a>";
                        html += "</li>";
                    }
                    html += "</ul>";
                }
                // render parent menu item
                else
                {
                    html += @"<a href=""" + href + @""">" + parent[textColumn].ToString() + "</a>";
                }

                html += "</li>";
            }
            html += "</ul>";
            return html;
        }
    }
}
