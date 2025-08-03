using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFACompanyPackage
    {
        public int Id { get; set; }
        public int TFACompanyPackageTypeId { get; set; }
        public TFACompanyPackageType TFACompanyPackageType { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Price { get; set; }
        public int MinSubscriber { get; set; }
        public int MaxSubscriber { get; set; }
        public string? Remarks { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
