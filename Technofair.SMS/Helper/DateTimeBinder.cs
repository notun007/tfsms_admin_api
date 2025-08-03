using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Helper
{
    //public class DateTimeBinder : IModelBinder
    //{
    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

    //        var date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);

    //        return date;
    //    }
    //}
    //public class NullableDateTimeBinder : IModelBinder
    //{
    //    public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
    //    {
    //        var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
    //        if (value != null)
    //        {
    //            var date = value.ConvertTo(typeof(DateTime), CultureInfo.CurrentCulture);
    //            return date;
    //        }
    //        return null;
    //    }
    //}
    public class YearMonth
    {
        static IList<MMonth> _MonthList = new List<MMonth>();
        public YearMonth()
        {
            LoadMonths();
        }

        public static void LoadMonths()
        {
            _MonthList = new List<MMonth>();
            _MonthList.Add(new MMonth() { Id = 1, Name = "January" });
            _MonthList.Add(new MMonth() { Id = 2, Name = "February" });
            _MonthList.Add(new MMonth() { Id = 3, Name = "March" });
            _MonthList.Add(new MMonth() { Id = 4, Name = "April" });
            _MonthList.Add(new MMonth() { Id = 5, Name = "May" });
            _MonthList.Add(new MMonth() { Id = 6, Name = "June" });
            _MonthList.Add(new MMonth() { Id = 7, Name = "July" });
            _MonthList.Add(new MMonth() { Id = 8, Name = "August" });
            _MonthList.Add(new MMonth() { Id = 9, Name = "September" });
            _MonthList.Add(new MMonth() { Id = 10, Name = "October" });
            _MonthList.Add(new MMonth() { Id = 11, Name = "Novemeber" });
            _MonthList.Add(new MMonth() { Id = 12, Name = "December" });
        }

        public static IList<MMonth> GetRestMonths()
        {
            LoadMonths();
            _MonthList = TakeLast<MMonth>(_MonthList, DateTime.Today.Month).ToList();
            return _MonthList;
        }
        public static IEnumerable<T> TakeLast<T>(IEnumerable<T> source, int N)
        {
            return source.Skip(Math.Max(0, source.Count() - N));
        }
        public static IList<MMonth> GetBeginningMonths()
        {
            LoadMonths();
            _MonthList = _MonthList.Take(DateTime.Today.Month - 1).ToList();
            return _MonthList;
        }
        public static IList<MMonth> GetMonths()
        {
            LoadMonths();
            return _MonthList;
        }
    }

    public class MMonth
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class DateUtility
    {
        public static int CountWeekEnds(DateTime startDate, DateTime endDate, DayOfWeek weekend)
        {
            int weekEndCount = 0;
            if (startDate > endDate)
            {
                DateTime temp = startDate;
                startDate = endDate;
                endDate = temp;
            }
            TimeSpan diff = endDate - startDate;
            int days = diff.Days;
            for (var i = 0; i <= days; i++)
            {
                var testDate = startDate.AddDays(i);
                if (testDate.DayOfWeek == weekend)
                {
                    weekEndCount += 1;
                }
            }
            return weekEndCount;
        }
    }

}