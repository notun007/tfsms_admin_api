using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Security
{
    public  class SecUserRole
    {
        public int Id { get; set; }
        public int SecUserId { get; set; }
        public int SecRoleId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        
    }
}
