using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Security
{
    public class SecDashboardPermission
    {
        public int Id { get; set; }
        public int SecRoleId { get; set; }
        public int SecDashboardId { get; set; }
        public bool IsPermitted { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        
        
    }
}
