using TFSMS.Admin.Model.Accounts;

using System;
using System.Collections.Generic;

namespace TFSMS.Admin.Model.Common
{
    public class CmnFinancialYear : CmnBaseEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime OpeningDate { get; set; }
        public System.DateTime ClosingDate { get; set; }
        public bool Status { get; set; }
        public int CmnCompanyId { get; set; }
        public bool YearClosingStatus { get; set; }
      
    }
}
