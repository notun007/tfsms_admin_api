using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Accounts
{
    public class AnFCompanyCollectionViewModel
    {
        public long Id { get; set; }
        public int CmnFinancialYearId { get; set; }
        public int SlsCustomerId { get; set; }
        public DateTime Date { get; set; }
        public long AnFClientPaymentId { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }
        public string WalletNo { get; set; }
        public string TrxID { get; set; }
        public decimal TotalAmount { get; set; }
        public Nullable<long> AnFVoucherId { get; set; }
        public string Remarks { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string PaymentMethod { get; set; }
        public string ClientName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Web { get; set; }
    }


}