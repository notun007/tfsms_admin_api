
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Common;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.Common;
using Technofair.Service.Common;

namespace TFSMS.Admin.Controllers.Common
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivisionController : ControllerBase
    {
        private ICmnDivisionService _IG;
        public DivisionController()
        {
            var dbFactory = new DatabaseFactory();
            _IG = new CmnDivisionService(new CmnDivisionRepository(dbFactory), new UnitOfWork(dbFactory));
            //this._IG = _IG;
        }

        #region Division

        [Authorize(Policy = "Authenticated")]
        [HttpPost("AddDivision")]
        public async Task<Operation> SaveDivision(CmnDivision obj)
        {
            return  _IG.Save(obj);
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateDivision")]
        public async Task<Operation> UpdateDivision(CmnDivision obj)
        {
            return _IG.Update(obj);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Division/{id:int}")]
        public async Task<CmnDivision> GetDivisionById(int id)
        {
            return _IG.GetById(id);
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Divisions")]
        public async Task<List<CmnDivision>> GetDivisionList()
        {
            return  _IG.GetAll();
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("DelDivision/{id:int}")]
        public async Task<Operation> DeleteDivisionById(int id)
        {
            CmnDivision obj=_IG.GetById(id);
            return  _IG.Delete(obj);
        }

        #endregion

    }
}
