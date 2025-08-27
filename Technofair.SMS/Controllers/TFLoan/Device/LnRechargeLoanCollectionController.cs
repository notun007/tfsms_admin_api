using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using Technofair.Service.TFLoan.Device;
using Technofair.Utiity.Http;
using Technofair.Utiity.Key;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Service.TFLoan.Device;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnRechargeLoanCollectionController : ControllerBase
    {
        private ILnRechargeLoanCollectionService service;
        private ILnDeviceLoanDisbursementService serviceDisbursement;
        private ITFACompanyCustomerService serviceCompanyCustomer;

        public LnRechargeLoanCollectionController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new LnRechargeLoanCollectionService(new LnRechargeLoanCollectionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceDisbursement = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("SaveRechargeLoanCollection")]
        public async Task<Operation> SaveRechargeLoanCollection(LnRechargeLoanCollectionViewModel obj)
        {
            Operation objOperation = new Operation();

            try
            {
                LnRechargeLoanCollection objCollection = new LnRechargeLoanCollection();

                objCollection.LoanId = obj.LoanId;
                objCollection.LenderId = obj.LenderId;
                objCollection.LoaneeId = obj.LoaneeId;

                objCollection.NetAmount = obj.NetAmount;
                objCollection.Remarks = obj.Remarks;
                objCollection.CollectionDate = obj.CollectionDate;

                objCollection.TransactionId = KeyGeneration.GenerateTimestamp();
                objCollection.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                objCollection.BnkBankId = obj.BnkBankId;

                objCollection.BnkBranchId = obj.BnkBranchId;
                objCollection.BnkAccountInfoId = obj.BnkAccountInfoId;
                objCollection.IsCancel = false;

                objCollection.CreatedBy = obj.CreatedBy;
                objCollection.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objCollection);
                objOperation.Message = "Withdrawan Successfull.";
            }
            catch(Exception exp)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to Withdraw";
            }

            return objOperation;
        }

        [HttpPost("GetAll")]
        public List<LnRechargeLoanCollection> GetAll()
        {
            List<LnRechargeLoanCollection> list = service.GetAll();
            return list;
        }

        
        [HttpGet("GetRechargeLoanCollectionByLoanNo")]
        public List<LnRechargeLoanCollectionViewModel> GetRechargeLoanCollectionByLoanNo( string loanNo)
        {

            List<LnRechargeLoanCollectionViewModel> objCollection = new List<LnRechargeLoanCollectionViewModel>();

            try
            {
               var objLoan = serviceDisbursement.GetAll().Where(x => x.LoanNo == loanNo).SingleOrDefault();

                if (objLoan != null)
                {
                    List<LnRechargeLoanCollection> objCollectionList = service.GetAll().Where(x => x.LoanId == objLoan.Id).ToList();

                    foreach(var collection in objCollectionList)
                    {
                        objCollection.Add(new LnRechargeLoanCollectionViewModel
                        {
                            Id = collection.Id,
                            LoaneeId = collection.LoaneeId,
                            LenderId = collection.LenderId,

                            //AnFPaymentMethodId = collection.AnFPaymentMethodId,

                            NetAmount = collection.NetAmount,
                            //PaymentChargePercent = collection.PaymentChargePercent,
                            //PaymentCharge = collection.PaymentCharge,

                            CollectionDate = collection.CollectionDate,
                            TransactionId = collection.TransactionId,
                            CreatedBy = collection.CreatedBy,
                            CreatedDate = collection.CreatedDate
                        });
                    }

                }
               // return objCollection;
            }
            catch (Exception exp)
            {
            }

            return objCollection;
        }

        [HttpGet("GetRechargeLoanCollectionSummaryByLoanNo")]
        public async Task<SmsAdminRechargeLoanCollectionSummaryViewModel> GetRechargeLoanCollectionSummaryByLoanNo(string appKey, string loanNo)
        {

            SmsAdminRechargeLoanCollectionSummaryViewModel objSmsAdminCollection = new SmsAdminRechargeLoanCollectionSummaryViewModel();

            try
            {
                #region Admin
                var objAdminRechargeCollection = service.GetRechargeLoanCollectionByLoanNo(loanNo);
                #endregion

                #region Sms
                var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);
                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;
                var url = smsApiBaseUrl + "/api/LnRechargeLoanCollection/GetRechargeLoanCollectionByLoanNo?loanNo=" + loanNo;
                var objSmsRechargeCollection = await Request<RechargeLoanCollectionSummaryViewModel, RechargeLoanCollectionSummaryViewModel>.GetObject(url);
                #endregion

                if (objSmsRechargeCollection != null && objAdminRechargeCollection != null)
                {
                    objSmsAdminCollection.LoanId = objSmsRechargeCollection.LoanId;
                    objSmsAdminCollection.LoanNo = loanNo;
                    //objSmsAdminCollection.SmsAmount = objSmsRechargeCollection.Amount;
                    //objSmsAdminCollection.SmsPaymentCharge = objSmsRechargeCollection.PaymentCharge;
                    objSmsAdminCollection.SmsNetAmount = objSmsRechargeCollection.NetAmount;

                    //objSmsAdminCollection.AdminAmount = objAdminRechargeCollection.Amount;
                    //objSmsAdminCollection.AdminPaymentCharge = objAdminRechargeCollection.PaymentCharge;
                    objSmsAdminCollection.AdminNetAmount = objAdminRechargeCollection.NetAmount;

                    //objSmsAdminCollection.DueAmount = objSmsRechargeCollection.NetAmount - objAdminRechargeCollection.NetAmount;

                    objSmsAdminCollection.NetDueAmount = objSmsRechargeCollection.NetAmount - objAdminRechargeCollection.NetAmount;
                }
            }
            catch(Exception exp)
            {
            }
           
            return objSmsAdminCollection;
        }
    }
}
