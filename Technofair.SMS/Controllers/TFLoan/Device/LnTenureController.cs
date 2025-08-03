using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Service.TFLoan.Device;


namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnTenureController : ControllerBase
    {
        private ILnTenureService service;

        public LnTenureController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnTenureService(new LnTenureRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //var dbfactory = new DatabaseFactory();
            //service = new LnTenureService(new LnTenureRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnTenure> GetAll()
        {
            List<LnTenure> list = service.GetAll();
            return list;
        }
    }
}
