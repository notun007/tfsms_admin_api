using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;

namespace Technofair.Model.Accounts
{
    public class AnFBranch
    {
        public short Id { get; set; }
        public short AnFFinancialServiceProviderId { get; set; }
        public AnFFinancialServiceProvider AnFFinancialServiceProvider { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public bool? Status { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
}
