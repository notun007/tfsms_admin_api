using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAClientServerInfo
    {
        public int Id { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public TFACompanyCustomer CmnCompanyCustomer { get; set; }
        public string? ServerIP { get; set; }
        public string? MotherBoardId { get; set; }
        public string? NetworkAdapterId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
