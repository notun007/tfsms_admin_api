using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Loan
    {
        public enum LnDeviceLenderTypeEnum:Int16
        {
            Distributor = 1,
            MainServiceOperator = 2
        }
        public enum LnLoanModelEnum : Int16
        {
            Standard = 1,
            DoubleFlow = 2
        }

        public enum LnLoanCollectionTypeEnum: Int16
        {            
            DownPayment = 1,
            Installment = 2,
            Advance = 3,
            Settlement = 4
        }

        //
    }
}
