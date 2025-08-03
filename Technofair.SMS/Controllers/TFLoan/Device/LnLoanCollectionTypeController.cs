using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.TFLoan.Device;
using Technofair.Service.TFLoan.Device;
using Technofair.Data.Repository.TFLoan.Device;
//using Technofair.Data.Repository.Loan.Device;
//using Technofair.Model.Loan.Device;
//using Technofair.Service.Loan.Device;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnLoanCollectionTypeController : ControllerBase
    {
        private ILnLoanCollectionTypeService service;
        private IWebHostEnvironment _hostingEnvironment;

        public LnLoanCollectionTypeController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();
            service = new LnLoanCollectionTypeService(new LnLoanCollectionTypeRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //var dbfactory = new DatabaseFactory();
            //service = new LnLoanCollectionTypeService(new LnLoanCollectionTypeRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public List<LnLoanCollectionType> GetAll()
        {
            List<LnLoanCollectionType> list = service.GetAll();
            return list;
        }

        [HttpPost("GetManualCollectionTypes")]
        public Task<List<LnLoanCollectionType>> GetManualCollectionTypes()
        {
            return service.GetManualCollectionTypes();
        }
    }
}
