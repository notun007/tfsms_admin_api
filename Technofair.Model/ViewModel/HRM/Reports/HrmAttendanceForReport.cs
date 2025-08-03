using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    public class HrmAttendanceForReport
    {
        public int Id { get; set; }
        public string EmployeeID { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public DateTime JoiningDate { get; set; }
        public int Priority { get; set; }
        public string CardNo { get; set; }
        public string OfficeName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string LateTime { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string Attendance { get; set; }
        public string Remarks { get; set; }
        public DateTime? Date { get; set; }
        public string Leaving { get; set; }
        public string WorkTime { get; set; }
        public string LateIn { get; set; }
        public string EarlyIn { get; set; }
        public string EarlyOut { get; set; }
        public string Company { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime? AttendanceDate { get; set; }
        public string Day
        {
            get { return Date.Value.DayOfWeek.ToString(); }
        }

        public int HrmDesignationId { get; set; }
        public int HrmOfficeId { get; set; }
        public int TotalWorkingDay { get; set; }
        public int TotalAbsentDay { get; set; }
        public int TotalLateDay { get; set; }
        public int TotalEarlyOutDay { get; set; }
        public string TotalWorkHour { get; set; }
        public string TotalLateIn { get; set; }
        public string TotalEarlyOut { get; set; }

        public int Total { get; set; }
        public int TotalPresent { get; set; }
        public int TotalAbsent { get; set; }
        public int TotalLeave { get; set; }
        public int TotalLate { get; set; }


        public bool HasRoster { get; set; }
        public bool IsHoliday { get; set; }
        public bool IsRosterHoliday { get; set; }

        public string OT { get; set; }
    }
}
