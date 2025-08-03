using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.Security;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{

    
    #region Interface
    public interface ICmnAppSettingRepository : IRepository<CmnAppSetting>
    {
        Task<CmnAppSetting> GetCmnAppSetting();
        CmnAppSetting GetCmnAppSettingNew();
        Task<CmnAppSetting> GetAppSettingById(int Id);
      
    }
    #endregion Interface

    public class CmnAppSettingRepository : AdminBaseRepository<CmnAppSetting>, ICmnAppSettingRepository
    {
        public CmnAppSettingRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public async Task<CmnAppSetting> GetAppSettingById(int Id)
        {
            CmnAppSetting objCmnAppSetting = await DataContext.CmnAppSettings.Where(c=>c.Id==Id).SingleOrDefaultAsync();
            return objCmnAppSetting;
        }
        public async Task<CmnAppSetting> GetCmnAppSetting()
        {
            CmnAppSetting objCmnAppSetting = new CmnAppSetting();
            try
            {
                //var query =  DataContext.CmnAppSettings.Where(x=> x.IsProduction == false).ToQueryString();
                //SELECT [c].* FROM[CmnAppSettings] AS[c] WHERE [c].[IsProduction] = false

                objCmnAppSetting = await DataContext.CmnAppSettings.SingleOrDefaultAsync();
            }
            catch(Exception exp)
            {
                throw;
            }
            return objCmnAppSetting;
        }

        public CmnAppSetting GetCmnAppSettingNew()
        {
            CmnAppSetting objCmnAppSetting = DataContext.CmnAppSettings.SingleOrDefault();
            return objCmnAppSetting;
        }
    }
}
