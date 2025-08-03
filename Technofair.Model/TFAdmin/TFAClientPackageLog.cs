using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientPackageLog
    {
        public int Id { get; set; }
        public int TFACompanyPackageTypeId { get; set; }
        public int? TFACompanyPackageId { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Discount { get; set; }
        public DateTime Date { get; set; }
        public Boolean? IsFixed { get; set; }
        public Boolean IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
