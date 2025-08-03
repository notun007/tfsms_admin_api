using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFLoan.Device
{
    public class LnLoanCollectionType
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public string? Remarks { get; set; }
        public bool? IsManual { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
