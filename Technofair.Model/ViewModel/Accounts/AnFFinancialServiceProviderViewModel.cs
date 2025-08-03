using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;

namespace TFSMS.Admin.Model.ViewModel.Accounts
{
    public class AnFFinancialServiceProviderViewModel
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public Int16 AnFFinancialServiceProviderTypeId { get; set; }
        public string? FinancialServiceProviderType { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
