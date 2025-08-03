using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.Common;
using Technofair.Model.Common;

namespace TFSMS.Admin.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        public readonly IDistrictRepository _IG;
        public DistrictController(IDistrictRepository _IG)
        {
            this._IG = _IG;
        }

        #region District

        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddDistrict")]
        public async Task<CmnDistrict> SaveDistrict(CmnDistrict obj)
        {
            return await _IG.Add(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateDistrict")]
        public async Task<CmnDistrict> UpdateDistrict(CmnDistrict obj)
        {
            return await _IG.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("District/{id:int}")]
        public async Task<CmnDistrict> GetDistrictById(int id)
        {
            return await _IG.GetDistrictById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Districts")]
        public async Task<List<CmnDistrict>> GetDistrictList()
        {
            return await _IG.GetDistrictList();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("DelDistrict/{id:int}")]
        public async Task<bool> DelDistrictById(int id)
        {
            return await _IG.DelDistrictById(id);
        }

        #endregion

    }
}
