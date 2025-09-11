using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.Accounts
{
    public class AnFAccountInfo
    {
        public short Id { get; set; }
        public short AnFBranchId { get; set; }
        public short? AnFFinancialServiceProviderId { get; set; }
        public string AccountName { get; set; } 
        public string AccountNo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
