using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Common;
using Technofair.Data.Repository.TFAdmin;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.ViewModel.Common;
using Technofair.Service.Common;
using Technofair.Service.TFAdmin;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using Technofair.Service.Security;
using Microsoft.AspNetCore.Authorization;


namespace TFSMS.Admin.Controllers.TFAdmin
{
  
    [Route("api/[Controller]")]
    [ApiController]
    public class TFAClientServerInfoController : ControllerBase
    {

        private ITFAClientServerInfoService service;
        //private ICmnCompanyService serviceCompany;
        public TFAClientServerInfoController()
        {
            var adminDbfactory = new AdminDatabaseFactory();
            var dbfactory = new DatabaseFactory();
            service = new TFAClientServerInfoService(new TFAClientServerInfoRepository(adminDbfactory), new AdminUnitOfWork(adminDbfactory));
            //serviceCompany = new CmnCompanyService(new CmnCompanyRepository(dbfactory), new UnitOfWork(dbfactory));
        }

        #region Method
        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFAClientServerInfoViewModel obj)
        {
            Operation objOperation = new Operation();
            TFAClientServerInfo objNew= new TFAClientServerInfo();
            objNew.Id = obj.Id;
            objNew.ServerIP = obj.ServerIP;
            objNew.TFACompanyCustomerId = obj.TFACompanyCustomerId;
            objNew.MotherBoardId = obj.MotherBoardId;
            objNew.NetworkAdapterId = obj.NetworkAdapterId;
           
           
            if (obj.Id == 0)//&& ButtonPermission.Add
            {
                objNew.CreatedBy = obj.CreatedBy;
                objNew.CreatedDate = DateTime.Now;
                objOperation = service.Save(objNew);
            }
            else if (obj.Id > 0)//&& ButtonPermission.Edit
            {
                objNew.ModifiedBy = obj.ModifiedBy;
                objNew.CreatedDate = DateTime.Now;
                objOperation = service.Update(objNew);
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)//&& ButtonPermission.Delete
            {
                TFAClientServerInfo obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetById/{id:int}"))]
        public async Task<TFAClientServerInfo> GetById(int id)
        {
            TFAClientServerInfo obj = service.GetById(id);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFAClientServerInfo>> GetAll()
        {
            List<TFAClientServerInfo> list = service.GetAll();
            return list;
        }

        //[HttpPost("GetClientServerInfoByCustomerCompanyId")]
        //public async Task<TFAClientServerInfo> GetClientServerInfoByCustomerCompanyId(int id)
        //{
        //    TFAClientServerInfo objClientServerInfo = await service.GetClientServerInfoByCustomerCompanyId(id);
        //    return objClientServerInfo;
        //}

        #endregion
    }

}
