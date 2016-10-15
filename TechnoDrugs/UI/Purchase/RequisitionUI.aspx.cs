using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PurchaseModule.Provider;
using BaseModule;
using SetupModule.Provider;
using SecurityModule.Provider;
using System.Data;
using TechnoDrugs.Helper;
using System.Globalization;
using TechnoDrugs.Reports.ReportEntity;


namespace TechnoDrugs.UI.Purchase
{
    public partial class RequisitionUI : PageBase
    {
        #region properties
        string statusMessage = string.Empty;
        string mode = "";
        public RequisitionUI()
        {
            RequiresAuthorization = true;
        }
        #endregion
        
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AddBlankRowTogvPurchaseProduct();
                btnSave.Enabled = true;
                txtRequisitionDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var requisitionProvider = new RequisitionProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = requisitionProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        try
                        {
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                txtRequisitionDate.Text = Convert.ToDateTime(dt.Rows[k][3]).ToString("dd/MM/yyyy");
                                txtRefNo.Text = dt.Rows[k][2].ToString();
                                ddlRequisitionDivision.SelectedValue = dt.Rows[k][1].ToString();
                                ckbOption.SelectedValue = dt.Rows[k]["StatusID"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw (ex);
                        }
                    }                    
                    gvRequisition.DataSource = dt;
                    gvRequisition.DataBind();                        
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            //string user = Session["UserID"];
            if (ckbOption.SelectedValue.Toint() == 3 && Session[SessionConstants.IsAdmin].ToString() == "False")
            {
                txtRefNo.Enabled = false;
                txtRequisitionDate.Enabled = false;
                ddlRequisitionDivision.Enabled = false;
                gvRequisition.Enabled = false;
                btnAdd.Enabled = false;
                btnUpdate.Enabled = false;
            }
            Session["Value"] = ddlRequisitionDivision.SelectedValue;
        }
        #endregion
               
        #region methods
        private void RowsIn_gvPurchaseForLSE()
        {
            List<RequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<RequisitionDetailProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                RequisitionDetailProvider obj = new RequisitionDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantityLSE");              
                TextBox txtSDAmount = (TextBox)row.FindControl("txtSDAmountLSE");
                Label lblSDRate = (Label)row.FindControl("lblSDRate");
                TextBox txtVatLeviable = (TextBox)row.FindControl("txtVATLeviableLSE");
                Label lblVATRate = (Label)row.FindControl("lblVATRate");
                TextBox txtVatAmount = (TextBox)row.FindControl("txtVATAmountLSE");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.RequiredQuantity = txtQuantity.Text.ToDecimal();           
                                
                purchaseLedgerDetailsProviderList.Add(obj);
            }
            gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
            gvRequisition.DataBind();           
        }

        public void AddBlankRowTogvPurchaseProduct()
        {
            var purchaseLedgerDetailsProviderList = new List<RequisitionDetailProvider> { new RequisitionDetailProvider() };           
        }
       
        private void Clear()
        {
            txtRequisitionDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;      
            txtRequisitionDate.Text = string.Empty;            
            ddlRequisitionDivision.SelectedIndex = 0;           
            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;          
            ddlRequisitionDivision.Enabled = true;          
            UC_ProductSearch1.Clear();
            gvRequisition.DataSource = null;
            gvRequisition.DataBind();
            btnAdd.Enabled = true;
        }
      
        private RequisitionProvider RequisitionInfoEntity()
        {
            RequisitionProvider entity = null;
            entity = new RequisitionProvider
            {
                ReferenceNo = txtRefNo.Text,
                TransactionDate = DateTime.ParseExact(txtRequisitionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                RequisitionDate = DateTime.ParseExact(txtRequisitionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), 
                DivisionID = int.Parse(ddlRequisitionDivision.SelectedValue),
                StatusID = ckbOption.SelectedValue.Toint(),
                EntryUserID = Convert.ToInt16(Session["ID"]),
                UpdateUserID = Convert.ToInt16(Session["ID"])
            };
            if (entity.StatusID == 2)
                entity.CheckUserID = Convert.ToInt16(Session["ID"]);
            if (entity.StatusID == 3)
                entity.ApproveUserID = Convert.ToInt16(Session["ID"]);
            return entity;
        }
        private List<RequisitionDetailProvider> requisitionDetailEntityList()
        {
            List<RequisitionDetailProvider> requisitionDetailProviderList = new List<RequisitionDetailProvider>();

            foreach (GridViewRow row in gvRequisition.Rows)
            {
                RequisitionDetailProvider obj = new RequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtQuantity = (TextBox)row.FindControl("txtRequirement");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequirement = (TextBox)row.FindControl("txtRequirement");

                TextBox txtMonthlyConsume = (TextBox)row.FindControl("txtMonthlyConsume");
                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.RequiredQuantity = txtQuantity.Text.ToDecimal();
                obj.MonthlyConsumeQty = txtMonthlyConsume.Text.ToDecimal();
                obj.PresentStock = txtPresentStock.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();

                if (obj.RequiredQuantity <= 0)
                    throw new Exception("Please input required quantity");

                requisitionDetailProviderList.Add(obj);
            }
            return requisitionDetailProviderList;
        }      
       
        #endregion

        #region button event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            mode = "Save";
            
            string permisionMessage= CheckUserAuthentication(mode);
            if (ckbOption.SelectedValue != "")
            {
                statusMessage = CheckUserAuthentication(ckbOption.SelectedItem.ToString());
            }            

            if(String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                bool msg = false;
                string message = string.Empty;
                string transactionNo = string.Empty;            
                try
                {
                    RequisitionProvider requisitionProvider = RequisitionInfoEntity();
                    List<RequisitionDetailProvider> requisitionDetailList = requisitionDetailEntityList();
                    if ((requisitionDetailList == null) || (requisitionDetailList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                        return;
                    }
                    msg = requisitionProvider.Save(requisitionDetailList, out transactionNo);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                if (msg)
                {
                    Clear();
                    txtRefNo.Text = transactionNo;
                    this.AlertSuccess(lblMsg, MessageConstants.Saved);
                }
                else
                {
                    MessageHelper.ShowAlertMessage(message);
                }
            }
            else
            {
               MessageHelper.ShowAlertMessage(permisionMessage + statusMessage);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            mode = "Update";
            bool msg = false;
            string message = string.Empty;
            string permisionMessage= CheckUserAuthentication(mode);

            if (ckbOption.SelectedValue != "")
            {
                statusMessage = CheckUserAuthentication(ckbOption.SelectedItem.ToString());
            }

            if (String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                try
                {
                    RequisitionProvider requisitionProvider = RequisitionInfoEntity();
                    List<RequisitionDetailProvider> requisitionDetailList = requisitionDetailEntityList();
                    if ((requisitionDetailList == null) || (requisitionDetailList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                        return;
                    }
                    msg = requisitionProvider.Update(requisitionDetailList);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                if (msg)
                {
                    Clear();
                    this.AlertSuccess(lblMsg, MessageConstants.Updated);
                }
                else
                {
                    MessageHelper.ShowAlertMessage(message);
                }
            }
            else
            {
                MessageHelper.ShowAlertMessage(permisionMessage + statusMessage);
            }        
        }
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                this.AlertNone(lblMsg);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                mode = "Preview";
                CheckUserAuthentication(mode);

                string requisitionID = txtRefNo.Text.Trim();
                if (requisitionID == "")
                {
                    MessageHelper.ShowAlertMessage("Requisition ID Never Empty!");
                    return;
                }
                int reportOption = 3;
                string productID = "";
                string fromDate = "";
                string todate = "";
                string adate = "";
                string transactionNo = txtRefNo.Text;
                int? reportCategory = 2;
                string page = "../../Reports/ReportUI/GeneralReportViewerUI.aspx?ReportType=" + ReportType.RequisitionReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&productID=" + productID + "&fromDate=" + fromDate + "&todate=" + todate + "&adate=" + adate + "&transactionNo=" + transactionNo;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Purchase/RequisitionUI.aspx");
            this.AlertNone(lblMsg);
        }        
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            ProductProvider productProvider = new ProductProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string measurementUnit = productProvider.GetMeasurementUnit(ddlProductValidation.SelectedValue.Toint());

            if (ddlProductValidation.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                lblMsg.Focus();
                return;
            }
            List<RequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<RequisitionDetailProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                RequisitionDetailProvider obj = new RequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequirement = (TextBox)row.FindControl("txtRequirement");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                TextBox txtMonthlyConsume = (TextBox)row.FindControl("txtMonthlyConsume");
                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                if (hfRowProductID.Value == ddlProductValidation.SelectedValue)
                {
                    MessageHelper.ShowAlertMessage("This product already exists!");
                    return;
                }
                if (txtRequirement.Text.ToDecimal() <= 0)
                {
                    MessageHelper.ShowAlertMessage("Enter Quantity!");
                    return;
                }
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.RequiredQuantity = txtRequirement.Text.ToDecimal();
                obj.MeasurementUnitName = txtUnit.Text.ToString();
                obj.MonthlyConsumeQty = txtMonthlyConsume.Text.ToDecimal();
                obj.PresentStock = txtPresentStock.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();

                purchaseLedgerDetailsProviderList.Add(obj);
            }
            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();

            RequisitionDetailProvider obj2 = new RequisitionDetailProvider();
            obj2.ProductID = productID;
            obj2.ProductName = productName;
            obj2.MeasurementUnitName = measurementUnit;
            obj2.PresentStock = productProvider.GetPresentStock(obj2.ProductID);
            purchaseLedgerDetailsProviderList.Add(obj2);

            if (!divGridForLSE.Visible)
            {
                divGridForLSE.Visible = true;
            }
            gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
            gvRequisition.DataBind();
            ddlRequisitionDivision.Enabled = false;
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            rbProductType.Enabled = false;
        }
        protected void btnAddRow_Click(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            if (ddlProductValidation.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                return;
            }
            string productName = ddlProductValidation.SelectedItem.Text;
            int productID = ddlProductValidation.SelectedValue.Toint();         

            List<RequisitionDetailProvider> providerList = new List<RequisitionDetailProvider>();           

            #region new list
            RequisitionDetailProvider obj = new RequisitionDetailProvider();           
            
            providerList.Add(obj);
            #endregion        
        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<RequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<RequisitionDetailProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                RequisitionDetailProvider obj = new RequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequirement = (TextBox)row.FindControl("txtRequirement");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtMonthlyConsume = (TextBox)row.FindControl("txtMonthlyConsume");
                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.RequiredQuantity = txtRequirement.Text.ToDecimal();

                    obj.MonthlyConsumeQty = txtMonthlyConsume.Text.ToDecimal();
                    obj.PresentStock = txtPresentStock.Text.ToDecimal();
                    obj.Remarks = txtRemarks.Text.ToString();
                 
                    purchaseLedgerDetailsProviderList.Add(obj);
                }
            }
            gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
            gvRequisition.DataBind();

            if (gvRequisition.Rows.Count == 0)
            {
                ddlRequisitionDivision.Enabled = true;
            }
        }
        #endregion

        //#region grid event
        protected void gvRequisition_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {            
            try
            {
                int divisionID = ddlRequisitionDivision.SelectedItem.Value.Toint();
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Price Declaration Code");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "SELECT ReferenceNo AS [Reference No], CONVERT(VARCHAR(30),RequisitionDate ,105) AS [Requisition Date] FROM [dbo].Requisition R WHERE R.DivisionID = " + divisionID + " ORDER BY RequisitionDate DESC, ReferenceNo DESC";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }
        protected void ddlRequisitionDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            Session["Value"] = ddlRequisitionDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = Convert.ToInt16(ddlRequisitionDivision.SelectedValue);
            LoadProduct(codeOrName, productType, divisionID);
        }
        private void LoadProduct(int codeName, int productType, int divisionID)
        {
            try
            {
                DataTable dt = new DataTable();
                dt = new ProductProvider().GetProductForProductSearch(codeName, productType, divisionID);

                AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");

                ddlProduct.DataSource = null;
                ddlProduct.DataBind();

                ddlProduct.DataSource = dt;
                ddlProduct.DataTextField = "ProductName";
                ddlProduct.DataValueField = "ID";
                ddlProduct.DataBind();
                ddlProduct.Items.Insert(0, new ListItem("----------Select Product----------", "0"));
                ddlProduct.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }
        }
        protected void btnCommonProducts_Click(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
           // Session["Value"] = ddlRequisitionDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = 30;
            LoadProduct(codeOrName, productType, divisionID);
        }        
    }
}