using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;
using Technofair.Lib.Model;
using Technofair.Model.Common;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Utiity.Http;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Service.TFAdmin;


namespace TFSMS.Admin.Controllers.TFAdmin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TFACompanyCustomerController : ControllerBase
    {
        private ITFACompanyCustomerService service;
        private ITFACompanyCustomerLogService serviceCompanyCustomerLog;
        private ITFACompanyCustomerRepository repository;
        private IWebHostEnvironment hostingEnvironment;
        public TFACompanyCustomerController(IWebHostEnvironment _hostingEnvironment)
        {
            var dbFactory = new AdminDatabaseFactory();
            repository = new TFACompanyCustomerRepository(dbFactory);
            service = new TFACompanyCustomerService(repository, new AdminUnitOfWork(dbFactory));

            serviceCompanyCustomerLog = new TFACompanyCustomerLogService(new TFACompanyCustomerLogRepository(dbFactory), new AdminUnitOfWork(dbFactory));

            hostingEnvironment = _hostingEnvironment;
            if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
            {
                hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFACompanyCustomerViewModel obj)
        {
            Operation objOperation = new Operation { OperationId = -1, Success = false, Message = "Unable to Save" };


            if (obj == null) 
            {
                objOperation.Success = false;
                objOperation.Message = "Sent information found as empty";
                return objOperation;
            }

            using (var dbContextTransaction = repository.BeginTransaction())
            {
                try
                {

                    TFACompanyCustomer objExist = service.GetById(obj.Id);
                    TFACompanyCustomer objCompanyCustomer = service.GetByEmailID(obj.Email.Trim());

                  
                    if (objExist == null)
                    {
                        if (objCompanyCustomer != null)
                        {
                            objOperation.Success = false;
                            objOperation.Message = "Email Address Already Exist.";
                            return objOperation;
                        }
                    }

                    if (objExist == null)
                    {
                        TFACompanyCustomer objCustomer = new TFACompanyCustomer();

                        objCustomer.Id = obj.Id;
                        objCustomer.Code = obj.Code = service.GetLastCode();
                        objCustomer.Name = obj.Name;
                        objCustomer.ShortName = obj.ShortName;
                        objCustomer.Address = obj.Address;
                        objCustomer.ContactNo = obj.ContactNo;
                        objCustomer.Email = obj.Email;
                        objCustomer.ContactPerson = obj.ContactPerson;
                        objCustomer.ContactPersonNo = obj.ContactPersonNo;
                        objCustomer.CmnCountryId = obj.CmnCountryId;
                        objCustomer.CmnCurrencyId = obj.CmnCurrencyId;
                        objCustomer.BIN = obj.BIN;
                        objCustomer.Web = obj.Web;
                        objCustomer.Logo = obj.Logo;
                        objCustomer.GraceDay = obj.GraceDay;
                        objCustomer.AppKey = obj.AppKey;
                        objCustomer.ServerIP = obj.ServerIP;
                        objCustomer.MotherBoardId = obj.MotherBoardId;
                        objCustomer.NetworkAdapterId = obj.NetworkAdapterId;
                        objCustomer.SmsApiBaseUrl = obj.SmsApiBaseUrl;
                        objCustomer.IsActive = obj.IsActive;
                        objCustomer.CreatedBy = obj.CreatedBy;
                        objCustomer.CreatedDate = obj.CreatedDate;
                                           
                        objOperation = await service.AddWithNoCommit(objCustomer);

                    }

                    if (objExist != null)
                    {
                        var objCompanyCustomerList = await service.GetCompanyCustomerExceptItseltByEmail(obj);

                        if (objCompanyCustomerList.Count() > 0)
                        {
                            objOperation.Success = false;
                            objOperation.Message = "Email already exist";
                            return objOperation;
                        }
                    }

                        if (objExist != null)
                        {
                                              
                        objExist.Name = obj.Name;
                        objExist.ShortName = obj.ShortName;
                        objExist.ContactNo = obj.ContactNo;
                        objExist.Address = obj.Address;
                        objExist.ContactPerson = obj.ContactPerson;
                        objExist.ContactPersonNo = obj.ContactPersonNo;
                        objExist.CmnCountryId = obj.CmnCountryId;
                        objExist.CmnCurrencyId = obj.CmnCurrencyId;
                        objExist.BIN = obj.BIN;
                        objExist.Web = obj.Web;
                        objExist.GraceDay = obj.GraceDay;

                        objExist.AppKey = obj.AppKey;
                        objExist.ServerIP = obj.ServerIP;
                        objExist.SmsApiBaseUrl = obj.SmsApiBaseUrl;
                        objExist.MotherBoardId = obj.MotherBoardId;
                        objExist.NetworkAdapterId = obj.NetworkAdapterId;


                        objExist.IsActive = obj.IsActive;
                        objExist.ModifiedDate = DateTime.Now;
                        objOperation = await service.UpdateWithNoCommit(objExist);
                    }

                    //Log
                    if (objOperation.Success)
                    {
                        TFACompanyCustomerLog objCompanyCustomerLog = new TFACompanyCustomerLog();
                        objCompanyCustomerLog.Code = obj.Code;
                        objCompanyCustomerLog.Name = obj.Name;
                        objCompanyCustomerLog.ShortName = obj.ShortName;
                        objCompanyCustomerLog.Email = obj.Email;
                        objCompanyCustomerLog.ContactNo = obj.ContactNo;
                        objCompanyCustomerLog.Address = obj.Address;
                        objCompanyCustomerLog.ContactPerson = obj.ContactPerson;
                        objCompanyCustomerLog.ContactPersonNo = obj.ContactPersonNo;
                        objCompanyCustomerLog.CmnCountryId = obj.CmnCountryId;
                        objCompanyCustomerLog.CmnCurrencyId = obj.CmnCurrencyId;
                        objCompanyCustomerLog.BIN = obj.BIN;
                        objCompanyCustomerLog.Web = obj.Web;

                        objCompanyCustomerLog.GraceDay = obj.GraceDay;
                        objCompanyCustomerLog.AppKey = obj.AppKey;
                        objCompanyCustomerLog.ServerIP = obj.ServerIP;
                        objCompanyCustomerLog.SmsApiBaseUrl = obj.SmsApiBaseUrl;
                        objCompanyCustomerLog.MotherBoardId = obj.MotherBoardId;
                        objCompanyCustomerLog.NetworkAdapterId = obj.NetworkAdapterId;

                        objCompanyCustomerLog.IsActive = obj.IsActive;
                        objCompanyCustomerLog.ModifiedDate = DateTime.Now;
                        objCompanyCustomerLog.CreatedDate = DateTime.Now;
                        objOperation = await serviceCompanyCustomerLog.AddWithNoCommit(objCompanyCustomerLog);
                    }
                    //End

                    repository.SaveChanges();
                    repository.Commit(dbContextTransaction);
                    objOperation.Success = true;
                    objOperation.Message = "Saved Successfully.";
                }
                catch (Exception exp)
                {
                    repository.Rollback(dbContextTransaction);
                    objOperation.Success = false;
                    objOperation.Message = "Operation Rolled Back By SMS";
                }
            }
            return objOperation;
        }

        [HttpPost("GetPermittedIntegrator")]
        public async Task<IEnumerable<CmnIntegrator>> GetPermittedIntegrator(string appKey)
        {
            if (string.IsNullOrWhiteSpace(appKey))
                return Enumerable.Empty<CmnIntegrator>();

            var company = service.GetAll()
                .FirstOrDefault(x => x.AppKey == appKey);

            if (company == null)
                return Enumerable.Empty<CmnIntegrator>();

            if (string.IsNullOrWhiteSpace(company.SmsApiBaseUrl))
                return Enumerable.Empty<CmnIntegrator>();

            try
            {
                var url = company.SmsApiBaseUrl.TrimEnd('/') +
                          "/Common/Integrator/GetPermittedIntegrator";

                var result = await Request<object, IEnumerable<CmnIntegrator>>.Post(
                    url,
                    new { }
                );

                return result ?? Enumerable.Empty<CmnIntegrator>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{company.Name} : {ex}");
                return Enumerable.Empty<CmnIntegrator>();
            }
        }

        [HttpPost("ValidateDeviceNumber")]
        public async Task<Operation> ValidateDeviceNumber(string appKey, string deviceNumber, int cmnIntegratorId)
        {
            try
            {
                var company = await service.GetCompanyCustomerByAppKey(appKey);

                if (company == null)
                {
                    return new Operation
                    {
                        Success = false,
                        Message = "Company not found."
                    };
                }

                if (string.IsNullOrWhiteSpace(company.SmsApiBaseUrl))
                {
                    return new Operation
                    {
                        Success = false,
                        Message = "SMS API Base URL is not configured."
                    };
                }

                var url = company.SmsApiBaseUrl.TrimEnd('/')
                          + "/api/DeviceAssign/ValidateDeviceNumberFarida"
                          + "?cmnIntegratorId=" + cmnIntegratorId
                          + "&deviceNumber=" + Uri.EscapeDataString(deviceNumber);

                return await Request<object, Operation>.Post(
                    url,
                    new { }
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);

                return new Operation
                {
                    Success = false,
                    Message = "Unable to connect to CAS."
                };
            }
        }
        [HttpGet("GetAllActiveIntegratorPingResponse")]
        public async Task<IEnumerable<PingResponseViewModel>> GetAllActiveIntegratorPingResponse(string appKey)
        {
            if (string.IsNullOrWhiteSpace(appKey))
                return Enumerable.Empty<PingResponseViewModel>();

            var company = service.GetAll()
                .FirstOrDefault(x => x.AppKey == appKey);

            if (company == null)
                return Enumerable.Empty<PingResponseViewModel>();

            if (string.IsNullOrWhiteSpace(company.SmsApiBaseUrl))
                return Enumerable.Empty<PingResponseViewModel>();

            try
            {
                var url = company.SmsApiBaseUrl.TrimEnd('/') +
                          "/Common/IntegratorCredential/GetAllActiveIntegratorPingResponse";

                var result =
                    await Request<object, IEnumerable<PingResponseViewModel>>
                        .GetObject(url);

                if (result == null)
                    return Enumerable.Empty<PingResponseViewModel>();

                var response = JsonConvert.DeserializeObject<List<PingResponseViewModel>>(
                    JsonConvert.SerializeObject(result)
                );

                return response ?? Enumerable.Empty<PingResponseViewModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{company.Name} : {ex}");

                return Enumerable.Empty<PingResponseViewModel>();
            }
        }

        //[Authorize(Policy = "Authenticated")]
        [HttpGet("GetCompanyCustomerByAppKey")]
        public async Task<TFACompanyCustomer> GetCompanyCustomerByAppKey(string appKey)
        {
            return await service.GetCompanyCustomerByAppKey(appKey);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Delete/{id:int}")]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)//&& ButtonPermission.Delete
            {
                TFACompanyCustomer obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFACompanyCustomer>> GetAll()
        {
            List<TFACompanyCustomer> list= new List<TFACompanyCustomer>();
            try
            {
                list = service.GetAll();
               
            }
            catch(Exception ex)
            {

            }
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetById/{id:int}")]
        public async Task<TFACompanyCustomer> GetById(int id)
        {
            return service.GetById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UploadLogo")]
        public async Task<IActionResult> UploadLogo(string companyId)//IFormFile file
        {
            try
            {
                if (companyId != null && companyId != "")
                {
                    int Id = Convert.ToInt32(companyId);
                    TFACompanyCustomer obj = service.GetById(Id);
                    if (obj != null)
                    {
                        if (obj.Logo != null && obj.Logo != "")
                        {
                            DeleteAttachment(obj);
                        }

                        if (Request.Form.Files.Count > 0)
                        {
                            TFACompanyCustomer objCom = service.GetById(obj.Id);
                            string webRootPath = hostingEnvironment.WebRootPath;
                            string folder = @"Upload File\Common\Company Customer\";
                            string uploadDir = Path.Combine(webRootPath, folder);

                            // wwwroot/uploads/
                            if (!Directory.Exists(uploadDir))
                            {
                                Directory.CreateDirectory(uploadDir);
                            }

                            for (int i = 0; i < Request.Form.Files.Count; i++)
                            {
                                IFormFile file = Request.Form.Files[i];
                                string? fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                if (fileName != null)
                                {
                                    string fullPath = Path.Combine(uploadDir, objCom.Code + "_" + fileName);
                                    if (System.IO.File.Exists(fullPath))
                                    {
                                        System.IO.File.Delete(fullPath);
                                    }

                                    var buffer = 1024 * 1024;
                                    using (FileStream stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None, buffer, useAsync: false))
                                    {
                                        await file.CopyToAsync(stream);
                                        await stream.FlushAsync();
                                        await stream.DisposeAsync();
                                    }

                                    string url = "Upload File/Common/Company Customer/" + objCom.Code + "_" + fileName;
                                    obj.Logo = url;
                                    service.Update(obj);
                                }
                            }
                        }
                    }
                }
                var result = new
                {
                    message = "Upload successful",
                    url = ""
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                //throw ex;
                return StatusCode(StatusCodes.Status500InternalServerError, "Upload failed: " + ex.Message);
            }
        }

        private void DeleteAttachment(TFACompanyCustomer obj)
        {
            if (obj.Logo != null && obj.Logo != "")
            {
                string[] paths = obj.Logo.Split('/');
                string fileName = paths[paths.Length - 1];
                string filePath = "";
                for (int j = 0; j < paths.Length - 1; j++)
                {
                    filePath += paths[j] + @"\";
                }
                //string fileRealPath = Server.MapPath(filePath);
                string webRootPath = hostingEnvironment.WebRootPath;
                //string folder = @"Upload File\Purchase";
                string fileRealPath = Path.Combine(webRootPath, filePath);
                if (System.IO.File.Exists(fileRealPath + fileName))
                {
                    System.IO.File.Delete(fileRealPath + fileName);
                }
            }
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetActiveCompanyCustomerWithClientPackage")]
        public List<CompanyCustomerWithClientPackageViewModel> GetActiveCompanyCustomerWithClientPackage(int monthId, int year)
        {
            return service.GetActiveCompanyCustomerWithClientPackage(monthId, year);
        }
        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetBillGeneratableCompanyCustomers")]
        public List<BillGeneratableCompanyCustomerViewModel> GetBillGeneratableCompanyCustomers()
        {
            return service.GetBillGeneratableCompanyCustomers();
        }




    }
}
