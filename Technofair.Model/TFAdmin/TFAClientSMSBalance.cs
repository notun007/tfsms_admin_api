using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientSMSBalance
    {
        public int Id { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public TFACompanyCustomer CmnCompanyCustomer { get; set; }
        public DateTime Date { get; set; }
        public int NoOfMessage { get; set; }
        public int Balance { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
