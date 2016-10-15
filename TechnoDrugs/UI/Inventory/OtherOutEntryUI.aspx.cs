using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryModule.Provider;
using BaseModule;
using SetupModule.Provider;
using SecurityModule.Provider;
using System.Data;
using TechnoDrugs.Helper;
using System.Globalization;
using TechnoDrugs.Reports.ReportEntity;

namespace TechnoDrugs.UI.Inventory
{
    public partial class OtherOutEntryUI : PageBase
    {
        #region properties
        string statusMessage = string.Empty;
        string mode = "";
        public OtherOutEntryUI()
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
                txtEntryDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var requisitionProvider = new OtherOutProductProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = requisitionProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        try
                        {
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                txtEntryDate.Text = Convert.ToDateTime(dt.Rows[k][3]).ToString("dd/MM/yyyy");
                                txtRefNo.Text = dt.Rows[k][2].ToString();
                                ddlProductDivision.SelectedValue = dt.Rows[k][1].ToString();
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
            //if (ckbOption.SelectedValue.Toint() == 3)
            //{
            //    txtRefNo.Enabled = false;
            //    txtRequisitionDate.Enabled = false;
            //    ddlRequisitionDivision.Enabled = false;
            //    gvRequisition.Enabled = false;
            //    btnAdd.Enabled = false;
            //    btnUpdate.Enabled = false;
            //}
            Session["Value"] = ddlProductDivision.SelectedValue;
        }
        #endregion
               
        #region methods
        //private void RowsIn_gvPurchaseForLSE()
        //{
        //    List<RequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<RequisitionDetailProvider>();
        //    foreach (GridViewRow row in gvRequisition.Rows)
        //    {
        //        RequisitionDetailProvider obj = new RequisitionDetailProvider();
        //        HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
        //        Label lblProductName = (Label)row.FindControl("lblProduct");
        //        TextBox txtQuantity = (TextBox)row.FindControl("txtQuantityLSE");              
        //        TextBox txtSDAmount = (TextBox)row.FindControl("txtSDAmountLSE");
        //        Label lblSDRate = (Label)row.FindControl("lblSDRate");
        //        TextBox txtVatLeviable = (TextBox)row.FindControl("txtVATLeviableLSE");
        //        Label lblVATRate = (Label)row.FindControl("lblVATRate");
        //        TextBox txtVatAmount = (TextBox)row.FindControl("txtVATAmountLSE");
        //        Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
        //        ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

        //        obj.ProductID = hfRowProductID.Value.Toint();
        //        obj.ProductName = lblProductName.Text.ToString();
        //        obj.RequiredQuantity = txtQuantity.Text.ToDecimal();           
                                
        //        purchaseLedgerDetailsProviderList.Add(obj);
        //    }
        //    gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
        //    gvRequisition.DataBind();           
        //}

        public void AddBlankRowTogvPurchaseProduct()
        {
            var purchaseLedgerDetailsProviderList = new List<OtherOutProductDetailProvider> { new OtherOutProductDetailProvider() };           
        }
       
        private void Clear()
        {
            txtEntryDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;      
            txtEntryDate.Text = string.Empty;            
            ddlProductDivision.SelectedIndex = 0;           
            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;          
            ddlProductDivision.Enabled = true;          
            UC_ProductSearch1.Clear();
            gvRequisition.DataSource = null;
            gvRequisition.DataBind();
            btnAdd.Enabled = true;
        }
      
        private OtherOutProductProvider RequisitionInfoEntity()
        {
            OtherOutProductProvider entity = null;
            entity = new OtherOutProductProvider
            {
                ReferenceNo = txtRefNo.Text,
                TransactionDate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                RequisitionDate = DateTime.ParseExact(txtEntryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), 
                DivisionID = int.Parse(ddlProductDivision.SelectedValue),
                EntryUserID = Convert.ToInt16(Session["ID"]),
                UpdateUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<OtherOutProductDetailProvider> requisitionDetailEntityList()
        {
            List<OtherOutProductDetailProvider> requisitionDetailProviderList = new List<OtherOutProductDetailProvider>();

            foreach (GridViewRow row in gvRequisition.Rows)
            {
                OtherOutProductDetailProvider obj = new OtherOutProductDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                Label lblProductName = (Label)row.FindControl("lblProduct");

                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.OtherOutQuantity = txtQuantity.Text.ToDecimal();
                obj.PresentStock = txtPresentStock.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();

                if (obj.OtherOutQuantity <= 0)
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
                   

            if(String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                bool msg = false;
                string message = string.Empty;
                string transactionNo = string.Empty;            
                try
                {
                    OtherOutProductProvider requisitionProvider = RequisitionInfoEntity();
                    List<OtherOutProductDetailProvider> requisitionDetailList = requisitionDetailEntityList();
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

           

            if (String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                try
                {
                    OtherOutProductProvider requisitionProvider = RequisitionInfoEntity();
                    List<OtherOutProductDetailProvider> requisitionDetailList = requisitionDetailEntityList();
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Inventory/OtherOutEntryUI.aspx");
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
            List<OtherOutProductDetailProvider> purchaseLedgerDetailsProviderList = new List<OtherOutProductDetailProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                OtherOutProductDetailProvider obj = new OtherOutProductDetailProvider();

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
                obj.OtherOutQuantity = txtRequirement.Text.ToDecimal();
                obj.MeasurementUnitName = txtUnit.Text.ToString();
                obj.MonthlyConsumeQty = txtMonthlyConsume.Text.ToDecimal();
                obj.PresentStock = txtPresentStock.Text.ToDecimal();
                obj.Remarks = txtRemarks.Text.ToString();

                purchaseLedgerDetailsProviderList.Add(obj);
            }
            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();

            OtherOutProductDetailProvider obj2 = new OtherOutProductDetailProvider();
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
            ddlProductDivision.Enabled = false;
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

            List<OtherOutProductDetailProvider> providerList = new List<OtherOutProductDetailProvider>();           

            #region new list
            OtherOutProductDetailProvider obj = new OtherOutProductDetailProvider();           
            
            providerList.Add(obj);
            #endregion        
        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<OtherOutProductDetailProvider> purchaseLedgerDetailsProviderList = new List<OtherOutProductDetailProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                OtherOutProductDetailProvider obj = new OtherOutProductDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequirement = (TextBox)row.FindControl("txtRequirement");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtPresentStock = (TextBox)row.FindControl("txtPresentStock");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.OtherOutQuantity = txtRequirement.Text.ToDecimal();

                    obj.PresentStock = txtPresentStock.Text.ToDecimal();
                    obj.Remarks = txtRemarks.Text.ToString();
                 
                    purchaseLedgerDetailsProviderList.Add(obj);
                }
            }
            gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
            gvRequisition.DataBind();

            if (gvRequisition.Rows.Count == 0)
            {
                ddlProductDivision.Enabled = true;
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
                int divisionID = ddlProductDivision.SelectedItem.Value.Toint();
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                //columns.Add("PriceDeclarationID", "Price Declaration Code");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "SELECT ReferenceNo AS [Reference No], CONVERT(VARCHAR(30),OtherOutEntryDate, 105) AS [Entry Date] FROM [dbo].OtherOutProduct OOP WHERE OOP.DivisionID = " + divisionID + " ORDER BY OtherOutEntryDate DESC, ReferenceNo DESC";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }
        protected void ddlProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            Session["Value"] = ddlProductDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = Convert.ToInt16(ddlProductDivision.SelectedValue);
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