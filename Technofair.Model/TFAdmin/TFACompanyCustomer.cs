using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.TFAdmin
{
    public class TFACompanyCustomer
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public string Address { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonNo { get; set; }
        public int? CmnCountryId { get; set; }
        public int? CmnCurrencyId { get; set; }
        public string? BIN { get; set; }
        public string? Web { get; set; }
        public string? Logo { get; set; }

        public Int16? GraceDay { get; set; }
        public string? AppKey { get; set; }
        public string? ServerIP { get; set; }
        public string? MotherBoardId { get; set; }
        public string? NetworkAdapterId { get; set; }
        public string? SmsApiBaseUrl { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    }
}
