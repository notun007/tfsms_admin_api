using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Technofair.Report.Model.TFAdmin
{
    //For Report Design:TFSMS.Admin.Model.ViewModel.TFAdmin
    public class TFAClientPaymentInvoiceViewModel
    {
        public long Id { get; set; }
        public string Subject { get; set; } = "Client Payment Invoice";
        public string InvoiceNumber { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyContactNo { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyCustomerName { get; set; }
        public string CompanyCustomerAddress { get; set; }
        public string CompanyCustomerContactNo { get; set; }
        public string CompanyCustomerEmail { get; set; }
        public int CompanyCustomerId { get; set; }
        public bool IsCollected { get; set; }
        public string BillingMonth { get; set; }
        public string PackageType { get; set; }
        public string PackageName { get; set; }
        public string Date { get; set; } = Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy");
        public decimal? Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal? Discount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalMonthlyBillAmount { get; set; }
        public DateTime ExpireDate { get; set; }
        public string BillGeneratorName { get; set; }
        public string PaymentStatus { get; set; } = "";

    }
}
