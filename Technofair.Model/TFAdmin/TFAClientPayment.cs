using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientPayment
    {
        public long Id { get; set; }
        public string RefNo { get; set; }
        public int TFAFinancialYearId { get; set; }
        public TFAFinancialYear TFAFinancialYear { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public TFACompanyCustomer TFACompanyCustomer { get; set; }
        public int TFAClientPackageId { get; set; }
        public TFAClientPackage TFAClientPackage { get; set; }
        public Int16? TFAPaymentMethodId { get; set; }
        public TFAPaymentMethod TFAPaymentMethod { get; set; }
        public DateTime Date { get; set; }
        //public Nullable<DateTime> DueDate { get; set; }
        //public Nullable<int> PaidBy { get; set; }
        //public Nullable<DateTime> PaidDate { get; set; }
        public string? WalletNo { get; set; }
        public string? TrxID { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> TotalDiscount { get; set; }
        public Nullable<long> AnFVoucherId { get; set; }
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        //public bool IsCancelled { get; set; }
        //public Nullable<int> CancelledBy { get; set; }
        //public Nullable<DateTime> CancelledDate { get; set; }
        //public string? CancelReason { get; set; }
        //public bool IsCollected { get; set; }
    }
}
