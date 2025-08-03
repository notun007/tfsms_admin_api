using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Repository.TFLoan.Device;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnionController : ControllerBase
    {
        public readonly IUnionRepository _IG;
        public UnionController(IUnionRepository _IG)
        {
            this._IG = _IG;
        }


        #region Union

        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddUnion")]
        public async Task<CmnUnion> SaveUnion(CmnUnion obj)
        {
            return await _IG.Add(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateUnion")]
        public async Task<CmnUnion> UpdateUnion(CmnUnion obj)
        {
            return await _IG.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Union/{id:int}")]
        public async Task<CmnUnion> GetUnionById(int id)
        {

            return await _IG.GetUnionById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Union")]
        public async Task<List<CmnUnion>> GetUnionList()
        {
            return await _IG.GetUnionList();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("DelUnion/{id:int}")]
        public async Task<bool> DeleteUnionById(int id)
        {
            return await _IG.DelUnionById(id);
        }

        #endregion

    }
}
