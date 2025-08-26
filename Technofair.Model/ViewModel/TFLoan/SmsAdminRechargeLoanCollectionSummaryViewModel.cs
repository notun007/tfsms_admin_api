using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class SmsAdminRechargeLoanCollectionSummaryViewModel
    {
        public Int64 LoanId { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal SmsAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal SmsPaymentCharge { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal AdminAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal AdminPaymentCharge { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal DueAmount { get; set; }
    }
}
