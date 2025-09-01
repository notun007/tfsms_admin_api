using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;


namespace TFSMS.Admin.Model.ViewModel.TFLoan
{
    public class LnDeviceLoanCollectionViewModel
    {
        public int Id { get; set; }
        public Int64 LoanId { get; set; }
        public Int16 LnLoanCollectionTypeId { get; set; }       
        public string? CollectionType { get; set; }
        public int LenderId { get; set; }
        public string? Lender { get; set; }
        public int LoaneeId { get; set; }
        public string? Loanee { get; set; }
        public string? LenderCode { get; set; }
        public string? LoaneeCode { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal Amount { get; set; }
        public decimal? PaymentChargePercent { get; set; }
        public decimal? PaymentCharge { get; set; }
        public string? Remarks { get; set; }
        public DateTime CollectionDate { get; set; }


        #region New
        public Int16? AnFFinancialServiceProviderTypeId { get; set; }
        public int? BnkBankId { get; set; }
        public int? BnkBranchId { get; set; }
        public int? BnkAccountInfoId { get; set; }
        #endregion

        public bool IsCancel { get; set; }
        public int? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string loanNo { get; set; }
    }
}
