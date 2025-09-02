using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LoanScheduleInfoViewModel
    {
        public Int64 LoanId { get; set; }
        public string LoanNo { get; set; }
        public Int64 LnDeviceLoanScheduleId { get; set; }
        public string InstallmentNumber { get; set; }

        [Column(TypeName = "decimal(12,2)")]
        public decimal MonthlyInstallmentAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal PerRechargeInstallmentAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal LoanBalance { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal MonthlyLoanBalance { get; set; }

        public bool IsMonthlyLastCollection { get; set; }
        public bool IsMonthlyInstallmentComplete { get; set; }
        public bool IsLastCollection { get; set; }
        public bool IsPaid { get; set; }
    }
}
