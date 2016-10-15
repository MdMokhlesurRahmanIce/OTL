using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using SecurityModule.DataAccess;
using System.Data;
using System.Data.SqlClient;
using BaseModule;

namespace TechnoDrugs.UI.Purchase
{
    public partial class GeneralSearch : PageBase
    {
        private BaseDataAccess aAccess;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GridView1.DataSource = CustomersData();
                GridView1.DataBind();
                //GetDDLValue();
            }
        }        
        private DataTable FillATable(string query)
        {
            var aTable = new DataTable();
            if (query != null)
            {
                aAccess = new BaseDataAccess();
                var adapter = new SqlDataAdapter(query, aAccess.GetConnectionString());
                adapter.Fill(aTable);
            }
            return aTable;
        }

        private DataTable CustomersData()
        {
            var query = (string)HttpContext.Current.Session[StaticInfo.Query];
            return FillATable(query);
        }
        private DataTable CustomersDataWithSearchText(string searchText)
        {
            string query = (string)HttpContext.Current.Session[StaticInfo.Query] + " AND " + "PR.BatchNo" + " LIKE '%" + searchText + "%'" + " ORDER BY RequisitionDate DESC ";
            return FillATable(query);
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearch.Text))
            {
                //lblMessage.Text = "Enter " + ddlSearch.SelectedItem.Text + " to search";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                GridView1.DataSource = CustomersData();
                GridView1.DataBind();
            }
            else
            {
                lblMessage.Text = "";
                GridView1.DataSource = CustomersDataWithSearchText(txtSearch.Text);
                GridView1.DataBind();
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataSource = CustomersData();
            GridView1.DataBind();
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridView1.SelectedRow;
                if (row != null)
                {
                    string arguments = "";
                    for (int i = 1; i < GridView1.SelectedRow.Cells.Count; i++)
                    {
                        if (arguments == "")
                            arguments = GridView1.SelectedRow.Cells[i].Text;
                        else
                            arguments = arguments + ":" + GridView1.SelectedRow.Cells[i].Text;
                    }
                    string javaScript = string.Format("ReturnInformation('{0}');", arguments);
                    ScriptManager.RegisterStartupScript(this, GetType(), "SelectedIndexChanged", javaScript, true);
                }
            }
            catch (Exception ex)
            {
                throw (ex);                
            }

        }
    }
}