using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TFSMS.Admin.Model.Common;
namespace TFSMS.Admin.Model.ViewModel.Subscription
{
    public class ScpDeviceAssignViewModel
    {
        public int Id { get; set; }
        public int ScpSubscriberId { get; set; }
        public Int64 PrdDeviceNumberId { get; set; }
        public int? PrdCardNumberId { get; set; }
        //public string? AccountNo { get; set; }
        public bool IsActive { get; set; }

        public int? Amount { get; set; }
        public string? DeviceNumber { get; set; }
        public string? CardNumber { get; set; }
        public string? CustomerNumber { get; set; }
        public bool? IsPayable { get; set; }
    }
}
