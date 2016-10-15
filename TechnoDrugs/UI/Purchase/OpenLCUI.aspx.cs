using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SecurityModule.Provider;
using System.Data;
using BaseModule;
using PurchaseModule.Provider;
using SetupModule.Provider;
using TechnoDrugs.Helper;
using System.Globalization;

namespace TechnoDrugs.UI.Purchase
{
    public partial class OpenLCUI : PageBase
    {
        #region properties
        string mode = "";
        public OpenLCUI()
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
                txtShipmentDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                txtExpiryDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                txtLCOpeningDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var lCProvider = new LCProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = lCProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                        gvLC.DataSource = dt;
                        gvLC.DataBind();                        
                    }
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;                        
                }
            }
        }
        #endregion
                
        #region methods
        private void RowsIn_gvLC()
        {
            List<LCDetailProvider> lcDetailsProviderList = new List<LCDetailProvider>();
            foreach (GridViewRow row in gvLC.Rows)
            {
                LCDetailProvider obj = new LCDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRequisitionRef = (TextBox)row.FindControl("txtRequisitionRef");
                DropDownList ddlCurrency = (DropDownList)row.FindControl("ddlCurrency");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");    
                
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.Currency = ddlCurrency.SelectedItem.Text;
                obj.RequisitionRef = txtRequisitionRef.Text;
                obj.Quantity = txtQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                obj.Value = txtQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                
                lcDetailsProviderList.Add(obj);
            }
            gvLC.DataSource = lcDetailsProviderList;
            gvLC.DataBind();           
        }

        private void PopulateControls(DataTable dt)
        {
            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    txtSystemLCNo.Text = dt.Rows[k][1].ToString();
                    txtBankLCNumber.Text = dt.Rows[k][2].ToString();                    
                    txtLCAFNumber.Text = dt.Rows[k][6].ToString();
                    txtLCOpeningDate.Text = Convert.ToDateTime(dt.Rows[k][7]).ToString("dd/MM/yyyy");
                    txtShipmentDate.Text = Convert.ToDateTime(dt.Rows[k][9]).ToString("dd/MM/yyyy");
                    txtExpiryDate.Text = Convert.ToDateTime(dt.Rows[k][5]).ToString("dd/MM/yyyy");  
                    ddlModeOfTransport.SelectedValue = dt.Rows[k][8].ToString();
                    
                    ddlSupplier.SelectedValue = dt.Rows[k][10].ToString();                                        
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }            
            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var lcDetailsProviderList = new List<LCDetailProvider> { new LCDetailProvider() };           
        }       
        private void Clear()
        {
            txtExpiryDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;
            txtShipmentDate.Text = string.Empty;           
            txtLCOpeningDate.Text = string.Empty;                
            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;
            UC_ProductSearch1.Clear();
            gvLC.DataSource = null;
            gvLC.DataBind();
            btnAdd.Enabled = true;
        }      
        private LCProvider LCInfoEntity()
        {
            if (ddlModeOfTransport.SelectedIndex == 0)
            {
                throw new Exception("Mode of transport never be empty");
            }
            LCProvider entity = null;
            entity = new LCProvider
            {
                TransactionNo = txtSystemLCNo.Text,
                LCAFNumber = txtLCAFNumber.Text,
                BankLCNumber = txtBankLCNumber.Text,
                LCOpeningDate = DateTime.ParseExact(txtLCOpeningDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ShipmentDate = DateTime.ParseExact(txtShipmentDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ExpiryDate = DateTime.ParseExact(txtExpiryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ModeOfTransport = ddlModeOfTransport.SelectedItem.Text,
                SupplierID = int.Parse(ddlSupplier.SelectedValue),
                EntryUserID = Convert.ToInt16(Session["ID"]),
                UpdateUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<LCDetailProvider> purchaseOrderDetailEntityList()
        {
            List<LCDetailProvider> lcDetailsProviderList = new List<LCDetailProvider>();

            foreach (GridViewRow row in gvLC.Rows)
            {
                LCDetailProvider obj = new LCDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtRequisitionRef = (TextBox)row.FindControl("txtRequisitionRef");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");

                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                DropDownList ddlCurrency = (DropDownList)row.FindControl("ddlCurrency");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.RequisitionRef = txtRequisitionRef.Text.ToString();
                obj.Quantity = txtQuantity.Text.ToDecimal();                
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Currency = ddlCurrency.SelectedItem.Text;
                obj.Value = txtValue.Text.ToDecimal();
                obj.Unit = txtUnit.Text.ToString();

                lcDetailsProviderList.Add(obj);
            }
            return lcDetailsProviderList;
        }      
        
        #endregion
        
        #region button event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            mode = "Save";
            CheckUserAuthentication(mode);

            bool msg = false;
            string message = string.Empty;
            string transactionNo = string.Empty;            
            try
            {
                LCProvider purchaseOrderProvider = LCInfoEntity();
                List<LCDetailProvider> purchaseOrderDetailList = purchaseOrderDetailEntityList();
                if ((purchaseOrderDetailList == null) || (purchaseOrderDetailList.Count == 0))
                {
                    MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                    return;
                }
                msg = purchaseOrderProvider.Save(purchaseOrderDetailList, out transactionNo);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            if (msg)
            {
                Clear();
                this.AlertSuccess(lblMsg, MessageConstants.Saved);
            }
            else
            {
                MessageHelper.ShowAlertMessage(message);
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
            Clear();
            Response.Redirect("~/UI/Purchase/OpenLCUI.aspx");
            this.AlertNone(lblMsg);
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");

            
            if (ddlProductValidation.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                lblMsg.Focus();
                return;
            }
            ProductProvider productProvider = new ProductProvider();
            List<LCDetailProvider> lcDetailsProviderList = new List<LCDetailProvider>();
            foreach (GridViewRow row in gvLC.Rows)
            {
                LCDetailProvider obj = new LCDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequisitionRef = (TextBox)row.FindControl("txtRequisitionRef");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                DropDownList ddlCurrency = (DropDownList)row.FindControl("ddlCurrency");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                if (hfRowProductID.Value == ddlProductValidation.SelectedValue)
                {
                    MessageHelper.ShowAlertMessage("This product already added!");
                    return;
                }
                if (txtQuantity.Text.ToDecimal() <= 0)
                {
                    MessageHelper.ShowAlertMessage("Enter Quantity!");
                    return;
                }
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.RequisitionRef = txtRequisitionRef.Text;
                obj.Currency = ddlCurrency.SelectedItem.Text;
                obj.Quantity = txtQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Value = txtValue.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                lcDetailsProviderList.Add(obj);
            }
            LCDetailProvider obj2 = new LCDetailProvider();
            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();
            obj2.ProductID = productID;
            obj2.ProductName = productName;

            obj2.Currency = "BDT";
            obj2.Unit = productProvider.GetMeasurementUnit(obj2.ProductID);
            lcDetailsProviderList.Add(obj2);

            if (!divGridForPO.Visible)
            {
                divGridForPO.Visible = true;
            }
            gvLC.DataSource = lcDetailsProviderList;
            gvLC.DataBind();
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            RowsIn_gvLC();
        }
        
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<LCDetailProvider> lcDetailsProviderList = new List<LCDetailProvider>();
            foreach (GridViewRow row in gvLC.Rows)
            {
                LCDetailProvider obj = new LCDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRequisitionRef = (TextBox)row.FindControl("txtRequisitionRef");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                DropDownList ddlCurrency = (DropDownList)row.FindControl("ddlCurrency");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.RequisitionRef = txtRequisitionRef.Text.ToString();
                    obj.Quantity = txtQuantity.Text.ToDecimal();
                    obj.Currency = ddlCurrency.SelectedItem.Value;
                    obj.Value = txtValue.Text.ToDecimal();
                    obj.Rate = txtRate.Text.ToDecimal();
                    obj.Unit = txtUnit.Text.ToString();
                 
                    lcDetailsProviderList.Add(obj);
                }
            }
            gvLC.DataSource = lcDetailsProviderList;
            gvLC.DataBind();            
        }
        
        #endregion
        
        protected void gvLC_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }       

        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Bank LC Number");
                //columns.Add("Name", "Name");
                //columns.Add("CardNo", "CardNo");
                //columns.Add("Address", "Address");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "Select BankLCNumber AS [Bank LC Number], Convert(VARCHAR(30), LCOpeningDate, 105) AS [Opening Date] from [dbo].LCChallan ORDER BY [Opening Date]";
                //HttpContext.Current.Session[StaticInfo.Query] = "Select PriceDeclarationID + ' - ' +P.Name + ISNULL( '(' + PG.Name + ')' ,'') + ISNULL( ' - ' + PPS.SizeName, '') from [Vat].PriceDeclaration PD LEFT JOIN Vat.Product P ON P.ID = PD.ID LEFT JOIN Vat.ProductGrade PG ON PG.ID = PD.Grade LEFT JOIN Vat.ProductPackSize PPS ON PPS.ID = PD.SizeID Where PD.StatusID=0 OR PD.StatusID=1";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }

        protected void ddlSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            mode = "Update";
            //   CheckUserAuthentication(mode);

            bool msg = false;
            string message = string.Empty;
            try
            {
                LCProvider purchaseOrderProvider = LCInfoEntity();
                List<LCDetailProvider> purchaseOrderDetailList = purchaseOrderDetailEntityList();
                if ((purchaseOrderDetailList == null) || (purchaseOrderDetailList.Count == 0))
                {
                    MessageHelper.ShowAlertMessage("Please select at least one product for purchase");

                    return;
                }
                msg = purchaseOrderProvider.Update(purchaseOrderDetailList);
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

        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            int codeOrName = 0;
            int productType = 0;
            int divisionID = 0;
            Session["Value"] = ddlDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = Convert.ToInt16(ddlDivision.SelectedValue);
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
            // Session["Value"] = ddlProductDivision.SelectedValue;
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = 30;
            LoadProduct(codeOrName, productType, divisionID);
        }        
    }
}