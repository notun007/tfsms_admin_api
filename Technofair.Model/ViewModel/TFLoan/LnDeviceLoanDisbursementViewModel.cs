using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.ViewModel.TFLoan
{
    public class LnDeviceLoanDisbursementViewModel
    {
        public Int64 Id { get; set; }
        public string LoanNo { get; set; }
        public int LenderId { get; set; }       
        public int LoaneeId { get; set; }
        public string LenderCode { get; set; }
        public string LoaneeCode { get; set; }
        public Int16 NumberOfDevice { get; set; }
        public decimal Rate { get; set; }

        #region New Fields: 27.05.2025
        [Column(TypeName = "decimal(12, 2)")]
        public decimal TotalAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal PaymentAmountPerDevice { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? DueAmountPerDevice { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? DownPaymentAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal LoanAmount { get; set; } // Not nullable
        public Int16 LnTenureId { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal MonthlyInstallmentAmount { get; set; }
        public DateTime InstallmentStartDate { get; set; }
        public short AnFFinancialServiceProviderTypeId { get; set; }
        public short? AnFFinancialServiceProviderId { get; set; }
        public int? BnkBankId { get; set; }
        public short? AnFBranchId { get; set; }
        public short? AnFAccountInfoId { get; set; }
        [StringLength(100)]
        public string? TransactionId { get; set; }
        #endregion
        public bool? IsClosed { get; set; }
        
        public string? Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? LenderName {  get; set; }
        public string? LoaneeName {  get; set; }
        public bool? IsScheduled { get; set; }

        public string? FinancialServiceProviderTypeName { get; set; }
    }
}
