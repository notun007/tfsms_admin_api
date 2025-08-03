using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    [Serializable]
    public class HrmEmployeeSummary
    {
        public int Id { get; set; }
        public string DateOfBirth { get; set; }
        public string JoiningDate { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string DesignationName { get; set; }
        public string EmployeeTypeName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string OfficeName { get; set; }
        //public string ProjectName { get; set; }
        public string GradeName { get; set; }
        public Boolean IsActive { get; set; }
        public string Company { get; set; }
        public string ContactNo { get; set; }
        public string Email { get; set; }
        public decimal Gross { get; set; }
        public Int16 Priority { get; set; }
    }
}
