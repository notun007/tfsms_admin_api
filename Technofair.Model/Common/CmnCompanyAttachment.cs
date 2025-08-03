using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Common
{
    [Serializable]
    public class CmnCompanyAttachment
    {
        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public string FilePath { get; set; }
        public string? IdentificationNo { get; set; }
        public int? HrmFileCategoryId { get; set; }
    }
}
