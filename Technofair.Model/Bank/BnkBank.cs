using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technofair.Model.Bank
{
    public class BnkBank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string? Address { get; set; }
        public Nullable<Int16> Type { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }
        

    }
}
