using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Security
{
    public  class SecUser
    {
        public int Id { get; set; }
        public int SecUserTypeId { get; set; }
        public SecUserType SecUserType { get; set; }
        public bool IsPowerUser { get; set; }
        public int? HrmEmployeeId { get; set; }
        public int CmnCompanyId { get; set; }
        public string LoginID { get; set; }        
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string? OriginalPassword { get; set; }
        public Nullable<int> LevelNo { get; set; }              
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        //public string? SecurePassword { get; set; }
        //public string? OriginalSecurePassword { get; set; }
    }
}
