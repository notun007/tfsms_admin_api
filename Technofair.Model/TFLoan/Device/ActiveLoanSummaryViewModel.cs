using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.TFLoan.Device
{
    public class ActiveLoanSummaryViewModel
    {
        public int LoaneeId { get; set; }

        public string CompanyName { get; set; }
        public string LoaneeCode { get; set; }
        public int ActiveLoanCount { get; set; }

        public decimal DisbursementAmount { get; set; }

        public decimal CollectionAmount { get; set; }

        public decimal DueAmount { get; set; }
    }
}
