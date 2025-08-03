using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{

   public interface ITFACompanyCustomerLogRepository : IRepository<TFACompanyCustomerLog>
    {
        Task<int> AddEntityAsync(TFACompanyCustomerLog obj);
    }
    public class TFACompanyCustomerLogRepository : AdminBaseRepository<TFACompanyCustomerLog>, ITFACompanyCustomerLogRepository
    {
        public TFACompanyCustomerLogRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public async Task<int> AddEntityAsync(TFACompanyCustomerLog obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                TFACompanyCustomerLog? last = DataContext.TFACompanyCustomerLogs.OrderByDescending(x => x.Id).FirstOrDefault();
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
