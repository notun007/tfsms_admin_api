using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Reporting.Map.WebForms.BingMaps;

using System.Net.Http.Headers;

using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.HRM;

using TFSMS.Admin.Model.Security;

using TFSMS.Admin.Model.ViewModel.Common;

using TFSMS.Admin.Service.HRM;
using TFSMS.Admin.Service.Security;
using TFSMS.Admin.Service.TFLoan.Device;
using TFSMS.Admin.Data.Repository.HRM;
using TFSMS.Admin.Service.Common;

//using CmnCompanyRepository = TFSMS.Admin.Data.Repository.Common.CmnCompanyRepository;
//using CmnCompanyService = TFSMS.Admin.Service.Common.CmnCompanyService;
//using ICmnCompanyRepository = TFSMS.Admin.Data.Repository.Common.ICmnCompanyRepository;
//using ICmnCompanyService = TFSMS.Admin.Service.Common.ICmnCompanyService;


namespace TFSMS.Admin.Controllers.Common
{
    [Route("Common/[controller]")]
    public class CompanyController : ControllerBase
    {
        
        private ICmnCompanyService service;
       
        private ISecUserService serviceUser;
        private ISecCompanyUserService serviceComUser;
        private IHrmEmployeeService serviceEmployee;
    
        private ICmnAppSettingService serviceAppSetting;
        private IWebHostEnvironment hostingEnvironment;
                
        private ICmnCompanyRepository repository;

        public CompanyController(IWebHostEnvironment _hostingEnvironment)
        {
            var dbFactory = new AdminDatabaseFactory();
            repository = new CmnCompanyRepository(dbFactory);

            service = new CmnCompanyService(new CmnCompanyRepository(dbFactory), new AdminUnitOfWork(dbFactory));
           
            serviceUser = new SecUserService(new SecUserRepository(dbFactory), new AdminUnitOfWork
        (dbFactory));
            serviceComUser = new SecCompanyUserService(new SecCompanyUserRepository(dbFactory), new AdminUnitOfWork(dbFactory));
            serviceEmployee = new HrmEmployeeService(new HrmEmployeeRepository(dbFactory), new AdminUnitOfWork(dbFactory));
           
            serviceAppSetting = new CmnAppSettingService(new CmnAppSettingRepository(dbFactory), new AdminUnitOfWork(dbFactory));

            hostingEnvironment = _hostingEnvironment;
            if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
            {
                hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        //New
           
        private int SaveEmployee(HrmEmployee objEmp, CmnCompany? objCom)
        {
            int empId = 0;
            if (objCom == null)// new
            {
                HrmEmployee objEmpExist = serviceEmployee.GetByEmployeeId(objEmp.OfficialEmail);
                if (objEmpExist == null)
                {
                    Operation objOperation = serviceEmployee.Save(objEmp);
                    if (objOperation.Success)
                    {
                        empId = objEmp.Id;
                    }
                }
                else if (objEmpExist != null && objEmpExist.Id > 0)
                {
                    empId = objEmpExist.Id;
                }
            }
            else if (objCom != null)
            {
                HrmEmployee objEmpExist = serviceEmployee.GetByEmployeeId(objCom.Email);
                if (objEmpExist == null)
                {
                    Operation objOperation = serviceEmployee.Save(objEmp);
                    if (objOperation.Success)
                    {
                        empId = objEmp.Id;
                    }
                }
                else if (objEmpExist != null && objEmpExist.Id > 0 && objEmpExist.CmnCompanyId == objCom.Id)
                {
                    empId = objEmpExist.Id;
                    objEmp.Id = objEmpExist.Id;
                    Operation objOperation = serviceEmployee.Update(objEmp);
                }
            }
            return empId;
        }

        private void SaveUser(SecUser objUser, CmnCompany? objComExist)
        {
            if (objComExist == null)// new
            {
                SecUser objUserExist = serviceUser.GetByLoginID(objUser.LoginID);
                if (objUserExist == null)
                {
                    Operation objUserOperation = serviceUser.Save(objUser);
                    if (objUserOperation.Success)
                    {
                        SecCompanyUser objExist = serviceComUser.GetByUserId(objUser.Id);
                        if (objExist == null)
                        {
                            objExist = new SecCompanyUser();
                            objExist.CmnCompanyId = objUser.CmnCompanyId;
                            objExist.SecUserId = objUser.Id;
                            objExist.IsActive = true;
                            serviceComUser.Save(objExist);
                        }
                    }
                }
            }
            else if (objComExist != null)
            {
                SecUser objUserExist = serviceUser.GetByLoginID(objComExist.Email);
                if (objUserExist == null)
                {
                    Operation objUserOperation = serviceUser.Save(objUser);
                    if (objUserOperation.Success)
                    {
                        SecCompanyUser objExist = serviceComUser.GetByUserId(objUser.Id);
                        if (objExist == null)
                        {
                            objExist = new SecCompanyUser();
                            objExist.CmnCompanyId = objUser.CmnCompanyId;
                            objExist.SecUserId = objUser.Id;
                            objExist.IsActive = true;
                            serviceComUser.Save(objExist);
                        }
                    }
                }
            }
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Delete/{id:int}")]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)//&& ButtonPermission.Delete
            {
                CmnCompany obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<CmnCompany>> GetAll()
        {
            List<CmnCompany> list = service.GetAll();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetCompanyList")]
        public  List<CmnCompanyViewModel> cmnCompanyViewList()
        {
            List<CmnCompanyViewModel> list =  service.cmnCompanyViewList();
            return list;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetById/{id:int}")]
        public async Task<CmnCompany> GetById(int id)
        {
            return service.GetById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetClientByCompanyId/{companyId:int}"))]
        public async Task<List<CmnCompanyViewModel>> GetClientByCompanyId(int companyId)
        {
            List<CmnCompanyViewModel> list = service.GetClientByCompanyId(companyId);
            if (list != null)
            {
                list = list.Where(w => w.Id != companyId).ToList();// except himself
                foreach (var item in list)
                {
                    item.Name = item.Name + " (" + item.CompanyType + ")";
                }
            }
            return list;
        }

    
       
        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetCompanyByCompanyTypeId"))]
        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeId(Int16 companyTypeId)
        {
            return await service.GetCompanyByCompanyTypeId(companyTypeId);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetAllCompanyTypeByCompanyId"))]
        public async Task<List<CmnCompanyType>> GetAllCompanyTypeByCompanyId([FromBody] int? companyId)
        {
            List<CmnCompanyType> list = await service.GetAllCompanyType();
            return list;
        }

        [Authorize(Policy = "Authenticated")] //SP, MSO, LSO, SLSO
        [HttpPost(("GetCompanyByCompanyTypeShortName"))]
        public async Task<List<CmnCompany>> GetCompanyByCompanyTypeShortName(string shortName)
        {
            return await service.GetCompanyByCompanyTypeShortName(shortName);
        }
                     
     
        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetAllCompanyType"))]
        public async Task<List<CmnCompanyType>> GetAllCompanyType()
        {
            List<CmnCompanyType> list = await service.GetAllCompanyType();
            return list;
        }
              

        [AllowAnonymous]
        [HttpGet(("GetSolutionProvider"))]
        public CmnCompany GetSolutionProvider()
        {
            CmnCompany company = service.GetSolutionProvider();
            return company;
        }

        //[Authorize(Policy = "Authenticated")]
        [AllowAnonymous]
        [HttpGet(("GetMainServiceOperator"))]
        public async Task<CmnCompany> GetMainServiceOperator()
        {
            CmnCompany company = service.GetMainServiceOperator();
            return company;
        }

        [AllowAnonymous]
        [HttpGet(("GetMainServiceOperators"))]
        public async Task<List<CmnCompany>> GetMainServiceOperators()
        {
            List<CmnCompany> companies = await service.GetMainServiceOperators();
            return companies;
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
                    CmnCompany obj = service.GetById(Id);
                    if (obj != null)
                    {
                        //if (obj.Logo != null && obj.Logo != "")
                        //{
                        //    DeleteAttachment(obj.FilePath);
                        //}

                        if (Request.Form.Files.Count > 0)
                        {
                            CmnCompany objCom = service.GetById(obj.Id);
                            string webRootPath = hostingEnvironment.WebRootPath;
                            string folder = @"Upload File\Common\Company\" + objCom.Code;
                            string uploadDir = Path.Combine(webRootPath, folder);

                            // wwwroot/uploads/
                            if (!Directory.Exists(uploadDir))
                            {
                                Directory.CreateDirectory(uploadDir);
                            }


                            for (int i = 0; i < Request.Form.Files.Count; i++)
                            {
                                IFormFile file = Request.Form.Files[i];
                                string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                                if (fileName != null)
                                {
                                    string fullPath = Path.Combine(uploadDir, fileName);
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

                                    string url = "Upload File/Common/Company/" + objCom.Code + "/" + fileName;
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
        private void DeleteAttachment(CmnCompanyAttachment obj)
        {
            if (obj.FilePath != null && obj.FilePath != "")
            {
                string[] paths = obj.FilePath.Split('/');
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
        [HttpPost(("GetClientByCompanyIdForAdmin/{companyId:int}"))]
        public async Task<List<CmnCompanyViewModel>> GetClientByCompanyIdForAdmin(int companyId)
        {
            List<CmnCompanyViewModel> list = service.GetClientByCompanyId(companyId);
            if (list != null)
            {
                foreach (var item in list)
                {
                    item.Name = item.Name + " (" + item.CompanyType + ")";
                }
            }
            return list;
        }
        public class RequestObject
        {
            public int comId { get; set; }
            public string? details { get; set; }
            private List<CmnCompanyAttachmentViewModel>? _list = null;
            public List<CmnCompanyAttachmentViewModel> list
            {
                get
                {
                    if (_list == null)
                    {
                        _list = new List<CmnCompanyAttachmentViewModel>();
                    }
                    return _list;
                }
                set
                {
                    _list = value;
                }
            }
            public string? lists { get; set; }
        }

    }
}