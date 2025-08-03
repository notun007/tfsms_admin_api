using Microsoft.EntityFrameworkCore;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface ICmnDivisionRepository : IRepository<CmnDivision>
    {
        int AddEntity(CmnDivision obj);
    }
    public class CmnDivisionRepository : AdminBaseRepository<CmnDivision>, ICmnDivisionRepository
    {
        public CmnDivisionRepository(IAdminDatabaseFactory databaseFactory): base(databaseFactory)
        {

        }

        public int AddEntity(CmnDivision obj)
        {
            int Id = 1;
            CmnDivision last = DataContext.CmnDivisions.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);            
            return Id;
        }


    }
}
