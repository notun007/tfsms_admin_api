using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFSMS.Admin.Model.Common;
#nullable disable
namespace TFSMS.Admin.Model.ViewModel.Subscription
{
    public class ScpSubscriberViewModel
    {
        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public Int64? PrdDeviceNumberId { get; set; }
        public string CustomerNumber { get; set; }
        public string? Code { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public Int16? Gender { get; set; }//1=Male,2=Female,3=Others
        public DateTime? BirthDate { get; set; }
        public string ContactNumber { get; set; }
        public string? Email { get; set; }
        public string? NIDNo { get; set; }
        public string? KYC { get; set; }
        public int? CmnCountryId { get; set; }
        public int? CmnDivisionId { get; set; }
        public int? CmnDistrictId { get; set; }
        public int? CmnUpazillaId { get; set; }
        public int? CmnUnionId { get; set; }
        public string? AreaName { get; set; }
        public string? Address { get; set; }
        public string? PostCode { get; set; }
        public int? SubscriberType { get; set; }//1=General,2=Corporate
        public string? SubscriberCategory { get; set; }
        public string? SubscriptionModel { get; set; }
        public string? Password { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string NetID { get; set; }

        public string CompanyName { get; set; }
        public string Type { get; set; }
        public string GenderType { get; set; }
        public string Country { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazilla { get; set; }
        public string? Union { get; set; }
        public string? DeviceNumber { get; set; }
        public int? PrdCardNumberId { get; set; }
        public bool? DeviceStatus { get; set; }
        public int? ScpPackageId { get; set; }
        public string? PackageName { get; set; }
        public Int16? PackageType { get; set; }
        //public int Amount { get; set; }
        public int? Rate { get; set; }
        public decimal? Amount { get; set; }
        public bool IsRenewable { get; set; }
        public int? IsExpired { get; set; }
        public string? StatusType { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public bool? IsPayable { get; set; }
        public string? LoginID { get; set; }
        public int? ScpSubscriberId { get; set; }
        public string? keyValue { get; set; }
        public string? PackageStatus { get; set; }
        public Int16? ScpPackageStatusId { get; set; }
        public Int16? Period { get; set; }

        private List<ScpDeviceAssignViewModel> view = null;
        public List<ScpDeviceAssignViewModel> Devices
        {
            get
            {
                if (view == null)
                {
                    view = new List<ScpDeviceAssignViewModel>();

                }
                return view;
            }
            set
            {
                view = value;
            }
        }

        private ScpSubscriberPackageViewModel packageView = null;
        public ScpSubscriberPackageViewModel Package
        {
            get
            {
                if (packageView == null)
                {
                    packageView = new ScpSubscriberPackageViewModel();

                }
                return packageView;
            }
            set
            {
                packageView = value;
            }
        }

        public long ScpSubscriberInvoiceDetailId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public DateTime? CASStartDate { get; set; }
        public DateTime? CASEndDate { get; set; }
                	
        public int ScpSubscriberPackageId { get; set; }
        
        public Int16? ActivateType { get; set; }//1=New,2=Renew
        public int? ExpiryDays { get; set; }
    }
}
