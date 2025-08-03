using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface IAnFFinancialServiceProviderRepository : IRepository<AnFFinancialServiceProvider>
    {
        Int16 AddEntity(AnFFinancialServiceProvider obj);
    }
    public class AnFFinancialServiceProviderRepository : AdminBaseRepository<AnFFinancialServiceProvider>, IAnFFinancialServiceProviderRepository
    {
        public AnFFinancialServiceProviderRepository(IAdminDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {

        }

        public Int16 AddEntity(AnFFinancialServiceProvider obj)
        {
            Int16 Id = 1;
            AnFFinancialServiceProvider last = DataContext.AnFFinancialServiceProviders.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }
    }
}
