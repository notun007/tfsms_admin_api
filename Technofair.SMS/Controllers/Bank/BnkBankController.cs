
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Technofair.Data.Repository.Bank;
using Technofair.Lib.Model;
using Technofair.Model.Bank;
using Technofair.Service.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
namespace OneZeroERP.Web.Areas.Bank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BnkBankController : ControllerBase
    {       
        private IBnkBankService service;
      
        public BnkBankController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new BnkBankService(new BnkBankRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<BnkBank> GetAll()
        {
            List<BnkBank> list = service.GetAll();
            return list;
        }
    }
}

