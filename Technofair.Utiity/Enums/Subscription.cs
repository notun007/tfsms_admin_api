using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Subscription
    {
        public enum PackageType : Int16
        {
            Daily = 1,
            Monthly = 2,
            Yearly = 3
        }

        public enum ScpSubscriptionTypeEnum: Int16
        {
            New = 1,
            Renew = 2,
            DeviceReplacement = 3
        }

        public enum ScpPackageStatusEnum: Int16
        {
            Active = 1,
            InActive = 2,
            Cancel = 6,
            CancelAgainstDeviceReturn = 7,
            Specify = 9
        }

        public enum ScpFreePackageStatus: Int16 //Used Only In Free Packages
        {
            Active = 1,
            InActive = 2,
            Cancel = 6
        }

        public enum ScpSplitLevelEnum : Int16
        {
            One = 1,
            Two = 2,
            Three = 3
        }

        public enum AnFPaymentMethodEnum : Int16
        {
            Cash = 1,
            BkashBillPay = 2,
            RocketBillPay = 3, 
            NagadBillPay = 4,
            SSL = 5,
            BkashPGW = 6,
            RocketPGW = 7,
            NagadPGW = 8,
            CellfinBillPay = 9
        }


        public enum AnFTransactionStatusEnum : Int16
        {
            Success = 1,
            Failed = 2,
            Cancelled = 3
        }

        public enum PeriodEnum: Int16
        {
            One = 1,
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5,
            Six = 6,
            Seven = 7,
            Eight = 8,
            Nine = 9,
            Ten = 10,
            Eleven = 11,
            Twelve = 12
        }

        public enum ScpMessageTypeEnum: Int16
        {
                PreRenewalReminder = 1,
                OverdueRenewalReminder = 2,
                WarnningMessage = 3,
                Bulletins = 4
        }
    }
}
