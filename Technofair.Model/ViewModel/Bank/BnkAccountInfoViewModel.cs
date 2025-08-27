using Technofair.Model.Bank;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Technofair.Model.ViewModel.Bank
{
    public  class BnkAccountInfoViewModel
    {
        public int Id { get; set; }

        [Required]
        public int BnkBranchId { get; set; }
        public int? BnkBankId { get; set; }

        [Required]
        [MaxLength(128)]
        public string AccountName { get; set; }

        [Required]
        [MaxLength(32)]
        public string AccountNo { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int CreatedBy { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }


        //public int BnkBankId { get; set; }
        //public string BankName { get; set; }
        //public string BranchName { get; set; }
    }
}
