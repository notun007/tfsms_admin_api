using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ILnLoanCollectionTypeRepository : IRepository<LnLoanCollectionType>
    {
        Int16 AddEntity(LnLoanCollectionType obj);
        Task<Int16> AddEntityAsync(LnLoanCollectionType obj);
        Task<List<LnLoanCollectionType>> GetManualCollectionTypes();
    }
    public class LnLoanCollectionTypeRepository : AdminBaseRepository<LnLoanCollectionType>, ILnLoanCollectionTypeRepository
    {
        public LnLoanCollectionTypeRepository(IAdminDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {

        }

        public Int16 AddEntity(LnLoanCollectionType obj)
        {
            Int16 Id = 1;
            LnLoanCollectionType last = DataContext.LnLoanCollectionTypes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnLoanCollectionType obj)
        {
            Int16 Id = 1;
            LnLoanCollectionType last = DataContext.LnLoanCollectionTypes.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        public async Task<List<LnLoanCollectionType>> GetManualCollectionTypes()
        {
            return await DataContext.LnLoanCollectionTypes.Where(l => l.IsManual == true).ToListAsync();
        }

    }
}
