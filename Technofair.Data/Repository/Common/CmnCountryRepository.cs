using Microsoft.EntityFrameworkCore;
using Technofair.Model.Common;
//using Technofair.Data.Infrastructure;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;

namespace TFSMS.Admin.Data.Repository.Common
{
    public interface ICmnCountryRepository : IRepository<CmnCountry>
    {
        Task<CmnCountry> Add(CmnCountry obj);
        Task<CmnCountry> Update(CmnCountry obj);
        Task<CmnCountry> GetCountryById(int id);
        Task<List<CmnCountry>> GetCountryList();
        Task<bool> DelCountryById(int id);

    }
    public class CmnCountryRepository : BaseRepository<CmnCountry>, ICmnCountryRepository
    {
        public CmnCountryRepository(IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
        }

        
        #region Country
        public async Task<CmnCountry> Add(CmnCountry obj)
        {
            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {
                    DataContext.CmnCountries.Add(obj);
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

        public async Task<CmnCountry> Update(CmnCountry obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {

                    var existObj = DataContext.CmnCountries.FirstOrDefault(x => x.Id == obj.Id);

                    if (existObj != null)
                    {
                       
                        existObj.Name = obj.Name;
                     
                        existObj.IsActive = obj.IsActive;

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

        public async Task<CmnCountry> GetCountryById(int id)
        {
            return await DataContext.CmnCountries.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CmnCountry>> GetCountryList()
        {
            return await DataContext.CmnCountries.ToListAsync();
        }

        public async Task<bool> DelCountryById(int id)
        {
            var obj = await DataContext.CmnCountries.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                obj.IsActive = false;
                DataContext.Entry(obj).State = EntityState.Modified;
                var save = await DataContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }
        #endregion
    }
}
