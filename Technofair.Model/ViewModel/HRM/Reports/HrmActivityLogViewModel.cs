using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TFSMS.Admin.Model.ViewModel.HRM.Reports
{
    [Serializable]
    public class HrmActivityLogViewModel
    {
        public int Id { get; set; }
        public int TaskAssgntoId { get; set; }
        public string RefNo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }



        public string Status { get; set; }
        public string AssignDate { get; set; }
        public string AssignBy { get; set; }
        public string AssignTo { get; set; }




        public string Remarks { get; set; }
        public string FilePath { get; set; }

    }
}
