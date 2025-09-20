using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;


namespace TFSMS.Admin.Model.TFLoan.Device
{  
    public class LnDeviceLoanCollection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Int64 LoanId { get; set; }
        public LnDeviceLoanDisbursement Loan { get; set; }
    
        public Int16 LnLoanCollectionTypeId { get; set; }
        public LnLoanCollectionType LnLoanCollectionType { get; set; }
        public int LenderId { get; set; }
        public CmnCompany Lender { get; set; }
        public int LoaneeId { get; set; }
        public CmnCompany Loanee { get; set; }
        public string? LenderCode { get; set; }
        public string? LoaneeCode { get; set; }       
        public decimal Amount { get; set; }


        //[Column(TypeName = "decimal(12, 2)")]
        //public decimal? PaymentChargePercent { get; set; }
        //[Column(TypeName = "decimal(12, 3)")]
        //public decimal? PaymentCharge { get; set; }
        public string? Remarks { get; set; }

        public DateTime CollectionDate { get; set; }
        public string TransactionId { get; set; }
        #region New
        public Int16? AnFFinancialServiceProviderTypeId { get; set; }
        public short? AnFFinancialServiceProviderId { get; set; }
        public short? AnFBranchId { get; set; }
        public short? AnFAccountInfoId { get; set; }
       
        #endregion

        public bool IsCancel { get; set; }
        public int? CancelBy { get; set; }
        public DateTime? CancelDate { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
