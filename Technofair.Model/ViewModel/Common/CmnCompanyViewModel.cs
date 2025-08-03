using TFSMS.Admin.Model.Accounts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TFSMS.Admin.Model.ViewModel.Common
{
    public class CmnCompanyViewModel
    {

        //New
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }//
        public string? ShortName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }//
        public string ContactNo { get; set; }
        public string? AlternatePhone { get; set; }
        public string Email { get; set; }//

        //public string LoginId { get; set; }//New
        //public string Password { get; set; }//New
        public int? CmnCompanyId { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }
        public int? CmnCountryId { get; set; }//--//
        public int? CmnDivisionId { get; set; }// division/state
        public int? CmnDistrictId { get; set; }// district/city
        public int? CmnUpazillaId { get; set; }//upazilla/thana //--//
        public int? CmnUnionId { get; set; }// union/area
        public string? Zip { get; set; }
        public string? Fax { get; set; }//
        public string? Web { get; set; }
        public string? Logo { get; set; }
        public string? Prefix { get; set; }//
        public string? WelcomeNote { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }//
        public DateTime CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }


        //--------------------------------------
       // public string? WelcomeNote { get; set; } //New
        public int? SecUserTypeId { get; set; } //New

        public string? CompanyType { get; set; }
        public string? ParentName { get; set; } //Made Nullable
        public string? Type { get; set; } //Made Nullable
        public string? Country { get; set; } //Made Nullable
        public string? Division { get; set; } //Made Nullable
        public string? District { get; set; } //Made Nullable
        public string? Upazilla { get; set; } //Made Nullable
        public string? Union { get; set; }

        public string? ParentLoginID { get; set; }
        public string? ParentPassword { get; set; }
        public string? LoginId { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public int? SlsCustomerId { get; set; }
        public string? ParentEmail { get; set; }
        public string? NameWithType { get; set; }
        public decimal? Balance { get; set; }

    }
}
