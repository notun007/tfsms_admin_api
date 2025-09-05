using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class RecoverScheduledLoanResponseViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal RequestedCollectionAmount { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal ActualCollectionAmount { get; set; }

    }
}
