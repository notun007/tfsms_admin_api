using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecRoleViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public Int16? CmnCompanyTypeId { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string? CmnCompanyTypeName { get; set; }
        public string? CmnCompanyTypeShortName { get; set; }
    }
}
