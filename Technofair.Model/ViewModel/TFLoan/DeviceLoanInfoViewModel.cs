using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TFSMS.Admin.Model.ViewModel.TFLoan
{
    public class DeviceLoanInfoViewModel
    {
        //New: 19052025
        [Column(TypeName = "decimal(12, 2)")]
        public decimal MonthlyInstallmentAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal PerRechargeInstallmentAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal LoanBalance { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal MonthlyLoanBalance { get; set; }
        public bool IsPaid { get; set; }
        public bool IsMonthlyInstallmentComplete { get; set; }
        
				
                

        //Old: 19052025
        //public Int16? LnLoanModelId { get; set; }

        //public int? LoanDistributorId { get; set; }
        //public decimal? MonthlyMsoInstallmentAmount { get; set; }
        //public decimal? PerRechargeMsoInstallmentAmount { get; set; }
        //public decimal? MsoLoanBalance { get; set; }
        //public int LoanRecoveryAgentId { get; set; }
        //public decimal MonthlyLsoSlsoInstallmentAmount { get; set; }
        //public decimal PerRechargeLsoSlsoInstallmentAmount { get; set; }
        //public decimal LsoSlsoLoanBalance { get; set; }

    }
}
