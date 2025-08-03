using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TFSMS.Admin.Model.TFAdmin;

namespace TFSMS.Admin.Model.ViewModel.TFAdmin
{
    public class TFABillGenPermssionViewModel
    {
        public int Id { get; set; }
        public Int16 TFAMonthId { get; set; }      
        public Int16 Year { get; set; }
        public Boolean IsClose { get; set; }
        public int CloseBy { get; set; }
        public DateTime CloseDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        public string? FullName { get; set; }
        public string? ShortName { get; set; }
        public string? MonthYear { get; set; }

    }
}
