using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Technofair.Data.Infrastructure;

using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.Accounts
{
    public interface IAnFFinancialServiceProviderTypRepository : IRepository<AnFFinancialServiceProviderType>
    {
        Int16 AddEntity(AnFFinancialServiceProviderType obj);
    }
    public class AnFFinancialServiceProviderTypRepository : AdminBaseRepository<AnFFinancialServiceProviderType>, IAnFFinancialServiceProviderTypRepository
    {
        public AnFFinancialServiceProviderTypRepository(IAdminDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {

        }

        public Int16 AddEntity(AnFFinancialServiceProviderType obj)
        {
            Int16 Id = 1;
            AnFFinancialServiceProviderType last = DataContext.AnFFinancialServiceProviderTypes.OrderByDescending(x => x.Id).FirstOrDefault();
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
