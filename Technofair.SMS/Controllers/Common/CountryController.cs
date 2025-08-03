using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Service.Common;


namespace TFSMS.Admin.Controllers.Common
{
    [Route("Common/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {

        private ICmnCountryService service;
        public CountryController()
        {
            var dbFactory = new AdminDatabaseFactory();
            service = new CmnCountryService(new CmnCountryRepository(dbFactory), new AdminUnitOfWork(dbFactory));
        }

        #region Country

        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddCountry")]
        public async Task<CmnCountry> SaveCountry(CmnCountry obj)
        {
            return await service.Add(obj);
        }
        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateCountry")]
        public async Task<CmnCountry> UpdateCountry(CmnCountry obj)
        {
            return await service.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Country/{id:int}")]
        public async Task<CmnCountry> GetCountryById(int id)
        {
            return await service.GetCountryById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Countries")]
        public async Task<List<CmnCountry>> GetCountryList()
        {
            return await service.GetCountryList();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("DelCountry/{id:int}")]
        public async Task<bool> DelCountryById(int id)
        {
            return await service.DelCountryById(id);
        }
        #endregion

    }
}
