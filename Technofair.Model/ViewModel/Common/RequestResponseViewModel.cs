using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class RequestResponseViewModel
    {
        public string processId { get; set; }
        public string customerMobileNumber { get; set; }
        public string transactionId { get; set; }
        public string partnerAccountNumber { get; set; }
        public string surecashAccountNo { get; set; }
        public string partnerCode { get; set; }
        public string billNo { get; set; }
        public string customerId { get; set; }
        public string amount { get; set; }

        public string status { get; set; }
        public string statusCode { get; set; }
        public string note { get; set; }
        public string description { get; set; }

        //for SMS response
        public string error { get; set; }

        //for Rocket
        public string errCode { get; set; }
        public string billerCode { get; set; }
        public string refNo1 { get; set; }
        public string refNo2 { get; set; }
        public string billAmount { get; set; }
        public string txnId { get; set; }
        public string txnDate { get; set; }
        public string Initiator { get; set; }
        public string benfMobNo { get; set; }


        //for Nagad
        public string Message { get; set; }
        public string StudentId { get; set; }
        public string nagad_txn_id { get; set; }
        public string paidAmount { get; set; }
        public string txnTime { get; set; }
        public string paidVia { get; set; }
        public string nagad_payee_no { get; set; }//Amount Receiver Mobile No
        public string nagad_payer_no { get; set; }//Amount Payor Mobile No
        public string error_code { get; set; }
    }
}
