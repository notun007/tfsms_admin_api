using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Technofair.Model.Bank
{
    public class BnkBranch
    {
        public int Id { get; set; }
        public int BnkBankId { get; set; }
        public BnkBank BnkBank { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Remarks { get; set; }
        public Nullable<bool> Status { get; set; }

        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDate { get; set; }


    }
}
