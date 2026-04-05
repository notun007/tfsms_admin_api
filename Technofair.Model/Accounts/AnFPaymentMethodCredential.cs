using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;

namespace Technofair.Model.Accounts
{
    public class AnFPaymentMethodCredential
    {
        public Int16 Id { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
        public int CmnCompanyId { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }
        public AnFPaymentMethod AnFPaymentMethod { get; set; }
        public Int16? AnFPaymentChannelId { get; set; }
        public AnFPaymentChannel AnFPaymentChannel { get; set; }
        public Int16? ScpSplitLevelId { get; set; }
        //public ScpSplitLevel ScpSplitLevel { get; set; }
        public string? PartnerCode { get; set; }
        public string UserID { get; set; }
        public string AuthorizationCode { get; set; }
        public string AccountNo { get; set; }
        public bool? IsActive { get; set; }
        public string? AppKey { get; set; }
        public string? AppSecret { get; set; }
        public string? PaymentUrl { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
