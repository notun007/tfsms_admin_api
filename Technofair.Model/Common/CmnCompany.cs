using TFSMS.Admin.Model.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TFSMS.Admin.Model.Common
{
    public class CmnCompany
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string? ShortName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string ContactNo { get; set; }
        public string? AlternatePhone { get; set; }        
        public string Email { get; set; }        
        public int? CmnCompanyId { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
        public int? CmnCountryId { get; set; }
        public int? CmnDivisionId { get; set; }// division/state
        public int? CmnDistrictId { get; set; }// district/city
        public int? CmnUpazillaId { get; set; }//upazilla/thana
        public int? CmnUnionId { get; set; }// union/area
        public string? Zip { get; set; }
        public string? Fax { get; set; }
        public string? Web { get; set; }
        public string? Logo { get; set; }
        public string? Prefix { get; set; }
        public string? WelcomeNote { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }

    }
}
