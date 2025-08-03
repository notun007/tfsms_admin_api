
using Technofair.Lib.Model;
using Technofair.Model.Accounts;
using Technofair.Model.Common;
using Technofair.Model.ViewModel.Accounts;
using Technofair.Model.ViewModel.Subscription;
using Technofair.Model.TFAdmin;
using Technofair.Model.ViewModel.TFAdmin;
using Technofair.Model.ViewModel.Accounts.Reports;
using TFSMS.Admin.Data.Repository.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Service.Common;
using TFSMS.Admin.Data.Infrastructure.TFAdmin;
using TFSMS.Admin.Data.Repository.Common;
using Technofair.Data.Infrastructure.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{  
    #region Interface
    public interface ITFAClientPaymentService
    {
        Operation Save(TFAClientPayment obj);
        Operation SaveWithTransaction(TFAClientPayment obj);
        Operation Update(TFAClientPayment obj);
        Operation UpdateWithTransaction(TFAClientPayment obj);
        Operation Delete(TFAClientPayment obj);
        TFAClientPayment GetById(long id);

        List<AnFClientPaymentDetailViewModel> GetMonthlyPayableByClientAndYearId(int yearId, int clientId, string domain);
        List<TFAClientPayment> GetPaymentByDate(DateTime dateFrom, DateTime dateTo);
        List<AnFClientPaymentDetailViewModel> GetMonthlyPaidAndPayableByYearAndClienId(long paymentId, int yearId, int clientId, string domain);
        ReportAnFClientPaymentViewModel GetPaymentByPaymentId(long paymentId, string domain);
        List<AnFPaymentMethod> GetCompanyPaymentMethod(string domain);
        List<AnFClientPaymentDetailViewModel> GetMonthlyPaidByPaymentId(long paymentId, string domain);
        List<AnFClientPaymentViewModel> GetPaymentByDateAndDomain(DateTime dateFrom, DateTime dateTo, string domain, bool? isAll);
        //List<AnFClientPaymentViewModel> GetPaymentByDateAndDomainForCompany(DateTime dateFrom, DateTime dateTo, string domain, Int16? paymentStatus);
        //Operation InsertPayment(AnFClientPayment obj, string domain);
        //Operation UpdateCompanyPayment(AnFClientPayment obj, string domain);
        List<AnFClientPaymentViewModel> GetPayableByClient(string domain);
        TFAClientPayment GetByTransactionID(string trxID);
        AnFClientPaymentViewModel GetPaymentByClientAndTransactionID(string domain, string trxID);
        List<TFAClientPaymentInvoiceViewModel> GetClientPaymentInvoice(int TFACompanyCustomerId, int TFAClientPaymentInvoiceId);
    }
    #endregion


    #region Member
    public class TFAClientPaymentService : ITFAClientPaymentService
    {
        private ITFAClientPaymentRepository repository;
        private IAdminUnitOfWork _UnitOfWork;

        ITFAClientPaymentDetailService serviceDetail;
        private ICmnMonthService serviceMonth;
        AdminDatabaseFactory dbfactory = new AdminDatabaseFactory();
        AdminDatabaseFactory dbfac = new AdminDatabaseFactory();
        public TFAClientPaymentService(ITFAClientPaymentRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;

            serviceDetail = new TFAClientPaymentDetailService(new TFAClientPaymentDetailRepository(dbfactory), new AdminUnitOfWork(dbfactory));
            serviceMonth = new CmnMonthService(new CmnMonthRepository(dbfac), new AdminUnitOfWork(dbfac));
        }

        public Operation Save(TFAClientPayment obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                obj.RefNo = repository.GetRefNo(obj.TFACompanyCustomerId);
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

        public Operation SaveWithTransaction(TFAClientPayment obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                obj.RefNo = repository.GetRefNo(obj.TFACompanyCustomerId);
                objOperation.OperationId = repository.AddEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(TFAClientPayment obj)
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

        public Operation UpdateWithTransaction(TFAClientPayment obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Update(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Delete(TFAClientPayment obj)
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

        public TFAClientPayment GetById(long id)
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

        public TFAClientPayment GetByTransactionID(string trxID)
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

        public List<TFAClientPayment> GetPaymentByDate(DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return repository.GetMany(w => w.Date >= dateFrom && w.Date <= dateTo).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AnFClientPaymentDetailViewModel> GetMonthlyPaidAndPayableByYearAndClienId(long paymentId, int yearId, int clientId, string domain)
        {
            try
            {
                List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
                List<CmnMonth> months = serviceMonth.GetMonthByYearId(yearId);
                List<ScpSubscriberViewModel> listSubscriber = Technofair.Lib.Utilities.Helper.GetSubscriberByDomainAndYear(domain, yearId);
                AnFClientPackageViewModel objPackage = GetPackageInfoByClientId(domain, clientId);
                if (objPackage != null && objPackage.Id > 0)
                {
                    foreach (CmnMonth m in months)
                    {
                        AnFClientPaymentDetailViewModel objF = new AnFClientPaymentDetailViewModel();
                        objF.CmnMonthId = m.Id;
                        objF.Month = m.Name;

                        objF.ClientPackage = objPackage;
                        TFAClientPaymentDetail objExist = serviceDetail.GetByYearAndMonthId(yearId, m.Id);
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
                                // objF.Amount = (decimal)objPackage.Amount;
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

        public List<AnFClientPaymentDetailViewModel> GetMonthlyPayableByClientAndYearId(int yearId, int clientId, string domain)
        {
            try
            {
                List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
                List<CmnMonth> months = serviceMonth.GetMonthByYearId(yearId);
                List<ScpSubscriberViewModel> listSubscriber = Technofair.Lib.Utilities.Helper.GetSubscriberByDomainAndYear(domain, yearId);
                AnFClientPackageViewModel objPackage = GetPackageInfoByClientId(domain, clientId);
                if (objPackage != null && objPackage.Id > 0)
                {
                    foreach (CmnMonth m in months)
                    {
                        TFAClientPaymentDetail objExist = serviceDetail.GetByYearAndMonthId(yearId, m.Id);
                        if (objExist == null)
                        {
                            AnFClientPaymentDetailViewModel objF = new AnFClientPaymentDetailViewModel();
                            objF.CmnMonthId = m.Id;
                            objF.Month = m.Name;
                            if (listSubscriber != null && listSubscriber.Count > 0)
                            {
                                objF.Quantity = listSubscriber.Count;
                            }

                            objF.ClientPackage = objPackage;
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
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnFPaymentMethod> GetCompanyPaymentMethod(string domain)
        {
            List<AnFPaymentMethod> list = new List<AnFPaymentMethod>();

            //DataTable dt = repository.GetCompanyPaymentMethod();
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        list.Add(((AnFPaymentMethod)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFPaymentMethod))));
            //    }
            //}
            return list;
        }

        private AnFClientPackageViewModel GetPackageInfoByClientId(string domain, int clientId)
        {
            List<AnFClientPackageViewModel> packages = GetPackageByClientId(domain, clientId);
            AnFClientPackageViewModel objPackage = new AnFClientPackageViewModel();
            if (packages != null && packages.Count > 0)
            {
                objPackage = packages.Where(w => w.IsActive).OrderByDescending(o => o.Date).FirstOrDefault();
            }
            return objPackage;
        }
        private List<AnFClientPackageViewModel> GetPackageByClientId(string domain, int clientId)
        {
            List<AnFClientPackageViewModel> list = new List<AnFClientPackageViewModel>();
            //DataTable dt = repository.GetPackageByClientId(clientId);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        list.Add(((AnFClientPackageViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPackageViewModel))));
            //    }
            //}
            return list;
        }

        private AnFClientPackageViewModel GetPackageByClientPackageId(string domain, int clientPackageId)
        {
            AnFClientPackageViewModel obj = null;
            //DataTable dt = repository.GetPackageByClientPackageId(clientPackageId);
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    obj = new AnFClientPackageViewModel();
            //    foreach (DataRow row in dt.Rows)
            //    {
            //        obj = (AnFClientPackageViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPackageViewModel));
            //    }
            //}
            return obj;
        }
        public ReportAnFClientPaymentViewModel GetPaymentByPaymentId(long paymentId, string domain)
        {
            try
            {
                ReportAnFClientPaymentViewModel obj = null;
                //DataTable dt = repository.GetPaymentByPaymentId(paymentId, domain);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    obj = new ReportAnFClientPaymentViewModel();
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        obj = (ReportAnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(ReportAnFClientPaymentViewModel));
                //    }

                //    if (obj.DueDate == null)
                //    {
                //        obj.DueDate = obj.Date.AddDays(10);//10 days from issue date
                //    }
                //    obj.AmountInWord = Technofair.Lib.Utilities.Helper.NumberToCurrencyText(obj.TotalAmount);
                //    obj.Barcode = obj.RefNo.Substring(11);

                //    if (obj.AnFPaymentMethodId > 0)
                //    {
                //        List<AnFPaymentMethod> methods = GetCompanyPaymentMethod(domain);
                //        AnFPaymentMethod objMethod = methods.Where(w => w.Id == obj.AnFPaymentMethodId).FirstOrDefault();
                //        if (objMethod != null)
                //        {
                //            obj.PaymentMethod = objMethod.Name;
                //        }
                //    }

                //    List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
                //    DataTable dtD = repository.GetPaymentDetailByPaymentId(paymentId, domain);
                //    if (dtD != null && dtD.Rows.Count > 0)
                //    {
                //        foreach (DataRow row in dtD.Rows)
                //        {
                //            list.Add(((AnFClientPaymentDetailViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentDetailViewModel))));
                //        }

                //        if (list.Count > 0)
                //        {
                //            AnFCompanyServiceType objServiceType = new AnFCompanyServiceType();
                //            DataTable dtST = repository.GetCompanyServiceTypeByServiceTypeId(list[0].AnFCompanyServiceTypeId);
                //            if (dtST != null && dtST.Rows.Count > 0)
                //            {
                //                foreach (DataRow row in dtST.Rows)
                //                {
                //                    objServiceType = ((AnFCompanyServiceType)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFCompanyServiceType)));
                //                }
                //                if (objServiceType != null)
                //                {
                //                    list[0].ServiceType = objServiceType.Name;
                //                }
                //            }
                //            obj.Details = list;
                //        }
                //    }
                //}
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AnFClientPaymentViewModel GetPaymentInfoByPaymentId(long paymentId, string domain)
        {
            try
            {
                AnFClientPaymentViewModel obj = null;
                //DataTable dt = repository.GetPaymentByPaymentId(paymentId, domain);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    obj = new AnFClientPaymentViewModel();
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        obj = (AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel));
                //    }
                //}
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnFClientPaymentDetailViewModel> GetMonthlyPaidByPaymentId(long paymentId, string domain)
        {
            try
            {
                List<AnFClientPaymentDetailViewModel> list = new List<AnFClientPaymentDetailViewModel>();
                //DataTable dtD = repository.GetPaymentDetailByPaymentId(paymentId, domain);
                //if (dtD != null && dtD.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dtD.Rows)
                //    {
                //        list.Add(((AnFClientPaymentDetailViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentDetailViewModel))));
                //    }

                //    AnFClientPaymentViewModel objPayment = GetPaymentInfoByPaymentId(paymentId, domain);
                //    if (objPayment != null && objPayment.AnFClientPackageId > 0)
                //    {
                //        AnFClientPackageViewModel objPackage = GetPackageByClientPackageId(domain, objPayment.AnFClientPackageId);
                //        if (objPackage != null)
                //        {
                //            for (int i = 0; i < list.Count; i++)
                //            {
                //                list[i].ClientPackage = objPackage;
                //            }
                //        }
                //    }

                //}
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AnFClientPaymentViewModel> GetPaymentByDateAndDomain(DateTime dateFrom, DateTime dateTo, string domain, bool? isAll)
        {
            try
            {
                List<AnFClientPaymentViewModel> list = new List<AnFClientPaymentViewModel>();
                //DataTable dt = repository.GetPaymentByDateAndDomain(dateFrom, dateTo, domain, isAll);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        list.Add((AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel)));
                //    }

                //    foreach (AnFClientPaymentViewModel obj in list)
                //    {
                //        if (obj.AnFPaymentMethodId > 0)
                //        {
                //            List<AnFPaymentMethod> methods = GetCompanyPaymentMethod(domain);
                //            AnFPaymentMethod objMethod = methods.Where(w => w.Id == obj.AnFPaymentMethodId).FirstOrDefault();
                //            if (objMethod != null)
                //            {
                //                obj.PaymentMethod = objMethod.Name;
                //            }
                //        }
                //    }
                //}
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<AnFClientPaymentViewModel> GetPaymentByDateAndDomainForCompany(DateTime dateFrom, DateTime dateTo, string domain, Int16? paymentStatus)
        //{
        //    try
        //    {
        //        List<AnFClientPaymentViewModel> list = new List<AnFClientPaymentViewModel>();
        //        DataTable dt = repository.GetPaymentByDateAndDomainForCompany(dateFrom, dateTo, domain, paymentStatus);
        //        if (dt != null && dt.Rows.Count > 0)
        //        {
        //            foreach (DataRow row in dt.Rows)
        //            {
        //                list.Add((AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel)));
        //            }

        //            foreach (AnFClientPaymentViewModel obj in list)
        //            {
        //                if (obj.AnFPaymentMethodId != null && obj.AnFPaymentMethodId > 0)
        //                {
        //                    List<AnFPaymentMethod> methods = GetCompanyPaymentMethod(domain);
        //                    AnFPaymentMethod objMethod = methods.Where(w => w.Id == obj.AnFPaymentMethodId).FirstOrDefault();
        //                    if (objMethod != null)
        //                    {
        //                        obj.PaymentMethod = objMethod.Name;
        //                    }
        //                }
        //            }
        //        }
        //        return list;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public Operation InsertPayment(AnFClientPayment obj, string domain)
        //{
        //    Operation objOperation = new Operation { Success = false };
        //    try
        //    {
        //        obj.RefNo = repository.GetIndivisualRefNo(domain);
        //        long ret = repository.InsertPayment(obj, domain);
        //        if (ret > 0)
        //        {
        //            objOperation.OperationId = ret;
        //            objOperation.Success = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return objOperation;
        //}
        //public Operation UpdateCompanyPayment(AnFClientPayment obj, string domain)
        //{
        //    Operation objOperation = new Operation { Success = false };
        //    try
        //    {
        //        long ret = repository.UpdateCompanyPayment(obj, domain);
        //        if (ret > 0)
        //        {
        //            objOperation.OperationId = ret;
        //            objOperation.Success = true;
        //        }
        //        return objOperation;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public List<AnFClientPaymentViewModel> GetPayableByClient(string domain)
        {
            try
            {
                List<AnFClientPaymentViewModel> list = new List<AnFClientPaymentViewModel>();
                //DataTable dt = repository.GetPayableByClient(domain);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        list.Add((AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel)));
                //    }
                //}
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public AnFClientPaymentViewModel GetPaymentByClientAndTransactionID(string domain, string trxID)
        {
            try
            {
                AnFClientPaymentViewModel obj = null;
                //DataTable dt = repository.GetPaymentByClientAndTransactionID(domain, trxID);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    obj = new AnFClientPaymentViewModel();
                //    foreach (DataRow row in dt.Rows)
                //    {
                //        obj = (AnFClientPaymentViewModel)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(AnFClientPaymentViewModel));
                //    }
                //}
                return obj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFAClientPaymentInvoiceViewModel> GetClientPaymentInvoice(int TFACompanyCustomerId, int TFAClientPaymentInvoiceId)
        {
            return repository.GetClientPaymentInvoice(TFACompanyCustomerId, TFAClientPaymentInvoiceId);
        }
    }

    #endregion
}
