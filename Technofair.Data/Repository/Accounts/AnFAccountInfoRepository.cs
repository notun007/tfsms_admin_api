using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Repository.Bank;
using Technofair.Model.Accounts;
using Technofair.Model.Bank;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.Accounts
{
    public interface IAnFAccountInfoRepository : IRepository<AnFAccountInfo>
    {
        Int16 AddEntity(AnFAccountInfo obj);
    }
    public class AnFAccountInfoRepository : AdminBaseRepository<AnFAccountInfo>, IAnFAccountInfoRepository
    {
        public AnFAccountInfoRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }
        public Int16 AddEntity(AnFAccountInfo obj)
        {
            Int16 Id = 1;
            AnFAccountInfo last = DataContext.AnFAccountInfos.OrderByDescending(x => x.Id).FirstOrDefault();
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
