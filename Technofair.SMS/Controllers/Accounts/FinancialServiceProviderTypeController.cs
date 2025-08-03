using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;

using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Accounts;
using TFSMS.Admin.Model.Accounts;

using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Service.Accounts;

namespace TFSMS.Admin.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialServiceProviderTypeController : ControllerBase
    {
        private IAnFFinancialServiceProviderTypeService service;

        public FinancialServiceProviderTypeController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new AnFFinancialServiceProviderTypeService(new AnFFinancialServiceProviderTypRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public async Task<List<AnFFinancialServiceProviderType>> GetAll()
        {
            List<AnFFinancialServiceProviderType> list = service.GetAll();
            return list;
        }


        [HttpPost("Save")]
        public async Task<object> Save([FromBody] AnFFinancialServiceProviderTypeViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            AnFFinancialServiceProviderType objFinancialServiceProviderType = new AnFFinancialServiceProviderType();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objFinancialServiceProviderType.Id = obj.Id;
                objFinancialServiceProviderType.Name = obj.Name;
                objFinancialServiceProviderType.CreatedBy = obj.CreatedBy;
                objFinancialServiceProviderType.CreatedDate = obj.CreatedDate;
                objOperation = service.Save(objFinancialServiceProviderType);
                objOperation.Message = "Financial Service Provider Type Created Successfully";
            }
            else if (objExit != null)
            {
                objExit.Id = obj.Id;
                objExit.Name = obj.Name;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = obj.ModifiedDate;
                objOperation = service.Update(objExit);
                objOperation.Message = "Financial Service Provider Type Update Successfully";
            }
            return objOperation;
        }

        [HttpPost("Delete")]
        public async Task<object> Delete(Int16 Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id > 0)
            {
                AnFFinancialServiceProviderType obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }
    }
}
