using TFSMS.Admin.Model.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TFSMS.Admin.Model.Security
{
    public  class SecModule
    {       
        public int Id { get; set; }
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public string? NameBn { get; set; }
        public string Icon { get; set; }
        public int SerialNo { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }      
    }
}
