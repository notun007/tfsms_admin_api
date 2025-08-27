
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Technofair.Data.Repository.Bank;
using Technofair.Model.Bank;
using Technofair.Service.Bank;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.Common;


namespace OneZeroERP.Web.Areas.Bank.Controllers
{
    //public class BankAccountController: Controller
    //{
       
    //    private IBnkAccountInfoService service;

    //    public BankAccountController()
    //    {
    //        var dbfactory = new AdminDatabaseFactory();
    //        service = new BnkAccountInfoService(new BnkAccountInfoRepository(dbfactory), new AdminUnitOfWork(dbfactory));
           
    //    }
             
    
    //}

    [Route("Common/[controller]")]
    [ApiController]
    public class BankAccountController : ControllerBase
    {

        private IBnkAccountInfoService service;
        public BankAccountController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new BnkAccountInfoService(new BnkAccountInfoRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        #region BankAccountController

        [HttpPost("GetAll")]
        public List<BnkAccountInfo> GetAll()
        {
            List<BnkAccountInfo> list = service.GetAll();
            return list;
        }
        #endregion

    }
}
