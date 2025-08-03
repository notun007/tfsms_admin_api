using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class VMMenu
    {

        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Link { get; set; }
        public string? IsParents { get; set; }
        public int? ParentsId { get; set; }
        public int ModuleId { get; set; }
        public string? Icon { get; set; }
        public int ParentSeqNo { get; set; }
        public int ChildSeqNo { get; set; }
        public string? IsActive { get; set; }
        public int? MenuLevel { get; set; }

        public string ? RoleName { get; set; }

        public string? Module { get; set; }
        public int ModuleSeq { get; set; }


    }
}
