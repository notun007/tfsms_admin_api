using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LnDeviceLoanScheduleViewModel
    {
        public Int64 Id { get; set; }
        public Int64 LoanId { get; set; }

        public Int16 MonthId { get; set; }
        public Int16 Year { get; set; }
        public Int16 InstallmentNumber { get; set; }
        public decimal ScheduledPrincipal { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsPaid { get; set; } = false;
        public string? MonthName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
