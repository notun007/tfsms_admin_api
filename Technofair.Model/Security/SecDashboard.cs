using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Security
{
    public  class SecDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SecModuleId { get; set; }
        public int CmnCompanyId { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }

    public class PermittedDashboard
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int SecModuleId { get; set; }
        public int CmnCompanyId { get; set; }
        public bool Status { get; set; }
        public string Tag { get; set; }
        public bool IsActive { get; set; }
    }




}
