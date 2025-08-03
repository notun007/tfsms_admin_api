using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Security
{
    public class SecMenuViewModel
    {
        //public SecMenuViewModel()
        //{
        //    items = new List<SecMenuViewModel>();
        //}

        //public int Id { get; set; }
        //public Nullable<int> SecMenuId { get; set; }


        //public List<SecMenuViewModel> items { get; set; }


        public int Id { get; set; }
        public string TitleBn { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string? Link { get; set; }
        public int? SecMenuId { get; set; }
        public int ParentMenuId { get; set; }
        public int SecModuleId { get; set; }
        public int ParentSerialNo { get; set; }
        public int ChildSerialNo { get; set; }
        public int LevelNo { get; set; }
        public bool IsParent { get; set; }
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

        public Nullable<int> SecRoleId { get; set; }
        public string? RoleName { get; set; }

        public string? Module { get; set; }
        public int ModuleSeq { get; set; }
    }
}