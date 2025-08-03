using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Utiity.Enums
{
    public class Inventory
    {

        public enum InvSupplierTypeEnum : Int16
        {
            Organization = 1,
            Individual = 2
        }

        public enum InvDeviceStateEnum : Int16
        {

            Purchase = 1, //InvPurchaseId: StockInOrOut=true
            Challan = 2, //SlsChallanId: StockInOrOut=false
            Receive = 3, //InvMRRId: StockInOrOut=false
            PurchaseReturn = 4, //InvReturnId: StockInOrOut=false

            ChallanReturn = 5, //SlsReturnId: StockInOrOut=false
            ChallanReturnReceive = 6, //SlsReturnId: StockInOrOut=true
            Damage = 7, //DamageId: StockInOrOut=false
            DeviceAssign = 8, 
            Transfer = 9, //TransferId: StockInOrOut = false;
            SubscriberDeviceReturn = 10
        }

        public enum InvWarrantyPeriodEnum : Int16
        {
            NA = 1,
            ThreeMonths = 2,
            SixMonths = 3,
            OneYear = 4,
            TwoYears = 5,
            ThreeYears = 6
        }

    }
}
