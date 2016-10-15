using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseModule
{
    public class StoredProcedureNames
    {
        #region Menu
        public const string MenuGet = "[UserAccess].[MenuGet]";
        #endregion

        #region UserGroup
        public const string UserGroupGet = "[UserAccess].[UserGroupGet]";
        public const string UserGroupSet = "[UserAccess].[UserGroupSet]";
        #endregion

        #region User
        public const string UserGet = "[UserAccess].[UserGet]";
        public const string UserSet = "[UserAccess].[UserSet]";
        #endregion

        #region LogFile
        public const string LogFileSet = "[UserAccess].[LogFileSet]";
        public const string LogFileGet = "[UserAccess].[LogFileGet]";
        #endregion

        #region GroupMenuPreviliges
        public const string UserPageControlsGet = "[UserAccess].[RoleDetailGet]";
        public const string UserPageControlsSet = "[UserAccess].[RoleDetailSet]";
        #endregion       

        #region Production
        public const string ProductionGet = "[dbo].[ProductionGET]";
        public const string ProductionSet = "[dbo].[ProductionSET]";
        #endregion                          

        #region ProductType
        public const string ProductTypeGet = "[dbo].[ProductTypeGet]";
        public const string ProductTypeSet = "[dbo].[ProductTypeSet]";
        #endregion                

        #region Dvision
        public const string DivisionGet = "[dbo].[DivisionGet]";
        public const string DivisionSet = "[dbo].[DivisionSet]";
        #endregion             
        
        #region Product
        public const string ProductGet = "[dbo].[ProductGet]";
        public const string ProductSet = "[dbo].[ProductSet]";
        #endregion          

        #region Product Stock
        public const string GetProductCurrentStock = "[Inventory].[GetProductCurrentStock]";
        public const string GetAllTypeProductStock = "[Inventory].[GetAllTypeProductStock]";
        public const string GetAllProductSummaryStock = "[Inventory].[GetAllProductSummaryStock]";
        public const string ProductStockSet = "[Inventory].[ProductStockSet]";
        public const string ProductStockDetailSet = "[Inventory].[ProductStockDetailSet]";
        public const string ProductRetRejDamageSet = "[Inventory].[ProductRetRejDamageSet]";
        public const string ProductRetRejDamageDetailSet = "[Inventory].[ProductRetRejDamageDetailSet]";
        public const string ProductOtherOutSet = "[Inventory].[ProductOtherOutSet]";
        public const string ProductOtherOutDetailSet = "[Inventory].[ProductOtherOutDetailSet]";                
        #endregion          
  
        #region Product Pack Size
        public const string ProductPackSizeGet = "[dbo].[ProductPackSizeGet]";
        public const string ProductPackSizeSet = "[dbo].[ProductPackSizeSet]";
        #endregion

        #region Suppliers
        public const string SuppliersGet = "[dbo].[SuppliersGet]";
        public const string SuppliersSet = "[dbo].[SuppliersSet]";
        public const string SupplierTypeGet = "[dbo].[SupplierTypeGet]";
        #endregion

        #region MeasurementUnit
        public const string MeasurementUnitSet = "[dbo].[MeasurementUnitSet]";
        public const string MeasurementUnitGet = "[dbo].[MeasurementUnitGet]";
        #endregion
      
        #region Requisition
        public const string RequisitionGet = "[dbo].[RequisitionGet]";
        public const string RequisitionSet = "[dbo].[RequisitionSet]";
        #endregion

        #region Damaged Product
        public const string DamagedProductSet = "[dbo].[DamagedProductSet]";
        public const string DamagedProductGet = "[dbo].[DamagedProductGet]";
        #endregion

        #region Other Out Product
        public const string OtherOutProductSet = "[dbo].[OtherOutProductSet]";
        public const string OtherOutProductGet = "[dbo].[OtherOutProductGet]";
        #endregion

        #region SendFPToHo
        public const string SendFPToHoGet = "[dbo].[SendFPToHoGet]";
        public const string SendFPToHoSet = "[dbo].[SendFPToHoSet]";
        public const string SendFPToHoDetailSet = "[dbo].[SendFPToHoDetailSet]";
        #endregion
        
        #region RequisitionDetail
        //public const string RequisitionDetailGet = "[dbo].[PurchaseLedgerDetailGet]";
        public const string RequisitionDetailSet = "[dbo].[RequisitionDetailSet]";
        #endregion

        #region Damaged Product Detail
        //public const string RequisitionDetailGet = "[dbo].[PurchaseLedgerDetailGet]";
        public const string DamagedProductDetailSet = "[dbo].[DamagedProductDetailSet]";
        #endregion

        #region Damaged Product Detail
        //public const string RequisitionDetailGet = "[dbo].[PurchaseLedgerDetailGet]";
        public const string OtherOutProductDetailSet = "[dbo].[OtherOutProductDetailSet]";
        #endregion

        #region Requisition Report
        public const string RequisitionReport = "[Report].[RequisitionReport]";
        #endregion

        #region Production Requisition
        public const string ProductionRequisitionGet = "[dbo].[ProductionRequisitionGet]";
        public const string ProductionRequisitionSet = "[dbo].[ProductionRequisitionSet]";
        public const string ProductionRetRejSet = "[dbo].[ProductionRetRejSet]";
        #endregion

        #region Finished Products
        public const string FinishedProductGet = "[dbo].[FinishedProductGet]";
        public const string FinishedProductSet = "[dbo].[FinishedProductSet]";
        #endregion

        #region Production Requisition Detail
        public const string ProductionRequisitionDetailGet = "[dbo].[ProductionRequisitionDetailGet]";
        public const string ProductionRequisitionDetailSet = "[dbo].[ProductionRequisitionDetailSet]";
        #endregion

        #region QAQC Requisition
        public const string QAQCRequisitionGet = "[dbo].[QAQCRequisitionGet]";
        public const string QAQCRequisitionSet = "[dbo].[QAQCRequisitionSet]";
        #endregion

        #region QAQC Requisition Detail
        public const string QAQCRequisitionDetailGet = "[dbo].[QAQCRequisitionDetailGet]";
        public const string QAQCRequisitionDetailSet = "[dbo].[QAQCRequisitionDetailSet]";
        #endregion

        #region Engineering Requisition
        public const string EngineeringRequisitionGet = "[dbo].[EngineeringRequisitionGet]";
        public const string EngineeringRequisitionSet = "[dbo].[EngineeringRequisitionSet]";
        #endregion

        #region Engineering Requisition Detail
        public const string EngineeringRequisitionDetailGet = "[dbo].[EngineeringRequisitionDetailGet]";
        public const string EngineeringRequisitionDetailSet = "[dbo].[EngineeringRequisitionDetailSet]";
        #endregion
                
        #region Purchase Order
        public const string PurchaseOrderGet = "[dbo].[PurchaseOrderGet]";
        public const string PurchaseOrderSet = "[dbo].[PurchaseOrderSet]";
        #endregion        

        #region Purchase Order Detail
        public const string PurchaseOrderDetailGet = "[dbo].[PurchaseOrderDetailGet]";
        public const string PurchaseOrderDetailSet = "[dbo].[PurchaseOrderDetailSet]";
        #endregion

        #region LC Challan
        public const string LCChallanGet = "[dbo].[LCChallanGet]";
        public const string LCChallanSet = "[dbo].[LCChallanSet]";
        #endregion

        #region LC Challan Detail
        public const string LCChallanDetailGet = "[dbo].[LCChallanDetailGet]";
        public const string LCChallanDetailSet = "[dbo].[LCChallanDetailSet]";
        public const string LCAmendmentSet = "[dbo].[LCAmendmentSet]";
        #endregion

        #region BOE Challan
        public const string BOEChallanGet = "[dbo].[BOEChallanGet]";
        public const string BOEChallanSet = "[dbo].[BOEChallanSet]";
        #endregion

        #region BOE Challan Detail
        public const string BOEChallanDetailGet = "[dbo].[BOEChallanDetailGet]";
        public const string BOEChallanDetailSet = "[dbo].[BOEChallanDetailSet]";
        public const string TAXInfoSet = "[dbo].[TAXInfoSet]";
        #endregion
        
        #region Delivery Challan
        public const string DeliveryChallanGet = "[dbo].[DeliveryChallanGet]";
        public const string DeliveryChallanSet = "[dbo].[DeliveryChallanSet]";
        #endregion

        #region Delivery Challan Detail
        public const string DeliveryChallanDetailGet = "[dbo].[DeliveryChallanDetailGet]";
        public const string DeliveryChallanDetailSet = "[dbo].[DeliveryChallanDetailSet]";
        #endregion

        #region Group User
        public const string GroupSet = "[UserAccess].[GroupSet]";
        public const string GroupUserSet = "[UserAccess].[GroupUserSet]";
        public const string GroupRoleSet = "[UserAccess].[GroupRoleSet]";
        #endregion       
    }
}
