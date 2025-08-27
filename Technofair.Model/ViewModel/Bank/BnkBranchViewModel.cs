using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.Bank
{
    public class BnkBranchViewModel
    {
        public int Id { get; set; }
        public int BnkBankId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }

        public Nullable<bool> Status { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string BankName { get; set; }

    }
}
