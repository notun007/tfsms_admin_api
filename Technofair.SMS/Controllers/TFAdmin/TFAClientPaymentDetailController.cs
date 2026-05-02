using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Technofair.Lib.Model;
using Technofair.Model.TFLoan.Device;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Utiity.Http;
using Technofair.Utiity.Log;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.Subscription;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Service.TFAdmin;


namespace TFSMS.Admin.Controllers.TFAdmin
{
   
    [Route("api/[Controller]")]
    [ApiController]
    public class TFAClientPaymentDetailController : ControllerBase
    {

        private ITFAClientPaymentDetailService service;
        private ITFACompanyCustomerService serviceCompanyCustomer;
        private ITFAClientPaymentDetailRepository repository;
        private readonly ITFLogger _logger;
	
	
        AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();
        public TFAClientPaymentDetailController(ITFLogger logger)
        {
            repository = new TFAClientPaymentDetailRepository(dbfactory);
            service = new TFAClientPaymentDetailService(repository, new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            this._logger = logger;
        }


        [HttpGet("GetAllLoanBalanceByCompletedSchedules")]
        public async Task<LoanBalanceViewModel> GetAllLoanBalanceByCompletedSchedules(string appKey)
        {
            var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

            var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

            var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/GetAllLoanBalanceByCompletedSchedules";

            LoanBalanceViewModel objLoanBalance = await Request<LoanBalanceViewModel, LoanBalanceViewModel>.GetObject(url);

            return objLoanBalance;
        }

        //[Authorize(Policy = "Authenticated")]
        [HttpGet("VerifyClientPackageByAppKey")]
        public async Task<Operation> VerifyClientPackageByAppKey(string appKey)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {
                
                var objCompanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

                if (objCompanyCustomer == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "You Are Not Authorized.";
                    _logger.LogError(objOperation.Message);
                    return objOperation;
                }

                if (objCompanyCustomer.IsActive == false)
                {
                    objOperation.Success = false;
                    objOperation.Message = "You Are Not Authorized.";
                    _logger.LogError(objOperation.Message);
                    return objOperation;
                }



                //Farida Added On 05-04-2026

                var objLastPayment = service.GetLastPaymentByAppKey(appKey);

                if (objLastPayment == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Something went wrong, please try again later";
                    _logger.LogError(objOperation.Message);
                    return objOperation;
                }

                if (objLastPayment != null)
                {
                    if (objLastPayment.ExpireDate >= DateTime.Now.Date)
                    {
                        objOperation.Success = true;
                        objOperation.Message = "Package Still Alive";
                        _logger.LogError(objOperation.Message);
                    }
                    else
                    {
                        objOperation.Success = false;
                        objOperation.Message = "এসএমএস সফ্টওয়ার এর মেয়াদ শেষ। দয়া করে বিল পরিশোধ করে নবায়ন করুন।";
                        _logger.LogError(objOperation.Message);
                        return objOperation;
                    }
                }


                //Farida Added On 26-04-2026

                var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;

                var url = smsApiBaseUrl + "/api/LnDeviceLoanCollection/GetAllLoanBalanceByCompletedSchedules";

                LoanBalanceViewModel objLoanBalance = await Request<LoanBalanceViewModel, LoanBalanceViewModel>.GetObject(url);

                if (objLoanBalance == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Something went wrong, please try again later";
                    _logger.LogError(objOperation.Message);
                    return objOperation;
                }

                if (objLoanBalance != null)
                {
                    if (objLoanBalance.Balance > 0)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "আপনার ঋণের কিস্তির " + Convert.ToString(objLoanBalance.Balance) + " টাকা বকেয়া রয়েছে। অনুগ্রহ করে বকেয়া পরিশোধ করুন।";
                        _logger.LogError(objOperation.Message);
                        return objOperation;
                    }
                }

                //End



                //End

                //Farida Commented On 04-05-2026
                //TFAClientPaymentDetail objTFAClientPaymentDetail = await service.GetClientPaymentDetailByAppKey(appKey);

                //if (objTFAClientPaymentDetail == null)
                //{
                //    objOperation.Success = false;
                //    objOperation.Message = "Something went wrong, please try again later";
                //    _logger.LogError(objOperation.Message);
                //    return objOperation;
                //}
                //End

                //double graceDay = objTFAClientPaymentDetail.GraceDay == null ? 0 : Convert.ToDouble(objTFAClientPaymentDetail.GraceDay);

                //objTFAClientPaymentDetail.ExpireDate = objTFAClientPaymentDetail.ExpireDate.AddDays(graceDay);
                //End

                //var objComoanyCustomer = await serviceCompanyCustomer.GetCompanyCustomerByAppKey(appKey);

                //if (objComoanyCustomer != null)
                //{
                //    double graceDay = objComoanyCustomer.GraceDay == null ? 0 : objComoanyCustomer.GraceDay.Value;

                //    objTFAClientPaymentDetail.ExpireDate = objTFAClientPaymentDetail.ExpireDate.AddDays(graceDay);
                //}
                //End New Code: Farida- 31-03-2026

                //if (objTFAClientPaymentDetail != null)
                //{
                //    if (objTFAClientPaymentDetail.ExpireDate >= DateTime.Now.Date)
                //    {
                //        objOperation.Success = true;
                //        objOperation.Message = "Package Still Alive";
                //        _logger.LogError(objOperation.Message);
                //    }
                //    else
                //    {
                //        objOperation.Success = false;
                //        objOperation.Message = "এসএমএস সফ্টওয়ার এর মেয়াদ শেষ। দয়া করে বিল পরিশোধ করে নবায়ন করুন।";
                //        _logger.LogError(objOperation.Message);
                //        return objOperation;
                //    }
                //}

            }
            catch (Exception exp)
            {
                objOperation.Success = false;
                objOperation.Message = exp.Message;
                _logger.LogError(exp.Message);
            }
            return objOperation;
        }

        [HttpPost("GetClientBillByClientPaymentDetailId")]
        public CompanyCustomerWithClientPackageViewModel GetClientBillByClientPaymentDetailId(int tfaCompanyCustomerId, Int64 tfaClientPaymentDetailId)
        {
            return service.GetClientBillByClientPaymentDetailId(tfaCompanyCustomerId, tfaClientPaymentDetailId);
        }

        [HttpGet("GetLastPaymentByAppKey")]
        public ClientPaymentViewModel GetLastPaymentByAppKey(string appKey)
        {
            return service.GetLastPaymentByAppKey(appKey);
        }

        [HttpPost("GetClientSubscriptionSummary")]
        public List<ClientSubscriptionSummaryViewModel> GetClientSubscriptionSummary()
        {
            return service.GetClientSubscriptionSummary();
        }

       
        [HttpPost("ExtendExpireDate")]
        public async Task<Operation> ExtendExpireDate([FromBody] ExtendExpireDateViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {

                //if (obj == null || obj.TFAClientPaymentDetailId <= 0)
                //{
                //    objOperation.Success = false;
                //    objOperation.Message = "Invalid request data";
                //    return objOperation;
                //}

                //if (obj.ExtendedExpireDate <= obj.ExpireDate)
                //{
                //    objOperation.Success = false;
                //    objOperation.Message = "Extend date must be greater than expire date";
                //    return objOperation;
                //}

                //if (obj.ExtendedExpireDate <= DateTime.Now.Date)
                //{
                //    objOperation.Success = false;
                //    objOperation.Message = "Extend date must be greater than today";
                //    return objOperation;
                //}

                var paymentDetail =  service.GetById(obj.TFAClientPaymentDetailId);

                if (paymentDetail == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Data not found";
                    return objOperation;
                }             
           
                               
                paymentDetail.GraceDay = obj.ExtendedGraceDay;
                paymentDetail.ModifiedBy = obj.CreatedBy;
                paymentDetail.ModifiedDate = DateTime.Now;              
                service.Update(paymentDetail);

                objOperation.Success = true;
                objOperation.Message = "Expire date extended successfully";
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Something went wrong";
            }

            return objOperation;
        }

    }

}

//public async Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey)