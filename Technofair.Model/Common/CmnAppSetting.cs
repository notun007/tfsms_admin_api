using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.TFLoan.Device;

namespace TFSMS.Admin.Model.Common
{
    public class CmnAppSetting
    {
        public int Id { get; set; }
        public Int16? ApplicationId { get; set; }
        public bool AllowSubscriberNumberWithPrefix { get; set; }
        public bool AllowAutoSubscriberNumber { get; set; }
        public int SubscriberNumberLength { get; set; }
        public string InitialSubscriberNumber { get; set; }
        public bool AllowPurchase { get; set; }
        public bool AllowSale { get; set; }
        public bool? AllowRenewableArrear { get; set; }

        public bool AllowNewPackageWithCash { get; set; }
        public bool AllowPackageRenewalWithCash { get; set; }
        public bool IsGlobalBalance { get; set; }
        public Int16? LnLoanModelId { get; set; }        
        public LnLoanModel LnLoanModel { get; set; }
        public bool? AllowDeviceLoanAtCash { get; set; }
        public bool? AllowDeviceLoanAtBkash { get; set; }
        public bool? AllowMigration { get; set; }
        
        public bool IsProduction { get; set; }
        public string? AppKey { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
