using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LnDeviceLenderLoaneePolicyViewModel
    {
        public Int16 Id { get; set; }
        public int LenderId { get; set; }
        public int LoaneeId { get; set; }
        public string? LenderCode { get; set; }
        public string? LoaneeCode { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal PerRechargeInstallmentAmount { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? LenderName { get; set; }
        public string? LoaneeName { get; set; }
    }
}
