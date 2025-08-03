using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFAPaymentRequestProcess
    {
        public long Id { get; set; }
        public string ProcessID { get; set; }
        public Int16 AnFPaymentMethodId { get; set; }
        public int PrdDeviceNumberId { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string? PaymentID { get; set; }
    }
}
