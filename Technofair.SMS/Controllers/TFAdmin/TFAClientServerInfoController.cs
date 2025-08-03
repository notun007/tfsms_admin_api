using Microsoft.AspNetCore.Mvc;

using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;

using Microsoft.AspNetCore.Authorization;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;


namespace TFSMS.Admin.Controllers.TFAdmin
{
  
    [Route("api/[Controller]")]
    [ApiController]
    public class TFAClientServerInfoController : ControllerBase
    {

        private ITFAClientServerInfoService service;

        public TFAClientServerInfoController()
        {
            var adminDbfactory = new AdminDatabaseFactory();
            var dbfactory = new AdminDatabaseFactory();
            service = new TFAClientServerInfoService(new TFAClientServerInfoRepository(adminDbfactory), new AdminUnitOfWork(adminDbfactory));
          
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
