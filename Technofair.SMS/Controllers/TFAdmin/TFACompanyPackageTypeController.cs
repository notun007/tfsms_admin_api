using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;
using Technofair.Model.Accounts;
using Technofair.Service.Accounts;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Service.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Model.TFAdmin;
using Technofair.Lib.Model;
using Microsoft.AspNetCore.Authorization;

namespace TFSMS.Admin.Controllers.TFAdmin
{
   
    [Route("api/[Controller]")]
    [ApiController]
    public class TFACompanyPackageTypeController : ControllerBase
    {
        private ITFACompanyPackageTypeService service;
        public TFACompanyPackageTypeController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new TFACompanyPackageTypeService(new TFACompanyPackageTypeRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFACompanyPackageType>> GetAll()
        {
            List<TFACompanyPackageType> list = service.GetAll();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetCompanyPackageTypeByAllowPackage")]
        public async Task<List<TFACompanyPackageType>> GetCompanyPackageTypeByAllowPackage(bool allowPackage)
        {
            List<TFACompanyPackageType> list = service.GetCompanyPackageTypeByAllowPackage(allowPackage);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFACompanyPackageType obj)
        {
            Operation objOperation = new Operation { Success = false };
            if (obj.Id == 0)//&& ButtonPermission.Add
            {
                //obj.CreatedBy = userId;
                obj.CreatedDate = DateTime.Now;
                objOperation = service.Save(obj);
            }
            else if (obj.Id > 0)
            {
                //obj.ModifiedBy = userId;
                obj.ModifiedDate = DateTime.Now;
                objOperation = service.Update(obj);
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int Id)
        {
            Operation objOperation = new Operation() { Success = false };
            if (Id > 0)//&& ButtonPermission.Delete
            {
                TFACompanyPackageType obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

    }
}
