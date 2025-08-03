using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TFSMS.Admin.Model.ViewModel.TFLoan
{
    public class DeviceLoanInfoViewModel
    {
        //New: 19052025
        public decimal MonthlyInstallmentAmount { get; set; }
        public decimal PerRechargeInstallmentAmount { get; set; }
        public decimal LoanBalance { get; set; }
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
