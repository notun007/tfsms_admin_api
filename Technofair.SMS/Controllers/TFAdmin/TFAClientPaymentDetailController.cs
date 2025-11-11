using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Model.TFAdmin;
using Technofair.Lib.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using Microsoft.AspNetCore.Authorization;
using Technofair.Utiity.Log;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using Technofair.Model.ViewModel.TFAdmin;


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

        //[Authorize(Policy = "Authenticated")]
        [HttpGet("VerifyClientPackageByAppKey")]
        public async Task<Operation> VerifyClientPackageByAppKey(string appKey)
        {
            Operation objOperation = new Operation { Success = false };

            try
            {
                //_logger.LogError("VerifyClientPackageByAppKey");

                //var ip = HttpContext.Connection.RemoteIpAddress?.ToString();
                //_logger.LogError("Remote IP Address: " + ip);

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

                TFAClientPaymentDetail objTFAClientPaymentDetail = await service.GetClientPaymentDetailByAppKey(appKey);


                if (objTFAClientPaymentDetail == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Please Buy Package";
                    _logger.LogError(objOperation.Message);
                    return objOperation;
                }

                if (objTFAClientPaymentDetail != null)
                {
                    if (objTFAClientPaymentDetail.ExpireDate >= DateTime.Now.Date)
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
            }
            catch(Exception exp)
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

    }

    }

//public async Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey)