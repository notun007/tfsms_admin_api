using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Data.Infrastructure;
//using Technofair.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Common
{
    public interface IUpazillaRepository : IRepository<CmnUpazilla>
    {
        Task<CmnUpazilla> Add(CmnUpazilla obj);
        Task<CmnUpazilla> Update(CmnUpazilla obj);
        Task<CmnUpazilla> GetUpazillaById(int id);
        Task<List<CmnUpazilla>> GetUpazilaList();
        Task<bool> DelUpazillaById(int id);
    }
    public class CmnUpazillaRepository : BaseRepository<CmnUpazilla>, IUpazillaRepository
    {
        public CmnUpazillaRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        #region Upazila
        public async Task<CmnUpazilla> Add(CmnUpazilla obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {

                try
                {

                    DataContext.CmnUpazillas.Add(obj);
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

        public async Task<CmnUpazilla> Update(CmnUpazilla obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {
                    var existObj = DataContext.CmnUpazillas.FirstOrDefault(x => x.Id == obj.Id);

                    if (existObj != null)
                    {
                        existObj.CmnDistrictId = obj.CmnDistrictId;
                        existObj.NameInBangla = obj.NameInBangla;
                        existObj.Name = obj.Name;
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

        public async Task<CmnUpazilla> GetUpazillaById(int id)
        {
            return await DataContext.CmnUpazillas.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CmnUpazilla>> GetUpazilaList()
        {
            return await DataContext.CmnUpazillas.ToListAsync();
        }

        public async Task<bool> DelUpazillaById(int id)
        {
            var obj = await DataContext.CmnUpazillas.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                DataContext.CmnUpazillas.Remove(obj);
                DataContext.Entry(obj).State = EntityState.Modified;
                var save = await DataContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        #endregion
    }
}
