using Technofair.Data.Repository.Common;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using Technofair.Model.Security;
using Technofair.Service.Common;
using Technofair.Service.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Technofair.Lib.Utilities;
using Technofair.Model.Common;
using Technofair.Model.ViewModel.Security;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Controllers.Security
{
    public class CompanyUserController : ControllerBase
    {
        
        private ISecUserService serviceUser;
        private ISecCompanyUserService serviceComUser;
              
        public CompanyUserController()
        {
            //Session["ModuleId"] = 1;
            var dbfactory = new AdminDatabaseFactory();
            serviceUser = new SecUserService(new SecUserRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceComUser = new SecCompanyUserService(new SecCompanyUserRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }
                        
    }
}
