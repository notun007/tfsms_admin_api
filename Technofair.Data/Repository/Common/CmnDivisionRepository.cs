using Microsoft.EntityFrameworkCore;
using Technofair.Model.Common;
//using Technofair.Data.Infrastructure;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Common
{
    public interface ICmnDivisionRepository : IRepository<CmnDivision>
    {
        int AddEntity(CmnDivision obj);
    }
    public class CmnDivisionRepository : BaseRepository<CmnDivision>, ICmnDivisionRepository
    {
        public CmnDivisionRepository(IDatabaseFactory databaseFactory): base(databaseFactory)
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
