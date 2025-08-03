using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Security
{
    public  class SecMenuPermission
    {
        public int Id { get; set; }
        public int? SecRoleId { get; set; }
        public int? SecUserId { get; set; }
        public int SecMenuId { get; set; }
        public bool Add { get; set; }
        public bool Read { get; set; }
        public bool Edit { get; set; }
        public bool Delete { get; set; }
        public bool Print { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }        
    }
}
