using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    public interface ITFAClientPackageLogRepository : IRepository<TFAClientPackageLog>
    {
        int AddEntity(TFAClientPackageLog obj);
        Task<int> AddEntityAsync(TFAClientPackageLog obj);
    }
    public class TFAClientPackageLogRepository : AdminBaseRepository<TFAClientPackageLog>, ITFAClientPackageLogRepository
    {
        public TFAClientPackageLogRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {


        }
        public int AddEntity(TFAClientPackageLog obj)
        {
            int Id = 1;
            TFAClientPackageLog last = DataContext.TFAClientPackageLogs.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
        public async Task<int> AddEntityAsync(TFAClientPackageLog obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                TFAClientPackageLog? last = DataContext.TFAClientPackageLogs.OrderByDescending(x => x.Id).FirstOrDefault();
                if (last != null)
                {
                    Id = last.Id + 1;
                }
                obj.Id = Id;
            }
            else
            {
                Id = obj.Id;
            }
            await base.AddAsync(obj);
            return Id;
        }
        

    }
}
