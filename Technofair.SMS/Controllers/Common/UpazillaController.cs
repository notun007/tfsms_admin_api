using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.Common;
using Technofair.Model.Common;

namespace TFSMS.Admin.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpazillaController : ControllerBase
    {
        public readonly IUpazillaRepository _IG;
        public UpazillaController(IUpazillaRepository _IG)
        {
            this._IG = _IG;
        }

        #region Upazila
        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddUpazilla")]
        public async Task<CmnUpazilla> SaveUpazila(CmnUpazilla obj)
        {
            return await _IG.Add(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateUpazilla")]
        public async Task<CmnUpazilla> UpdateUpazila(CmnUpazilla obj)
        {
            return await _IG.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Upazilla/{id:int}")]
        public async Task<CmnUpazilla> GetUpazilaById(int id)
        {
            return await _IG.GetUpazillaById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Upazillaes")]
        public async Task<List<CmnUpazilla>> GetUpazilaList()
        {
            return await _IG.GetUpazilaList();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("DelUpazilla/{id:int}")]
        public async Task<bool> DeleteUpazilaById(int id)
        {
            return await _IG.DelUpazillaById(id);
        }

        #endregion

    }
}
