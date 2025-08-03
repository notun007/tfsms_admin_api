using Microsoft.AspNetCore.Mvc;
using Technofair.Model.ViewModel.Common;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Service.TFAdmin;
using Technofair.Lib.Model;
using Technofair.Model.TFAdmin;
using Microsoft.AspNetCore.Authorization;

namespace TFSMS.Admin.Controllers.TFAdmin
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class TFAClientSMSBalanceController : ControllerBase
    {
        private ITFAClientSMSBalanceService service;
        public TFAClientSMSBalanceController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new TFAClientSMSBalanceService(new TFAClientSMSBalanceRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] TFASMSBalanceViewModel obj)
        {
            Operation objOperation = new Operation();
            TFAClientSMSBalance objTFAClientSMSBalance = new TFAClientSMSBalance();

            TFAClientSMSBalance objExist = service.GetById(obj.Id);


            if (objExist == null)
            {
                objTFAClientSMSBalance.Id = obj.Id;
                objTFAClientSMSBalance.TFACompanyCustomerId = obj.TFACompanyCustomerId;
                objTFAClientSMSBalance.NoOfMessage = obj.NoOfMessage;
                objTFAClientSMSBalance.Rate = obj.Rate;
                objTFAClientSMSBalance.Balance = obj.Balance;
                objTFAClientSMSBalance.Date = DateTime.Now;
                objTFAClientSMSBalance.IsActive = obj.IsActive;
                objTFAClientSMSBalance.CreatedBy = obj.CreatedBy;
                objTFAClientSMSBalance.CreatedDate = DateTime.Now;
                objOperation = service.Save(objTFAClientSMSBalance);
                objOperation.Message = "Client SMS Balance Saved Successfull";
            }

            if (objExist != null)
            {                
                objTFAClientSMSBalance.Id = obj.Id;
                objTFAClientSMSBalance.TFACompanyCustomerId = obj.TFACompanyCustomerId;
                objTFAClientSMSBalance.NoOfMessage = obj.NoOfMessage;
                objTFAClientSMSBalance.Rate = obj.Rate;
                objTFAClientSMSBalance.Balance = obj.Balance;
                objTFAClientSMSBalance.Date = DateTime.Now;
                objTFAClientSMSBalance.IsActive = obj.IsActive;
                objTFAClientSMSBalance.ModifiedBy = obj.CreatedBy;
                objTFAClientSMSBalance.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objTFAClientSMSBalance);
                objOperation.Message = "Bill Gen Permission Updated Successfully";
            }

            return objOperation;

            //old

            //Operation objOperation = new Operation { Success = false };
            //int prevBalance = 0;
            //List<TFAClientSMSBalance> list = service.GetByClientId(obj.TFACompanyCustomerId);
            //if (list != null && list.Count > 0)
            //{
            //    list = list.Where(w => w.Balance > 0 && w.Id != obj.Id).ToList();
            //    foreach (TFAClientSMSBalance objS in list)
            //    {
            //        prevBalance += objS.Balance;
            //        objS.Balance = 0;
            //        service.Update(objS);
            //    }
            //    obj.Balance += prevBalance;
            //}

            //if (obj.Id == 0)//&& ButtonPermission.Add
            //{
            //    obj.Balance += obj.NoOfMessage;
            //    obj.Date = DateTime.Now;
            //    objOperation = service.Save(obj);
            //}
            //else if (obj.Id > 0)//&& ButtonPermission.Edit
            //{
            //    objOperation = service.Update(obj);
            //}
            //return objOperation;
        }

        public async Task<List<TFASMSBalanceViewModel>> GetSMSBalanceByClientId(int clientId)
        {
            List<TFASMSBalanceViewModel> list = service.GetSMSBalanceByClientId(clientId);
            list = list.OrderByDescending(e => e.Date).ToList();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetAll")]
        public async Task<List<TFASMSBalanceViewModel>> GetAll()
        {
            List<TFASMSBalanceViewModel> list = await service.GetCustomerDetails();
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetById/{id:int}")]
        public async Task<TFAClientSMSBalance> GetById(int id)
        {
            return service.GetById(id);
        }

    }
}
