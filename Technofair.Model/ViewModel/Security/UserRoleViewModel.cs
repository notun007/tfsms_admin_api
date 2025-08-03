using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.Security;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class UserRoleViewModel
    {
        public int SecUserId { get; set; }
        public string SecUserTypeName { get; set; }
        public string UserLevel { get; set; }
        public string EmployeeName { get; set; }
        public string LoginID { get; set; }
        public string CompanyName { get; set; }
    }
}
