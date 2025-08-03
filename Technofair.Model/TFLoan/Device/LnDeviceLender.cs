using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.TFLoan.Device
{
    public class LnDeviceLender
    {
        public Int16 Id { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
        public CmnCompanyType CmnCompanyType { get; set; }
        public int CmnCompanyId { get; set; }
        public CmnCompany CmnCompany { get; set; }
        //public Int16? LnDeviceLenderTypeId { get; set; }
        //public LnDeviceLenderType LnDeviceLenderType { get; set; }
        public Int16? LnDeviceParentLenderId { get; set; }
        public LnDeviceLender LnDeviceParentLender { get; set; }
        public bool IsLoanRecoveryAgent { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
