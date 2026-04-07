using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFAdmin
{
    public class ExtendExpireDateViewModel
    {
        public long TFAClientPaymentDetailId { get; set; }

        public Int16? GraceDay { get; set; }

        public DateTime? ExpireDate { get; set; }

        public DateTime ExtendedExpireDate { get; set; }

        public Int16? FinalGraceDay { get; set; }

        public int CreatedBy { get; set; }
    }
}
