using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Common
    {
        public enum CmnCompanyTypeEnum : Int16
        {
            MSO = 1,
            LSO = 2,
            SLSO = 3,
            SP = 4
        }

        public enum AnFPaymentChannelsEnum : Int16
        {
            CASH = 1,
            PGW = 2,
            BillPay = 3,
            Online = 4,
            PGWBank = 5
        }


    }
}
