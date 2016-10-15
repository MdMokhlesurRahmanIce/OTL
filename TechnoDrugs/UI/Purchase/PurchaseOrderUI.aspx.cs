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
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;

namespace TechnoDrugs.UI.Purchase
{
    public partial class PurchaseOrderUI : PageBase
    {
        //string strConnString = @"Data Source=(local); database=mvc; user id=sa; password=1234;Integrated Security=true";

        #region properties
        string mode = "";
        public PurchaseOrderUI()
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
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var purchaseOrderProvider = new PurchaseOrderProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = purchaseOrderProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                        gvPurchaseOrder.DataSource = dt;
                        gvPurchaseOrder.DataBind();                        
                    }
                    btnSave.Visible = false;
                    btnUpdate.Visible = true;                        
                }
            }
            //txtPurchaseOrderDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //txtIntDeliveryDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            Session["Value"] = ddlProductDivision.SelectedValue;
        }
        #endregion
               
        #region methods
        private void RowsIn_gvPurchaseOrder()
        {
            List<PurchaseOrderDetailProvider> purchaseLedgerDetailsProviderList = new List<PurchaseOrderDetailProvider>();
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");    
                
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.Quantity = txtQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                obj.Value = txtQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                
                purchaseLedgerDetailsProviderList.Add(obj);
                txtQuantity.Focus();
            }
            gvPurchaseOrder.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseOrder.DataBind();           
        }

        private void PopulateControls(DataTable dt)
        {
            try
            {
                txtPurchaseOrderDate.Text = Convert.ToDateTime(dt.Rows[0]["PurchaseOrderDate"]).ToString("dd/MM/yyyy");
                txtIntDeliveryDate.Text = Convert.ToDateTime(dt.Rows[0]["AppxDeliveryDate"]).ToString("dd/MM/yyyy");
                txtPOrderNo.Text = dt.Rows[0]["POrderNo"].ToString();
                ddlRequistionRef.SelectedValue = dt.Rows[0]["RequisitionRefID"].ToString();
                ddlSupplier.SelectedValue = dt.Rows[0]["SupplierID"].ToString();
                ddlProductDivision.SelectedValue = dt.Rows[0]["DivisionID"].ToString();
                ddlProductDivision.Enabled = false;
                switch (Convert.ToInt16(dt.Rows[0]["MessageValue"]))
                {
                    case 3:
                        ckbVATTAXMessage.Items[0].Selected = true;//vat
                        ckbVATTAXMessage.Items[1].Selected = true;//tax
                        break;
                    case 2:
                        ckbVATTAXMessage.Items[1].Selected = true;
                        break;
                    case 1:
                        ckbVATTAXMessage.Items[0].Selected = true;
                        break;
                    default:
                        ckbVATTAXMessage.Items[0].Selected = false;//vat
                        ckbVATTAXMessage.Items[1].Selected = false;//tax
                        break;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }            
            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var purchaseLedgerDetailsProviderList = new List<PurchaseOrderDetailProvider> { new PurchaseOrderDetailProvider() };           
        }       
        private void Clear()
        {
            //txtPurchaseOrderDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            //txtIntDeliveryDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;      
            ddlRequistionRef.SelectedIndex = 0;
            ddlProductDivision.Enabled = true;
            AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;
            ddlRequistionRef.Enabled = true;
            ckbVATTAXMessage.ClearSelection();
            UC_ProductSearch1.Clear();
            gvPurchaseOrder.DataSource = null;
            gvPurchaseOrder.DataBind();
            btnAdd.Enabled = true;
        }      
        private PurchaseOrderProvider PurchaseOrderInfoEntity()
        {
            PurchaseOrderProvider entity = null;
            entity = new PurchaseOrderProvider
            {
                POrderNo = txtPOrderNo.Text,
                TransactionDate = DateTime.ParseExact(txtPurchaseOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                PurchaseOrderDate = DateTime.ParseExact(txtPurchaseOrderDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                AppxDeliveryDate = DateTime.ParseExact(txtIntDeliveryDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                SupplierID = int.Parse(ddlSupplier.SelectedValue),
                RequisitionRefID = int.Parse(ddlRequistionRef.SelectedValue),
                EntryUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<PurchaseOrderDetailProvider> purchaseOrderDetailEntityList()
        {
            List<PurchaseOrderDetailProvider> purchaseLedgerDetailsProviderList = new List<PurchaseOrderDetailProvider>();

            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");

                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.Quantity = txtQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Value = txtValue.Text.ToDecimal();
                obj.Unit = txtUnit.Text.ToString();

                if (obj.Quantity <= 0 || obj.Rate <= 0)
                    throw new Exception("Quantity and rate can't be zero");

                purchaseLedgerDetailsProviderList.Add(obj);                
            }
            return purchaseLedgerDetailsProviderList;
        }            
        #endregion
        
        #region button event
        protected void btnSave_Click(object sender, EventArgs e)
        {

            DateTime moment = DateTime.Now;

            int sumAll = moment.Year + moment.Month + moment.Hour+moment.Minute + moment.Second;

            //imagesh.Visible = true;
            mode = "Save";
            CheckUserAuthentication(mode);
            
            bool msg = false;
            string message = string.Empty;
            string transactionNo = string.Empty;
            //if ((sumAll - Convert.ToInt32(Session["sumTime"] == null ? 0 : Session["sumTime"])) > 55)
            // {
                try
                {
                    PurchaseOrderProvider purchaseOrderProvider = PurchaseOrderInfoEntity();

                    foreach (ListItem item in ckbVATTAXMessage.Items)
                    {
                        if (item.Selected)
                        {
                            purchaseOrderProvider.MessageValue += item.Value.Toint();
                        }
                    }
                    List<PurchaseOrderDetailProvider> purchaseOrderDetailList = purchaseOrderDetailEntityList();
                    if ((purchaseOrderDetailList == null) || (purchaseOrderDetailList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                        return;
                    }
                    msg = purchaseOrderProvider.Save(purchaseOrderDetailList, out transactionNo);
                    //
                    Session["sumTime"] = sumAll;
                    
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
             //}
            if (msg)
            {
                Clear();
                txtPOrderNo.Text = transactionNo;
               // imagesh.Visible = false;
                btnSave.Enabled = true;
                this.AlertSuccess(lblMsg, MessageConstants.Saved);
               // Session["btnDoubleSave"] = 1;
                //hfDoubleSaveBtn.Value = "1";
                ////popup1.Show();
                //lblNoDataFound.Text = "Data Saved Successfully";
                //btnCancel.Text = "OK";
                

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
        protected void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                mode = "Preview";
                CheckUserAuthentication(mode);

                string purchasesID = txtPOrderNo.Text.Trim();
                if (purchasesID == "")
                {
                    MessageHelper.ShowAlertMessage("Please select a Purchases Order!");
                    return;
                }
                int reportOption = 2;
                string productID = "";
                string fromDate = "";
                string todate = "";
                string adate = "";
                string transactionNo = txtPOrderNo.Text;
                int? reportCategory = 1;

                string page = "../../Reports/ReportUI/GeneralReportViewerUI.aspx?ReportType=" + ReportType.PurchaseOrderReport + "&reportOption=" + reportOption + "&reportCategory=" + reportCategory + "&productID=" + productID + "&fromDate=" + fromDate + "&todate=" + todate + "&transactionNo=" + transactionNo;
                ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + page + "',null,'scrollbars=yes,height=auto,width=auto,toolbar=no,menubar=no,statusbar=yes');", true);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Purchase/PurchaseOrderUI.aspx");
            this.AlertNone(lblMsg);
        }        
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            this.AlertNone(lblMsg);

            ProductProvider productProvider = new ProductProvider();
            AjaxControlToolkit.ComboBox ddlProductValidation = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");

            string requisitionAndDate = ddlRequistionRef.SelectedItem.Text;
            string[] splitValue = requisitionAndDate.Split(' ');
            string requisitionRefNo = splitValue[0];

            bool isExist = productProvider.GetMeasurementUnit(ddlProductValidation.SelectedValue.Toint(), requisitionRefNo);
            if (isExist == false)
            {
                MessageHelper.ShowAlertMessage("This product is not listed in the requistion");

                return;
            }
            if (ddlProductValidation.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                lblMsg.Focus();
                return;
            }
            
            List<PurchaseOrderDetailProvider> purchaseLedgerDetailsProviderList = new List<PurchaseOrderDetailProvider>();
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");

                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
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
                obj.Quantity = txtQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.Value = txtValue.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                purchaseLedgerDetailsProviderList.Add(obj);
            }

            AjaxControlToolkit.ComboBox ddlProduct = (AjaxControlToolkit.ComboBox)UC_ProductSearch1.FindControl("ddlProduct");
            string productName = ddlProduct.SelectedItem.Text;
            int productID = ddlProduct.SelectedValue.Toint();

            PurchaseOrderDetailProvider obj2 = new PurchaseOrderDetailProvider();
            obj2.ProductID = productID;
            obj2.ProductName = productName;
            obj2.Rate = obj2.GetLastUnitPrice(obj2.ProductID);
            obj2.Unit = productProvider.GetMeasurementUnit(obj2.ProductID);
            purchaseLedgerDetailsProviderList.Add(obj2);

            if (!divGridForPO.Visible)
            {
                divGridForPO.Visible = true;
            }
            gvPurchaseOrder.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseOrder.DataBind();
            ddlProductDivision.Enabled = false;
            RadioButtonList rbProductType = (RadioButtonList)UC_ProductSearch1.FindControl("rbProductType");
            rbProductType.Enabled = false;
        }
        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            RowsIn_gvPurchaseOrder();
        }
        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            RowsIn_gvPurchaseOrder();
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

            List<PurchaseOrderDetailProvider> providerList = new List<PurchaseOrderDetailProvider>();           

            PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();           
            
            providerList.Add(obj);
        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<PurchaseOrderDetailProvider> purchaseLedgerDetailsProviderList = new List<PurchaseOrderDetailProvider>();
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                PurchaseOrderDetailProvider obj = new PurchaseOrderDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtQuantity = (TextBox)row.FindControl("txtQuantity");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.Quantity = txtQuantity.Text.ToDecimal();

                    obj.Value = txtValue.Text.ToDecimal();
                    obj.Rate = txtRate.Text.ToDecimal();
                    obj.Unit = txtUnit.Text.ToString();
                 
                    purchaseLedgerDetailsProviderList.Add(obj);
                }
            }
            gvPurchaseOrder.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseOrder.DataBind();
            if (gvPurchaseOrder.Rows.Count == 0)
            {
                ddlRequistionRef.Enabled = true;
            }
        }
        #endregion

        public decimal GetTotalAmount()
        {
            decimal totalAmount = 0;
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                totalAmount = totalAmount + txtValue.Text.ToDecimal();
            }
            return totalAmount;
        }

        protected void gvPurchaseOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }       

        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            //try
            //{
            //    int divisionID = ddlProductDivision.SelectedItem.Value.Toint();
            //    System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
            //    Dictionary<string, string> columns = new Dictionary<string, string>();
            //    columns.Add("PriceDeclarationID", "Price Declaration Code");
            //    HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
            //    HttpContext.Current.Session[StaticInfo.Query] = "Select POrderNo AS [Purchase Order No], Convert(varchar(30),PurchaseOrderDate ,105) AS [Purchase Order Date], Name AS [Supplier Name] from [dbo].PurchaseOrder  AS PO INNER JOIN dbo.Suppliers S ON PO.SupplierID = S.ID INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID where  R.DivisionID = " + divisionID + " ORDER BY PurchaseOrderDate DESC";
            //    string javaScript = string.Format("javascript:Search();");
            //    ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            //}
            //catch (Exception ex)
            //{
            //    MessageHelper.ShowAlertMessage(ex.ToString());
            //}

            //////
            //txtCustomerID.ReadOnly = false;
            //txtCustomerID.Text = string.Empty;
            //txtContactName.Text = string.Empty;
            //txtPhone.Text = string.Empty;
            //popup.Show();



            //string id = "98";// txtID.Text.Trim();
            try
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                lblNoDataFound.Text = "";
                int divisionID = ddlProductDivision.SelectedItem.Value.Toint();
                if (divisionID.ToString() != "0")
                {
                    string strConnString = ConfigurationManager.ConnectionStrings["technoConnectionString"].ToString();
                    //string strConnString = @"Data Source = POLASH-PC; User Id = sa; pwd = @technodrugs@; Initial Catalog = TechnoDrugsLiveTestVersion;";

                    string qurStrng = "Select POrderNo AS [PurchaseOrderNo], Convert(varchar(30),PurchaseOrderDate ,105) AS [PurchaseOrderDate], Name AS [SupplierName] from [dbo].PurchaseOrder  AS PO INNER JOIN dbo.Suppliers S ON PO.SupplierID = S.ID INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID where  R.DivisionID = " + divisionID + " ORDER BY PO.PurchaseOrderDate DESC";

                    SqlConnection con = new SqlConnection(strConnString);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(qurStrng, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    GridView2.DataSource = dt;
                    GridView2.DataBind();

                    popup1.Show();
                }

                else if (divisionID.ToString() == "0")
                {
                    lblNoDataFound.Text = "No Data Found....";
                    popup1.Show();
                }
            }

            catch(Exception ex)
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
                PurchaseOrderProvider purchaseOrderProvider = PurchaseOrderInfoEntity();
                foreach (ListItem item in ckbVATTAXMessage.Items)
                {
                    if (item.Selected)
                    {
                        purchaseOrderProvider.MessageValue += item.Value.Toint();
                    }
                }
                List<PurchaseOrderDetailProvider> purchaseOrderDetailList = purchaseOrderDetailEntityList();
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

        protected void ddlRequistionRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            string requisitionAndDate = ddlRequistionRef.SelectedItem.Text;
            string[] splitValue = requisitionAndDate.Split(' ');
            string requisitionRefNo = splitValue[0];
            RequisitionProvider requisitionProvider = new RequisitionProvider();
            DataTable dt = requisitionProvider.GetByID(requisitionRefNo);
            lvRequisitionProduct.DataSource = dt;
            lvRequisitionProduct.DataBind();
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

            // Load Requistion References
            ddlRequistionRef.Items.Clear();
            RequisitionProvider requisitionProvider = new RequisitionProvider();
            string filterExpression = "R.DivisionID = "+ divisionID +" ";
            DataSet ds = requisitionProvider.GetDivisioinWiseRequisitionNo(filterExpression);            
            ddlRequistionRef.DataSource = ds;
            ddlRequistionRef.DataBind();
            ddlRequistionRef.Items.Insert(0, new ListItem("----------Select Product----------", "0"));
            ddlRequistionRef.SelectedIndex = 0;
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
       


        //...............my code for popup and image loading...................//

        protected void Select(object sender, EventArgs e)
        {
            using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
            {
                string id = row.Cells[1].Text;


                var purchaseOrderProvider = new PurchaseOrderProvider();
               // string code = Request["__EVENTARGUMENT"];
                DataTable dt = purchaseOrderProvider.GetByID(id);        
                if (dt.IsNotNull())
                {
                    PopulateControls(dt);
                    gvPurchaseOrder.DataSource = dt;
                    gvPurchaseOrder.DataBind();
                }
                btnSave.Visible = false;
                btnUpdate.Visible = true; 
            }

        }

                   //................not paging in modal grid................//
        //protected void OnPageChange(object sender, GridViewPageEventArgs e)
        //{
        //    int divisionID = ddlProductDivision.SelectedItem.Value.Toint();
        //    if (divisionID.ToString() != "0")
        //    {
        //        string strConnString = @"Data Source=(local); database=TechnoDrugsDevelopmentVersion; user id=sa; password=1234;Integrated Security=true";

        //        string qurStrng = "Select POrderNo AS [PurchaseOrderNo], Convert(varchar(30),PurchaseOrderDate ,105) AS [PurchaseOrderDate], Name AS [SupplierName] from [dbo].PurchaseOrder  AS PO INNER JOIN dbo.Suppliers S ON PO.SupplierID = S.ID INNER JOIN dbo.Requisition R ON PO.RequisitionRefID = R.ID where  R.DivisionID = " + divisionID + " ORDER BY PurchaseOrderDate DESC";

        //        SqlConnection con = new SqlConnection(strConnString);
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand(qurStrng, con);
        //        DataTable dt = new DataTable();
        //        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //        sda.Fill(dt);
        //        GridView2.DataSource = dt;
        //        GridView2.DataBind();

        //       popup1.Show();
        //    }

        //    GridView2.PageIndex = e.NewPageIndex;
        //    GridView2.DataBind();
        //}
    }
}