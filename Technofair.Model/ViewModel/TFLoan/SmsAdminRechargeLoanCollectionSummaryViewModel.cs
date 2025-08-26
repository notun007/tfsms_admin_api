using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class SmsAdminRechargeLoanCollectionSummaryViewModel
    {
        public Int64 LoanId { get; set; }
        public decimal SmsAmount { get; set; }
        public decimal SmsPaymentCharge { get; set; }
        public decimal AdminAmount { get; set; }
        public decimal AdminPaymentCharge { get; set; }
    }
}
