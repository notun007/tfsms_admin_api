using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Lib
{
    public class CryptoGuard
    {

        public const string AddSubScriber = "api/CGAPI/AddNewCustomer";
        public const string UpdateSubScriber = "api/CGAPI/UpdateCustomer";
        public const string GetCustomerByCustomerNumber = "api/CGAPI/CustomerByCustomerNumber";
        public const string GetCustomers = "api/CGAPI/GetCustomers";

        public const string AddCard = "api/CGAPI/AddNewCard";
        public const string AssignCard = "api/CGAPI/AssignCard";
        public const string GetCard = "api/CGAPI/GetCard";
        public const string ValidateCardNumber = "api/CGAPI/ValidateCardNumber";
        public const string InactiveCard = "api/CGAPI/InActiveCard";
        public const string ReativeCard = "api/CGAPI/ReActiveCard";
        public const string PairCard = "api/CGAPI/PairCard";
        public const string UnPairCard = "api/CGAPI/UnPairCard";
        public const string AddBlacklistCard = "api/CGAPI/AddBlacklistCard";
        public const string RemoveBlacklist = "api/CGAPI/RemoveBlacklist";
        public const string RemoveSTBByDeviceId = "api/CGAPI/RemoveSTBByDeviceId";

        public const string AddSubscription = "api/CGAPI/AddSubscription";
        public const string UpdateSubscription = "api/CGAPI/UpdateSubscription";
        public const string RenewSubscription = "api/CGAPI/RenewSubscription";
        public const string StopSubscription = "api/CGAPI/StopSubscription";

        public const string InActivePackage = "api/CGAPI/InActivePackage";
        public const string ReActivePackage = "api/CGAPI/ReActivePackage";       
        public const string PackageListByNetId = "api/CGAPI/PackageListByNetId";

        public const string GetSubscriptionsbyCardNo = "api/CGAPI/SubscriptionsbyCardNo";

        public const string NetworkList = "api/CGAPI/GetNetworkList";
        public const string PackageChannelList = "api/CGAPI/PackageChannelList";
        public const string AddChannelToPackage = "api/CGAPI/PackageChannelList";
        public const string ChannelList = "api/CGAPI/GetChannelList";
        public const string RemoveChannelToPackage = "api/CGAPI/RemoveChannelToPackage";


        public const string SendMessage = "api/CGAPI/SendMessage";
        public const string SendLongMessage = "api/CGAPI/SendLongMessage";
        public const string SendScrollMessage = "api/CGAPI/SendScrollMessage";

    }
}
