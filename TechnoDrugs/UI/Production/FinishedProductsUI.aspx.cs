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
using System.Collections;


namespace TechnoDrugs.UI.Production
{
    public partial class FinishedProductsUI : PageBase
    {
        #region properties
        string statusMessage = string.Empty;
        string refNo = string.Empty;
        string mode = "";
        public FinishedProductsUI()
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
                txtReceivedDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                btnAddNewChallan.Visible = false;
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var requisitionProvider = new FinishedProductProvider();
                    string batchNRefNo = Request["__EVENTARGUMENT"];
                    char[] splitchar = { ',' };

                    string[] batchNRefNoSpliting = batchNRefNo.Split(splitchar);
                    string batchNo = batchNRefNoSpliting[0].ToString();
                    refNo = batchNRefNoSpliting[1].ToString();
                    DataTable dt = requisitionProvider.GetByBatchNRef(batchNo, refNo);
                    if (dt.IsNotNull())
                    {
                        PopulateControls(dt);
                    }
                }
            }
            Session["Value"] = ddlDivision.SelectedValue;
        }
        #endregion
        
        #region methods
        
        private void PopulateControls(DataTable dt)
        {
            try
            {
                decimal totalComPackReceived = 0.0M;
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    ddlFinishedProduct.SelectedValue = dt.Rows[k]["FinishedProductID"].ToString();
                    Session["PackSizeConv"] = Convert.ToInt16(dt.Rows[k]["PackSizeConv"]);
                    if (dt.Rows[k]["ReceivedDate"] == DBNull.Value)
                    {
                        txtReceivedDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
                        txtReceivedDate.Text = string.Empty;
                        txtActualYield.Text = string.Empty;
                        ddlActualYieldUnit.SelectedIndex = 0;
                        txtCommPackReceived.Text = string.Empty;
                        txtCommPackReceived.Enabled = true;
                        txtReferenceNo.Text = string.Empty;
                        lblTotalReceivedAmount.Text = string.Empty;
                        lblActualYieldCalAmount.Text = string.Empty;
                        btnAddNewChallan.Visible = false;
                        //btnUpdate.Visible = false;
                        btnReceived.Visible = true;
                        txtActualYield.Enabled = true;
                    }
                    else
                    {
                        if (dt.Rows[k]["ReferenceNo"].ToString() == refNo)
                        {
                            txtReceivedDate.Text = Convert.ToDateTime(dt.Rows[k]["ReceivedDate"]).ToString("dd/MM/yyyy");
                            txtActualYield.Text = dt.Rows[k]["ActualYield"].ToString();
                            txtCommPackReceived.Text = dt.Rows[k]["CommercialPackRec"].ToString();
                            txtCommPackReceived.Enabled = false;
                            txtReferenceNo.Text = dt.Rows[k]["ReferenceNo"].ToString();
                            ddlActualYieldUnit.SelectedValue = dt.Rows[k]["ActualYieldUnit"].ToString();
                            //btnUpdate.Visible = true;
                            btnReceived.Visible = false;
                            btnAddNewChallan.Visible = true;
                            txtActualYield.Enabled = false;
                        }
                    }
                    txtMfgDate.Text = Convert.ToDateTime(dt.Rows[k]["MfgDate"]).ToString("dd/MM/yyyy");
                    txtExpDate.Text = Convert.ToDateTime(dt.Rows[k]["ExpDate"]).ToString("dd/MM/yyyy");
                    lblPackSize.Text = dt.Rows[k]["PackSize"].ToString();
                    lblMeasurementUnit.Text = dt.Rows[k]["MeasurementUnitName"].ToString();
                   // txtRefNo.Text = dt.Rows[k]["ReferenceNo"].ToString();
                    ddlDivision.SelectedValue = dt.Rows[k]["DivisionID"].ToString();
                   // ckbOption.SelectedValue = dt.Rows[k]["StatusID"].ToString();
                    txtBatchNo.Text = dt.Rows[k]["BatchNo"].ToString();
                    txtBatchSize.Text = dt.Rows[k]["BatchSize"].ToString();
                    txtTheoriticalYield.Text = dt.Rows[k]["TheoriticalYield"].ToString();
                    decimal comPackReceived = Convert.ToDecimal(dt.Rows[k]["CommercialPackRec"]);
                    totalComPackReceived = totalComPackReceived + comPackReceived;
                    lblTotalReceivedAmount.Text = totalComPackReceived.ToString();
                    int packSizeResult = CalculatePackSize();
                    decimal actualYield = lblTotalReceivedAmount.Text.ToDecimal() * packSizeResult;
                    lblActualYieldCalAmount.Text = actualYield.ToString();
                    Session["TotalReceivedAmount"] = lblTotalReceivedAmount.Text.ToDecimal();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public int CalculatePackSize()
        {
            string txtInputVal = "";
            char chrs = 'X';
            txtInputVal = lblPackSize.Text.ToString();

            if (txtInputVal.Contains("X") == true)
            {
                string[] findNum = txtInputVal.Split(chrs);
                ArrayList arrOfFindNum = new ArrayList();
                for (int i = 0; i < findNum.Length; i++)
                {
                    string chrtrs = "";
                    string selectedFromfindNum = findNum[i].ToString();
                    for (int k = 0; k < selectedFromfindNum.Length; k++)
                    {
                        string txtstr = selectedFromfindNum[k].ToString();
                        if (txtstr.ToCharArray().Any(c => Char.IsDigit(c)))
                        {
                            chrtrs = chrtrs + selectedFromfindNum[k];
                        }
                    }
                    arrOfFindNum.Add(chrtrs);
                }

                int multplyVal = 1;
                for (int j = 0; j < arrOfFindNum.Count; j++)
                {
                    multplyVal = multplyVal * Convert.ToInt32(arrOfFindNum[j]);
                }
                return multplyVal;

                //lblShow.Text = multplyVal.ToString();
            }
            else
            {
                string chrtrs = "";
                for (int r = 0; r < txtInputVal.Length; r++)
                {
                    string txtValToArr = txtInputVal[r].ToString();
                    if (txtValToArr.ToCharArray().Any(c => Char.IsDigit(c)))
                    {

                        chrtrs = chrtrs + txtInputVal[r];
                    }
                }
                //lblShow.Text = chrtrs.ToString();
                return chrtrs.Toint();
            }
        }            
        public void AddBlankRowTogvPurchaseProduct()
        {
            var purchaseLedgerDetailsProviderList = new List<ProductionRequisitionDetailProvider> { new ProductionRequisitionDetailProvider() };           
        }
       
        private void Clear()
        {
            txtReceivedDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;  
            txtReceivedDate.Text = string.Empty;            
            ddlDivision.SelectedIndex = 0;           
            AddBlankRowTogvPurchaseProduct();
            ddlDivision.Enabled = true;                      
        }
      
        private FinishedProductProvider RequisitionInfoEntity()
        {
            FinishedProductProvider entity = null;
            entity = new FinishedProductProvider
            {
                ReferenceNo = txtReferenceNo.Text,
                BatchNo = txtBatchNo.Text,
                TheoriticalYield = txtTheoriticalYield.Text,
                ActualYield = txtActualYield.Text.ToDecimal(),
                ActualYieldUnit = ddlActualYieldUnit.SelectedValue,
                CommercialPack = txtCommPackReceived.Text.ToDecimal(),
                BatchSize = txtBatchSize.Text,
                ReceivedDate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                MfgDate = DateTime.ParseExact(txtMfgDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ExpDate = DateTime.ParseExact(txtExpDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture), 
                DivisionID = int.Parse(ddlDivision.SelectedValue),
                ProductID = int.Parse(ddlFinishedProduct.SelectedValue),
                EntryUserID = Convert.ToInt16( Session["ID"])
            };
            return entity;
        }
        #endregion

        #region button event
        
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
                    FinishedProductProvider requisitionProvider = RequisitionInfoEntity();
                    msg = requisitionProvider.Update();
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            //Clear();
            Response.Redirect("~/UI/Production/FinishedProductsUI.aspx");
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
                int divisionID = ddlDivision.SelectedItem.Value.Toint();
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Price Declaration Code");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "SELECT  PR.BatchNO [Batch No], FPAF.ReferenceNo, CONVERT(VARCHAR, RequisitionDate, 103) [Requisition Date] FROM [dbo].ProductionRequisition PR INNER JOIN Product P ON PR.FinishedProductID = P.ID LEFT JOIN FinishedProductAfterProduction FPAF ON PR.BatchNo = FPAF.BatchNo WHERE P.DivisionID = " + divisionID + "";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }

        protected void btnReceived_Click(object sender, EventArgs e)
        {
            mode = "Save";

            string permisionMessage = CheckUserAuthentication(mode);
            //if (ckbOption.SelectedValue != "")
            //{
            //    statusMessage = CheckUserAuthentication(ckbOption.SelectedItem.ToString());
            //}
            if (String.IsNullOrEmpty(permisionMessage) && string.IsNullOrEmpty(statusMessage))
            {
                bool msg = false;
                string message = string.Empty;
                string transactionNo = string.Empty;
                try
                {
                    FinishedProductProvider requisitionProvider = RequisitionInfoEntity();
                    msg = requisitionProvider.Save(out transactionNo);
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                }
                if (msg)
                {
                    Clear();
                    txtReferenceNo.Text = transactionNo;
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

        protected void btnAddNewChallan_Click(object sender, EventArgs e)
        {
           // btnUpdate.Visible = false;
            btnReceived.Visible = true;
            txtCommPackReceived.Text = string.Empty;
            txtCommPackReceived.Enabled = true;
           // txtActualYield.Text = string.Empty;
            txtReceivedDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
        }

        protected void txtCommPackReceived_TextChanged(object sender, EventArgs e)
        {
            if (txtReferenceNo.Text == "")
            {
                lblTotalReceivedAmount.Text = txtCommPackReceived.Text;
            }
            else
            {
                lblTotalReceivedAmount.Text = Convert.ToString(Convert.ToDecimal(Session["TotalReceivedAmount"]) + txtCommPackReceived.Text.ToDecimal());
            }            
            //int packSizeResult = CalculatePackSize();
            decimal actualYield = lblTotalReceivedAmount.Text.ToDecimal() * Convert.ToInt16(Session["PackSizeConv"]);
            lblActualYieldCalAmount.Text = actualYield.ToString();
        }        
    }
}