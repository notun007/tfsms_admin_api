using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Accounts;

namespace TFSMS.Admin.Model.Utility
{
    public class RevenueSubscription
    {

        //Subscription
        public bool IsEntitled { get; set; }
        public bool IsBackendSuccess { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CASStartDate { get; set; }
        public DateTime? CASEndDate { get; set; }

        //Renenue
        public Int64? ScpSubscriberInvoiceDetailId { get; set; }
        public Int16 LnLoanCollectionTypeId { get; set; }
       
        public Int16 AnFPaymentMethodId { get; set; }
        public Int16? AnFPaymentChannelId { get; set; }
        
        public int CreatedBy { get; set; }
      
        public decimal? DeviceAmount { get; set; } = 0;
        public decimal? DistributorDeviceAmount { get; set; } = 0;

        public decimal? DistributorDeviceSplitAmount { get; set; } = 0;
   
        public decimal? MSOSplitAmount { get; set; } = 0;
        public decimal? FirstLevelCommission { get; set; } = 0;
        public decimal? SecondLevelCommission { get; set; } = 0;
        public decimal? FirstLevelAmount { get; set; } = 0;
        public decimal? SecondLevelAmount { get; set; } = 0;
        public decimal? ThirdLevelAmount { get; set; } = 0;


        public decimal? PaymentChargePercent { get; set; }
        public decimal? DistributorPaymentCharge { get; set; }
        public decimal? MSOPaymentCharge { get; set; }
        public decimal? LSOPaymentCharge { get; set; }
        public decimal? SLSOPaymentCharge { get; set; }

        public int DistributorCompanyId { get; set; }
        public int MSOCompanyId { get; set; }
        public int? LSOCompanyId { get; set; }
        public int? SLSOCompanyId { get; set; }

        public string? DistributorCode { get; set; }
        public string? MSOCode { get; set; }
        public string? LSOCode { get; set; }
        public string? SLSOCode { get; set; }
        public bool IsPaid { get; set; } = true;
        public bool IsMonthlyInstallmentComplete { get; set; } = true;
        public string? Remarks { get; set; }
        public string? AppKey { get; set; }

    }
}
