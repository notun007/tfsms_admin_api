using Microsoft.EntityFrameworkCore;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
//using Technofair.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Common
{
    public interface IDistrictRepository : IRepository<CmnDistrict>
    {
        Task<CmnDistrict> Add(CmnDistrict obj);
        Task<CmnDistrict> Update(CmnDistrict obj);
        Task<CmnDistrict> GetDistrictById(int id);
        Task<List<CmnDistrict>> GetDistrictList();
        Task<bool> DelDistrictById(int id);

    }
    public class CmnDistrictRepository : AdminBaseRepository<CmnDistrict>, IDistrictRepository
    {
        public CmnDistrictRepository(IAdminDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

       
        #region District
        public async Task<CmnDistrict> Add(CmnDistrict obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {

                try
                {

                    DataContext.CmnDistricts.Add(obj);
                    await DataContext.SaveChangesAsync();
                    trns.Commit();
                }
                catch (Exception ex)
                {
                    trns.Rollback();
                    throw ex;
                }

            }
            return obj;

        }
        public async Task<CmnDistrict> Update(CmnDistrict obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {

                    var existObj = DataContext.CmnDistricts.FirstOrDefault(x => x.Id == obj.Id);

                    if (existObj != null)
                    {
                        existObj.NameInBangla = obj.NameInBangla;
                        existObj.Name = obj.Name;
                        
                        existObj.CmnDivisionId = obj.CmnDivisionId;

                        DataContext.Entry(existObj).State = EntityState.Modified;
                        await DataContext.SaveChangesAsync();
                        trns.Commit();

                    }
                }
                catch (Exception ex)
                {

                    throw;
                }

            }
            return obj;

        }

        public async Task<CmnDistrict> GetDistrictById(int id)
        {
            return await DataContext.CmnDistricts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CmnDistrict>> GetDistrictList()
        {
            return await DataContext.CmnDistricts.ToListAsync();
        }

        public async Task<bool> DelDistrictById(int id)
        {
            var obj = await DataContext.CmnDistricts.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                DataContext.CmnDistricts.Remove(obj);
                DataContext.Entry(obj).State = EntityState.Modified;
                var save = await DataContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        #endregion

    }
}
