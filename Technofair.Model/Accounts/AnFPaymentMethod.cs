using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.Accounts
{
    public class AnFPaymentMethod: CmnBaseEntity
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public Int16? AnFFinancialServiceProviderId { get; set; }
        public AnFFinancialServiceProvider AnFFinancialServiceProvider { get; set; }
        public Int16? AnFPaymentChannelId { get; set; }
        public AnFPaymentChannel AnFPaymentChannel { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public decimal? PaymentChargePercent { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }

    }
}
