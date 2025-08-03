using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Service.Accounts;

namespace TFSMS.Admin.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinancialServiceProviderController : ControllerBase
    {
        private IAnFFinancialServiceProviderService service;
        public FinancialServiceProviderController()
        {
            var dbfactory = new DatabaseFactory();
            service = new AnFFinancialServiceProviderService(new AnFFinancialServiceProviderRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public async Task<List<AnFFinancialServiceProvider>> GetAll()
        {
            List<AnFFinancialServiceProvider> list = service.GetAll();
            return list;
        }

        [HttpPost("Save")]
        public async Task<object> Save([FromBody] AnFFinancialServiceProviderViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            AnFFinancialServiceProvider objFinancialServiceProvider = new AnFFinancialServiceProvider();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objFinancialServiceProvider.Id = obj.Id;
                objFinancialServiceProvider.Name = obj.Name;
                objFinancialServiceProvider.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                objFinancialServiceProvider.CreatedBy = obj.CreatedBy;
                objFinancialServiceProvider.CreatedDate = obj.CreatedDate;
                objOperation = service.Save(objFinancialServiceProvider);
                objOperation.Message = "Financial Service Provider Created Successfully";
            }
            else if (objExit != null)
            {
                objExit.Id = obj.Id;
                objExit.Name = obj.Name;
                objFinancialServiceProvider.AnFFinancialServiceProviderTypeId = obj.AnFFinancialServiceProviderTypeId;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = obj.ModifiedDate;
                objOperation = service.Update(objExit);
                objOperation.Message = "Financial Service Provider Update Successfully";
            }
            return objOperation;
        }

        [HttpPost("Delete")]
        public async Task<object> Delete(Int16 Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id > 0)
            {
                AnFFinancialServiceProvider obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }
    }
}
