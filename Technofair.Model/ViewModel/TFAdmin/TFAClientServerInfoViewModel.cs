using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.TFAdmin;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class TFAClientServerInfoViewModel
    {
        public int Id { get; set; }
        public int TFACompanyCustomerId { get; set; }
        public string ServerIP { get; set; }
        public string MotherBoardId { get; set; }
        public string NetworkAdapterId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string? TFACompanyCustomerName { get; set; }

        public string[]? ServerIPList { get; set; }
        public string[]? MotherBoardList { get; set; }
        public string[]? NetworkAdapterList { get; set; }

    }
}
