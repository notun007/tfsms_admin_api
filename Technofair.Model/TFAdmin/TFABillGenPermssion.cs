using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFABillGenPermssion
    {
        public int Id { get; set; }
        public Int16 TFAMonthId { get; set; }
        public TFAMonth TFAMonth { get; set; }
        public Int16 Year { get; set; }
        public Boolean IsClose { get; set; }
        public int CloseBy { get; set; }
        public DateTime CloseDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
