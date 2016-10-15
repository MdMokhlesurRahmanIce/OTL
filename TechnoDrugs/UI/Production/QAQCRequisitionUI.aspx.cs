using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BaseModule;
using SetupModule.Provider;
using SecurityModule.Provider;
using System.Data;
using TechnoDrugs.Helper;
using System.Globalization;
using ProductionModule.Provider;

namespace TechnoDrugs.UI.Production
{
    public partial class QAQCRequisitionUI : PageBase
    {
       #region properties
      //  private decimal totalAmount = 0;
        string statusMessage = string.Empty;
        string mode = "";
        public QAQCRequisitionUI()
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
                    var requisitionProvider = new QAQCRequisitionProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = requisitionProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                        gvPurchaseForLSE.DataSource = dt;
                        gvPurchaseForLSE.DataBind();                        
                    }
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;
                }
            }
            Session["Value"] = ddlRequisitionDivision.SelectedValue;
        }
        #endregion
                
        #region methods
        
        private void PopulateControls(DataTable dt)
        {
            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    txtRequisitionDate.Text = Convert.ToDateTime(dt.Rows[k]["RequisitionDate"]).ToString("dd/MM/yyyy");
                    txtRefNo.Text = dt.Rows[k]["ReferenceNo"].ToString();
                    ckbOption.SelectedValue = dt.Rows[k]["StatusID"].ToString();
                    if (ckbOption.SelectedValue.Toint() == 2)
                    {
                        gvPurchaseForLSE.Enabled = false;
                        btnAdd.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }            
            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var purchaseLedgerDetailsProviderList = new List<ProductionRequisitionDetailProvider> { new ProductionRequisitionDetailProvider() };           
        }
       
        private void Clear()
        {
            txtRequisitionDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;
            txtRefNo.Text = string.Empty;           
            txtRequisitionDate.Text = string.Empty;            
            ddlRequisitionDivision.SelectedIndex = 0;           
            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;          
            ddlRequisitionDivision.Enabled = true;          
            UC_ProductSearch1.Clear();
            gvPurchaseForLSE.DataSource = null;
            gvPurchaseForLSE.DataBind();
            btnAdd.Enabled = true;
        }
      
        private QAQCRequisitionProvider RequisitionInfoEntity()
        {
            QAQCRequisitionProvider entity = null;
            entity = new QAQCRequisitionProvider
            {
                TransactionNo = txtRefNo.Text,
                TransactionDate = DateTime.ParseExact(txtRequisitionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                RequisitionDate = DateTime.ParseExact(txtRequisitionDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DivisionID = int.Parse(ddlRequisitionDivision.SelectedValue),
                StatusID = ckbOption.SelectedValue.Toint(),
                EntryUserID = Convert.ToInt16( Session["ID"])
            };
            return entity;
        }
        private List<QAQCRequisitionDetailProvider> requisitionDetailEntityList()
        {
            List<QAQCRequisitionDetailProvider> requisitionDetailProviderList = new List<QAQCRequisitionDetailProvider>();

            foreach (GridViewRow row in gvPurchaseForLSE.Rows)
            {
                QAQCRequisitionDetailProvider obj = new QAQCRequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtRequiredQuantity = (TextBox)row.FindControl("txtRequiredQuantity");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                TextBox txtUsedForProduct = (TextBox)row.FindControl("txtUsedForProduct");

                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                Label lblProductName = (Label)row.FindControl("lblProduct");
                
                TextBox txtSentQuantity = (TextBox)row.FindControl("txtSentQuantity");
                
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.RequiredQuantity = txtRequiredQuantity.Text.ToDecimal();
                obj.SentQuantity = txtSentQuantity.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();
                obj.UsedForProduct = txtUsedForProduct.Text.ToString();
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
                    QAQCRequisitionProvider requisitionProvider = RequisitionInfoEntity();
                    List<QAQCRequisitionDetailProvider> requisitionDetailList = requisitionDetailEntityList();
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
                    QAQCRequisitionProvider requisitionProvider = RequisitionInfoEntity();
                    List<QAQCRequisitionDetailProvider> requisitionDetailList = requisitionDetailEntityList();
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

                string purchasesID = txtRefNo.Text.Trim();
                if (purchasesID == "")
                {
                    MessageHelper.ShowAlertMessage("Purchases ID Never Empty!");
                    return;
                }
                int reportOption = 3;
                int? vatRegistrationID = null;
                int? warehouseID = null;
                string productID = "";
                string fromDate = "";
                string todate = "";
                string adate = "";
                string transactionNo = txtRefNo.Text;
                int? reportCategory = 2;

                //string page = "../../Reports/ReportUI/GeneralReportViewerUI.aspx?ReportType=" + ReportType.PurchaseLedgerReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&productID=" + productID + "&fromDate=" + fromDate + "&todate=" + todate + "&Adate=" + adate + "&VatRegistrationID=" + vatRegistrationID + "&warehouseID=" + warehouseID + "&transactionNo=" + purchasesID;
                //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "'," + " null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //Clear();
            Response.Redirect("~/UI/Production/QAQCRequisitionUI.aspx");
            this.AlertNone(lblMsg);
        }        
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            ProductProvider productProvider = new ProductProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            if (ddlProductValidation.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                lblMsg.Focus();
                return;
            }
            string measurementUnit = productProvider.GetMeasurementUnit(ddlProductValidation.SelectedValue.Toint());
            List<QAQCRequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<QAQCRequisitionDetailProvider>();
            foreach (GridViewRow row in gvPurchaseForLSE.Rows)
            {
                QAQCRequisitionDetailProvider obj = new QAQCRequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequiredQuantity = (TextBox)row.FindControl("txtRequiredQuantity");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");

                TextBox txtSentQuantity = (TextBox)row.FindControl("txtSentQuantity");
                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");
                Label lblStockLocation = (Label)row.FindControl("lblStockLocation");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                if (hfRowProductID.Value == ddlProductValidation.SelectedValue)
                {
                    MessageHelper.ShowAlertMessage("This product already added!");
                    return;
                }
                if (txtRequiredQuantity.Text.ToDecimal() <= 0)
                {
                    MessageHelper.ShowAlertMessage("Enter Quantity!");
                    return;
                }
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.RawProductName = lblProductName.Text.ToString();
                obj.PresentStock = txtPresentStock.Text.ToDecimal();
                obj.StockLocation = lblStockLocation.Text;
                obj.MeasurementUnitName = txtUnit.Text.ToString();
                obj.RequiredQuantity = txtRequiredQuantity.Text.ToDecimal();
                obj.SentQuantity = txtSentQuantity.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();

                purchaseLedgerDetailsProviderList.Add(obj);
            }

            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();

            QAQCRequisitionDetailProvider obj2 = new QAQCRequisitionDetailProvider();
            obj2.ProductID = productID;
            obj2.RawProductName = productName;
            obj2.MeasurementUnitName = measurementUnit;
            obj2.PresentStock = productProvider.GetPresentStock(obj2.ProductID);
            obj2.StockLocation = productProvider.GetStockLocation(obj2.ProductID);
            purchaseLedgerDetailsProviderList.Add(obj2);
            if (!divGridForLSE.Visible)
            {
                divGridForLSE.Visible = true;
            }
            gvPurchaseForLSE.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseForLSE.DataBind();
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

            List<ProductionRequisitionDetailProvider> providerList = new List<ProductionRequisitionDetailProvider>();          

            #region new list
            ProductionRequisitionDetailProvider obj = new ProductionRequisitionDetailProvider();           
            
            providerList.Add(obj);
            #endregion        
        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<QAQCRequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<QAQCRequisitionDetailProvider>();
            foreach (GridViewRow row in gvPurchaseForLSE.Rows)
            {
                QAQCRequisitionDetailProvider obj = new QAQCRequisitionDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequiredQuantity = (TextBox)row.FindControl("txtRequiredQuantity");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtSentQuantity = (TextBox)row.FindControl("txtSentQuantity");
                Label lblStockLocation = (Label)row.FindControl("lblStockLocation");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.RawProductName = lblProductName.Text.ToString();
                    obj.RequiredQuantity = txtRequiredQuantity.Text.ToDecimal();
                    obj.MeasurementUnitName = txtUnit.Text;
                    obj.SentQuantity = txtSentQuantity.Text.ToDecimal();
                    obj.PresentStock = txtPresentStock.Text.ToDecimal();
                    obj.StockLocation = lblStockLocation.Text;
                    obj.Remarks = txtRemarks.Text.ToString();
                 
                    purchaseLedgerDetailsProviderList.Add(obj);
                }
            }
            gvPurchaseForLSE.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseForLSE.DataBind();

            if (gvPurchaseForLSE.Rows.Count == 0)
            {
                ddlRequisitionDivision.Enabled = true;
            }
        }        
        #endregion

       // #region grid event
        protected void gvPurchaseForLSE_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }

        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Price Declaration Code");
                //columns.Add("Name", "Name");
                //columns.Add("CardNo", "CardNo");
                //columns.Add("Address", "Address");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "Select ReferenceNo AS [Reference No], Convert(varchar(30),RequisitionDate ,105) AS [Requisition Date] from [dbo].QARequisition ORDER BY RequisitionDate DESC";
                //HttpContext.Current.Session[StaticInfo.Query] = "Select PriceDeclarationID + ' - ' +P.Name + ISNULL( '(' + PG.Name + ')' ,'') + ISNULL( ' - ' + PPS.SizeName, '') from [Vat].PriceDeclaration PD LEFT JOIN Vat.Product P ON P.ID = PD.ID LEFT JOIN Vat.ProductGrade PG ON PG.ID = PD.Grade LEFT JOIN Vat.ProductPackSize PPS ON PPS.ID = PD.SizeID Where PD.StatusID=0 OR PD.StatusID=1";
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

    }
}