using Technofair.Data.Repository.HRM;
using Technofair.Data.Infrastructure;
using Technofair.Lib.Model;
using Technofair.Model.HRM;
using Technofair.Service.HRM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using Technofair.Model.ViewModel.HRM;
using Technofair.Model.ViewModel.HRM.Reports;
using Technofair.Model.Common;
using Technofair.Service.Common;
using Technofair.Data.Repository.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting.Internal;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using Technofair.Data.Infrastructure.TFAdmin;

namespace Technofair.SMS.Controllers.HRM
{
    //[CustomHandleErrorAttribute]
    [Route("HRM/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //
        // GET: /HRM/Employee/
        private IHrmEmployeeService service;
        private ICmnCompanyService serviceCom;
        private IWebHostEnvironment hostingEnvironment;
        public EmployeeController(IWebHostEnvironment _hostingEnvironment)
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new HrmEmployeeService(new HrmEmployeeRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCom = new CmnCompanyService(new CmnCompanyRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            hostingEnvironment = _hostingEnvironment;
            if (string.IsNullOrWhiteSpace(hostingEnvironment.WebRootPath))
            {
                hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetHrmEmployeeByCompanyId/{companyId:int}")]
        public async Task<List<HrmEmployeeViewModel>> GetEmployeeByCompanyId(int companyId)
        {
            List<HrmEmployeeViewModel> list = service.GetEmployeeByCompanyId(companyId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeByAnyKey")]
        public async Task<List<HrmEmployeeViewModel>> GetEmployeeByAnyKey(Int16? cmnCompanyTypeId, int? companyId, Int16 userLevel)
        {
            List<HrmEmployeeViewModel> list = service.GetEmployeeByAnyKey(cmnCompanyTypeId, companyId, userLevel);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeByCompanyId")]
        public async Task<List<HrmEmployeeViewModel>> GetEmployeeByCompanyId(int? companyId, Int16 userLevel)
        {
            List<HrmEmployeeViewModel> list = service.GetEmployeeByCompanyId(companyId, userLevel);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeList")]
        public async Task<List<HrmEmployeeViewModel>> GetEmployeeList([FromBody] SearchViewModel obj)
        {
            List<HrmEmployeeViewModel> list = new List<HrmEmployeeViewModel>();
            list = service.GetEmployeeByParameterWise(obj);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeInfo")]
        public async Task<HrmEmployeeViewModel> GetEmployeeInfo(String employeeID)
        {
            HrmEmployeeViewModel obj = service.GetEmployee(employeeID);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetById/{id:int}")]
        public async Task<HrmEmployee> GetById(int id)
        {
            HrmEmployee obj = service.GetById(id);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeById/{employeeId:int}")]
        public async Task<HrmEmployeeViewModel> GetEmployeeById(int employeeId)
        {
            HrmEmployeeViewModel obj = service.GetEmployeeById(employeeId);
            return obj;
        }

        //public ActionResult GetEmployeeInfoById(int empId)
        //{
        //    HrmEmployeeViewModel obj = service.GetEmployeeById(empId);
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetByEmployeeId(string EmployeeId)
        //{
        //    HrmEmployee obj = service.GetByEmployeeId(EmployeeId);
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetByCardNo(string cardNo)
        //{
        //    HrmEmployeeViewModel obj = service.GetByCardNo(cardNo);
        //    return Json(obj, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost("GetEmployeeByCompanyId/{companyId:int}")]
        //public async Task<List<HrmEmployee>> GetEmployeeByCompanyId(int companyId)
        //{
        //    List<HrmEmployee> list = service.GetEmployeeByCompanyId(companyId);
        //    return list;
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeByOfficeId/{officeId:int}")]
        public async Task<List<HrmEmployee>> GetByOfficeId(int officeId)
        {
            List<HrmEmployee> list = service.GetByOfficeId(officeId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEqualOrToManagement/{employeeId:int}")]
        public async Task<List<HrmEmployeeViewModel>> GetEqualOrToManagement(int employeeId)
        {
            List<HrmEmployeeViewModel> list = service.GetEqualOrToManagement(employeeId);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        //New
        public async Task<Operation> Save([FromBody] HrmEmployeeViewModel objEmployee)
        {
            Operation objOperation = new Operation { Success = false };

            HrmEmployee obj = new HrmEmployee();

            try
            {

                var objEmployeeExist = service.GetById(objEmployee.Id);

                //New
                if (objEmployeeExist == null)
                {

                    HrmEmployee objExist = service.GetByEmailID(objEmployee.OfficialEmail.Trim());
                    if (objExist != null)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "Duplicate Email Address.";
                        return objOperation;
                    }

                    obj.Id = objEmployee.Id;
                    obj.CmnCompanyId = objEmployee.CmnCompanyId;
                    obj.EmployeeId = objEmployee.EmployeeId;


                    obj.Name = objEmployee.Name; //
                    obj.Mobile = objEmployee.Mobile; //
                    obj.OfficialEmail = objEmployee.OfficialEmail;//

                    obj.Sex = objEmployee.Sex;//
                    obj.HrmDesignationId = objEmployee.HrmDesignationId;
                    obj.HrmEmployeeTypeId = objEmployee.HrmEmployeeTypeId;

                    obj.ContractStartDate = objEmployee.ContractStartDate;
                    obj.ContractEndDate = objEmployee.ContractEndDate;
                    obj.HrmEmployeeId = objEmployee.HrmEmployeeId;

                    obj.HrmOfficeId = objEmployee.HrmOfficeId;
                    obj.HrmDivisionId = objEmployee.HrmDivisionId;
                    obj.HrmDepartmentId = objEmployee.HrmDepartmentId;


                    obj.HrmSectionId = objEmployee.HrmSectionId;
                    obj.JoiningDate = objEmployee.JoiningDate;//
                    obj.ConfirmationDate = objEmployee.ConfirmationDate;

                    obj.ProbationPeriod = objEmployee.ProbationPeriod;
                    obj.HrmGradeId = objEmployee.HrmGradeId;
                    obj.LineManager = objEmployee.LineManager;

                    obj.Basic = objEmployee.Basic;
                    obj.Gross = objEmployee.Gross;
                    obj.JobLocation = objEmployee.JobLocation;

                    obj.BankAccNo = objEmployee.BankAccNo;
                    obj.BnkBankId = objEmployee.BnkBankId;
                    obj.PABXExtNo = objEmployee.PABXExtNo;

                    obj.AttendanceCardNo = objEmployee.AttendanceCardNo;
                    obj.IsRoastaringDuty = objEmployee.IsRoastaringDuty;

                    obj.IsOTPayable = objEmployee.IsOTPayable;
                    obj.SignatureUrl = objEmployee.SignatureUrl;
                    obj.DateOfDiscontinuation = objEmployee.DateOfDiscontinuation;

                    obj.ReasonOfDiscontinuation = objEmployee.ReasonOfDiscontinuation;
                    obj.IsSuperAdmin = false;

                    //New: 20.10.2024
                    obj.IsProxy = false;
                    //Old: 20.10.2024
                    //obj.IsProxy = objEmployee.IsProxy;

                    obj.IsActive = objEmployee.IsActive;//
                    obj.CreatedBy = objEmployee.CreatedBy;
                    obj.CreatedDate = DateTime.Now;

                    objOperation = service.Save(obj);
                    objOperation.Message = "Employee Created Successfully.";

                }
                else
                {

                    HrmEmployee objEmpExist = service.GetByEmailID(objEmployee.OfficialEmail.Trim());

                    if (objEmpExist != null)
                    {
                        if (objEmpExist.Id != objEmployee.Id)
                        {
                            objOperation.Success = false;
                            objOperation.Message = "Duplicate Email Address.";
                            return objOperation;
                        }
                    }

                    objEmployeeExist.Id = objEmployee.Id;
                    objEmployeeExist.Name = objEmployee.Name; //
                    objEmployeeExist.Mobile = objEmployee.Mobile; //
                    objEmployeeExist.OfficialEmail = objEmployee.OfficialEmail;//
                    objEmployeeExist.Sex = objEmployee.Sex;//
                    objEmployeeExist.JoiningDate = objEmployee.JoiningDate;//
                    objEmployeeExist.IsSuperAdmin = false;

                    objEmployeeExist.IsProxy = objEmployee.IsProxy;//
                    objEmployeeExist.IsActive = objEmployee.IsActive;//
                    objEmployeeExist.ModifiedBy = objEmployee.ModifiedBy;
                    objEmployeeExist.ModifiedDate = DateTime.Now;
                    objOperation = service.Update(objEmployeeExist);
                    objOperation.Message = "Employee Updated Successfully.";

                }
            }
            catch(Exception ex)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to Save";
            }

            //Old
            //if (obj.Id == 0)
            //{

            //    HrmEmployee objEmpExist = service.GetByEmployeeId(obj.OfficialEmail.Trim());
            //    if (objEmpExist == null)
            //    {
            //        obj.CreatedDate = DateTime.Now;
            //        objOperation = service.Save(obj);
            //    }
            //}
            //else if (obj.Id > 0)
            //{
            //    obj.ModifiedDate = DateTime.Now;
            //    objOperation = service.Update(obj);
            //}

            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UploadPhoto")]
        public async Task<IActionResult> UploadPhoto(string employeeId)//IFormFile file
        {
            try
            {
                if (employeeId != null && employeeId != "")
                {
                    int Id = Convert.ToInt32(employeeId);
                    HrmEmployee obj = service.GetById(Id);
                    if (obj != null)
                    {
                        
                        if (Request.Form.Files.Count > 0)
                        {
                            HrmEmployee objEmployee = service.GetById(obj.Id);
                            string webRootPath = hostingEnvironment.WebRootPath;
                            string folder = @"Upload File\HRM\Employee\Photo\" + objEmployee.EmployeeId;
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

                                    string url = "Upload File/HRM/Employee/Photo/" + objEmployee.EmployeeId + "/" + fileName;
                                    obj.PhotoUrl = url;
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

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)
            {
                HrmEmployee obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        //[HttpPost]
        //public ActionResult GetHrmEmployeeByParameterWise(SearchViewModel obj, int companyId)
        //{
        //    if (obj.CmnCompanyId == 0)
        //    {
        //        obj.CmnCompanyId = companyId;
        //    }
        //    List<HrmEmployeeViewModel> list = service.GetEmployeeByParameterWise(obj);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}
        //public ActionResult GetEmployeeProfileByEmployeeId(string employeeId)
        //{
        //    List<HrmEmployeeProfile> list = new List<HrmEmployeeProfile>();
        //    list = service.GetEmployeeProfileByEmployeeId(employeeId);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetEmployeeSummary")]
        public async Task<List<HrmEmployeeSummary>> GetEmployeeSummary([FromBody] SearchViewModel obj)
        {            
            CmnCompany company = new CmnCompany();
            company = serviceCom.GetById(obj.CmnCompanyId);
            List<HrmEmployeeSummary> list = service.GetEmployeeSummary(obj);
            if (list != null)
            {
                list.Where(w => w.Company == null).ToList().ForEach(s => s.Company = company.Name);
            }
            return list;
        }

    }
}
