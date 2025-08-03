using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Security
{
    public class SecWebsiteVisitor
    {
        public int Id { get; set; }
        public string IP { get; set; }
        public DateTime Date { get; set; }
    }
}
