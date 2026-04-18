using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.TFAdmin;

namespace Technofair.Model.ViewModel.TFAdmin
{
    public class BillGeneratableCompanyCustomerViewModel
    {

        public int TFACompanyCustomerId { get; set; }
        public string CompanyCustomerName { get; set; }
        public string CompanyCustomerAddress { get; set; }
        public string? SmsApiBaseUrl { get; set; }
        public int TFAClientPackageId { get; set; }
        public int TFACompanyPackageTypeId { get; set; }
        public int? TFACompanyPackageId { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool IsApproved { get; set; }
        public Int64? TFAClientPaymentDetailId { get; set; }
        public string? CompanyPackageTypeName { get; set; }
        public string? PackageName { get; set; }
        public int? NumberOfAssignedDevice { get; set; }
        public int? NumberOfLivePackage { get; set; }
        public bool BillExists { get; set; }     
    
        public Int16 TFAMonthId { get; set; }
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public Int16 Year { get; set; }
    }
}
