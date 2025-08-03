using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.ViewModel.Subscription;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class RequestResponseSMSSentViewModel
    {
        public string status { get; set; }
        public string error { get; set; }
        public string smslog_id { get; set; }
        public string queue { get; set; }
        public string to { get; set; }
        public string error_string { get; set; }
        public string timestamp { get; set; }
        public int response_code { get; set; }
        public string error_message { get; set; }


        private statusInfo gpStatus = null;
        public statusInfo statusInfo
        {
            get
            {
                if (gpStatus == null)
                {
                    gpStatus = new statusInfo();

                }
                return gpStatus;
            }
            set
            {
                gpStatus = value;
            }
        }
    }

    public class ResponseSMSSentViewModel
    {
        public List<RequestResponseSMSSentViewModel> data { get; set; }
    }

    public class statusInfo
    {
        public string statusCode { get; set; }
        public string errordescription { get; set; }
        public string clienttransid { get; set; }
        public string serverReferenceCode { get; set; }
        public string availablebalance { get; set; }
    }


}
