using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections;

namespace TFSMS.Admin.Model.ViewModel.HRM
{
    [Serializable]
    public class HrmEmployeeViewModel
    {

        //new
        public int Id { get; set; }
        public int CmnCompanyId { get; set; }
        //public int? AppointedCompanyId { get; set; }
        public Int16 CmnCompanyTypeId { get; set; }

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
        public int? HrmSectionId { get; set; } //from dm

        public DateTime? JoiningDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int? ProbationPeriod { get; set; }

        public int? HrmGradeId { get; set; }
        public int? LineManager { get; set; }
        public Decimal? Basic { get; set; }
        public Decimal? Gross { get; set; }
        public string? JobLocation { get; set; } //nullable in db model
        public string? BankAccNo { get; set; } //nullable in db model
        public int? BnkBankId { get; set; }
        public string? PABXExtNo { get; set; } //nullable in db model
        public string? PhotoUrl { get; set; } //nullable in db model
        public string? AttendanceCardNo { get; set; } //nullable in db model
        public Boolean? IsRoastaringDuty { get; set; } //nullable in db model

        public Boolean? IsOTPayable { get; set; } //nullable in db model
        public string? SignatureUrl { get; set; } //nullable in db model
        public DateTime? DateOfDiscontinuation { get; set; }
        public string? ReasonOfDiscontinuation { get; set; } //nullable in db model
        private bool? IsSuperAdmin { get; set; }//New
        public bool? IsProxy { get; set; }
        public bool IsActive { get; set; }

        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        //End Of DB Model

        public string? DesignationName { get; set; }
        public string? EmployeeTypeName { get; set; }
        public string? DivisionName { get; set; }
        public string? DepartmentName { get; set; }
        public string? SectionName { get; set; }
        public string? OfficeName { get; set; }
        //public string ProjectName { get; set; }
        public string? GradeName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? BloodGroupString { get; set; }
        public string? ReligionString { get; set; }
        public string? CardNo { get; set; }
        public int? LeaveRefNo { get; set; }
        public Nullable<decimal> Days { get; set; }
        public DateTime? ApproveDateFrom { get; set; }
        public DateTime? ApproveDateTo { get; set; }
        public int? HrmLeaveId { get; set; }
        public int? HrmLeaveTypeId { get; set; }
        public decimal? NetPayable { get; set; }
        public Int16? Priority { get; set; }
        public string? RoleName { get; set; }//New

        //old
        //vm
        //public int Id { get; set; }
        //public int CmnCompanyId { get; set; }
        ////public int? AppointedCompanyId { get; set; }
        //public Int16 CmnCompanyTypeId { get; set; }

        //public string EmployeeId { get; set; }
        //public string Name { get; set; }
        //public string Mobile { get; set; }
        //public string OfficialEmail { get; set; }
        //public bool? Sex { get; set; }
        //public int? HrmDesignationId { get; set; }
        //public int? HrmEmployeeTypeId { get; set; }
        //public DateTime? ContractStartDate { get; set; }
        //public DateTime? ContractEndDate { get; set; }
        //public int? HrmEmployeeId { get; set; }
        //public int HrmOfficeId { get; set; }
        //public int? HrmDivisionId { get; set; }

        //public int? HrmDepartmentId { get; set; }
        //public int? HrmSectionId { get; set; } //from dm

        //public DateTime? JoiningDate { get; set; }
        //public DateTime? ConfirmationDate { get; set; }
        //public int? ProbationPeriod { get; set; }

        //public int? HrmGradeId { get; set; }
        //public int? LineManager { get; set; }
        //public Decimal? Basic { get; set; }
        //public Decimal? Gross { get; set; }
        //public string? JobLocation { get; set; } //nullable in db model
        //public string? BankAccNo { get; set; } //nullable in db model
        //public int? BnkBankId { get; set; }
        //public string? PABXExtNo { get; set; } //nullable in db model
        //public string? PhotoUrl { get; set; } //nullable in db model
        //public string? AttendanceCardNo { get; set; } //nullable in db model
        //public Boolean? IsRoastaringDuty { get; set; } //nullable in db model

        //public Boolean? IsOTPayable { get; set; } //nullable in db model
        //public string? SignatureUrl { get; set; } //nullable in db model
        //public DateTime? DateOfDiscontinuation { get; set; }
        //public string? ReasonOfDiscontinuation { get; set; } //nullable in db model
        //public bool IsSuperAdmin { get; set; }//New
        //public bool? IsProxy { get; set; }
        //public bool IsActive { get; set; }

        //public int CreatedBy { get; set; }
        //public DateTime CreatedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
        ////End Of DB Model

        //public string DesignationName { get; set; }
        //public string EmployeeTypeName { get; set; }
        //public string DivisionName { get; set; }
        //public string DepartmentName { get; set; }
        //public string SectionName { get; set; }
        //public string OfficeName { get; set; }
        ////public string ProjectName { get; set; }
        //public string GradeName { get; set; }
        //public string Gender { get; set; }
        //public string Email { get; set; }
        //public string BloodGroupString { get; set; }
        //public string ReligionString { get; set; }
        //public string CardNo { get; set; }
        //public int? LeaveRefNo { get; set; }
        //public Nullable<decimal> Days { get; set; }
        //public DateTime? ApproveDateFrom { get; set; }
        //public DateTime? ApproveDateTo { get; set; }
        //public int? HrmLeaveId { get; set; }
        //public int? HrmLeaveTypeId { get; set; }
        //public decimal NetPayable { get; set; }
        //public Int16 Priority { get; set; }
        //public string RoleName { get; set; }//New

    }

    public class HrmOverview
    {
        public int NoOfCurrentEmployee { get; set; }
        public int NoOfEmployeeLeave { get; set; }
        public int NoOfEmployeeFieldVisit { get; set; }
        public int NoOfEmployeeOutside { get; set; }
    }

}
