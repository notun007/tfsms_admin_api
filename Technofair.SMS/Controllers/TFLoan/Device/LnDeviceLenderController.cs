using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Technofair.Data.Repository.TFLoan.Device;
using Technofair.Lib.Model;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Service.TFLoan.Device;

namespace TFSMS.Admin.Controllers.TFLoan.Device
{
    [Route("api/[controller]")]
    [ApiController]
    public class LnDeviceLenderController : ControllerBase
    {
        private ILnDeviceLenderService service;
        private ILnDeviceLenderRepository repository;
        private IWebHostEnvironment _hostingEnvironment;
        private ICmnAppSettingService serviceAppSetting;
        public LnDeviceLenderController()
        {
            //New:28072025
            var dbfactory = new AdminDatabaseFactory();          
            service = new LnDeviceLenderService(new LnDeviceLenderRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceAppSetting = new CmnAppSettingService(new CmnAppSettingRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }
        [HttpPost("GetAll")]
        public List<LnDeviceLender> GetAll()
        {
            List<LnDeviceLender> list = service.GetAll();
            return list;
        }

        [HttpPost("GetLender")]
        public List<LnDeviceLenderViewModel> GetLender()
        {
            List<LnDeviceLenderViewModel> list = service.GetLender();
            return list;
        }

        [HttpPost("GetDeviceParentLender")]
        public List<LnDeviceLenderViewModel> GetDeviceParentLender()
        {
            List<LnDeviceLenderViewModel> list = service.GetDeviceParentLender();
            return list;
        }

        [HttpPost("SaveDeviceLender")]
        public async Task<Operation> Save([FromBody] LnDeviceLenderViewModel obj)
        {
            Operation objOperation = new Operation();
            LnDeviceLender objLnDeviceLender = new LnDeviceLender();
            CmnAppSetting objAppSetting = await serviceAppSetting.GetCmnAppSetting();

            var objExist = service.GetById(obj.Id);

            //var objExistByType = await service.GetLenderByLenderTypeId(obj.LnDeviceLenderTypeId);

            var objExistByType = await service.GetLenderByCompanyTypeId(obj.CmnCompanyTypeId);
           

            if (objAppSetting.LnLoanModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.Standard)
            {

                if (objExist == null && objExistByType.Count > 0)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Already Exist";
                    return objOperation;
                }

                if (objExist == null && obj.IsLoanRecoveryAgent == false)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Please select Loan Recovery Agent";
                    return objOperation;
                }

                if (obj.CmnCompanyTypeId != (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.SP)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Please select right lender type";
                    return objOperation;
                }
                
                objLnDeviceLender.IsLoanRecoveryAgent = true;
               
            }

            if (objAppSetting.LnLoanModelId == (Int16)Technofair.Utiity.Enums.Loan.LnLoanModelEnum.DoubleFlow)
            {
                if (objExist == null && objExistByType.Count > 0)
                {
                    objOperation.Success = false;
                    objOperation.Message = "Already Exist";
                    return objOperation;
                }

                if (obj.CmnCompanyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.SP)
                {
                    if (obj.IsLoanRecoveryAgent == true)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "Please select right lender type";
                        return objOperation;
                    }
                }
                else if (obj.CmnCompanyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.MSO)
                {
                    if (obj.IsLoanRecoveryAgent == false)
                    {
                        objOperation.Success = false;
                        objOperation.Message = "Please select right lender type";
                        return objOperation;
                    }

                }

                    if (obj.CmnCompanyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.SP)
                    {
                        objLnDeviceLender.IsLoanRecoveryAgent = false;
                    }
                    if (obj.CmnCompanyTypeId == (Int16)Technofair.Utiity.Enums.Common.CmnCompanyTypeEnum.MSO)
                    {
                        objLnDeviceLender.IsLoanRecoveryAgent = true;
                    }
               
            }



            if (objExist == null && objExistByType.Count == 0)
            {                
                objLnDeviceLender.Id = obj.Id;
                objLnDeviceLender.CmnCompanyId = obj.CmnCompanyId;
                objLnDeviceLender.CmnCompanyTypeId = obj.CmnCompanyTypeId;
                //objLnDeviceLender.LnDeviceLenderTypeId = obj.LnDeviceLenderTypeId;
                objLnDeviceLender.LnDeviceParentLenderId = obj.LnDeviceParentLenderId;
                //objLnDeviceLender.IsLoanRecoveryAgent = obj.IsLoanRecoveryAgent;
                objLnDeviceLender.IsActive = obj.IsActive;

                objLnDeviceLender.CreatedBy = obj.CreatedBy;
                objLnDeviceLender.CreatedDate = DateTime.Now;
                objOperation = await service.Save(objLnDeviceLender);
                objOperation.Message = "Device Lender Created Successfully.";
            }
            else if (objExist != null && objExistByType.Count > 0)
            {
                objExist.CmnCompanyId = obj.CmnCompanyId;
                //objExit.LnDeviceLenderTypeId = obj.LnDeviceLenderTypeId;
                objExist.LnDeviceParentLenderId = obj.LnDeviceParentLenderId;
                //objExit.IsLoanRecoveryAgent = obj.IsLoanRecoveryAgent;
                objExist.IsActive = obj.IsActive;
                objExist.ModifiedBy = obj.ModifiedBy;
                objExist.ModifiedDate = DateTime.Now;
                objOperation = service.Update(objExist);
                objOperation.Message = "Device Lender Updated Successfully.";
            }
            return objOperation;
        }

    }
}
