using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Integrator
    {
        public enum CAS: Int16
        {
            Cryptoguard = 1,
            Kingvon = 2,
            ABV = 3,
            Gospell = 4,
            NSTV = 5,
            Ensurity = 6,
            Telecast = 7,
            Sumavision = 8
        }

        public enum SVCASCommand
        {
            Reserved = 0,
            Authorizition = 1,
            OSDDisplay = 2,
            ResetPIN = 3,
            StartFingerprint = 6,
            SetTerminalStatus = 11,
            //SendEmail = 13,
            StartStopMandatoryMessage = 13,
            ConditionalAddressing = 14,
            SetACValue = 15,
            SetAreaInformation = 17,
            CancelUrgencyBroadcasting = 18,
            CloseAnAccount = 21,
            OpenAnAccount = 27,
            ClearAuthorizationsOfASingleTerminal = 31,
            CheckAuthorizationInformationOfAsingleTerminal = 32
        }
    }
}
