using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFACompanyPackageType
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Remarks { get; set; }
        public bool AllowPackage { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
