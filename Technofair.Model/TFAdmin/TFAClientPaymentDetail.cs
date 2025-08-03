using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientPaymentDetail
    {
        public long Id { get; set; }
        public long TFAClientPaymentId { get; set; }
        public TFAClientPayment TFAClientPayment { get; set; }
        public Int16 Year { get; set; }
        public Int16 TFAMonthId { get; set; }
        public TFAMonth TFAMonth { get; set; }
        public Int16 AnFCompanyServiceTypeId { get; set; }
        public int? NumberOfAssignedDevice { get; set; }
        public int? NumberOfLivePackage { get; set; }
        public Nullable<decimal> Rate { get; set; }//Rate would be null when fixed amount payment
        public Nullable<decimal> Discount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime ExpireDate { get; set; }

        public bool IsApproved { get; set; }
        public int? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }

        //
        public bool IsPaid { get; set; }
        public int? PaidBy { get; set; }
        public DateTime? PaymentDate { get; set; }
        public Nullable<DateTime> DueDate { get; set; }
        public bool IsCancelled { get; set; }
        public string? CancelReason { get; set; }
        public int? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }     
        //
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        
    }
}
