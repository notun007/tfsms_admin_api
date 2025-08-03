using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class TFAClientPackageViewModel
    {
        public int Id { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public int TFACompanyPackageTypeId { get; set; }
        public int? TFACompanyPackageId { get; set; }
        public DateTime? Date { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public bool IsActive { get; set; }
        public bool? IsFixed { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string? CompanyCustomerName { get; set; }
        public string? CompanyPackageTypeName { get; set; }
        public string? MinMaxSubscriber { get; set; }
    }
}
