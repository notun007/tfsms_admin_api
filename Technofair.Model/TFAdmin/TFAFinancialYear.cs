using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAFinancialYear
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime OpeningDate { get; set; }
        public System.DateTime ClosingDate { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public bool YearClosingStatus { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
