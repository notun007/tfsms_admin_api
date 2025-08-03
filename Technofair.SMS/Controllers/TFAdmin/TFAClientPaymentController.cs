using Microsoft.AspNetCore.Mvc;
using Technofair.Lib.Model; 
using TFSMS.Admin.Model.ViewModel.Accounts.Reports;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using System.Data;
using Technofair.Lib.Utilities;

using Microsoft.AspNetCore.Authorization;
using TFSMS.Admin.Service.TFAdmin;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;



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
        AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();
        public TFAClientPaymentController(IWebHostEnvironment hostingEnvironment)
        {
            repository = new TFAClientPaymentRepository(dbfactory);
            service = new TFAClientPaymentService(repository, new AdminUnitOfWork(dbfactory));
            serviceDetail = new TFAClientPaymentDetailService(new TFAClientPaymentDetailRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceComCustomer = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));
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
    }

}
