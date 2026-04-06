using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFAdmin
{
    public class ClientSubscriptionSummaryViewModel
    {
       
            public int Id { get; set; }
            public int TFACompanyCustomerId { get; set; }
            public int TFACompanyPackageTypeId { get; set; }
            public int? TFACompanyPackageId { get; set; }
            public DateTime Date { get; set; }
            public decimal? Rate { get; set; }
            public decimal? Discount { get; set; }
            public bool IsActive { get; set; }
            public decimal? Amount { get; set; }
            public string? CompanyCustomerName { get; set; }
            public string? CompanyPackageTypeName { get; set; }
            public string? MinMaxSubscriber { get; set; }
            public DateTime? ExpireDate { get; set; }
            public int? Year { get; set; }
            public string? MonthName { get; set; }

            public int CreatedBy { get; set; }
            public DateTime CreatedDate { get; set; }

            public int? ModifiedBy { get; set; }
            public DateTime? ModifiedDate { get; set; }            
       
    }
}
