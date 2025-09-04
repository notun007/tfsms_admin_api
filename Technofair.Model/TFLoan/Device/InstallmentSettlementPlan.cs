using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.TFLoan.Device
{
    public class InstallmentSettlementPlan
    {       
            public long Id { get; set; }
            public long LoanId { get; set; }
            public Int16 InstallmentNumber { get; set; }
            public Int16 MonthId { get; set; }
            public Int16 Year { get; set; }

            public decimal ScheduledPrincipal { get; set; }
            public decimal CollectedAmount { get; set; }
            public decimal Balance { get; set; }
            public decimal CollectionAmount { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsPaid { get; set; }
            public bool IsClosed { get; set; }
      

    }
}
