using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.Accounts
{
    public class AnFPaymentMethodCredentialViewModel
    {
        public Int16 Id { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
        public int CmnCompanyId { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }
        public Int16? AnFPaymentChannelId { get; set; }
        public Int16? ScpSplitLevelId { get; set; }
        public string? PartnerCode { get; set; }
        public string UserID { get; set; }
        public string AuthorizationCode { get; set; }
        public string AccountNo { get; set; }
        public bool? IsActive { get; set; }
        public string? AppKey { get; set; }
        public string? AppSecret { get; set; }
        public string? PaymentUrl { get; set; }
        public string? PaymentMethod { get; set; }
        public string? PaymentChannel { get; set; }
        public string? SplitLevel { get; set; }
        public string? CompanyTypeName { get; set; }
        public string? CompanyName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
