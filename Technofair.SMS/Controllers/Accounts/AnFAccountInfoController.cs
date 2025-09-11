using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.Accounts;
using Technofair.Data.Repository.Bank;
using Technofair.Model.Accounts;
using Technofair.Model.Bank;
using Technofair.Service.Accounts;
using Technofair.Service.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnFAccountInfoController : ControllerBase
    {
        private IAnFAccountInfoService service;
        public AnFAccountInfoController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new AnFAccountInfoService(new AnFAccountInfoRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<AnFAccountInfo> GetAll()
        {
            List<AnFAccountInfo> list = service.GetAll();
            return list;
        }
    }
}
