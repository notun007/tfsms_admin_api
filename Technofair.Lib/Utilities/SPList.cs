using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;

namespace Technofair.Lib.Utilities
{
    public static partial class SPList
    {

        #region Security 
       

        public static class SecCompanyModule
        {
            public static string GetSecCompanyModulesByCompanyId = "GetSecCompanyModulesByCompanyId";
            public static string DeleteSecCompanyModulesByCompanyId = "DeleteSecCompanyModulesByCompanyId";
        }
        public static class SecCompanyUser
        {
            public static string DeleteSecCompanyUsersBySecUserId = "DeleteSecCompanyUsersBySecUserId";
            public static string GetSecCompanyUsersBySecUserId = "GetSecCompanyUsersBySecUserId";
            public static string GetSecCompanyUserBySecUserIdForMapping = "GetSecCompanyUserBySecUserIdForMapping";
        }

        public static class SecModule
        {
            public static string GetSecModules = "GetSecModules";
            public static string GetSecModulesByCompanyId = "GetSecModulesByCompanyId";
            public static string GetSecPermittedModuleByUserId = "GetSecPermittedModuleByUserId";
        }
        public static class SecMenu
        {
            public static string GetSecModuleResourcesByUserIdAndModuleId = "GetSecModuleResourcesByUserIdAndModuleId";
            public static string GetCmnProcessLevelByUserId = "GetCmnProcessLevelByUserId";
            public static string GetModuleMenuByRoleId = "GetModuleMenuByRoleId";
        }
        public static class SecMenuPermission
        {
            public static string DeleteSecMenuPermissionByUserOrRoleId = "DeleteSecMenuPermissionByUserOrRoleId";
            public static string GetSecMenuPermissionsByRoleId = "GetSecMenuPermissionsByRoleId";
            public static string GetSecPermittedModuleByUserId = "GetSecPermittedModuleByUserId";
            public static string GetSecMenuButtonPermission = "GetSecMenuButtonPermission";
        }

        public static class SecUserRole
        {
            public static string DeleteSecUserRolesByUserId = "DeleteSecUserRolesByUserId";
            public static string GetSecUserRoleByUserId = "GetSecUserRoleByUserId";
        }
        public static class SecUser
        {
            public static string GetSecUsersByCompanyId = "GetSecUsersByCompanyId";
            public static string GetSecUsersByCompanyAndUserType = "GetSecUsersByCompanyAndUserType";
            public static string GetSecUsersByCompanyAndUserLevel = "GetSecUsersByCompanyAndUserLevel";
            public static string GetSecUserInfoByCompanyId = "GetSecUserInfoByCompanyId";
            public static string GetSecUsersByLevel = "GetSecUsersByLevel";
            public static string GetSecUsersByLoginName = "GetSecUsersByLoginName";
            //public static string GetSecUserRolesByCompanyAndRoleId = "GetSecUserRolesByCompanyAndRoleId";
            public static string GetRoleLessUserByCompanyId = "GetRoleLessUserByCompanyId";
            public static string GetRoleOrientedUserByCompanyRoleId  = "GetRoleOrientedUserByCompanyRoleId";
            public static string GetSecUserByCompanyId = "GetSecUserByCompanyId";
            public static string GetUserRechargeUserId = "GetUserRechargeUserId";
            public static string GetSecUserByAnyKey = "GetSecUserByAnyKey";
            public static string GetUserRechargeByAnyKey = "GetUserRechargeByAnyKey";
            public static string GetUserRechargeBalanceByAnyKey = "GetUserRechargeBalanceByAnyKey";
        }
        public static class SecDashboardPermission
        {
            public static string GetSecDashboardPermissionByRoleId = "GetSecDashboardPermissionByRoleId";
            public static string DeleteSecDashboardPermissionByRoleId = "DeleteSecDashboardPermissionByRoleId";
        }

        #endregion

        #region Common
        public static class Company
        {
            public static string GetCmnCompanyViewList = "GetAllCompany";//added by shariful 24-12-2023
            //public static string GetCompanyByParentCompanyId = "GetCompanyByParentCompanyId";
              public static string GetChildCompanyByParentCompanyId = "GetChildCompanyByParentCompanyId";//change by shariful 04-11-2024
            public static string GetChildCompanyUserByParentCompanyId = "GetChildCompanyUserByParentCompanyId";//added by shariful 04-11-2024

            public static string GetCmnCompanies = "GetCmnCompanies";
            public static string GetSlsCustomers = "GetSlsCustomers";
            public static string GetCompanyBySelfOrParentCompanyId = "GetCompanyBySelfOrParentCompanyId";
            public static string GetSubscriptionStatistics = "GetSubscriptionStatistics";
           
            public static string GetCompanyByCompanyId = "GetCompanyByCompanyId";
            public static string GetSolutionProvider = "GetSolutionProvider";
            public static string GetMainServiceOperator = "GetMainServiceOperator";
            public static string GetSecondLevelCompanies = "GetSecondLevelCompanies";
            public static string GetClientForQuickLoginByCompanyId = "GetClientForQuickLoginByCompanyId";
            public static string GetUpperLevelCompanyByCompanyType = "GetUpperLevelCompanyByCompanyType";

        }
        public static class CmnApprovalComment
        {
            public static string GetCmnApprovalCommentsByProcessAndRefId = "GetCmnApprovalCommentsByProcessAndRefId";
            public static string InsertCmnApprovalComment = "InsertCmnApprovalComment";

        }
        public static class CmnApprovalProcess
        {
            
            public static string GetCmnApprovalProcessesByModuleId = "GetCmnApprovalProcessesByModuleId";
            
        }
        public static class CmnApprovalUserPermissions
        {
            public static string GetApprovalUserPermission = "GetApprovalUserPermission";
            public static string GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId = "GetCmnApprovalUserPermissionCountByUserAndProcessAndLevelId";
        }
        public static class CmnApproval
        {
            public static string GetCmnApprovalsById = "GetCmnApprovalsById";
            public static string GetCmnApprovalsByRefAndProcessAndLevelId = "GetCmnApprovalsByRefAndProcessAndLevelId";
            public static string InsertCmnApprovals = "InsertCmnApprovals";
            public static string UpdateCmnApproval = "UpdateCmnApproval";
            public static string UpdateCmnApprovalByRefIdNProcessIdNLevelId = "UpdateCmnApprovalByRefIdNProcessIdNLevelId";
            public static string UpdateCmnApprovalWithComments = "UpdateCmnApprovalWithComments";
            public static string GetCmnApprovalHistoryByRefId = "GetCmnApprovalHistoryByRefId";
            public static string GetNoOfUnApproveByProcess = "GetNoOfUnApproveByProcess";
            public static string GetCmnApprovalsStatus = "GetCmnApprovalsStatus";
            public static string GetApprovalPermissions = "GetApprovalPermissions";
            public static string ExecuteSqlText = "ExecuteSqlText";
            public static string GetCmnApprovalsByProcessId = "GetCmnApprovalsByProcessId";
        }


        public static class CmnApprovalProcessLevels
        {
            public static string GetCmnApprovalProcessLevelMappingByCompanyIdAndModuleId = "GetCmnApprovalProcessLevelMappingByCompanyIdAndModuleId";
        }

        public static class CmnProcessLevel
        {
            public static string GetCmnProcessLevelsByApprovalProcessId = "GetCmnProcessLevelsByApprovalProcessId";
        }
        public static class CmnProjects
        {
            public static string GetCmnProjectsByCompanyAndBusinessIdAndChartOfAccountId = "GetCmnProjectsByCompanyAndBusinessIdAndChartOfAccountId";
        }
        public static class CmnFinancialYears
        {
            public static string GetCmnFinancialYears = "GetCmnFinancialYears";
        }

        #endregion

        #region Accounts

        public static class Common
        {
            public static string GetAnFTransactinalHeadForMappingByProjectId = "GetAnFTransactinalHeadForMappingByProjectId";
            public static string GetNextPurchaseRefNo = "GetNextPurchaseRefNo";
            public static string NextChalanSlNo = "GetNextChallanNo";
            public static string NextChalanMemoNo = "GetNextMemoNo";
            public static string GetNextSubscriberNumber = "GetNextSubscriberNumber";
            public static string ValidateClientPackageRateCommission = "ValidateClientPackageRateCommission";
            

        }

        public static class AnFClientPayment
        {
            public static string InsertAnFClientPayments = "InsertAnFClientPayments";
            public static string UpdateAnFClientPayments = "UpdateAnFClientPayments";

            public static string InsertAnFClientPaymentDetails = "InsertAnFClientPaymentDetails";
            public static string UpdateAnFClientPaymentDetails = "UpdateAnFClientPaymentDetails";
        }
        public static class AnFChartOfAccounts
        {
            public static string GetAnFCOAsLastChildNode = "GetAnFCOAsLastChildNode";
            public static string GetAnFTransactinalHeadByCmnCompanyId = "GetAnFTransactinalHeadByCmnCompanyId";
        }

        public static class AnFChequeBooks
        {
            public static string GetAnFCompanyCashAtBankByCompanyId = "GetAnFCompanyCashAtBankByCompanyId";

        }

        public static class AnFVoucherDetail
        {
            public static string GetAnFVoucherDetailsByVoucherId = "GetAnFVoucherDetailsByVoucherId";
        }
        public static class AnFVoucher
        {
            public static string GetAnFVoucherByDate = "GetAnFVoucherByDate";
            public static string GetAnFVoucherNo = "GetAnFVoucherNo";
            public static string ExecuteSqlText = "ExecuteSqlText";
            public static string GetAnFApprovedVoucherForTransaction = "GetAnFApprovedVoucherForTransaction";
            public static string GetAnFVouchersForBankReconciliation = "GetAnFVouchersForBankReconciliation";
        }


        #endregion

        #region Inventory


        public static class InvCurrentStock
        {
            public static string GetInvCurrentStockByStore = "GetInvCurrentStockByStore";
        }

        public static class InvItem
        {
            public static string GetInvItemParents = "GetInvItemParents";
            public static string GetInvItemTypesByCompanyId = "GetInvItemTypesByCompanyId";
            public static string GetInvItemLastCode = "GetInvItemLastCode";
            public static string GetInvDuplicateItem = "GetInvDuplicateItem";
            public static string GetInvItemsWithLatestPriceByCompanyId = "GetInvItemsWithLatestPriceByCompanyId";
            public static string GetInvItemsById = "GetInvItemsById";
            public static string GetInvItemAveragePurchaseRate = "GetInvItemAveragePurchaseRate";
            public static string GetInvItemsWithUnitByCompanyId = "GetInvItemsWithUnitByCompanyId";
        }

        public static class InvPurchase
        {
            public static string GetInvPurchasesLastCode = "GetInvPurchasesLastCode";
            public static string GetInvPurchasesSearchByParameterWise = "GetInvPurchasesSearchByParameterWise";
            public static string GetInvPurchaseForReturn = "GetInvPurchaseForReturn";
            public static string RptInvPurchasePrint = "RptInvPurchasePrint";

        }

        public static class InvFgPurchase
        {
            public static string GetInvFgPurchasesSearchByParameterWise = "GetInvFgPurchasesSearchByParameterWise";
            public static string GetInvFgPurchaseForReturn = "GetInvFgPurchaseForReturn";
            public static string RptInvFgPurchasePrint = "RptInvFgPurchasePrint";

        }
        public static class InvPurchaseDetail
        {
            public static string GetInvPurchaseDetailsByPurchaseId = "GetInvPurchaseDetailsByPurchaseId";
            public static string DeleteInvPurchaseDetailsByInvPurchaseId = "DeleteInvPurchaseDetailsByInvPurchaseId";
        }


        public static class InvStockInOut
        {
            public static string DeleteInvStockInOutsByRefId = "DeleteInvStockInOutsByRefId";
        }
        public static class InvStockOpen
        {
        }


        public static class InvUnit
        {
            public static string GetInvUnitByItemId = "GetInvUnitByItemId";
        }

        public static class InvReport
        {
            public static string RptInvPurchaseLedger = "RptInvPurchaseLedger";
            public static string RptInvPurchasePaid = "RptInvPurchasePaid";
            public static string RptInvSupplierList = "RptInvSupplierList";
            public static string RptInvCurrentStock = "RptInvCurrentStock";
            public static string RptInvStoreStockSummary = "RptInvStoreStockSummary";
            public static string RptInvInvoices = "RptInvInvoices";
            public static string RptInvIssue = "RptInvIssue";
            public static string RptInvItemList = "RptInvItemList";
            public static string RptInvMRR = "RptInvMRR";
            public static string RptInvMRRDetails = "RptInvMRRDetails";
            public static string RptInvMRRList = "RptInvMRRList";
            //public static string RptInvPRDetails = "RptInvPRDetails";
            public static string RptInvPurchaseRequisition = "RptInvPurchaseRequisition";
            public static string RptInvPurchaseRequisitionDetails = "RptInvPurchaseRequisitionDetails";
            public static string RptInvPurchaseRequisitionsList = "RptInvPurchaseRequisitionsList";
            public static string RptInvQualityControlDetails = "RptInvQualityControlDetails";
            public static string RptInvQualityControls = "RptInvQualityControls";
            public static string RptInvQualityControlsList = "RptInvQualityControlsList";
            public static string RptInvStoreRequisition = "RptInvStoreRequisition";
            public static string RptInvStoreRequisitionsList = "RptInvStoreRequisitionsList";
            public static string RptInvStoreRequisitionDetails = "RptInvStoreRequisitionDetails";
            public static string RptInvStoreRequisitionsIssueDetails = "RptInvStoreRequisitionsIssueDetails";


            public static string RptPrcPurchaseOrderDetails = "RptPrcPurchaseOrderDetails";
            public static string RptPrcPurchaseOrders = "RptPrcPurchaseOrders";
            public static string RptPrcPurchaseOrdersList = "RptPrcPurchaseOrdersList";
            public static string RptPrcQuotationDetails = "RptPrcQuotationDetails";
            public static string RptPrcQuotationList = "RptPrcQuotationList";
            public static string RptPrcRFQ = "RptPrcRFQ";
            public static string RptPrcRFQDetails = "RptPrcRFQDetails";
            public static string RptRFQList = "RptPrcRFQList";
            public static string RptRequisitionInfoById = "RptRequisitionInfoById";

            public static string RptPrcFgPurchaseOrders = "RptPrcFgPurchaseOrders";
        }


        #endregion



        #region HRM
        public static class HrmEmployee
        {
            public static string GetHrmEmployees = "GetHrmEmployees";
            public static string GetHrmEmployeeByParameterWise = "GetHrmEmployeeByParameterWise";
            public static string RptHrmEmployeeCV = "RptHrmEmployeeCV";
            public static string RptHrmEmployeeSummary = "RptHrmEmployeeSummary";
            public static string GetHROverviewForDashboard = "GetHROverviewForDashboard";
            public static string GetHrmEmployeeByCompanyId = "GetHrmEmployeeByCompanyId";
            public static string GetEmployeeByCompanyId = "GetEmployeeByCompanyId";
            public static string GetEmployeeByAnyKey = "GetEmployeeByAnyKey";
        }

        public static class HrmAttendance
        {
            public static string RptHrmAttendance = "RptHrmAttendance";
            public static string RptHrmMovementRegister = "RptHrmMovementRegister";
            public static string GetHrmMovementByLineManagerId = "GetHrmMovementByLineManagerId";
            public static string GetHrmAttendancesByDateAndEmployeeId = "GetHrmAttendancesByDateAndEmployeeId";
            public static string GetHrmHolidayByOfficeAndDate = "GetHrmHolidayByOfficeAndDate";
            public static string GetHrmMovementsOfToday = "GetHrmMovementsOfToday";
        }

        public static class HrmLeave
        {
            public static string GetHrmLeaveForDashboard = "GetHrmLeaveForDashboard";
            public static string GetHrmLeaveApplicationByLineManagerId = "GetHrmLeaveApplicationByLineManagerId";
            public static string GetHrmApprovedLeaveApplicationByLineManagerId = "GetHrmApprovedLeaveApplicationByLineManagerId";
            public static string GetHrmOffDayAdjustmentByLineManagerId = "GetHrmOffDayAdjustmentByLineManagerId";
            public static string GetHrmLeaveApplicationStatus = "GetHrmLeaveApplicationStatus";
        }
        public static class HrmLeaveBalance
        {
            public static string RptHrmEmployeesLeaveBalanceSummary = "RptHrmEmployeesLeaveBalanceSummary";
        }
        public static class HrmTour
        {
            public static string GetHrmTourByLineManagerId = "GetHrmTourByLineManagerId";
            public static string GetHrmTourForDashboard = "GetHrmTourForDashboard";
            public static string RptHrmTourStatus = "RptHrmTourStatus";
        }
        public static class HrmEmployeeOvertime
        {
            public static string GetHrmOvertimeEmloyeesByOffice = "GetHrmOvertimeEmloyeesByOffice";
            public static string GetHrmEmployeeOvertimeByMonthAndEmployeeId = "GetHrmEmployeeOvertimeByMonthAndEmployeeId";
        }
        public static class PrlSalary
        {
            public static string GetPrlSalary = "GetPrlSalary";
            public static string GetPrlIncomeTax = "GetPrlIncomeTax";
            public static string GetPrlExistingIncomeTax = "GetPrlExistingIncomeTax";
            public static string GetPrlEmployeeSalaryByDateRange = "GetPrlEmployeeSalaryByDateRange";
            public static string GetPrlPayrollEmployees = "GetPrlPayrollEmployees";
            public static string RptPrlMonthlySalaryStatement = "RptPrlMonthlySalaryStatement";
            public static string RptPrlPayrollMemo = "RptPrlPayrollMemo";
        }

        #endregion

        #region Sales


        public static class SlsDashboardChart
        {
            public static string RegionWiseSales = "RegionWiseSales";
        }
        public static class SPChallan
        {
            public static string GetSlsChallansByDate = "GetSlsChallansByDate";
            public static string GetSlsChallanReturnReceiveHistories = "GetSlsChallanReturnReceiveHistories";
        }

        public static class SlsInvoiceDetail
        {
                     public static string DeleteSlsInvoiceDetailsBySlsInvoiceId = "DeleteSlsInvoiceDetailsBySlsInvoiceId";
            public static string GetSlsInvoiceDetailByInvoiceId = "GetSlsInvoiceDetailByInvoiceId";
        }
        public static class SlsInvoice
        {
            public static string GetSlsInvoiceById = "GetSlsInvoiceById";
            public static string GetSlsInvoiceLastCode = "GetSlsInvoiceLastCode";
            public static string GetSlsInvoiceSearch = "GetSlsInvoiceSearch";
            public static string UpdateSlsInvoicesPrintStatus = "UpdateSlsInvoicesPrintStatus";
            public static string GetSlsInvoiceSearchByParameterWise = "GetSlsInvoiceSearchByParameterWise";
            public static string RptSlsMultipleInvoicePrint = "RptSlsMultipleInvoicePrint";
            public static string GetSlsInvoiceForReturn = "GetSlsInvoiceForReturn";
            public static string RptSlsCashMemoPrint = "RptSlsCashMemoPrint";
        }

        public static class SlsReturnDetail
        {
            public static string DeleteSlsReturnDetailsBySlsReturnId = "DeleteSlsReturnDetailsBySlsReturnId";
        }


        public static class SlsStockInOut
        {
            public static string GetSlsStockInOutByStoreId = "GetSlsStockInOutByStoreId";
            public static string InsertSlsStockInOut = "InsertSlsStockInOut";
            public static string UpdateSlsStockInOut = "UpdateSlsStockInOut";
            public static string UpdateSlsStockInOutForReceive = "UpdateSlsStockInOutForReceive";
            public static string DeleteSlsStockInOutsByRefId = "DeleteSlsStockInOutsByRefId";
        }

        public static class SlsCurrentStock
        {
            public static string GetSlsCurrentStockByStore = "GetSlsCurrentStockByStore";
        }


        public static class SlsReport
        {
            public static string RptSlsStoreStockSummary = "RptSlsStoreStockSummary";
            public static string RptSlsStoreStockSummaryWithMarketCredit = "RptSlsStoreStockSummaryWithMarketCredit";
            public static string RptSlsSalesOrderAndChallan = "RptSlsSalesOrderAndChallan";
            public static string RptSlsOfficerDueStock = "RptSlsOfficerDueStock";
            public static string RptSlsOfficerStoreStockSummary = "RptSlsOfficerStoreStockSummary";
            public static string RptSlsProductOrderSalesReturnSummary = "RptSlsProductOrderSalesReturnSummary";
            public static string RptSlsSalesSummary = "RptSlsSalesSummary";
            public static string RptSlsSalesLedger = "RptSlsSalesLedger";
            public static string RptSlsDailySalesProductWise = "RptSlsDailySalesProductWise";
            public static string RptSlsStoreProductReceive = "RptSlsStoreProductReceive";
            public static string RptSlsCustomerList = "RptSlsCustomerList";
            public static string RptSlsCurrentStock = "RptSlsCurrentStock";
            public static string RptSlsCurrentStockBatchWise = "RptSlsCurrentStockBatchWise";
            public static string RptSlsSalesReceived = "RptSlsSalesReceived";
            public static string RptInvPurchaseLedger = "RptInvPurchaseLedger";

            public static string RptMoneyReceipt = "RptGetAllSpSlsCollections";
            public static string RptSalesReturnById = "RptSalesReturnById";

            public static string RptSlsPartyList = "RptSlsPartyList";
            public static string RptSlsProductPrice = "RptSlsProductPrice";
            public static string GetAllProductList = "GetAllProductList";
            public static string RptSlsChallanWiseCollection = "RptSlsChallanWiseCollection";
            public static string RptSlsDailySales = "RptSlsDailySales";

            
            public static string RptSalesOrderList = "RptSalesOrderList";
            public static string RptSlsStoreStockBalance = "RptSlsStoreStockBalance";
            public static string RptSalesCommission = "RptSalesCommission";
            public static string RptPartyCredit = "RptPartyCredit";
            public static string RptSalesReturnList = "RptSalesReturnList";
            public static string RptSlsSalesOrder = "RptSlsSalesOrder";
            public static string RptSlsDeliveryChallan = "RptSlsDeliveryChallan";
            public static string RptSlsUndeliveredSOList = "RptSlsUndeliveredSOList";
            public static string RptSlsSalesOrderStatement = "RptSlsSalesOrderStatement";
            public static string RptSlsChallanStatement = "RptSlsChallanStatement";
            public static string RptSlsVATSheet = "RptSlsVATSheet";
            public static string RptSlsTaxInvoice = "RptSlsTaxInvoice";
            public static string GetPrdProductBatchNo = "GetPrdProductBatchNo";

        }



        #endregion
        #region Production

        public static class PrdCurrentStock
        {
            public static string GetPrdCurrentStockByStore = "GetPrdCurrentStockByStore";
        }


        public static class PrdProduct
        {
            public static string GetPrdProductsByCmnCompanyId = "GetPrdProductsByCmnCompanyId";
            public static string GetPrdProductsByCompanyId = "GetPrdProductsByCompanyId";
            public static string GetPrdProductsByProductType = "GetPrdProductsByProductType";
            public static string GetPrdProductsRegardingBOMsByCompanyId = "GetPrdProductsRegardingBOMsByCompanyId";
            public static string GetPrdProductStoreOpen = "GetPrdProductStoreOpen";
            public static string GetPrdProductsWithPriceByCmnCompanyId = "GetPrdProductsWithPriceByCmnCompanyId";
            public static string GetSlsProductPreviousDate = "GetSlsProductPreviousDate";
            public static string GetPrdProductTypesByCompanyId = "GetPrdProductTypesByCompanyId";
            public static string GetPrdProductWithUnitByCompanyId = "GetPrdProductWithUnitByCompanyId";
            public static string GetPrdRawMaterialWithLatestPurchaseRateByCompanyId = "GetPrdRawMaterialWithLatestPurchaseRateByCompanyId";
            public static string GetPrdFinishedGoodsWithLatestPurchaseRateByCompanyId = "GetPrdFinishedGoodsWithLatestPurchaseRateByCompanyId";
            public static string GetSlsProductInfoByEffectiveDate = "GetSlsProductInfoByEffectiveDate";
            public static string GetSlsProductStoreOpen = "GetSlsProductStoreOpen";
            public static string GetPrdVirtualProductsByCompanyId = "GetPrdVirtualProductsByCompanyId";
            public static string GetPrdProductParents = "GetPrdProductParents";
            public static string GetPrdProductLastCode = "GetPrdProductLastCode";
            public static string GetPrdProductById = "GetPrdProductById";
            //public static string InsertPrdProducts = "InsertPrdProducts";
            public static string GetPrdDuplicateProduct = "GetPrdDuplicateProduct";
            public static string GetPrdProductsWithCostByCompanyId = "GetPrdProductsWithCostByCompanyId";
            public static string GetInvFgProductAveragePurchaseRate = "GetInvFgProductAveragePurchaseRate";
            public static string GetPrdProductAveragePurchaseRate = "GetPrdProductAveragePurchaseRate";
            public static string GetPrdDeviceNumbers = "GetPrdDeviceNumbers";
            public static string GetCredentialByIntegratorId = "GetCredentialByIntegratorId";
			public static string GetPrdDeviceNumbersByRange = "GetPrdDeviceNumbersByRange";
            public static string IsDeviceStateTransitionable = "IsDeviceStateTransitionable";


        }
        public static class PrdProductUnit
        {
            public static string DeletePrdProductUnitsByPrdProductId = "DeletePrdProductUnitsByPrdProductId";
            public static string GetPrdProductUnitByPrdProductId = "GetPrdProductUnitByPrdProductId";
        }
        public static class PrdStockInOut
        {
            
            public static string DeletePrdStockInOutsByRefId = "DeletePrdStockInOutsByRefId";
        }

        public static class PrdReport
        {
            public static string GetPrdProductsForBarcodeByCompanyId = "GetPrdProductsForBarcodeByCompanyId";
            public static string GetAllProductList = "GetAllProductList";
            public static string GetInvPurchaseChallanReturnProductInfoByRefId = "GetInvPurchaseChallanReturnProductInfoByRefId";
        }

        public static class PrdProductPrice
        {
            public static string GetPrdProductsWithPriceByCmnCompanyId = "GetPrdProductsWithPriceByCmnCompanyId";
        }
        #endregion


        #region Bank
        public static class BnkReport
        {
            public static string RptBnkTransactionByDate = "RptBnkTransactionByDate";
            public static string RptBnkLedgerByAccountNo = "RptBnkLedgerByAccountNo";
            public static string RptBnkBankStatement = "RptBnkBankStatement";
        }

        #endregion

        #region Accounts Report
        public static class Report
        {
            public static string GetAnFOpeningBalanceByDate = "GetAnFOpeningBalanceByDate";
            public static string RptAnFSupplierWiseLedger = "RptAnFSupplierWiseLedger";
            public static string RptAnFPartyWiseLedger = "RptAnFPartyWiseLedger";
            //public static string RptCrmAccounts = "RptCrmAccounts";
            //public static string RptActualSRs = "RptActualSRs";
            public static string RptAnFBalanceSheet = "RptAnFBalanceSheet";
            public static string RptAnFChartsofAccount = "RptAnFChartsofAccount";
            public static string RptAnFProfitLoss = "RptAnFProfitLoss";
            public static string RptAnFGeneralLedgerProject = "RptAnFGeneralLedgerProject";
            public static string RptAnFOpenningBalance = "RptAnFOpenningBalance";
            public static string RptAnFCostOfGoodsSold = "RptAnFCostOfGoodsSold";
            public static string RptAnFGeneralLedger = "RptAnFGeneralLedger";
            public static string RptAnFNoteSchedule = "RptAnFNoteSchedule";
            public static string RptAnFNoteScheduleProject = "RptAnFNoteScheduleProject";
            //public static string RptProjectWiseOpeningBalances = "RptProjectWiseOpeningBalances";
            //public static string RptProjectWiseOpeningBalancesNew = "RptProjectWiseOpeningBalancesNew";
            public static string RptAnFTrialBalanceProjectWise = "RptAnFTrialBalanceProjectWise";
            public static string RptAnFTrialBalanceDetails = "RptAnFTrialBalanceDetails";
            //public static string RptAnFTrialBalanceSummary = "RptAnFTrialBalanceSummary";
            //public static string RptUndeliveredSOList = "RptUndeliveredSOList";
            public static string RptAnFVoucherDetailsByVoucherId = "RptAnFVoucherDetailsByVoucherId";
            public static string RptAnFVoucherDetailsList = "RptAnFVoucherDetailsList";
            public static string RptAnFVoucherList = "RptAnFVoucherList";
            public static string RptAnFDailyCashBook = "RptAnFDailyCashBook";
            public static string GetAnFDailyExpenditure = "GetAnFDailyExpenditure";
            public static string RptAnFIncomeStatement = "RptAnFIncomeStatement";
        }

        #endregion

        #region ScpSubscriber

        public static class SubscriberSP
        {
            public static string GetLockableUnlockableSubscriptionInfoByAnyKey = "GetLockableUnlockableSubscriptionInfoByAnyKey";
            public static string GetCancelableSubscriptionInfoByAnyKey = "GetCancelableSubscriptionInfoByAnyKey";
            public static string GetScpSubscriberByParameter = "GetScpSubscriberByParameter";

            public static string GetSubscriptionInfoByAnyKey = "GetSubscriptionInfoByAnyKey";
            public static string GetFreeSubscriptionInfoByAnyKey = "GetFreeSubscriptionInfoByAnyKey";
            public static string GetRenewableSubscriptionInfoByAnyKey = "GetRenewableSubscriptionInfoByAnyKey";
            public static string GetRenewalSubscriptionInfoByDaysBeforeExpireDate = "GetRenewalSubscriptionInfoByDaysBeforeExpireDate";
            public static string GetRenewalSubscriptionByCustomerNumber = "GetRenewalSubscriptionByCustomerNumber";
            public static string GetSubscriptionHistoryByCustomerNumber = "GetSubscriptionHistoryByCustomerNumber";
            public static string GetSelfSubscriberByParameter = "GetSelfSubscriberByParameter";
            public static string GetScpSubscribers = "GetScpSubscribers";
            public static string GetScpSubscriberPackageInfoBySubscriberId = "GetScpSubscriberPackageInfoBySubscriberId";
            public static string GetScpSubscriberLatestPackageInfoByDeviceNumberId = "GetScpSubscriberLatestPackageInfoByDeviceNumberId";
            public static string GetScpSubscriberLatestPackageInfoBySubscriberId = "GetScpSubscriberLatestPackageInfoBySubscriberId";
            public static string GetScpSubscriberInvoicesBydDateAndDeviceNumberId = "GetScpSubscriberInvoicesBydDateAndDeviceNumberId";
            public static string GetScpSubscriberInvoicesByInvoiceId = "GetScpSubscriberInvoicesByInvoiceId";
            public static string GetPrdUnassignStockInDeviceByCompanyId = "GetPrdUnassignStockInDeviceByCompanyId";
            public static string GetPayableUnassignDeviceByCompanyId = "GetPayableUnassignDeviceByCompanyId";
            public static string GetUnassignDeviceByAnyKey = "GetUnassignDeviceByAnyKey";
            public static string GetAssignDeviceByAnyKey = "GetAssignDeviceByAnyKey";
            public static string GetPrdCurrentStockDeviceByCompanyId = "GetPrdCurrentStockDeviceByCompanyId";
            public static string GetPrdDeviceInfoByClientId = "GetPrdDeviceInfoByClientId";
            public static string GetPrdDeviceProfile = "GetPrdDeviceProfile";
            public static string GetSelfPlusSuccessorDevicesByCompanyId = "GetSelfPlusSuccessorDevicesByCompanyId";

            public static string GetCmnIntegratorCredentialByCompanyAndIntegratorId = "GetCmnIntegratorCredentialByCompanyAndIntegratorId";
            public static string GetScpSubscriberWithDeviceByParameter = "GetScpSubscriberWithDeviceByParameter";
            public static string GetScpSubscriptionInfoByParameter = "GetScpSubscriptionInfoByParameter";
            public static string GetScpSubscriptionInfoBySubscriberId = "GetScpSubscriptionInfoBySubscriberId";
            public static string GetIntegratorPackageByAnyKey = "GetIntegratorPackageByAnyKey";
            public static string GetSubscriptionByPeriod = "GetSubscriptionByPeriod";

            public static string GetScpSubscriberLatestInvoiceAndPackageBySubscriberId = "GetScpSubscriberLatestInvoiceAndPackageBySubscriberId";
            //Remove: 13.01.2025
            //public static string GetScpSubscriberLatestInvoiceAndPackageByDeviceNumberId = "GetScpSubscriberLatestInvoiceAndPackageByDeviceNumberId";
            public static string GetActiveSubscriberPackageBySubscriberId = "GetActiveSubscriberPackageBySubscriberId";
            public static string GetLastSubscriberPackageBySubscriberId = "GetLastSubscriberPackageBySubscriberId";


            public static string GetSubscriberWithNeverdOrExpiredPackage = "GetSubscriberWithNeverdOrExpiredPackage";

            public static string GetDeviceInfo = "GetDeviceInfo";
            public static string GetSubscriptionInfo = "GetSubscriptionInfo";

            public static string VerifyPackageAssignment = "VerifyPackageAssignment";
            public static string GetNextSubscriptionModeBySubscriberId = "GetNextSubscriptionModeBySubscriberId";
            public static string GetPackageStateByDevice = "GetPackageStateByDevice";
            public static string GetSubscriberDevicePackageInfoByCustomerNo = "GetSubscriberDevicePackageInfoByCustomerNo";
            public static string GetSubscriberDevicePackageInfoBySubscriberId = "GetSubscriberDevicePackageInfoBySubscriberId";
            //Remove 13.01.2025
            //public static string GetSubscriberDevicePackageInfoByPrdDeviceNumberId = "GetSubscriberDevicePackageInfoByPrdDeviceNumberId";

            //public static string GetAlternatePaymentLog = "GetAlternatePaymentLog";
            public static string GetPaymentLog = "GetPaymentLog";
            public static string GetLoanCollection = "GetLoanCollection";
            public static string GetLoanCollectionByLoanId = "GetLoanCollectionByLoanId";
            public static string GetPaymentLogSummaryByAnyKey = "GetPaymentLogSummaryByAnyKey";
            public static string GetCreditDeviceByAnyKey = "GetCreditDeviceByAnyKey";
            public static string GetSubscriptionSummaryByClient = "GetSubscriptionSummaryByClient";
            public static string GetClientRechargeSummaryByAnyKey = "GetClientRechargeSummaryByAnyKey";

            public static string GetSellableDeviceByCompanyId = "GetSellableDeviceByCompanyId";
            public static string GetSubscriptionHold = "GetSubscriptionHold";
            //public static string GetLastSubscriberPackageInfoByAnyKey = "GetLastSubscriberPackageInfoByAnyKey";
            public static string GetAssignedDeviceBySubscriberId = "GetAssignedDeviceBySubscriberId";
            public static string GetSubscriptionStatisticsDtl = "GetSubscriptionStatisticsDtl";
            public static string GetAssignedDeviceBySubscriberDevice = "GetAssignedDeviceBySubscriberDevice";
            public static string GetAvailableDeviceForFreeByDeviceNumberId = "GetAvailableDeviceForFreeByDeviceNumberId";
            public static string GetPackageStatusBySubscriberId = "GetPackageStatusBySubscriberId";
            public static string GetNextLoanNo = "GetNextLoanNo"; 

        }

        public static class RechargeSP
        {
            public static string GetScpClientRechargeForApprovalByClient = "GetScpClientRechargeForApprovalByClient";
            public static string GetClientRechargeByAnyKey = "GetClientRechargeByAnyKey";
            public static string GetClientRechargeHistoryByAnyKey = "GetClientRechargeHistoryByAnyKey";
            public static string GetScpClientRechargeBalanceByClientId = "GetScpClientRechargeBalanceByClientId";
            public static string GetScpClientCurrentRechargeBalanceByClientId = "GetScpClientCurrentRechargeBalanceByClientId";
            public static string GetScpClientAvailableRechargeBalanceByClientId = "GetScpClientAvailableRechargeBalanceByClientId";
            public static string GetScpUserRechargeBalanceByUserId = "GetScpUserRechargeBalanceByUserId";
            public static string InitiateSelfRecharge = "InitiateSelfRecharge";
            public static string FinalizeSelfRecharge = "FinalizeSelfRecharge";

        }

        //Asad Added On 27.11.2023
        public static class PackageSP
        {
            public static string GetPackageDetailByCompanyId = "GetPackageDetailByCompanyId";
            public static string GetScpClientPackageByScpPackageId = "GetScpClientPackageByScpPackageId";
            public static string GetCompanyPackagePeriod = "GetCompanyPackagePeriod";
            public static string GetScpClientPackageDetailByClientAndPackageId = "GetScpClientPackageDetailByClientAndPackageId";
            public static string GetLastHistoryBySubscriberAndDeviceId = "GetLastHistoryBySubscriberAndDeviceId";
            public static string GetRenewalActiveSubscriberInvoiceDetailBySubscriberId = "GetRenewalActiveSubscriberInvoiceDetailBySubscriberId";
            public static string GetPreceedingRenewalActiveSubscriberInvoiceDetailBySubscriberId = "GetPreceedingRenewalActiveSubscriberInvoiceDetailBySubscriberId";
            public static string GetPrecedingSubscriberInvoiceDetailByDeviceId = "GetPrecedingSubscriberInvoiceDetailByDeviceId";
            public static string GetScpPackagesByCompanyId = "GetScpPackagesByCompanyId";
            public static string GetScpClientPackageRateCommission = "GetScpClientPackageRateCommission";
            public static string GetDeviceLoanInfoByLoaneeId = "GetDeviceLoanInfoByLoaneeId";
            public static string GetDeviceLoanInfo = "GetDeviceLoanInfo";
            public static string GetDeviceLoanInfoByAppKey = "GetDeviceLoanInfoByAppKey";

        }


        #endregion

        #region Collection
        public static class Collection
        {
            public static string GetCollectionSummaryByDateRange = "GetCollectionSummaryByDateRange";
        }
        public static class Migration
        {
            public static string FetchMigrationStatistics = "FetchMigrationStatistics";
            public static string MigrateFormerSmsDbSyncToCas = "MigrateFormerSmsDbSyncToCas";
        }

        public static class TFAClientBill
        {
            public static string GenerateClientInvoice = "GenerateClientInvoice";
            public static string GetClientInvoice = "GetClientInvoice";
            public static string GetClientApprovedUnpaidBill = "GetClientApprovedUnpaidBill";
            public static string GetClientApprovedBill = "GetClientApprovedBill";
            public static string GetClientPaymentInvoice = "GetClientPaymentInvoice";
            public static string ApproveClientInvoice = "ApproveClientInvoice";
          


        }
        #endregion

        public static class SendSMS
        {
            public static string UpdateCmnClientSMSBalances = "UpdateCmnClientSMSBalances";
        }
    
        public static class TFAdmin
        {
            public static string GetActiveCompanyCustomerWithClientPackage = "GetActiveCompanyCustomerWithClientPackage";
            public static string GetClientBillByClientPaymentDetailId = "GetClientBillByClientPaymentDetailId";
            public static string GetClientPackageExpireDate = "GetClientPackageExpireDate";


        }
          
        public static class DeviceLoan
        {
            public static string GetRechargeLoanCollectionByLoanNo = "GetRechargeLoanCollectionByLoanNo";
            public static string GetRechargeLoanCollectionSummaryByLoanNo = "GetRechargeLoanCollectionSummaryByLoanNo";
        }

    }
}
