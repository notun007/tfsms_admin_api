using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFLoan.Device;

namespace Technofair.Model.TFLoan.Device
{
    public class LnDeviceLoanDisbursementRequestObject
    {
        public long Id { get; set; }

        [Required, StringLength(20)]
        public string LoanNo { get; set; }

        public int LenderId { get; set; }

        public int LoaneeId { get; set; }
        public CmnCompany Loanee { get; set; }

        [Required, StringLength(16)]
        public string LenderCode { get; set; }

        [Required, StringLength(16)]
        public string LoaneeCode { get; set; }

        public short NumberOfDevice { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal Rate { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? PaymentAmountPerDevice { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? DueAmountPerDevice { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal? DownPaymentAmount { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal LoanAmount { get; set; }

        public short LnTenureId { get; set; }
        public LnTenure LnTenure { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal MonthlyInstallmentAmount { get; set; }

        public DateTime InstallmentStartDate { get; set; }

        [StringLength(512)]
        public string? Remarks { get; set; }

        [Required, StringLength(100)]
        public string TransactionId { get; set; }

        public short? AnFFinancialServiceProviderTypeId { get; set; }

        public int? BnkBankId { get; set; }

        public int? BnkBranchId { get; set; }

        public int? BnkAccountInfoId { get; set; }

        public bool IsSmsSuccess { get; set; }

        public bool IsAdminSuccess { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsClosed { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }
    }
}
