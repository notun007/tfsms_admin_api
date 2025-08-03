using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Security;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Service.Security;

namespace TFSMS.Admin.Controllers.Security
{
 
    [Route("Security/[Controller]")]
    public class SecUserTypeController : ControllerBase
    {

        private ISecUserTypeService service;
        
       public SecUserTypeController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new SecUserTypeService(new SecUserTypeRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAllSecUserType")]
        public async Task<IList<SecUserType>> GetAllSecUserType()
        //public async Task<List<SecUser>> GetUserByCompanyId(int companyId)
        {
            return await service.GetAllSecUserType();
        }

    }
}
