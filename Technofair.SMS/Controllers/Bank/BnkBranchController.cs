
using Technofair.Service.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Bank;
using Technofair.Model.Bank;
using Microsoft.AspNetCore.Mvc;

namespace OneZeroERP.Web.Areas.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class  BnkBranchController : ControllerBase
    {
      
        private IBnkBranchService service;
               
        public BnkBranchController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new BnkBranchService(new BnkBranchRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<BnkBranch> GetAll()
        {
            List<BnkBranch> list = service.GetAll();
            return list;
        }

        [HttpPost("GetBranchByBankId")]
        public List<BnkBranch> GetBranchByBankId(int bankId)
        {
            List<BnkBranch> list = service.GetAll().Where(x=>x.BnkBankId == bankId).ToList();
            return list;
        }

    }
}
