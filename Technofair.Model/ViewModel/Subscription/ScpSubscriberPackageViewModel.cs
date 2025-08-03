using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFSMS.Admin.Model.Common;
#nullable disable
namespace TFSMS.Admin.Model.ViewModel.Subscription
{
    public class ScpSubscriberPackageViewModel
    {
        public int Id { get; set; }
        //public int? ScpDeviceAssignId { get; set; }
        public int ScpSubscriberId { get; set; }
        public Int64? PrdDeviceNumberId { get; set; }
        public int ScpPackageId { get; set; }
        public decimal Amount { get; set; }
        public int? Discount { get; set; }
        public bool? IsFree { get; set; }
        public int? FreeDays { get; set; }
        public bool IsActive { get; set; }
        public Int16? ScpPackageStatusId { get; set; }
        public bool IsCancel { get; set; }
        public int? CancelledBy { get; set; }
        public DateTime? CancelledDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? RefNo { get; set; }
        public Int16 PackageType { get; set; }//1=Daily,2=Monthly,3=Yearly
        public Int16 Period { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? CASStartDate { get; set; }
        public DateTime? CASEndDate { get; set; }
        public string? SubscriberName { get; set; }
        public string? SubscriberAddress { get; set; }
        public string? SubscriberContact { get; set; }
        public string? SubscriberEmail { get; set; }
        public string? DeviceNumber { get; set; }
        public string? PackageName { get; set; }
        public string? PackageTypeString { get; set; }
        public string? Remarks { get; set; }
        public string? WalletNo { get; set; }
        public string? TrxID { get; set; }
        public Int16? AnFPaymentMethodId { get; set; }
        public bool? IsPaid { get; set; }

        public decimal? Rate { get; set; }
        public int? InvoiceAmount { get; set; }
        public string? StatusType { get; set; }
        public int? IsExpired { get; set; }
        public Int16? ActivateType { get; set; }//1=New,2=Renew,6=Cancel
        public string? ActivateTypeString { get; set; }
        public int? CmnCompanyId { get; set; }
        //New
        public bool? IsFirst { get; set; }
        public bool? IsLast { get; set; }

        public Nullable<int> RechargeBy { get; set; } //Make Not Null after updating database
        public string? CustomerNumber { get;set; }
        public int? ScpSubscriberPackageId { get; set; }
        public Int64? ScpSubscriberInvoiceDetailId { get; set; }
    }
}
