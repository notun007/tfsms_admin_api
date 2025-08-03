using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.HRM
{
    public  class HrmDesignation
    {
        public int Id { get; set; }
        //public int CmnCompanyId { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public Nullable<int> HrmDesignationId { get; set; }        
        public Int16 Priority { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
    }
}
