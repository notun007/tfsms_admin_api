
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Policy;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Lib.Model;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.Subscription;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using Technofair.Service.TFAdmin;
using Technofair.Utiity.Http;

namespace TFSMS.Admin.Controllers.TFAdmin
{
    [Route("api/[controller]")]
    [ApiController]
    public class TFAClientBillController : ControllerBase
    {
        private ITFAClientBillService service;
        private ITFAClientPackageService serviceClientPackage;
        private ITFACompanyCustomerService serviceCompanyCustomer;
        private ITFAClientPaymentDetailService serviceClientPaymentDetail;
        private ITFAClientPaymentService serviceClientPayment;
        public TFAClientBillController()
        {
            var dbfactory = new AdminDatabaseFactory();
            service = new TFAClientBillService(new TFAClientBillRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceClientPackage = new TFAClientPackageService(new TFAClientPackageRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceCompanyCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceClientPaymentDetail = new TFAClientPaymentDetailService(new TFAClientPaymentDetailRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceClientPayment = new TFAClientPaymentService(new TFAClientPaymentRepository(dbfactory), new AdminUnitOfWork(dbfactory));
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("GenerateClientBill")]
        public async Task<Operation> GenerateClientBill([FromBody] GenerationClientBillViewModel obj)
        {
            Operation objOperation = service.GenerateClientBill(obj.billGenPermssionId, obj.TFACompanyCustomerId, obj.createdBy);


            try
            {
                if(objOperation.Success == true) { 
                //Commented for Testing
                var objClientPackages = serviceClientPackage.GetActiveClientPackageByCustomerId(obj.TFACompanyCustomerId); //null = All

                    foreach (var objClientPackage in objClientPackages)
                    {
                        var objCompanyCustomer = serviceCompanyCustomer.GetById(objClientPackage.TFACompanyCustomerId);
                        var smsApiBaseUrl = objCompanyCustomer.SmsApiBaseUrl;
                        var activePackageUrl = smsApiBaseUrl + "/api/SubscriberPackage/GetSubscriptionInfo?monthId=" + obj.monthId + "&year=" + obj.year;
                        var assignDeviceUrl = smsApiBaseUrl + "/api/DeviceAssign/GetDeviceInfo";
                        //Call like Calling CAS API for geting Quantity

                        var paymentDetail = await serviceClientPaymentDetail.clientPaymentDetails(objClientPackage.TFACompanyCustomerId, obj.monthId, obj.year);

                        //SubscriptionInfo
                        var objSubscriptionInfo = await Request<SubscriptionHistoryInfoViewModel, SubscriptionHistoryInfoViewModel>.GetObject(activePackageUrl);

                        if (objSubscriptionInfo != null)
                        {
                            paymentDetail.NumberOfLivePackage = objSubscriptionInfo.NumberOfLivePackage;
                        }

                        //DeviceInfo
                        var objDeviceInfo = await Request<DeviceInfoViewModel, DeviceInfoViewModel>.GetObject(assignDeviceUrl);

                        if (objDeviceInfo != null)
                        {
                            paymentDetail.NumberOfAssignedDevice = objDeviceInfo.NumberOfAssignedDevice;
                        }

                        if (objClientPackage.TFACompanyPackageTypeId == 2) //Rate Per Subscriber
                        {
                            paymentDetail.Amount = (paymentDetail.Rate == null ? 0 : Convert.ToDecimal(paymentDetail.Rate)) * (paymentDetail.NumberOfLivePackage == null ? 0 : Convert.ToDecimal(paymentDetail.NumberOfLivePackage));
                        }

                        paymentDetail.TotalAmount = paymentDetail.Amount - (paymentDetail.Discount == null ? 0 : Convert.ToDecimal(paymentDetail.Discount));

                        var clientPackage = serviceClientPackage.GetClientPackageByCustomerId(objClientPackage.TFACompanyCustomerId);


                        serviceClientPaymentDetail.Update(paymentDetail);
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return objOperation;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetClientInvoice")]
        public List<TFAClientInvoiceViewModel> GetClientInvoice(int companyCustomerId)
        {
            List<TFAClientInvoiceViewModel> list =  service.GetClientInvoice(companyCustomerId);
            
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetClientApprovedUnpaidBill")]
        public List<TFAClientInvoiceViewModel> GetClientApprovedUnpaidBill(int companyCustomerId)
        {
            List<TFAClientInvoiceViewModel> list = service.GetClientApprovedUnpaidBill(companyCustomerId);

            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetClientApprovedBill")]
        public List<TFAClientInvoiceViewModel> GetClientApprovedBill(int companyCustomerId)
        {
            List<TFAClientInvoiceViewModel> list = service.GetClientApprovedBill(companyCustomerId);

            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("ApproveClientBill")]
        public Operation ApproveClientBill(int id, int approveBy)
        {
            Operation obj = new Operation();
            obj = service.ApproveClientBill(id, approveBy);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public Operation Save([FromBody] RequestObject obj)
        {
            var cmnCompanyCustomerId = obj.CmnCompanyCustomerId;
            var issueDate = obj.issueDate;
            var list = obj.list;

            foreach( var item in list )
            {
                TFAClientPaymentDetail  clientPackage = serviceClientPaymentDetail.GetById(item.TFAClientPaymentDetailId);

                clientPackage.IsPaid = item.IsPaid;
                clientPackage.PaymentDate = issueDate;
                clientPackage.PaidBy = obj.PaidBy;
                clientPackage.ModifiedDate = DateTime.Now;
                serviceClientPaymentDetail.Update(clientPackage);
            }
            return null;
        }
        public class RequestObject
        {
            public int PaidBy { get; set; }
            public int CmnCompanyCustomerId { get; set; }
            public DateTime? issueDate { get; set; }

            private List<TFAClientInvoiceViewModel> view = null;
            public List<TFAClientInvoiceViewModel> list
            {
                get
                {
                    if (view == null)
                    {
                        view = new List<TFAClientInvoiceViewModel>();
                    }
                    return view;
                }
                set
                {
                    view = value;
                }
            }

        }
    }
}
