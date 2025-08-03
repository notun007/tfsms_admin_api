
using Technofair.Model.TFLoan.Device;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace TFSMS.Admin.Data.Repository.TFLoan.Device
{
    public interface ILnTenureRepository : IRepository<LnTenure>
    {
        Int16 AddEntity(LnTenure obj);
        Task<Int16> AddEntityAsync(LnTenure obj);       
    }
    public class LnTenureRepository : AdminBaseRepository<LnTenure>, ILnTenureRepository
    {
        public LnTenureRepository(IAdminDatabaseFactory databaseFactory)
         : base(databaseFactory)
        {

        }
        public Int16 AddEntity(LnTenure obj)
        {
            Int16 Id = 1;
            LnTenure last = DataContext.LnTenures.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<Int16> AddEntityAsync(LnTenure obj)
        {
            Int16 Id = 1;
            LnTenure last = DataContext.LnTenures.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = Convert.ToInt16(last.Id + 1);
            }
            obj.Id = Id;
            await base.AddAsync(obj);
            return Id;
        }
    }
}
