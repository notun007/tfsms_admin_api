using System;
using System.Collections.Generic;
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
        public string? LoanNo { get; set; }
        public int LenderId { get; set; }       
        public int LoaneeId { get; set; }
        public Int16 NumberOfDevice { get; set; }
        public decimal Rate { get; set; }
        //Commented On 27.07.2025
        //public decimal Amount { get; set; }

        #region New Fields: 27.05.2025
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? TotalAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? PaymentAmountPerDevice { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? DueAmountPerDevice { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? DownPaymentAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal LoanAmount { get; set; } // Not nullable
        public Int16? LnTenureId { get; set; }
        #endregion

        //[Column(TypeName = "decimal(12, 2)")]
        //public decimal? DownPaymentAmount { get; set; }
        public string? Remarks { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? LenderName {  get; set; }
        public string? LoaneeName {  get; set; }
    }
}
