using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class TFAClientInvoiceReportRequestViewModel
    {
       public int TFACompanyCustomerId { get; set; }
       public int InvoiceId { get; set; }
       public DateTime DateTo { get; set; }


    }
}
