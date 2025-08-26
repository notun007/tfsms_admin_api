using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class RechargeLoanCollectionSummaryViewModel
    {
        public string LoanId { get; set; }
        public decimal Amount { get; set; }
        public decimal PaymentCharge { get; set; }

    }
}
