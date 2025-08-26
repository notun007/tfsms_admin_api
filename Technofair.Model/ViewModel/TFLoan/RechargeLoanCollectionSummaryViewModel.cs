using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class RechargeLoanCollectionSummaryViewModel
    {
        public Int64 LoanId { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal PaymentCharge { get; set; }

    }
}
