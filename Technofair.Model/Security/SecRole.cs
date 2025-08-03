using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Security
{
    public  class SecRole
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Int16? CmnCompanyTypeId { get; set; }
        //public int Level { get; set; }
        //public Nullable<int> SecUserId { get; set; }
        public bool IsActive { get; set; }
        //public Nullable<int> CmnCompanyId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
