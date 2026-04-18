using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFAdmin
{
    public class BillGenPermissionCheckViewModel
    {
        public int TFACompanyCustomerId { get; set; }
        public int? BillGenPermssionId { get; set; }
        public int? Year { get; set; }
        public short? MonthId { get; set; }
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public string? BillMonth { get; set; }

        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;

        public string? CompanyCustomerName { get; set; }
        public string? Address { get; set; }
        public string? PackageName { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
    }
}
