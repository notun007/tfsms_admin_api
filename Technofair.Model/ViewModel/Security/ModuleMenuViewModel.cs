using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class ModuleMenuViewModel
    {

        //----------
        public string Name { get; set; }
        public string? NameBn { get; set; }
        public string Icon { get; set; }
        public string Link { get; set; }

        public int SecModuleId { get; set; }
        public int? ParentMenuId { get; set; }
        public int? ParentSerialNo { get; set; }
        public int? ChildSerialNo { get; set; }
        public int? IsParent { get; set; } //New By Asad & Extra
        public bool? IsActive { get; set; } //New By Asad & Extra
        public bool? IsModule { get; set; } //New By Asad & Extra
        public int? LevelNo { get; set; } //New By Asad & Extra
        public int? SerialNo { get; set; } //New By Asad & Extra
        public int? Tier { get; set; } //New By Asad & Extra
        

    }
}
