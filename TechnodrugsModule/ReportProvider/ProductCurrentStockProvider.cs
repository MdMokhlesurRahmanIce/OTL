using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnodrugsModule.ReportDataAccess;

namespace TechnodrugsModule.ReportProvider
{
    public class ProductCurrentStockProvider
    {
        #region Property

        public string BatchNo
        {
            get;
            set;
        }
        public string BillEntryNo
        {
            get;
            set;
        }

        public string TransactionSerialNo
        {
            get;
            set;
        }
        public string TransactionDate
        {
            get;
            set;
        }
        public string TransactionDateTime
        {
            get;
            set;
        }
        public decimal OpeningQuantity
        {
            get;
            set;
        }
        public string SupplierName
        {
            get;
            set;
        }
        public int ProductID
        {
            get;
            set;
        }
        public int DivisionID
        {
            get;
            set;
        }
        public int ProductType
        {
            get;
            set;
        }
        public string ProductName
        {
            get;
            set;
        }
        public string FromDate
        {
            get;
            set;
        }
        public string Todate
        {
            get;
            set;
        }
        public decimal PurchaseQuantity
        {
            get;
            set;
        }
        public string DeliveryChallanNo
        {
            get;
            set;
        }

        public decimal StockInQunatity
        {
            get;
            set;
        }
        public decimal OtherReceive
        {
            get;
            set;
        }
        public decimal StockOutQuantity
        {
            get;
            set;
        }
        public decimal BrokenOrDamageQty
        {
            get;
            set;
        }
        public decimal OtherOutQty
        {
            get;
            set;
        }
        public decimal ClosingQuantity
        {
            get;
            set;
        }
        public string Remarks
        {
            get;
            set;
        }
        public string Unit
        {
            get;
            set;
        }



        #endregion

        public ProductCurrentStockProvider()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public List<ProductCurrentStockProvider> GetAll()
        {
            var dataAccess = new ProductCurrentStockDataAccess();
            try
            {
                var pcsReportList = new List<ProductCurrentStockProvider>();
                var dt = dataAccess.GetAll(this);
                decimal totalQuantity = 0;
                bool first = true;
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        var productCurrentStockProvider = new ProductCurrentStockProvider
                        {
                            FromDate = FromDate,
                            Todate = Todate,
                            DeliveryChallanNo = row["DeliveryChallanNo"].ToString(),

                            TransactionDate = row["TransactionDate"].ToString(),
                            BatchNo = row["BatchNo"].ToString(),
                            ProductID = int.Parse(row["ProductID"].ToString()),
                            ProductName = row["ProductName"].ToString(),
                            SupplierName = row["SupplierName"].ToString(),
                            StockInQunatity = decimal.Parse(row["StockInQunatity"].ToString()),
                            StockOutQuantity = decimal.Parse(row["StockOutQuantity"].ToString()),
                            BrokenOrDamageQty = decimal.Parse(row["BrokenOrDamageQty"].ToString()),
                            OtherOutQty = decimal.Parse(row["OtherOutQty"].ToString()),
                            OtherReceive = decimal.Parse(row["OtherReceive"].ToString()),
                            ClosingQuantity = decimal.Parse(row["ClosingQuantity"].ToString())                            
                        };
                        if (first)
                        {
                            productCurrentStockProvider.Unit = (row["Unit"].ToString());

                            productCurrentStockProvider.OpeningQuantity = decimal.Parse(row["OpeningQuantity"].ToString());
                            productCurrentStockProvider.ClosingQuantity = decimal.Parse(row["OpeningQuantity"].ToString());
                            totalQuantity = decimal.Parse(row["OpeningQuantity"].ToString());
                            first = false;
                        }
                        else
                        {
                            productCurrentStockProvider.OpeningQuantity = totalQuantity;
                            
                            totalQuantity = productCurrentStockProvider.OpeningQuantity + productCurrentStockProvider.ClosingQuantity;
                            productCurrentStockProvider.ClosingQuantity = totalQuantity;
                        }
                        pcsReportList.Add(productCurrentStockProvider);
                    }
                }
                return pcsReportList;
            }
            catch (Exception exp)
            {
                throw new Exception(exp.Message);
            }
        }
        public DataTable GetDivisionAndItemwise()
        {
            var dataAccess = new ProductCurrentStockDataAccess();
            return dataAccess.GetDivisionAndItemwise(this);
        }
    }
}
