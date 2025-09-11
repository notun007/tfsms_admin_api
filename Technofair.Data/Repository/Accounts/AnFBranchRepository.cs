using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.Accounts
{
    public interface IAnFBranchRepository : IRepository<AnFBranch>
    {
        Int16 AddEntity(AnFBranch obj);
    }
    public class AnFBranchRepository : AdminBaseRepository<AnFBranch>, IAnFBranchRepository
    {
        public AnFBranchRepository(IAdminDatabaseFactory databaseFactory)
           : base(databaseFactory)
        {

        }

        public Int16 AddEntity(AnFBranch obj)
        {
            Int16 Id = 1;
            AnFBranch last = DataContext.AnFBranches.OrderByDescending(x => x.Id).FirstOrDefault();
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
