using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFLoan
{
    public class LOanScheduleRequestViewModel
    {
        public int LoaneeId {get;set;}
        public Int64 loanId { get; set; }
        public int createdBy { get; set; }
       
    }
}
