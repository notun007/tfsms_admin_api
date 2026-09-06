using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Model.ViewModel.TFAdmin
{
    public class LatestBillGenPermissionViewModel
    {
        public string? CompanyName { get; set; }
        public string? MonthName { get; set; }
        public string? ShortName { get; set; }
        public int Year { get; set; }
        public bool IsClose { get; set; }
    }
}
