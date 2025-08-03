using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class VMRoleBaseMenu
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool? IsActive { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? RoleName { get; set; }
    }
}
