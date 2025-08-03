using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Technofair.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.Common;
using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using Technofair.Model.Common;
using Technofair.Model.ViewModel.Common;
using Technofair.Service.Common;
using Technofair.Service.TFLoan.Device;
//using Technofair.Service.TFLoan.Device;


namespace TFSMS.Admin.Controllers.Common
{
    
    [Route("Common/[Controller]")]
    public class CmnAppSettingController : ControllerBase
    {

        private ICmnAppSettingService service;
        private ILnLoanModelService serviceLoanModel;
        private ILnDeviceLenderTypeService serviceLenderType;
        public CmnAppSettingController()
        {
            
            var dbfactory = new AdminDatabaseFactory();
            service = new CmnAppSettingService(new CmnAppSettingRepository(dbfactory), new AdminUnitOfWork (dbfactory));
            serviceLoanModel = new LnLoanModelService(new LnLoanModelRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceLenderType = new LnDeviceLenderTypeService(new LnDeviceLenderTypeRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("UpdateAppSetting")]
        public async Task<Operation> UpdateAppSetting([FromBody] CmnAppSettingViewModel obj)
        {
            Operation objOperation = new Operation();

            

            if (obj == null)
            {
                objOperation.Success = false;
                objOperation.Message = "Sent information found as empty in the server.";
                return objOperation;
            }


            try
            {
                CmnAppSetting objAppSetting = await service.GetAppSettingById(obj.Id);

                if (objAppSetting == null)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Must have a default app settings.";
                    return objOperation;
                }

                objAppSetting.AllowAutoSubscriberNumber = obj.AllowAutoSubscriberNumber;
                objAppSetting.SubscriberNumberLength = obj.SubscriberNumberLength;
                objAppSetting.AllowPurchase = obj.AllowPurchase;
                objAppSetting.AllowSale = obj.AllowSale;//
                objAppSetting.AllowMigration = obj.AllowMigration;
                objAppSetting.IsProduction = obj.IsProduction;
                objAppSetting.AppKey = obj.AppKey;
                objAppSetting.AllowRenewableArrear = obj.AllowRenewableArrear;
                objAppSetting.LnLoanModelId = obj.LnLoanModelId;
                objAppSetting.AllowDeviceLoanAtCash = obj.AllowDeviceLoanAtCash;
                objAppSetting.AllowDeviceLoanAtBkash = obj.AllowDeviceLoanAtBkash;
                objAppSetting.ModifiedBy = obj.CreatedBy;
                objAppSetting.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objAppSetting);
                objOperation.Success = true;
                objOperation.Message = "AppSetting Updated Successfully";

               
                var objLenderTypes = serviceLenderType.GetAll();
                foreach (var item in objLenderTypes)
                {

                    if (obj.LnLoanModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.Standard)
                    {
                        if(item.Id == (Int16)Technofair.Utiity.Enums.Loan.LnDeviceLenderTypeEnum.Distributor)
                        {
                            item.IsActive = true;
                        }
                        if (item.Id == (Int16)Technofair.Utiity.Enums.Loan.LnDeviceLenderTypeEnum.MainServiceOperator)
                        {
                            item.IsActive = false;
                        }
                    }

                    if (obj.LnLoanModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.DoubleFlow)
                    {
                        item.IsActive = true;
                    }
                    serviceLenderType.Update(item);
                }             
            }
            catch (Exception exp)
            {
                objOperation.Success = false;
                objOperation.Message = "Unable to Save";
            }
           
            return objOperation;
        }


        //[Authorize(Policy = "Authenticated")]
        [AllowAnonymous]
        [HttpPost("GetCmnAppSetting")]
        public async Task<CmnAppSetting> GetCmnAppSetting()
        {
            return await service.GetCmnAppSetting();
        }
    }

}
