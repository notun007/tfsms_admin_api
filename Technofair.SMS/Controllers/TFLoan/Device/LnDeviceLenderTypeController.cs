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
    public class LnDeviceLenderTypeController : ControllerBase
    {
        private ILnDeviceLenderTypeService service;
        private IWebHostEnvironment _hostingEnvironment;

        public LnDeviceLenderTypeController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnDeviceLenderTypeService(new LnDeviceLenderTypeRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //Old
            //var dbfactory = new DatabaseFactory();
            //service = new LnDeviceLenderTypeService(new LnDeviceLenderTypeRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnDeviceLenderType> GetAll()
        {
            List<LnDeviceLenderType> list = service.GetAll();
            return list;
        }
        [HttpPost("GetActiveDeviceLenderType")]
        public async Task<List<LnDeviceLenderType>> GetActiveDeviceLenderType()
        {
            return await service.GetActiveDeviceLenderType();
        }
    }
}
