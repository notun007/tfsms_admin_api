using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientPackage
    {
        public int Id { get; set; }
        public int TFACompanyCustomerId { get; set; }

        public TFACompanyCustomer TFACompanyCustomer { get; set; }
        public int TFACompanyPackageTypeId { get; set; }
        public TFACompanyPackageType TFACompanyPackageType { get; set; }
        public int? TFACompanyPackageId { get; set; }
        public TFACompanyPackage TFACompanyPackage { get; set; }
        public DateTime Date { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public bool IsActive { get; set; }
        public bool? IsFixed { get; set; }
       
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
