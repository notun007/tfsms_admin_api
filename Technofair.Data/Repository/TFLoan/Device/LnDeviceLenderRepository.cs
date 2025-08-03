using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Model.ViewModel.TFLoan;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLenderRepository : IRepository<LnDeviceLender>
    {
        Int16 AddEntity(LnDeviceLender obj);
        Task<Int16> AddEntityAsync(LnDeviceLender obj);
        Task<List<LnDeviceLender>> GetLenderByCompanyTypeId(Int16 companyTypeId);
      
        List<LnDeviceLenderViewModel> GetDeviceParentLender();
        List<LnDeviceLenderViewModel> GetLender();
    }
    public class LnDeviceLenderRepository : AdminBaseRepository<LnDeviceLender>, ILnDeviceLenderRepository
    {
        public LnDeviceLenderRepository(IAdminDatabaseFactory databaseFactory)
         : base(databaseFactory)
        {

        }
        public Int16 AddEntity(LnDeviceLender obj)
        {
            Int16 Id = 1;
            LnDeviceLender last = DataContext.LnDeviceLenders.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnDeviceLender obj)
        {
            Int16 Id = 1;
            LnDeviceLender last = DataContext.LnDeviceLenders.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        //New
        public async Task<List<LnDeviceLender>> GetLenderByCompanyTypeId(Int16 companyTypeId)
        {
            var result = await DataContext.LnDeviceLenders.Where(x => x.CmnCompanyTypeId == companyTypeId).ToListAsync();
            return result;
        }

        public List<LnDeviceLenderViewModel> GetDeviceParentLender()
        {
            var result = from dl in DataContext.LnDeviceLenders
                         join ln in DataContext.LnDeviceLenders
                         on dl.LnDeviceParentLenderId equals ln.Id into joined
                         from ln in joined.DefaultIfEmpty()
                         join c in DataContext.CmnCompanies
                         on dl.CmnCompanyId equals c.Id
                         join lt in DataContext.CmnCompanyTypes
                         on dl.CmnCompanyTypeId equals lt.Id
                         where dl.LnDeviceParentLenderId == null
                         select new LnDeviceLenderViewModel
                         {
                             Id = dl.Id,
                             CompanyName = c.Name,
                             LnDeviceParentLenderId = dl.LnDeviceParentLenderId,
                             CmnCompanyId = dl.CmnCompanyId,
                             CmnCompanyTypeId = dl.CmnCompanyTypeId,
                             //LnDeviceLenderTypeId = dl.LnDeviceLenderTypeId,                            
                             IsLoanRecoveryAgent = dl.IsLoanRecoveryAgent,
                             IsActive = dl.IsActive,
                             CreatedBy = dl.CreatedBy,
                             CreatedDate = dl.CreatedDate,
                             ModifiedBy = dl.ModifiedBy,
                             ModifiedDate = dl.ModifiedDate,                            
                             DeviceLenderTypeName = lt.Name,
                             //LnDeviceParentLenderName = c.Name
                         };

            return result.ToList();
        }

        public List<LnDeviceLenderViewModel> GetLender()
        {
            var result = from dl in DataContext.LnDeviceLenders
                         join pl in DataContext.LnDeviceLenders
                         on dl.LnDeviceParentLenderId equals pl.Id into joined
                         from pl in joined.DefaultIfEmpty()
                         join c in DataContext.CmnCompanies
                         on dl.CmnCompanyId equals c.Id 
                         join lt in DataContext.CmnCompanyTypes
                         on dl.CmnCompanyTypeId equals lt.Id                        
                         join cp in DataContext.CmnCompanies
                         on pl.CmnCompanyId equals cp.Id into cpJoin
                         from cp in cpJoin.DefaultIfEmpty()
                         select new LnDeviceLenderViewModel
                         {
                             Id = dl.Id,
                             CompanyName = c.Name,
                             LnDeviceParentLenderId = dl.LnDeviceParentLenderId,
                             CmnCompanyId = dl.CmnCompanyId,
                             CmnCompanyTypeId = dl.CmnCompanyTypeId,
                             //LnDeviceLenderTypeId = dl.LnDeviceLenderTypeId,
                             IsLoanRecoveryAgent = dl.IsLoanRecoveryAgent,
                             IsActive = dl.IsActive,
                             CreatedBy = dl.CreatedBy,
                             CreatedDate = dl.CreatedDate,
                             ModifiedBy = dl.ModifiedBy,
                             ModifiedDate = dl.ModifiedDate,
                             DeviceLenderTypeName = lt.Name,
                             LnDeviceParentLenderName = cp.Name
                         };

            return result.ToList();
        }

    }
}
