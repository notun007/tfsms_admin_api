using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Technofair.Lib.Utilities;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Data.Repository.TFAdmin
{
    public interface ITFABillGenPermssionRepository : IRepository<TFABillGenPermssion>
    {
        int AddEntity(TFABillGenPermssion obj);
        int UpdateEntity(TFABillGenPermssion obj);
        List<TFABillGenPermssion> GetBillGenPermissionExceptItSelf(TFABillGenPermssionViewModel objTFABillGenPermssion);
        Task<int> AddEntityAsync(TFABillGenPermssion obj);
        List<TFABillGenPermssion> GetBillGenPermission(int id);
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermission();
        
        Task<List<TFABillGenPermssionViewModel>> GetOpenBillGenPermission();
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedYear();
        Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedMonthByYear(int year);
        TFABillGenPermssion? GetBillGenPermissionByMonthIdYear(int monthId, int year);

        Task<List<TFABillGenPermssionViewModel>> GetList();
    }
    public class TFABillGenPermssionRepository : AdminBaseRepository<TFABillGenPermssion>, ITFABillGenPermssionRepository
    {
        public TFABillGenPermssionRepository(IAdminDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public int AddEntity(TFABillGenPermssion obj)
        {
            int Id = 1;
            TFABillGenPermssion? last = DataContext.TFABillGenPermssions.OrderByDescending(x => x.Id).FirstOrDefault();
            if (last != null)
            {
                Id = last.Id + 1;
            }
            obj.Id = Id;
            base.Add(obj);
            return Id;
        }

        public async Task<int> AddEntityAsync(TFABillGenPermssion obj)
        {
            int Id = 1;
            if (obj.Id == 0)
            {
                TFABillGenPermssion? last = DataContext.TFABillGenPermssions.OrderByDescending(x => x.Id).FirstOrDefault();
                if (last != null)
                {
                    Id = last.Id + 1;
                }
                obj.Id = Id;
            }
            else
            {
                Id = obj.Id;
            }
            await base.AddAsync(obj);
            return Id;
        }
        public int UpdateEntity(TFABillGenPermssion obj)
        {
            DataContext.TFABillGenPermssions.Update(obj);
            return obj.Id;
        }

        public List<TFABillGenPermssion> GetBillGenPermissionExceptItSelf(TFABillGenPermssionViewModel objTFABillGenPermssion)
        {
            List<TFABillGenPermssion> list = DataContext.TFABillGenPermssions.Where(x => x.Id != objTFABillGenPermssion.Id && (x.TFAMonthId == objTFABillGenPermssion.TFAMonthId && x.Year == objTFABillGenPermssion.Year)).ToList();
            return list;
        }

        public List<TFABillGenPermssion> GetBillGenPermission(int id)
        {
            List<TFABillGenPermssion> list = DataContext.TFABillGenPermssions.Where(x => x.Id == id).ToList();
            return list;
        }
        public TFABillGenPermssion? GetBillGenPermissionByMonthIdYear(int monthId,int year)
        {
            TFABillGenPermssion? obj = DataContext.TFABillGenPermssions.Where(x => x.TFAMonthId == monthId && x.Year == year).FirstOrDefault();
            return obj;
        }


        ///month show
        public async Task<List<TFABillGenPermssionViewModel>> GetList()
        {
            var obj = from cbp in DataContext.TFABillGenPermssions
                        join m in DataContext.TFAMonths on cbp.TFAMonthId equals m.Id
                        orderby cbp.Year descending , cbp.TFAMonthId descending
                        select new TFABillGenPermssionViewModel
                        {
                            Id = cbp.Id,
                            TFAMonthId = cbp.TFAMonthId,
                            Year = cbp.Year,
                            IsClose = cbp.IsClose,
                            CloseBy = cbp.CloseBy,
                            CloseDate = cbp.CloseDate,
                            CreatedBy = cbp.CreatedBy,
                            CreatedDate = cbp.CreatedDate,
                            ShortName = m.ShortName,
                            FullName = m.FullName,
                            MonthYear = m.ShortName + "' " + Convert.ToString(cbp.Year)
                        };

            return await obj.ToListAsync();
        }


        public  async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermission()
        {
            var result = await (from bgp in DataContext.TFABillGenPermssions
                                join m in DataContext.TFAMonths on bgp.TFAMonthId equals m.Id
                                select new TFABillGenPermssionViewModel
                         {
                             Id = bgp.Id,
                             TFAMonthId = bgp.TFAMonthId,
                             Year = bgp.Year,
                             IsClose = bgp.IsClose,
                             CloseBy = bgp.CloseBy,
                             CloseDate = bgp.CloseDate,
                             CreatedBy = bgp.CreatedBy,
                             CreatedDate = bgp.CreatedDate,
                             ShortName = m.ShortName,
                             FullName = m.FullName,
                             MonthYear = m.ShortName + "' " + Convert.ToString(bgp.Year)
                         }).ToListAsync();

            return result;
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetOpenBillGenPermission()
        {
            var result = await (from bgp in DataContext.TFABillGenPermssions
                                join m in DataContext.TFAMonths on bgp.TFAMonthId equals m.Id
                                where bgp.IsClose == false
                                select new TFABillGenPermssionViewModel
                                {
                                    Id = bgp.Id,
                                    TFAMonthId = bgp.TFAMonthId,
                                    Year = bgp.Year,
                                    IsClose = bgp.IsClose,
                                    CloseBy = bgp.CloseBy,
                                    CloseDate = bgp.CloseDate,
                                    CreatedBy = bgp.CreatedBy,
                                    CreatedDate = bgp.CreatedDate,
                                    ShortName = m.ShortName,
                                    FullName = m.FullName,
                                    MonthYear = m.ShortName + "' " + Convert.ToString(bgp.Year)
                                }).ToListAsync();

            return result;
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedYear()
        {
            var distinctYears = await DataContext.TFABillGenPermssions
                                .Select(p => p.Year)
                                .Distinct()
                                .ToListAsync();

            List<TFABillGenPermssionViewModel> yearResults = distinctYears.Select(year => new TFABillGenPermssionViewModel { Year = year }).ToList();
                      
            return yearResults;
        }

        public async Task<List<TFABillGenPermssionViewModel>> GetBillGenPermittedMonthByYear(int year)
        {

            var result = await (from bgp in DataContext.TFABillGenPermssions
                                join m in DataContext.TFAMonths on bgp.TFAMonthId equals m.Id
                                where bgp.Year == year
                                select new TFABillGenPermssionViewModel
                                {
                                    Id = bgp.Id,
                                    TFAMonthId = bgp.TFAMonthId,
                                    Year = bgp.Year,
                                    IsClose = bgp.IsClose,
                                    CloseBy = bgp.CloseBy,
                                    CloseDate = bgp.CloseDate,
                                    CreatedBy = bgp.CreatedBy,
                                    CreatedDate = bgp.CreatedDate,
                                    ShortName = m.ShortName,
                                    FullName = m.FullName,
                                    MonthYear = m.ShortName + "' " + Convert.ToString(bgp.Year)
                                }).ToListAsync();

            return result;
        }
    }
}
