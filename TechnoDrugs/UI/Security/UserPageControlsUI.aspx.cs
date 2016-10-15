using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using System.Data;
using TechnoDrugs.Helper;
using BaseModule;
using System.Data.SqlClient;

namespace TechnoDrugs.UI.Security
{
    public partial class UserPageControlsUI : PageBase
    {
        string mode = "";
        public UserPageControlsUI()
        {
            RequiresAuthorization = true;
        }
        private PageControlsProvider aProviders;
        private DataTable aTable;
        protected void Page_Load(object sender, EventArgs e)
        {
            aProviders = new PageControlsProvider();
            if (IsPostBack)
            {
                var eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                string roleCode = Request["__EVENTARGUMENT"];
                //hfRoleCode.Value = roleCode;
                if (eventTarget == "Search" && roleCode != null)
                {
                    aProviders = new PageControlsProvider();
                    GridView1.DataSource = null;
                    aTable = aProviders.GetAllbyRoleCode(roleCode);
                    if (aTable.Rows.Count > 0)
                    {
                        GridView1.DataSource = aTable;
                        GridView1.DataBind();
                        Session["pageControlsTable"] = aTable;
                        btnSubmit.Text = "Update";
                    }
                    else
                    {
                        //this.AlertWarning(lblMsg, MessageConstants.DataNotFind);
                        MessageHelper.ShowAlertMessage(MessageConstants.DataNotFind);
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        btnSubmit.Text = "Submit";
                    }
                    ddlMenuType.SelectedValue = "0";
                }
            }
            else
            {
                PopulateddlMenuType();
                GridView1.DataSource = null;
                Session["pageControlsTable"] = string.Empty;
                aTable = aProviders.GetAllPageControls();
                GridView1.DataSource = aTable;
                GridView1.DataBind();
                Session["pageControlsTable"] = aTable;
            }
        }

        private SecurityModule.Provider.RoleProvider GenerateRoleMaster()
        {
            SecurityModule.Provider.RoleProvider provider = new SecurityModule.Provider.RoleProvider();
            try
            {
                provider.RoleCode = hfRoleCode.Value.IsNullOrEmpty() ? 0 : hfRoleCode.Value.ToInt();
                provider.RoleName = roleNameTextBox.Text.ToString();
                provider.RoleDescription = descriptionTextBox.Text.ToString();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            return provider;
        }
        private List<PageControlsProvider> PageControlsProviders()
        {
            List<PageControlsProvider> roleDetailList = new List<PageControlsProvider>();
            //bool isSelected = false;
            try
            {
                #region by pre dev
                var alist = new List<PageControlsProvider>();
                GetwithCaption();
                DataTable aTable = (DataTable)(Session["pageControlsTable"]);
                foreach (DataRow aRow in aTable.Rows)
                {
                    aProviders = new PageControlsProvider
                    {
                        CanSelect = Convert.ToBoolean(aRow[3].ToString()),
                        CanInsert = Convert.ToBoolean(aRow[4].ToString()),
                        CanUpdate = Convert.ToBoolean(aRow[5].ToString()),
                        CanDelete = Convert.ToBoolean(aRow[6].ToString()),
                        CanSend = Convert.ToBoolean(aRow[7].ToString()),
                        CanCheck = Convert.ToBoolean(aRow[8].ToString()),
                        CanApprove = Convert.ToBoolean(aRow[9].ToString()),
                        CanPreview = Convert.ToBoolean(aRow[10].ToString()),
                        CanReceive = Convert.ToBoolean(aRow[11].ToString()),
                        //AllChk = Convert.ToBoolean(aRow[8].ToString()),
                        MenuId = aRow[0].ToString()
                    };
                    alist.Add(aProviders);
                }
                return alist;
                #endregion
            }
            catch
            {
                throw;
            }
            //return roleDetailList;
        }

        private void PopulateddlMenuType()
        {
            SecurityModule.Provider.RoleProvider provider = new SecurityModule.Provider.RoleProvider();
            try
            {
                ddlMenuType.DataSource = provider.GetAllMenu();
                ddlMenuType.DataTextField = "Caption";
                ddlMenuType.DataValueField = "ID";
                ddlMenuType.DataBind();
                ddlMenuType.Items.Insert(0, new ListItem(string.Empty, "0"));
                ddlMenuType.SelectedIndex = 0;
            }
            catch (SqlException ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                var page = HttpContext.Current.CurrentHandler as Page;
                var columns = new Dictionary<string, string>
                                  {
                                      //{"Caption", "Caption"},
                                      {"RoleName", "RoleName"}
                                  };
                HttpContext.Current.Session[StaticInfo.LoadCritria] = "PageConFind";
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.QueryPage] = "SELECT * FROM [UserAccess].[Role]";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (IsPostBack && roleNameTextBox.Text != string.Empty)
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        //Find the check boxes and assign the values from the data source
                        ((CheckBox)e.Row.FindControl("chkSelect")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[3]);
                        ((CheckBox)e.Row.FindControl("chkAdd")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[4]);
                        ((CheckBox)e.Row.FindControl("chkEdit")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[5]);
                        ((CheckBox)e.Row.FindControl("chkDelate")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[6]);
                        ((CheckBox)e.Row.FindControl("chkSend")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[7]);
                        ((CheckBox)e.Row.FindControl("chkCheck")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[8]);
                        ((CheckBox)e.Row.FindControl("chkApprove")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[9]);
                        ((CheckBox)e.Row.FindControl("chkPreview")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[10]);
                        ((CheckBox)e.Row.FindControl("chkReceive")).Checked = Convert.ToBoolean(((DataRowView)e.Row.DataItem)[11]);
                        //((CheckBox)e.Row.FindControl("chkAll")).Checked =
                        //    Convert.ToBoolean(((DataRowView)e.Row.DataItem)[8]);

                        //Find the checkboxes and assign the javascript function which should
                        //be called when the user clicks the checkboxes.
                        ((CheckBox)e.Row.FindControl("chkSelect")).Attributes.Add("onclick", "checkBoxClicked('" +
                                                                                   e.Row.FindControl("chkSelect").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkAdd").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkEdit")
                                                                                       .ClientID + "','" +
                                                                                   e.Row.FindControl("chkDelate").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkPreview").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkReceive").ClientID + "'," + "'SELECT')");
                        //    .ClientID + "','" +
                        //e.Row.FindControl("chkAll").
                        //    ClientID + "'," + "'SELECT')");
                        ((CheckBox)e.Row.FindControl("chkAdd")).Attributes.Add("onclick",
                                                                                "checkBoxClicked('" +
                                                                                e.Row.FindControl("chkSelect").
                                                                                    ClientID + "','" +
                                                                                e.Row.FindControl("chkAdd").
                                                                                    ClientID + "','" +
                                                                                e.Row.FindControl("chkEdit").
                                                                                    ClientID + "','" +
                                                                                e.Row.FindControl("chkDelate").
                                                                                    ClientID + "','" +
                                                                                      e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                e.Row.FindControl("chkPreview")
                                                                                    .ClientID + "','" +
                                                                                e.Row.FindControl("chkReceive").ClientID + "'," + "'ADD')");
                        //    ClientID + "','" +
                        //e.Row.FindControl("chkAll").
                        //    ClientID + "'," + "'ADD')");
                        ((CheckBox)e.Row.FindControl("chkEdit")).Attributes.Add("onclick",
                                                                                 "checkBoxClicked('" +
                                                                                 e.Row.FindControl("chkSelect")
                                                                                     .ClientID + "','" +
                                                                                 e.Row.FindControl("chkAdd").
                                                                                     ClientID + "','" +
                                                                                 e.Row.FindControl("chkEdit").
                                                                                     ClientID + "','" +
                                                                                 e.Row.FindControl("chkDelate")
                                                                                     .ClientID + "','" +
                                                                                       e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                 e.Row.FindControl("chkPreview")
                                                                                     .ClientID + "','" +
                                                                                 e.Row.FindControl("chkReceive").ClientID + "'," + "'EDIT')");
                        //    ClientID + "','" +
                        //e.Row.FindControl("chkAll").
                        //    ClientID + "'," + "'EDIT')");
                        ((CheckBox)e.Row.FindControl("chkDelate")).Attributes.Add("onclick",
                                                                                   "checkBoxClicked('" +
                                                                                   e.Row.FindControl("chkSelect").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkAdd").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkEdit")
                                                                                       .ClientID + "','" +
                                                                                   e.Row.FindControl("chkDelate").
                                                                                       ClientID + "','" +
                                                                                         e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkPreview").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkReceive").ClientID + "'," + "'DELATE')");


                        ((CheckBox)e.Row.FindControl("chkSend")).Attributes.Add("onclick", "checkBoxClicked('" +
                                                                                   e.Row.FindControl("chkSelect").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkAdd").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkEdit")
                                                                                       .ClientID + "','" +
                                                                                   e.Row.FindControl("chkDelate").
                                                                                       ClientID + "','" +
                                                                                         e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkPreview").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkReceive").ClientID + "'," + "'SEND')");

                        ((CheckBox)e.Row.FindControl("chkCheck")).Attributes.Add("onclick", "checkBoxClicked('" +
                                                                                   e.Row.FindControl("chkSelect").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkAdd").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkEdit")
                                                                                       .ClientID + "','" +
                                                                                   e.Row.FindControl("chkDelate").
                                                                                       ClientID + "','" +
                                                                                         e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkPreview").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkReceive").ClientID + "'," + "'CHECK')");

                        ((CheckBox)e.Row.FindControl("chkApprove")).Attributes.Add("onclick", "checkBoxClicked('" +
                                                                                   e.Row.FindControl("chkSelect").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkAdd").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkEdit")
                                                                                       .ClientID + "','" +
                                                                                   e.Row.FindControl("chkDelate").
                                                                                       ClientID + "','" +
                                                                                         e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkPreview").
                                                                                       ClientID + "','" +
                                                                                   e.Row.FindControl("chkReceive").ClientID + "'," + "'APPROVE')");
                        //    .ClientID + "','" +
                        //e.Row.FindControl("chkAll").
                        //    ClientID + "'," + "'DELATE')");
                        ((CheckBox)e.Row.FindControl("chkPreview")).Attributes.Add("onclick",
                                                                                    "checkBoxClicked('" +
                                                                                    e.Row.FindControl("chkSelect").
                                                                                        ClientID + "','" +
                                                                                    e.Row.FindControl("chkAdd")
                                                                                        .
                                                                                        ClientID + "','" +
                                                                                    e.Row.FindControl("chkEdit")
                                                                                        .
                                                                                        ClientID + "','" +
                                                                                    e.Row.FindControl("chkDelate").
                                                                                        ClientID + "','" +
                                                                                          e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                    e.Row.FindControl("chkPreview")
                                                                                        .
                                                                                        ClientID + "','" +
                                                                                    e.Row.FindControl("chkReceive").
                            //    ClientID + "','" +
                            //e.Row.FindControl("chkAll")
                            //    .
                                                                                        ClientID + "'," + "'PREVIEW')");
                        ((CheckBox)e.Row.FindControl("chkReceive")).Attributes.Add("onclick",
                                                                                  "checkBoxClicked('" +
                                                                                  e.Row.FindControl("chkSelect")
                                                                                      .ClientID + "','" +
                                                                                  e.Row.FindControl("chkAdd").
                                                                                      ClientID + "','" +
                                                                                  e.Row.FindControl("chkEdit").
                                                                                      ClientID + "','" +
                                                                                  e.Row.FindControl("chkDelate")
                                                                                      .ClientID + "','" +
                                                                                        e.Row.FindControl("chkSend").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkCheck").
                                                                                       ClientID + "','" +
                                                                                        e.Row.FindControl("chkApprove").
                                                                                       ClientID + "','" +
                                                                                  e.Row.FindControl("chkPreview").
                                                                                      ClientID + "','" +
                                                                                  e.Row.FindControl("chkReceive").ClientID + "'," + "'RECEIVE')");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }

        }

        protected void ddlMenuType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SecurityModule.Provider.RoleProvider provider = new SecurityModule.Provider.RoleProvider();
            try
            {
                GetwithCaption();
                DataTable dt = new DataTable();
                dt = (DataTable)HttpContext.Current.Session["pageControlsTable"];
                DataTable filterSetDT = new DataTable();
                DataTable exFiltersetDT = new DataTable();

                string filterExpression = "ParentID=" + ddlMenuType.SelectedValue;
                DataRow[] dr;
                dr = dt.Select(filterExpression);
                if (dr.Length == 0)
                {
                    return;
                }
                filterSetDT = dr.CopyToDataTable();

                string filterExpression2 = "ParentID<>" + ddlMenuType.SelectedValue;
                dr = dt.Select(filterExpression2);
                exFiltersetDT = dr.CopyToDataTable();
                filterSetDT.Merge(exFiltersetDT);

                dt.Clear();
                dt = filterSetDT;
                //GridView1.DataSource = null;
                //GridView1.DataBind();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["pageControlsTable"] = String.Empty;
                Session["pageControlsTable"] = dt;
            }
            catch (SqlException ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

        protected void chkEditAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkEditAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkEdit");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkInsertAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkInsertAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkAdd");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkSendAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkSendAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkSend");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkCheckAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkCheckAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkCheck");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkApproveAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkApproveAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkApprove");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkSelectAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkSelect");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkDelateAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkDelateAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkDelate");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkReceiveAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkReceiveAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkReceive");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void chkPreviewAll_CheckedChanged(object sender, EventArgs e)
        {
            var chkEdit = GridView1.HeaderRow.FindControl("chkPreviewAll") as CheckBox;
            foreach (GridViewRow row in GridView1.Rows)
            {
                var chkSelect = (CheckBox)row.FindControl("chkPreview");
                chkSelect.Checked = chkEdit.Checked;
                GetwithCaption();
            }
        }
        protected void newButton_Click(object sender, EventArgs e)
        {
            Clear();
            HttpContext.Current.Session["pageControlsTable"] = string.Empty;
            HttpContext.Current.Session["pageControlsTable"] = aProviders.GetAllPageControls();
            GridView1.DataSource = HttpContext.Current.Session["pageControlsTable"];
            GridView1.DataBind();
        }
        protected void Clear()
        {
            // this.AlertNone(lblMsg);
            ddlMenuType.SelectedValue = "0";
            GridView1.DataSource = null;
            GridView1.DataBind();
            roleNameTextBox.Text = string.Empty;
            descriptionTextBox.Text = string.Empty;
            btnSubmit.Text = "Submit";
            lblMsg.InnerText = string.Empty;
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GetwithCaption();
            GridView1.PageIndex = e.NewPageIndex;
            DataTable dt = (DataTable)HttpContext.Current.Session["pageControlsTable"];
            //GridView1.DataSource = null;
            //GridView1.DataBind();
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        private void GetwithCaption()
        {
            try
            {
                aTable = new DataTable();
                DataTable aTableObj = (DataTable)(HttpContext.Current.Session["pageControlsTable"]);
                foreach (GridViewRow aRow in GridView1.Rows)
                {
                    aProviders = new PageControlsProvider
                    {
                        CanSelect = ((CheckBox)aRow.FindControl("chkSelect")).Checked,
                        CanInsert = ((CheckBox)aRow.FindControl("chkAdd")).Checked,
                        CanUpdate = ((CheckBox)aRow.FindControl("chkEdit")).Checked,
                        CanDelete = ((CheckBox)aRow.FindControl("chkDelate")).Checked,
                        CanSend = ((CheckBox)aRow.FindControl("chkSend")).Checked,
                        CanCheck = ((CheckBox)aRow.FindControl("chkCheck")).Checked,
                        CanApprove = ((CheckBox)aRow.FindControl("chkApprove")).Checked,
                        CanPreview = ((CheckBox)aRow.FindControl("chkPreview")).Checked,
                        CanReceive = ((CheckBox)aRow.FindControl("chkReceive")).Checked,
                        MenuId = aRow.Cells[0].Text
                    };
                    foreach (DataRow aRowobj in aTableObj.Rows.Cast<DataRow>().Where(aRowobj => aRowobj[0].ToString() == aProviders.MenuId))
                    {
                        aRowobj[3] = aProviders.CanSelect;
                        aRowobj[4] = aProviders.CanInsert;
                        aRowobj[5] = aProviders.CanUpdate;
                        aRowobj[6] = aProviders.CanDelete;
                        aRowobj[7] = aProviders.CanSend;
                        aRowobj[8] = aProviders.CanCheck;
                        aRowobj[9] = aProviders.CanApprove;
                        aRowobj[10] = aProviders.CanPreview;
                        aRowobj[11] = aProviders.CanReceive;
                    }
                }
                HttpContext.Current.Session["pageControlsTable"] = aTableObj;
            }
            catch
            {
                throw;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SecurityModule.Provider.RoleProvider roleMasterList = GenerateRoleMaster();
                List<PageControlsProvider> roleDetailsList = PageControlsProviders();

                //aProviders = new PageControlsProvider();
                string text = btnSubmit.Text;
                string roleName = roleNameTextBox.Text;
                //var aList = PageControlsProviders();

                if (roleName != "" && text == "Submit")
                {
                    if (roleDetailsList.Count == 0)
                    {
                        throw new Exception("No item is selected.");
                    }

                    roleMasterList.Save(roleDetailsList);
                    //aProviders.SavePageControls(aList, text);
                    Clear();
                    //this.AlertSuccess(lblMsg, MessageConstants.Saved);
                    MessageHelper.ShowAlertMessage(MessageConstants.Saved);
                }
                else if (roleName != "" && text == "Update")
                {
                    roleMasterList.Update(roleDetailsList);
                    //aProviders.UpdatePageControls(aList, text);
                    Clear();
                    // this.AlertSuccess(lblMsg, MessageConstants.Updated);
                    MessageHelper.ShowAlertMessage(MessageConstants.Updated);
                }
                else
                {
                    // this.AlertWarning(lblMsg, MessageConstants.SavedWarning);
                    MessageHelper.ShowAlertMessage(MessageConstants.SavedWarning);
                }
            }
            catch (SqlException ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.Message);
            }
        }

    }
}