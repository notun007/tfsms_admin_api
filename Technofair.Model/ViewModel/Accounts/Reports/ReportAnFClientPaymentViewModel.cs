using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TFSMS.Admin.Model.ViewModel.Accounts;

namespace TFSMS.Admin.Model.ViewModel.Accounts.Reports
{
    public class ReportAnFClientPaymentViewModel
    {
        public long Id { get; set; }
        public string RefNo { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int CmnCompanyCustomerId { get; set; }
        public int AnFClientPackageId { get; set; }
        public DateTime Date { get; set; }        
        public Int16 AnFPaymentMethodId { get; set; }
        public Nullable<DateTime> PaidDate { get; set; }
        public string WalletNo { get; set; }
        public string TrxID { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<decimal> TotalDiscount { get; set; }
        public Nullable<long> AnFVoucherId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        public bool IsCancelled { get; set; }
        public Nullable<int> CancelledBy { get; set; }
        public Nullable<DateTime> CancelledDate { get; set; }
        public string CancelReason { get; set; }
        public bool IsCollected { get; set; }

        public string PaymentMethod { get; set; }
        public DateTime DueDate { get; set; }
        public bool HasProblem { get; set; }
        public int MonthSerial { get; set; }


        private List<AnFClientPaymentDetailViewModel> datail = null;
        public List<AnFClientPaymentDetailViewModel> Details
        {
            get
            {
                if (datail == null)
                {
                    datail = new List<AnFClientPaymentDetailViewModel>();

                }
                return datail;
            }
            set
            {
                datail = value;
            }
        }

        public string AmountInWord { get; set; }
        public string Barcode { get; set; }
    }


}