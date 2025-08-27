
using Technofair.Service.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Bank;
using Technofair.Model.Bank;
using Microsoft.AspNetCore.Mvc;

namespace OneZeroERP.Web.Areas.Bank.Controllers
{
    public class  BranchController : Controller
    {
      

        private IBnkBranchService service;

       
        public BranchController()
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

    }
}
