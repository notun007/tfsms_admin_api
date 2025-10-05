

using Microsoft.AspNetCore.Mvc;
using Technofair.Service.TFLoan.Device;
using Technofair.Utiity.Log;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Service.Common;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Service.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using Technofair.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFAdmin;
using Technofair.Model.TFLoan.Device;
using Technofair.Utiity.Http;
using Technofair.Model.ViewModel.TFLoan;
using Technofair.Utiity.Key;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLoanDisbursementController : ControllerBase
    {
        private ILnDeviceLoanDisbursementService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;
        private ICmnCompanyService serviceCompany;
        private ILnDeviceLoanDisbursementRequestObjectService serviceReqObject;

        private IAnFFinancialServiceProviderTypeService serviceProviderType;

        private IWebHostEnvironment _hostingEnvironment;

        private readonly ITFLogger _logger;
        public LnDeviceLoanDisbursementController(ITFLogger logger)
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompany = new CmnCompanyService(new CmnCompanyRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceReqObject = new LnDeviceLoanDisbursementRequestObjectService(new LnDeviceLoanDisbursementRequestObjectRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            serviceProviderType = new AnFFinancialServiceProviderTypeService(new AnFFinancialServiceProviderTypRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            this._logger = logger;

            //var dbfactory = new DatabaseFactory();
            //service = new LnDeviceLoanDisbursementService(new LnDeviceLoanDisbursementRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnDeviceLoanDisbursement> GetAll()
        {
            List<LnDeviceLoanDisbursement> list = service.GetAll();
            return list;
        }

        //Asad-2
        [HttpPost("GetLoanDisbursementDdlByLoaneeId")]
        public async Task<List<DropDownViewModel>> GetLoanDisbursementDdlByLoaneeId(int loaneeId)
        {
           var loans = await service.GetLoanDisbursementDdlByLoaneeId(loaneeId);
            return loans;
        }

        //New
        [HttpGet("GetLoanDisbursementByLoanNo")]
        public async Task<LnDeviceLoanDisbursement> GetLoanDisbursementByLoanNo(string loanNo)
        {
            return await service.GetLoanDisbursementByLoanNo(loanNo);
        }


        //Asad
        [HttpPost("GetLoanDisbursementByLoaneeId")]
        public async Task<List<LnDeviceLoanDisbursement>> GetLoanDisbursementByLoaneeId(int loaneeId)
        {
            return await service.GetLoanDisbursementByLoaneeId(loaneeId);
        }

        //Farida
        [HttpPost("GetLoanNoByLoaneeId")]
        public async Task<List<string>> GetLoanNoByLoaneeId(int loaneeId)
        {
            return await service.GetLoanNoByLoaneeId(loaneeId);
        }


        //New: 10092025
        [HttpPost("SaveDeviceLoanDisbursement")]
        public async Task<Operation> SaveDeviceLoanDisbursement([FromBody] LnDeviceLoanDisbursementViewModel obj)
        {
            Operation objReqOperation = new Operation();
            Operation objOperation = new Operation();
                      
            LnDeviceLoanDisbursementViewModel objPayload = new LnDeviceLoanDisbursementViewModel();

            CmnCompany objSolutionProvider = new CmnCompany();
            TFACompanyCustomer objCompanyCustomer = new TFACompanyCustomer();


            try
            {

                #region Admin 

                objSolutionProvider = serviceCompany.GetSolutionProvider();
                objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(obj.LoaneeCode);

                LnDeviceLoanDisbursementRequestObject objRequest = new LnDeviceLoanDisbursementRequestObject();


                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/GetLoanDisbursementByLoanNo?loanNo=" + obj.LoanNo;

                LnDeviceLoanDisbursement objLnDeviceLoanDisbursement = await Request<LnDeviceLoanDisbursement, LnDeviceLoanDisbursement>.GetObject(url);

                if (objLnDeviceLoanDisbursement != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Duplicate loan number in admin database";
                    return objOperation;
                }


                var objExist = await service.GetLoanDetailsByLoanNo(obj.LoanNo);

                if (objExist != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Duplicate loan number in sms database";
                    return objOperation;
                }
                                      

                objRequest.Id = obj.Id;
                objRequest.LoanNo = obj.LoanNo;
                objRequest.LenderId = objSolutionProvider.Id;
                objRequest.LoaneeId = objCompanyCustomer.Id;

                objRequest.LenderCode = obj.LenderCode;
                objRequest.LoaneeCode = obj.LoaneeCode;

              
                objRequest.NumberOfDevice = obj.NumberOfDevice;
                objRequest.Rate = obj.Rate;
                objRequest.TotalAmount = obj.TotalAmount;

                objRequest.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                objRequest.DueAmountPerDevice = obj.DueAmountPerDevice;
                objRequest.DownPaymentAmount = obj.DownPaymentAmount;

              
                objRequest.LoanAmount = obj.LoanAmount;
                objRequest.LnTenureId = obj.LnTenureId;
                objRequest.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

                objRequest.InstallmentStartDate = obj.InstallmentStartDate;
                objRequest.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                objRequest.AnFFinancialServiceProviderId = obj.AnFFinancialServiceProviderId;
                

                objRequest.AnFBranchId = obj.AnFBranchId;
                objRequest.AnFAccountInfoId = obj.AnFAccountInfoId;

                objRequest.Remarks = obj.Remarks;
                objRequest.TransactionId = KeyGeneration.GenerateTimestamp();
                objRequest.AnFAccountInfoId = obj.AnFAccountInfoId;

                objRequest.IsSmsSuccess = false;
                objRequest.IsAdminSuccess = false;
                objRequest.IsSuccess = false;

                objRequest.CreatedBy = obj.CreatedBy;
                objRequest.CreatedDate = DateTime.Now;

                //Service + Repository likhben for req object....then uncomment
                objReqOperation = await serviceReqObject.Save(objRequest);
                objReqOperation.Message = "Request Saved Successfully.";

                if (!objReqOperation.Success)
                {
                    //objScheduledLoanResponse.Success = objReqOperation.Success;
                    //objScheduledLoanResponse.Message = objReqOperation.Message;
                    return objOperation;
                }


                #endregion'

                if (objReqOperation.Success)
                {
                    objPayload.Id = obj.Id;
                    objPayload.LoanNo = obj.LoanNo; //service.NextLoanNo();
                    objPayload.LenderId = obj.LenderId;
                    objPayload.LoaneeId = obj.LoaneeId;

                    objPayload.LenderCode = obj.LenderCode;
                    objPayload.LoaneeCode = obj.LoaneeCode;

                    objPayload.NumberOfDevice = obj.NumberOfDevice;
                    objPayload.Rate = obj.Rate;

                    objPayload.TotalAmount = obj.TotalAmount;
                    objPayload.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                    objPayload.DueAmountPerDevice = obj.DueAmountPerDevice;
                    objPayload.DownPaymentAmount = obj.DownPaymentAmount;
                    objPayload.LoanAmount = obj.LoanAmount;
                    objPayload.LnTenureId = obj.LnTenureId;
                    objPayload.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

                    objPayload.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                    objPayload.AnFFinancialServiceProviderId = obj.AnFFinancialServiceProviderId;
                    
                    objPayload.AnFBranchId = obj.AnFBranchId;
                    objPayload.AnFAccountInfoId = obj.AnFAccountInfoId;
                    objPayload.TransactionId = objRequest.TransactionId;

                    objPayload.Remarks = obj.Remarks;
                    objPayload.InstallmentStartDate = obj.InstallmentStartDate;
                    objPayload.IsClosed = false;
                    objPayload.CreatedBy = obj.CreatedBy;
                    objPayload.CreatedDate = DateTime.Now;

                    //var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);

                    smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                     url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/SaveLoanDisbursement";

                    objOperation = await Request<LnDeviceLoanDisbursementViewModel, Operation>.Post(url, objPayload);

                }


                if (!objOperation.Success)
                {
                    return objOperation;
                }


                //service lekha hola unccommentt korben
                var objCollectionRequest = serviceReqObject.GetById((Int64)objReqOperation.OperationId);

                // ata o unccommentt korben
                if (objCollectionRequest == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Falied to retrive request object from admin database, loan recovery succeeded in sms database";

                    return objOperation;
                }


                // ata o unccommentt korben
                objCollectionRequest.IsSmsSuccess = true;
                objCollectionRequest.ModifiedBy = obj.CreatedBy;
                objCollectionRequest.ModifiedDate = DateTime.Now;
                objReqOperation = serviceReqObject.Update(objCollectionRequest);

                if (!objReqOperation.Success)
                {

                    objOperation.Success = false;
                    objOperation.Message = "Falied to update request object in admin database, loan recovery succeeded in sms database.";

                    return objOperation;
                }



                objLnDeviceLoanDisbursement = new LnDeviceLoanDisbursement();


                var objExit = service.GetById(obj.Id);

                //Ay validation gulo collection a deya nay...atao collection a deta habe...
                //Ay validation habe TransactionId deya in both LoanDisbursementRequestObject and LoanDisbursement table a...jodi exist na kare then insert habe shudhu..
                //Kono update habe na...
                //r jodi same trx id entry thake then just return and message = "Transaction Id exist...";


                if (objOperation.Success == true)
                {
                   
                        objLnDeviceLoanDisbursement.Id = obj.Id;
                        objLnDeviceLoanDisbursement.LoanNo = objPayload.LoanNo;
                        objLnDeviceLoanDisbursement.LenderId = obj.LenderId;
                        objLnDeviceLoanDisbursement.LoaneeId = obj.LoaneeId;

                        objLnDeviceLoanDisbursement.LenderCode = obj.LenderCode;
                        objLnDeviceLoanDisbursement.LoaneeCode = obj.LoaneeCode;

                        objLnDeviceLoanDisbursement.NumberOfDevice = obj.NumberOfDevice;
                        objLnDeviceLoanDisbursement.Rate = obj.Rate;

                        objLnDeviceLoanDisbursement.TotalAmount = obj.TotalAmount;
                        objLnDeviceLoanDisbursement.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
                        objLnDeviceLoanDisbursement.DueAmountPerDevice = obj.DueAmountPerDevice;
                        objLnDeviceLoanDisbursement.DownPaymentAmount = obj.DownPaymentAmount;
                        objLnDeviceLoanDisbursement.LoanAmount = obj.LoanAmount;
                        objLnDeviceLoanDisbursement.LnTenureId = obj.LnTenureId;
                        objLnDeviceLoanDisbursement.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

                        objLnDeviceLoanDisbursement.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                        objLnDeviceLoanDisbursement.AnFFinancialServiceProviderId = obj.AnFFinancialServiceProviderId;
                        
                        objLnDeviceLoanDisbursement.AnFBranchId = obj.AnFBranchId;
                        objLnDeviceLoanDisbursement.AnFAccountInfoId = obj.AnFAccountInfoId;
                        objLnDeviceLoanDisbursement.TransactionId = objRequest.TransactionId;

                        objLnDeviceLoanDisbursement.Remarks = obj.Remarks;
                        objLnDeviceLoanDisbursement.InstallmentStartDate = obj.InstallmentStartDate;
                        objLnDeviceLoanDisbursement.IsClosed = false;

                        objLnDeviceLoanDisbursement.CreatedBy = obj.CreatedBy;
                        objLnDeviceLoanDisbursement.CreatedDate = DateTime.Now;
                        objOperation = await service.Save(objLnDeviceLoanDisbursement);
                        objOperation.Message = "Device Loan Disbursement Created Successfully.";
                    
                   
                }


                //Do not remove this block
                //if (objOperation.Success)
                //{
                //    objScheduledLoanResponse.Success = objOperation.Success;
                //    objScheduledLoanResponse.Message = "Loan Collection succeeded in admin database.";
                //}
                //else
                //{
                //    objScheduledLoanResponse.Success = objOperation.Success;
                //    objScheduledLoanResponse.Message = "Loan Collection failed in admin database.";
                //    return objScheduledLoanResponse;
                //}


                if (objOperation.Success)
                {
                    //Uncomment after writting service
                    objCollectionRequest = serviceReqObject.GetById((Int64)objReqOperation.OperationId);

                    objCollectionRequest.IsAdminSuccess = true;
                    objCollectionRequest.IsSuccess = true;
                    objCollectionRequest.ModifiedBy = obj.CreatedBy;
                    objCollectionRequest.ModifiedDate = DateTime.Now;
                    objOperation = serviceReqObject.Update(objCollectionRequest);

                    if (objOperation.Success)
                    {
                        objOperation.Success = objOperation.Success;
                        objOperation.Message = "Loan recovery succeeded.";
                    }
                    else
                    {
                        objOperation.Success = objOperation.Success;
                        objOperation.Message = "Failed to finalize loan recovery in admin database, please sync.";

                        return objOperation;
                    }
                }
               

            }
            catch (Exception exp)
            {
                throw exp;
            }

            return objOperation;
        }



        //Old: 10092025

        //[HttpPost("Save")]
        //public async Task<Operation> Save([FromBody] LnDeviceLoanDisbursementViewModel obj)
        //{
        //    Operation objOperation = new Operation();

        //    var objDisbursementExit = service.GetById(obj.Id);

        //    LnDeviceLoanDisbursementViewModel objPayload = new LnDeviceLoanDisbursementViewModel();

        //    if (objDisbursementExit == null)
        //    {
        //        #region Save To SMS Database by SMS Api



        //        objPayload.Id = obj.Id;
        //        objPayload.LoanNo = obj.LoanNo; //service.NextLoanNo();
        //        objPayload.LenderId = obj.LenderId;
        //        objPayload.LoaneeId = obj.LoaneeId;

        //        objPayload.LenderCode = obj.LenderCode;
        //        objPayload.LoaneeCode = obj.LoaneeCode;

        //        objPayload.NumberOfDevice = obj.NumberOfDevice;
        //        objPayload.Rate = obj.Rate;

        //        objPayload.TotalAmount = obj.TotalAmount;
        //        objPayload.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
        //        objPayload.DueAmountPerDevice = obj.DueAmountPerDevice;
        //        objPayload.DownPaymentAmount = obj.DownPaymentAmount;
        //        objPayload.LoanAmount = obj.LoanAmount;
        //        objPayload.LnTenureId = obj.LnTenureId;
        //        objPayload.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

        //        objPayload.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
        //        objPayload.BnkBankId = obj.BnkBankId;
        //        objPayload.BnkBranchId = obj.BnkBranchId;
        //        objPayload.BnkAccountInfoId = obj.BnkAccountInfoId;
        //        objPayload.TransactionId =KeyGeneration.GenerateTimestamp();

        //        objPayload.Remarks = obj.Remarks;
        //        objPayload.InstallmentStartDate = obj.InstallmentStartDate;
        //        objPayload.IsClosed = false;
        //        objPayload.CreatedBy = obj.CreatedBy;
        //        objPayload.CreatedDate = DateTime.Now;

        //        var objCompanyCustomer = serviceCompanyCustomer.GetById(obj.LoaneeId);

        //        var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

        //        var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/SaveLoanDisbursement";

        //        objOperation = await Request<LnDeviceLoanDisbursementViewModel, Operation>.Post(url, objPayload);


        //        #endregion
        //    }


        //    LnDeviceLoanDisbursement objLnDeviceLoanDisbursement = new LnDeviceLoanDisbursement();


        //    var objExit = service.GetById(obj.Id);

        //    //Ay validation gulo collection a deya nay...atao collection a deta habe...
        //    //Ay validation habe TransactionId deya in both LoanDisbursementRequestObject and LoanDisbursement table a...jodi exist na kare then insert habe shudhu..
        //    //Kono update habe na...
        //    //r jodi same trx id entry thake then just return and message = "Transaction Id exist...";


        //    if (objOperation.Success == true)
        //    {
        //        if (objExit == null)
        //        {
        //            objLnDeviceLoanDisbursement.Id = obj.Id;
        //            objLnDeviceLoanDisbursement.LoanNo = objPayload.LoanNo;
        //            objLnDeviceLoanDisbursement.LenderId = obj.LenderId;
        //            objLnDeviceLoanDisbursement.LoaneeId = obj.LoaneeId;

        //            objLnDeviceLoanDisbursement.LenderCode = obj.LenderCode;
        //            objLnDeviceLoanDisbursement.LoaneeCode = obj.LoaneeCode;

        //            objLnDeviceLoanDisbursement.NumberOfDevice = obj.NumberOfDevice;
        //            objLnDeviceLoanDisbursement.Rate = obj.Rate;

        //            objLnDeviceLoanDisbursement.TotalAmount = obj.TotalAmount;
        //            objLnDeviceLoanDisbursement.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
        //            objLnDeviceLoanDisbursement.DueAmountPerDevice = obj.DueAmountPerDevice;
        //            objLnDeviceLoanDisbursement.DownPaymentAmount = obj.DownPaymentAmount;
        //            objLnDeviceLoanDisbursement.LoanAmount = obj.LoanAmount;
        //            objLnDeviceLoanDisbursement.LnTenureId = obj.LnTenureId;
        //            objLnDeviceLoanDisbursement.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

        //            objLnDeviceLoanDisbursement.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
        //            objLnDeviceLoanDisbursement.BnkBankId = obj.BnkBankId;
        //            objLnDeviceLoanDisbursement.BnkBranchId = obj.BnkBranchId;
        //            objLnDeviceLoanDisbursement.BnkAccountInfoId = obj.BnkAccountInfoId;
        //            objLnDeviceLoanDisbursement.TransactionId = objPayload.TransactionId;

        //            objLnDeviceLoanDisbursement.Remarks = obj.Remarks;
        //            objLnDeviceLoanDisbursement.InstallmentStartDate = obj.InstallmentStartDate;
        //            objLnDeviceLoanDisbursement.IsClosed = false;

        //            objLnDeviceLoanDisbursement.CreatedBy = obj.CreatedBy;
        //            objLnDeviceLoanDisbursement.CreatedDate = DateTime.Now;
        //            objOperation = await service.Save(objLnDeviceLoanDisbursement);
        //            objOperation.Message = "Device Loan Disbursement Created Successfully.";
        //        }
        //        if (objExit != null)
        //        {
        //            objExit.LenderId = obj.LenderId;
        //            objExit.LoaneeId = obj.LoaneeId;
        //            objExit.NumberOfDevice = obj.NumberOfDevice;
        //            objExit.Rate = obj.Rate;

        //            objLnDeviceLoanDisbursement.TotalAmount = obj.TotalAmount;
        //            objLnDeviceLoanDisbursement.PaymentAmountPerDevice = obj.PaymentAmountPerDevice;
        //            objLnDeviceLoanDisbursement.DueAmountPerDevice = obj.DueAmountPerDevice;
        //            objLnDeviceLoanDisbursement.DownPaymentAmount = obj.DownPaymentAmount;
        //            objLnDeviceLoanDisbursement.LoanAmount = obj.LoanAmount;
        //            objLnDeviceLoanDisbursement.LnTenureId = obj.LnTenureId;
        //            objLnDeviceLoanDisbursement.MonthlyInstallmentAmount = obj.MonthlyInstallmentAmount;

        //            objLnDeviceLoanDisbursement.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
        //            objLnDeviceLoanDisbursement.BnkBankId = obj.BnkBankId;
        //            objLnDeviceLoanDisbursement.BnkBranchId = obj.BnkBranchId;
        //            objLnDeviceLoanDisbursement.BnkAccountInfoId = obj.BnkAccountInfoId;
        //            objLnDeviceLoanDisbursement.TransactionId = obj.TransactionId;

        //            objExit.Remarks = obj.Remarks;
        //            objExit.InstallmentStartDate = obj.InstallmentStartDate;
        //            objExit.ModifiedBy = obj.ModifiedBy;
        //            objExit.ModifiedDate = DateTime.Now;
        //            objOperation = service.Update(objExit);
        //            objOperation.Message = "Device Loan Disbursement Updated Successfully.";
        //        }
        //    }
        //    return objOperation;
        //}





        [HttpPost("GetDeviceLoanDisbursement")]
        public Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursement()
        {
            return service.GetDeviceLoanDisbursement();
        }


        //New: Farida- 01092025

        [HttpPost("GetDeviceLoanDisbursementByLoaneeCode")]
        public async Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursementByLoaneeCode(string loaneeCode)
        {
            _logger.LogError("loaneeCode: " + loaneeCode);

            TFACompanyCustomer objCompanyCustomer = new TFACompanyCustomer();
            CmnCompany objSolutionProvider = new CmnCompany();

            List<LnDeviceLoanDisbursement> objAdminDisbursementList = new List<LnDeviceLoanDisbursement>();

            List<LnDeviceLoanDisbursementViewModel> objSmsDisbursementList = new List<LnDeviceLoanDisbursementViewModel>();

            var objServiceProviderList = serviceProviderType.GetAll();

            List<LnDeviceLoanDisbursementViewModel> result = new List<LnDeviceLoanDisbursementViewModel>();

            try
            {

                objAdminDisbursementList = await service.GetDeviceLoanDisbursementByLoaneeCode(loaneeCode);

                objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(loaneeCode);
             
                var objCompanyCustomerList = serviceCompanyCustomer.GetAll();
                var objSolutionProviderList = serviceCompany.GetAll();


                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/GetDeviceLoanDisbursementByLoaneeCode?loaneeCode=" + loaneeCode;

                objSmsDisbursementList = await Request<LnDeviceLoanDisbursementViewModel, LnDeviceLoanDisbursementViewModel>.GetCollecttion(url);

                result = (from smsDisburse in objSmsDisbursementList
                          join adminDisburse in objAdminDisbursementList
                          on smsDisburse.LoanNo equals adminDisburse.LoanNo
                          join loanee in objCompanyCustomerList
                             on smsDisburse.LoaneeCode equals loanee.Code
                             join lender in objSolutionProviderList
                             on smsDisburse.LenderCode equals lender.Code

                            join serviceProvider in objServiceProviderList
                            on adminDisburse.AnFFinancialServiceProviderTypeId equals serviceProvider.Id

                             select new LnDeviceLoanDisbursementViewModel
                             {
                                Id = smsDisburse.Id,
                                LoanNo = smsDisburse.LoanNo,
                                LenderId = lender.Id,
                                LoaneeId = loanee.Id,

                                LenderCode = smsDisburse.LenderCode,
                                LoaneeCode = smsDisburse.LoaneeCode,

                                PaymentAmountPerDevice = smsDisburse.PaymentAmountPerDevice,
                                DueAmountPerDevice = smsDisburse.DueAmountPerDevice,
                                LnTenureId = smsDisburse.LnTenureId,
                                MonthlyInstallmentAmount = smsDisburse.MonthlyInstallmentAmount,
                                                                
                                NumberOfDevice = smsDisburse.NumberOfDevice,
                                Rate = smsDisburse.Rate,
                                TotalAmount = smsDisburse.TotalAmount,
                                DownPaymentAmount = smsDisburse.DownPaymentAmount,
                                LoanAmount = smsDisburse.LoanAmount,
                                Remarks = smsDisburse.Remarks,
                                LenderName = smsDisburse.LenderName,
                                LoaneeName = smsDisburse.LoaneeName,
                                CreatedDate = smsDisburse.CreatedDate,
                                InstallmentStartDate = smsDisburse.InstallmentStartDate,
                                IsScheduled = smsDisburse.IsScheduled,

                                RechargeCollectionAmount = smsDisburse.RechargeCollectionAmount,
                                ManualCollectionAmount = smsDisburse.ManualCollectionAmount,
                                TotalCollectionAmount = smsDisburse.TotalCollectionAmount,
                                Balance = smsDisburse.Balance,

                                 AnFFinancialServiceProviderTypeId = adminDisburse.AnFFinancialServiceProviderTypeId,
                                 AnFFinancialServiceProviderId = adminDisburse.AnFFinancialServiceProviderId,
                                
                                 AnFBranchId = adminDisburse.AnFBranchId,
                                 AnFAccountInfoId = adminDisburse.AnFAccountInfoId,
                                 TransactionId = adminDisburse.TransactionId,
                                 FinancialServiceProviderTypeName = serviceProvider.Name

                             }).ToList();
                               
            }
            catch(Exception exp)
            {
                throw exp;
            }

            return result;
        }

        //New: 25082025
        [HttpPost("GetDeviceLoanDisbursementByAppKey")]
        public async Task<List<LnDeviceLoanDisbursementViewModel>> GetDeviceLoanDisbursementByAppKey(string appKey)
        {
            _logger.LogError("AppKey: " + appKey);

            var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

            var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

            var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/GetDeviceLoanDisbursementByAppKey?appKey=" + appKey;

            var list = await Request<LnDeviceLoanDisbursementViewModel, LnDeviceLoanDisbursementViewModel>.GetCollecttion(url);

            return list;
        }

        [HttpPost("GenerateLoanSchedule")]
        public async Task<Operation> GenerateLoanSchedule(LOanScheduleRequestViewModel objPayload)
        {                                    
            var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(objPayload.LoaneeCode);

            var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

            var url = smsApiBaseUrl + "/api/LnDeviceLoanDisbursement/GenerateLoanSchedule";

           var objOperation = await Request<LOanScheduleRequestViewModel, Operation>.Post(url, objPayload);

            return objOperation;
        }

        [HttpGet("GetDeviceLoanScheduleByLoanNo")]
        public async Task<List<LnDeviceLoanScheduleCollectionViewModel>> GetDeviceLoanScheduleByLoanId(string loaneeCode, string loanNo)
        {
            try
            {
                //_logger.LogError("loaneeId: " + loaneeId.ToString() + "loanId: " + loanId.ToString());


                var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByLoaneeCode(loaneeCode);

                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                _logger.LogError("smsApiBaseUrl: " + smsApiBaseUrl);

                var url = smsApiBaseUrl + "/api/LnDeviceLoanSchedule/GetDeviceLoanScheduleByLoanNo?loanNo=" + loanNo;

                _logger.LogError("url: " + url);

                var list = await Request<LnDeviceLoanScheduleCollectionViewModel, LnDeviceLoanScheduleCollectionViewModel>.GetCollecttion(url);
                return list;
            }

            catch (Exception ex)
            {
                _logger.LogError("Exception: " + ex.Message);
                throw ex.InnerException;
            }
                        

        }
    }
}
