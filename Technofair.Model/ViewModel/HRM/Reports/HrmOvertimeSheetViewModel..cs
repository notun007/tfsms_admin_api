using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    public class HrmOvertimeSheetViewModel
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string JoiningDate { get; set; }        
        public string DesignationName { get; set; }
        public int HrmDesignationId { get; set; }
        public int HrmEmployeeTypeId { get; set; }
        public string EmployeeTypeName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string OfficeName { get; set; }
        public string Company { get; set; }
        public decimal Gross { get; set; }

        public string OTHour { get; set; }
        public string TotalOTHour { get; set; }
        public decimal OTRate { get; set; }
        public decimal OTBonus { get; set; }
        public decimal OTAmount { get; set; }
        public decimal NetAmount { get; set; }

        private List<string> otHour = null;
        public List<string> Overtimes
        {
            get
            {
                if (otHour == null)
                {
                    otHour = new List<string>();
                }
                return otHour;
            }
            set
            {
                otHour = value;
            }
        }

    }
}
