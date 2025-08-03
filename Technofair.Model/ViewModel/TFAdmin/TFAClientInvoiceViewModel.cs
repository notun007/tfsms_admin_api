using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class TFAClientInvoiceViewModel
    {
        //public long Id { get; set; }
        public long TFAClientPaymentId { get; set; }
        public long TFAClientPaymentDetailId { get; set; }                
        public string MonthOfBill { get; set; }
        public string PackageType { get; set;}
        public string PackageName { get; set;}
        public int? NumberOfAssignedDevice { get; set;}
        public int? NumberOfLivePackage { get; set; }
        public decimal Amount { get; set;}
        public decimal Discount { get; set; }
        public decimal TotalAmount { get; set;}
        public DateTime ExpireDate { get; set;}
        public bool IsCollected { get; set; }
        public bool IsPaid { get; set; }
        

    }
}
