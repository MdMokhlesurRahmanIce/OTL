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
    public partial class DeliveryChallanUI : PageBase
    {
        #region properties
        //private decimal totalAmount = 0;
        string mode = "";
        string statusMessage = string.Empty;

        public DeliveryChallanUI()
        {
            RequiresAuthorization = true;
        }
        #endregion
        
        #region page load
        protected void Page_Load(object sender, EventArgs e)
        {
           // string userName = Session["UserName"].ToString();
            
            if (!IsPostBack)
            {
                AddBlankRowTogvPurchaseProduct();
                btnSave.Enabled = true;
                txtDeliveryChallanDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else
            {

                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup") 
                {
                    int divisionID = ddlProductDivision.SelectedValue.ToInt();
                    HttpContext.Current.ApplicationInstance.Session["DivisionID"] = divisionID;

                    int challanTypeID = ddlChallanType.SelectedValue.Toint();
                    HttpContext.Current.ApplicationInstance.Session["ChallanTypeID"] = challanTypeID;

                    //if (userName == "Nazrul")
                    //    gvDeliveryChallan.Columns[8].Visible = true;

                    var deliveryChallanProvider = new DeliveryChallanProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = deliveryChallanProvider.GetByID(code);
                    
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                        List<DeliveryChallanDetailProvider> deliveryChallanDetailProviderList1 = new List<DeliveryChallanDetailProvider>();
                        for (int k = 0; k < dt.Rows.Count; k++)
                        {
                            DeliveryChallanDetailProvider obj2 = new DeliveryChallanDetailProvider();
                            obj2.ProductID = Convert.ToInt16(dt.Rows[k]["ProductID"]); //Explicit type conversion
                            obj2.ProductName = dt.Rows[k]["ProductName"].ToString();
                            obj2.SupplierChallanDate = dt.Rows[k]["SupplierChallanDate"].ToString();
                            obj2.SupplierChallanNo = dt.Rows[k]["SupplierChallanNo"].ToString();
                            obj2.MeasurementUnitName = dt.Rows[k]["MeasurementUnitName"].ToString();
                            obj2.ProvidedQuantity = Convert.ToDecimal( dt.Rows[k]["ProvidedQuantity"]);
                            obj2.Value = Convert.ToDecimal(dt.Rows[k]["Value"]);
                            obj2.ReceivedQuantity = Convert.ToDecimal(dt.Rows[k]["ReceivedQuantity"]);
                            deliveryChallanDetailProviderList1.Add(obj2);
                        }
                        gvDeliveryChallan.DataSource = deliveryChallanDetailProviderList1;
                        gvDeliveryChallan.DataBind();

                        //HttpContext.Current.ApplicationInstance.Session["DivisionID"] = null;

                        int nl = 0;
                        foreach (GridViewRow row in gvDeliveryChallan.Rows)
                        {  
                            DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");
                            DropDownList ddlPurchaseOrderNo = (DropDownList)row.FindControl("ddlPurchaseOrderNo");

                            ddlSupplier.SelectedValue = dt.Rows[nl]["SupplierID"].ToString();
                            ddlPurchaseOrderNo.SelectedValue = dt.Rows[nl]["POrderNo"].ToString();
                            nl++;
                        }
                    }
                    if (ckbOption.SelectedValue.Toint() == 2)
                    {
                        txtVehicleInfo.Enabled = false;
                        txtDeliveryChallanDate.Enabled = false;
                        //ddlDestinationUnit.Enabled = false;
                        gvDeliveryChallan.Enabled = false;
                        btnAdd.Enabled = false;
                        btnUpdate.Enabled = false;
                    }
                    else
                    {
                        btnUpdate.Enabled = true;
                    }
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;                        
                }
            }
        }
        #endregion
        
        #region methods
        private void RowsIn_gvPurchaseOrder()
        {
            List<PurchaseOrderDetailProvider> deliveryDetailsProviderList = new List<PurchaseOrderDetailProvider>();
            foreach (GridViewRow row in gvDeliveryChallan.Rows)
            {
                PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtProvidedQuantity = (TextBox)row.FindControl("txtProvidedQuantity");              
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.Quantity = txtProvidedQuantity.Text.ToDecimal();             
                                
                deliveryDetailsProviderList.Add(obj);
            }
            gvDeliveryChallan.DataSource = deliveryDetailsProviderList;
            gvDeliveryChallan.DataBind();           
        }

        private void PopulateControls(DataTable dt)
        {
            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    txtDeliveryChallanDate.Text = Convert.ToDateTime(dt.Rows[k]["DeliveryChallanDate"]).ToString("dd/MM/yyyy");
                    txtDeliveryChallanNo.Text = dt.Rows[k]["DeliveryChallanNo"].ToString();
                    txtVehicleInfo.Text = dt.Rows[k]["VehicleInfo"].ToString();
                    //ddlDestinationUnit.SelectedItem.Text = dt.Rows[k]["DestinationUnit"].ToString();
                    ckbOption.SelectedValue = dt.Rows[k]["StatusID"].ToString();
                    ddlProductDivision.SelectedValue = dt.Rows[k]["DivisionID"].ToString();
                }
            }
            catch (Exception ex)
            {

                throw (ex);
            }
        }            
            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var deliveryDetailsProviderList = new List<DeliveryChallanDetailProvider> { new DeliveryChallanDetailProvider() };           
        }
       
        private void Clear()
        {
            txtDeliveryChallanDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;
            txtDeliveryChallanDate.Text = string.Empty;
            txtVehicleInfo.Text = string.Empty;

            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;
            UC_ProductSearch1.Clear();
            gvDeliveryChallan.DataSource = null;
            gvDeliveryChallan.DataBind();
            btnAdd.Enabled = true;
            
            this.AlertNone(lblMsg);
        }
      
        private DeliveryChallanProvider PurchaseOrderInfoEntity()
        {
            DeliveryChallanProvider entity = null;
            entity = new DeliveryChallanProvider
            {
                TransactionNo = txtDeliveryChallanNo.Text,
                DeliveryChallanDate = DateTime.ParseExact(txtDeliveryChallanDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),                
                TransactionDate = DateTime.ParseExact(txtDeliveryChallanDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                VehicleInfo = txtVehicleInfo.Text,
               // DestinationUnit = ddlDestinationUnit.SelectedValue.ToString(),
                StatusID = ckbOption.SelectedValue.Toint(),
                DivisionID = ddlProductDivision.SelectedValue.Toint(),
                ChallanTypeID = ddlChallanType.SelectedValue.Toint(),
                EntryUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<DeliveryChallanDetailProvider> deliveryDetailEntityList()
        {
            List<DeliveryChallanDetailProvider> deliveryDetailsProviderList = new List<DeliveryChallanDetailProvider>();

            foreach (GridViewRow row in gvDeliveryChallan.Rows)
            {
                DeliveryChallanDetailProvider obj = new DeliveryChallanDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtProvidedQuantity = (TextBox)row.FindControl("txtProvidedQuantity");
                TextBox txtSupplierChallanNo = (TextBox)row.FindControl("txtSupplierChallanNo");
                DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");                
                DropDownList ddlPurchaseOrderNo = (DropDownList)row.FindControl("ddlPurchaseOrderNo");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtReceivedQuantity = (TextBox)row.FindControl("txtReceivedQuantity");
                TextBox txtSupplierChallanDate = (TextBox)row.FindControl("txtSupplierChallanDate");
            
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.SupplierID = Convert.ToInt16(ddlSupplier.SelectedValue);
                string[] PONo = ddlPurchaseOrderNo.SelectedItem.Text.Split('-');
                obj.POrderNo = PONo[0].Trim();
                obj.ProvidedQuantity = txtProvidedQuantity.Text.ToDecimal();
                obj.ReceivedQuantity = txtReceivedQuantity.Text.ToDecimal();
                obj.SupplierChallanNo = txtSupplierChallanNo.Text.ToString();
                obj.SupplierChallanDate = txtSupplierChallanDate.Text;
              
                deliveryDetailsProviderList.Add(obj);
            }
            return deliveryDetailsProviderList;
        }              
        #endregion
        
        #region button event
        protected void btnSave_Click(object sender, EventArgs e)
        {
            mode = "Save";

            string permisionMessage = CheckUserAuthentication(mode);
            if (ckbOption.SelectedValue != "")
            {
                statusMessage = CheckUserAuthentication(ckbOption.SelectedItem.ToString());
            }
            if (String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                bool msg = false;
                string message = string.Empty;
                string transactionNo = string.Empty;
                try
                {
                    DeliveryChallanProvider deliveryProvider = PurchaseOrderInfoEntity();
                    List<DeliveryChallanDetailProvider> deliveryDetailList = deliveryDetailEntityList();
                    if ((deliveryDetailList == null) || (deliveryDetailList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please select at least one product for delivery");
                        return;
                    }
                    msg = deliveryProvider.Save(deliveryDetailList, out transactionNo);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                if (msg)
                {
                    Clear();
                    txtDeliveryChallanNo.Text = transactionNo;
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

            string permisionMessage = CheckUserAuthentication(mode);
            if (ckbOption.SelectedValue != "")
            {
                statusMessage = CheckUserAuthentication(ckbOption.SelectedItem.ToString());
            }
            if (String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                bool msg = false;
                string message = string.Empty;
                try
                {
                    DeliveryChallanProvider deliveryProvider = PurchaseOrderInfoEntity();
                    List<DeliveryChallanDetailProvider> deliveryDetailList = deliveryDetailEntityList();
                    if ((deliveryDetailList == null) || (deliveryDetailList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please select at least one product for delivery challan.");
                        return;
                    }
                    msg = deliveryProvider.Update(deliveryDetailList);
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
            Response.Redirect("~/UI/Purchase/DeliveryChallanUI.aspx");
            this.AlertNone(lblMsg);
        }
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                mode = "Preview";
                CheckUserAuthentication(mode);

                string deliveryChallanNo = txtDeliveryChallanNo.Text.Trim();
                if (deliveryChallanNo == "")
                {
                    MessageHelper.ShowAlertMessage("Please select a Delivery Challan No!");
                    return;
                }
                int reportOption = 2;
                string productID = "";
                string fromDate = "";
                string todate = "";
                //string adate = "";
                string transactionNo = txtDeliveryChallanNo.Text;
                int? reportCategory = 2;

                string page = "../../Reports/ReportUI/GeneralReportViewerUI.aspx?ReportType=" + ReportType.DeliveryChallanReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&productID=" + productID + "&fromDate=" + fromDate + "&todate=" + todate + "&transactionNo=" + transactionNo;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
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
            List<DeliveryChallanDetailProvider> deliveryChallanDetailProviderList = new List<DeliveryChallanDetailProvider>();
            foreach (GridViewRow row in gvDeliveryChallan.Rows)
            {
                DeliveryChallanDetailProvider obj = new DeliveryChallanDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                DropDownList ddlPurchaseOrderNo = (DropDownList)row.FindControl("ddlPurchaseOrderNo");
                DropDownList ddlReqRefNo = (DropDownList)row.FindControl("ddlReqRefNo");
                DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");

                TextBox txtSupplierChallanNo = (TextBox)row.FindControl("txtSupplierChallanNo");
                TextBox txtReceivedQuantity = (TextBox)row.FindControl("txtReceivedQuantity");
                TextBox txtProvidedQuantity = (TextBox)row.FindControl("txtProvidedQuantity");                
                TextBox txtSupplierChallanDate = (TextBox)row.FindControl("txtSupplierChallanDate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                if (hfRowProductID.Value == ddlProductValidation.SelectedValue)
                {
                    MessageHelper.ShowAlertMessage("This product already added!");
                    return;
                }
                if (txtProvidedQuantity.Text.ToDecimal() <= 0)
                {
                    MessageHelper.ShowAlertMessage("Please enter provided quantity!");
                    return;
                }
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.POrderNo = ddlPurchaseOrderNo.SelectedItem.Value;
                obj.SupplierChallanDate = txtSupplierChallanDate.Text;
                obj.SupplierName = ddlSupplier.SelectedItem.Value;
                obj.MeasurementUnitName = txtUnit.Text;
                obj.ProvidedQuantity = txtProvidedQuantity.Text.ToDecimal();
                obj.SupplierChallanNo = txtSupplierChallanNo.Text;
                obj.ReceivedQuantity = txtReceivedQuantity.Text.ToDecimal();
                deliveryChallanDetailProviderList.Add(obj);
            }

            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();                     

            DeliveryChallanDetailProvider obj2 = new DeliveryChallanDetailProvider();
            obj2.ProductID = productID;
            obj2.ProductName = productName;
            obj2.MeasurementUnitName = productProvider.GetMeasurementUnit(obj2.ProductID);
            obj2.PackSizeName = productProvider.GetPackSizeName(obj2.ProductID);
            deliveryChallanDetailProviderList.Add(obj2);

            if (!divGridForPO.Visible)
            {
                divGridForPO.Visible = true;
            }

            int divisionID = ddlProductDivision.SelectedValue.ToInt();
            int challanTypeID = ddlChallanType.SelectedValue.ToInt();
            HttpContext.Current.ApplicationInstance.Session["DivisionID"] = divisionID;
            HttpContext.Current.ApplicationInstance.Session["ChallanTypeID"] = challanTypeID;

            gvDeliveryChallan.DataSource = deliveryChallanDetailProviderList;
            gvDeliveryChallan.DataBind();


            //HttpContext.Current.ApplicationInstance.Session["DivisionID"] = null;
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

            List<DeliveryChallanDetailProvider> providerList = new List<DeliveryChallanDetailProvider>();

            DeliveryChallanDetailProvider obj = new DeliveryChallanDetailProvider();           
            
            providerList.Add(obj);
        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<DeliveryChallanDetailProvider> deliveryChallanDetailProviderList = new List<DeliveryChallanDetailProvider>();
            foreach (GridViewRow row in gvDeliveryChallan.Rows)
            {
                DeliveryChallanDetailProvider obj = new DeliveryChallanDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtReceivedQuantity = (TextBox)row.FindControl("txtReceivedQuantity");
                DropDownList ddlPurchaseOrderNo = (DropDownList)row.FindControl("ddlPurchaseOrderNo");
                //DropDownList ddlReqRefNo = (DropDownList)row.FindControl("ddlReqRefNo");
                DropDownList ddlSupplier = (DropDownList)row.FindControl("ddlSupplier");

               // TextBox txtValue = (TextBox)row.FindControl("txtValue");
                //TextBox txtPurchaseDate = (TextBox)row.FindControl("txtPurchaseDate");
                TextBox txtSupplierChallanDate = (TextBox)row.FindControl("txtSupplierChallanDate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");


                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtProvidedQuantity = (TextBox)row.FindControl("txtProvidedQuantity");
                TextBox txtSupplierChallanNo = (TextBox)row.FindControl("txtSupplierChallanNo");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.ProvidedQuantity = txtProvidedQuantity.Text.ToDecimal();
                   // obj.ReqReferenceNo = ddlReqRefNo.SelectedItem.Value.ToString();
                    obj.SupplierName = ddlSupplier.SelectedItem.Value.ToString();
                    obj.POrderNo = ddlPurchaseOrderNo.SelectedItem.Value.ToString();
                    obj.ReceivedQuantity = txtReceivedQuantity.Text.ToDecimal();
                    
                    //obj.PurchaseDate = Convert.ToDateTime(txtPurchaseDate.Text).ToString("dd/MM/yyyy");
                    //obj.PurchaseDate = txtPurchaseDate.Text;
                    obj.SupplierChallanDate = txtSupplierChallanDate.Text;
                    obj.SupplierChallanNo = txtSupplierChallanNo.Text.ToString();
                   // obj.Value = txtValue.Text.ToDecimal();

                    obj.MeasurementUnitName = txtUnit.Text;
                
                    deliveryChallanDetailProviderList.Add(obj);
                }
            }
            gvDeliveryChallan.DataSource = deliveryChallanDetailProviderList;
            gvDeliveryChallan.DataBind();       

        }
        
        #endregion

        protected void gvDeliveryChallan_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlPurchaseOrderNo = (e.Row.FindControl("ddlPurchaseOrderNo") as DropDownList);

                PurchaseOrderProvider purchaseOrderProvider = new PurchaseOrderProvider();
                DataSet ds = purchaseOrderProvider.GetDivisioinWisePONo(ddlProductDivision.SelectedItem.Value.Toint());
                ddlPurchaseOrderNo.DataSource = ds;
                ddlPurchaseOrderNo.DataBind();
            }
        }       
        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (ddlChallanType.SelectedValue == "" || ddlProductDivision.SelectedValue == "0")
                    throw new Exception("Please select division and challan type.");
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Price Declaration Code");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "SELECT distinct D.DeliveryChallanNo AS [Delivery Challan No], Convert(VARCHAR(30), " +
                " D.DeliveryChallanDate ,105) AS [Delivery Challan Date], PT.Name AS [Product Type] FROM [dbo].Delivery AS D" +
                " INNER JOIN dbo.DeliveryDetail DD ON D.ID = DD.DeliveryID" +
                " INNER JOIN dbo.Product P ON DD.ProductID = P.ID" +
                " INNER JOIN dbo.ProductType PT ON P.ItemTypeID = PT.ID" +
                " WHERE D.DivisionID = " + ddlProductDivision.SelectedValue.Toint() + " AND D.ChallanTypeID = " + ddlChallanType.SelectedValue.Toint() + " " +
                " ORDER BY D.DeliveryChallanNo DESC";
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
        protected void ddlProductDivision_SelectedIndexChanged(object sender, EventArgs e)
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
            RadioButtonList rbProductCodeName = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductCodeName");
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            codeOrName = Convert.ToInt32(rbProductCodeName.SelectedValue);
            productType = Convert.ToInt32(rbProductType.SelectedValue);
            divisionID = 30;
            LoadProduct(codeOrName, productType, divisionID);
        }        
    }
}