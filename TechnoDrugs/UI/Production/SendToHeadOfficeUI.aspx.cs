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
//using System.Globalization;
using ProductionModule.Provider;
using System.Globalization;

namespace TechnoDrugs.UI.Production
{
    public partial class SendToHeadOfficeUI : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //AddBlankRowTogvPurchaseProduct();
                btnSave.Enabled = true;
                txtChallanDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Now);
            }
            else
            {
                Page.ClientScript.GetPostBackEventReference(this, String.Empty);
                String eventTarget = Request["__EVENTTARGET"].IsNullOrEmpty() ? String.Empty : Request["__EVENTTARGET"];
                if (Request["__EVENTTARGET"] == "SearchPriceSetup")
                {
                    var finishedProductProvider = new FinishedProductProvider();
                    string code = Request["__EVENTARGUMENT"];
                    DataTable dt = finishedProductProvider.GetByID(code);
                    if (dt.IsNotNull())
                    {
                        try
                        {
                            for (int k = 0; k < dt.Rows.Count; k++)
                            {
                                txtChallanDate.Text = Convert.ToDateTime(dt.Rows[k]["ChallanDate"]).ToString("dd/MM/yyyy");
                                txtChallanNo.Text = dt.Rows[k]["ChallanNo"].ToString();
                                ddlDivision.SelectedValue = dt.Rows[k]["DivisionID"].ToString();
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
            Session["Value"] = ddlDivision.SelectedValue;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool msg = false;
            string message = string.Empty;
            try
            {
                FinishedProductProvider sendToHOProvider = SendFPToHOEntity();
                List<FinishedProductProvider> finishedProductProviderList = SendFPToHODetailEntityList();
                if ((finishedProductProviderList == null) || (finishedProductProviderList.Count == 0))
                {
                    MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                    return;
                }
                msg = sendToHOProvider.UpdateFPToHeadOffice(finishedProductProviderList);
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
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UI/Production/SendToHeadOfficeUI.aspx");
            this.AlertNone(lblMsg);
        }
        private void Clear()
        {
            txtChallanDate.Text = string.Format("{0:dd MMM yyyy}", DateTime.Now);
            lblMsg.InnerText = string.Empty;
            txtChallanDate.Text = string.Empty;
            ddlDivision.SelectedIndex = 0;
            //AddBlankRowTogvPurchaseProduct();
            btnSave.Enabled = true;
            gvRequisition.DataSource = null;
            gvRequisition.DataBind();
            btnAdd.Enabled = true;
        }
        //public void AddBlankRowTogvPurchaseProduct()
        //{
        //    var purchaseLedgerDetailsProviderList = new List<FinishedProductProvider> { new FinishedProductProvider() };
        //}

        protected void btnFind_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                int divisionID = ddlDivision.SelectedItem.Value.Toint();
                System.Web.UI.Page page = System.Web.HttpContext.Current.CurrentHandler as System.Web.UI.Page;
                Dictionary<string, string> columns = new Dictionary<string, string>();
                columns.Add("PriceDeclarationID", "Price Declaration Code");
                HttpContext.Current.Session[StaticInfo.SearchCriteria] = columns;
                HttpContext.Current.Session[StaticInfo.Query] = "SELECT ChallanNo AS [Challan No], CONVERT(VARCHAR(30),ChallanDate ,105) AS [Challan Date] FROM [dbo].SendFPToHeadOffice SHO WHERE SHO.DivisionID = " + divisionID + " ORDER BY ChallanDate DESC, ChallanNo DESC";
                string javaScript = string.Format("javascript:Search();");
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "OnClick", javaScript, true);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowAlertMessage(ex.ToString());
            }
        }
        protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProductProvider productProvider = new ProductProvider();
            DataSet ds = productProvider.GetDivisionwiseFinishedProduct(ddlDivision.SelectedItem.Value.Toint());
            
            ddlFinishedProduct.DataSource = null;
            ddlFinishedProduct.DataBind();

            ddlFinishedProduct.DataSource = ds;
            ddlFinishedProduct.DataBind();
            ddlFinishedProduct.Items.Insert(0, new ListItem("----------Select Product----------", "0"));
            ddlFinishedProduct.SelectedIndex = 0;
        }


        protected void btnAdd_OnClick(object sender, EventArgs e)
        {           
            this.AlertNone(lblMsg);
            ProductProvider productProvider = new ProductProvider();
            string measurementUnit = productProvider.GetMeasurementUnit(ddlFinishedProduct.SelectedValue.Toint());
            string packSize = productProvider.GetPackSizeName(ddlFinishedProduct.SelectedValue.Toint());
            string code = productProvider.GetFinishedProductCode(ddlFinishedProduct.SelectedValue.Toint());
            decimal tradePrice = productProvider.GetTradePrice(ddlFinishedProduct.SelectedValue.Toint());
            

            if (ddlFinishedProduct.SelectedValue == "")
            {
                MessageHelper.ShowAlertMessage("Select Product!");
                lblMsg.Focus();
                return;
            }
            List<FinishedProductProvider> purchaseLedgerDetailsProviderList = new List<FinishedProductProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                FinishedProductProvider obj = new FinishedProductProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                Label lblPackSize = (Label)row.FindControl("lblPackSize");
                
                Label lblBatchNo = (Label)row.FindControl("lblBatchNo");
                Label lblUnit = (Label)row.FindControl("lblUnit");
                Label lblTradePrice = (Label)row.FindControl("lblTradePrice");
                TextBox txtBatchQuantity = (TextBox)row.FindControl("txtBatchQuantity");
                Label lblTotalTradePrice = (Label)row.FindControl("lblTotalTradePrice");
                ImageButton btnAddOrDelete = (ImageButton)row.FindControl("btnDeleteSelectedRowLSE");

                if (hfRowProductID.Value == ddlFinishedProduct.SelectedValue)
                {
                    MessageHelper.ShowAlertMessage("This product already exists!");
                    return;
                }
                if (txtBatchQuantity.Text.ToDecimal() <= 0)
                {
                    MessageHelper.ShowAlertMessage("Enter Quantity!");
                    return;
                }
                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.BatchQuantity = txtBatchQuantity.Text.ToDecimal();
                obj.PackSize = lblPackSize.Text;
                obj.BatchNo = lblBatchNo.Text;
                obj.MeasurementUnitName = lblUnit.Text.ToString();
                obj.TradePrice = lblTradePrice.Text.ToDecimal();
                obj.TotalTradePrice = lblTotalTradePrice.Text.ToDecimal();

                purchaseLedgerDetailsProviderList.Add(obj);
            }
            string productName = ddlFinishedProduct.SelectedItem.Text;
            int productID = ddlFinishedProduct.SelectedValue.Toint();

            FinishedProductProvider obj2 = new FinishedProductProvider();
            DataTable dt = obj2.GetBatchWiseMFCExpDate(ddlBatchNo.SelectedItem.ToString());
            
            obj2.ProductID = productID;
            obj2.ProductName = productName;
            obj2.ProductCode = code;
            obj2.MeasurementUnitName = measurementUnit;
            obj2.PackSize = packSize;
            obj2.TradePrice = tradePrice;
            obj2.BatchNo = ddlBatchNo.SelectedItem.ToString();
            obj2.MfgDate = DateTime.ParseExact(dt.Rows[0]["MfgDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            obj2.MfgDate = DateTime.ParseExact(dt.Rows[0]["ExpDate"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            purchaseLedgerDetailsProviderList.Add(obj2);

            if (!divGridForLSE.Visible)
            {
                divGridForLSE.Visible = true;
            }
            gvRequisition.DataSource = purchaseLedgerDetailsProviderList;
            gvRequisition.DataBind();
            ddlDivision.Enabled = false;           

        }
        protected void btnDeleteSelectedRowLSE_Click(object sender, EventArgs e)
        {
            ImageButton btnDelete = sender as ImageButton;
            GridViewRow selectedRow = (GridViewRow)btnDelete.NamingContainer;
            HiddenField hfDeleteProdID = (HiddenField)selectedRow.FindControl("hfProductID");
            List<FinishedProductProvider> finishedProductDetailProviderList = new List<FinishedProductProvider>();
            foreach (GridViewRow row in gvRequisition.Rows)
            {
                FinishedProductProvider obj = new FinishedProductProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                Label lblProductCode = (Label)row.FindControl("lblProductCode");
                
                Label lblPackSize = (Label)row.FindControl("lblPackSize");

                Label lblBatchNo = (Label)row.FindControl("lblBatchNo");
                Label lblUnit = (Label)row.FindControl("lblUnit");
                Label lblTradePrice = (Label)row.FindControl("lblTradePrice");
                TextBox txtBatchQuantity = (TextBox)row.FindControl("txtBatchQuantity");
                Label lblTotalTradePrice = (Label)row.FindControl("lblTotalTradePrice");
                              

                if (hfRowProductID.Value != hfDeleteProdID.Value)
                {
                    obj.ProductID = hfRowProductID.Value.Toint();
                    obj.ProductCode = lblProductCode.Text;
                    obj.ProductName = lblProductName.Text;
                    obj.BatchQuantity = txtBatchQuantity.Text.ToDecimal();
                    obj.PackSize = lblPackSize.Text;
                    obj.BatchNo = lblBatchNo.Text;
                    obj.MeasurementUnitName = lblUnit.Text;
                    obj.TradePrice = lblTradePrice.Text.ToDecimal();
                    obj.TotalTradePrice = lblTotalTradePrice.Text.ToDecimal();

                    finishedProductDetailProviderList.Add(obj);
                }
            }
            gvRequisition.DataSource = finishedProductDetailProviderList;
            gvRequisition.DataBind();

            if (gvRequisition.Rows.Count == 0)
            {
                ddlDivision.Enabled = true;
            }
        }
        protected void gvRequisition_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
        private FinishedProductProvider SendFPToHOEntity()
        {
            FinishedProductProvider entity = null;
            entity = new FinishedProductProvider
            {
                ChallanNo = txtChallanNo.Text,
                ChallanDate = DateTime.ParseExact(txtChallanDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                DivisionID = int.Parse(ddlDivision.SelectedValue),
                EntryUserID = Convert.ToInt16(Session["ID"]),
                UpdateUserID = Convert.ToInt16(Session["ID"])
            };
            return entity;
        }
        private List<FinishedProductProvider> SendFPToHODetailEntityList()
        {
            List<FinishedProductProvider> finishedProductProviderList = new List<FinishedProductProvider>();

            foreach (GridViewRow row in gvRequisition.Rows)
            {
                FinishedProductProvider obj = new FinishedProductProvider();

                HiddenField hfRowProductID = (HiddenField)row.FindControl("hfProductID");
                Label lblProductName = (Label)row.FindControl("lblProduct");
                Label lblPackSize = (Label)row.FindControl("lblPackSize");

                Label lblBatchNo = (Label)row.FindControl("lblBatchNo");
                Label lblUnit = (Label)row.FindControl("lblUnit");
                Label lblTradePrice = (Label)row.FindControl("lblTradePrice");
                TextBox txtBatchQuantity = (TextBox)row.FindControl("txtBatchQuantity");
                Label lblTotalTradePrice = (Label)row.FindControl("lblTotalTradePrice");

                obj.ProductID = hfRowProductID.Value.Toint();
                obj.ProductName = lblProductName.Text.ToString();
                obj.BatchQuantity = txtBatchQuantity.Text.ToDecimal();
                obj.PackSize = lblPackSize.Text;
                obj.BatchNo = lblBatchNo.Text;
                obj.MeasurementUnitName = lblUnit.Text.ToString();
                obj.TradePrice = lblTradePrice.Text.ToDecimal();
                obj.TotalTradePrice = lblTotalTradePrice.Text.ToDecimal();

                if (txtBatchQuantity.Text.ToDecimal() <= 0)
                {
                    throw new Exception("Enter Quantity!");
                }

                finishedProductProviderList.Add(obj);
            }
            return finishedProductProviderList;
        }      
        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool msg = false;
            string message = string.Empty;
            string transactionNo = string.Empty;
            try
            {
                FinishedProductProvider sendToHOProvider = SendFPToHOEntity();
                List<FinishedProductProvider> finishedProductProviderList = SendFPToHODetailEntityList();
                if ((finishedProductProviderList == null) || (finishedProductProviderList.Count == 0))
                {
                    MessageHelper.ShowAlertMessage("Please select at least one product for purchase");
                    return;
                }
                msg = sendToHOProvider.SendFPToHeadOffice(finishedProductProviderList, out transactionNo);
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            if (msg)
            {
                Clear();
                txtChallanNo.Text = transactionNo;
                this.AlertSuccess(lblMsg, MessageConstants.Saved);
            }
            else
            {
                MessageHelper.ShowAlertMessage(message);
            }


        }

        protected void ddlFinishedProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            FinishedProductProvider finishedProductProvider = new FinishedProductProvider();
            //string[] batchNo = ddlBatchNo.SelectedItem.ToString().Split(' ');
            DataTable dt = finishedProductProvider.GetFGWiseBatch(ddlFinishedProduct.SelectedValue.Toint());

            ddlBatchNo.DataSource = null;
            ddlBatchNo.DataBind();

            ddlBatchNo.DataSource = dt;
            ddlBatchNo.DataBind();
            ddlBatchNo.Items.Insert(0, new ListItem("----------Select Product----------", "0"));
            ddlBatchNo.SelectedIndex = 0;            
        }
    }
}