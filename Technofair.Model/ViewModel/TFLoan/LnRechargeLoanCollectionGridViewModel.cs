using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LnRechargeLoanCollectionGridViewModel
    {

        public long Id { get; set; }
        public long LoanId { get; set; }
        public int LenderId { get; set; }
        public int LoaneeId { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal NetAmount { get; set; }
        public string? Remarks { get; set; }
        public DateTime CollectionDate { get; set; }
        public string? TransactionId { get; set; }

        public Int16 AnFFinancialServiceProviderTypeId { get; set; }
        public Int16? AnFFinancialServiceProviderId { get; set; }
        public Int16? AnFBranchId { get; set; }
        public Int16? AnFAccountInfoId { get; set; }
        public int? BnkBankId { get; set; }
        public int? BnkBranchId { get; set; }
        public int? BnkAccountInfoId { get; set; }

        public bool IsCancel { get; set; }
        public int? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public string? FinancialServiceProviderTypeName { get; set; }
        public string? BankName { get; set; }
        public string? BranchName { get; set; }
        public string? AccountNo { get; set; }
    }
}
