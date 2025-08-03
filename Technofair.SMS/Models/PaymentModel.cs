using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Web.Security;

namespace TFSMS.Admin.Web.Models
{
    public class PaymentModel 
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public string MobileNo { get; set; }
        public string TrxID { get; set; }

        public string Success { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }

    }
    public class AuthenticationModel
    {
        public string UserID { get; set; }
        public string AuthenticationCode { get; set; }        

    }

   
}
