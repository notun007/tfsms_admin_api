using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Security
{
    public class SecCompanyModule
    {
        public int Id { get; set; }
        public int SecModuleId { get; set; }
        public int CmnCompanyId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
