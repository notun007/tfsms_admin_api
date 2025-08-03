using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecDashboardPermissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> SecRoleId { get; set; }
        public Nullable<int> SecDashboardId { get; set; }
        public bool IsPermitted { get; set; }
    }
}