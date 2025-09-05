using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
            [Column(TypeName = "decimal(12, 2)")]   
            public decimal ScheduledPrincipal { get; set; }
            [Column(TypeName = "decimal(12, 2)")]
            public decimal CollectedAmount { get; set; }
            [Column(TypeName = "decimal(12, 2)")]
            public decimal Balance { get; set; }
            [Column(TypeName = "decimal(12, 2)")]
            public decimal CollectionAmount { get; set; }

            public DateTime DueDate { get; set; }

            public bool IsPaid { get; set; }
            public bool IsClosed { get; set; }
      

    }
}
