using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Security
{
    public class SecUserVisitor
    {
        public long Id { get; set; }
        public int LoginId { get; set; }
        public string IP { get; set; }
        public DateTime Date { get; set; }
        public Int16 Type { get; set; }
    }
}
