//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.TFLoan.Device;

//using Technofair.Data.Repository.Common;
//using Technofair.Data.Repository.Security;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.Common;
//using TFSMS.Admin.Model.Security;
//using Technofair.Model.Subscription;

namespace TFSMS.Admin.Service.TFLoan.Device
{   
    public interface ICmnAppSettingService
    {
        Task<CmnAppSetting> GetCmnAppSetting();
        CmnAppSetting GetCmnAppSettingNew();
        Task<CmnAppSetting> GetAppSettingById(int Id);
        Operation Update(CmnAppSetting obj);
    }
    public class CmnAppSettingService : ICmnAppSettingService
    {
        private ICmnAppSettingRepository repository;
        private IAdminUnitOfWork _UnitOfWork;


        public CmnAppSettingService(ICmnAppSettingRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }
        public Operation Update(CmnAppSetting obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public async Task<CmnAppSetting> GetAppSettingById(int Id)
        {
            CmnAppSetting objCmnAppSetting = await repository.GetAppSettingById(Id);
            return objCmnAppSetting;
        }
        public async Task<CmnAppSetting> GetCmnAppSetting()
        {
            CmnAppSetting objCmnAppSetting = new CmnAppSetting();
            try
            {
                objCmnAppSetting = await repository.GetCmnAppSetting();
            }
            catch(Exception exp) { 
            }
            return objCmnAppSetting;
        }

        public CmnAppSetting GetCmnAppSettingNew()
        {
            CmnAppSetting objCmnAppSetting = repository.GetCmnAppSettingNew();
            return objCmnAppSetting;
        }
    }
}
