using Microsoft.AspNetCore.Authorization;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFLoan;
using Technofair.Service.TFLoan.Device;
using Technofair.Utiity.Http;
using Technofair.Utiity.Key;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.TFLoan.Device;
//using TFSMS.Admin.Model.Utility;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.Common;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Service.TFLoan.Device;


namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLoanCollectionController : ControllerBase
    {        
        private ILnDeviceLoanCollectionService service;
        private ILnDeviceLoanCollectionRequestObjectService serviceReqObject;

        private ICmnCompanyService serviceCompany;
        private ITFACompanyCustomerService serviceCompanyCustomer;


        private IWebHostEnvironment _hostingEnvironment;

        public LnDeviceLoanCollectionController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLoanCollectionService(new LnDeviceLoanCollectionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceReqObject = new LnDeviceLoanCollectionRequestObjectService(new LnDeviceLoanCollectionRequestObjectRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            serviceCompany = new CmnCompanyService(new CmnCompanyRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));

        }

        [HttpPost("GetAll")]
        public List<LnDeviceLoanCollection> GetAll()
        {
            List<LnDeviceLoanCollection> list = service.GetAll();
            return list;
        }

        [HttpPost("RecoverScheduledLoan")]
        public async Task<RecoverScheduledLoanResponseViewModel> RecoverScheduledLoan([FromBody] ScheduleLoanRecoverViewModel obj)
        {
            Operation objReqOperation = new Operation();
            Operation objOperation = new Operation();

            RecoverScheduledLoanResponseViewModel objScheduledLoanResponse = new RecoverScheduledLoanResponseViewModel { Success = false, Message = "Failed to recover loan" };

            LnDeviceLoanCollectionViewModel objPayload = new LnDeviceLoanCollectionViewModel();
            
            CmnCompany objSolutionProvider = new CmnCompany();
            TFACompanyCustomer objCompanyCustomer = new TFACompanyCustomer();


                try
                {

                #region Admin 
                
                objSolutionProvider = serviceCompany.GetSolutionProvider();
                objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(obj.LoaneeCode);

                LnDeviceLoanCollectionRequestObject objRequest = new LnDeviceLoanCollectionRequestObject();
                objRequest.Id = obj.Id;
                objRequest.LoanId = obj.LoanId;
                objRequest.LnLoanCollectionTypeId = obj.LnLoanCollectionTypeId;
                objRequest.AnFPaymentMethodId = obj.AnFPaymentMethodId;
                objRequest.LenderId = objSolutionProvider.Id;
                objRequest.LoaneeId = objCompanyCustomer.Id;

                objRequest.LenderCode = obj.LenderCode;
                objRequest.LoaneeCode = obj.LoaneeCode;

                objRequest.Amount = obj.Amount;
                objRequest.Remarks = obj.Remarks;
                objRequest.CollectionDate = obj.CollectionDate;
                objRequest.TransactionId = KeyGeneration.GenerateTimestamp();
                objRequest.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                objRequest.BnkBankId = obj.BnkBankId;

                objRequest.BnkBranchId = obj.BnkBranchId;
                objRequest.BnkAccountInfoId = obj.BnkAccountInfoId;

                objRequest.IsSmsSuccess = false;
                objRequest.IsAdminSuccess = false;
                objRequest.IsSuccess = false;

                objRequest.IsCancel = false;
                objRequest.CancelBy = obj.CancelBy;
                objRequest.CancelDate = obj.CancelDate;

                objRequest.CreatedBy = obj.CreatedBy;
                objRequest.CreatedDate = DateTime.Now;

                objReqOperation = await serviceReqObject.Save(objRequest);

                if (!objReqOperation.Success)
                {
                    objScheduledLoanResponse.Success = objReqOperation.Success;
                    objScheduledLoanResponse.Message = objReqOperation.Message;
                    return objScheduledLoanResponse;
                }

                
                #endregion'

                    if (objReqOperation.Success)
                    {
                        objPayload.loanNo = obj.LoanNo;
                        objPayload.LoanId = obj.LoanId;
                        objPayload.LnLoanCollectionTypeId = obj.LnLoanCollectionTypeId;
                        objPayload.AnFPaymentMethodId = obj.AnFPaymentMethodId;
                        objPayload.LenderCode = obj.LenderCode;
                        objPayload.LoaneeCode = obj.LoaneeCode;
                        objPayload.Amount = obj.Amount;
                        objPayload.Remarks = obj.Remarks;
                        objPayload.CollectionDate = obj.CollectionDate;
                        objPayload.TransactionId = objRequest.TransactionId;
                        objPayload.IsCancel = false;
                        objPayload.CancelBy = obj.CancelBy;
                        objPayload.CancelDate = obj.CancelDate;
                        objPayload.CreatedBy = obj.CreatedBy;
                        objPayload.CreatedDate = DateTime.Now;
                        var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                        var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/RecoverScheduledLoan";

                    objScheduledLoanResponse = await Request<LnDeviceLoanCollectionViewModel, RecoverScheduledLoanResponseViewModel>.Post(url, objPayload);

                    //objOperation.Success = objScheduledLoanResponse.Success;
                    //objOperation.Message = objScheduledLoanResponse.Message;

                }


                if (!objScheduledLoanResponse.Success)
                {
                    return objScheduledLoanResponse;
                }

                //if(objOperation.Success == true)
                //{
                    
                   var objCollectionRequest = serviceReqObject.GetById((Int64)objReqOperation.OperationId);

                    if(objCollectionRequest == null)
                    {
                    objScheduledLoanResponse.Success = false;
                    objScheduledLoanResponse.Message = "Falied to retrive request object from admin database, loan recovery succeeded in sms database";

                    return objScheduledLoanResponse;
                    }
                    
                    
                    objCollectionRequest.IsSmsSuccess = true;
                    objCollectionRequest.Amount = objScheduledLoanResponse.ActualCollectionAmount;
                    objCollectionRequest.ModifiedBy = obj.CreatedBy;
                    objCollectionRequest.ModifiedDate = DateTime.Now;
                    objReqOperation = serviceReqObject.Update(objCollectionRequest);

                    if (!objReqOperation.Success)
                    {
                       
                    objScheduledLoanResponse.Success = false;
                    objScheduledLoanResponse.Message = "Falied to update request object in admin database, loan recovery succeeded in sms database.";

                    return objScheduledLoanResponse;
                    }

                    //if (objReqOperation.Success == true)
                    //{

                        LnDeviceLoanCollection objCollection = new LnDeviceLoanCollection();

                        objCollection.Id = obj.Id;
                        objCollection.LoanId = obj.LoanId;
                        objCollection.LnLoanCollectionTypeId = obj.LnLoanCollectionTypeId;
                        objCollection.AnFPaymentMethodId = obj.AnFPaymentMethodId;
                        objCollection.LenderId = objSolutionProvider.Id;
                        objCollection.LoaneeId = objCompanyCustomer.Id;

                        objCollection.LenderCode = obj.LenderCode;
                        objCollection.LoaneeCode = obj.LoaneeCode;

                        objCollection.Amount = objScheduledLoanResponse.ActualCollectionAmount;
                        objCollection.Remarks = obj.Remarks;
                        objCollection.CollectionDate = obj.CollectionDate;
                        objCollection.TransactionId = objRequest.TransactionId;

                        objCollection.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                        objCollection.BnkBankId = obj.BnkBankId;

                        objCollection.BnkBranchId = obj.BnkBranchId;
                        objCollection.BnkAccountInfoId = obj.BnkAccountInfoId;

                        objCollection.IsCancel = false;
                        objCollection.CancelBy = obj.CancelBy;
                        objCollection.CancelDate = obj.CancelDate;

                        objCollection.CreatedBy = obj.CreatedBy;
                        objCollection.CreatedDate = DateTime.Now;
                        objOperation = await service.Save(objCollection);

                        

                if (objOperation.Success)
                   {
                     objScheduledLoanResponse.Success = objOperation.Success;
                     objScheduledLoanResponse.Message = "Loan Collection succeeded in admin database.";
                   }
                   else
                   {
                    objScheduledLoanResponse.Success = objOperation.Success;
                    objScheduledLoanResponse.Message = "Loan Collection failed in admin database.";
                     return objScheduledLoanResponse;
                   }
                        

                        if (objOperation.Success)
                        {
                            objCollectionRequest = serviceReqObject.GetById((Int64)objReqOperation.OperationId);

                            objCollectionRequest.IsAdminSuccess = true;
                            objCollectionRequest.IsSuccess = true;
                            objCollectionRequest.ModifiedBy = obj.CreatedBy;
                            objCollectionRequest.ModifiedDate = DateTime.Now;
                            objOperation = serviceReqObject.Update(objCollectionRequest);

                            if (objOperation.Success)
                            {
                        objScheduledLoanResponse.Success = objOperation.Success;
                        objScheduledLoanResponse.Message = "Loan recovery succeeded.";
                            }
                            else
                            {
                        objScheduledLoanResponse.Success = objOperation.Success;
                        objScheduledLoanResponse.Message = "Failed to finalize loan recovery in admin database, please sync.";

                           return objScheduledLoanResponse;
                            }
                    }
                    //}
                    
                //}

                }
                catch (Exception exp)
                {
                    throw exp;
                }
                          
            return objScheduledLoanResponse;
        }

        [Authorize]
        [HttpPost("GetLoanCollection")]
        public List<LnDeviceLoanCollectionViewModel> GetLoanCollection(int lenderId, int loaneeId)
        {
            List<LnDeviceLoanCollectionViewModel> list = service.GetLoanCollection(lenderId, loaneeId);
            return list;
        }

        [HttpPost("GetDeviceLoanInfo")]
        public DeviceLoanInfoViewModel GetDeviceLoanInfo(int lenderId, int loaneeId)
        {
            var loanInfo = service.GetDeviceLoanInfo(lenderId, loaneeId);
            return loanInfo;
        }

        [HttpGet("GetDeviceLoanInfoByAppKey")]
        public DeviceLoanInfoViewModel GetDeviceLoanInfoByAppKey(string appKey)
        {
            var loanInfo = service.GetDeviceLoanInfoByAppKey(appKey);
            return loanInfo;
        }


        [HttpGet("FetchCurrentLoanSchedule")]
        public async Task<LoanScheduleInfoViewModel> FetchCurrentLoanSchedule(string lenderCode, string loaneeCode)
        {
            LoanScheduleInfoViewModel objDeviceLoanInfo = new LoanScheduleInfoViewModel();

            try
            {
                var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(loaneeCode);
                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/FetchCurrentLoanSchedule?lenderCode=" + lenderCode + "&loaneeCode=" + loaneeCode;

                objDeviceLoanInfo = await Request<LoanScheduleInfoViewModel, LoanScheduleInfoViewModel>.GetObject(url);
            }
            catch(Exception exp)
            {
                throw exp;
            }

            return objDeviceLoanInfo;
        }

        //[HttpPost("RecoverScheduledLoan")]
        //public async Task<Operation> RecoverScheduledLoan(LnDeviceLoanCollectionViewModel objCollection)
        //{
        //    Operation objOperation = new Operation();

        //    try
        //    {
        //        var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(objCollection.LoaneeCode);

        //        var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

        //        var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/RecoverScheduledLoan";

        //        objOperation = await Request<LnDeviceLoanCollectionViewModel, Operation>.Post(url, objCollection);
        //    }
        //    catch (Exception exp)
        //    {
        //        throw exp;
        //    }

        //    return objOperation;
        //}


        [HttpGet("BuildInstallmentSettlementPlan")]
        public async Task<List<InstallmentSettlementPlan>> BuildInstallmentSettlementPlan(string loaneeCode, string loanNo, decimal amount)
        {
            List<InstallmentSettlementPlan> objPlan = new List<InstallmentSettlementPlan>();

            try
            {
                var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(loaneeCode);
                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/BuildInstallmentSettlementPlan?loanNo=" + loanNo + "&amount=" + amount;

                objPlan = await Request<InstallmentSettlementPlan, InstallmentSettlementPlan>.GetCollecttion(url);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return objPlan;
        }
    }
}
