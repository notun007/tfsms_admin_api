using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    [Serializable]
    public class ChildMenu
    {
        public string label { get; set; }
        public string? labelBn { get; set; }
        public string icon { get; set; }
        public string routerLink { get; set; }
        public int SecModuleId { get; set; }
        public int? ParentMenuId { get; set; }
        public int? ParentSerialNo { get; set; }
        public int? ChildSerialNo { get; set; }
       
    }
    [Serializable]
    public class UserMenu
    {
        public string label { get; set; }
        public string? labelBn { get; set; }
        public string? Icon { get; set; }
        public List<ChildMenu> items { get => _items; set => _items = value; }

        private List<ChildMenu> _items = new List<ChildMenu>();
    }

    [Serializable]
    public class Modules
    {
        public string label { get; set; }
        public string? labelBn { get; set; }
        public string? Icon { get; set; }
        public List<UserMenu> items { get => _items; set => _items = value; }

        private List<UserMenu> _items = new List<UserMenu>();

    }
}
