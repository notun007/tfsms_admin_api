using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using Technofair.Model.Security;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;

using Technofair.Model.ViewModel.Security;
using Technofair.Model.Common;
using Microsoft.AspNetCore.Authorization;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Security
{
    [Route("Security/[Controller]")]
    public class RoleController : ControllerBase
    {

        private ISecRoleService service;
        public RoleController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new SecRoleService(new SecRoleRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<SecRole>> GetAll()
        {
            List<SecRole> list = service.GetAll();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAllSecRole")]
        public async Task<List<SecRoleViewModel>> GetAllSecRole()
        {
            return await service.GetAllSecRole();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetSecRoleByCompanyId")]
        public async Task<List<SecRole>> GetSecRoleByCompanyId(Int16 cmnCompanyId)
        {
            return await service.GetSecRoleByCompanyId(cmnCompanyId);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetSecRoleByCompanyTypeId")]
        public async Task<List<SecRole>> GetSecRoleByCompanyTypeId(Int16? cmnCompanyTypeId)
        {
            return await service.GetSecRoleByCompanyTypeId(cmnCompanyTypeId);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetById/{id:int}"))]
        public async Task<SecRole> GetById(int id)
        {
            SecRole obj = service.GetById(id);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] SecRole obj)
        {
            Operation objOperation = new Operation { Success = false };
            objOperation.Message = "Unable to Save";


            var objRole = service.GetById(obj.Id);

            if (objRole == null)
            {
                var objRoleExist = service.GetAll().Where(x => x.Name == obj.Name && x.CmnCompanyTypeId == obj.CmnCompanyTypeId).SingleOrDefault();

                if (objRoleExist != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Already Exist";
                    return objOperation;
                }

                obj.CreatedDate = DateTime.Now;
                objOperation = service.Save(obj);

                if (objOperation.Success)
                {
                    objOperation.Message = "Added Successfully.";
                }
                else
                {
                    objOperation.Message = "Unable to Add.";
                }

                
            }
            else
            {
                var objRoleExist = service.GetAll().Where(x => x.Id != obj.Id && (x.Name == obj.Name && x.CmnCompanyTypeId == objRole.CmnCompanyTypeId)).SingleOrDefault();

                if (objRoleExist != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Already Exist";
                    return objOperation;
                }

                objRole.ModifiedBy = obj.ModifiedBy;
                objRole.ModifiedDate = DateTime.Now;
                objRole.Name = obj.Name;

                objOperation = service.Update(objRole);

                if (objOperation.Success)
                {
                    objOperation.Message = "Updated Successfully.";
                }
                else
                {
                    objOperation.Message = "Unable to Update.";
                }
            }

            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int Id)
        {
            Operation objOperation = new Operation() { Success = false };
            //if (Id > 0 && ButtonPermission.Delete)
            if (Id > 0)
            {
                SecRole obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

    }
}
