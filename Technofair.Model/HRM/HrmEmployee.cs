using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections;
using TFSMS.Admin.Model.Common;

namespace TFSMS.Admin.Model.HRM
{
    //RoleName
    public class HrmEmployee
    {
        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        public CmnCompany CmnCompany { get; set; }

        //public int? AppointedCompanyId { get; set; }
        //public CmnCompany AppointedCompany { get; set; }

        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string OfficialEmail { get; set; }
        public bool? Sex { get; set; }
        public int? HrmDesignationId { get; set; }
        public int? HrmEmployeeTypeId { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }        
        public int? HrmEmployeeId { get; set; }
        public int? HrmOfficeId { get; set; }
        public int? HrmDivisionId { get; set; }
        public int? HrmDepartmentId { get; set; }
        public int? HrmSectionId { get; set; }
        public DateTime? JoiningDate { get; set; }
        
        public DateTime? ConfirmationDate { get; set; }
        public int? ProbationPeriod { get; set; }
        public int? HrmGradeId { get; set; }
        public int? LineManager { get; set; }
        public Decimal? Basic { get; set; }
        public Decimal? Gross { get; set; }
        public string? JobLocation { get; set; }
        public string? BankAccNo { get; set; }
        public int? BnkBankId { get; set; }
        public string? PABXExtNo { get; set; }
        public string? PhotoUrl { get; set; }
        public string? AttendanceCardNo { get; set; }
        public Boolean? IsRoastaringDuty { get; set; }
        public Boolean? IsOTPayable { get; set; }
        public string? SignatureUrl { get; set; }
        public DateTime? DateOfDiscontinuation { get; set; }
        public string? ReasonOfDiscontinuation { get; set; }
        public bool IsSuperAdmin { get; set; }//New
        public bool? IsProxy { get; set; }
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
    }
}
