using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.Security
{
    [Table(name: "SecMenus")]
    public class SecMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [StringLength(128)]
        public string? TitleBn { get; set; } = string.Empty;
        [Required(ErrorMessage = "Menu Title is required.")]
        [StringLength(128)]
        public string Title { get; set; } = string.Empty;
        [StringLength(256)]
        public string? Link { get; set; }
        public int? ParentMenuId { get; set; }
        public int SecModuleId { get; set; }
        public int ParentSerialNo { get; set; }
        public int ChildSerialNo { get; set; }
        public int LevelNo { get; set; }
        [StringLength(1)]
        public bool IsParent { get; set; }
        [StringLength(256)]
        public string? Icon { get; set; }
        public bool IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool IsModule { get; set; }
    }
}
