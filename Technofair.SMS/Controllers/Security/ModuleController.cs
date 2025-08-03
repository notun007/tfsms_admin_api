using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.Security;
using TFSMS.Admin.Service.Security;


namespace TFSMS.Admin.Controllers.Security
{
    [Route("Security/[Controller]")]
    [ApiController]
    public class ModuleController  : ControllerBase
    {
       
        private ISecModuleService service;
        public ModuleController()
        {
            var dbFactory = new AdminDatabaseFactory();
            service = new SecModuleService(new SecModuleRepository(dbFactory), new AdminUnitOfWork(dbFactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<SecModule>> GetAll()
        {
            List<SecModule> list = service.GetAll();
            return list;
        }

        [HttpPost("Save")]
        public async Task<object> Save([FromBody] SecModuleViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            SecModule objModule = new SecModule();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                var objModuleDuplicate = service.GetModuleByName(obj.Name,obj.NameBn);
                if (objModuleDuplicate != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "This Module Already exist.";
                    return objOperation;
                }

                objModule.Id = obj.Id;
                objModule.Name = obj.Name;
                objModule.NameBn = obj.NameBn;
                objModule.Icon = obj.Icon;
                objModule.SerialNo = obj.SerialNo;
                objModule.IsActive = obj.IsActive;
                objModule.CreatedBy = obj.CreatedBy;
                objModule.CreatedDate = DateTime.Now;
                objOperation = service.Save(objModule);
                objOperation.Message = "Module Created Successfully";
            }
            else if (objExit != null)
            {
                var objModuleDuplicate = service.GetModuleByName(obj.Name,obj.NameBn);
                if (objModuleDuplicate != null && objModuleDuplicate.Id != obj.Id)
                {
                    objOperation.Success = false;
                    objOperation.Message = "This Module Already exist.";
                    return objOperation;
                }

                //objExit.Id = obj.Id;
                objExit.Name = obj.Name;
                objExit.NameBn = obj.NameBn;
                objExit.Icon = obj.Icon;
                objExit.SerialNo = obj.SerialNo;
                objExit.IsActive = obj.IsActive;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExit);
                objOperation.Message = "Module Update Successfully";
            }
            return objOperation;
        }

    }
}
