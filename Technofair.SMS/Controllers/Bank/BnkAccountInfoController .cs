
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
    [Route("api/[controller]")]
    [ApiController]
    public class BnkAccountInfoController : ControllerBase
    {

        private IBnkAccountInfoService service;
        public BnkAccountInfoController()
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

        [HttpPost("GetAccountInfoByBranchId")]
        public List<BnkAccountInfo> GetAccountInfoByBranchId(int bankId, int branchId)
        {
            List<BnkAccountInfo> list = service.GetAll().Where(x=> x.BnkBankId == bankId && x.BnkBranchId == branchId).ToList();
            return list;
        }
        #endregion

    }
}
