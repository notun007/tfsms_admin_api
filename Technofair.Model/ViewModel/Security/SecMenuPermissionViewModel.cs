using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecMenuPermissionViewModel
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
        public int ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string Title { get; set; }
    }
}