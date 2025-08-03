using System;
using System.Collections.Generic;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.ViewModel.Accounts
{
    public class AnFClientPackageViewModel
    {
        public int Id { get; set; }
        public int CmnCompanyCustomerId { get; set; }
        public int AnFCompanyPackageTypeId { get; set; }
        public int? AnFCompanyPackageId { get; set; }
        public DateTime Date { get; set; }
        public Nullable<decimal> Discount { get; set; }
        public bool IsActive { get; set; }
        public bool? IsFixed { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public string? CompanyCustomerName { get; set; }
        public string? CompanyPackageTypeName { get; set; }
        public string? MinMaxSubscriber { get; set; }



        //---------Omadul
        //public int SlsCustomerId { get; set; }
        //public string PackageType { get; set; }

    }
}
