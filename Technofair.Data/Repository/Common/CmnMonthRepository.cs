using System.Collections.Generic;
using System.Globalization;
using System.Linq;
//using Technofair.Data.Infrastructure;
using Technofair.Model.Common;
using TFSMS.Admin.Data.Infrastructure;


namespace TFSMS.Admin.Data.Repository.Common
{

    #region Interface
    public interface ICmnMonthRepository : IRepository<CmnMonth>
    {
        List<CmnMonth> GetMonthByYearId(int yearId);
        List<DateTime> GetYearMonthDatesByYearId(int yearId);
        List<DateTime> GetCurrentYearMonthDates();
        List<CmnMonth> GetFinancialYearMonth();
        List<DateTime> GetCurrentFinancialYearMonthDates();
        List<CmnMonth> GetFinancialYearMonthByYearId(int yearId);
        List<DateTime> GetFinancialYearMonthDatesByYearId(int yearId);
    }

    #endregion

    public class CmnMonthRepository : BaseRepository<CmnMonth>, ICmnMonthRepository
    {
        public CmnMonthRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {

        }

        public List<CmnMonth> GetMonth()
        {
            List<CmnMonth> list = new List<CmnMonth>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.OrderByDescending(x => x.OpeningDate).FirstOrDefault();
            if (objYear != null)
            {
                //int count = objYear.EndDate.Month - objYear.OpeningDate.Month;
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                    CmnMonth obj = new CmnMonth();
                    obj.Id = (Int16)date.Month;
                    obj.Name = dateTimeFormat.GetMonthName(date.Month);
                    list.Add(obj);
                }
            }
            return list;
        }
        public List<DateTime> GetCurrentYearMonthDates()
        {
            List<DateTime> list = new List<DateTime>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.OrderByDescending(x => x.OpeningDate).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    list.Add(date);
                }
            }
            return list;
        }
        public List<CmnMonth> GetFinancialYearMonth()
        {
            List<CmnMonth> list = new List<CmnMonth>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.OrderByDescending(x => x.OpeningDate).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                    CmnMonth obj = new CmnMonth();
                    obj.Id = (Int16)date.Month;
                    obj.Name = dateTimeFormat.GetMonthName(date.Month);
                    list.Add(obj);
                }
            }
            return list;
        }

        public List<CmnMonth> GetFinancialYearMonthByYearId(int yearId)
        {
            List<CmnMonth> list = new List<CmnMonth>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.Where(w => w.Id == yearId).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                    CmnMonth obj = new CmnMonth();
                    obj.Id = (Int16)date.Month;
                    obj.Name = dateTimeFormat.GetMonthName(date.Month);
                    list.Add(obj);
                }
            }
            return list;
        }

        public List<DateTime> GetCurrentFinancialYearMonthDates()
        {
            List<DateTime> list = new List<DateTime>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.OrderByDescending(x => x.OpeningDate).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    list.Add(date);
                }
            }
            return list;
        }

        public List<DateTime> GetFinancialYearMonthDatesByYearId(int yearId)
        {
            List<DateTime> list = new List<DateTime>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.Where(w => w.Id == yearId).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    list.Add(date);
                }
            }
            return list;
        }

        public List<CmnMonth> GetMonthByYearId(int yearId)
        {
            List<CmnMonth> list = new List<CmnMonth>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.Where(x => x.Id == yearId).FirstOrDefault();
            if (objYear != null)
            {
                //int count = objYear.EndDate.Month - objYear.OpeningDate.Month;
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    var dateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat;
                    CmnMonth obj = new CmnMonth();
                    obj.Id = (Int16)date.Month;
                    obj.Name = dateTimeFormat.GetMonthName(date.Month);
                    list.Add(obj);
                }
            }
            return list;
        }

        public List<DateTime> GetYearMonthDatesByYearId(int yearId)
        {
            List<DateTime> list = new List<DateTime>();
            CmnFinancialYear objYear = DataContext.CmnFinancialYears.Where(w => w.Id == yearId).FirstOrDefault();
            if (objYear != null)
            {
                for (int i = 0; i < 12; i++)
                {
                    DateTime date = objYear.OpeningDate.AddMonths(i);
                    list.Add(date);
                }
            }
            return list;
        }

    }

}
