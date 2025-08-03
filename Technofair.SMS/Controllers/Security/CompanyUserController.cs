
using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Service.Security;


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
