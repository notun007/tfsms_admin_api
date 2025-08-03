using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Report.Model.Report
{
    public class AlternetPaymentLogResponseVm
    {
        //new
        public long Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
        public string CustomerNo { get; set; }
        public string SubscriberName { get; set; }
        public string TrxStatus { get; set; }
        public string DeviceNumber { get; set; }
        public string PackageName { get; set; }
        public Decimal FirstLevelCommission { get; set; }
        public Decimal FirstLevelAmount { get; set; }
        public Decimal SecondLevelCommission { get; set; }
        public Decimal SecondLevelAmount { get; set; }
        public Decimal ThirdLevelAmount { get; set; }
        public int PaymentAmount { get; set; }
        public string EmployeeName { get; set; }
        public string CompanyName { get; set; }
        public string LogoUrl { get; set; }
        public byte[] Logo { get; set; }
        public string CompanyAddress { get; set; }
        public string PrintDate { get; set; }


        //old
        //public DateTime PaymentDate { get; set; }
        //public string PackageName { get; set; }
        //public string TransactionId { get; set; }
        //public string TrxStatus { get; set; }        
        //public string DeviceNumber { get; set; }
        //public string SubscriberName { get; set; }
        //public double CommissionAmount { get; set; }
        //public double Amount { get; set; }       

    }
}
