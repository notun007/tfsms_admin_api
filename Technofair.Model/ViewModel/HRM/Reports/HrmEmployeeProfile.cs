using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    [Serializable]
    public class HrmEmployeeProfile
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string EmployeeType { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string Department { get; set; }
        public string Section { get; set; }
        public string ReportingPersonnel { get; set; }
        public string Office { get; set; }
        public string Project { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string OfficialEmail { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public int ProbationPeriod { get; set; }
        public string Grade { get; set; }
        public Decimal? Basic { get; set; }
        public Decimal Gross { get; set; }
        public string JobLocation { get; set; }
        public string BankAccNo { get; set; }
        public string Bank { get; set; }
        public string PABXExtNo { get; set; }
        public string PhotoUrl { get; set; }
        public string AttendanceCardNo { get; set; }
        public Boolean IsRoastaringDuty { get; set; }
        public Boolean IsOTPayable { get; set; }
        public string SignatureUrl { get; set; }
        public Boolean IsActive { get; set; }
        public DateTime? DateOfDiscontinuation { get; set; }
        public string ReasonOfDiscontinuation { get; set; }
        public string Company { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string SpouseName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Nationality { get; set; }
        public string NationalId { get; set; }
        public string TIN { get; set; }
        public string BloodGroup { get; set; }
        public string MaritalStatus { get; set; }
        public int NoOfChildren { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string EmergencyContactName { get; set; }
        public string EmergencyContactAddress { get; set; }
        public string EmergencyContactNo { get; set; }
        public string EmergencyContactPersonRelation { get; set; }
        public string PassportNo { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpireDate { get; set; }
        public string IssuingAuthority { get; set; }
        public string Remarks { get; set; }

    }
}
