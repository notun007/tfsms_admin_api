using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Data;
using Technofair.Data.Repository.Accounts;
using Technofair.Lib.Model; 
using Technofair.Lib.Utilities;
using Technofair.Model.Accounts;
using Technofair.Service.Accounts;
using Technofair.Utiity.Key;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.Accounts.Reports;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Models;
using TFSMS.Admin.Service.TFAdmin;
using static TFSMS.Admin.Models.SSLCommerz;



namespace TFSMS.Admin.Controllers.TFAdmin
{
   
    [Route("api/[Controller]")]
    public class TFAClientPaymentController : ControllerBase
    {

        private ITFAClientPaymentService service;
        private ITFAClientPaymentDetailService serviceDetail;
        private ITFAClientPaymentRepository repository;
        private ITFACompanyCustomerService serviceComCustomer;
        private IWebHostEnvironment _hostingEnvironment;
        private IAnFPaymentMethodCredentialService servicePaymentCredential;
        AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();
        public TFAClientPaymentController(IWebHostEnvironment hostingEnvironment)
        {
            repository = new TFAClientPaymentRepository(dbfactory);
            service = new TFAClientPaymentService(repository, new AdminUnitOfWork(dbfactory));
            serviceDetail = new TFAClientPaymentDetailService(new TFAClientPaymentDetailRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceComCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            //servicePaymentCredential = new AnFPaymentMethodCredentialService(new AnFPaymentMethodCredentialRepository(dbfactory), new UnitOfWork(dbfactory));
            _hostingEnvironment = hostingEnvironment;
        }



        //[HttpPost("GetAll")]
        //public async Task<List<AnFPaymentMethod>> GetCompanyPaymentMethod()
        //{
        //    string domain = GetHost();
        //    List<AnFPaymentMethod> list = service.GetCompanyPaymentMethod(domain);
        //    return list;
        //}

        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] RequestObject objReq)
        {
            Operation objOperation = new Operation { Success = false };
            Operation objOperationDetail = new Operation { Success = false };
            long paymentId = 0;
            if (objReq != null && objReq.obj != null && objReq.list != null)
            {
                TFAClientPayment obj = objReq.obj;
                List<TFAClientPaymentDetail> list = objReq.list;
                using (var dbContextTransaction = repository.BeginTransaction())
                {
                    try
                    {
                        //if (obj.TFAPaymentMethodId != null && obj.TFAPaymentMethodId > 0 && obj.PaidDate == null)
                        //{
                        //    obj.PaidDate = DateTime.Now;
                        //}

                        //if (obj.Id == 0)//&& ButtonPermission.Add
                        //{
                        //    if (list.Count > 0 && list != null)
                        //    {
                        //        obj.Date = DateTime.Now;//issue date                            
                        //        obj.DueDate = obj.Date.AddDays(10);//10 days from issue date
                        //                                           //obj.CreatedBy = userId;
                        //        obj.CreatedDate = DateTime.Now;
                        //        objOperation = service.SaveWithTransaction(obj);
                        //        if (objOperation.Success)
                        //        {
                        //            paymentId = obj.Id;
                        //            SaveDetail(paymentId, list);
                        //            repository.SaveChanges();
                        //            repository.Commit(dbContextTransaction);
                        //        }
                        //    }
                        //}
                        //else if (obj.Id > 0 && !obj.IsCollected)//it is not updateable because already collected to company
                        //{
                        //    obj.ModifiedDate = DateTime.Now;
                        //    objOperation = service.UpdateWithTransaction(obj);
                        //    if (objOperation.Success)
                        //    {
                        //        paymentId = obj.Id;
                        //        UpdateDetail(paymentId, list);

                        //        repository.SaveChanges();
                        //        repository.Commit(dbContextTransaction);
                        //    }
                        //}
                    }
                    catch (Exception ex)
                    {
                        repository.Rollback(dbContextTransaction);
                        objOperation.Success = false;
                        throw ex;
                    }
                }
            }
            return objOperation;
        }

        private void SaveDetail(long paymentId, List<TFAClientPaymentDetail> list)
        {
            Operation objOperation = new Operation { Success = false };
            long detailId = 0;
            foreach (TFAClientPaymentDetail objDetail in list)
            {
                objDetail.Id = detailId;
                objDetail.TFAClientPaymentId = paymentId;
                objOperation = serviceDetail.SaveWithTransaction(objDetail);
                if (objOperation.Success)
                {
                    if (detailId == 0)
                    {
                        detailId = objDetail.Id;
                        detailId++;
                    }
                    else
                    {
                        detailId++;
                    }
                }
            }
        }
        private void UpdateDetail(long paymentId, List<TFAClientPaymentDetail> list)
        {
            Operation objOperation = new Operation { Success = false };
            long detailId = 0;
            List<TFAClientPaymentDetail> listExist = serviceDetail.GetByPaymentId(paymentId);
            if (listExist != null && listExist.Count > 0)
            {
                if (list != null && list.Count > 0)
                {
                    foreach (TFAClientPaymentDetail objD in listExist)
                    {
                        TFAClientPaymentDetail? objExist = (from objView in list where objView.TFAMonthId == objD.TFAMonthId select objView).FirstOrDefault();
                        if (objExist == null)
                        {
                            objOperation = serviceDetail.DeleteWithTransaction(objD);
                        }
                    }
                }
                else
                {
                    foreach (TFAClientPaymentDetail objD in listExist)
                    {
                        objOperation = serviceDetail.DeleteWithTransaction(objD);
                    }
                }
            }

            if (list != null && list.Count > 0)
            {
                foreach (TFAClientPaymentDetail objDetail in list)
                {
                    TFAClientPaymentDetail objExist = serviceDetail.GetByPaymentAndMonthId(paymentId, objDetail.TFAMonthId);
                    if (objExist == null)
                    {
                        objDetail.Id = detailId;
                        objDetail.TFAClientPaymentId = paymentId;
                        objOperation = serviceDetail.SaveWithTransaction(objDetail);
                        if (objOperation.Success)
                        {
                            if (detailId == 0)
                            {
                                detailId = Convert.ToInt64(objOperation.OperationId);
                                detailId++;
                            }
                            else
                            {
                                detailId++;
                            }
                        }
                    }
                    else if (objExist != null && objExist.Id > 0)
                    {
                        objDetail.Id = objExist.Id;
                        objDetail.TFAClientPaymentId = objExist.TFAClientPaymentId;
                        objOperation = serviceDetail.UpdateWithTransaction(objDetail);
                    }
                }
            }

        }




        //[HttpPost("Cancel")]
        //public async Task<Operation> Cancel([FromBody] TFAClientPayment obj)
        //{
        //    Operation objOperation = new Operation { Success = false };
        //    try
        //    {
        //        if (obj.Id > 0 && !obj.IsCollected)//it is not cancelable because already collected to company
        //        {
        //            obj.IsCancelled = true;
        //            //obj.CancelledBy = userId;
        //            obj.CancelledDate = DateTime.Now;
        //            objOperation = service.Update(obj);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return objOperation;
        //}

        //[HttpPost("GetMonthlyPayableByClientAndYearId")]
        //public async Task<List<AnFClientPaymentDetailViewModel>> GetMonthlyPayableByClientAndYearId(int yearId)
        //{
        //    List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
        //    TFACompanyCustomer objClient = new TFACompanyCustomer();
        //    //string domain = "www.abc.com";
        //    string domain = GetHost();
        //    //Commented By Asad Temporarily On 06.07.2024
        //    //objClient = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
        //    if (objClient != null)
        //    {
        //        list = service.GetMonthlyPayableByClientAndYearId(yearId, objClient.Id, domain);
        //    }
        //    return list;
        //}

        //[Authorize(Policy = "Authenticated")]
        //[HttpPost("GetMonthlyPaidAndPayableByYearAndMonthId")]
        //public async Task<List<AnFClientPaymentDetailViewModel>> GetMonthlyPaidAndPayableByYearAndMonthId(long paymentId, int yearId)
        //{
        //    List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
        //    CmnCompanyCustomer objClient = new CmnCompanyCustomer();
        //    //string hostName = "www.abc.com";
        //    string domain = GetHost();
        //    objClient = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
        //    if (objClient != null)
        //    {
        //        list = service.GetMonthlyPaidAndPayableByYearAndClienId(paymentId, yearId, objClient.Id, domain);
        //    }
        //    return list;
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetPaymentByPaymentId")]
        public async Task<ReportAnFClientPaymentViewModel> GetPaymentByPaymentId(long paymentId, string domain)
        {
            if (domain == null || domain == "")
            {
                //domain = "www.kmpss.edu.bd";
                domain = GetHost();
            }
            ReportAnFClientPaymentViewModel obj = service.GetPaymentByPaymentId(paymentId, domain);
            return obj;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetMonthlyPaidByPaymentId")]
        public async Task<List<AnFClientPaymentDetailViewModel>> GetMonthlyPaidByPaymentId(long paymentId, string domain)
        {
            List<AnFClientPaymentDetailViewModel> list = service.GetMonthlyPaidByPaymentId(paymentId, domain);
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetPaymentInfoByDate")]
        public async Task<List<AnFClientPaymentViewModel>> GetPaymentInfoByDate(DateTime dateFrom, DateTime dateTo, bool? isAll)
        {
            List<AnFClientPaymentViewModel> list = new List<AnFClientPaymentViewModel>();
            string domain = GetHost();
            if (domain != null && domain != "")
            {
                list = service.GetPaymentByDateAndDomain(dateFrom, dateTo, domain, isAll);
            }
            return list;
        }

        private Operation InsertDetail(long paymentId, List<TFAClientPaymentDetail> list, int yearId, string domain)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                foreach (TFAClientPaymentDetail objDetail in list)
                {
                    TFAClientPaymentDetail objExist = serviceDetail.GetDetailByYearAndMonthId(yearId, objDetail.TFAMonthId, domain);
                    if (objExist == null)
                    {
                        objDetail.TFAClientPaymentId = paymentId;
                        objOperation = serviceDetail.InsertPaymentDetail(objDetail, domain);
                    }
                }
                return objOperation;
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                throw ex;
            }
        }
        private Operation UpdateDetail(long paymentId, List<TFAClientPaymentDetail> list, int yearId, string domain)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                //for delete first
                List<TFAClientPaymentDetail> existList = new List<TFAClientPaymentDetail>();
                existList = serviceDetail.GetDetailByPaymentIdAndDomain(paymentId, domain);
                if (existList != null && existList.Count > 0)
                {
                    if (list == null)
                    {
                        foreach (TFAClientPaymentDetail objDis in existList)
                        {
                            objOperation = serviceDetail.DeleteByIdAndDomain(objDis.Id, domain);
                        }
                    }
                    else if (list != null && list.Count > 0)
                    {
                        foreach (TFAClientPaymentDetail objDis in existList)
                        {
                            TFAClientPaymentDetail objExist = (from TFAClientPaymentDetail d in list where (d.TFAClientPaymentId == objDis.TFAClientPaymentId && d.TFAMonthId == objDis.TFAMonthId) select d).FirstOrDefault();
                            if (objExist == null)
                            {
                                objOperation = serviceDetail.DeleteByIdAndDomain(objDis.Id, domain);
                            }
                        }
                    }
                }

                if (list != null && list.Count > 0)
                {
                    foreach (TFAClientPaymentDetail objDetail in list)
                    {
                        TFAClientPaymentDetail objExist = serviceDetail.GetDetailByYearAndMonthId(yearId, objDetail.TFAMonthId, domain);
                        if (objExist == null)
                        {
                            objDetail.TFAClientPaymentId = paymentId;
                            objOperation = serviceDetail.InsertPaymentDetail(objDetail, domain);
                        }
                        else if (objExist != null && objExist.Id > 0)
                        {
                            objDetail.Id = objExist.Id;
                            objOperation = serviceDetail.UpdatePaymentDetail(objDetail, domain);
                        }
                    }
                }
                return objOperation;
            }
            catch (Exception ex)
            {
                objOperation.Success = false;
                throw ex;
            }
        }
        //private void SendSMS(CmnClient obj,string billNo)
        //{
        //    CmnSMSGateway objGateway = CustomHelper.GetActiveSMSGateway();
        //    string toRecipient = "";
        //    string message = "You have a software bill (no. " + billNo + ") unpaid.Please pay within due date.Your ID is " + obj.Code + ".OneZeroBD Limited";
        //    string sub = obj.ContactNo.Trim().Substring(0, 2);
        //    if (!sub.Contains("88"))
        //    {
        //        toRecipient = "88" + obj.ContactNo.Trim();
        //    }
        //    else
        //    {
        //        toRecipient = obj.ContactNo.Trim();
        //    }

        //    RequestResponseSMSSentViewModel objResponse = CustomHelper.SendSMS(objGateway, toRecipient, message);
        //    if (objResponse.status == "OK")
        //    {

        //    }
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetActiveClient")]
        public async Task<List<TFACompanyCustomer>> GetActiveClient()
        {
            //string hostName = "www.Technofair.com";
            //string domain = GetHost();
            List<TFACompanyCustomer> list = serviceComCustomer.GetAll();// Technofair.Lib.Utilities.Helper.GetActiveClient();
            if (list != null && list.Count > 0)
            {
                list = list.Where(w => w.IsActive).ToList();
            }
            return list;
        }
        //[HttpPost("GetClientPaymentInvoice")]
        //public List<TFAClientPaymentInvoiceViewModel> GetClientPaymentInvoice([FromBody] RequestReportObject objReq)
        //{
        //    List < TFAClientPaymentInvoiceViewModel > list= service.GetClientPaymentInvoice(objReq.obj.TFACompanyCustomerId, objReq.obj.InvoiceId);
        //    return list;
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetClientPaymentInvoice")]
        public async Task<object> CurrentStockForRDLC([FromBody] RequestReportObject objReq, string reportPath, string reportType)
        {
            object bytes = null; bool resstate = false;
            List<TFAClientPaymentInvoiceViewModel> list = new List<TFAClientPaymentInvoiceViewModel>();
            if (objReq != null)
            {
                TFAClientInvoiceReportRequestViewModel obj = objReq.obj;
                int?[] SelectedGroup = objReq.SelectedGroup;
                string?[] SelectedSubGroup = objReq.SelectedSubGroup;
                //if (obj.DateTo != null)
                //{
                //    //obj.DateFrom = Convert.ToDateTime(obj.DateFrom).ToString("yyyy-MM-dd");
                //    obj.DateTo = Convert.ToDateTime(obj.DateTo).ToString("yyyy-MM-dd");
                //}

                string group = "";
                if (SelectedGroup != null)
                {
                    for (int i = 0; i < SelectedGroup.Length; i++)
                    {
                        if (group != "")
                        {
                            group = group + "," + SelectedGroup[i].ToString();
                        }
                        else
                        {
                            group = SelectedGroup[i].ToString();
                        }
                    }
                }
                string product = "";
                if (SelectedSubGroup != null)
                {
                    for (int i = 0; i < SelectedSubGroup.Length; i++)
                    {
                        if (product != "")
                        {
                            product = product + "," + SelectedSubGroup[i].ToString();
                        }
                        else
                        {
                            product = SelectedSubGroup[i].ToString();
                        }
                    }
                }

                list = service.GetClientPaymentInvoice(obj.TFACompanyCustomerId, obj.InvoiceId);

                if (list != null && list.Count > 0)
                {
                    List<DataTable> listDataTable = new List<DataTable>();
                    DataTable dataList = new DataTable();

                    dataList = Extension.GetDataTable(list);
                   
                    dataList = Extension.AddDataSetName(dataList, "DataSet1");
                    listDataTable.Add(dataList);

                    string reportPaths = _hostingEnvironment.WebRootPath + reportPath;
                    reportType = string.IsNullOrEmpty(reportType) ? "PDF" : reportType;
                    bytes = ReportingService.Report(listDataTable, reportPaths, reportType);

                    if (bytes != null)
                    {
                        resstate = true;
                    }
                }
            }

            return new
            {
                bytes,
                resstate
            };

            //return list;
        }


        //[HttpPost("GetCompanySignatory")]
        //public async Task<HrmEmployeeViewModel> GetCompanySignatory()
        //{
        //    //string hostName = "www.kmpss.edu.bd";
        //    //string domain = GetHost();
        //    HrmEmployeeViewModel obj = Technofair.Lib.Utilities.Helper.GetCompanySignatory();
        //    if (obj != null)
        //    {
        //        string protocol = Request.Url.Scheme;
        //        if (protocol == "https")// get from current domain folder
        //        {
        //            obj.SignatureUrl = "/Content/Images/OZBDL/Signature.png";
        //        }
        //        else
        //        {
        //            if (obj.SignatureUrl != null && obj.SignatureUrl != "")
        //            {
        //                obj.SignatureUrl = "http://Technofair.com" + obj.SignatureUrl;
        //            }
        //            else// default signature
        //            {
        //                obj.SignatureUrl = "http://Technofair.com/Content/Images/OZBDL/Signature.png";
        //            }

        //        }
        //    }
        //    return obj;
        //}      

        private string GetHost()
        {
            //string hostName = "www.abc.com";
            //You can try the following code to get fully qualified domain name:
            //string host = Request.Url.Scheme + System.Uri.SchemeDelimiter + Request.Url.Host;
            //string hostName = HttpContext.Request.Url.Host;
            //if (!hostName.StartsWith("www."))
            //{
            //    hostName = "www." + hostName;
            //}
            string hostName = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            hostName += ":" + Request.HttpContext.Connection.RemotePort.ToString();
            return hostName;
        }

        public class RequestObject
        {
            private TFAClientPayment _obj = null;
            public TFAClientPayment obj
            {
                get
                {
                    if (_obj == null)
                    {
                        _obj = new TFAClientPayment();

                    }
                    return _obj;
                }
                set
                {
                    _obj = value;
                }
            }

            private List<TFAClientPaymentDetail> _list = null;
            public List<TFAClientPaymentDetail> list
            {
                get
                {
                    if (_list == null)
                    {
                        _list = new List<TFAClientPaymentDetail>();

                    }
                    return _list;
                }
                set
                {
                    _list = value;
                }
            }

        }


        public class RequestReportObject
        {
            public int?[] SelectedGroup { get; set; }
            public string?[] SelectedSubGroup { get; set; }
            private TFAClientInvoiceReportRequestViewModel _obj = null;
            public TFAClientInvoiceReportRequestViewModel obj
            {
                get
                {
                    if (_obj == null)
                    {
                        _obj = new TFAClientInvoiceReportRequestViewModel();

                    }
                    return _obj;
                }
                set
                {
                    _obj = value;
                }
            }

        }


        #region SSL Commerz
        static string redirectUrl = "";

        [HttpPost("GetSSLCommerzGrantToken")]//HttpPost
        public async Task<SSLCommerzInitResponse> GetSSLCommerzGrantToken(int fundSourceId, int amount, string remarks, Int16 paymentMethodId, int createdBy, string retUrl)
        {
            Operation objOperation = new Operation();

            redirectUrl = retUrl;
            string http = Request.IsHttps ? "https://" : "http://";
            string host = http + Request.Host;
            string urlSuccess = host + "/api/ClientRecharge/SSLCommerzSuccess";
            string urlFail = host + "/api/ClientRecharge/SSLCommerzFail";
            string urlCancel = host + "/api/ClientRecharge/SSLCommerzCancel";
            string urlIPN = "";
            NameValueCollection postData = new NameValueCollection();

            SSLCommerzInitResponse objResponse = new SSLCommerzInitResponse
            {
                status = "FAILED",
                failedreason = "Unable generating token"
            };

            try
            {
               AnFPaymentMethodCredential? objPaymentMethod = servicePaymentCredential.GetByPaymentMethodId(paymentMethodId);

                if (objPaymentMethod == null)
                {
                    objResponse.status = "FAILED";
                    objResponse.failedreason = "Invalid Gateway";
                    return objResponse;
                }

                //CmnCompany objFundSource = serviceCompany.GetById(fundSourceId);

                string logId = KeyGeneration.GenerateTimestamp();

                //New: 17022025
                //fundSourceId,
               // objOperation = service.InitiateSelfRecharge(logId, paymentMethodId,  amount, remarks, createdBy);

                if (objOperation == null)
                {
                    objResponse.status = "FAILED";
                    objResponse.failedreason = "Unable to initiate recharge";
                    objResponse.desc = new List<Desc>();
                    return objResponse;
                }

                if (objOperation.Success == false)
                {
                    objResponse.status = "FAILED";
                    objResponse.failedreason = objOperation.Message;
                    objResponse.desc = new List<Desc>();
                    return objResponse;
                }

                if (objOperation.Success)
                {
                    SSLCommerz sslcz = new SSLCommerz(objPaymentMethod, false);
                    postData.Add("total_amount", amount.ToString());
                    postData.Add("tran_id", logId);
                    postData.Add("success_url", urlSuccess);
                    postData.Add("fail_url", urlFail);
                    postData.Add("cancel_url", urlCancel);
                    postData.Add("ipn_url", urlIPN);
                    //postData.Add("cus_name", objFundSource.Name);
                    //postData.Add("cus_email", "info@technofairbd.com");
                    //postData.Add("cus_add1", objFundSource.Address);
                    //postData.Add("cus_add2", objFundSource.Address);
                    //postData.Add("cus_city", objFundSource.Address);
                    //postData.Add("cus_postcode", objFundSource.Address);
                    //postData.Add("cus_country", "Bangladesh");
                    //postData.Add("cus_phone", objFundSource.ContactNo);
                    postData.Add("shipping_method", "NO");
                    postData.Add("num_of_item", "1");
                    postData.Add("product_name", "Technofair SMS");
                    postData.Add("product_profile", "general");
                    postData.Add("product_category", "SMS");
                    postData.Add("disbursements_acct", "");
                    objResponse = sslcz.InitiateTransaction(postData);
                    return objResponse;
                }
            }
            catch (Exception ex)
            {
                objResponse.status = "FAILED";
                objResponse.failedreason = "Unable to initiate recharge";
                objResponse.desc = new List<Desc>();
            }

            return objResponse;
        }



        //Operation Type Success
        [HttpPost("SSLCommerzSuccess")]
        public async Task<IActionResult> SSLCommerzSuccess([FromForm] SSLCommerzResponse response)
        {

            //New: 05.01.2024
            Operation objOperation = new Operation { Success = false, Message = "Unable to finalize recharge" };

            string message = "";
            bool isValid = false;
            int subscriberId = 0;
            Int16 status = 2;//2=failed
            string[] arr1 = redirectUrl.Split('#');

            if (response == null)
            {
                objOperation.Success = false;
                objOperation.Message = "Something went wrong, please try again later";
                redirectUrl += "?status=" + status.ToString() + " & trxID=" + "" + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;
                return Redirect(redirectUrl);
            }

            if (response.status != "VALID")
            {
                objOperation.Success = false;
                objOperation.Message = "Something went wrong, please try again later";
                redirectUrl += "?status=" + status.ToString() + " & trxID=" + response.tran_id + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;
                return Redirect(redirectUrl);
            }

            AnFPaymentMethodCredential? objPaymentMethod = servicePaymentCredential.GetByPaymentMethodId((Int16)Technofair.Utiity.Enums.Subscription.AnFPaymentMethodEnum.SSL);

            if (objPaymentMethod == null)
            {
                objOperation.Success = false;
                objOperation.Message = "Your payment has been successfully processed, but unable to recharge, our team is investigating the issue";
                redirectUrl += "?status=" + status.ToString() + " & trxID=" + response.tran_id + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;//1=success
                return Redirect(redirectUrl);
            }

            //AnFClientRechargeRequestObject objReqObject = new AnFClientRechargeRequestObject();


            message = response.status + ":successful transaction.";
            try
            {
                //New: 19.12.2024
                //objReqObject = await serviceRequestObject.GetClientRechargeRequestObjectByTrxId(response.tran_id);

                //if (objReqObject != null)
                //{
                //    string currency = "BDT";
                //    SSLCommerz sslcz = new SSLCommerz(objPaymentMethod, true);
                //    isValid = sslcz.ValidateTransaction(response.tran_id, objReqObject.Amount, currency, response.val_id);
                //}

                //if (isValid)
                //{

                //    objOperation = service.FinalizeSelfRecharge(response.tran_id, response.bank_tran_id);

                 

                //    redirectUrl += "?status=" + status.ToString() + " & trxID=" + response.tran_id + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;//1=success
                //}
                //else
                //{

                //    objOperation.Success = false;
                //    objOperation.Message = "Your payment has been successfully processed, but unable to recharge, our team is investigating the issue";
                //    redirectUrl += "?status=" + status.ToString() + " & trxID=" + response.tran_id + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;//1=success
                //}

            }
            catch (Exception)
            {
                objOperation.Success = false;
                objOperation.Message = "Your payment has been successfully processed, but unable to recharge, our team is investigating the issue";
                redirectUrl += "?status=" + status.ToString() + " & trxID=" + response.tran_id + "&id=" + subscriberId + "&urlNam=" + arr1[1] + "&message=" + objOperation.Message;//1=success

            }

            //#endregion
            return Redirect(redirectUrl);         


        }

        [HttpPost("SSLCommerzFail")]
        public async Task<IActionResult> SSLCommerzFail([FromForm] SSLCommerzResponse response)
        {
            //New
            string message = "";
            Operation objOperation = new Operation();
            objOperation.Success = false;
            objOperation.Message = "Payment failed, Please try again later";

            string[] arr = redirectUrl.Split('#');

            if (response == null)
            {
                redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            }

            try
            {
                message = response.status + ":fail transaction.";

                //var objReqObject = await serviceRequestObject.GetClientRechargeRequestObjectByTrxId(response.tran_id);

                //objReqObject.AnFTransactionStatusId = (Int16)Technofair.Utiity.Enums.Subscription.AnFTransactionStatusEnum.Failed;
                //serviceRequestObject.Update(objReqObject);

                //var objClientRecharge = await service.GetClientRechargeByTrxId(response.tran_id);
                //objClientRecharge.AnFTransactionStatusId = (Int16)Technofair.Utiity.Enums.Subscription.AnFTransactionStatusEnum.Failed;
                //service.Update(objClientRecharge);

            }
            catch (Exception ex)
            {
                redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            }

            redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            return Redirect(redirectUrl);

           
        }

        [HttpPost("SSLCommerzCancel")]
        public async Task<IActionResult> SSLCommerzCancel([FromForm] SSLCommerzResponse response)
        {
            
            string message = "";
            Operation objOperation = new Operation();
            objOperation.Success = false;
            objOperation.Message = "You have cancelled, Please try again later";

            string[] arr = redirectUrl.Split('#');

            if (response == null)
            {
                redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            }

            try
            {
                if (response.status == "CANCELLED")
                {
                    message = response.status + ":cancel transaction.";

                    //var objReqObject = await serviceRequestObject.GetClientRechargeRequestObjectByTrxId(response.tran_id);

                   // objReqObject.AnFTransactionStatusId = (Int16)Technofair.Utiity.Enums.Subscription.AnFTransactionStatusEnum.Cancelled;
                    //serviceRequestObject.Update(objReqObject);

                    //var objClientRecharge = await service.GetClientRechargeByTrxId(response.tran_id);
                    //objClientRecharge.AnFTransactionStatusId = (Int16)Technofair.Utiity.Enums.Subscription.AnFTransactionStatusEnum.Cancelled;
                    //service.Update(objClientRecharge);
                }
            }
            catch (Exception ex)
            {
                redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            }

            redirectUrl += "?status=3&trxID=" + response.tran_id + "&urlNam=" + arr[1] + "&message=" + objOperation.Message;//1=success
            return Redirect(redirectUrl);
        }



        private async Task<string> GetSplitList(Int64 deviceNumberId, int companyId, int amount)
        {           
            //New
            string json = string.Empty;
            return json;
        }
        #endregion

        //public class RequestObject
        //{
        //    private ScpClientRecharge? _obj = null;
        //    public ScpClientRecharge obj
        //    {
        //        get
        //        {
        //            if (_obj == null)
        //            {
        //                _obj = new ScpClientRecharge();
        //            }
        //            return _obj;
        //        }
        //        set
        //        {
        //            _obj = value;
        //        }
        //    }
        //    public bool? isMso { get; set; }
        //}
    }

}
