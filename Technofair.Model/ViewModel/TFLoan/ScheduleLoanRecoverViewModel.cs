using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class ScheduleLoanRecoverViewModel
    {
        public int Id { get; set; }
        public Int64 LoanId { get; set; } //do not send to sms api/db
        public string LoanNo { get; set; }
        public Int16 LnLoanCollectionTypeId { get; set; }

        public int LenderId { get; set; } //do not send to sms api/db
        public int LoaneeId { get; set; } //do not send to sms api/db

        public string LenderCode { get; set; }
        public string LoaneeCode { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }

        public decimal Amount { get; set; }

        public string? Remarks { get; set; }
        public DateTime CollectionDate { get; set; }
        public string? TransactionId { get; set; }

        #region New
        public Int16? AnFFinancialServiceProviderTypeId { get; set; }
        public int? BnkBankId { get; set; }
        public int? BnkBranchId { get; set; }
        public int? BnkAccountInfoId { get; set; }
        #endregion

        public bool? IsCancel { get; set; }
        public int? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
