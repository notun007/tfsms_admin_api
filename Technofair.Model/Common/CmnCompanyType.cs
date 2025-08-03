using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Common
{
    [Serializable]
    public class CmnCompanyType
    {
        public Int16 Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public Int16 SerialNo { get; set; }
        public bool IsActive { get; set; }
    }
}
