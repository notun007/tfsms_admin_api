using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Security
    {
        public enum SecUserTypeEnum : Int32
        {
            SuperAdmin = 1,
            Administrator = 2,
            FrontlineUser = 3,
            Subscriber = 4
        }
    }
}
