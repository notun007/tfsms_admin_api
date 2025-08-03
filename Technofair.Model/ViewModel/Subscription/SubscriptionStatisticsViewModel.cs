using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFSMS.Admin.Model.ViewModel.Subscription
{
    public class SubscriptionStatisticsViewModel
    {
        public int CmnCompanyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set;}
        public int NotExpiredCount { get; set; }
        public int ExpiredCount { get; set; }
        public int LockedCount { get; set; }
        public int DisableCount { get; set; }
        public int PayableCount { get; set; }
        public int FreeCount { get; set; }
        public int TotalCount { get; set; }
        
    }
}
