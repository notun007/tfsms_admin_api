using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Common
{
    public class CmnSMSGateway   
    {
        public Int16 Id { get; set; }
        public string Provider { get; set; }
        public string UserID { get; set; }
        public string AuthorizationCode { get; set; }
        public string? SenderID { get; set; }
        public string? MaskingTitle { get; set; }
        public string Url { get; set; }        
        public Int16 Priority { get; set; }
        public bool IsActive { get; set; }
        public bool IsSelfProvider { get; set; }
    }
}
