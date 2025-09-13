using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.Accounts;
using Technofair.Model.Accounts;
using Technofair.Model.Bank;
using Technofair.Service.Accounts;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnFBranchController : ControllerBase
    {
        private IAnFBranchService service;
        public AnFBranchController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new AnFBranchService(new AnFBranchRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<AnFBranch> GetAll()
        {
            List<AnFBranch> list = service.GetAll();
            return list;
        }
        [HttpPost("GetBranchByFinancialServiceProviderId")]
        public List<AnFBranch> GetBranchByFinancialServiceProviderId(int financialServiceProviderId)
        {
            List<AnFBranch> list = service.GetAll().Where(x => x.AnFFinancialServiceProviderId == financialServiceProviderId).ToList();
            return list;
        }

    }
}
