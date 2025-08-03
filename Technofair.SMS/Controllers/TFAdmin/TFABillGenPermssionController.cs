using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

using Technofair.Data.Repository.TFAdmin;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;

using Technofair.Service.TFAdmin;

namespace TFSMS.Admin.Controllers.TFAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFABillGenPermssionController : ControllerBase
    {
        private ITFABillGenPermssionService  service;
        public TFABillGenPermssionController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new TFABillGenPermssionService(new TFABillGenPermssionRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("SaveBillGenPermission")]
        public async Task<Operation> Save([FromBody] TFABillGenPermssionViewModel obj)
        {
            Operation objOperation = new Operation();

            TFABillGenPermssion objExist = service.GetById(obj.Id); 
                                            

            if (objExist == null)
            {
               var objBillGenPermssionDup = service.GetBillGenPermissionByMonthIdYear(obj.TFAMonthId, obj.Year);

                if(objBillGenPermssionDup != null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Permission already exist";
                    return objOperation;
                }

                var objOpenBillGenPermission = await service.GetOpenBillGenPermission();
                
                if(objOpenBillGenPermission.Count()>0)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Previous Bill Gen Permission is Open";
                    return objOperation;
                }

                //New
                TFABillGenPermssion objBillGenPermssion = new TFABillGenPermssion();
                objBillGenPermssion.Id = obj.Id;
                objBillGenPermssion.TFAMonthId = obj.TFAMonthId;
                objBillGenPermssion.Year = obj.Year;
                objBillGenPermssion.IsClose = obj.IsClose;
                objBillGenPermssion.CloseBy = obj.CloseBy;
                objBillGenPermssion.CloseDate = DateTime.Now;
                objBillGenPermssion.CreatedBy = obj.CreatedBy;
                objBillGenPermssion.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objBillGenPermssion);
                objOperation.Success = true;
                objOperation.Message = "Bill Gen Permission Saved Successfully";
            }
            
            if (objExist != null)
               {

                var objDuplicate = service.GetBillGenPermissionExceptItSelf(obj);

                if(objDuplicate.Count() > 0)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Permission already exist in other record";
                    return objOperation;
                }

                objExist.TFAMonthId = obj.TFAMonthId;
                objExist.Year = obj.Year;
                objExist.IsClose = obj.IsClose;
                objExist.CloseBy = obj.CloseBy;
                objExist.CloseDate = DateTime.Now;
                objExist.ModifiedBy = obj.ModifiedBy;
                objExist.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExist);
                objOperation.Success = true;
                objOperation.Message = "Bill Gen Permission Updated Successfully";
               }
                      
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("Delete/{id:int}"))]
        public async Task<Operation> Delete(int id)
        {
            Operation objOperation = new Operation { Success = false };
            if (id > 0)
            {
                TFABillGenPermssion obj = service.GetById(id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost(("GetById/{id:int}"))]
        public async Task<TFABillGenPermssion> GetById(int id)
        {
            TFABillGenPermssion obj = service.GetById(id);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetList")]
        public async Task<List<TFABillGenPermssionViewModel>> GetList()
        {
            List<TFABillGenPermssionViewModel> list = await service.GetList();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetBillGenPermission")]
        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermission()
        {
            List<TFABillGenPermssionViewModel> list = await service.GetBillGenPermission();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetOpenBillGenPermission")]
        public async Task<List<TFABillGenPermssionViewModel>> GetOpenBillGenPermission()
        {
            List<TFABillGenPermssionViewModel> list = await service.GetOpenBillGenPermission();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetBillGenPermittedYear")]
        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedYear()
        {
            List<TFABillGenPermssionViewModel> list = await service.GetBillGenPermittedYear();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetBillGenPermittedMonthByYear")]
        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedMonthByYear(int year)
        {
            List<TFABillGenPermssionViewModel> list = await service.GetBillGenPermittedMonthByYear(year);
            return list;
        }
    }
}
