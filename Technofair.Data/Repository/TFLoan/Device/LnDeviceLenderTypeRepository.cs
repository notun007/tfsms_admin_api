using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
//using Technofair.Model.Loan.Device;

namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface ILnDeviceLenderTypeRepository : IRepository<LnDeviceLenderType>
    {
        Int16 AddEntity(LnDeviceLenderType obj);
        Task<Int16> AddEntityAsync(LnDeviceLenderType obj);
        Task<List<LnDeviceLenderType>> GetActiveDeviceLenderType();
    }
    public class LnDeviceLenderTypeRepository : AdminBaseRepository<LnDeviceLenderType>, ILnDeviceLenderTypeRepository
    {
        public LnDeviceLenderTypeRepository(IAdminDatabaseFactory databaseFactory)
         : base(databaseFactory)
        {

        }

        public Int16 AddEntity(LnDeviceLenderType obj)
        {
            Int16 Id = 1;
            LnDeviceLenderType last = DataContext.LnDeviceLenderTypes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnDeviceLenderType obj)
        {
            Int16 Id = 1;
            LnDeviceLenderType last = DataContext.LnDeviceLenderTypes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        public async Task<List<LnDeviceLenderType>> GetActiveDeviceLenderType()
        {
            
            List<LnDeviceLenderType> list = await DataContext.LnDeviceLenderTypes.Where(x=> x.IsActive == true).ToListAsync();
            return list;
        }

    }
}
