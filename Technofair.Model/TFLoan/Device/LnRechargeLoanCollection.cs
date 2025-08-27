using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFLoan.Device;

namespace Technofair.Model.TFLoan.Device
{
    public class LnRechargeLoanCollection
    {
        [Key]
        public long Id { get; set; }
        public long LoanId { get; set; }
        public LnDeviceLoanDisbursement Loan { get; set; }
     
        [ForeignKey(nameof(Lender))]
        public int LenderId { get; set; }
        public CmnCompany Lender { get; set; }

        [ForeignKey(nameof(Loanee))]
        public int LoaneeId { get; set; }
        public CmnCompany Loanee { get; set; }

        //[ForeignKey(nameof(AnFPaymentMethod))]
        //public short AnFPaymentMethodId { get; set; }
        //public AnFPaymentMethod AnFPaymentMethod { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal NetAmount { get; set; }

        //[Column(TypeName = "decimal(12, 2)")]
        //public decimal? PaymentChargePercent { get; set; }

        //[Column(TypeName = "decimal(12, 3)")]
        //public decimal? PaymentCharge { get; set; }

        [MaxLength(100)]
        public string? Remarks { get; set; }

        public DateTime CollectionDate { get; set; }

        [Required]
        [MaxLength(100)]
        public string TransactionId { get; set; }
        public Int16 AnFFinancialServiceProviderTypeId { get; set; }
        public int? BnkBankId { get; set; }
        public int? BnkBranchId { get; set; }
        public int? BnkAccountInfoId { get; set; }
        public bool IsCancel { get; set; }

        public int? CancelBy { get; set; }

        public DateTime? CancelDate { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
