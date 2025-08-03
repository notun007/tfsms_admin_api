
using TFSMS.Admin.Model.ViewModel.Accounts;
using TFSMS.Admin.Model.ViewModel.Subscription;
using Technofair.Lib.Model;

using TFSMS.Admin.Model.TFAdmin;

//using TFSMS.Admin.Model.Common;
using System.Data;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    #region Interface
    public interface ITFACompanyCollectionService
    {
        Operation Save(TFACompanyCollection obj);
        Operation Update(TFACompanyCollection obj);
        Operation Delete(TFACompanyCollection obj);
        TFACompanyCollection GetById(long id);
        TFAClientPayment GetCompanyPaymentByPaymentId(long paymentId, string domain);
        TFACompanyCollection GetByPaymentId(long paymentId);
        TFACompanyCollection GetByClientAndPaymentId(int clientId, long paymentId);
        List<AnFCompanyCollectionViewModel> GetCollectionByDateAndClientId(DateTime dateFrom, DateTime dateTo, int clientId, string domain, int? paymentMethodId, string filePath);
        List<AnFClientPaymentViewModel> GetClientStatusByCompanyYearAndMonthId(int yearId, Int16? monthId, string domain, Int16? status, string filePath);
        List<AnFClientPaymentDetailViewModel> GetMonthlyPaidAndPayableByYearAndClienId(long paymentId, int yearId, int clientId, string domain, string filePath);
    }
    #endregion


    #region Member
    public class TFACompanyCollectionService : ITFACompanyCollectionService
    {
        private ITFACompanyCollectionRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFACompanyCollectionService(ITFACompanyCollectionRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(TFACompanyCollection obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(TFACompanyCollection obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }


        public Operation Delete(TFACompanyCollection obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                _UnitOfWork.Commit();
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public TFACompanyCollection GetById(long id)
        {
            try
            {
                return repository.GetById(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFACompanyCollection GetByPaymentId(long paymentId)
        {
            try
            {
                return repository.GetMany(m => m.TFAClientPaymentId == paymentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFACompanyCollection GetByClientAndPaymentId(int clientId, long paymentId)
        {
            try
            {
                return repository.GetMany(w => w.TFACompanyCustomerId == clientId && w.TFAClientPaymentId == paymentId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFACompanyCollection GetByTransactionID(string trxID)
        {
            try
            {
                return repository.GetMany(m => m.TrxID == trxID.Trim()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnFClientPaymentDetailViewModel> GetMonthlyPaidAndPayableByYearAndClienId(long paymentId, int yearId, int clientId, string domain, string filePath)
        {
            try
            {
                List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
                List<TFSMS.Admin.Model.Common.CmnMonth> months = Technofair.Lib.Utilities.Helper.GetInstituteMonthByYearId(yearId, domain);
                List<ScpSubscriberViewModel> listSubscriber = Technofair.Lib.Utilities.Helper.GetSubscriberByDomainAndYear(domain, yearId);
                AnFClientPackageViewModel objPackage = GetPackageInfoByClientId(clientId, filePath);
                if (objPackage != null && objPackage.Id > 0)
                {
                    foreach (TFSMS.Admin.Model.Common.CmnMonth m in months)
                    {
                        AnFClientPaymentDetailViewModel objF = new AnFClientPaymentDetailViewModel();
                        objF.CmnMonthId = m.Id;
                        objF.Month = m.Name;

                        objF.ClientPackage = objPackage;
                        TFAClientPaymentDetail objExist = GetPaymentDetailByYearAndMonthId(yearId, m.Id, domain);
                        if (objExist == null)
                        {
                            if (listSubscriber != null && listSubscriber.Count > 0)
                            {
                                objF.Quantity = listSubscriber.Count;
                            }

                            if (objPackage.IsFixed == true)
                            {
                                //new
                                objF.Amount = (decimal)(objPackage.Amount - objPackage.Discount);
                                //old
                                //objF.Amount = (decimal)objPackage.Amount;
                            }
                            else
                            {
                                //new
                                objF.Rate = objPackage.Amount;
                                //old
                                //objF.Rate = objPackage.Rate;
                                objF.Discount = objPackage.Discount;
                                objF.Amount = (decimal)(objF.Quantity * objF.Rate);
                            }
                            list.Add(objF);
                        }
                        else if (objExist != null && objExist.Id > 0 && objExist.TFAClientPaymentId == paymentId)
                        {
                            objF.Id = objExist.Id;
                            objF.AnFClientPaymentId = objExist.TFAClientPaymentId;
                            //objF.Quantity = objExist.Quantity;
                            objF.Rate = objExist.Rate;
                            objF.Discount = objExist.Discount;
                            objF.Amount = objExist.Amount;
                            objF.IsPaid = true;
                            list.Add(objF);
                        }
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TFAClientPaymentDetail GetPaymentDetailByYearAndMonthId(int calYearId, short monthId, string domain)
        {
            try
            {
                TFAClientPaymentDetail obj = null;
                DataTable dt = repository.GetPaymentDetailByYearAndMonthId(calYearId, monthId, domain);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj = new TFAClientPaymentDetail();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = (TFAClientPaymentDetail)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(TFAClientPaymentDetail));
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFAClientPayment GetCompanyPaymentByPaymentId(long paymentId, string domain)
        {
            try
            {
                TFAClientPayment obj = null;
                DataTable dt = repository.GetCompanyPaymentByPaymentId(paymentId, domain);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj = new TFAClientPayment();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = (TFAClientPayment)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(TFAClientPayment));
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnFCompanyCollectionViewModel> GetCollectionByDateAndClientId(DateTime dateFrom, DateTime dateTo, int clientId, string domain, int? paymentMethodId, string filePath)
        {
            try
            {
                List<AnFCompanyCollectionViewModel> list = null;
                DataTable dt = repository.GetCollectionByDateAndClientId(dateFrom, dateTo, clientId, paymentMethodId, filePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<AnFCompanyCollectionViewModel>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((AnFCompanyCollectionViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFCompanyCollectionViewModel)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnFClientPaymentViewModel> GetClientStatusByCompanyYearAndMonthId(int yearId, Int16? monthId, string domain, Int16? status, string filePath)
        {
            try
            {
                List<AnFClientPaymentViewModel> list = null;
                List<TFACompanyCustomer> clients = new List<TFACompanyCustomer>();
                if (domain != null && domain != "")
                {
                    //CmnCompanyCustomer objC = Technofair.Lib.Utilities.Helper.GetClientByDomain(domain);
                    //if (objC != null && objC.Id > 0)
                    //{
                    //    clients.Add(objC);
                    //}
                }
                else
                {
                    //clients = Technofair.Lib.Utilities.Helper.GetActivePackageClient(districtId, upazilaId);
                }

                if (clients != null && clients.Count > 0)
                {
                    list = new List<AnFClientPaymentViewModel>();
                    foreach (TFACompanyCustomer objClient in clients)
                    {
                        AnFClientPackageViewModel objPackage = GetPackageInfoByClientId(objClient.Id, filePath);
                        if (objPackage != null && objPackage.Id > 0)
                        {
                            //CmnFinancialYear objYear = Technofair.Lib.Utilities.Helper.GetInstituteYearByCompanyYearId(yearId, objClient.Web);
                            //if (objYear != null)
                            //{
                            //    List<CmnMonth> months = Technofair.Lib.Utilities.Helper.GetInstituteMonthByYearId(objYear.Id, objClient.Web);
                            //    if (monthId != null && monthId > 0)
                            //    {
                            //        months = months.Where(w => w.Id == monthId).ToList();
                            //    }

                            //    if (months != null && months.Count > 0)
                            //    {
                            //        for (int i = 0; i < months.Count; i++)
                            //        {
                            //            AnFClientPaymentViewModel objView = GetCompanyPaymentByYearAndMonthId(objYear.Id, months[i].Id, objClient.Web, objPackage.Id, status);
                            //            if (objView != null)
                            //            {
                            //                objView.ClientName = objClient.Name;
                            //                objView.Address = objClient.Address;
                            //                objView.ContactNo = objClient.ContactNo;
                            //                objView.Web = objClient.Web;
                            //                if (objView.IsCollected)
                            //                {
                            //                    objView.PaymentStatus = "Collected";
                            //                }
                            //                else if (objView.AnFPaymentMethodId > 0)
                            //                {
                            //                    objView.PaymentStatus = "Paid";
                            //                }
                            //                else
                            //                {
                            //                    objView.PaymentStatus = "Unpaid";
                            //                }
                            //            }
                            //            else
                            //            {
                            //                objView = new AnFClientPaymentViewModel();
                            //                objView.CmnFinancialYearId = objYear.Id;
                            //                objView.ClientName = objClient.Name;
                            //                objView.Address = objClient.Address;
                            //                objView.ContactNo = objClient.ContactNo;
                            //                objView.Web = objClient.Web;
                            //                objView.PaymentStatus = "Not Found";
                            //                objView.AnFClientPackageId = objPackage.Id;
                            //            }

                            //            AnFClientPaymentViewModel objExist = list.Where(w => w.Id == objView.Id).FirstOrDefault();
                            //            if (objExist == null)
                            //            {
                            //                list.Add(objView);
                            //            }
                            //        }
                            //    }
                            //}
                        }
                    }
                }
                if (status == null || status == 1)//Not Generate = true;
                {
                    list = list.Where(w => w.PaymentStatus == "Not Found").Distinct().ToList();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AnFClientPaymentViewModel GetCompanyPaymentByYearAndMonthId(int yearId, Int16 monthId, string domain, int packageId, Int16? status)
        {
            try
            {
                AnFClientPaymentViewModel obj = null;
                DataTable dt = repository.GetCompanyPaymentByYearAndMonthId(yearId, monthId, domain, packageId, status);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj = new AnFClientPaymentViewModel();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = (AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel));
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFACompanyCollection GetCollectionByPaymentAndClientId(long paymentId, int clientId, string filePath)
        {
            try
            {
                TFACompanyCollection obj = null;
                DataTable dt = repository.GetCollectionByPaymentAndClientId(paymentId, clientId, filePath);
                if (dt != null && dt.Rows.Count > 0)
                {
                    obj = new TFACompanyCollection();
                    foreach (DataRow row in dt.Rows)
                    {
                        obj = (TFACompanyCollection)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(TFACompanyCollection));
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private AnFClientPackageViewModel GetPackageInfoByClientId(int clientId, string filePath)
        {
            List<AnFClientPackageViewModel> packages = GetPackageByClientId(clientId, filePath);
            AnFClientPackageViewModel objPackage = new AnFClientPackageViewModel();
            if (packages != null && packages.Count > 0)
            {
                objPackage = packages.Where(w => w.IsActive).OrderByDescending(o => o.Date).FirstOrDefault();
            }
            return objPackage;
        }
        private List<AnFClientPackageViewModel> GetPackageByClientId(int clientId, string filePath)
        {
            List<AnFClientPackageViewModel> list = new List<AnFClientPackageViewModel>();
            DataTable dt = repository.GetPackageByClientId(clientId, filePath);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    list.Add(((AnFClientPackageViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPackageViewModel))));
                }
            }
            return list;
        }

    }

    #endregion
}
