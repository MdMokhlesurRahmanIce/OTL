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
    public partial class ReturnRejectEntryUI : PageBase
    {
        #region properties
       // private decimal totalAmount = 0;
        string statusMessage = string.Empty;
        string mode = "";
        public ReturnRejectEntryUI()
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
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var requisitionProvider = new ProductionRequisitionProvider();
                    string batchNRefNo = Request["__EVENTARGUMENT"];
                    char[] splitchar = { ',' };
                    string[] batchNRefNoSpliting = batchNRefNo.Split(splitchar);
                    string batchNo = batchNRefNoSpliting[0].ToString();
                    string refNo = batchNRefNoSpliting[1].ToString();

                    DataTable dt = requisitionProvider.GetByIDForRetRej(batchNo, refNo);
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                        gvPurchaseForLSE.DataSource = dt;
                        gvPurchaseForLSE.DataBind();                        
                    }
                    
                }
            }
            Session["Value"] = ddlRequisitionDivision.SelectedValue;
        }
        #endregion

        #region methods
        private void RowsIn_gvPurchaseForLSE()
        {
            List<ProductionRequisitionDetailProvider> purchaseLedgerDetailsProviderList = new List<ProductionRequisitionDetailProvider>();
            foreach (GridViewRow row in gvPurchaseForLSE.Rows)
            {
                ProductionRequisitionDetailProvider obj = new ProductionRequisitionDetailProvider();
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
                obj.RawProductName = lblProductName.Text.ToString();
                obj.RequiredQuantity = txtQuantity.Text.ToDecimal();             

                
                purchaseLedgerDetailsProviderList.Add(obj);
            }
            gvPurchaseForLSE.DataSource = purchaseLedgerDetailsProviderList;
            gvPurchaseForLSE.DataBind();           
        }

        private void PopulateControls(DataTable dt)
        {
            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    ddlFinishedProduct.SelectedValue = dt.Rows[k]["FinishedProductID"].ToString();
                    txtRequisitionDate.Text = Convert.ToDateTime(dt.Rows[k]["RequisitionDate"]).ToString("dd/MM/yyyy");
                    txtMfgDate.Text = Convert.ToDateTime(dt.Rows[k]["MfgDate"]).ToString("dd/MM/yyyy");
                    txtExpDate.Text = Convert.ToDateTime(dt.Rows[k]["ExpDate"]).ToString("dd/MM/yyyy");
                    if(dt.Rows[k]["RetRejDate"].ToString() != string.Empty)
                    txtRetRejDate.Text = Convert.ToDateTime(dt.Rows[k]["RetRejDate"]).ToString("dd/MM/yyyy");
                    txtRefNo.Text = dt.Rows[k]["ReferenceNo"].ToString();
                    ddlRequisitionDivision.SelectedValue = dt.Rows[k]["DivisionID"].ToString();
                    ckbOption.SelectedValue = dt.Rows[k]["StatusID"].ToString();
                    ckbBatchRejection.Checked = Convert.ToBoolean(dt.Rows[k]["IsBatchRejected"]);
                    txtBatchNo.Text = dt.Rows[k]["BatchNo"].ToString();
                    txtBatchSize.Text = dt.Rows[k]["BatchSize"].ToString();
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
            ddlRequisitionDivision.Enabled = true;          
            UC_ProductSearch1.Clear();
            gvPurchaseForLSE.DataSource = null;
            gvPurchaseForLSE.DataBind();
            btnAdd.Enabled = true;
        }     
        
       
        #endregion

        #region button event
        
        
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
            Response.Redirect("~/UI/Production/ReturnRejectEntryUI.aspx");
            this.AlertNone(lblMsg);
        }                
        #endregion

        //#region grid event
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
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "Select BatchNo, ReferenceNo AS [Reference Number], Convert(VARCHAR(30), RequisitionDate, 105) AS [Requisition Date] from [dbo].ProductionRequisition PR where DivisionID = " + ddlRequisitionDivision.SelectedValue.Toint() + " ";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }
                
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
                    ProductionRequisitionProvider requisitionProvider = new ProductionRequisitionProvider();

                        requisitionProvider.TransactionNo = txtRefNo.Text;
                        requisitionProvider.BatchNo = txtBatchNo.Text;
                       requisitionProvider.RetRejDate = DateTime.ParseExact(txtRetRejDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        requisitionProvider.StatusID = ckbOption.SelectedValue.Toint();
                        requisitionProvider.IsBatchRejcted = ckbBatchRejection.Checked;
                        requisitionProvider.EntryUserID = Convert.ToInt16(Session["ID"]);
                    

                    List<ProductionRequisitionDetailProvider> requisitionDetailProviderList = new List<ProductionRequisitionDetailProvider>();

                    
                    foreach (GridViewRow row in gvPurchaseForLSE.Rows)
                    {
                        ProductionRequisitionDetailProvider obj = new ProductionRequisitionDetailProvider();

                        HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                        TextBox txtReturnQuantity = (TextBox)row.FindControl("txtReturnQuantity");
                        TextBox txtRejectQuantity = (TextBox)row.FindControl("txtRejectQuantity");
                        TextBox txtReturnReceived = (TextBox)row.FindControl("txtReturnReceived");
                        TextBox txtRejectReceived = (TextBox)row.FindControl("txtRejectReceived");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        //Label lblTotalAmount = (Label)row.FindControl("lblTotalAmount");
                        ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                        Label lblProductName = (Label)row.FindControl("lblProduct");
                        obj.ProductID = hfRowProductID.Value.Toint();
                        if(txtReturnQuantity.Text.ToDecimal() > 0 || txtRejectQuantity.Text.ToDecimal() >0 || txtReturnReceived.Text.ToDecimal() > 0 || txtRejectReceived.Text.ToDecimal() > 0)
                        {
                        obj.ReturnQuantity = txtReturnQuantity.Text.ToDecimal();//
                        obj.RejectQuantity = txtRejectQuantity.Text.ToDecimal();//
                        obj.ReturnReceived = txtReturnReceived.Text.ToDecimal();//
                        obj.RejectReceived = txtRejectReceived.Text.ToDecimal();//
                        //if (obj.ReturnQuantity <= 0 || obj.RejectQuantity <= 0)
                        //    throw new Exception("Please input return or reject quantity");
                        obj.Remarks = txtRemarks.Text.ToString();
                        requisitionDetailProviderList.Add(obj);
                        }
                    }

                    if ((requisitionDetailProviderList == null) || (requisitionDetailProviderList.Count == 0))
                    {
                        MessageHelper.ShowAlertMessage("Please put values for quantity");
                        return;
                    }
                    msg = requisitionProvider.Return(requisitionDetailProviderList);
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

            else
            {
               MessageHelper.ShowAlertMessage(permisionMessage + statusMessage);
            }
        
        }        
        
    }
}