using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;
using Technofair.Data.Infrastructure;
using Technofair.Data.Repository.Accounts;
using Technofair.Data.Repository.Common;
using Technofair.Lib.Utilities;
using TFSMS.Admin.Model.Accounts;
using TFSMS.Admin.Model.Common;
using TFSMS.Admin.Model.HRM;
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.Common;
using Technofair.Service.Accounts;
using Technofair.Service.Common;
using TFSMS.Admin.Web.Models;
using Technofair.Service.TFAdmin;
using Technofair.Data.Repository.TFAdmin;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using Microsoft.AspNetCore.Authorization;

namespace TFSMS.Admin.Controllers.TFAdmin
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class TFACompanyCollectionController : ControllerBase
    {
        private ITFACompanyCollectionService service;
        private ITFAClientPaymentService servicePayment;
        private ITFACompanyCollectionRepository repository;
        //private ICmnPaymentGatewayUserService serviceGatewayUser;
        private ITFACompanyCustomerService serviceClient;

     
        AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();
        DatabaseFactory dbfac = new DatabaseFactory();
        public TFACompanyCollectionController()
        {
            repository = new TFACompanyCollectionRepository(dbfactory);
            service = new TFACompanyCollectionService(repository, new AdminUnitOfWork(dbfactory));
            servicePayment = new TFAClientPaymentService(new TFAClientPaymentRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            //serviceGatewayUser = new CmnPaymentGatewayUserService(new CmnPaymentGatewayUserRepository(dbfac), new UnitOfWork(dbfac));
            serviceClient = new TFACompanyCustomerService(new TFACompanyCustomerRepository(dbfactory), new AdminUnitOfWork(dbfactory));

            //servicePaymentMethod = new AnFPaymentMethodService(new AnFPaymentMethodRepository(dbfac), new UnitOfWork(dbfac));
            //serviceMethodDetail = new AnFPaymentMethodDetailService(new AnFPaymentMethodDetailRepository(dbfac), new UnitOfWork(dbfac));
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("Save")]
        public async Task<Operation> Save([FromBody] List<AnFClientPaymentViewModel> list)
        {
            Operation objOperation = new Operation { Success = false };
            if (list != null && list.Count > 0)
            {
                //foreach (AnFClientPaymentViewModel objView in list)
                //{
                //    try
                //    {
                //        //New: Asad --> Uncomment after Finlize
                //        TFACompanyCustomer objClient =new TFACompanyCustomer();
                //        //Old: Asad
                //        //TFACompanyCustomer objClient = GetClientByDomain(objView.Web);
                //        TFAClientPayment objPayment = service.GetCompanyPaymentByPaymentId(objView.Id, objClient.Web);
                //        if (objClient != null && objPayment != null && objPayment.Id > 0 && !objPayment.IsCancelled && objView.AnFPaymentMethodId != null && objView.AnFPaymentMethodId > 0)
                //        {
                //            TFACompanyCollection objExist = service.GetByClientAndPaymentId(objClient.Id, objView.Id);
                //            if (objExist == null)
                //            {
                //                TFACompanyCollection obj = new TFACompanyCollection();
                //                obj.TFAClientPaymentId = objView.Id;
                //                obj.TFAFinancialYearId = objView.CmnFinancialYearId;
                //                obj.TFACompanyCustomerId = objClient.Id;
                //                obj.Date = DateTime.Now;
                //                obj.TFAPaymentMethodId = (Int16)objView.AnFPaymentMethodId;
                //                obj.WalletNo = objView.WalletNo;
                //                obj.TrxID = objView.TrxID;
                //                obj.TotalAmount = objView.TotalAmount;
                //                //obj.AnFVoucherId = objView.AnFVoucherId;
                //                obj.Remarks = objView.Remarks;
                //                obj.CreatedBy = objView.CreatedBy;
                //                obj.CreatedDate = DateTime.Now;
                //                objOperation = service.Save(obj);
                //            }
                //            else if (objExist != null)
                //            {
                //                objExist.TFAPaymentMethodId = (Int16)objView.AnFPaymentMethodId;
                //                objExist.WalletNo = objView.WalletNo;
                //                objExist.TrxID = objView.TrxID;
                //                objExist.TotalAmount = objView.TotalAmount;
                //                objExist.ModifiedBy = objView.CreatedBy;
                //                objExist.ModifiedDate = DateTime.Now;
                //                objOperation = service.Update(objExist);
                //            }
                //            if (objOperation.Success)
                //            {
                //                if (objPayment.PaidDate == null)
                //                {
                //                    objPayment.PaidDate = DateTime.Now;
                //                }
                //                objPayment.IsCollected = true;
                //                objPayment.TFAPaymentMethodId = objView.AnFPaymentMethodId;
                //                objPayment.WalletNo = objView.WalletNo;
                //                objPayment.TrxID = objView.TrxID;
                //                //servicePayment.UpdateCompanyPayment(objPayment, objClient.Web);
                //            }
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        continue;
                //    }
                //}
            }
            return objOperation;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetCollectionByDateAndClientId")]
        public async Task<List<AnFCompanyCollectionViewModel>> GetCollectionByDateAndClientId(DateTime dateFrom, DateTime dateTo, string domain, int? paymentMethodId)
        {
            List<AnFCompanyCollectionViewModel> list = new List<AnFCompanyCollectionViewModel>();
            if (domain != null && domain != "")
            {
                TFACompanyCustomer objClient = serviceClient.GetClientByDomain(domain); //Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
                list = service.GetCollectionByDateAndClientId(dateFrom, dateTo, objClient.Id, domain, paymentMethodId, "");
            }
            else
            {
                List<TFACompanyCustomer> clients = serviceClient.GetAll(); //Technofair.Lib.Utilities.Helper.GetActivePackageClient();
                if (clients != null && clients.Count > 0)
                {
                    for (int i = 0; i < clients.Count; i++)
                    {
                        List<AnFCompanyCollectionViewModel> temp = service.GetCollectionByDateAndClientId(dateFrom, dateTo, clients[i].Id, clients[i].Web, paymentMethodId, "");
                        if (temp != null && temp.Count > 0)
                        {
                            list.AddRange(temp);
                        }
                    }
                    list = list.OrderByDescending(o => o.Date).ToList();
                }
            }
            return list;
        }

        //[Authorize(Policy = "Authenticated")]
        //[HttpPost("GetMonthlyPayableByClientAndYearId")]
        //public async Task<List<AnFClientPaymentDetailViewModel>> GetMonthlyPayableByClientAndYearId(int calYearId, string domain)
        //{
        //    List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
        //    CmnCompanyCustomer objClient = new CmnCompanyCustomer();
        //    objClient = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
        //    if (objClient != null)
        //    {
        //        list = servicePayment.GetMonthlyPayableByClientAndYearId(calYearId, objClient.Id, domain);
        //    }
        //    return list;
        //}

        [HttpPost("GetMonthlyPaidAndPayableByYearAndMonthId")]
        public async Task<List<AnFClientPaymentDetailViewModel>> GetMonthlyPaidAndPayableByYearAndMonthId(long paymentId, int yearId, string domain)
        {
            List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
            TFACompanyCustomer objClient = new TFACompanyCustomer();
            //Asad Commented--> Uncomment after Finalize
            //objClient = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
            if (objClient != null)
            {
                list = service.GetMonthlyPaidAndPayableByYearAndClienId(paymentId, yearId, objClient.Id, domain, "");
            }
            return list;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetClientStatusByCompanyYearAndMonthId")]
        public async Task<List<AnFClientPaymentViewModel>> GetClientStatusByCompanyYearAndMonthId(int yearId, Int16? monthId, string domain, Int16? status)
        {
            List<AnFClientPaymentViewModel> list = new List<AnFClientPaymentViewModel>();

            //string comDomain = "www.abc.bd";
            //string comDomain = GetHost();
            list = service.GetClientStatusByCompanyYearAndMonthId(yearId, monthId, domain, status, "");
            return list;
        }

        //Make change to TFA afer finalize
        //private CmnCompanyCustomer GetClientByDomain(string domain)
        //{
        //    CmnCompanyCustomer obj = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
        //    return obj;
        //}

        //[Authorize(Policy = "Authenticated")]
        //[HttpPost("GetCompanyYear")]
        //public async Task<List<HrmCalendarYear>> GetCompanyYear(string domain)
        //{
        //    //if (domain == null || domain == "")
        //    //{
        //    //    //domain = "www.Technofair.com";
        //    //    domain = GetHost();                
        //    //}
        //    List<HrmCalendarYear> list = Technofair.Lib.Utilities.Helper.GetCompanyYear();
        //    return list;
        //}

        [Authorize(Policy = "Authenticated")]
        [HttpPost("GetInstituteMonth")]
        public async Task<List<CmnMonth>> GetInstituteMonth(int yearId, string domain)
        {
            if (domain == null || domain == "")
            {
                //domain = "www.abc.bd";
                domain = GetHost();
            }
            List<CmnMonth> list = Technofair.Lib.Utilities.Helper.GetInstituteMonthByYearId(yearId, domain);
            return list;
        }

        private string GetHost()
        {
            //string hostName = "www.abc.com";
            //string hostName = HttpContext.Request.Url.Host;
            //if (!hostName.StartsWith("www."))
            //{
            //    hostName = "www." + hostName;
            //}

            string hostName = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            hostName += ":" + Request.HttpContext.Connection.RemotePort.ToString();
            return hostName;
        }

        [Authorize(Policy = "Authenticated")]
        [HttpPost("ClientValidation")]
        public async Task<PaymentModel> ClientValidation(string userID, string clientID)
        {
            PaymentModel obj = new PaymentModel();
            try
            {
                if ((userID != null && userID != "") && (clientID != null && clientID != ""))
                {
                    //CmnPaymentGatewayUser objGatewayUser = new CmnPaymentGatewayUser();
                    //objGatewayUser = serviceGatewayUser.GetByUserID(userID.Trim());
                    //if (objGatewayUser != null && objGatewayUser.Id > 0)
                    //{
                    //    TFACompanyCustomer objClient = serviceClient.GetByCode(clientID.Trim());
                    //    if (objClient != null && objClient.Id > 0)
                    //    {
                    //        obj.ID = objClient.Code.ToString();
                    //        obj.Name = objClient.Name;
                    //        List<AnFClientPaymentViewModel> list = GetClientPayable(objClient.Web.Trim());
                    //        if (list != null && list.Count > 0)
                    //        {
                    //            obj.Amount = (int)(list.Sum(s => s.TotalAmount));
                    //        }
                    //        obj.Success = "Succeed";
                    //        obj.Message = "Authorized";
                    //    }
                    //    else
                    //    {
                    //        obj.Message = "Invalid ID";
                    //    }
                    //}
                    //else
                    //{
                    //    obj.Message = "Unauthorized";
                    //}
                }
                else
                {
                    obj.Message = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                obj.Error = "Failed";
                obj.Message = "Internal Server Error";
                //throw ex;
                //obj.Message = ex.Message;
            }
            return obj;
        }


        [Authorize(Policy = "Authenticated")]
        [HttpPost("PaymentConfirmation")]
        public async Task<PaymentModel> PaymentConfirmation(string userID, string authenCode, string clientID, string trxID, string walletNo)
        {
            PaymentModel obj = new PaymentModel();
            try
            {
                if ((userID != null && userID != "") && (authenCode != null && authenCode != "") && (clientID != null && clientID != ""))
                {
                    //CmnPaymentGatewayUser objGatewayUser = new CmnPaymentGatewayUser();
                    //objGatewayUser = serviceGatewayUser.GetByUserID(userID.Trim());
                    //if (objGatewayUser != null && objGatewayUser.Id > 0)
                    //{
                    //    if (objGatewayUser.AuthorizationCode == authenCode)
                    //    {
                    //        if ((trxID != null && trxID != "") && (walletNo != null && walletNo != ""))
                    //        {
                    //            TFACompanyCustomer objClient = serviceClient.GetByCode(clientID.Trim());
                    //            if (objClient != null && objClient.Id > 0)
                    //            {
                    //                Operation objOperation = new Operation { Success = false };
                    //                objOperation = SaveMFS(objClient.Web, trxID.Trim(), walletNo.Trim(), objGatewayUser.AnFPaymentMethodId);
                    //                if (objOperation.Success)
                    //                {
                    //                    obj.ID = objClient.Code.ToString();
                    //                    obj.Name = objClient.Name;
                    //                    obj.Amount = Convert.ToInt32(objOperation.Message);
                    //                    obj.TrxID = trxID.Trim();
                    //                    obj.MobileNo = walletNo.Trim();
                    //                    obj.Success = "Succeed";
                    //                    obj.Message = "Successfully done";
                    //                }
                    //                else
                    //                {
                    //                    obj.Error = "Failed";
                    //                    obj.Message = objOperation.Message;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                obj.Message = "Invalid ID";

                    //            }
                    //        }
                    //        else
                    //        {
                    //            obj.Message = "Invalid TrxID Or Mobile No.";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        obj.Message = "Unauthorized";
                    //    }
                    //}
                    //else
                    //{
                    //    obj.Message = "Unauthorized";
                    //}
                }
                else
                {
                    obj.Message = "Unauthorized";
                }
            }
            catch (Exception ex)
            {
                obj.Error = "Failed";
                obj.Message = "Internal Server Error";
                //throw ex;
                //obj.Message = ex.Message;
            }
            return obj;
        }
        private List<AnFClientPaymentViewModel> GetClientPayable(string domain)
        {
            List<AnFClientPaymentViewModel> list = servicePayment.GetPayableByClient(domain);
            return list;
        }

        private Operation SaveMFS(string domain, string trxID, string walletNo, Int16 paymentMethodId)
        {
            int totalPayable = 0;
            Operation objOperation = new Operation { Success = false };
            List<AnFClientPaymentViewModel> list = GetClientPayable(domain.Trim());
            if (list.Count > 0 && list != null)
            {
                totalPayable = (int)(list.Sum(s => s.TotalAmount));
                //obj.TotalAmount = totalPayable;
                objOperation = GetTransactionID(domain, paymentMethodId, trxID, walletNo, totalPayable);
                if (objOperation.Success)
                {
                    foreach (AnFClientPaymentViewModel objView in list)
                    {
                        try
                        {
                            TFACompanyCustomer objClient = new TFACompanyCustomer();
                            //Old: Uncomment after Finalize
                            //CmnCompanyCustomer objClient = GetClientByDomain(domain.Trim());

                            //TFAClientPayment objPayment = service.GetCompanyPaymentByPaymentId(objView.Id, objClient.Web);
                            //if (objClient != null && objPayment != null && objPayment.Id > 0 && !objPayment.IsCancelled)
                            {
                                //AnFCompanyCollection objExist = service.GetByClientAndPaymentId(objClient.Id, objView.Id);
                                //if (objExist == null)
                                //{
                                //    AnFCompanyCollection obj = new AnFCompanyCollection();
                                //    obj.AnFClientPaymentId = objView.Id;
                                //    obj.CmnFinancialYearId = calendarYearId;
                                //    obj.CmnCompanyCustomerId = objClient.Id;
                                //    obj.Date = DateTime.Now;
                                //    obj.AnFPaymentMethodId = (Int16)objView.AnFPaymentMethodId;
                                //    obj.WalletNo = objView.WalletNo;
                                //    obj.TrxID = objView.TrxID;
                                //    obj.TotalAmount = objView.TotalAmount;
                                //    //obj.AnFVoucherId = objView.AnFVoucherId;
                                //    obj.Remarks = objView.Remarks;
                                //    obj.CreatedBy = userId;
                                //    obj.CreatedDate = DateTime.Now;
                                //    objOperation = service.Save(obj);
                                //}
                                //else if (objExist != null)
                                //{
                                //    objExist.AnFPaymentMethodId = (Int16)objView.AnFPaymentMethodId;
                                //    objExist.WalletNo = objView.WalletNo;
                                //    objExist.TrxID = objView.TrxID;
                                //    objExist.TotalAmount = objView.TotalAmount;
                                //    objExist.ModifiedBy = userId;
                                //    objExist.ModifiedDate = DateTime.Now;
                                //    objOperation = service.Update(objExist);
                                //}
                                //if (objOperation.Success)
                                //{
                                //if (objPayment.PaidDate == null)
                                //{
                                //    objPayment.PaidDate = DateTime.Now;
                                //}
                                //objPayment.TFAPaymentMethodId = paymentMethodId;
                                //objPayment.WalletNo = walletNo;
                                //objPayment.TrxID = trxID;
                                //objOperation = servicePayment.UpdateCompanyPayment(objPayment, objClient.Web);
                                objOperation.Message = totalPayable.ToString();
                                //}
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
            }
            return objOperation;
        }

        private Operation GetTransactionID(string domain, Int16 paymentMethodId, string trxID, string walletNo, int amount)
        {
            Operation objOperation = new Operation { Success = false };
           
            if ((trxID != null && trxID.Trim() != "") && (walletNo != null && walletNo.Trim() != ""))
            {
                AnFClientPaymentViewModel objExist = servicePayment.GetPaymentByClientAndTransactionID(domain, trxID);
                if (objExist == null)
                {
                    //AnFPaymentMethod objMethod = servicePaymentMethod.GetById(paymentMethodId);
                    //if (objMethod != null && objMethod.Name.Trim().ToUpper() == "ROCKET")
                    //{
                    //    //objOperation = Rocket(trxID, amount, objAuthentication);
                    //    objOperation.OperationId = 0;
                    //    objOperation.Message = trxID.Trim();
                    //    objOperation.Success = true;
                    //}
                    //else if (objMethod != null && objMethod.Name.Trim().ToUpper() == "NAGAD")
                    //{
                    //    //objOperation = Nagad(trxID, amount, objAuthentication);
                    //    objOperation.OperationId = 0;
                    //    objOperation.Message = trxID.Trim();
                    //    objOperation.Success = true;
                    //}
                }
                else
                {
                    objOperation.OperationId = 0;
                    objOperation.Message = "Transaction ID already inused";
                    objOperation.Success = false;
                }
            }
            else
            {
                objOperation.OperationId = 0;
                objOperation.Message = "Invalid";
                objOperation.Success = false;
            }
            //}
            //else
            //{
            //    objOperation.OperationId = 0;
            //    objOperation.Message = "Invalid";
            //    objOperation.Success = false;
            //}
            return objOperation;
        }

        int requestCount = 0;
        #region ROCKET
        //private Operation Rocket(string tnxID, decimal amount, AnFPaymentMethodCredential objAuthentication)
        //{
        //    requestCount = 0;
        //    Operation objOperation = new Operation();
        //    string paymentUrl = objAuthentication.PaymentUrl.Trim() + "/BillInquiryService?shortcode=" + objAuthentication.PartnerCode.Trim() + "&userid=" + objAuthentication.UserID.Trim() + "&password=" + objAuthentication.AuthorizationCode.Trim() + "&txnid=" + tnxID.Trim();
        //    //string authorizationHeader = "onezerobd:onezerobd@SureCash";
        //    string authorizationHeader = objAuthentication.UserID.Trim() + ":" + objAuthentication.AuthorizationCode.Trim();
        //    RequestResponseViewModel objResponse = CheckPaymentRocket(paymentUrl, authorizationHeader);
        //    if (objResponse != null)
        //    {
        //        if (objResponse.errCode == "00")
        //        {
        //            if (amount == Convert.ToDecimal(objResponse.billAmount))
        //            {
        //                objOperation.OperationId = 0;
        //                objOperation.Message = tnxID.Trim();
        //                objOperation.Success = true;
        //            }
        //            else
        //            {
        //                objOperation.OperationId = 0;
        //                objOperation.Message = "Input amount and paid amount must be equal";
        //                objOperation.Success = false;
        //            }
        //        }
        //        else
        //        {
        //            objOperation.OperationId = objResponse.errCode;
        //            objOperation.Success = false;
        //            if (objResponse.description == null)
        //            {
        //                objOperation.Message = GlobalConstants.ReturnResultRocket(objResponse.errCode == null ? 0 : Convert.ToInt32(objResponse.errCode));
        //            }
        //            else if (objResponse.description != null)
        //            {
        //                objOperation.Message = objResponse.description;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        objOperation.OperationId = 0;
        //        objOperation.Message = "Invalid";
        //        objOperation.Success = false;
        //    }
        //    return objOperation;
        //}
        public RequestResponseViewModel CheckPaymentRocket(string requestURL, string authorization)
        {
            RequestResponseViewModel objResponse = new RequestResponseViewModel();
            try
            {
                authorization = "dbill:!23";
                byte[] encbuff = Encoding.UTF8.GetBytes(authorization);
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(requestURL);
                //request.Accept = "application/json";
                //request.ContentType = "application/json";
                request.PreAuthenticate = true;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(encbuff));
                request.Headers.Add("responseType", "json");
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    string responseData = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    if (responseData != null && responseData != "")
                    {
                        objResponse = JsonConvert.DeserializeObject<RequestResponseViewModel>(responseData);
                    }
                }
            }
            catch (Exception e)
            {
                if (requestCount < 3)
                {
                    requestCount++;
                    CheckPaymentRocket(requestURL, authorization);
                }
                else
                {
                    objResponse.description = e.Message;
                }

                //throw e;                
            }
            return objResponse;
        }
        #endregion

        #region NAGAD
        //private Operation Nagad(string tnxID, decimal amount, AnFPaymentMethodCredential objAuthentication)
        //{
        //    requestCount = 0;
        //    Operation objOperation = new Operation();
        //    string paymentUrl = objAuthentication.PaymentUrl.Trim();
        //    //string authorizationHeader = objAuthentication.UserID.Trim() + ":" + objAuthentication.AuthorizationCode.Trim();
        //    RequestResponseViewModel objResponse = CheckPaymentNagad(paymentUrl, objAuthentication.PartnerCode, tnxID);
        //    if (objResponse != null)
        //    {
        //        if (objResponse.Message == "Success")
        //        {
        //            if (amount == Convert.ToDecimal(objResponse.paidAmount))
        //            {
        //                objOperation.OperationId = 0;
        //                objOperation.Message = tnxID.Trim();
        //                objOperation.Success = true;
        //            }
        //            else
        //            {
        //                objOperation.OperationId = 0;
        //                objOperation.Message = "Input amount and paid amount must be equal";
        //                objOperation.Success = false;
        //            }
        //        }
        //        else
        //        {
        //            objOperation.OperationId = objResponse.error_code;
        //            objOperation.Success = false;
        //            if (objResponse.description == null)
        //            {
        //                objOperation.Message = GlobalConstants.ReturnResultNagad(objResponse.error_code);
        //            }
        //            else if (objResponse.description != null)
        //            {
        //                objOperation.Message = objResponse.description;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        objOperation.OperationId = 0;
        //        objOperation.Message = "Invalid";
        //        objOperation.Success = false;
        //    }

        //    return objOperation;
        //}

        public RequestResponseViewModel CheckPaymentNagad(string requestURL, string billerId, string tnxID)
        {
            RequestResponseViewModel objResponse = new RequestResponseViewModel();
            try
            {
                //byte[] encbuff = Encoding.UTF8.GetBytes(authorizationHeader);
                //string authorization = Convert.ToBase64String(encbuff);
                string authorization = "IyNvbmUhI3plcm9AQGJkIyM6I2JkQ==";

                var client = new RestClient(requestURL.Trim());
                //client.Timeout = 10 * 1000; // 1000 milliseconds == 1 seconds
                var request = new RestRequest();
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "Basic " + authorization);
                request.AddHeader("content-type", "application/json");

                string jsonBody = JsonConvert.SerializeObject(new
                {
                    txnId = tnxID,
                    billerId = billerId
                });
                request.AddParameter("application/json", jsonBody, ParameterType.RequestBody);
                RestResponse response = client.Execute(request, Method.Post);
                string responseData = response.Content;
                if (responseData != null && responseData != "")
                {
                    objResponse = JsonConvert.DeserializeObject<RequestResponseViewModel>(responseData);
                }
            }
            catch (Exception e)
            {
                if (requestCount < 3)
                {
                    requestCount++;
                    CheckPaymentNagad(requestURL, billerId, tnxID);
                }
                else
                {
                    objResponse.description = e.Message;
                }
                //throw e;                
            }
            return objResponse;
        }


        #endregion

    }
}
