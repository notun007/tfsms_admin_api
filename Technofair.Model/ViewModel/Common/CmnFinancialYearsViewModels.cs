using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class CmnFinancialYearsForView
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime OpeningDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public int SecModuleId { get; set; }
        public bool YearClosingStatus { get; set; }

    }

    public class CmnFinancialYearSearchForRange
    {
    public DateTime Date { get; set; }
    }
    public class CmnFinancialYearResultForRange
    {
        public string OpeningDate { get; set; }
        public string ClosingDate { get; set; }
    }
}