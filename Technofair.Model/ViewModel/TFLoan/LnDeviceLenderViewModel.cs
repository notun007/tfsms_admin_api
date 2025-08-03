using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;


namespace TFSMS.Admin.Model.ViewModel.TFLoan
{
    public class LnDeviceLenderViewModel
    {
        public Int16 Id { get; set; }
        public int CmnCompanyId { get; set; }       
        //public Int16? LnDeviceLenderTypeId { get; set; }      
        public Int16? LnDeviceParentLenderId { get; set; }       
        public bool? IsLoanRecoveryAgent { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string? CompanyName { get; set; }
        public string? DeviceLenderTypeName { get; set; }
        public string? LnDeviceParentLenderName { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
    }
}
