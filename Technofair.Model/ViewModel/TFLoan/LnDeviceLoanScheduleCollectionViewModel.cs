using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LnDeviceLoanScheduleCollectionViewModel
    {
        public Int64 LoanId { get; set; }
        public string LoanNo { get; set; }
        public Int16 InstallmentNumber { get; set; }


        public string MonthName { get; set; }
        public Int16 Year { get; set; }

        public string MonthYear { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal ScheduledPrincipal { get; set; }

        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; }
        public string PaymentStatus { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal NoneRechargeCollectionAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal RechargeCollectionAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal CollectionAmount { get; set; }
        [Column(TypeName = "decimal(12,2)")]
        public decimal DueAmount { get; set; }

    }
}
