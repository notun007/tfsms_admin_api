using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Collections;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{

    public class HrmLeaveViewModel
    {
       public int? Id { get; set; }
        public int HrmLeaveApplicationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public string Remarks { get; set; }
        public decimal Days { get; set; }

       

        public int HrmLeaveTypeId { get; set; }
        public int HrmEmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string DesignationName { get; set; }
        public string LeaveType { get; set; }
        public string OfficeName { get; set; }
        public string RefNo { get; set; }
        public string Responsible { get; set; }
        public bool? IsHalfDay { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        //public string DateFromString { get; set; }
        //public string DateToString { get; set; }
    }
}
