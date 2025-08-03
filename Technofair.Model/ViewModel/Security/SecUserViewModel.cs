using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecUserViewModel
    {
        public int Id { get; set; }
        public int SecUserTypeId { get; set; }
        public bool IsPowerUser { get; set; }
        public Nullable<int> HrmEmployeeId { get; set; }
        public string LoginID { get; set; }
        public string Password { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> LevelNo { get; set; }
        //public Nullable<int> ParentUserId { get; set; }
        public Nullable<int> CmnCompanyId { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        public Int16 Type { get; set; }
        public string? EmployeeID { get; set; }
        public string? EmployeeName { get; set; }
        public string? PhotoUrl { get; set; }
        public string CompanyName { get; set; }
        public string CompanyType { get; set; }
        public string? RoleName { get; set; }
        public string? Prefix { get; set; }
        public Int16? CmnCompanyTypeId { get; set; }
    }
}