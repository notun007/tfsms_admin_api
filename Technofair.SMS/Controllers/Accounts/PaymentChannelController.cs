using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Service.Accounts;


namespace TFSMS.Admin.Controllers.Accounts
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentChannelController : ControllerBase
    {
        private IAnFPaymentChannelService service;

        public PaymentChannelController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new AnFPaymentChannelService(new AnFPaymentChannelRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [HttpPost("GetAll")]
        public async Task<List<AnFPaymentChannel>> GetAll()
        {
            List<AnFPaymentChannel> list = service.GetAll();
            return list;
        }


        [HttpPost("Save")]
        public async Task<object> Save([FromBody] AnFPaymentChannelViewModel obj)
        {
            Operation objOperation = new Operation { Success = false };
            AnFPaymentChannel objAnFPaymentChannel = new AnFPaymentChannel();

            var objExit = service.GetById(obj.Id);

            if (objExit == null)
            {
                objAnFPaymentChannel.Id = obj.Id;
                objAnFPaymentChannel.Name = obj.Name;
                objAnFPaymentChannel.CreatedBy = obj.CreatedBy;
                objAnFPaymentChannel.CreatedDate = obj.CreatedDate;
                objOperation = service.Save(objAnFPaymentChannel);
                objOperation.Message = "Payment Channel Created Successfully";
            }
            else if (objExit != null)
            {
                objExit.Id = obj.Id;
                objExit.Name = obj.Name;
                objExit.ModifiedBy = obj.ModifiedBy;
                objExit.ModifiedDate = obj.ModifiedDate;
                objOperation = service.Update(objExit);
                objOperation.Message = "Payment Channel Update Successfully";
            }
            return objOperation;
        }

        [HttpPost("Delete")]
        public async Task<object> Delete(Int16 Id)
        {
            Operation objOperation = new Operation { Success = false };
            if (Id > 0)
            {
                AnFPaymentChannel obj = service.GetById(Id);
                if (obj != null)
                {
                    objOperation = service.Delete(obj);
                }
            }
            return objOperation;
        }
    }
}
