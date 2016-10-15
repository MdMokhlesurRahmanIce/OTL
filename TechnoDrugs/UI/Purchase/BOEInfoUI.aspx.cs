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
    public partial class BOEInfoUI : PageBase
    {
       #region properties
        string mode = "";
        public BOEInfoUI()
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
                ddlPreviousBOE.Items.Add(new ListItem("----------Select----------", "0", true));
                ddlPreviousBOE.DataSource = null;
                ddlPreviousBOE.DataBind();
                txtBOEDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);    
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var BOEProvider = new BOEProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = BOEProvider.GetByID(code);

                    DataTable bOE = BOEProvider.GetBOENumber(code);

                    if (dt.IsNotNull() && dt.Rows.Count > 0)
                    {
                        lblMsg.InnerText = string.Empty;
                        PopulateControls(dt);
                        gvPurchaseOrder.DataSource = dt;
                        gvPurchaseOrder.DataBind();

                        gvTaxInformation.Visible = false;
                        btnSave.Visible = false;
                        btnUpdate.Visible = false;
                        btnClear.Visible = false;
                    }
                    else
                    {
                        txtRefLCNumber.Text = code;
                        MessageHelper.ShowAlertMessage("No BOE found. Please select new BOE to enter one.");
                    }
                    if (bOE.Rows.Count > 0)
                    {
                        ddlPreviousBOE.Items.Clear();
                        ddlPreviousBOE.Items.Add(new ListItem("----------Select----------", "0", true));
                        ddlPreviousBOE.DataSource = bOE;
                        ddlPreviousBOE.DataBind();
                    }
                    else
                    {
                        ddlPreviousBOE.Items.Clear();
                        ddlPreviousBOE.Items.Add(new ListItem("----------Select----------", "0", true));
                        ddlPreviousBOE.DataSource = null;
                        ddlPreviousBOE.DataBind();
                        btnSave.Visible = true;
                        btnUpdate.Visible = false;
                    }
                }
            }
        }
        #endregion
       
       #region methods
        private void RowsIn_gvPurchaseOrder()
        {
            List<BOEDetailProvider> boeDetailsProviderList = new List<BOEDetailProvider>();
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                BOEDetailProvider obj = new BOEDetailProvider();
                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtActualQuantity = (TextBox)row.FindControl("txtActualQuantity");
                TextBox txtInvoiceQuantity = (TextBox)row.FindControl("txtInvoiceQuantity");
                TextBox txtRemainingQuantity = (TextBox)row.FindControl("txtRemainingQuantity");
                TextBox txtHSCode = (TextBox)row.FindControl("txtHSCode");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");    
                
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.HSCode = txtHSCode.Text;
                obj.ActualQuantity = txtActualQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.InvoiceQuantity = txtInvoiceQuantity.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                obj.RemainingQuantity = txtActualQuantity.Text.ToDecimal() - txtInvoiceQuantity.Text.ToDecimal();
                if (txtInvoiceQuantity.Text.ToDecimal() > 0.0M)
                {
                    obj.InvoiceValue = txtInvoiceQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                }
                else
                {
                    obj.InvoiceValue = txtActualQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                }
                boeDetailsProviderList.Add(obj);
            }
            gvPurchaseOrder.DataSource = boeDetailsProviderList;
            gvPurchaseOrder.DataBind();           
        }

        private void PopulateControls(DataTable dt)
        {
            try
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    txtSystemLCNo.Text = dt.Rows[k]["SystemLCNo"].ToString();
                    txtRefLCNumber.Text = dt.Rows[k]["BankLCNumber"].ToString();
                    txtRefLCDate.Text = Convert.ToDateTime(dt.Rows[k]["LCOpeningDate"]).ToString("dd/MM/yyyy");              
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
            
            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var boeDetailsProviderList = new List<BOEDetailProvider> { new BOEDetailProvider() };           
        }       
        private void Clear()
        {
            lblMsg.InnerText = string.Empty;
            txtBOENumber.Text = string.Empty;
            txtExcRate.Text = string.Empty;
            txtBOEDate.Text = string.Empty;                
            AddBlankRowTogvPurchaseProduct();
            ddlPreviousBOE.SelectedIndex = 0;
            btnSave.Enabled = true;
            gvPurchaseOrder.DataSource = null;
            gvPurchaseOrder.DataBind();

            gvTaxInformation.DataSource = null;
            gvTaxInformation.DataBind();
        }      
        private BOEProvider boeInfoEntity()
        {
            BOEProvider entity = null;
            entity = new BOEProvider
            {
                SystemLCNo = txtSystemLCNo.Text,
                BOENumber = txtBOENumber.Text,
                ExcRate = txtExcRate.Text.ToDecimal(),
                BOEDate = DateTime.ParseExact(txtBOEDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EntryUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<BOEDetailProvider> boeDetailEntityList()
        {
            List<BOEDetailProvider> boeDetailsProviderList = new List<BOEDetailProvider>();

            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                BOEDetailProvider obj = new BOEDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                TextBox txtHSCode = (TextBox)row.FindControl("txtHSCode");
                TextBox txtInvoiceQuantity = (TextBox)row.FindControl("txtInvoiceQuantity");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtActualQuantity = (TextBox)row.FindControl("txtActualQuantity");
                TextBox txtRemainingQuantity = (TextBox)row.FindControl("txtRemainingQuantity");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.HSCode = txtHSCode.Text;
                obj.ActualQuantity = txtActualQuantity.Text.ToDecimal();
                obj.Rate = txtRate.Text.ToDecimal();
                obj.InvoiceQuantity = txtInvoiceQuantity.Text.ToDecimal();
                obj.Unit = txtUnit.Text;
                obj.RemainingQuantity = txtActualQuantity.Text.ToDecimal() - txtInvoiceQuantity.Text.ToDecimal();
                if (txtInvoiceQuantity.Text.ToDecimal() > 0.0M)
                {
                    obj.InvoiceValue = txtInvoiceQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                }
                else
                {
                    obj.InvoiceValue = txtActualQuantity.Text.ToDecimal() * txtRate.Text.ToDecimal();
                }                
                if(obj.InvoiceQuantity > 0 && obj.HSCode !=string.Empty)
                boeDetailsProviderList.Add(obj);
            }
            return boeDetailsProviderList;           
        }

        private List<TAXInfoProvider> taxInfoList()
        {
            List<TAXInfoProvider> taxInfoList = new List<TAXInfoProvider>();

            foreach (GridViewRow row in gvTaxInformation.Rows)
            {
                TAXInfoProvider obj = new TAXInfoProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtAssessmentValue = (TextBox)row.FindControl("txtAssessmentValue");
                TextBox txtCDPerc = (TextBox)row.FindControl("txtCDPerc");
                TextBox txtCDAmt = (TextBox)row.FindControl("txtCDAmt");
                TextBox txtVATPerc = (TextBox)row.FindControl("txtVATPerc");
                TextBox txtVATAmt = (TextBox)row.FindControl("txtVATAmt");
                TextBox txtAITPerc = (TextBox)row.FindControl("txtAITPerc");
                TextBox txtAITAmt = (TextBox)row.FindControl("txtAITAmt");
                TextBox txtATVPerc = (TextBox)row.FindControl("txtATVPerc");
                TextBox txtATVAmt = (TextBox)row.FindControl("txtATVAmt");
                TextBox txtSDPerc = (TextBox)row.FindControl("txtSDPerc");
                TextBox txtSDAmt = (TextBox)row.FindControl("txtSDAmt");
                TextBox txtRDPerc = (TextBox)row.FindControl("txtRDPerc");
                TextBox txtRDAmt = (TextBox)row.FindControl("txtRDAmt");
                TextBox txtDFCVATFPAmt = (TextBox)row.FindControl("txtDFCVATFPAmt");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.AssessmentValue = txtAssessmentValue.Text.ToDecimal();
                obj.CDPerc = txtCDPerc.Text.ToDecimal();
                obj.CDAmt = txtCDAmt.Text.ToDecimal();
                obj.VATPerc = txtVATPerc.Text.ToDecimal();
                obj.VATAmt = txtVATAmt.Text.ToDecimal();
                obj.AITPerc = txtAITPerc.Text.ToDecimal();
                obj.AITAmt = txtAITAmt.Text.ToDecimal();
                obj.ATVPerc = txtATVPerc.Text.ToDecimal();
                obj.ATVAmt = txtATVAmt.Text.ToDecimal();
                obj.SDPerc = txtSDPerc.Text.ToDecimal();
                obj.SDAmt = txtSDAmt.Text.ToDecimal();
                obj.RDPerc = txtRDPerc.Text.ToDecimal();
                obj.RDAmt = txtRDAmt.Text.ToDecimal();
                obj.DFCVATFPAmt = txtDFCVATFPAmt.Text.ToDecimal();
                if(obj.AssessmentValue > 0)
                taxInfoList.Add(obj);
            }
            return taxInfoList;         
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
                BOEProvider boeProvider = boeInfoEntity();
                List<BOEDetailProvider> boeDetailList = boeDetailEntityList();
                List<TAXInfoProvider> taxInfoProviderList = taxInfoList();
                if (boeProvider.ExcRate <= 0)
                {
                    MessageHelper.ShowAlertMessage("Please input exchange rate for product(s)");
                    return;
                }
                if ((boeDetailList == null) || (boeDetailList.Count == 0))  //// check this validation later
                {
                    MessageHelper.ShowAlertMessage("Please input data for product(s)");
                    return;
                }
                if (taxInfoProviderList.Count == 0)  //// check this validation later
                {
                    MessageHelper.ShowAlertMessage("Please input tax values for product(s)");
                    return;
                }
                msg = boeProvider.Save(boeDetailList, taxInfoProviderList, out transactionNo);
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
            Response.Redirect("~/UI/Purchase/BOEInfoUI.aspx");
            this.AlertNone(lblMsg);
        }
        protected void txtInvoiceQuantity_TextChanged(object sender, EventArgs e)
        {
            RowsIn_gvPurchaseOrder();
        }        
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<BOEDetailProvider> boeDetailsProviderList = new List<BOEDetailProvider>();
            foreach (GridViewRow row in gvPurchaseOrder.Rows)
            {
                BOEDetailProvider obj = new BOEDetailProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                TextBox txtValue = (TextBox)row.FindControl("txtValue");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                TextBox txtActualQuantity = (TextBox)row.FindControl("txtActualQuantity");
                TextBox txtInvoiceQuantity = (TextBox)row.FindControl("txtInvoiceQuantity");
                TextBox txtRemainingQuantity = (TextBox)row.FindControl("txtRemainingQuantity");
                TextBox txtRate = (TextBox)row.FindControl("txtRate");
                TextBox txtUnit = (TextBox)row.FindControl("txtUnit");
                
                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductName = lblProductName.Text.ToString();
                    obj.ActualQuantity = txtActualQuantity.Text.ToDecimal();

                    obj.InvoiceValue = txtValue.Text.ToDecimal();
                    obj.Rate = txtRate.Text.ToDecimal();
                    obj.Unit = txtUnit.Text.ToString();
                 
                    boeDetailsProviderList.Add(obj);
                }
            }
            gvPurchaseOrder.DataSource = boeDetailsProviderList;
            gvPurchaseOrder.DataBind();
            if (gvPurchaseOrder.Rows.Count == 0)
            {
                //ddlRequistionRef.Enabled = true;
            }
        }
        
        #endregion
        
        protected void gvPurchaseOrder_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
        }
        protected void gvTaxInformation_OnRowDataBound(object sender, GridViewRowEventArgs e)
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
                HttpContext.Current.Session[StaticInfo.Query] = "Select BankLCNumber AS [Bank LC Number], Convert(VARCHAR(30), LCOpeningDate, 105) AS [Opening Date] from [dbo].LCChallan ORDER BY LCOpeningDate DESC";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            mode = "Update";
            CheckUserAuthentication(mode);

            bool msg = false;
            string message = string.Empty;
            try
            {
                BOEProvider boeProvider = boeInfoEntity();
                List<BOEDetailProvider> boeDetailList = boeDetailEntityList();
                List<TAXInfoProvider> taxInfoProviderList = taxInfoList();
                if ((boeDetailList == null) || (boeDetailList.Count == 0))
                {
                    MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                    return;
                }
                msg = boeProvider.Update(boeDetailList, taxInfoProviderList);
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

        protected void ddlPreviousBOE_SelectedIndexChanged(object sender, EventArgs e)
        {
            string lcNo = txtSystemLCNo.Text;
            if (ddlPreviousBOE.SelectedIndex == 0)
            {
                throw new Exception("Please select a boe number.");
            }
            string bOENumber = ddlPreviousBOE.SelectedItem.ToString();
            var BOEProvider = new BOEProvider();
            DataTable dt = BOEProvider.GetByLCAndBOENumber(lcNo, bOENumber);

            if (dt.IsNotNull())
            {
                txtBOENumber.Enabled = false;
                txtSystemLCNo.Text = dt.Rows[0]["SystemLCNo"].ToString();
                txtRefLCNumber.Text = dt.Rows[0]["BankLCNumber"].ToString();
                txtExcRate.Text = dt.Rows[0]["ExcRate"].ToString();
                txtBOENumber.Text = dt.Rows[0]["BOENumber"].ToString();
                txtRefLCDate.Text = Convert.ToDateTime(dt.Rows[0]["LCOpeningDate"]).ToString("dd/MM/yyyy");
                txtBOEDate.Text = Convert.ToDateTime(dt.Rows[0]["BOEDate"]).ToString("dd/MM/yyyy");   

                gvPurchaseOrder.DataSource = dt;
                gvPurchaseOrder.DataBind();

                gvTaxInformation.Visible = true;
                gvTaxInformation.DataSource = dt;
                gvTaxInformation.DataBind();
            }
            btnSave.Visible = false;
            btnUpdate.Visible = true;    
        }

        protected void btnNewBOE_Click(object sender, EventArgs e)
        {
            Clear();
            var BOEProvider = new BOEProvider();
            if (txtRefLCNumber.Text == string.Empty)
            {
                MessageHelper.ShowAlertMessage("Please select reference LC number.");
            }
            DataTable dt = BOEProvider.GetDataForNewBOE(txtRefLCNumber.Text);

            if (dt.IsNotNull())
            {
                txtBOENumber.Enabled = true;
                PopulateControls(dt);
                gvPurchaseOrder.DataSource = dt;
                gvPurchaseOrder.DataBind();

                gvTaxInformation.Visible = true;
                gvTaxInformation.DataSource = dt;
                gvTaxInformation.DataBind();                
            }
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
    }
}