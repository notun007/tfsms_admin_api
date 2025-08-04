
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Model.HRM;

namespace TFSMS.Admin.Data.Repository.HRM
{
    public interface IHrmFileCategoryRepository : IRepository<HrmFileCategory>
    {
        List<HrmFileCategory> GetAll();
        int AddEntity(HrmFileCategory obj);
       
    }
    public class HrmFileCategoryRepository : AdminBaseRepository<HrmFileCategory>, IHrmFileCategoryRepository
    {
        public HrmFileCategoryRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public List<HrmFileCategory> GetAll()
        {
            return DataContext.HrmFileCategories.ToList();
        }
        public int AddEntity(HrmFileCategory obj)
        {
            int Id = 1;
            HrmFileCategory last = DataContext.HrmFileCategories.OrderByDescending(x => x.Id).FirstOrDefault();

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