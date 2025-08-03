using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFACompanyCollection
    {
        public long Id { get; set; }
        public int TFAFinancialYearId { get; set; }
        public TFAFinancialYear TFAFinancialYear { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public TFACompanyCustomer TFACompanyCustomer { get; set; }
        public long TFAClientPaymentId { get; set; }
        public TFAClientPayment TFAClientPayment { get; set; }
        public Int16 TFAPaymentMethodId { get; set; }
        public TFAPaymentMethod TFAPaymentMethod { get; set; }
        public DateTime Date { get; set; }
        public string? WalletNo { get; set; }
        public string? TrxID { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<long> AnFVoucherId { get; set; }
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
