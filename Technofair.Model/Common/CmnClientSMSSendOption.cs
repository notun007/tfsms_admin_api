using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Common
{
    public class CmnClientSMSSendOption
    {
        public int Id { get; set; }
        public int CmnCompanyCustomerId { get; set; }
        public bool IsActive { get; set; }
        public bool IsAutoSend { get; set; }
    }
}
