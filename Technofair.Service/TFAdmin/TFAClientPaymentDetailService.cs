using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Technofair.Data.Infrastructure.TFAdmin;
using Technofair.Lib.Model;
using TFSMS.Admin.Model.TFAdmin;
using TFSMS.Admin.Model.ViewModel.TFAdmin;
using TFSMS.Admin.Data.Infrastructure;
using TFSMS.Admin.Data.Repository.TFAdmin;
using Technofair.Model.ViewModel.TFAdmin;

namespace TFSMS.Admin.Service.TFAdmin
{
    
    #region Interface
    public interface ITFAClientPaymentDetailService
    {
        Operation Save(TFAClientPaymentDetail obj);
        Operation SaveWithTransaction(TFAClientPaymentDetail obj);
        Operation Update(TFAClientPaymentDetail obj);
        Operation UpdateWithTransaction(TFAClientPaymentDetail obj);
        Operation Delete(TFAClientPaymentDetail obj);
        Operation DeleteWithTransaction(TFAClientPaymentDetail obj);
        TFAClientPaymentDetail GetById(long id);
        TFAClientPaymentDetail GetByClientPaymentDetailId(long clientPaymentDetailId);
        List<TFAClientPaymentDetail> GetByPaymentId(long paymentId);
        TFAClientPaymentDetail GetByPaymentAndMonthId(long paymentId, Int16 monthId);
        TFAClientPaymentDetail GetDetailByYearAndMonthId(int yearId, Int16 monthId, string domain);
        TFAClientPaymentDetail GetByYearAndMonthId(int calYearId, Int16 monthId);
        Operation InsertPaymentDetail(TFAClientPaymentDetail obj, string domain);
        Operation UpdatePaymentDetail(TFAClientPaymentDetail obj, string domain);
        Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey);
        Operation DeleteByIdAndDomain(long Id, string domain);
        List<TFAClientPaymentDetail> GetDetailByPaymentIdAndDomain(long paymentId, string domain);
        Task<TFAClientPaymentDetail> clientPaymentDetails(int companyCustomerId, int monthId, int year);
        CompanyCustomerWithClientPackageViewModel GetClientBillByClientPaymentDetailId(int tfaCompanyCustomerId, Int64 tfaClientPaymentDetailId);
        ClientPaymentViewModel GetClientPackageExpireDate(string appKey);


    }
    #endregion


    #region Member
    public class TFAClientPaymentDetailService : ITFAClientPaymentDetailService
    {
        private ITFAClientPaymentDetailRepository repository;
        private IAdminUnitOfWork _UnitOfWork;
        public TFAClientPaymentDetailService(ITFAClientPaymentDetailRepository _repository, IAdminUnitOfWork unitOfWork)
        {
            this.repository = _repository;
            this._UnitOfWork = unitOfWork;
        }

        public Operation Save(TFAClientPaymentDetail obj)
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
        public Operation SaveWithTransaction(TFAClientPaymentDetail obj)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                objOperation.OperationId = repository.AddEntity(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public Operation Update(TFAClientPaymentDetail obj)
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
        public Operation UpdateWithTransaction(TFAClientPaymentDetail obj)
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

        public Operation Delete(TFAClientPaymentDetail obj)
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
        public Operation DeleteWithTransaction(TFAClientPaymentDetail obj)
        {
            Operation objOperation = new Operation { Success = false, OperationId = obj.Id };
            try
            {
                repository.Delete(obj);
                objOperation.Success = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public TFAClientPaymentDetail GetById(long id)
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

        public TFAClientPaymentDetail GetByClientPaymentDetailId(long clientPaymentDetailId)
        {
            try
            {
                return repository.GetByClientPaymentDetailId(clientPaymentDetailId);
                //return repository.GetMany(r => r.Id == clientPaymentDetailId).SingleOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<TFAClientPaymentDetail> GetByPaymentId(long paymentId)
        {
            try
            {
                return repository.GetMany(r => r.TFAClientPaymentId == paymentId).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public TFAClientPaymentDetail GetByPaymentAndMonthId(long paymentId, Int16 monthId)
        {
            try
            {
                return repository.GetMany(r => r.TFAClientPaymentId == paymentId && r.TFAMonthId == monthId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public TFAClientPaymentDetail GetByYearAndMonthId(int yearId, Int16 monthId)
        {
            try
            {
                return repository.GetByYearAndMonthId(yearId, monthId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Operation InsertPaymentDetail(TFAClientPaymentDetail obj, string domain)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                long ret = repository.InsertPaymentDetail(obj, domain);
                if (ret > 0)
                {
                    objOperation.OperationId = ret;
                    objOperation.Success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public Operation UpdatePaymentDetail(TFAClientPaymentDetail obj, string domain)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                long ret = repository.UpdatePaymentDetail(obj, domain);
                if (ret > 0)
                {
                    objOperation.Success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }

        public async Task<TFAClientPaymentDetail> GetClientPaymentDetailByAppKey(string appKey)
        {
            return await repository.GetClientPaymentDetailByAppKey(appKey);
        }
        public Operation DeleteByIdAndDomain(long Id, string domain)
        {
            Operation objOperation = new Operation { Success = false };
            try
            {
                long ret = repository.DeleteByIdAndDomain(Id, domain);
                if (ret > 0)
                {
                    objOperation.Success = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return objOperation;
        }
        public TFAClientPaymentDetail GetDetailByYearAndMonthId(int yearId, Int16 monthId, string domain)
        {
            try
            {
                TFAClientPaymentDetail obj = null;
                DataTable dt = repository.GetDetailByYearAndMonthId(yearId, monthId, domain);
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

        public List<TFAClientPaymentDetail> GetDetailByPaymentIdAndDomain(long paymentId, string domain)
        {
            try
            {
                List<TFAClientPaymentDetail> list = null;
                DataTable dt = repository.GetDetailByPaymentIdAndDomain(paymentId, domain);
                if (dt != null && dt.Rows.Count > 0)
                {
                    list = new List<TFAClientPaymentDetail>();
                    foreach (DataRow row in dt.Rows)
                    {
                        list.Add((TFAClientPaymentDetail)Technofair.Lib.Utilities.Helper.FillTo(row, typeof(TFAClientPaymentDetail)));
                    }
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        
       public async Task<TFAClientPaymentDetail> clientPaymentDetails(int companyCustomerId, int monthId, int year)
        {
            try
            {
                return await repository.clientPaymentDetails(companyCustomerId, monthId, year);   
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CompanyCustomerWithClientPackageViewModel GetClientBillByClientPaymentDetailId(int tfaCompanyCustomerId, Int64 tfaClientPaymentDetailId)
        {
            return repository.GetClientBillByClientPaymentDetailId(tfaCompanyCustomerId, tfaClientPaymentDetailId);
        }

        public ClientPaymentViewModel GetClientPackageExpireDate(string appKey)
        {
            return repository.GetClientPackageExpireDate(appKey);
        }
    }

    #endregion
}
