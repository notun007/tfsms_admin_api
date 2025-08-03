using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    public class SearchViewModel
    {
        public int CmnCompanyId { get; set; }
        public int HrmCalendarYearId { get; set; }
        public int? HrmEmployeeId { get; set; }
        public string EmployeeID { get; set; }
        public int? HrmDivisionId { get; set; }
        public int? HrmDepartmentId { get; set; }
        public int? HrmSectionId { get; set; }
        public int? HrmDesignationId { get; set; }
        public int? HrmOfficeId { get; set; }
        public int? HrmGradeId { get; set; }
        public int? HrmEmployeeTypeId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public bool? IsRoastaringDuty { get; set; }
        public bool? IsOTPayable { get; set; }
        public bool? IsActive { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public Int16? CmnMonthId { get; set; }
    }
}
