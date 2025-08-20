using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.TFLoan.Device
{
    public class LnDeviceLenderLoaneePolicy
    {
        public Int16 Id { get; set; }
        public int LenderId { get; set; }
        public CmnCompany Lender { get; set; }
        public int LoaneeId { get; set; }
        public CmnCompany Loanee { get; set; }
        //public decimal MonthlyInstallmentAmount { get; set; }
        public decimal PerRechargeInstallmentAmount { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
