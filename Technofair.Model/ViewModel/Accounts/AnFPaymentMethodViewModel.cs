using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;

namespace TFSMS.Admin.Model.ViewModel.Accounts
{
    public class AnFPaymentMethodViewModel
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public Int16? AnFFinancialServiceProviderId { get; set; }       
        public Int16? AnFPaymentChannelId { get; set; }
        public string? UserId { get; set; }
        public string? Password { get; set; }
        public decimal? PaymentChargePercent { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }        
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }       
        public DateTime? ModifiedDate { get; set; }


        public string? FinancialServiceProvider { get; set; }
        public string? PaymentChannel{ get; set; }
    }
}
