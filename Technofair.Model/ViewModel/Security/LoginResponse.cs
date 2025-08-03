using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class LoginResponse
    {
        public string UserName { get; set; }
        public int UserId { get; set; }
        public int UserLevel { get; set; }
        public string? PhotoUrl { get; set; }
        public int RoleId { get; set; }
        public int CompanyId { get; set; }
        public string CompanyTypeShortName { get; set; }
        public int? DistrictId { get; set; }
        public int? UpazilaId { get; set; }
        public int? UnionId { get; set; }
        public CmnAppSetting CmnAppSetting { get; set; }        
        public string IsCompanyUser { get; set; }
        public bool IsAuhentic { get; set; }
        public string Message { get; set; }
        public bool IsGlobalBalance { get; set; }
        public Int16? LnLoanModelId { get; set; }
    }
}
