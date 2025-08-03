using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Model.Common;
using Technofair.Model.Accounts;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;


namespace Technofair.Data.Repository.TFLoan.Device
{
    public interface IUnionRepository : IRepository<CmnUnion>
    {
        Task<CmnUnion> Add(CmnUnion obj);
        Task<CmnUnion> Update(CmnUnion obj);
        Task<CmnUnion> GetUnionById(int id);
        Task<List<CmnUnion>> GetUnionList();
        Task<bool> DelUnionById(int id);

    }
    public class CmnUnionRepository : AdminBaseRepository<CmnUnion>, IUnionRepository
    {
        public CmnUnionRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        #region Union
        public async Task<CmnUnion> Add(CmnUnion obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {

                try
                {

                    DataContext.CmnUnions.Add(obj);
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

        public async Task<CmnUnion> Update(CmnUnion obj)
        {

            using (var trns = DataContext.Database.BeginTransaction())
            {
                try
                {
                    var existObj = DataContext.CmnUnions.FirstOrDefault(x => x.Id == obj.Id);

                    if (existObj != null)
                    {
                        existObj.CmnUpazillaId = obj.CmnUpazillaId;
                        existObj.NameInBangla = obj.NameInBangla;
                        existObj.Name = obj.Name;
                        //existObj.url = obj.url;
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

        public async Task<CmnUnion> GetUnionById(int id)
        {

            return await DataContext.CmnUnions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<CmnUnion>> GetUnionList()
        {
            return await DataContext.CmnUnions.ToListAsync();
        }

        public async Task<bool> DelUnionById(int id)
        {

            var obj = await DataContext.CmnUnions.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                DataContext.CmnUnions.Remove(obj);
                DataContext.Entry(obj).State = EntityState.Modified;
                var save = await DataContext.SaveChangesAsync();
                return true;
            }
            else return false;
        }

        #endregion


    }
}
