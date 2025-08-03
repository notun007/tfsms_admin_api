using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class GenerationClientBillViewModel
    {
        public int year { get; set; }
        public int monthId { get; set; }  
        public int billGenPermssionId { get; set; }
        public int createdBy { get; set; }
        public int? TFACompanyCustomerId { get; set; }
    }
}
