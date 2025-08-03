using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface ILnLoanModelRepository : IRepository<LnLoanModel>
    {
        Int16 AddEntity(LnLoanModel obj);
        Task<Int16> AddEntityAsync(LnLoanModel obj);
        Task<List<LnLoanModel>> GetActiveLoanModel();

    }
    public class LnLoanModelRepository : AdminBaseRepository<LnLoanModel>, ILnLoanModelRepository
    {
        public LnLoanModelRepository(IAdminDatabaseFactory databaseFactory)
         : base(databaseFactory)
        {

        }
        public Int16 AddEntity(LnLoanModel obj)
        {
            Int16 Id = 1;
            LnLoanModel last = DataContext.LnLoanModels.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnLoanModel obj)
        {
            Int16 Id = 1;
            LnLoanModel last = DataContext.LnLoanModels.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }

        public async Task<List<LnLoanModel>> GetActiveLoanModel()
        {
            return await DataContext.LnLoanModels.Where(x=>x.IsActive == true).ToListAsync();
        }
    }
}
