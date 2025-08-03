using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSMS.Admin.Model.ViewModel.Accounts
{
    public class AnFClientPaymentDetailViewModel
    {
        public long Id { get; set; }
        public long AnFClientPaymentId { get; set; }
        public Int16 CmnMonthId { get; set; }
        public Int16 AnFCompanyServiceTypeId { get; set; }
        public int Quantity { get; set; }
        public Nullable<decimal> Rate { get; set; }//Rate would be null when fixed amount payment
        public Nullable<decimal> Discount { get; set; }
        public decimal Amount { get; set; }

        public string Month { get; set; }
        public string ServiceType { get; set; }
        public int? AnFCompanyPackageId { get; set; }
        public bool IsPaid { get; set; }

        private AnFClientPackageViewModel packageViewModel = null;
        public AnFClientPackageViewModel ClientPackage
        {
            get
            {
                if (packageViewModel == null)
                {
                    packageViewModel = new AnFClientPackageViewModel();

                }
                return packageViewModel;
            }
            set
            {
                packageViewModel = value;
            }
        }

    }
}