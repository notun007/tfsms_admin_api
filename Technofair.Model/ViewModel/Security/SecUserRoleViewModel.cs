using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecUserRoleViewModel
    {
        public int Id { get; set; }
        public int SecUserId { get; set; }
        public int? SecRoleId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        
        public string? Name { get; set; }
        public string? LoginID { get; set; }
        public string? EmployeeName { get; set; }
        public string? CompanyName { get; set; }
        public string? SecUserTypeName { get; set; }
    }
}